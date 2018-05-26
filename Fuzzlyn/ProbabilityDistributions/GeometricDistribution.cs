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

        public double SuccessProbability { get; set; }
        public int BaseValue { get; set; }

        internal override int Sample(Rng rng)
            => BaseValue + (int)Math.Floor(Math.Log(1 - rng.NextDouble(), 1 - SuccessProbability));
    }
}
