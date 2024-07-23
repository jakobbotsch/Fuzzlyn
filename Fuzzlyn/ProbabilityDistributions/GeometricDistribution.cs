using System;

namespace Fuzzlyn.ProbabilityDistributions;

internal class GeometricDistribution(double successProbability, int baseValue = 0) : ProbabilityDistribution
{
    public double SuccessProbability { get; } = successProbability;
    public int BaseValue { get; } = baseValue;

    internal override int Sample(Rng rng)
    {
        // https://stackoverflow.com/questions/23517138/random-number-generator-using-geometric-distribution
        return BaseValue + (int)Math.Floor(Math.Log(1 - rng.NextDouble(), 1 - SuccessProbability));
    }
}
