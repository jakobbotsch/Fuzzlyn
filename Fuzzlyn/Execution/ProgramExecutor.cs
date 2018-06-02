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

        private static ProgramPairResults RunPair(ProgramPair pair)
        {
            ProgramResult result1 = RunAndGetResult(pair.Program1);
            ProgramResult result2 = RunAndGetResult(pair.Program2);
            ChecksumSite unmatch1 = null;
            ChecksumSite unmatch2 = null;

            if (result1.Checksum != result2.Checksum)
            {
                int index;
                for (index = 0; index < Math.Min(result1.ChecksumSites.Count, result2.ChecksumSites.Count); index++)
                {
                    ChecksumSite val1 = result1.ChecksumSites[index];
                    ChecksumSite val2 = result2.ChecksumSites[index];
                    if (val1 != val2)
                        break;
                }

                if (index < result1.ChecksumSites.Count)
                    unmatch1 = result1.ChecksumSites[index];
                if (index < result2.ChecksumSites.Count)
                    unmatch2 = result2.ChecksumSites[index];
            }

            return new ProgramPairResults(result1, result2, unmatch1, unmatch2);

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
        public ProgramPair(byte[] program1, byte[] program2)
        {
            Program1 = program1;
            Program2 = program2;
        }

        public byte[] Program1 { get; }
        public byte[] Program2 { get; }
    }

    internal class ProgramPairResults
    {
        public ProgramPairResults(
            ProgramResult result1, ProgramResult result2,
            ChecksumSite firstUnmatch1, ChecksumSite firstUnmatch2)
        {
            Result1 = result1;
            Result2 = result2;
            FirstUnmatch1 = firstUnmatch1;
            FirstUnmatch2 = firstUnmatch2;
        }

        public ProgramResult Result1 { get; }
        public ProgramResult Result2 { get; }
        public ChecksumSite FirstUnmatch1 { get; }
        public ChecksumSite FirstUnmatch2 { get; }
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
