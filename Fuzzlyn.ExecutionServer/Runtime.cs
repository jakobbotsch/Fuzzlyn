using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Fuzzlyn.ExecutionServer;

internal unsafe class Runtime : IRuntime
{
    public List<ChecksumSite> ChecksumSites { get; set; }
    public long NumChecksumCalls { get; set; }

    public string FinishHashCode()
    {
        return _state.ToString("X16");
    }

    private ulong _state = 14695981039346656037;
    public void Checksum<T>(string id, T val) where T : unmanaged
    {
        NumChecksumCalls++;

        if (ChecksumSites != null)
            ChecksumSites.Add(new ChecksumSite(id, val.ToString()));

        ChecksumBytes(ref Unsafe.As<T, byte>(ref val), sizeof(T));
    }

    private void ChecksumBytes(ref byte pb, int numBytes)
    {
        for (int i = 0; i < numBytes; i++)
        {
            _state ^= Unsafe.AddByteOffset(ref pb, i);
            _state *= 1099511628211;
        }
    }

    public void ChecksumSingles<T>(string id, T val) where T : unmanaged
    {
        NumChecksumCalls++;

        int numFloats = sizeof(T) / sizeof(float);
        ReadOnlySpan<float> floats = MemoryMarshal.CreateReadOnlySpan(ref Unsafe.As<T, float>(ref val), numFloats);

        if (ChecksumSites != null)
        {
            string s = string.Join(", ", floats.ToArray().Select(BitConverter.SingleToUInt32Bits));
            if (numFloats > 1)
                s = $"<{s}>";
            ChecksumSites.Add(new ChecksumSite(id, s));
        }

        for (int i = 0; i < floats.Length; i++)
        {
            float flt = floats[i];
            if (float.IsNaN(flt))
                flt = float.NaN;

            ChecksumBytes(ref Unsafe.As<float, byte>(ref flt), sizeof(float));
        }
    }

    public void ChecksumDoubles<T>(string id, T val) where T : unmanaged
    {
        NumChecksumCalls++;

        int numDoubles = sizeof(T) / sizeof(double);
        ReadOnlySpan<double> doubles = MemoryMarshal.CreateReadOnlySpan(ref Unsafe.As<T, double>(ref val), numDoubles);

        if (ChecksumSites != null)
        {
            string s = string.Join(", ", doubles.ToArray().Select(BitConverter.DoubleToUInt64Bits));
            if (numDoubles > 1)
                s = $"<{s}>";
            ChecksumSites.Add(new ChecksumSite(id, s));
        }

        for (int i = 0; i < doubles.Length; i++)
        {
            double dbl = doubles[i];
            if (double.IsNaN(dbl))
                dbl = double.NaN;

            ChecksumBytes(ref Unsafe.As<double, byte>(ref dbl), sizeof(double));
        }
    }

    public void ChecksumSingle(string id, float val)
    {
        ChecksumSingles(id, val);
    }

    public void ChecksumDouble(string id, double val)
    {
        ChecksumDoubles(id, val);
    }
}
