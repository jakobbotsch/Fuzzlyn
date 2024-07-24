using Fuzzlyn.ExecutionServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fuzzlyn;

internal class SeedSpecification(ulong seed, HashSet<Extension> extensions)
{
    public ulong Seed { get; set; } = seed;
    public HashSet<Extension> Extensions { get; set; } = extensions;

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append(Seed);
        if (Extensions.Count > 0)
        {
            sb.Append('-');
            bool first = true;
            foreach (Extension ext in Extensions)
            {
                if (!first)
                    sb.Append(',');

                sb.Append(ext.ToString().ToLowerInvariant());
                first = false;
            }
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
