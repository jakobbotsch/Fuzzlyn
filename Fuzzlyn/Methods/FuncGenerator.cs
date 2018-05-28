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
        private int _counter;

        public FuncGenerator(string name, Randomizer random, TypeManager types, StaticsManager statics)
        {
            Random = random;
            Types = types;
            Statics = statics;
            Name = name;
        }

        public Randomizer Random { get; }
        public TypeManager Types { get; }
        public StaticsManager Statics { get; }
        public string Name { get; }
        public BlockSyntax Body { get; private set; }

        public void Generate(bool randomizeSignature)
        {
            Body = GenBlock();
        }

        private StatementSyntax GenStatement()
        {
            while (true)
            {
                try
                {
                    StatementKind kind =
                        (StatementKind)Random.Options.StatementTypeDist.Sample(Random.Rng);

                    switch (kind)
                    {
                        case StatementKind.Block:
                            return GenBlock();
                        case StatementKind.Assignment:
                            return GenAssignmentStatement();
                        case StatementKind.Call:
                            return ExpressionStatement(GenCall());
                        case StatementKind.Increment:
                            return ExpressionStatement(GenIncDec(true));
                        case StatementKind.Decrement:
                            return ExpressionStatement(GenIncDec(false));
                        case StatementKind.NewObject:
                            return ExpressionStatement(GenNewObject());
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

        private BlockSyntax GenBlock()
        {
            int numStatements = Random.Options.BlockStatementCountDist.Sample(Random.Rng);

            _scope.Add(new ScopeFrame());
            BlockSyntax block = Block(
                Enumerable.Range(0, numStatements).Select(_ => GenStatement()));
            _scope.RemoveAt(_scope.Count - 1);

            return block;
        }

        private StatementSyntax GenAssignmentStatement()
        {
            ExpressionSyntax lhs = null;
            FuzzType type = null;
            if (!Random.FlipCoin(Random.Options.AssignToNewVarProb))
                (lhs, type) = GenMemberAccess(null);

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
                                            GenExpressionOfType(variable.Type))))));

                    _scope.Last().Variables.Add(variable);

                    return decl;
                }

                StaticField newStatic = Statics.GenerateNewField(type);
                lhs = IdentifierName(newStatic.Var.Name);
            }

            SyntaxKind assignmentKind = SyntaxKind.SimpleAssignmentExpression;
            if (type.AllowedAdditionalAssignmentKinds.Length > 0 && Random.FlipCoin(Random.Options.FancyAssignmentProb))
                assignmentKind = Random.NextElement(type.AllowedAdditionalAssignmentKinds);

            if (assignmentKind == SyntaxKind.LeftShiftAssignmentExpression ||
                assignmentKind == SyntaxKind.RightShiftAssignmentExpression)
                type = Types.GetPrimitiveType(SyntaxKind.IntKeyword);

            return
                ExpressionStatement(
                    AssignmentExpression(assignmentKind, lhs, GenExpressionOfType(type)));
        }

        /// <summary>
        /// Generates an access to a random member:
        /// * A static variable
        /// * A local variable
        /// * An argument
        /// * A field in one of these, if aggregate type
        /// * An element in one of these, if array type
        /// If type is nonnull, generates access to a member of that type.
        /// </summary>
        private (ExpressionSyntax expr, FuzzType type) GenMemberAccess(FuzzType type)
        {
            List<(ExpressionSyntax, FuzzType)> paths = new List<(ExpressionSyntax, FuzzType)>();

            foreach (ScopeFrame sf in _scope)
            {
                foreach (VariableIdentifier variable in sf.Variables)
                    AddPathsRecursive(IdentifierName(variable.Name), variable.Type);
            }

            foreach (StaticField stat in Statics.Fields)
                AddPathsRecursive(IdentifierName(stat.Var.Name), stat.Var.Type);

            if (type != null)
                paths.RemoveAll(t => !t.Item2.Equals(type));

            return paths.Count > 0 ? Random.NextElement(paths) : (null, null);

            void AddPathsRecursive(ExpressionSyntax curAccess, FuzzType curType)
            {
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

        private ExpressionSyntax GenExpressionOfType(FuzzType type)
        {
            ExpressionSyntax gen;
            do
            {
                ExpressionKind kind = (ExpressionKind)Random.Options.ExpressionTypeDist.Sample(Random.Rng);
                switch (kind)
                {
                    case ExpressionKind.MemberAccess:
                        gen = GenMemberAccess(type).expr;
                        break;
                    case ExpressionKind.Literal:
                        gen = GenLiteralOfType(type);
                        break;
                    default:
                        throw new Exception("Unreachable");
                }
            }
            while (gen == null);

            return gen;
        }

        private ExpressionSyntax GenLiteralOfType(FuzzType type)
        {
            switch (type)
            {
                case PrimitiveType prim:
                    return GenPrimitiveLiteral(prim);
                case AggregateType agg:
                    return
                        ObjectCreationExpression(type.GenReferenceTo())
                        .WithArgumentList(
                            ArgumentList(
                                SeparatedList(agg.Fields.Select(af => Argument(GenLiteralOfType(af.Type))))));
                case ArrayType arr:
                    List<int> dims = GenArrayDimensions(arr);
                    return GenArrayCreation(arr, dims);
                default:
                    throw new Exception("Unreachable");
            }
        }

        private LiteralExpressionSyntax GenPrimitiveLiteral(PrimitiveType primType)
        {
            if (!primType.Info.IsIntegral || !Random.FlipCoin(Random.Options.PickLiteralFromTableProb))
                return primType.Info.GenRandomLiteral(Random.Rng);

            object minValue = primType.Info.Type.GetField("MinValue").GetValue(null);
            object maxValue = primType.Info.Type.GetField("MaxValue").GetValue(null);
            dynamic val = 0;
            do
            {
                int num = Random.Options.LiteralDist.Sample(Random.Rng);

                if (num == int.MinValue)
                    val = minValue;
                if (num == int.MinValue + 1)
                    val = (dynamic)minValue + 1;

                if (num == int.MaxValue)
                    val = maxValue;
                if (num == int.MaxValue - 1)
                    val = (dynamic)maxValue - 1;
            } while (primType.Info.IsUnsigned && val < 0);

            return LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(val));
        }

        private List<int> GenArrayDimensions(ArrayType at)
        {
            int dimsRequired = at.Rank;
            FuzzType elemType = at.ElementType;
            while (elemType is ArrayType innerArr)
            {
                dimsRequired += innerArr.Rank;
                elemType = innerArr.ElementType;
            }

            List<int> dimensions = new List<int>(dimsRequired);
            while (true)
            {
                // If we are constructing an aggregate type start out by the number of fields, to limit
                // the number of constants required here.
                int totalSize = elemType is AggregateType elemAgg ? elemAgg.GetTotalNumPrimitiveFields() : 1;

                int maxArrayTotalSize = Math.Max(totalSize, Random.Options.MaxArrayTotalSize);

                for (int i = 0; i < dimensions.Capacity; i++)
                {
                    int dim = Random.Next(1, Random.Options.MaxArrayLengthPerDimension + 1);
                    if (totalSize * dim > maxArrayTotalSize)
                        break;

                    dimensions.Add(dim);
                    totalSize *= dim;
                }

                if (dimensions.Count == dimensions.Capacity)
                    return dimensions;

                dimensions.Clear();
            }
        }

        private ExpressionSyntax GenArrayCreation(ArrayType at, List<int> dimensions)
        {
            return ArrayCreationExpression(at.GenReferenceToArrayType())
                   .WithInitializer(GenArrayInitializer(at, dimensions, 0));
        }

        private InitializerExpressionSyntax GenArrayInitializer(ArrayType at, List<int> dimensions, int index)
        {
            return
                InitializerExpression(
                    SyntaxKind.ArrayInitializerExpression,
                    SeparatedList(
                        Enumerable.Range(0, dimensions[index]).Select(_ => GenInner())));

            ExpressionSyntax GenInner()
            {
                if (index != at.Rank - 1)
                    return GenArrayInitializer(at, dimensions, index + 1);

                if (at.ElementType is ArrayType innerArr)
                {
                    Debug.Assert(index < dimensions.Count - 1);
                    // If inner type is an array, then randomize its length up to the dimension
                    // (this means we will create inner arrays of different length)
                    List<int> restOfDimensions = dimensions.Skip(index + 1).ToList();
                    restOfDimensions[0] = Random.Next(1, restOfDimensions[0] + 1);
                    ExpressionSyntax creation = GenArrayCreation(innerArr, restOfDimensions);
                    return creation;
                }

                Debug.Assert(index == dimensions.Count - 1);
                return GenLiteralOfType(at.ElementType);
            }
        }

        private ExpressionSyntax GenCall()
        {
            throw new NotImplementedException();
        }

        private ExpressionSyntax GenIncDec(bool isIncrement)
        {
            throw new NotImplementedException();
        }

        private ExpressionSyntax GenNewObject()
        {
            throw new NotImplementedException();
        }

        private StatementSyntax GenIf()
        {
            throw new NotImplementedException();
        }

        private StatementSyntax GenReturn()
        {
            throw new NotImplementedException();
        }
    }

    internal enum StatementKind
    {
        Block,
        Assignment,
        Call,
        Increment,
        Decrement,
        NewObject,
        If,
        Return,
    }

    internal enum ExpressionKind
    {
        MemberAccess,
        Literal,
    }
}
