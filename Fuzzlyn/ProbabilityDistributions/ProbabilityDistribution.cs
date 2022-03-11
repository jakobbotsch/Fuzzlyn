namespace Fuzzlyn.ProbabilityDistributions;

internal abstract class ProbabilityDistribution
{
    internal abstract int Sample(Rng rng);
}
