using Fuzzlyn.Execution;
using Fuzzlyn.ProbabilityDistributions;
using Fuzzlyn.Reduction;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using NDesk.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ulong? seed = null;
            int? numPrograms = null;
            int? parallelism = null;
            FuzzlynOptions options = null;
            bool help = false;
            bool dumpOptions = false;
            bool? output = null;
            bool? executePrograms = null;
            bool? enableChecksumming = null;
            bool? reduce = null;
            bool? reduceInChildProcesses = null;
            string reduceDebugGitDir = null;
            string removeFixed = null;
            bool? stats = null;
            OptionSet optionSet = new OptionSet
            {
                { "seed=|s=", "Seed to use when generating a single program", (ulong v) => seed = v },
                { "parallelism=", "Number of cores to use", (int? p) => parallelism = p },
                { "num-programs=|n=", "Number of programs to generate", (int v) => numPrograms = v },
                {
                    "options=",
                    "Path to options.json. Command-line options will override options from this file.",
                    s => options = JsonConvert.DeserializeObject<FuzzlynOptions>(File.ReadAllText(s))
                },
                { "dump-options", "Dump options to stdout and do nothing else", v => dumpOptions = v != null },
                { "output-source", "Output program source instead of feeding them directly to Roslyn and execution", v => output = v != null },
                { "execute-programs", "Accept programs to execute on stdin and report back differences", v => executePrograms = v != null },
                { "checksum", "Enable or disable checksumming in the generated code", v => enableChecksumming = v != null },
                { "reduce", "Reduce program to a minimal example", v => reduce = v != null },
                { "reduce-use-child-processes", "Check reduced example candidates in child processes", v => reduceInChildProcesses = v != null },
                { "reduce-debug-git-dir=", "Create reduce path in specified dir (must not exists beforehand)", v => reduceDebugGitDir = v },
                { "remove-fixed=", "Remove fixed programs in directory", v => removeFixed = v },
                { "stats", "Generate a bunch of programs and record their sizes", v => stats = v != null },
                { "help|h", v => help = v != null }
            };

            string error = null;
            try
            {
                List<string> leftover = optionSet.Parse(args);
                if (leftover.Any())
                    error = "Unknown arguments: " + string.Join(" ", leftover);
            }
            catch (OptionException ex)
            {
                error = ex.Message;
            }

            if (error != null)
            {
                Console.WriteLine("Fuzzlyn: {0}", error);
                Console.WriteLine("Use --help for help.");
                return;
            }

            if (help)
            {
                Console.WriteLine("Usage: fuzzlyn.exe");
                optionSet.WriteOptionDescriptions(Console.Out);
                return;
            }

            if (executePrograms.HasValue && executePrograms.Value)
            {
                ProgramExecutor.Run();
                return;
            }

            if (options == null)
                options = new FuzzlynOptions();

            if (seed.HasValue)
                options.Seed = seed.Value;
            if (numPrograms.HasValue)
                options.NumPrograms = numPrograms.Value;
            if (parallelism.HasValue)
                options.Parallelism = parallelism.Value;
            if (output.HasValue)
                options.Output = output.Value;
            if (enableChecksumming.HasValue)
                options.EnableChecksumming = enableChecksumming.Value;
            if (reduce.HasValue)
                options.Reduce = reduce.Value;
            if (reduceInChildProcesses.HasValue)
                options.ReduceWithChildProcesses = reduceInChildProcesses.Value;
            if (stats.HasValue)
                options.Stats = stats.Value;

            if (options.NumPrograms != 1 && options.Seed.HasValue)
            {
                Console.WriteLine("Error: Must specify exactly 1 program if a seed is specified.");
                return;
            }

            if (options.Reduce && !options.Seed.HasValue)
            {
                Console.WriteLine("Error: Cannot reduce without a seed.");
                return;
            }

            if (dumpOptions)
            {
                Console.Write(JsonConvert.SerializeObject(options, Formatting.Indented));
                return;
            }

            string val = Environment.GetEnvironmentVariable("COMPlus_TieredCompilation");
            if (val != "0")
            {
                Console.WriteLine(
                    "Please set the COMPlus_TieredCompilation environment variable " +
                    "to \"0\" before starting Fuzzlyn.");
                Console.WriteLine("For cmd use \"set COMPlus_TieredCompilation=0\".");
                Console.WriteLine("For powershell use \"$env:COMPlus_TieredCompilation='0'\"");
                Console.WriteLine("For bash use \"export COMPlus_TieredCompilation=0\"");
                Console.WriteLine("For Visual Studio, check the debug tab");
            }
            else if (removeFixed != null)
                RemoveFixedPrograms(options, removeFixed);
            else if (options.Reduce)
                ReduceProgram(options, reduceDebugGitDir);
            else if (options.Stats)
                GenerateProgramsAndGetStats(options);
            else if (options.Output)
                GenerateProgramsAndOutput(options);
            else
                GenerateProgramsAndCheck(options);

            if (Debugger.IsAttached)
                Console.ReadLine();
        }

        private static void RemoveFixedPrograms(FuzzlynOptions options, string dir)
        {
            const string rereduceFile = "Rereduce_required.txt";
            string[] files = Directory.GetFiles(dir, "*.cs").OrderBy(p => p.ToLowerInvariant()).ToArray();
            for (int i = 0; i < files.Length; i++)
            {
                Console.Title = $"Processing {i + 1}/{files.Length}";

                string contents = File.ReadAllText(files[i]);
                MatchCollection matches = Regex.Matches(contents, "// Seed: ([0-9]+)");
                if (matches.Count != 1)
                    continue;

                ulong seed = ulong.Parse(matches[0].Groups[1].Value);
                Console.Write("Processing {0}: ", seed);

                options.Seed = seed;
                var cg = new CodeGenerator(options);
                CompilationUnitSyntax original = cg.GenerateProgram();

                CompileResult debug = Compiler.Compile(original, Compiler.DebugOptions);
                CompileResult release = Compiler.Compile(original, Compiler.ReleaseOptions);

                if (debug.CompileErrors.Length > 0 || release.CompileErrors.Length > 0)
                {
                    Console.WriteLine("Compiler error");
                    continue;
                }

                if (debug.RoslynException != null || release.RoslynException != null)
                {
                    Console.WriteLine("Compiler exception");
                    continue;
                }

                RunSeparatelyResults runResults = ProgramExecutor.RunSeparately(
                    new List<ProgramPair> { new ProgramPair(false, debug.Assembly, release.Assembly) },
                    60000);

                if (runResults.Kind != RunSeparatelyResultsKind.Success)
                {
                    Console.WriteLine("Got {0} from sub-process, still interesting", runResults.Kind);
                    continue;
                }

                ProgramPairResults execResults = runResults.Results[0];

                if (execResults.DebugResult.Checksum != execResults.ReleaseResult.Checksum ||
                    execResults.DebugResult.ExceptionType != execResults.ReleaseResult.ExceptionType)
                {
                    // Execute the reduced form to see if we get interesting behavior.
                    // Otherwise we may need to rereduce it.
                    // HACK: Currently IsReducedVersionInteresting runs the programs
                    // in our own process, so we are conservative and do not run programs
                    // that may crash us (it is possible that the unreduced example does not
                    // crash, but that the reduced does.
                    if (contents.Contains("Crashes the runtime") || IsReducedVersionInteresting(execResults, contents))
                    {
                        Console.WriteLine("Still interesting");
                    }
                    else
                    {
                        File.AppendAllText(rereduceFile, seed + Environment.NewLine);
                        Console.WriteLine("Marked for rereduction");
                    }

                    continue;
                }

                Console.WriteLine("Deleted, no longer interesting");
                File.Delete(files[i]);
            }
        }

        /// <summary>
        /// Checks if a reduced version (on disk) is interesting by running it and checking for exceptions
        /// and output. Output is captured by redirecting stdout during executiong.
        private static bool IsReducedVersionInteresting(ProgramPairResults fullResults, string code)
        {
            CompilationUnitSyntax comp = ParseCompilationUnit(code, options: new CSharpParseOptions(LanguageVersion.Latest));

            var debug = Execute(Compiler.DebugOptions);
            var release = Execute(Compiler.ReleaseOptions);

            if (debug.stdout == null || release.stdout == null)
                return true;

            if (fullResults.DebugResult.ExceptionType != fullResults.ReleaseResult.ExceptionType)
            {
                return debug.exceptionType == fullResults.DebugResult.ExceptionType &&
                       release.exceptionType == fullResults.ReleaseResult.ExceptionType;
            }

            return debug.stdout != release.stdout;

            (string stdout, string exceptionType) Execute(CSharpCompilationOptions opts)
            {
                CompileResult result = Compiler.Compile(comp, opts);
                if (result.Assembly == null)
                {
                    Console.WriteLine("Got compiler errors:");
                    Console.WriteLine(string.Join(Environment.NewLine, result.CompileErrors));
                    return (null, null);
                }

                Assembly asm = Assembly.Load(result.Assembly);
                MethodInfo mainMethodInfo = asm.GetType("Program").GetMethod("Main");
                Action entryPoint = (Action)Delegate.CreateDelegate(typeof(Action), mainMethodInfo);

                Exception ex = null;
                TextWriter origOut = Console.Out;

                MemoryStream ms = new MemoryStream();
                StreamWriter sw = new StreamWriter(ms, Encoding.UTF8);

                try
                {
                    Console.SetOut(sw);
                    entryPoint();
                }
                catch (Exception caughtEx)
                {
                    ex = caughtEx;
                }
                finally
                {
                    Console.SetOut(origOut);
                    sw.Close();
                }

                string stdout = Encoding.UTF8.GetString(ms.ToArray());
                return (stdout, ex?.GetType().FullName);
            }
        }

        private static void ReduceProgram(FuzzlynOptions options, string reduceDebugGitDir)
        {
            var cg = new CodeGenerator(options);
            CompilationUnitSyntax original = cg.GenerateProgram();

            Reducer reducer = new Reducer(original, options.Seed.Value, options.ReduceWithChildProcesses, reduceDebugGitDir);
            CompilationUnitSyntax reduced = reducer.Reduce();
            Console.WriteLine(reduced.NormalizeWhitespace().ToFullString());
        }

        private static void GenerateProgramsAndOutput(FuzzlynOptions options)
        {
            void Output(CompilationUnitSyntax unit, ulong seed)
            {
                Console.WriteLine(unit.NormalizeWhitespace().ToFullString());
            }

            GeneratePrograms(options, Output);
        }

        private static void GenerateProgramsAndGetStats(FuzzlynOptions options)
        {
            List<double> sizes = new List<double>();
            void AddProgramSize(CompilationUnitSyntax unit, ulong seed)
            {
                unit = unit.NormalizeWhitespace();
                double size = unit.ToFullString().Length;
                lock (sizes)
                {
                    sizes.Add(size);
                    if (sizes.Count % 500 != 0)
                        return;

                    Console.WriteLine("Stats of {0} examples", sizes.Count);
                    Console.WriteLine("Average program size: {0:F2} KiB", sizes.Average() / 1024.0);
                    Console.WriteLine("Min program size: {0} B", sizes.Min());
                    Console.WriteLine("Max program size: {0} KiB", sizes.Max() / 1024.0);
                    Console.WriteLine("# programs < 2 KiB: {0}", sizes.Count(s => s < 2048));
                    Console.WriteLine("10th percentiles:");
                    List<double> sortedSizes = sizes.OrderBy(s => s).ToList();
                    int programsPerPercentile = sortedSizes.Count / 10;
                    for (int i = 0; i < 10; i++)
                    {
                        double min = sortedSizes[i * programsPerPercentile];
                        double max = i == 9 ? sortedSizes.Last() : sortedSizes[(i + 1) * programsPerPercentile];
                        Console.WriteLine("{0:F2} KiB <= size <{1} {2:F2} KiB: {3} programs", min / 1024, i == 9 ? "=" : "", max / 1024, programsPerPercentile);
                    }

                    Console.WriteLine();
                }
            }

            GeneratePrograms(options, AddProgramSize);
        }

        private static void GenerateProgramsAndCheck(FuzzlynOptions options)
        {
            GeneratePrograms(options, Compile);
            ExecuteQueue();
        }

        private static void GeneratePrograms(FuzzlynOptions options, Action<CompilationUnitSyntax, ulong> action)
        {
            ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = options.Parallelism
            };

            int numGenerated = 0;
            Parallel.For(0, options.NumPrograms, po, i =>
            {
                CodeGenerator gen = new CodeGenerator(options);
                CompilationUnitSyntax unit = gen.GenerateProgram();
                action(unit, gen.Random.Seed);
                int numGen = Interlocked.Increment(ref numGenerated);
                if (numGen % 100 == 0)
                {
                    Console.Title = $"{numGen}/{options.NumPrograms} programs generated, {s_numDeviating} examples found";
                    Console.WriteLine($"{numGen}/{options.NumPrograms} programs generated, {s_numDeviating} examples found");
                }
            });
        }

        private static readonly object s_fileLock = new object();
        private static readonly List<(ulong, byte[], byte[])> s_programQueue = new List<(ulong, byte[], byte[])>();
        private static void Compile(CompilationUnitSyntax program, ulong seed)
        {
            byte[] debug = DoCompilation(Compiler.DebugOptions);
            byte[] release = DoCompilation(Compiler.ReleaseOptions);

            if (debug != null && release != null)
            {
                lock (s_programQueue)
                {
                    s_programQueue.Add((seed, debug, release));

                    if (s_programQueue.Count >= 100)
                        ExecuteQueue();
                }
            }

            byte[] DoCompilation(CSharpCompilationOptions opts)
            {
                CompileResult comp = Compiler.Compile(program, opts);
                if (comp.RoslynException != null)
                {
                    lock (s_fileLock)
                        File.AppendAllText("Crashes.txt", "seed: " + seed + Environment.NewLine);

                    return null;
                }

                if (comp.CompileErrors.Length > 0)
                {
                    IEnumerable<Diagnostic> errors = comp.CompileErrors.Where(d => d.Severity == DiagnosticSeverity.Error);
                    string logEntry =
                        "seed: " + seed + Environment.NewLine +
                        string.Join(Environment.NewLine, errors.Select(d => "  " + d));
                    lock (s_fileLock)
                        File.AppendAllText("Errors.txt", logEntry + Environment.NewLine);

                    return null;
                }

                return comp.Assembly;
            }
        }

        private static int s_numDeviating;
        private static void ExecuteQueue()
        {
            if (s_programQueue.Count <= 0)
                return;

            RunSeparatelyResults results =
                ProgramExecutor.RunSeparately(s_programQueue.Select(t => new ProgramPair(false, t.Item2, t.Item3)).ToList(), 20000);

            if (results.Kind != RunSeparatelyResultsKind.Success)
            {
                // Did not finish run, go linearly
                foreach (var (seed, debug, release) in s_programQueue)
                {
                    results =
                        ProgramExecutor.RunSeparately(
                            new List<ProgramPair> { new ProgramPair(false, debug, release) }, 20000);

                    // Skip time outs as we currently can produce some very long running programs.
                    if (results.Kind == RunSeparatelyResultsKind.Timeout)
                        continue;

                    if (results.Kind == RunSeparatelyResultsKind.Crash)
                        AddExample(seed, results.Kind);
                    else
                        CheckExample(seed, results.Results[0]);
                }
            }
            else
            {
                Trace.Assert(s_programQueue.Count == results.Results.Count, "Returned results count is wrong");
                for (int i = 0; i < results.Results.Count; i++)
                    CheckExample(s_programQueue[i].Item1, results.Results[i]);
            }

            void CheckExample(ulong seed, ProgramPairResults result)
            {
                bool checksumMismatch = result.DebugResult.Checksum != result.ReleaseResult.Checksum;
                bool exceptionMismatch = result.DebugResult.ExceptionType != result.ReleaseResult.ExceptionType;

                if (checksumMismatch || exceptionMismatch)
                    AddExample(seed, RunSeparatelyResultsKind.Success);
            }

            void AddExample(ulong seed, RunSeparatelyResultsKind kind)
            {
                string line = "Seed: " + seed;
                if (kind != RunSeparatelyResultsKind.Success)
                    line += $" ({kind})";
                line += Environment.NewLine;

                File.AppendAllText("Execution_Mismatch.txt", line);
                Interlocked.Increment(ref s_numDeviating);
            }

            s_programQueue.Clear();
        }
    }
}
