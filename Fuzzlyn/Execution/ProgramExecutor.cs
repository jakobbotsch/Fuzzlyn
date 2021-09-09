using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Fuzzlyn.Execution
{
    internal static class ProgramExecutor
    {
        [Flags]
        private enum ErrorModes
        {
            SYSTEM_DEFAULT         = 0x0,
            SEM_FAILCRITICALERRORS     = 0x0001,
            SEM_NOALIGNMENTFAULTEXCEPT = 0x0004,
            SEM_NOGPFAULTERRORBOX      = 0x0002,
            SEM_NOOPENFILEERRORBOX     = 0x8000
        }

        [DllImport("kernel32.dll")]
        private static extern ErrorModes SetErrorMode(ErrorModes uMode);

        public static void Run(string executeInput)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // Prevent post-mortem debuggers from launching here. We get
                // runtime crashes sometimes and the parent Fuzzlyn process will
                // handle it.
                SetErrorMode(ErrorModes.SEM_NOGPFAULTERRORBOX);
            }

            string input;
            if (executeInput == null)
                input = Console.In.ReadToEnd();
            else
                input = File.ReadAllText(executeInput);
            List<ProgramPair> programs = JsonConvert.DeserializeObject<List<ProgramPair>>(input);
            List<ProgramPairResults> results = programs.Select(RunPair).ToList();
            Console.Write(JsonConvert.SerializeObject(results));
        }

        internal static ProgramPairResults RunPair(ProgramPair pair)
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
                Assembly asm = Assembly.Load(bytes);
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

        // Launches a new instance of Fuzzlyn to run the specified programs in.
        public static RunSeparatelyResults RunSeparately(List<ProgramPair> programs, int timeout)
        {
            string host;
            using (Process proc = Process.GetCurrentProcess())
            using (ProcessModule mm = proc.MainModule)
            {
                host = mm.FileName;
            }

            string fuzzlynAssemblyPath = Assembly.GetExecutingAssembly().Location;
            bool hostIsFuzzlyn = Path.ChangeExtension(host, ".dll") == fuzzlynAssemblyPath;
            ProcessStartInfo info = new()
            {
                FileName = host,
                Arguments = hostIsFuzzlyn ? "--execute-programs" : $"\"{fuzzlynAssemblyPath}\" --execute-programs",
                WorkingDirectory = Environment.CurrentDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
            };

            // Disable tiering as even release builds will run in minopts otherwise.
            info.EnvironmentVariables["COMPlus_TieredCompilation"] = "0";

            using (Process proc = Process.Start(info))
            {
                proc.StandardInput.Write(JsonConvert.SerializeObject(programs));
                proc.StandardInput.Close();
                Task<string> resultsTask = proc.StandardOutput.ReadToEndAsync();
                Task<string> errorTask = proc.StandardError.ReadToEndAsync();
                if (!proc.WaitForExit(timeout))
                {
                    proc.Kill();
                    resultsTask.ContinueWith(t => { }, TaskContinuationOptions.OnlyOnFaulted);
                    errorTask.ContinueWith(t => { }, TaskContinuationOptions.OnlyOnFaulted);
                    return new RunSeparatelyResults(RunSeparatelyResultsKind.Timeout, null, null);
                }
                string stdout = resultsTask.Result;
                string stderr = errorTask.Result;

                var pairResults = JsonConvert.DeserializeObject<List<ProgramPairResults>>(stdout);
                if (pairResults == null)
                {
                    return new RunSeparatelyResults(RunSeparatelyResultsKind.Crash, null, stderr);
                }

                return new RunSeparatelyResults(RunSeparatelyResultsKind.Success, pairResults, null);
            }
        }
    }

    internal class ProgramPair
    {
        public ProgramPair(bool trackOutput, byte[] debug, byte[] release)
        {
            TrackOutput = trackOutput;
            Debug = debug;
            Release = release;
        }

        public bool TrackOutput { get; }
        public byte[] Debug { get; }
        public byte[] Release { get; }
    }

    internal class ProgramPairResults
    {
        public ProgramPairResults(
            ProgramResult debugResult, ProgramResult releaseResult,
            ChecksumSite debugFirstUnmatch, ChecksumSite releaseFirstUnmatch)
        {
            DebugResult = debugResult;
            ReleaseResult = releaseResult;
            DebugFirstUnmatch = debugFirstUnmatch;
            ReleaseFirstUnmatch = releaseFirstUnmatch;
        }

        public ProgramResult DebugResult { get; }
        public ProgramResult ReleaseResult { get; }
        public ChecksumSite DebugFirstUnmatch { get; }
        public ChecksumSite ReleaseFirstUnmatch { get; }
    }

    internal class ProgramResult
    {
        public ProgramResult(
            string checksum,
            string exceptionType,
            string exceptionText,
            string exceptionStackTrace,
            List<ChecksumSite> checksumSites)
        {
            Checksum = checksum;
            ExceptionType = exceptionType;
            ExceptionText = exceptionText;
            ExceptionStackTrace = exceptionStackTrace;
            ChecksumSites = checksumSites;
        }

        public string Checksum { get; }
        public string ExceptionType { get; }
        public string ExceptionText { get; }
        public string ExceptionStackTrace { get; }
        [JsonIgnore]
        public List<ChecksumSite> ChecksumSites { get; }
    }

    internal enum RunSeparatelyResultsKind
    {
        Crash,
        Timeout,
        Success
    }

    internal class RunSeparatelyResults
    {
        public RunSeparatelyResults(RunSeparatelyResultsKind kind, List<ProgramPairResults> results, string crashError)
        {
            Kind = kind;
            Results = results;
            CrashError = crashError;
        }

        public RunSeparatelyResultsKind Kind { get; }
        public List<ProgramPairResults> Results { get; }
        public string CrashError { get; }
    }
}
