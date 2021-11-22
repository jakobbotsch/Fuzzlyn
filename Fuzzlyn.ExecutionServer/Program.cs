using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Text.Json;
using System.Threading.Tasks;

namespace Fuzzlyn.ExecutionServer
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            using var exceptionListener = new ExceptionListener();
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
                readLine = Console.ReadLine;

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
                        ProgramPairResults results = await RunPairAsync(currentAlc, exceptionListener, req.Pair);
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
        }

        private static async Task<ProgramPairResults> RunPairAsync(AssemblyLoadContext alc, ExceptionListener exceptionListener, ProgramPair pair)
        {
            ProgramResult debugResult = await RunAndGetResultAsync(pair.Debug);
            ProgramResult releaseResult = await RunAndGetResultAsync(pair.Release);
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

            async Task<ProgramResult> RunAndGetResultAsync(byte[] bytes)
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

                exceptionListener.Start();
                try
                {
                    entryPoint(runtime);
                }
                catch (Exception ex)
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
                    SourceException sourceEx = await exceptionListener.GetSourceExceptionAsync(ex);
                    SourceException sourceOrEx = sourceEx ?? new SourceException(ex.GetType().FullName, ex.Message);

                    if (sourceOrEx.ExceptionType == "System.InvalidProgramException" && sourceOrEx.ExceptionMessage.Contains("JIT assert failed"))
                    {
                        return new ProgramResult
                        {
                            Kind = ProgramResultKind.HitsJitAssert,
                            Checksum = runtime.FinishHashCode(),
                            ChecksumSites = TakeChecksumSites(),
                            JitAssertError = sourceOrEx.ExceptionMessage,
                        };
                    }

                    if (sourceEx != null)
                    {
                        return new ProgramResult
                        {
                            Kind = ProgramResultKind.ThrowsException,
                            Checksum = runtime.FinishHashCode(),
                            ChecksumSites = TakeChecksumSites(),
                            ExceptionType = sourceEx.ExceptionType,
                            ExceptionText = sourceEx.ExceptionMessage,
                        };
                    }

                    return new ProgramResult
                    {
                        Kind = ProgramResultKind.ThrowsException,
                        Checksum = runtime.FinishHashCode(),
                        ChecksumSites = TakeChecksumSites(),
                        ExceptionType = ex.GetType().FullName,
                        ExceptionText = ex.ToString(),
                        ExceptionStackTrace = ex.StackTrace,
                    };
                }
                finally
                {
                    exceptionListener.Stop();
                }

                return new ProgramResult
                {
                    Kind = ProgramResultKind.RunsSuccessfully,
                    Checksum = runtime.FinishHashCode(),
                    ChecksumSites = TakeChecksumSites(),
                };
            }
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

        // We are interested in the _first_ exception that happens in the child program,
        // not subsequent exceptions that might be thrown because a finally saw an invalid
        // state due to the first exception. Unfortunately getting it is not too simple,
        // but ETW events provide a way.
        private class ExceptionListener : EventListener
        {
            private bool _capture;
            private List<ThrownExceptionInfo> _thrownExceptions;
            private List<ThrownExceptionInfo> ThrownExceptions => _thrownExceptions ??= new();
            private TaskCompletionSource<int> _currentTcs;
            private (string TypeName, string Message) _currentWaitingFor;

            protected override void OnEventSourceCreated(EventSource eventSource)
            {
                if (eventSource.Name == "Microsoft-Windows-DotNETRuntime")
                {
                    EventKeywords evt = (EventKeywords)0x8000; // Exception
                    EnableEvents(eventSource, EventLevel.Verbose, evt);
                }
            }

            protected override void OnEventWritten(EventWrittenEventArgs eventData)
            {
                object GetPayload(string name)
                    => eventData.Payload[eventData.PayloadNames.IndexOf(name)];

                if (eventData.EventName == "ExceptionThrown_V1")
                {
                    string type = (string)GetPayload("ExceptionType");
                    string message = (string)GetPayload("ExceptionMessage");
                    uint flags = (ushort)GetPayload("ExceptionFlags");
                    const uint isNestedFlag = 0x2;
                    var inf = new ThrownExceptionInfo(type, message, (flags & isNestedFlag) != 0);
                    TaskCompletionSource<int> tcs = null;
                    int tcsIndex = -1;
                    lock (this)
                    {
                        if (!_capture)
                            return;

                        List<ThrownExceptionInfo> exceptions = ThrownExceptions;
                        exceptions.Add(inf);
                        if (_currentTcs != null && _currentWaitingFor.TypeName == type && _currentWaitingFor.Message == message)
                        {
                            tcs = _currentTcs;
                            tcsIndex = exceptions.Count - 1;
                            _currentTcs = null;
                        }
                    }

                    if (tcs != null)
                        tcs.SetResult(tcsIndex);
                }
            }

            public async Task<SourceException> GetSourceExceptionAsync(Exception ex)
            {
                string type = ex.GetType().FullName;
                string message = ex.Message;
                TaskCompletionSource<int> tcs;
                lock (this)
                {
                    List<ThrownExceptionInfo> exceptions = ThrownExceptions;
                    SourceException found = Find(exceptions, exceptions.Count - 1);
                    if (found != null)
                        return found;

                    // Set up TCS
                    _currentTcs = tcs = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);
                    _currentWaitingFor = (type, message);
                }

                int startIndex = await tcs.Task;
                return Find(ThrownExceptions, startIndex);

                SourceException Find(List<ThrownExceptionInfo> exceptions, int startIndex)
                {
                    int index;
                    for (index = startIndex; index >= 0; index--)
                    {
                        ThrownExceptionInfo thrownEx = exceptions[index];
                        if (thrownEx.ExceptionType == type && thrownEx.ExceptionMessage == message)
                        {
                            if (!thrownEx.IsNested)
                                return null;

                            break;
                        }
                    }

                    if (index >= 0)
                    {
                        index--;
                        // Go backwards until we find first exception that is not nested.
                        // That should be the source exception.
                        while (index >= 0)
                        {
                            if (!exceptions[index].IsNested)
                            {
                                ThrownExceptionInfo thrownEx = exceptions[index];
                                return new SourceException(thrownEx.ExceptionType, thrownEx.ExceptionMessage);
                            }
                        }

                        return null;
                    }

                    return null;
                }
            }

            public void Start()
            {
                lock (this)
                {
                    _capture = true;
                }
            }

            public void Stop()
            {
                lock (this)
                {
                    ThrownExceptions.Clear();
                    _capture = false;
                }
            }
        }

        private record class ThrownExceptionInfo(string ExceptionType, string ExceptionMessage, bool IsNested);
        private record class SourceException(string ExceptionType, string ExceptionMessage);
    }
}
