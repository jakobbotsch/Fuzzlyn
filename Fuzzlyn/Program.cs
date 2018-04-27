using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;

namespace Fuzzlyn
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = SyntaxFactory.ParseCompilationUnit("namespace A { class B { void C() { } } }");
            Console.WriteLine("Hello World!");
        }
    }
}
