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

        public MethodDeclarationSyntax Output()
        {
            TypeSyntax retType;
            if (ReturnType == null)
                retType = PredefinedType(Token(SyntaxKind.VoidKeyword));
            else
                retType = ReturnType.GenReferenceTo();

            ParameterListSyntax parameters =
                ParameterList(
                    SeparatedList(
                        Parameters.Select(
                            (pt, i) => Parameter(Identifier(pt.Name))
                                       .WithType(pt.Type.GenReferenceTo()))));

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
                    var type = Types.PickType(Options.ParameterIsByRefProb);
                    string name = $"arg{i}";
                    Parameters[i] = new VariableIdentifier(type, name);
                }
            }
            else
                Parameters = Array.Empty<VariableIdentifier>();

            _level = -1;
            Body = GenBlock(ReturnType != null);
        }

        private StatementSyntax GenStatement()
        {
            while (true)
            {
                StatementKind kind =
                    (StatementKind)Options.StatementTypeDist.Sample(Random.Rng);

                if ((kind == StatementKind.Block || kind == StatementKind.If) &&
                    ShouldRejectRecursion())
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
                    case StatementKind.Return:
                        return GenReturn();
                    default:
                        throw new Exception("Unreachable");
                }
            }
        }

        private bool ShouldRejectRecursion()
        {
            double rand = Random.NextDouble();
            double n = Options.StatementRejectionLevelParameterN;
            double h = Options.StatementRejectionLevelParameterH;
            double levelPow = Math.Pow(_level, n);
            return rand < levelPow / (levelPow + Math.Pow(h, n));
        }

        private BlockSyntax GenBlock(bool root)
        {
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
                int numStatements = Options.BlockStatementCountDist.Sample(Random.Rng);
                StatementSyntax retStmt = null;
                for (int i = 0; i < numStatements; i++)
                {
                    StatementSyntax stmt = GenStatement();
                    if (stmt is ReturnStatementSyntax)
                    {
                        retStmt = stmt;
                        break;
                    }

                    yield return stmt;
                }

                if (root && retStmt == null)
                    retStmt = GenReturn();

                if (Options.EnableChecksumming)
                {
                    foreach (StatementSyntax stmt in GenChecksumming(scope.Variables, _genChecksumSiteId))
                        yield return stmt;
                }

                if (retStmt != null)
                    yield return retStmt;
            }
        }

        private StatementSyntax GenAssignmentStatement()
        {
            ExpressionSyntax lhs = null;
            FuzzType type = null;
            if (!Random.FlipCoin(Options.AssignToNewVarProb))
                (lhs, type) = GenMemberAccess(ft => true);

            if (lhs == null)
            {
                type = Types.PickType();
                if (Random.FlipCoin(Options.NewVarIsLocalProb))
                {
                    VariableIdentifier variable = new VariableIdentifier(type, $"var{_varCounter++}");

                    LocalDeclarationStatementSyntax decl =
                        LocalDeclarationStatement(
                            VariableDeclaration(
                                variable.Type.GenReferenceTo(),
                                SingletonSeparatedList(
                                    VariableDeclarator(variable.Name)
                                    .WithInitializer(
                                        EqualsValueClause(
                                            GenExpression(variable.Type))))));

                    _scope.Last().Variables.Add(variable);

                    return decl;
                }

                StaticField newStatic = Statics.GenerateNewField(type);
                lhs = IdentifierName(newStatic.Var.Name);
            }

            SyntaxKind assignmentKind = SyntaxKind.SimpleAssignmentExpression;
            if (type.AllowedAdditionalAssignmentKinds.Length > 0 && Random.FlipCoin(Options.FancyAssignmentProb))
                assignmentKind = Random.NextElement(type.AllowedAdditionalAssignmentKinds);

            if (assignmentKind == SyntaxKind.PreIncrementExpression ||
                assignmentKind == SyntaxKind.PreDecrementExpression)
            {
                return ExpressionStatement(PrefixUnaryExpression(assignmentKind, lhs));
            }

            if (assignmentKind == SyntaxKind.PostIncrementExpression ||
                assignmentKind == SyntaxKind.PostDecrementExpression)
            {
                return ExpressionStatement(PostfixUnaryExpression(assignmentKind, lhs));
            }

            if (assignmentKind == SyntaxKind.LeftShiftAssignmentExpression ||
                assignmentKind == SyntaxKind.RightShiftAssignmentExpression)
                type = Types.GetPrimitiveType(SyntaxKind.IntKeyword);

            ExpressionSyntax right = GenExpression(type);
            if (assignmentKind == SyntaxKind.ModuloAssignmentExpression ||
                assignmentKind == SyntaxKind.DivideAssignmentExpression)
            {
                right =
                    CastExpression(
                        type.GenReferenceTo(),
                        ParenthesizedExpression(
                            BinaryExpression(
                                SyntaxKind.BitwiseOrExpression,
                                ParenthesizeIfNecessary(right),
                                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1)))));
            }

            return ExpressionStatement(AssignmentExpression(assignmentKind, lhs, right));
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

        private StatementSyntax GenReturn()
        {
            if (ReturnType == null)
                return ReturnStatement();

            return ReturnStatement(GenExpression(ReturnType));
        }

        private ExpressionSyntax GenExpression(FuzzType type)
        {
            ExpressionSyntax gen;
            do
            {
                ExpressionKind kind = (ExpressionKind)Options.ExpressionTypeDist.Sample(Random.Rng);
                switch (kind)
                {
                    case ExpressionKind.MemberAccess:
                        gen = GenMemberAccess(ft => ft.Equals(type)).expr;
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

        /// <summary>
        /// Generates an access to a random member:
        /// * A static variable
        /// * A local variable
        /// * An argument
        /// * A field in one of these, if aggregate type
        /// * An element in one of these, if array type
        /// Only expressions of types that satisfy the filter passed can be returned.
        /// </summary>
        private (ExpressionSyntax expr, FuzzType type) GenMemberAccess(Func<FuzzType, bool> filter)
        {
            List<(ExpressionSyntax, FuzzType)> paths = new List<(ExpressionSyntax, FuzzType)>();

            foreach (ScopeFrame sf in _scope)
            {
                foreach (VariableIdentifier variable in sf.Variables)
                    CollectVariablePaths(paths, variable, filter);
            }

            foreach (StaticField stat in Statics.Fields)
                CollectVariablePaths(paths, stat.Var, filter);

            return paths.Count > 0 ? Random.NextElement(paths) : (null, null);

        }

        private static void CollectVariablePaths(List<(ExpressionSyntax, FuzzType)> paths, VariableIdentifier var, Func<FuzzType, bool> filter)
        {
            AddPathsRecursive(IdentifierName(var.Name), var.Type);

            void AddPathsRecursive(ExpressionSyntax curAccess, FuzzType curType)
            {
                if (filter(curType))
                    paths.Add((curAccess, curType));

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
                            arr.ElementType);
                        break;
                    case AggregateType agg:
                        foreach (AggregateField field in agg.Fields)
                        {
                            AddPathsRecursive(
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    curAccess,
                                    IdentifierName(field.Name)),
                                field.Type);
                        }
                        break;
                }
            }
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
                PrimitiveType rightType = Types.PickPrimitiveType(f => BinOpTable.GetImplicitlyConvertedToType(leftType.Keyword, f.Keyword).HasValue);
                right = GenExpression(rightType);
            }
            else
            {
                Debug.Assert(op == SyntaxKind.LessThanOrEqualExpression || op == SyntaxKind.LessThanExpression ||
                             op == SyntaxKind.GreaterThanOrEqualExpression || op == SyntaxKind.GreaterThanExpression);

                PrimitiveType leftType = Types.PickPrimitiveType(f => f.Keyword != SyntaxKind.BoolKeyword);
                PrimitiveType rightType = Types.PickPrimitiveType(f => BinOpTable.GetImplicitlyConvertedToType(leftType.Keyword, f.Keyword).HasValue);
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
            if (op == SyntaxKind.LeftShiftExpression || op == SyntaxKind.RightShiftExpression)
                return GenIntegralProducingBinary(type); // todo: handle. Needs separate table.

            PrimitiveType leftType = Types.PickPrimitiveType(f => f.Keyword != SyntaxKind.BoolKeyword);
            PrimitiveType rightType = Types.PickPrimitiveType(f => BinOpTable.GetImplicitlyConvertedToType(leftType.Keyword, f.Keyword).HasValue);
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
            if (BinOpTable.GetImplicitlyConvertedToType(leftType.Keyword, rightType.Keyword) != type.Keyword)
                expr = CastExpression(type.GenReferenceTo(), ParenthesizedExpression(expr));

            return expr;
        }

        private ExpressionSyntax GenCall(FuzzType type, bool allowNew)
        {
            FuncGenerator func;
            if (allowNew && Random.FlipCoin(Options.GenNewMethodProb))
            {
                type = type ?? Types.PickType();

                func = new FuncGenerator(_funcs, Random, Types, Statics, _genChecksumSiteId);
                func.Generate(type, true);
            }
            else
            {
                IEnumerable<FuncGenerator> funcs = _funcs.Skip(_funcIndex + 1);
                if (type != null)
                    funcs = funcs.Where(f => f.ReturnType.IsConvertibleTo(type));

                List<FuncGenerator> list = funcs.ToList();
                if (list.Count == 0)
                    return null;

                func = Random.NextElement(list);
                type = type ?? func.ReturnType;
            }

            InvocationExpressionSyntax invoc =
                InvocationExpression(
                    IdentifierName(func.Name),
                    ArgumentList(
                        SeparatedList(
                            func.ParameterTypes.Select(pt => Argument(GenExpression(pt))))));

            if (func.ReturnType.Equals(type))
                return invoc;
            return CastExpression(type.GenReferenceTo(), invoc);
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
                SyntaxKind.CharKeyword
            };

            if (!(type is PrimitiveType pt) || !acceptedTypes.Contains(pt.Keyword))
                return null;

            ExpressionSyntax subject = GenMemberAccess(ft => ft.Equals(type)).expr;
            if (subject == null)
                return null;

            ExpressionSyntax gen = PostfixUnaryExpression(
                isIncrement ? SyntaxKind.PostIncrementExpression : SyntaxKind.PostDecrementExpression,
                subject);

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
            List<(ExpressionSyntax, FuzzType)> paths = new List<(ExpressionSyntax, FuzzType)>();
            foreach (VariableIdentifier variable in variables)
                CollectVariablePaths(paths, variable, ft => ft is PrimitiveType);

            foreach (var (pathExpr, pathType) in paths)
            {
                string checksumSiteId = siteIdGenerator();
                LiteralExpressionSyntax id =
                    LiteralExpression(
                        SyntaxKind.StringLiteralExpression,
                        Literal(checksumSiteId));

                // Checksum chars as ints since they can be unprintable and we convert them to strings
                ExpressionSyntax expr = pathExpr;
                if (pathType is PrimitiveType pt && pt.Keyword == SyntaxKind.CharKeyword)
                    expr = CastExpression(PredefinedType(Token(SyntaxKind.IntKeyword)), expr);

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
    }

    internal enum StatementKind
    {
        Block,
        Assignment,
        Call,
        If,
        Return,
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
