using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Fuzzlyn
{
    internal class Program
    {
        private static Random s_rand = new Random(1234);
        private const int MaxDepth = 10;

        private static void Main(string[] args)
        {
            for (int i = 0; ; i++)
            {
                string program = null;
                try
                {
                    program = GenerateProgram();
                }
                catch (Exception ex)
                {
                    File.AppendAllText("GenerationExceptions.txt", "Iteration " + i + ": " + ex.ToString() + Environment.NewLine);
                    continue;
                }

                try
                {
                    SyntaxFactory.ParseCompilationUnit(program);
                }
                catch (Exception ex)
                {
                    string s = string.Format(
                        "Iteration {0}{1}Program: -------------{1}{2}------------{2}{2}Exception: {3}{2}{2}",
                        i, Environment.NewLine, program, ex);

                    File.AppendAllText("ParseExceptions.txt", s);
                    Console.WriteLine("Got exception!");
                }

                if (i % 100 == 0)
                    Console.Title = i.ToString();
            }
        }

        private static string GenerateProgram()
        {
            RandomSyntaxGenerator generator = new RandomSyntaxGenerator(s_rand);
            SyntaxNode program = (SyntaxNode)GenerateProgramPart(generator, typeof(CompilationUnitSyntax), 0);
            return program.NormalizeWhitespace().ToFullString();
        }

        private static object GenerateProgramPart(RandomSyntaxGenerator generator, Type type, int depth)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                int amount = s_rand.Next(1, 3);
                Type innerType = type.GetGenericArguments()[0];

                object[] arr = (object[])Array.CreateInstance(innerType, amount);

                for (int i = 0; i < arr.Length; i++)
                    arr[i] = GenerateProgramPart(generator, innerType, depth + 1);

                object o = Activator.CreateInstance(type, new object[] { arr });
                return o;
            }

            if (type == typeof(string))
                return Helpers.GenerateString(s_rand);

            MethodInfo factory = FindFactory(type, depth <= MaxDepth);
            object[] nodes = factory.GetParameters().Select(pm => GenerateProgramPart(generator, pm.ParameterType, depth + 1)).ToArray();
            return (SyntaxNode)factory.Invoke(generator, nodes);
        }

        private static MethodInfo FindFactory(Type type, bool allowRecursion)
        {
            Type factory = typeof(RandomSyntaxGenerator);
            MethodInfo[] methods = factory.GetMethods();

            List<MethodInfo> candidates =
                methods.Where(
                    m => type.IsAssignableFrom(m.ReturnType) &&
                         (allowRecursion || m.GetCustomAttribute<RecursiveAttribute>() == null))
                       .ToList();

            if (candidates.Count == 1)
                return candidates[0];

            return candidates[s_rand.Next(candidates.Count)];
        }
    }
}
