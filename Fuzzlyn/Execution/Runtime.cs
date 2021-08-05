using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Fuzzlyn.Execution
{
    internal class Runtime : IRuntime
    {
        public List<ChecksumSite> ChecksumSites { get; set; }

        public string FinishHashCode()
        {
            return _state.ToString("X16");
        }

        private ulong _state = 14695981039346656037;
        public void Checksum<T>(string id, T val)
        {
            if (ChecksumSites != null)
                ChecksumSites.Add(new ChecksumSite(id, val.ToString()));

            ref byte valBuf = ref Unsafe.As<T, byte>(ref val);
            for (int i = 0; i < Unsafe.SizeOf<T>(); i++)
            {
                _state ^= Unsafe.AddByteOffset(ref valBuf, (IntPtr)i);
                _state *= 1099511628211;
            }
        }
    }
}
