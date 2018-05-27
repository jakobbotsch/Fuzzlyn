using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Fuzzlyn.ProbabilityDistributions
{
    internal class TableDistribution : ProbabilityDistribution
    {
        private readonly int[] _keys;
        private readonly double[] _cumulativeProbs;

        public TableDistribution(Dictionary<int, double> pairs)
        {
            Pairs = pairs;

            _keys = new int[pairs.Count];
            _cumulativeProbs = new double[pairs.Count];

            double prob = 0;
            int index = 0;
            foreach (KeyValuePair<int, double> kvp in pairs)
            {
                prob += kvp.Value;

                _keys[index] = kvp.Key;
                _cumulativeProbs[index] = prob;

                index++;
            }

            Trace.Assert(Math.Abs(prob - 1) < 0.0000000001);
        }

        public IReadOnlyDictionary<int, double> Pairs { get; }

        internal override int Sample(Rng rng)
        {
            double val = rng.NextDouble();
            for (int i = 0; i < _keys.Length; i++)
            {
                if (val < _cumulativeProbs[i])
                    return _keys[i];
            }

            throw new Exception("Unreachable");
        }
    }
}
