using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Fuzzlyn.Execution
{
    internal class Runtime : IRuntime, IDisposable
    {
        private readonly HashAlgorithm _hash = SHA256.Create();

        public List<ChecksumSite> ChecksumSites { get; } = new List<ChecksumSite>();

        public string FinishHashCode()
        {
            _hash.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
            return string.Concat(_hash.Hash.Select(b => b.ToString("x2")));
        }

        public void Dispose()
        {
            _hash.Dispose();
        }

        private byte[] _buffer = new byte[0];
        public void Checksum<T>(string id, T val)
        {
            // Some characters mess up the JSON, so write them as ints.
            if (typeof(T) == typeof(char))
                ChecksumSites.Add(new ChecksumSite(id, ((int)(char)(object)val).ToString()));
            else
                ChecksumSites.Add(new ChecksumSite(id, val.ToString()));

            if (Unsafe.SizeOf<T>() > _buffer.Length)
                _buffer = new byte[Unsafe.SizeOf<T>()];

            Unsafe.CopyBlockUnaligned(ref _buffer[0], ref Unsafe.As<T, byte>(ref val), (uint)Unsafe.SizeOf<T>());
            _hash.TransformBlock(_buffer, 0, Unsafe.SizeOf<T>(), null, 0);
        }
    }
}
