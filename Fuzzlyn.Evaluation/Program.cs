using System;
using System.Collections.Generic;
using System.Diagnostics;
using Fuzzlyn;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace Fuzzlyn.Evaluation
{
    class Program
    {
        private static int iterations = 10000;

        static void Main(string[] args)
        {
            TimeTest();

            Console.WriteLine(" ----- ");
            Console.WriteLine(" ----- ");

            SizeTest();
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
            var avgSize = generatedPrograms.Select((p) => p.ToString().Length / 1024.0).Average();

            Console.WriteLine("DONE");
            Console.WriteLine($"Iterations: {iterations}");
            Console.WriteLine($"Average size: {avgSize} KiB");
        }            
    }
}
