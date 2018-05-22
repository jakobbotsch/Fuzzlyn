using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Fuzzlyn
{
    public class Randomizer
    {
        public Randomizer(FuzzlynOptions options)
        {
            Options = options;
            Seed = options.Seed ?? GenSeed();
            Rng = Rng.FromSplitMix64Seed(Seed);
        }

        public FuzzlynOptions Options { get; }
        public ulong Seed { get; }
        public Rng Rng { get; }

        public bool FlipCoin(double probability)
            => Rng.NextDouble() < probability;

        private ulong GenSeed()
        {
            byte[] bytes = new byte[8];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(bytes);

            return BitConverter.ToUInt64(bytes, 0);
        }
    }
}
