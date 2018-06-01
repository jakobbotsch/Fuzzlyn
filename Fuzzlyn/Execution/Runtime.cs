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

        public List<string> Values { get; } = new List<string>();

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
            if (typeof(T) == typeof(char))
                Values.Add(id + ": " + (int)(char)(object)val);
            else
                Values.Add(id + ": " + val);

            if (Unsafe.SizeOf<T>() > _buffer.Length)
                _buffer = new byte[Unsafe.SizeOf<T>()];

            Unsafe.CopyBlockUnaligned(ref _buffer[0], ref Unsafe.As<T, byte>(ref val), (uint)Unsafe.SizeOf<T>());
            _hash.TransformBlock(_buffer, 0, Unsafe.SizeOf<T>(), null, 0);
        }
    }
}
