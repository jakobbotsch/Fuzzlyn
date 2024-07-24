using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fuzzlyn;

public enum Extension
{
    Vector64,
    Vector128,
    Vector256,
    Vector512,

    Aes,
    AesX64,
    Avx,
    AvxX64,
    Avx2,
    Avx2X64,
    Avx512BW,
    Avx512BWVL,
    Avx512BWX64,
    Avx512CD,
    Avx512CDVL,
    Avx512CDX64,
    Avx512DQ,
    Avx512DQVL,
    Avx512DQX64,
    Avx512F,
    Avx512FVL,
    Avx512FX64,
    Avx512Vbmi,
    Avx512VbmiVL,
    Avx512VbmiX64,
    AvxVnni,
    AvxVnniX64,
    Bmi1,
    Bmi1X64,
    Bmi2,
    Bmi2X64,
    Fma,
    FmaX64,
    Lzcnt,
    LzcntX64,
    Pclmulqdq,
    PclmulqdqX64,
    Popcnt,
    PopcntX64,
    Sse,
    SseX64,
    Sse2,
    Sse2X64,
    Sse3,
    Sse3X64,
    Sse41,
    Sse41X64,
    Sse42,
    Sse42X64,
    Ssse3,
    Ssse3X64,
    X86Base,
    X86BaseX64,
    X86Serialize,
    X86SerializeX64,

    AllSupported, // Pseudo extension that expands to all supported extensions
}

internal class SeedSpecification(ulong seed, HashSet<Extension> extensions)
{
    public ulong Seed { get; set; } = seed;
    public HashSet<Extension> Extensions { get; set; } = extensions;

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append(Seed);
        sb.Append('-');
        bool first = true;
        foreach (Extension ext in Extensions)
        {
            if (!first)
                sb.Append(',');

            sb.Append(ext.ToString().ToLowerInvariant());
            first = false;
        }

        return sb.ToString();
    }

    public static bool TryParseExtensions(string value, HashSet<Extension> extensions, out string error)
    {
        foreach (string ext in value.Split(','))
        {
            if (!Enum.TryParse(ext, ignoreCase: true, out Extension result))
            {
                error = $"Could not parse '{ext}' to an extension";
                return false;
            }

            extensions.Add(result);
        }

        error = null;
        return true;
    }

    public static bool TryParse(string value, out SeedSpecification seedSpec, out string error)
    {
        seedSpec = null;

        int dash = value.IndexOf('-');
        HashSet<Extension> extensions = [];
        ReadOnlySpan<char> seedSpan;
        if (dash == -1)
        {
            seedSpan = value;
        }
        else
        {
            seedSpan = value.AsSpan()[..dash];
            ReadOnlySpan<char> extensionsSpan = value.AsSpan()[(dash + 1)..];
            if (!TryParseExtensions(extensionsSpan.ToString(), extensions, out error))
            {
                return false;
            }
        }

        if (!ulong.TryParse(seedSpan, out ulong seed))
        {
            seedSpec = null;
            error = $"Could not parse '{value}' to a UInt64 seed";
            return false;
        }

        seedSpec = new SeedSpecification(seed, extensions);
        error = null;
        return true;
    }
}
