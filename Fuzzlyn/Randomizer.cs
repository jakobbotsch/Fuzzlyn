using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Fuzzlyn
{
    internal class Randomizer
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

        public int Next(int max) => Rng.Next(max);
        public int Next(int min, int max) => Rng.Next(min, max);
        public ulong NextUInt64() => Rng.NextUInt64();
        public double NextDouble() => Rng.NextDouble();

        private ulong GenSeed()
        {
            byte[] bytes = new byte[8];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(bytes);

            return BitConverter.ToUInt64(bytes, 0);
        }
    }
}
