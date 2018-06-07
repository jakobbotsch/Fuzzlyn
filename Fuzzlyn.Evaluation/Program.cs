using System;
using System.Collections.Generic;
using System.Diagnostics;
using Fuzzlyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Text;
using Fuzzlyn.Reduction;

namespace Fuzzlyn.Evaluation
{
    class Program
    {
        private static int iterations = 10000;

        static void Main(string[] args)
        {
            GenerationTest();

            Console.WriteLine(" ----- ");
            Console.WriteLine(" ----- ");

            ReduceTest();
        }

        static void GenerationTest()
        {
            Console.WriteLine("Running generation tests....");

            var options = new FuzzlynOptions();

            // Warm up.
            var codeGen = new CodeGenerator(options);
            codeGen.GenerateProgram(false);

            // Start timing.
            var stopwatch = Stopwatch.StartNew();

            List<(double size, TimeSpan time)> genTimes = new List<(double size, TimeSpan time)>();
            for (int i = 0; i < iterations; i++) 
            {
                options.Seed = (ulong)i;
                codeGen = new CodeGenerator(options);
                var before = stopwatch.Elapsed;
                var prog = codeGen.GenerateProgram(false);
                var time = stopwatch.Elapsed - before;
                genTimes.Add((prog.NormalizeWhitespace().ToString().Length / 1024.0, time));
            }

            stopwatch.Stop();

            var msPerProgram = stopwatch.ElapsedMilliseconds / iterations;
            var avgSize = genTimes.Average(t => t.size);

            Console.WriteLine("DONE");
            Console.WriteLine($"Iterations: {iterations}");
            Console.WriteLine($"Average time: {msPerProgram} ms");
            Console.WriteLine($"Average size: {avgSize} KiB");

            StringBuilder mathematicaData = new StringBuilder();

            List<(double sizeCenter, double timeAvgMS)> groupTimes =
                genTimes
                .GroupBy(t => (int)(t.size / 5))
                .Select(g => (g.Key * 5 + 2.5, g.Average(t => t.time.TotalMilliseconds)))
                .OrderBy(t => t.Item1)
                .ToList();
            string groupsStr =
                string.Join(
                    ",",
                    groupTimes.Select(t => string.Format(CultureInfo.InvariantCulture, "{{{0}, {1}}}", t.sizeCenter, t.timeAvgMS)));

            mathematicaData.AppendLine($"times = {{{groupsStr}}};");
            // Find size of all programs.
            mathematicaData.AppendLine($"sizes = {{{string.Join(",", genTimes.Select(t => t.size.ToString(CultureInfo.InvariantCulture)))}}};");
            File.WriteAllText("data.txt", mathematicaData.ToString());
        }

        static void ReduceTest()
        {
            Console.WriteLine("Running reduce test ....");

            var options = new FuzzlynOptions();

            ulong[] seeds = Directory.EnumerateFiles(@"../examples/reduced/", "*.cs").Select(f => ulong.Parse(Path.GetFileNameWithoutExtension(f))).ToArray();

            if (seeds.Length < iterations) 
            {
                Console.WriteLine("Not enough seeds. Reduce number of iterations.");
                return;
            }

            // Warm up.
            var codeGenWarmUp = new CodeGenerator(options);
            codeGenWarmUp.GenerateProgram(false);

            var generatedPrograms = new List<CompilationUnitSyntax>();
            var reducedPrograms = new List<CompilationUnitSyntax>();

            for (int i = 0; i < iterations; i++)
            {
                options.Seed = seeds[i];
                var codeGen = new CodeGenerator(options);
                var program = codeGen.GenerateProgram(false);
                generatedPrograms.Add(program);

                var reducer = new Reducer(program, options.Seed.Value);
                var reduced = reducer.Reduce();
                generatedPrograms.Add(reduced);
            }

            // Find size of all programs.
            var avgSize = generatedPrograms.Average((p) => p.ToString().Length / 1024.0);
            var avgSizeReduced = reducedPrograms.Average((p) => p.ToString().Length / 1024.0);

            // I don't know how to do this in a fancy linq way :(
            var sizeDifferences = new List<double>();
            for (int i = 0; i < generatedPrograms.Count; i++) {
                var original = generatedPrograms[i];
                var reduced = reducedPrograms[i];

                sizeDifferences.Add((original.ToString().Length - reduced.ToString().Length) / 1024.0);
            }

            var avgSizeDifferences = sizeDifferences.Average();

            Console.WriteLine("DONE");
            Console.WriteLine($"Iterations: {iterations}");
            Console.WriteLine($"Average original size: {avgSize} KiB");
            Console.WriteLine($"Average reduced size: {avgSizeReduced} KiB");
            Console.WriteLine($"Average size difference: {avgSizeDifferences} KiB");
        }      
    }
}
