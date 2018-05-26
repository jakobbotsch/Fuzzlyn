using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Fuzzlyn.ProbabilityDistributions
{
    internal class StringTableDistribution : ProbabilityDistribution
    {
        private readonly double[] _cumulativeProbabilities;
        private readonly string[] _strings;

        public StringTableDistribution(Dictionary<string, double> probabilities)
        {
            if (Math.Abs(probabilities.Sum(kvp => kvp.Value) - 1) > 0.00000001)
                throw new ArgumentException("Probabilities must sum to 1", nameof(probabilities));

            Probabilities = probabilities;

            _cumulativeProbabilities = new double[probabilities.Count];
            _strings = new string[probabilities.Count];
            double prob = 0;
            int index = 0;
            foreach (KeyValuePair<string, double> kvp in probabilities)
            {
                prob += kvp.Value;
                _cumulativeProbabilities[index] = prob;
                _strings[index] = kvp.Key;
                index++;
            }
        }

        public IReadOnlyDictionary<string, double> Probabilities { get; }

        internal override int Sample(Rng rng)
        {
            throw new NotSupportedException();
        }
    }
}
