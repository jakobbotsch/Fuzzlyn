using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Fuzzlyn.ExecutionServer
{
    public static class Executor
    {
        public static ProgramPairResults RunPair(AssemblyLoadContext alc, ProgramPair pair)
        {
            ProgramResult debugResult = RunAndGetResult(pair.Debug);
            ProgramResult releaseResult = RunAndGetResult(pair.Release);
            ChecksumSite unmatch1 = null;
            ChecksumSite unmatch2 = null;

            if (debugResult.Checksum != releaseResult.Checksum && pair.TrackOutput)
            {
                int index;
                int count = Math.Min(debugResult.ChecksumSites.Count, releaseResult.ChecksumSites.Count);
                for (index = 0; index < count; index++)
                {
                    ChecksumSite val1 = debugResult.ChecksumSites[index];
                    ChecksumSite val2 = releaseResult.ChecksumSites[index];
                    if (val1 != val2)
                        break;
                }

                if (index < debugResult.ChecksumSites.Count)
                    unmatch1 = debugResult.ChecksumSites[index];
                if (index < releaseResult.ChecksumSites.Count)
                    unmatch2 = releaseResult.ChecksumSites[index];
            }

            return new ProgramPairResults(debugResult, releaseResult, unmatch1, unmatch2);

            ProgramResult RunAndGetResult(byte[] bytes)
            {
                Assembly asm = alc.LoadFromStream(new MemoryStream(bytes));
                MethodInfo mainMethodInfo = asm.GetType("Program").GetMethod("Main");
                Action<IRuntime> entryPoint = (Action<IRuntime>)Delegate.CreateDelegate(typeof(Action<IRuntime>), mainMethodInfo);
                var runtime = new Runtime();
                if (pair.TrackOutput)
                    runtime.ChecksumSites = new List<ChecksumSite>();
                Exception ex = null;
                try
                {
                    entryPoint(runtime);
                }
                catch (Exception caughtEx)
                {
                    ex = caughtEx;
                }

                // A reference to the runtime stays in the loaded assembly and there may be a lot of checksum sites
                // so during reduction this can use a lot of memory.
                List<ChecksumSite> checksumSites = runtime.ChecksumSites;
                runtime.ChecksumSites = null;
                return new ProgramResult(runtime.FinishHashCode(), ex?.GetType().FullName, ex?.ToString(), ex?.StackTrace, checksumSites);
            }
        }
    }
}
