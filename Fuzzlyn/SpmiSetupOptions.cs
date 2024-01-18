using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuzzlyn;

internal class SpmiSetupOptions
{
    public string CollectionDirectory { get; private set; }
    public string ShimPath { get; private set; }
    public string ShimName { get; private set; }
    public string JitPath { get; private set; }

    public static SpmiSetupOptions Create(string collectionDir, string host, out string error)
    {
        if (!Directory.Exists(collectionDir))
        {
            error = $"SPMI collection directory {collectionDir} does not exist";
            return null;
        }

        SpmiSetupOptions options = new()
        {
            CollectionDirectory = collectionDir
        };

        string hostDir = Path.GetDirectoryName(host);
        (string shimName, string jitName)[] setups =
        {
            ("superpmi-shim-collector.dll", "clrjit.dll"),
            ("libsuperpmi-shim-collector.so", "libclrjit.so"),
            ("libsuperpmi-shim-collector.dylib", "libclrjit.dylib")
        };

        foreach ((string shimName, string jitName) in setups)
        {
            string shimPath = Path.Combine(hostDir, shimName);
            if (!File.Exists(shimPath))
            {
                continue;
            }

            string jitPath = Path.Combine(hostDir, jitName);
            if (!File.Exists(jitPath))
            {
                error = $"Expected JIT to exist next to SPMI shim (at {jitPath})";
                return null;
            }

            options.ShimPath = shimPath;
            options.ShimName = shimName;
            options.JitPath = jitPath;
            error = null;
            return options;
        }

        error = $"Could not find an SPMI shim in host directory {hostDir}";
        return null;
    }
}
