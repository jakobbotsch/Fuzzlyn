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
            OptionSet optionSet = new OptionSet
            {
                { "seed=|s=", (ulong v) => seed = v },
                { "parallelism=", "Number of cores to use. -1 to use all.", (int? p) => parallelism = p },
                { "num-programs=|n=", "Number of programs to generate", (int v) => numPrograms = v },
                {
                    "options=",
                    "Path to options.json. Command-line options will override options from this file.",
                    s => options = JsonConvert.DeserializeObject<FuzzlynOptions>(File.ReadAllText(s))
                },
                { "dump-options", "Dump options to stdout and do nothing else", v => dumpOptions = v != null },
                { "output-programs", "Output programs instead of feeding them directly to Roslyn", v => output = v != null },
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
            ParallelOptions po = new ParallelOptions();
            po.MaxDegreeOfParallelism = options.Parallelism;

            StringBuilder sb = new StringBuilder();
            int numGenerated = 0;
            Parallel.For(0, options.NumPrograms, po, i =>
            {
                CodeGenerator gen = new CodeGenerator(options);
                File.AppendAllText("seeds.txt", gen.Random.Seed + Environment.NewLine);

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
            Console.ReadLine();
        }

        private static readonly MetadataReference[] s_references = { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) };
        private static void Compile(CodeGenerator gen, CompilationUnitSyntax program)
        {
            CSharpCompilationOptions opts = new CSharpCompilationOptions(OutputKind.ConsoleApplication, concurrentBuild: false);
            CSharpParseOptions parseOpts = new CSharpParseOptions(LanguageVersion.Latest);
            SyntaxTree[] trees = { SyntaxTree(program, parseOpts) };

            CSharpCompilation comp = CSharpCompilation.Create("Random", trees, s_references, opts);
            try
            {
                EmitResult result = comp.Emit(Stream.Null);
                if (!result.Success)
                {
                    IEnumerable<Diagnostic> errors = result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error);

                    string logEntry =
                        "seed: " + gen.Random.Seed + Environment.NewLine +
                        string.Join(Environment.NewLine, errors.Select(d => "  " + d));

                    File.AppendAllText("Errors.txt", logEntry + Environment.NewLine);
                }
            }
            catch
            {
                File.AppendAllText("Crashes.txt", "seed: " + gen.Random.Seed + Environment.NewLine);
            }
        }
    }
}
