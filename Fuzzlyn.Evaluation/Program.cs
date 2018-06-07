using System;
using System.Collections.Generic;
using System.Diagnostics;
using Fuzzlyn;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using Fuzzlyn.Reduction;
using System.IO;

namespace Fuzzlyn.Evaluation
{
    class Program
    {
        private static int iterations = 1;

        static void Main(string[] args)
        {
            //TimeTest();

            //Console.WriteLine(" ----- ");
            //Console.WriteLine(" ----- ");

            //SizeTest();
            ReduceTest();
        }

        static void TimeTest()
        {
            Console.WriteLine("Running generation time performance test....");

            var options = new FuzzlynOptions();

            // Warm up.
            var codeGenWarmUp = new CodeGenerator(options);
            codeGenWarmUp.GenerateProgram(false);

            // Start timing.
            var stopwatch = Stopwatch.StartNew();
            
            for (int i = 0; i < iterations; i++) 
            {
                options.Seed = (ulong?) i;
                var codeGen = new CodeGenerator(options);
                codeGen.GenerateProgram(false);
            }

            stopwatch.Stop();

            var msPerProgram = stopwatch.ElapsedMilliseconds / iterations;

            Console.WriteLine("DONE");
            Console.WriteLine($"Iterations: {iterations}");
            Console.WriteLine($"Average time: {msPerProgram} ms");
        }

        static void SizeTest()
        {
            Console.WriteLine("Running size test ....");

            var options = new FuzzlynOptions();

            // Warm up.
            var codeGenWarmUp = new CodeGenerator(options);
            codeGenWarmUp.GenerateProgram(false);

            var generatedPrograms = new List<CompilationUnitSyntax>();

            for (int i = 0; i < iterations; i++)
            {
                options.Seed = (ulong?) i;
                var codeGen = new CodeGenerator(options);
                var program = codeGen.GenerateProgram(false);
                generatedPrograms.Add(program);
            }

            // Find size of all programs.
            var avgSize = generatedPrograms.Average((p) => p.ToString().Length / 1024.0);

            Console.WriteLine("DONE");
            Console.WriteLine($"Iterations: {iterations}");
            Console.WriteLine($"Average size: {avgSize} KiB");
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
