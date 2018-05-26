namespace Fuzzlyn.ProbabilityDistributions
{
    internal class UniformRangeDistribution : ProbabilityDistribution
    {
        public UniformRangeDistribution(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public int Min { get; }
        public int Max { get; }

        internal override int Sample(Rng rng)
            => rng.Next(Min, Max + 1);
    }
}
