using Fuzzlyn.Methods;
using Fuzzlyn.ProbabilityDistributions;
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

        public ProbabilityDistribution MakeAggregateTypeCountDist { get; set; } = new GeometricDistribution(0.2);
        public ProbabilityDistribution MaxStructFieldsDist { get; set; } = new UniformRangeDistribution(1, 10);
        public ProbabilityDistribution MaxClassFieldsDist { get; set; } = new UniformRangeDistribution(1, 10);
        public ProbabilityDistribution MakeArrayCountDist { get; set; } = new GeometricDistribution(0.8);
        public ProbabilityDistribution ArrayRankDist { get; set; } = new GeometricDistribution(0.9, 1);
        public ProbabilityDistribution BlockStatementCountDist { get; set; } = new GeometricDistribution(0.3, 1);
        public ProbabilityDistribution StatementTypeDist { get; set; }
            = new TableDistribution(new Dictionary<int, double>
            {
                [(int)StatementKind.Block] = 0.1,
                [(int)StatementKind.Assignment] = 0.25,
                [(int)StatementKind.Call] = 0.05,
                [(int)StatementKind.Increment] = 0.05,
                [(int)StatementKind.Decrement] = 0.05,
                [(int)StatementKind.NewObject] = 0.2,
                [(int)StatementKind.If] = 0.2,
                [(int)StatementKind.Return] = 0.1,
            });
        public ProbabilityDistribution ExpressionTypeDist { get; set; }
            = new TableDistribution(new Dictionary<int, double>
            {
                [(int)ExpressionKind.MemberAccess] = 0.7,
                [(int)ExpressionKind.Literal] = 0.3,
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

        public int MaxArrayTotalSize { get; set; } = 300;
        public int MaxArrayLengthPerDimension { get; set; } = 10;
    }
}
