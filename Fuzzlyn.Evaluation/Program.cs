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
        private static int iterations = 1000;

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
            var codeGen = new CodeGenerator(options);

            // Warm up.
            codeGen.GenerateProgram(false);

            // Start timing.
            var stopwatch = Stopwatch.StartNew();
            
            for (int i = 0; i < iterations; i++) 
            {
                codeGen.GenerateProgram(false);
                Console.WriteLine(i);
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
            var codeGen = new CodeGenerator(options);

            // Warm up.
            codeGen.GenerateProgram(false);

            var generatedPrograms = new List<CompilationUnitSyntax>();

            for (int i = 0; i < iterations; i++)
            {
                var program = codeGen.GenerateProgram(false);
                generatedPrograms.Add(program);
                Console.WriteLine(i);
            }

            // Find size of all programs.
            var avgSize = generatedPrograms.Select((p) => p.ToString().Length / 1024.0).Average();

            Console.WriteLine("DONE");
            Console.WriteLine($"Iterations: {iterations}");
            Console.WriteLine($"Average size: {avgSize} KiB");
        }            
    }
}
