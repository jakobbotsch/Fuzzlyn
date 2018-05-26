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

        internal static ProbabilityDistribution FromDescription(string desc)
        {
            // Match descriptions of the form
            // geometric(0.9)
            // binomial(3, 0.4)
            Match m = Regex.Match(desc, "^([a-z]+)\\((.*)\\)$");
            if (!m.Success)
                throw new ArgumentException("Description is not a valid probability distribution description", nameof(desc));

            string name = m.Groups[1].Value;
            string[] args = m.Groups[2].Value.Split(',').Select(s => s.Trim()).ToArray();
            switch (m.Groups[1].Value)
            {
                case "geometric":
                    return new GeometricDistribution(
                        double.Parse(args[0], CultureInfo.InvariantCulture),
                        args.Length > 1 ? int.Parse(args[1]) : 0);
                default:
                    throw new ArgumentException("Unknown probability distribution '" + m.Groups[1].Value + "'", nameof(desc));
            }
        }
    }
}
