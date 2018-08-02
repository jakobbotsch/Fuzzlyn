using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Diagnostics;
using System.Linq;

namespace Fuzzlyn.TableGen
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] types = { "sbyte", "short", "int", "long", "byte", "ushort", "uint", "ulong", "bool" };
            string[] shortNames = { "i08", "i16", "i32", "i64", "u08", "u16", "u32", "u64", "bol" };

            Console.WriteLine("//        {0}", string.Join("  ", shortNames));
            for (int i = 0; i < types.Length; i++)
            {
                Console.Write($"/*{shortNames[i]}*/ {{ ");
                for (int j = 0; j < types.Length; j++)
                {
                    if (j > 0)
                        Console.Write(", ");

                    SyntaxTree tree = SyntaxFactory.ParseSyntaxTree($@"
class C
{{
    public static void Main()
    {{
        {types[i]} v1 = default;
        {types[j]} v2 = default;
        bool b = v1 == v2;
    }}
}}", new CSharpParseOptions(LanguageVersion.CSharp7_3));

                    CSharpCompilation comp = CSharpCompilation.Create(
                        "R",
                        new[] { tree },
                        new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });
                    SemanticModel model = comp.GetSemanticModel(tree);
                    if (model.GetDiagnostics().Any(d => d.Severity == DiagnosticSeverity.Error))
                    {
                        Console.Write("ERR");
                        continue;
                    }

                    var node = (BinaryExpressionSyntax)tree.GetRoot().DescendantNodes().Single(f => f is BinaryExpressionSyntax);
                    IMethodSymbol symbol = (IMethodSymbol)model.GetSymbolInfo(node).Symbol;
                    Trace.Assert(symbol.Parameters.Length == 2 && symbol.Parameters[0].Type == symbol.Parameters[1].Type);
                    switch (symbol.Parameters[0].Type.SpecialType)
                    {
                        case SpecialType.System_Int32: Console.Write("I32"); break;
                        case SpecialType.System_Int64: Console.Write("I64"); break;
                        case SpecialType.System_UInt32: Console.Write("U32"); break;
                        case SpecialType.System_UInt64: Console.Write("U64"); break;
                        case SpecialType.System_Boolean: Console.Write("BOL"); break;
                        default: throw new Exception("wtf");
                    }
                }

                Console.WriteLine(" },");
            }

            Console.ReadLine();
        }
    }
}
