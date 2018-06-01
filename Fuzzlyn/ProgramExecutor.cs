using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Fuzzlyn
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

            return new ProgramPairResults(result1, result2);

            ProgramResult RunAndGetResult(byte[] bytes)
            {
                Assembly asm = Assembly.Load(bytes);
                Action entryPoint = (Action)Delegate.CreateDelegate(typeof(Action), asm.EntryPoint);
                try
                {
                    entryPoint();
                    return new ProgramResult(null, null, null);
                }
                catch (Exception ex)
                {
                    return new ProgramResult(ex.GetType().FullName, ex.ToString(), ex.StackTrace);
                }
            }
        }

        // Launches a new instance of Fuzzlyn to run the specified programs in.
        public static List<ProgramPairResults> RunSeparately(List<ProgramPair> programs)
        {
            string tempPath = Path.GetTempFileName();
            File.WriteAllText(tempPath, JsonConvert.SerializeObject(programs));

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
        public ProgramPairResults(ProgramResult result1, ProgramResult result2)
        {
            Result1 = result1;
            Result2 = result2;
        }

        public ProgramResult Result1 { get; }
        public ProgramResult Result2 { get; }
    }

    internal class ProgramResult
    {
        public ProgramResult(string exceptionType, string exceptionText, string exceptionStackTrace)
        {
            ExceptionType = exceptionType;
            ExceptionText = exceptionText;
            ExceptionStackTrace = exceptionStackTrace;
        }

        public string ExceptionType { get; }
        public string ExceptionText { get; }
        public string ExceptionStackTrace { get; }
    }
}
