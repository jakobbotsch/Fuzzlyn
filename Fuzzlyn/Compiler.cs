using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Immutable;
using System.IO;
using System.Reflection;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn
{
    internal static class Compiler
    {
        private static readonly MetadataReference[] s_references =
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(Assembly.GetExecutingAssembly().Location),
            MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            // These two are needed to properly pick up System.Object when using methods on System.Console.
            // See here: https://github.com/dotnet/corefx/issues/11601
            MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Runtime")).Location),
            MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("mscorlib")).Location),
        };

        private static readonly CSharpParseOptions s_parseOptions = new CSharpParseOptions(LanguageVersion.Latest);

        public static readonly CSharpCompilationOptions DebugOptions =
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, concurrentBuild: false, optimizationLevel: OptimizationLevel.Debug);

        public static readonly CSharpCompilationOptions ReleaseOptions =
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, concurrentBuild: false, optimizationLevel: OptimizationLevel.Release);

        public static CompileResult Compile(CompilationUnitSyntax program, CSharpCompilationOptions opts)
        {
            SyntaxTree[] trees = { SyntaxTree(program, s_parseOptions) };
            CSharpCompilation comp = CSharpCompilation.Create("FuzzlynProgram", trees, s_references, opts);

            using (var ms = new MemoryStream())
            {
                EmitResult result;
                try
                {
                    result = comp.Emit(ms);
                }
                catch (Exception ex)
                {
                    return new CompileResult(ex, ImmutableArray<Diagnostic>.Empty, null);
                }

                if (!result.Success)
                    return new CompileResult(null, result.Diagnostics, null);

                return new CompileResult(null, ImmutableArray<Diagnostic>.Empty, ms.ToArray());
            }
        }
    }

    internal class CompileResult
    {
        public CompileResult(Exception roslynException, ImmutableArray<Diagnostic> compileDiagnostics, byte[] assembly)
        {
            RoslynException = roslynException;
            CompileDiagnostics = compileDiagnostics;
            Assembly = assembly;
        }

        public Exception RoslynException { get; }
        public ImmutableArray<Diagnostic> CompileDiagnostics { get; }
        public byte[] Assembly { get; }
    }
}
