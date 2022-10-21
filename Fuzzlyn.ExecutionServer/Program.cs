using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Text.Json;
using System.Threading;

namespace Fuzzlyn.ExecutionServer;

public static class Program
{
    public static void Main(string[] args)
    {
        const int stackSize = 64 * 1024 * 1024;
        Thread thread = new(() =>
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Prevent post-mortem debuggers from launching here. We get
                // runtime crashes sometimes and the parent Fuzzlyn process will
                // handle it.
                SetErrorMode(ErrorModes.SEM_NOGPFAULTERRORBOX);
            }

            Func<string> readLine;
            if (args.Length > 0)
            {
                string[] lines = File.ReadAllLines(args[0]);
                int index = 0;
                readLine = () => index >= lines.Length ? null : lines[index++];
            }
            else
            {
                readLine = Console.ReadLine;
            }

            AssemblyLoadContext currentAlc = new ServerAssemblyLoadContext();
            int currentRunsInAlc = 0;
            while (true)
            {
                string line = readLine();
                if (line == null)
                    return;

                Request req = JsonSerializer.Deserialize<Request>(line);
                Response resp;
                switch (req.Kind)
                {
                    case RequestKind.RunPair:
                        ProgramPairResults results = RunPairAsync(currentAlc, req.Pair);
                        currentRunsInAlc++;
                        resp = new Response
                        {
                            RunPairResult = results
                        };
                        break;
                    case RequestKind.Shutdown:
                        resp = new Response();
                        break;
                    default:
                        return;
                }

                Console.WriteLine(JsonSerializer.Serialize(resp));
                if (req.Kind == RequestKind.Shutdown)
                    return;

                if (currentRunsInAlc > 100)
                {
                    currentAlc.Unload();
                    currentAlc = new ServerAssemblyLoadContext();
                    currentRunsInAlc = 0;
                }
            }
        }, stackSize);

        thread.Start();
        thread.Join();
    }

    private static ProgramPairResults RunPairAsync(AssemblyLoadContext alc, ProgramPair pair)
    {
        ProgramResult debugResult = RunAndGetResultAsync(pair.Debug);
        ProgramResult releaseResult = RunAndGetResultAsync(pair.Release);
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

        return new ProgramPairResults(debugResult, releaseResult, debugResult.NumChecksumCalls, releaseResult.NumChecksumCalls, unmatch1, unmatch2);

        ProgramResult RunAndGetResultAsync(byte[] bytes)
        {
            Assembly asm = alc.LoadFromStream(new MemoryStream(bytes));
            MethodInfo mainMethodInfo = asm.GetType("Program").GetMethod("Main");
            Action<IRuntime> entryPoint = (Action<IRuntime>)Delegate.CreateDelegate(typeof(Action<IRuntime>), mainMethodInfo);
            var runtime = new Runtime();

            List<ChecksumSite> TakeChecksumSites()
            {
                // A reference to the runtime stays in the loaded assembly and there may be a lot of checksum sites
                // so during reduction this can use a lot of memory.
                List<ChecksumSite> checksumSites = runtime.ChecksumSites;
                runtime.ChecksumSites = null;
                return checksumSites;
            }

            if (pair.TrackOutput)
                runtime.ChecksumSites = new List<ChecksumSite>();

            FillStack(8 * 1024 * 1024);

            int threadID = Environment.CurrentManagedThreadId;
            List<Exception> exceptions = null;
            void FirstChanceExceptionHandler(object sender, FirstChanceExceptionEventArgs args)
            {
                if (Environment.CurrentManagedThreadId == threadID)
                {
                    (exceptions ??= new List<Exception>()).Add(args.Exception);
                }
            }

            AppDomain.CurrentDomain.FirstChanceException += FirstChanceExceptionHandler;
            try
            {
                entryPoint(runtime);
            }
            catch
            {
                // We consider the innermost exception the root cause and only report it.
                // Otherwise we may be confusing the viewer about what the problem is.
                // Consider for example (adapted from a real example):
                // try
                // {
                //   value = -1;
                //   FunctionThatJitAssertsInDebug();
                //   value = 1;
                // }
                // finally
                // {
                //   int.MinValue / value;
                // }
                // We are interested in the JIT assert that was hit, and not the OverflowException
                // thrown because value = 1 did not get to run.
                Exception ex = exceptions[0];

                if (ex is InvalidProgramException && ex.Message.Contains("JIT assert failed"))
                {
                    return new ProgramResult
                    {
                        Kind = ProgramResultKind.HitsJitAssert,
                        Checksum = runtime.FinishHashCode(),
                        ChecksumSites = TakeChecksumSites(),
                        NumChecksumCalls = runtime.NumChecksumCalls,
                        JitAssertError = ex.Message,
                    };
                }

                return new ProgramResult
                {
                    Kind = ProgramResultKind.ThrowsException,
                    Checksum = runtime.FinishHashCode(),
                    ChecksumSites = TakeChecksumSites(),
                    NumChecksumCalls = runtime.NumChecksumCalls,
                    ExceptionType = ex.GetType().FullName,
                    ExceptionText = ex.ToString(),
                    ExceptionStackTrace = ex.StackTrace,
                };
            }
            finally
            {
                AppDomain.CurrentDomain.FirstChanceException -= FirstChanceExceptionHandler;
            }

            return new ProgramResult
            {
                Kind = ProgramResultKind.RunsSuccessfully,
                Checksum = runtime.FinishHashCode(),
                ChecksumSites = TakeChecksumSites(),
                NumChecksumCalls = runtime.NumChecksumCalls,
            };
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void FillStack(int size)
    {
        Span<byte> data = stackalloc byte[size];
        data.Fill(0xcd);
        Consume(data);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void Consume(Span<byte> bytes)
    {
    }

    [Flags]
    private enum ErrorModes
    {
        SYSTEM_DEFAULT = 0x0,
        SEM_FAILCRITICALERRORS = 0x0001,
        SEM_NOALIGNMENTFAULTEXCEPT = 0x0004,
        SEM_NOGPFAULTERRORBOX = 0x0002,
        SEM_NOOPENFILEERRORBOX = 0x8000
    }

    [DllImport("kernel32.dll")]
    private static extern ErrorModes SetErrorMode(ErrorModes uMode);

    private class ServerAssemblyLoadContext : AssemblyLoadContext
    {
        public ServerAssemblyLoadContext() : base(true)
        {
        }
    }
}
