using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Fuzzlyn
{
    internal class Program
    {
        private const int MaxDepth = 10;

        private static void Main(string[] args)
        {
            for (int i = 0; ; i++)
            {
                string program = GenerateProgram();
                Console.WriteLine(program);
                var test = SyntaxFactory.ParseCompilationUnit(program);
                Console.Title = i.ToString();
            }
            Console.ReadLine();
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

                try
                {
                    for (int i = 0; i < arr.Length; i++)
                        arr[i] = GenerateProgramPart(generator, innerType, depth + 1);
                }
                catch (ArgumentOutOfRangeException)
                {
                    return Activator.CreateInstance(type, new object[] { Array.CreateInstance(innerType, 0) });
                }

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

        private static readonly Random s_rand = new Random();
    }
}
