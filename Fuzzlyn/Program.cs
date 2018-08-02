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
            string removeFixed = null;
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
                { "remove-fixed=", "Remove fixed programs in directory", v => removeFixed = v },
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

            if (options.NumPrograms != 1 && options.Seed.HasValue)
            {
                Console.WriteLine("Error: Must specify exactly 1 program if a seed is specified.");
                return;
            }

            if (options.NumPrograms != 1 && options.Output)
            {
                Console.WriteLine("Error: Must specify exactly 1 program if output is desired.");
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

            if (removeFixed != null)
            {
                RemoveFixedPrograms(options, removeFixed);
                return;
            }

            if (options.Reduce)
                ReduceProgram(options);
            else
                GenerateProgram(options);
        }

        private static void RemoveFixedPrograms(FuzzlynOptions options, string dir)
        {
            string[] files = Directory.GetFiles(dir, "*.cs");
            List<ulong> toRereduce = new List<ulong>();
            for (int i = 0; i < files.Length; i++)
            {
                Console.Title = $"Processing {i + 1}/{files.Length}";

                string contents = File.ReadAllText(files[i]);
                MatchCollection matches = Regex.Matches(contents, "// Seed: ([0-9]+)");
                if (matches.Count != 1)
                    continue;

                ulong seed = ulong.Parse(matches[0].Groups[1].Value);
                options.Seed = seed;
                var cg = new CodeGenerator(options);
                CompilationUnitSyntax original = cg.GenerateProgram(false);

                CompileResult debug = Compiler.Compile(original, Compiler.DebugOptions);
                CompileResult release = Compiler.Compile(original, Compiler.ReleaseOptions);

                if (debug.CompileErrors.Length > 0 || release.CompileErrors.Length > 0)
                    continue;

                if (debug.RoslynException != null || release.RoslynException != null)
                    continue;

                ProgramPairResults execResults = ProgramExecutor.RunPair(new ProgramPair(debug.Assembly, release.Assembly));
                if (execResults.DebugResult.Checksum != execResults.ReleaseResult.Checksum ||
                    execResults.DebugResult.ExceptionType != execResults.ReleaseResult.ExceptionType)
                {
                    // Execute the reduced form to see if we get interesting behavior.
                    // Otherwise we may need to rereduce it.
                    if (!IsReducedVersionInteresting(execResults, contents))
                    {
                        toRereduce.Add(seed);
                        Console.WriteLine("Marking {0} for rereduction", Path.GetFileName(files[i]));
                    }

                    continue;
                }

                Console.WriteLine("Removing {0}", Path.GetFileName(files[i]));
                File.Delete(files[i]);
            }

            const string rereduceFile = "Rereduce_required.txt";
            File.WriteAllText(rereduceFile, string.Join(Environment.NewLine, toRereduce));
            Console.WriteLine("Wrote {0} seeds to be rereduced to '{1}'", toRereduce.Count, Path.GetFullPath(rereduceFile));
        }

        /// <summary>
        /// Checks if a reduced version (on disk) is interesting by running it and checking for exceptions
        /// and output. Output is captured by redirecting stdout during executiong.
        private static bool IsReducedVersionInteresting(ProgramPairResults fullResults, string code)
        {
            CompilationUnitSyntax comp = ParseCompilationUnit(code);

            var debug = Execute(Compiler.DebugOptions);
            var release = Execute(Compiler.ReleaseOptions);

            if (fullResults.DebugResult.ExceptionType != fullResults.ReleaseResult.ExceptionType)
            {
                return debug.exceptionType == fullResults.DebugResult.ExceptionType &&
                       release.exceptionType == fullResults.ReleaseResult.ExceptionType;
            }

            return debug.stdout != release.stdout;

            (string stdout, string exceptionType) Execute(CSharpCompilationOptions opts)
            {
                CompileResult result = Compiler.Compile(comp, opts);
                Trace.Assert(result.Assembly != null);

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

        private static void ReduceProgram(FuzzlynOptions options)
        {
            var cg = new CodeGenerator(options);
            CompilationUnitSyntax original = cg.GenerateProgram(true);
            Reducer reducer = new Reducer(original, options.Seed.Value);
            CompilationUnitSyntax reduced = reducer.Reduce();
            Console.WriteLine(reduced.NormalizeWhitespace().ToFullString());
        }

        private static void GenerateProgram(FuzzlynOptions options)
        {
            ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = options.Parallelism
            };

            int numGenerated = 0;
            Parallel.For(0, options.NumPrograms, po, i =>
            {
                CodeGenerator gen = new CodeGenerator(options);
                CompilationUnitSyntax unit = gen.GenerateProgram(true);
                if (options.Output)
                {
                    string asString = unit.NormalizeWhitespace().ToFullString();
                    Console.Write(asString);
                }
                else
                {
                    Compile(gen, unit);
                }

                int numGen = Interlocked.Increment(ref numGenerated);
                if (numGen % 100 == 0)
                    Console.Title = $"{numGen}/{options.NumPrograms} programs generated";

            });

            ExecuteQueue();
        }

        private static readonly object s_fileLock = new object();
        private static readonly List<(ulong, byte[], byte[])> s_programQueue = new List<(ulong, byte[], byte[])>();
        private static void Compile(CodeGenerator gen, CompilationUnitSyntax program)
        {
            byte[] debug = DoCompilation(Compiler.DebugOptions);
            byte[] release = DoCompilation(Compiler.ReleaseOptions);

            if (debug != null && release != null)
            {
                lock (s_programQueue)
                {
                    s_programQueue.Add((gen.Random.Seed, debug, release));

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
                        File.AppendAllText("Crashes.txt", "seed: " + gen.Random.Seed + Environment.NewLine);

                    return null;
                }

                if (comp.CompileErrors.Length > 0)
                {
                    IEnumerable<Diagnostic> errors = comp.CompileErrors.Where(d => d.Severity == DiagnosticSeverity.Error);
                    string logEntry =
                        "seed: " + gen.Random.Seed + Environment.NewLine +
                        string.Join(Environment.NewLine, errors.Select(d => "  " + d));
                    lock (s_fileLock)
                        File.AppendAllText("Errors.txt", logEntry + Environment.NewLine);

                    return null;
                }

                return comp.Assembly;
            }
        }

        private static void ExecuteQueue()
        {
            if (s_programQueue.Count <= 0)
                return;

            File.AppendAllText("Seed_Trace.txt", "Starting seeds " + string.Join(" ", s_programQueue.Select(t => t.Item1)));

            List<ProgramPairResults> results =
                ProgramExecutor.RunSeparately(s_programQueue.Select(t => new ProgramPair(t.Item2, t.Item3)).ToList());

            Trace.Assert(s_programQueue.Count == results.Count, "Returned results count is wrong");
            for (int i = 0; i < results.Count; i++)
            {
                ProgramPairResults result = results[i];
                bool checksumMismatch = result.DebugResult.Checksum != result.ReleaseResult.Checksum;
                bool exceptionMismatch = result.DebugResult.ExceptionType != result.ReleaseResult.ExceptionType;

                if (checksumMismatch || exceptionMismatch)
                {
                    File.AppendAllText(
                        "Execution_Mismatch.txt",
                        "Seed: " + s_programQueue[i].Item1 + Environment.NewLine + JsonConvert.SerializeObject(result, Formatting.Indented) + Environment.NewLine);
                }
            }

            s_programQueue.Clear();
        }
    }
}
