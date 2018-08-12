using Fuzzlyn.ProbabilityDistributions;
using Fuzzlyn.Statics;
using Fuzzlyn.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Methods
{
    internal class FuncGenerator
    {
        private readonly List<ScopeFrame> _scope = new List<ScopeFrame>();
        private readonly List<FuncGenerator> _funcs;
        private readonly int _funcIndex;
        private int _varCounter;
        private int _level;
        private int _finallyCount;
        private readonly Func<string> _genChecksumSiteId;

        public FuncGenerator(
            List<FuncGenerator> funcs,
            Randomizer random,
            TypeManager types,
            StaticsManager statics,
            Func<string> genChecksumSiteId)
        {
            _funcs = funcs;
            Random = random;
            Types = types;
            Statics = statics;
            _genChecksumSiteId = genChecksumSiteId;

            _funcIndex = funcs.Count;
            Name = $"M{funcs.Count}";

            funcs.Add(this);
        }

        public Randomizer Random { get; }
        public FuzzlynOptions Options => Random.Options;
        public TypeManager Types { get; }
        public StaticsManager Statics { get; }
        public BlockSyntax Body { get; private set; }
        public FuzzType ReturnType { get; private set; }
        public VariableIdentifier[] Parameters { get; private set; }
        public string Name { get; }
        public int NumStatements { get; private set; }

        public MethodDeclarationSyntax Output()
        {
            TypeSyntax retType;
            if (ReturnType == null)
                retType = PredefinedType(Token(SyntaxKind.VoidKeyword));
            else
                retType = ReturnType.GenReferenceTo();

            IEnumerable<ParameterSyntax> GenParameters()
            {
                foreach (VariableIdentifier pm in Parameters)
                {
                    if (pm.Type is RefType rt)
                    {
                        yield return
                            Parameter(Identifier(pm.Name))
                            .WithType(rt.InnerType.GenReferenceTo())
                            .WithModifiers(TokenList(Token(SyntaxKind.RefKeyword)));
                    }
                    else
                    {
                        yield return
                            Parameter(Identifier(pm.Name))
                            .WithType(pm.Type.GenReferenceTo());
                    }
                }
            }

            ParameterListSyntax parameters = ParameterList(SeparatedList(GenParameters()));

            return
                MethodDeclaration(retType, Name)
                .WithModifiers(TokenList(Token(SyntaxKind.StaticKeyword)))
                .WithParameterList(parameters)
                .WithBody(Body);
        }

        public void Generate(FuzzType returnType, bool randomizeParams)
        {
            ReturnType = returnType;

            if (randomizeParams)
            {
                int numArgs = Options.MethodParameterCountDist.Sample(Random.Rng);
                Parameters = new VariableIdentifier[numArgs];
                for (int i = 0; i < Parameters.Length; i++)
                {
                    FuzzType type = Types.PickType(Options.ParameterIsByRefProb);
                    string name = $"arg{i}";
                    // A ref to a by-ref parameter can escape to at least its parent method
                    int refEscapeScope = type is RefType ? 1 : 0;
                    Parameters[i] = new VariableIdentifier(type, name, refEscapeScope);
                }
            }
            else
                Parameters = Array.Empty<VariableIdentifier>();

            _level = -1;
            Body = GenBlock(true);
        }

        private StatementSyntax GenStatement(bool allowReturn = true)
        {
            if (_finallyCount > 0)
                allowReturn = false;

            while (true)
            {
                StatementKind kind =
                    (StatementKind)Options.StatementTypeDist.Sample(Random.Rng);

                if ((kind == StatementKind.Block || kind == StatementKind.If || kind == StatementKind.TryFinally) &&
                    ShouldRejectRecursion())
                    continue;

                if (kind == StatementKind.Return && !allowReturn)
                    continue;

                switch (kind)
                {
                    case StatementKind.Block:
                        return GenBlock(false);
                    case StatementKind.Assignment:
                        return GenAssignmentStatement();
                    case StatementKind.Call:
                        return GenCallStatement(tryExisting: ShouldRejectRecursion());
                    case StatementKind.If:
                        return GenIf();
                    case StatementKind.TryFinally:
                        return GenTryFinally();
                    case StatementKind.Return:
                        return GenReturn();
                    default:
                        throw new Exception("Unreachable");
                }
            }

            bool ShouldRejectRecursion()
                => Options.StatementRejection.Reject(_level, Random.Rng);
        }

        private BlockSyntax GenBlock(bool root, int numStatements = -1)
        {
            if (numStatements == -1)
                numStatements = Options.BlockStatementCountDist.Sample(Random.Rng);

            _level++;

            ScopeFrame scope = new ScopeFrame();
            _scope.Add(scope);

            if (root)
                scope.Variables.AddRange(Parameters);

            BlockSyntax block = Block(GenStatements());

            _scope.RemoveAt(_scope.Count - 1);

            _level--;

            return block;

            IEnumerable<StatementSyntax> GenStatements()
            {
                StatementSyntax retStmt = null;
                int numGenerated = 0;
                while (true)
                {
                    StatementSyntax stmt = GenStatement(allowReturn: !root);

                    if (stmt is ReturnStatementSyntax)
                    {
                        retStmt = stmt;
                        break;
                    }

                    NumStatements++;
                    yield return stmt;

                    numGenerated++;
                    if (numGenerated < numStatements)
                        continue;

                    // For first block we ensure we get a minimum amount of statements
                    if (root && _funcIndex == 0)
                    {
                        if (_funcs.Sum(f => f.NumStatements) < Options.ProgramMinStatements)
                            continue;
                    }

                    break;
                }

                if (Options.EnableChecksumming)
                {
                    foreach (StatementSyntax stmt in GenChecksumming(scope.Variables, _genChecksumSiteId))
                        yield return stmt;
                }

                if (root && retStmt == null && ReturnType != null)
                    retStmt = GenReturn();

                if (retStmt != null)
                {
                    NumStatements++;
                    yield return retStmt;
                }
            }
        }

        private StatementSyntax GenAssignmentStatement()
        {
            LValueInfo lvalue = null;
            if (!Random.FlipCoin(Options.AssignToNewVarProb))
                lvalue = GenExistingLValue(null, int.MinValue);

            if (lvalue == null)
            {
                FuzzType newType = Types.PickType(Options.LocalIsByRefProb);
                // Determine if we should create a new local. We do this with a certain probabilty,
                // or always if the new type is a by-ref type (we cannot have static by-refs).
                if (newType is RefType || Random.FlipCoin(Options.NewVarIsLocalProb))
                {
                    VariableIdentifier variable;
                    string varName = $"var{_varCounter++}";
                    ExpressionSyntax rhs;
                    if (newType is RefType newRt)
                    {
                        LValueInfo rhsLV = GenLValue(newRt.InnerType, int.MinValue);
                        variable = new VariableIdentifier(newType, varName, rhsLV.RefEscapeScope);
                        rhs = RefExpression(rhsLV.Expression);
                    }
                    else
                    {
                        rhs = GenExpression(newType);
                        variable = new VariableIdentifier(newType, varName, -(_scope.Count - 1));
                    }

                    LocalDeclarationStatementSyntax decl =
                        LocalDeclarationStatement(
                            VariableDeclaration(
                                variable.Type.GenReferenceTo(),
                                SingletonSeparatedList(
                                    VariableDeclarator(variable.Name)
                                    .WithInitializer(
                                        EqualsValueClause(rhs)))));

                    _scope.Last().Variables.Add(variable);

                    return decl;
                }

                StaticField newStatic = Statics.GenerateNewField(newType);
                lvalue = new LValueInfo(IdentifierName(newStatic.Var.Name), newType, int.MaxValue);
            }

            // Determine if we should generate a ref-reassignment. In that case we cannot do anything
            // clever, like generate compound assignments.
            FuzzType rhsType = lvalue.Type;
            if (lvalue.Type is RefType rt)
            {
                if (Random.FlipCoin(Options.AssignGenRefReassignProb))
                {
                    RefExpressionSyntax refRhs = RefExpression(GenLValue(rt.InnerType, lvalue.RefEscapeScope).Expression);
                    return
                        ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                lvalue.Expression,
                                refRhs));
                }

                // We have a ref-type, but are not generating a ref-reassign, so lift the type and make a normal assignment.
                rhsType = rt.InnerType;
            }

            SyntaxKind assignmentKind = SyntaxKind.SimpleAssignmentExpression;
            // Determine if we should generate compound assignment.
            if (rhsType.AllowedAdditionalAssignmentKinds.Length > 0 && Random.FlipCoin(Options.CompoundAssignmentProb))
                assignmentKind = Random.NextElement(rhsType.AllowedAdditionalAssignmentKinds);

            // Early our for simple cases.
            if (assignmentKind == SyntaxKind.PreIncrementExpression ||
                assignmentKind == SyntaxKind.PreDecrementExpression)
            {
                return ExpressionStatement(PrefixUnaryExpression(assignmentKind, lvalue.Expression));
            }

            if (assignmentKind == SyntaxKind.PostIncrementExpression ||
                assignmentKind == SyntaxKind.PostDecrementExpression)
            {
                return ExpressionStatement(PostfixUnaryExpression(assignmentKind, lvalue.Expression));
            }

            // Right operand of shifts are always ints.
            if (assignmentKind == SyntaxKind.LeftShiftAssignmentExpression ||
                assignmentKind == SyntaxKind.RightShiftAssignmentExpression)
            {
                rhsType = Types.GetPrimitiveType(SyntaxKind.IntKeyword);
            }

            ExpressionSyntax right = GenExpression(rhsType);
            // For modulo and division we don't want to throw divide-by-zero exceptions,
            // so always or right-hand-side with 1.
            if (assignmentKind == SyntaxKind.ModuloAssignmentExpression ||
                assignmentKind == SyntaxKind.DivideAssignmentExpression)
            {
                right =
                    CastExpression(
                        rhsType.GenReferenceTo(),
                        ParenthesizedExpression(
                            BinaryExpression(
                                SyntaxKind.BitwiseOrExpression,
                                ParenthesizeIfNecessary(right),
                                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1)))));
            }

            return ExpressionStatement(AssignmentExpression(assignmentKind, lvalue.Expression, right));
        }

        private StatementSyntax GenCallStatement(bool tryExisting)
        {
            // If we are supposed to try existing first, then do not allow new
            ExpressionSyntax call = GenCall(null, allowNew: !tryExisting);

            while (call == null)
            {
                // There are no existing, so allow new until we get a new one
                call = GenCall(null, true);
            }

            return ExpressionStatement(call);
        }

        private StatementSyntax GenIf()
        {
            StatementSyntax gen = null;
            ExpressionSyntax guard = GenExpression(new PrimitiveType(SyntaxKind.BoolKeyword));
            if (Random.FlipCoin(0.5))
            {
                gen = IfStatement(guard, GenBlock(false));
            }
            else
            {
                gen = IfStatement(guard, GenBlock(false), ElseClause(GenBlock(false)));
            }
            return gen;
        }

        private StatementSyntax GenTryFinally()
        {
            int numStatements = Options.BlockStatementCountDist.Sample(Random.Rng);
            int tryStatements = Random.Next(numStatements);
            int finallyStatements = numStatements - tryStatements;

            BlockSyntax body = GenBlock(false, tryStatements);
            _finallyCount++;
            BlockSyntax finallyBody = GenBlock(false, finallyStatements);
            _finallyCount--;
            return
                TryStatement(
                    body,
                    List<CatchClauseSyntax>(),
                    FinallyClause(finallyBody));
        }

        private StatementSyntax GenReturn()
        {
            if (ReturnType == null)
                return ReturnStatement();

            ExpressionSyntax expr;
            if (ReturnType is RefType rt)
                expr = RefExpression(GenLValue(rt.InnerType, 1).Expression);
            else
                expr = GenExpression(ReturnType);

            return ReturnStatement(expr);
        }

        private ExpressionSyntax GenExpression(FuzzType type)
        {
            Debug.Assert(!(type is RefType));
            ExpressionSyntax gen;
            do
            {
                ExpressionKind kind = (ExpressionKind)Options.ExpressionTypeDist.Sample(Random.Rng);
                switch (kind)
                {
                    case ExpressionKind.MemberAccess:
                        gen = GenMemberAccess(type)?.Expression;
                        break;
                    case ExpressionKind.Literal:
                        gen = GenLiteral(type);
                        break;
                    case ExpressionKind.Binary:
                        gen = GenBinary(type);
                        break;
                    case ExpressionKind.Call:
                        gen = GenCall(type, true);
                        break;
                    case ExpressionKind.Increment:
                        gen = GenIncDec(type, true);
                        break;
                    case ExpressionKind.Decrement:
                        gen = GenIncDec(type, false);
                        break;
                    case ExpressionKind.NewObject:
                        gen = GenNewObject(type);
                        break;
                    case ExpressionKind.Cast:
                        gen = null;
                        continue;
                        gen = GenCast(type);
                        break;
                    default:
                        throw new Exception("Unreachable");
                }
            }
            while (gen == null);

            return gen;
        }

        /// <summary>Returns an lvalue.</summary>
        private LValueInfo GenLValue(FuzzType type, int minRefEscapeScope)
        {
            Debug.Assert(type != null);

            LValueInfo lv = GenExistingLValue(type, minRefEscapeScope);

            if (lv == null)
            {
                StaticField newStatic = Statics.GenerateNewField(type);
                lv = new LValueInfo(IdentifierName(newStatic.Var.Name), type, int.MaxValue);
            }

            return lv;
        }

        private LValueInfo GenExistingLValue(FuzzType type, int minRefEscapeScope)
        {
            Debug.Assert(type == null || !(type is RefType));

            LValueKind kind = (LValueKind)Options.ExistingLValueDist.Sample(Random.Rng);

            if (kind == LValueKind.RefReturningCall)
            {
                List<FuncGenerator> refReturningFuncs = new List<FuncGenerator>();
                foreach (FuncGenerator func in _funcs.Skip(_funcIndex + 1))
                {
                    if (!(func.ReturnType is RefType rt))
                        continue;

                    if (type == null || type == rt.InnerType)
                        refReturningFuncs.Add(func);
                }

                // If we don't have a ref-returning function, fall back to local/static by retrying.
                // The reason we don't always fall back to trying other options is that otherwise
                // we may stack overflow in cases like ref int M(ref int a), if we have no locals
                // or statics of type int. We would keep generating method calls in this case.
                // This is kind of a hack, although it is pretty natural to pick statics/locals for
                // lvalues, so it is not that big of a deal.
                if (refReturningFuncs.Count == 0)
                    return GenExistingLValue(type, minRefEscapeScope);

                FuncGenerator funcToCall = Random.NextElement(refReturningFuncs);

                // When calling a func that returns a by-ref, we need to take into account that the C# compiler
                // performs 'escape-analysis' on by-refs. If we want to produce a non-local by-ref we are only
                // allowed to pass non-local by-refs. The following example illustrates the analysis performed
                // by the compiler:
                // ref int M(ref int b, ref int c) {
                //   int a = 2;
                //   return ref Max(ref a, ref b); // compiler error, returned by-ref could point to 'a' and escape
                //   return ref Max(ref b, ref c); // ok, returned ref cannot point to local
                // }
                // ref int Max(ref int a, ref int b) { ... }
                // This is the reason for the second arg passed to GenArgs. For example, if we want a call to return
                // a ref that may escape, we need a minimum ref escape scope of 1, since 0 could pass refs to locals,
                // and the result of that call would not be valid to return.
                ArgumentSyntax[] args = GenArgs(funcToCall, minRefEscapeScope, out int argsMinRefEscapeScope);
                InvocationExpressionSyntax invoc =
                    InvocationExpression(
                        IdentifierName(funcToCall.Name),
                        ArgumentList(
                            SeparatedList(
                                args)));

                return new LValueInfo(invoc, ((RefType)funcToCall.ReturnType).InnerType, argsMinRefEscapeScope);
            }

            List<LValueInfo> lvalues = CollectVariablePaths(type, minRefEscapeScope, kind == LValueKind.Local, kind == LValueKind.Static);
            if (lvalues.Count == 0)
            {
                // We typically fall back to generating a static, so just try to find a static if we found no local.
                if (kind != LValueKind.Local)
                    return null;

                lvalues = CollectVariablePaths(type, minRefEscapeScope, false, true);
                if (lvalues.Count == 0)
                    return null;
            }

            return Random.NextElement(lvalues);
        }

        /// <summary>
        /// Generates an access to a random member:
        /// * A static variable
        /// * A local variable
        /// * An argument
        /// * A field in one of these, if aggregate type
        /// * An element in one of these, if array type
        /// </summary>
        private LValueInfo GenMemberAccess(FuzzType ft)
        {
            List<LValueInfo> paths =
                Random.FlipCoin(Options.MemberAccessSelectLocalProb)
                ? CollectVariablePaths(ft, int.MinValue, true, false)
                : CollectVariablePaths(ft, int.MinValue, false, true);

            return paths.Count > 0 ? Random.NextElement(paths) : null;
        }

        private List<LValueInfo> CollectVariablePaths(FuzzType type, int minRefEscapeScope, bool collectLocals, bool collectStatics)
        {
            List<LValueInfo> paths = new List<LValueInfo>();

            if (collectLocals)
            {
                foreach (ScopeFrame sf in _scope)
                {
                    foreach (VariableIdentifier variable in sf.Variables)
                        AppendVariablePaths(paths, variable);
                }
            }

            if (collectStatics)
            {
                foreach (StaticField stat in Statics.Fields)
                    AppendVariablePaths(paths, stat.Var);
            }

            Debug.Assert(type == null || !(type is RefType), "Cannot collect variables of ref type");

            // Verify that a type is allowed. Often we want to implicitly promote
            // a by-ref to its inner type, because we are taking a ref anyway or using
            // its value, and it is automatically promoted.
            // This checks for both of these cases:
            // ref int lhs = ...;
            // int rhs1 = ...;
            // ref int rhs2 = ...;
            // lhs = ref rhs1; // must be supported
            // lhs = ref rhs2; // must be supported
            bool IsAllowedType(FuzzType other)
                => type == null || (other == type || (other is RefType rt && rt.InnerType == type));

            paths.RemoveAll(lv => lv.RefEscapeScope < minRefEscapeScope || !IsAllowedType(lv.Type));
            return paths;
        }

        private ExpressionSyntax GenLiteral(FuzzType type)
        {
            return LiteralGenerator.GenLiteral(Random, type);
        }

        private ExpressionSyntax GenBinary(FuzzType type)
        {
            if (!(type is PrimitiveType pt))
                return null;

            if (pt.Keyword == SyntaxKind.BoolKeyword)
                return GenBoolProducingBinary();

            return GenIntegralProducingBinary(pt);
        }

        private ExpressionSyntax GenBoolProducingBinary()
        {
            FuzzType boolType = Types.GetPrimitiveType(SyntaxKind.BoolKeyword);
            ExpressionSyntax left, right;
            SyntaxKind op = (SyntaxKind)Options.BinaryBoolDist.Sample(Random.Rng);
            if (op == SyntaxKind.LogicalAndExpression || op == SyntaxKind.LogicalOrExpression ||
                op == SyntaxKind.ExclusiveOrExpression || op == SyntaxKind.BitwiseAndExpression ||
                op == SyntaxKind.BitwiseOrExpression)
            {
                // &&, ||, ^, &, | must be between bools to produce bool
                left = GenExpression(boolType);
                right = GenExpression(boolType);
            }
            else if (op == SyntaxKind.EqualsExpression || op == SyntaxKind.NotEqualsExpression)
            {
                PrimitiveType leftType = Types.PickPrimitiveType(f => true);
                left = GenExpression(leftType);
                PrimitiveType rightType = Types.PickPrimitiveType(f => BinOpTable.Equality.GetResultType(leftType.Keyword, f.Keyword).HasValue);
                right = GenExpression(rightType);
            }
            else
            {
                Debug.Assert(op == SyntaxKind.LessThanOrEqualExpression || op == SyntaxKind.LessThanExpression ||
                             op == SyntaxKind.GreaterThanOrEqualExpression || op == SyntaxKind.GreaterThanExpression);

                PrimitiveType leftType = Types.PickPrimitiveType(f => f.Keyword != SyntaxKind.BoolKeyword);
                PrimitiveType rightType = Types.PickPrimitiveType(f => BinOpTable.Relop.GetResultType(leftType.Keyword, f.Keyword).HasValue);
                left = GenExpression(leftType);
                right = GenExpression(rightType);
            }

            return BinaryExpression(op, ParenthesizeIfNecessary(left), ParenthesizeIfNecessary(right));
        }

        private ExpressionSyntax ParenthesizeIfNecessary(ExpressionSyntax expr)
        {
            if (Helpers.RequiresParentheses(expr))
                return ParenthesizedExpression(expr);

            return expr;
        }

        private ExpressionSyntax GenIntegralProducingBinary(PrimitiveType type)
        {
            Debug.Assert(type.Info.IsIntegral);
            SyntaxKind op = (SyntaxKind)Options.BinaryIntegralDist.Sample(Random.Rng);

            BinOpTable table;
            if (op == SyntaxKind.LeftShiftExpression || op == SyntaxKind.RightShiftExpression)
                table = BinOpTable.Shifts;
            else
                table = BinOpTable.Arithmetic;

            PrimitiveType leftType = Types.PickPrimitiveType(f => f.Keyword != SyntaxKind.BoolKeyword);
            PrimitiveType rightType =
                Types.PickPrimitiveType(f => table.GetResultType(leftType.Keyword, f.Keyword).HasValue);

            ExpressionSyntax left = GenExpression(leftType);
            ExpressionSyntax right = GenExpression(rightType);
            while (left is LiteralExpressionSyntax && right is LiteralExpressionSyntax)
                right = GenExpression(rightType);

            if (op == SyntaxKind.ModuloExpression || op == SyntaxKind.DivideExpression)
            {
                right = CastExpression(
                    rightType.GenReferenceTo(),
                    ParenthesizedExpression(
                        BinaryExpression(
                            SyntaxKind.BitwiseOrExpression,
                            ParenthesizeIfNecessary(right),
                            LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1)))));
            }

            ExpressionSyntax expr = BinaryExpression(op, ParenthesizeIfNecessary(left), ParenthesizeIfNecessary(right));

            if (table.GetResultType(leftType.Keyword, rightType.Keyword) != type.Keyword)
                expr = CastExpression(type.GenReferenceTo(), ParenthesizedExpression(expr));

            return expr;
        }

        private ExpressionSyntax GenCall(FuzzType type, bool allowNew)
        {
            Debug.Assert(!(type is RefType), "Cannot GenCall to ref type -- use GenExistingLValue for that");

            FuncGenerator func;
            if (allowNew && Random.FlipCoin(Options.GenNewFunctionProb) && !Options.FuncGenRejection.Reject(_funcs.Count, Random.Rng))
            {
                type = type ?? Types.PickType(Options.ReturnTypeIsByRefProb);

                func = new FuncGenerator(_funcs, Random, Types, Statics, _genChecksumSiteId);
                func.Generate(type, true);
            }
            else
            {
                IEnumerable<FuncGenerator> funcs = _funcs.Skip(_funcIndex + 1);
                if (type != null)
                    funcs = funcs.Where(f => f.ReturnType.IsCastableTo(type) || (f.ReturnType is RefType rt && rt.InnerType.IsCastableTo(type)));

                List<FuncGenerator> list = funcs.ToList();
                if (list.Count == 0)
                    return null;

                func = Random.NextElement(list);
                type = type ?? func.ReturnType;
            }

            ArgumentSyntax[] args = GenArgs(func, 0, out _);
            InvocationExpressionSyntax invoc =
                InvocationExpression(
                    IdentifierName(func.Name),
                    ArgumentList(
                        SeparatedList(args)));

            if (func.ReturnType == type || func.ReturnType is RefType retRt && retRt.InnerType == type)
                return invoc;

            return CastExpression(type.GenReferenceTo(), invoc);
        }

        private ArgumentSyntax[] GenArgs(FuncGenerator funcToCall, int minRefEscapeScope, out int argsMinRefEscapeScope)
        {
            ArgumentSyntax[] args = new ArgumentSyntax[funcToCall.Parameters.Length];
            argsMinRefEscapeScope = int.MaxValue;

            for (int i = 0; i < args.Length; i++)
            {
                FuzzType paramType = funcToCall.Parameters[i].Type;
                if (paramType is RefType rt)
                {
                    LValueInfo lv = GenLValue(rt.InnerType, minRefEscapeScope);
                    argsMinRefEscapeScope = Math.Min(argsMinRefEscapeScope, lv.RefEscapeScope);
                    args[i] = Argument(lv.Expression).WithRefKindKeyword(Token(SyntaxKind.RefKeyword));
                }
                else
                {
                    args[i] = Argument(GenExpression(paramType));
                }
            }

            return args;
        }

        private ExpressionSyntax GenIncDec(FuzzType type, bool isIncrement)
        {
            SyntaxKind[] acceptedTypes =
            {
                SyntaxKind.UShortKeyword,
                SyntaxKind.LongKeyword,
                SyntaxKind.UIntKeyword,
                SyntaxKind.IntKeyword,
                SyntaxKind.UShortKeyword,
                SyntaxKind.ShortKeyword,
                SyntaxKind.ByteKeyword,
                SyntaxKind.SByteKeyword,
            };

            if (!(type is PrimitiveType pt) || !acceptedTypes.Contains(pt.Keyword))
                return null;

            LValueInfo subject = GenExistingLValue(type, int.MinValue);
            if (subject == null)
                return null;

            ExpressionSyntax gen = PostfixUnaryExpression(
                isIncrement ? SyntaxKind.PostIncrementExpression : SyntaxKind.PostDecrementExpression,
                subject.Expression);

            return gen;
        }

        private ExpressionSyntax GenNewObject(FuzzType type)
        {
            if (!(type is AggregateType at))
                return null;

            ObjectCreationExpressionSyntax creation =
                ObjectCreationExpression(type.GenReferenceTo())
                .WithArgumentList(
                    ArgumentList(
                        SeparatedList(
                            at.Fields.Select(f => Argument(GenExpression(f.Type))))));

            return creation;
        }

        private ExpressionSyntax GenCast(FuzzType type)
        {
            return CastExpression(type.GenReferenceTo(), GenExpression(type));
        }

        internal static IEnumerable<StatementSyntax> GenChecksumming(IEnumerable<VariableIdentifier> variables, Func<string> siteIdGenerator)
        {
            List<LValueInfo> paths = new List<LValueInfo>();
            foreach (VariableIdentifier variable in variables)
                AppendVariablePaths(paths, variable);

            paths.RemoveAll(lv => !(lv.Type is PrimitiveType) && !(lv.Type is RefType rt && rt.InnerType is PrimitiveType));

            foreach (LValueInfo lvalue in paths)
            {
                string checksumSiteId = siteIdGenerator();
                LiteralExpressionSyntax id =
                    LiteralExpression(
                        SyntaxKind.StringLiteralExpression,
                        Literal(checksumSiteId));

                ExpressionSyntax expr = lvalue.Expression;
                ExpressionStatementSyntax stmt = 
                    ExpressionStatement(
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("s_rt"),
                                IdentifierName("Checksum")))
                        .WithArgumentList(
                            ArgumentList(
                                SeparatedList(new[] { Argument(id), Argument(expr) }))));

                yield return stmt.WithAdditionalAnnotations(new SyntaxAnnotation("ChecksumSiteId", checksumSiteId));
            }
        }

        private static void AppendVariablePaths(List<LValueInfo> paths, VariableIdentifier var)
        {
            ExpressionSyntax varAccess = IdentifierName(var.Name);
            AddPathsRecursive(varAccess, var.Type, var.RefEscapeScope);

            void AddPathsRecursive(ExpressionSyntax curAccess, FuzzType curType, int curRefEscapeScope)
            {
                LValueInfo info = new LValueInfo(curAccess, curType, curRefEscapeScope);
                paths.Add(info);

                if (curType is RefType rt)
                    curType = rt.InnerType;

                switch (curType)
                {
                    case ArrayType arr:
                        AddPathsRecursive(
                            ElementAccessExpression(
                                curAccess,
                                BracketedArgumentList(
                                    SeparatedList(
                                        Enumerable.Repeat(
                                            Argument(LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))),
                                            arr.Rank)))),
                            arr.ElementType,
                            curRefEscapeScope: int.MaxValue);
                        break;
                    case AggregateType agg:
                        foreach (AggregateField field in agg.Fields)
                        {
                            AddPathsRecursive(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    curAccess,
                                    IdentifierName(field.Name)),
                                field.Type,
                                curRefEscapeScope: agg.IsClass ? int.MaxValue : curRefEscapeScope);
                        }
                        break;
                }
            }
        }
    }

    internal enum StatementKind
    {
        Block,
        Assignment,
        Call,
        If,
        Return,
        TryFinally,
    }

    internal enum ExpressionKind
    {
        MemberAccess,
        Literal,
        Binary,
        Ternary,
        Assignment,
        Call,
        Increment,
        Decrement,
        NewObject,
        Cast,
    }
}
