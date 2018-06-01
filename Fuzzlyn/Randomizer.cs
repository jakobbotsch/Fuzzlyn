using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
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

        public T NextElement<T>(IReadOnlyList<T> list)
            => list[Next(list.Count)];

        private ulong GenSeed()
        {
            Span<byte> bytes = stackalloc byte[8];
            RandomNumberGenerator.Fill(bytes);
            return Unsafe.ReadUnaligned<ulong>(ref bytes[0]);
        }
    }
}
