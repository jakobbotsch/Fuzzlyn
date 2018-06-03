using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Fuzzlyn
{
    public class Rng
    {
        private ulong _s0, _s1, _s2, _s3;

        public Rng(ulong s0, ulong s1, ulong s2, ulong s3)
        {
            _s0 = s0;
            _s1 = s1;
            _s2 = s2;
            _s3 = s3;
        }

        public int Next(int max) => Next(0, max);
        public int Next(int min, int max)
            => min + (int)((long)(NextUInt64() & ~0x8000000000000000) % (max - min));

        public ulong NextUInt64()
        {
            ulong result = Rotl(_s1 * 5, 7) * 9;

            ulong t = _s1 << 17;

            _s2 ^= _s0;
            _s3 ^= _s1;
            _s1 ^= _s2;
            _s0 ^= _s3;

            _s2 ^= t;

            _s3 = Rotl(_s3, 45);

            return result;
        }

        public double NextDouble()
        {
            // From http://xoshiro.di.unimi.it/
            ulong val = (0x3FFul << 52) | (NextUInt64() >> 12);
            return Unsafe.As<ulong, double>(ref val) - 1.0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ulong Rotl(ulong x, int k)
            => (x << k) | (x >> (64 - k));

        internal static ulong GenSeed()
        {
            Span<byte> bytes = stackalloc byte[8];
            RandomNumberGenerator.Fill(bytes);
            return Unsafe.ReadUnaligned<ulong>(ref bytes[0]);
        }

        public static Rng FromSplitMix64Seed(ulong seed)
        {
            // "The state must be seeded so that it is not everywhere zero. If you have
            // a 64-bit seed, we suggest to seed a splitmix64 generator and use its
            // output to fill s."

            ulong x = seed;
            ulong NextSplitMix64()
            {
                ulong z = (x += 0x9E3779B97F4A7C15ul);
                z = (z ^ (z >> 30)) * 0xBF58476D1CE4E5B9ul;
                z = (z ^ (z >> 27)) * 0x94D049BB133111EBul;
                return z ^ (z >> 31);
            }

            return new Rng(NextSplitMix64(), NextSplitMix64(), NextSplitMix64(), NextSplitMix64());
        }
    }
}
