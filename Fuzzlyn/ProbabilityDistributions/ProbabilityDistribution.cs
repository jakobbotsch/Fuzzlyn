using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fuzzlyn.ProbabilityDistributions
{
    [JsonConverter(typeof(ProbabilityDistributionSerializer))]
    internal abstract class ProbabilityDistribution
    {
        public string Type => GetType().Name;
        internal abstract int Sample(Rng rng);
    }
}
