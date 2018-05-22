using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NDesk.Options;
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
            FuzzlynOptions options = new FuzzlynOptions();
            bool help = false;
            OptionSet optionSet = new OptionSet
            {
                { "seed=|s=", (ulong v) => options.Seed = v },
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

            GenerateProgram(options);
        }

        private static void GenerateProgram(FuzzlynOptions options)
        {
            CGContext context = new CGContext(options);
            context.GenerateTypes();
            string program = context.OutputProgram().NormalizeWhitespace().ToFullString();
        }
    }
}
