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
        private int _counter;
        private int _level;

        public FuncGenerator(List<FuncGenerator> funcs, Randomizer random, TypeManager types, StaticsManager statics)
        {
            _funcs = funcs;
            Random = random;
            Types = types;
            Statics = statics;

            _funcIndex = funcs.Count;
            Name = $"M{funcs.Count}";

            funcs.Add(this);
        }

        public Randomizer Random { get; }
        public TypeManager Types { get; }
        public StaticsManager Statics { get; }
        public BlockSyntax Body { get; private set; }
        public FuzzType ReturnType { get; private set; }
        public FuzzType[] ParameterTypes { get; private set; }
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
                        ParameterTypes.Select(
                            (pt, i) => Parameter(Identifier($"arg{i}")).WithType(pt.GenReferenceTo()))));

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
                int numArgs = Random.Options.MethodParameterCountDist.Sample(Random.Rng);
                ParameterTypes = Enumerable.Range(0, numArgs).Select(i => Types.PickType()).ToArray();
            }
            else
                ParameterTypes = Array.Empty<FuzzType>();

            _level = -1;
            _scope.Add(new ScopeFrame());
            _scope.Last().Variables.AddRange(ParameterTypes.Select((p, i) => new VariableIdentifier(p, $"arg{i}")));
            Body = GenBlock(ReturnType != null);
            _scope.RemoveAt(0);
        }

        private StatementSyntax GenStatement()
        {
            while (true)
            {
                try
                {
                    StatementKind kind =
                        (StatementKind)Random.Options.StatementTypeDist.Sample(Random.Rng);

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
                catch (NotImplementedException)
                {

                }
            }
        }

        private bool ShouldRejectRecursion()
        {
            double rand = Random.NextDouble();
            double n = Random.Options.StatementRejectionLevelParameterN;
            double h = Random.Options.StatementRejectionLevelParameterH;
            double levelPow = Math.Pow(_level, n);
            return rand < levelPow / (levelPow + Math.Pow(h, n));
        }

        private BlockSyntax GenBlock(bool forceReturn)
        {
            int numStatements = Random.Options.BlockStatementCountDist.Sample(Random.Rng);

            _level++;
            _scope.Add(new ScopeFrame());
            BlockSyntax block = Block(GenStatements());
            _scope.RemoveAt(_scope.Count - 1);
            _level--;

            return block;

            IEnumerable<StatementSyntax> GenStatements()
            {
                for (int i = 0; i < numStatements; i++)
                    yield return GenStatement();

                if (forceReturn)
                    yield return GenReturn();
            }
        }

        private StatementSyntax GenAssignmentStatement()
        {
            ExpressionSyntax lhs = null;
            FuzzType type = null;
            if (!Random.FlipCoin(Random.Options.AssignToNewVarProb))
                (lhs, type) = GenMemberAccess(ft => true);

            if (lhs == null)
            {
                type = Types.PickType();
                if (Random.FlipCoin(Random.Options.NewVarIsLocalProb))
                {
                    VariableIdentifier variable = new VariableIdentifier(type, $"var{_counter++}");

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
            if (type.AllowedAdditionalAssignmentKinds.Length > 0 && Random.FlipCoin(Random.Options.FancyAssignmentProb))
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

            return
                ExpressionStatement(
                    AssignmentExpression(assignmentKind, lhs, GenExpression(type)));
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
                    AddPathsRecursive(IdentifierName(variable.Name), variable.Type);
            }

            foreach (StaticField stat in Statics.Fields)
                AddPathsRecursive(IdentifierName(stat.Var.Name), stat.Var.Type);

            return paths.Count > 0 ? Random.NextElement(paths) : (null, null);

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

        private ExpressionSyntax GenExpression(FuzzType type)
        {
            ExpressionSyntax gen;
            do
            {
                ExpressionKind kind = (ExpressionKind)Random.Options.ExpressionTypeDist.Sample(Random.Rng);
                switch (kind)
                {
                    case ExpressionKind.MemberAccess:
                        gen = GenMemberAccess(ft => ft.Equals(type)).expr;
                        break;
                    case ExpressionKind.Literal:
                        gen = GenLiteral(type);
                        break;
                    case ExpressionKind.Call:
                        gen = GenCall(type, true);
                        break;
                    case ExpressionKind.Increment:
                        gen = GenIncDec(true).expr;
                        break;
                    case ExpressionKind.Decrement:
                        gen = GenIncDec(false).expr;
                        break;
                    case ExpressionKind.Binary:
                        gen = GenBinary(type);
                        break;
                    case ExpressionKind.Cast:
                        gen = GenCast(type);
                        break;
                    default:
                        throw new Exception("Unreachable");
                }
            }
            while (gen == null);

            return gen;
        }

        private ExpressionSyntax GenCast(FuzzType type)
        {
            return CastExpression(type.GenReferenceTo(), GenMemberAccess((x => true)).expr);
        }

        private ExpressionSyntax GenBinary(FuzzType type)
        {
            var leftHandSide = GenMemberAccess(x => x.Equals(type)).expr;
            var rightHandSide = GenMemberAccess(x => x.Equals(type)).expr;
            SyntaxKind op;

            if (type.Equals(Types.GetPrimitiveType(SyntaxKind.BoolKeyword)))
            {
                op = (SyntaxKind)Random.Options.BinaryBoolDist.Sample(Random.Rng);
            }
            else
            {
                op = (SyntaxKind)Random.Options.BinaryMathDist.Sample(Random.Rng);
            }
            return BinaryExpression(op, leftHandSide, rightHandSide);
        }

        private ExpressionSyntax GenLiteral(FuzzType type) => LiteralGenerator.GenLiteral(Random, type);

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

        private ExpressionSyntax GenCall(FuzzType type, bool allowNew)
        {
            FuncGenerator func;
            if (allowNew && Random.FlipCoin(Random.Options.GenNewMethodProb))
            {
                type = type ?? Types.PickType();

                func = new FuncGenerator(_funcs, Random, Types, Statics);
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

        private (ExpressionSyntax expr, FuzzType type) GenIncDec(bool isIncrement)
        {
            ExpressionSyntax gen = null;

            var acceptedTypes =
            new List<SyntaxKind>{
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

            var (subject, type) = GenMemberAccess((x) => (acceptedTypes.Select(y => Types.GetPrimitiveType(y)).Contains(x)));

            gen = PostfixUnaryExpression(
                isIncrement ? SyntaxKind.PostIncrementExpression : SyntaxKind.PostDecrementExpression,
                subject);

            return (gen, type);
        }

        private ExpressionSyntax GenNewObject()
        {
            AggregateType type = Types.PickAggregateType();
            if (type == null)
                return null;

            ObjectCreationExpressionSyntax creation =
                ObjectCreationExpression(type.GenReferenceTo())
                .WithArgumentList(
                    ArgumentList(
                        SeparatedList(
                            type.Fields.Select(f => Argument(GenExpression(f.Type))))));

            return creation;
        }

        private StatementSyntax GenIf()
        {
            StatementSyntax gen = null;
            var subject = GenExpression(new PrimitiveType(SyntaxKind.BoolKeyword));
            if (Random.FlipCoin(0.5))
            {
                gen = IfStatement(subject, GenBlock(false));
            }
            else
            {
                gen = IfStatement(subject, GenBlock(false), ElseClause(GenBlock(false)));
            }
            return gen;
        }

        private StatementSyntax GenReturn()
        {
            if (ReturnType == null)
                return ReturnStatement();

            return ReturnStatement(GenExpression(ReturnType));
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
