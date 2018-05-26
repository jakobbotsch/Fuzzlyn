using Fuzzlyn.ProbabilityDistributions;
using Newtonsoft.Json;

namespace Fuzzlyn
{
    internal class FuzzlynOptions
    {
        public ulong? Seed { get; set; }
        public int NumPrograms { get; set; } = 1;
        // Probability that we pick a class when generating a new type. Otherwise we make a struct.
        public double MakeClassProb { get; set; } = 0.5;
        // Probability that a field of an aggregate type gets a primitive type vs a aggregate type.
        public double PrimitiveFieldProb { get; set; } = 0.8;

        public ProbabilityDistribution MakeAggregateTypeCountDist { get; set; } = new GeometricDistribution(0.5);
        public ProbabilityDistribution MaxStructFieldsDist { get; set; } = new UniformRangeDistribution(1, 10);
        public ProbabilityDistribution MaxClassFieldsDist { get; set; } = new UniformRangeDistribution(1, 10);
        public ProbabilityDistribution StaticFieldMakeArrayCountDist { get; set; } = new GeometricDistribution(0.8);
        public ProbabilityDistribution ArrayRankDist { get; set; } = new GeometricDistribution(0.9, 1);
    }
}
