using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Fuzzlyn.Execution
{
    internal static class ProgramExecutor
    {
        public static void Run()
        {
            List<ProgramPair> programs = JsonConvert.DeserializeObject<List<ProgramPair>>(Console.In.ReadToEnd());
            List<ProgramPairResults> results = programs.Select(RunPair).ToList();
            Console.Write(JsonConvert.SerializeObject(results));
        }

        internal static ProgramPairResults RunPair(ProgramPair pair)
        {
            ProgramResult debugResult = RunAndGetResult(pair.Debug);
            ProgramResult releaseResult = RunAndGetResult(pair.Release);
            ChecksumSite unmatch1 = null;
            ChecksumSite unmatch2 = null;

            if (debugResult.Checksum != releaseResult.Checksum)
            {
                int index;
                for (index = 0; index < Math.Min(debugResult.ChecksumSites.Count, releaseResult.ChecksumSites.Count); index++)
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
                using (var runtime = new Runtime())
                {
                    Exception ex = null;
                    try
                    {
                        entryPoint(runtime);
                    }
                    catch (Exception caughtEx)
                    {
                        ex = caughtEx;
                    }

                    return new ProgramResult(runtime.FinishHashCode(), ex?.GetType().FullName, ex?.ToString(), ex?.StackTrace, runtime.ChecksumSites);
                }
            }
        }

        // Launches a new instance of Fuzzlyn to run the specified programs in.
        public static List<ProgramPairResults> RunSeparately(List<ProgramPair> programs)
        {
            string dotnet;
            using (Process proc = Process.GetCurrentProcess())
            using (ProcessModule mm = proc.MainModule)
            {
                dotnet = mm.FileName;
            }

            string fuzzlyn = Assembly.GetExecutingAssembly().Location;
            string fuzzlynDir = Path.GetDirectoryName(fuzzlyn);
            ProcessStartInfo info = new ProcessStartInfo
            {
                FileName = dotnet,
                Arguments = $"\"{fuzzlyn}\" --execute-programs",
                WorkingDirectory = fuzzlynDir,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
            };

            using (Process proc = Process.Start(info))
            {
                proc.StandardInput.Write(JsonConvert.SerializeObject(programs));
                proc.StandardInput.Close();
                string results = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();

                return JsonConvert.DeserializeObject<List<ProgramPairResults>>(results);
            }
        }
    }

    internal class ProgramPair
    {
        public ProgramPair(byte[] debug, byte[] release)
        {
            Debug = debug;
            Release = release;
        }

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
}
