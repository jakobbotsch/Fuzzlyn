using Fuzzlyn.Methods;
using Fuzzlyn.ProbabilityDistributions;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Fuzzlyn
{
    internal class FuzzlynOptions
    {
        public ulong? Seed { get; set; }
        public int NumPrograms { get; set; } = 1;
        public int Parallelism { get; set; } = 1;
        public bool Output { get; set; } = false;
        // Probability that we pick a class when generating a new type. Otherwise we make a struct.
        public double MakeClassProb { get; set; } = 0.5;
        // Probability that a field of an aggregate type gets a primitive type vs a aggregate type.
        public double PrimitiveFieldProb { get; set; } = 0.8;
        public double AssignToNewVarProb { get; set; } = 0.3;
        public double NewVarIsLocalProb { get; set; } = 0.8;
        public double FancyAssignmentProb { get; set; } = 0.1;

        public ProbabilityDistribution MakeAggregateTypeCountDist { get; set; } = new GeometricDistribution(0.4);
        public ProbabilityDistribution MaxStructFieldsDist { get; set; } = new UniformRangeDistribution(1, 10);
        public ProbabilityDistribution MaxClassFieldsDist { get; set; } = new UniformRangeDistribution(1, 10);
        public ProbabilityDistribution MakeArrayCountDist { get; set; } = new GeometricDistribution(0.8);
        public ProbabilityDistribution ArrayRankDist { get; set; } = new GeometricDistribution(0.9, 1);
        public ProbabilityDistribution BlockStatementCountDist { get; set; } = new GeometricDistribution(0.35, 1);
        public ProbabilityDistribution StatementTypeDist { get; set; }
            = new TableDistribution(new Dictionary<int, double>
            {
                [(int)StatementKind.Block] = 0.1,
                [(int)StatementKind.Assignment] = 0.25,
                [(int)StatementKind.Call] = 0.1,
                [(int)StatementKind.Increment] = 0.07,
                [(int)StatementKind.Decrement] = 0.07,
                [(int)StatementKind.NewObject] = 0.2,
                [(int)StatementKind.If] = 0.2,
                [(int)StatementKind.Return] = 0.01,
            });
        public ProbabilityDistribution ExpressionTypeDist { get; set; }
            = new TableDistribution(new Dictionary<int, double>
            {
                [(int)ExpressionKind.MemberAccess] = 0.7,
                [(int)ExpressionKind.Call] = 0.1,
                [(int)ExpressionKind.Literal] = 0.2,
            });

        public double PickLiteralFromTableProb { get; set; } = 0.5;
        public ProbabilityDistribution LiteralDist { get; set; }
            = new TableDistribution(new Dictionary<int, double>
            {
                [int.MinValue] = 0.1,
                [int.MinValue + 1] = 0.1,
                [-10] = 0.04,
                [-2] = 0.04,
                [-1] = 0.04,
                [0] = 0.2,
                [1] = 0.2,
                [2] = 0.04,
                [10] = 0.04,
                [int.MaxValue - 1] = 0.1,
                [int.MaxValue] = 0.1,
            });

        public ProbabilityDistribution BinaryMathDist { get; set; }
    = new TableDistribution(new Dictionary<int, double>
    {
        [(int)SyntaxKind.AddExpression] = 0.2,
        [(int)SyntaxKind.SubtractExpression] = 0.2,
        [(int)SyntaxKind.DivideExpression] = 0.2,
        [(int)SyntaxKind.MultiplyExpression] = 0.2,
        [(int)SyntaxKind.ModuloExpression] = 0.2,
    });

        public ProbabilityDistribution BinaryBoolDist { get; set; }
            = new TableDistribution(new Dictionary<int, double>
            {
                [(int)SyntaxKind.LogicalAndExpression] = 0.25,
                [(int)SyntaxKind.LogicalOrExpression] = 0.25,
                [(int)SyntaxKind.ExclusiveOrExpression] = 0.25,
                [(int)SyntaxKind.IsExpression] = 0.25,
            });


        public int MaxArrayTotalSize { get; set; } = 300;
        public int MaxArrayLengthPerDimension { get; set; } = 10;
        // Probability that we generate a new method when trying to generate a call.
        public double GenNewMethodProb { get; set; } = 0.07;
        public ProbabilityDistribution MethodParameterCountDist { get; set; } = new GeometricDistribution(0.4);
    }
}
