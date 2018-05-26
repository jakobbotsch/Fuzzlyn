using Fuzzlyn.ProbabilityDistributions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NDesk.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Fuzzlyn
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ulong? seed = null;
            int? numPrograms = null;
            FuzzlynOptions options = null;
            bool help = false;
            bool dumpOptions = false;
            OptionSet optionSet = new OptionSet
            {
                { "seed=|s=", (ulong v) => seed = v },
                { "num-programs=|n=", "Number of programs to generate", (int v) => numPrograms = v },
                {
                    "options=",
                    "Path to options.json. Command-line options will override options from this file.",
                    s => options = JsonConvert.DeserializeObject<FuzzlynOptions>(File.ReadAllText(s))
                },
                { "dump-options", "Dump options to stdout and do nothing else", v => dumpOptions = v != null },
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

            if (dumpOptions)
            {
                Console.Write(JsonConvert.SerializeObject(options, Formatting.Indented));
                return;
            }

            GenerateProgram(options);
        }

        private static void GenerateProgram(FuzzlynOptions options)
        {
            for (int i = 0; i < options.NumPrograms; i++)
            {
                CodeGenerator gen = new CodeGenerator(options);

                gen.GenerateTypes();
                gen.GenerateMethods();

                string program = gen.OutputProgram().NormalizeWhitespace().ToFullString();
                //Console.WriteLine(program);
                options.Seed = null;
            }
        }
    }
}
