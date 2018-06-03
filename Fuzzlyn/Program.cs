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
            OptionSet optionSet = new OptionSet
            {
                { "seed=|s=", (ulong v) => seed = v },
                { "parallelism=", "Number of cores to use.", (int? p) => parallelism = p },
                { "num-programs=|n=", "Number of programs to generate", (int v) => numPrograms = v },
                {
                    "options=",
                    "Path to options.json. Command-line options will override options from this file.",
                    s => options = JsonConvert.DeserializeObject<FuzzlynOptions>(File.ReadAllText(s))
                },
                { "dump-options", "Dump options to stdout and do nothing else", v => dumpOptions = v != null },
                { "output-source", "Output program source instead of feeding them directly to Roslyn", v => output = v != null },
                { "execute-programs", "Accept programs to execute on stdin and report back differences", v => executePrograms = v != null },
                { "checksum", v => enableChecksumming = v != null },
                { "reduce", "Reduce program to a minimal example", v => reduce = v != null },
                { "help|h", v => help = v != null }
            };

            try
            {
                optionSet.Parse(args);
            }
            catch (OptionException ex)
            {
                Console.WriteLine("Fuzzlyn: {0}", ex.Message);
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

            if (options.Reduce)
                ReduceProgram(options);
            else
                GenerateProgram(options);

#if DEBUG
            Console.ReadLine();
#endif
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
                CompilationUnitSyntax unit = gen.GenerateProgram(options.Output);
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

                if (comp.CompileDiagnostics.Length > 0)
                {
                    IEnumerable<Diagnostic> errors = comp.CompileDiagnostics.Where(d => d.Severity == DiagnosticSeverity.Error);
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
