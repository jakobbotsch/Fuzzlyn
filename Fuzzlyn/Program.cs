using Fuzzlyn.ProbabilityDistributions;
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
                { "output-programs", "Output programs instead of feeding them directly to Roslyn", v => output = v != null },
                { "execute-programs", "Accept programs to execute on stdin and report back differences", v => executePrograms = v != null },
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

            if (options.NumPrograms > 1 && options.Seed.HasValue)
            {
                Console.WriteLine("Warning: Specifying more than one program is incompatible with a starting seed. Removing starting seed.");
                options.Seed = null;
            }

            if (dumpOptions)
            {
                Console.Write(JsonConvert.SerializeObject(options, Formatting.Indented));
                return;
            }

            GenerateProgram(options);
        }

        private static void GenerateProgram(FuzzlynOptions options)
        {
            ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = options.Parallelism
            };

            StringBuilder sb = new StringBuilder();
            int numGenerated = 0;
            Parallel.For(0, options.NumPrograms, po, i =>
            {
                CodeGenerator gen = new CodeGenerator(options);

                gen.GenerateTypes();
                gen.GenerateMethods();

                CompilationUnitSyntax unit = gen.OutputProgram(options.Output);
                if (options.Output)
                {
                    string asString = unit.NormalizeWhitespace().ToFullString();
                    lock (sb)
                    {
                        sb.AppendLine(asString);
                    }
                }
                else
                {
                    Compile(gen, unit);
                }

                int numGen = Interlocked.Increment(ref numGenerated);
                if (numGen % 100 == 0)
                    Console.Title = $"{numGen}/{options.NumPrograms} programs generated";

            });

            Console.Write(sb.ToString());

            ExecuteQueue();
#if DEBUG
            Console.ReadLine();
#endif
        }

        private static readonly MetadataReference[] s_references = { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) };
        private static readonly CSharpCompilationOptions[] s_optionMatrix =
        {
            new CSharpCompilationOptions(OutputKind.ConsoleApplication, concurrentBuild: false, optimizationLevel: OptimizationLevel.Debug),
            new CSharpCompilationOptions(OutputKind.ConsoleApplication, concurrentBuild: false, optimizationLevel: OptimizationLevel.Release),
        };

        private static readonly object s_fileLock = new object();
        private static readonly List<(ulong, byte[], byte[])> s_programQueue = new List<(ulong, byte[], byte[])>();
        private static void Compile(CodeGenerator gen, CompilationUnitSyntax program)
        {
            CSharpParseOptions parseOpts = new CSharpParseOptions(LanguageVersion.Latest);
            SyntaxTree[] trees = { SyntaxTree(program, parseOpts) };

            List<byte[]> programs = new List<byte[]>();
            foreach (CSharpCompilationOptions opt in s_optionMatrix)
            {
                CSharpCompilation comp = CSharpCompilation.Create("Random", trees, s_references, opt);
                try
                {
                    using (var ms = new MemoryStream())
                    {
                        EmitResult result = comp.Emit(ms);

                        if (result.Success)
                        {
                            programs.Add(ms.ToArray());
                        }
                        else
                        {
                            IEnumerable<Diagnostic> errors = result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error);

                            string logEntry =
                                "seed: " + gen.Random.Seed + Environment.NewLine +
                                string.Join(Environment.NewLine, errors.Select(d => "  " + d));

                            lock (s_fileLock)
                                File.AppendAllText("Errors.txt", logEntry + Environment.NewLine);
                        }
                    }
                }
                catch
                {
                    lock (s_fileLock)
                        File.AppendAllText("Crashes.txt", "seed: " + gen.Random.Seed + Environment.NewLine);
                }
            }

            if (programs.Count == 2)
            {
                lock (s_programQueue)
                {
                    s_programQueue.Add((gen.Random.Seed, programs[0], programs[1]));

                    if (s_programQueue.Count >= 100)
                        ExecuteQueue();
                }
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
                if (result.Result1.ExceptionType != result.Result2.ExceptionType ||
                    (result.Result1.ExceptionType != null && result.Result1.ExceptionType != typeof(DivideByZeroException).FullName))
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
