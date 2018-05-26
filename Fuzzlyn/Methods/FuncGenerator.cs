using Fuzzlyn.Statics;
using Fuzzlyn.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            VariableIdentifier variable;

            bool newVar = Random.FlipCoin(Random.Options.AssignToNewVarProb);
            if (newVar)
            {
                FuzzType type = Types.PickType();
                string name = $"var{_counter++}";
                variable = new VariableIdentifier(type, name);
                _scope.Last().Variables.Add(variable);
            }
            else if (Random.FlipCoin(Random.Options.AssignToStaticVarProb))
            {
                variable = Statics.PickStatic().Var;
            }
            else
            {
                List<VariableIdentifier> allVars = _scope.SelectMany(s => s.Variables).ToList();
                if (allVars.Count == 0)
                    return GenAssignmentStatement();

                variable = Random.NextElement(allVars);
            }

            if (newVar)
            {
                return
                    LocalDeclarationStatement(
                        VariableDeclaration(
                            variable.Type.GenReferenceTo(),
                            SingletonSeparatedList(
                                VariableDeclarator(variable.Name)
                                .WithInitializer(
                                    EqualsValueClause(
                                        GenExpressionOfType(variable.Type))))));
            }

            SyntaxKind assignmentKind = SyntaxKind.SimpleAssignmentExpression;
            if (variable.Type.AllowedAdditionalAssignmentKinds.Length > 0 && Random.FlipCoin(Random.Options.FancyAssignmentProb))
                assignmentKind = Random.NextElement(variable.Type.AllowedAdditionalAssignmentKinds);

            return
                ExpressionStatement(
                    AssignmentExpression(assignmentKind, IdentifierName(variable.Name), GenExpressionOfType(variable.Type)));
        }

        private ExpressionSyntax GenExpressionOfType(FuzzType type)
        {
            if (Random.FlipCoin(Random.Options.PickLocalOfTypeProb))
            {
                List<VariableIdentifier> vars = _scope.SelectMany(s => s.Variables).Where(v => v.Type.Equals(type)).ToList();
                if (vars.Count == 0)
                    return GenExpressionOfType(type);
            }

            if (Random.FlipCoin(Random.Options.PickStaticOfTypeProb))
                return IdentifierName(Statics.PickStatic(type).Var.Name);

            return GenConstantOfType(type);
        }

        private ExpressionSyntax GenConstantOfType(FuzzType type)
        {
            switch (type)
            {
                case PrimitiveType prim:
                    return GenLiteralInt(prim);
                case AggregateType agg:
                    return
                        ObjectCreationExpression(
                            type.GenReferenceTo())
                        .WithArgumentList(
                            ArgumentList(
                                SeparatedList(agg.Fields.Select(af => Argument(GenConstantOfType(af.Type))))));
                case ArrayType arr:
                    return DefaultExpression(type.GenReferenceTo());
                default:
                    throw new Exception("Unreachable");
            }
        }

        private LiteralExpressionSyntax GenLiteralInt(PrimitiveType primType)
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
}
