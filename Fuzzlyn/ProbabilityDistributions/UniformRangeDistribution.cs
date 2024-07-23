namespace Fuzzlyn.ProbabilityDistributions;

internal class UniformRangeDistribution(int min, int max) : ProbabilityDistribution
{
    public int Min { get; } = min;
    public int Max { get; } = max;

    internal override int Sample(Rng rng)
        => rng.Next(Min, Max + 1);
}
