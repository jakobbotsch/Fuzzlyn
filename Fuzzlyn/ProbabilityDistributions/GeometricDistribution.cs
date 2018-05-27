using System;
using System.Collections.Generic;
using System.Text;

namespace Fuzzlyn.ProbabilityDistributions
{
    internal class GeometricDistribution : ProbabilityDistribution
    {
        public GeometricDistribution(double successProbability, int baseValue = 0)
        {
            SuccessProbability = successProbability;
            BaseValue = baseValue;
        }

        public double SuccessProbability { get; }
        public int BaseValue { get; }

        internal override int Sample(Rng rng)
        {
            // https://stackoverflow.com/questions/23517138/random-number-generator-using-geometric-distribution
            return BaseValue + (int)Math.Floor(Math.Log(1 - rng.NextDouble(), 1 - SuccessProbability));
        }
    }
}
