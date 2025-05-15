using Fuzzlyn.ExecutionServer;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn;

internal class Compiler
{
    private readonly MetadataReference[] _references;

    private Compiler(MetadataReference[] references)
    {
        _references = references;
    }

    private static int _compiles;
    public CompileResult Compile(CompilationUnitSyntax program, CompilerOptions options)
    {
        int compileID = Interlocked.Increment(ref _compiles);
        SyntaxTree[] trees = [SyntaxTree(program, options.CSParseOptions)];
        CSharpCompilation comp = CSharpCompilation.Create("FuzzlynProgram" + compileID, trees, _references, options.CSCompilerOptions);

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
                return new CompileResult(null, result.Diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error).ToImmutableArray(), null);

            return new CompileResult(null, ImmutableArray<Diagnostic>.Empty, ms.ToArray());
        }
    }

    public static Compiler CreateForHost(string hostPath)
    {
        string hostDir = Path.GetDirectoryName(hostPath);
        string[] assembliesNextToHost = ["System.Private.CoreLib.dll", "System.Runtime.dll", "System.Console.dll", "mscorlib.dll"];
        MetadataReference[] references;
        if (assembliesNextToHost.All(a => File.Exists(Path.Combine(hostDir, a))))
        {
            references =
                assembliesNextToHost.Select(a => MetadataReference.CreateFromFile(Path.Combine(hostDir, a))).ToArray();
        }
        else
        {
            references =
                [MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                // These two are needed to properly pick up System.Object when using methods on System.Console.
                // See here: https://github.com/dotnet/corefx/issues/11601
                MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("System.Runtime")).Location),
                MetadataReference.CreateFromFile(Assembly.Load(new AssemblyName("mscorlib")).Location)];
        }

        references = [.. references, MetadataReference.CreateFromFile(typeof(IRuntime).Assembly.Location)];
        return new Compiler(references);
    }
}

internal class CompilerOptions(string name, CSharpCompilationOptions csCompilerOptions, CSharpParseOptions csParseOptions)
{
    public string Name { get; } = name;
    public CSharpCompilationOptions CSCompilerOptions { get; } = csCompilerOptions;
    public CSharpParseOptions CSParseOptions { get; } = csParseOptions;

    private static readonly CSharpCompilationOptions s_debugCompilationOptions =
        new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary,
            concurrentBuild: false,
            optimizationLevel: OptimizationLevel.Debug,
            specificDiagnosticOptions: [KeyValuePair.Create("SYSLIB5003", ReportDiagnostic.Suppress)]); // Suppress experimental APIs error

    private static readonly CSharpCompilationOptions s_releaseCompilationOptions =
        new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary,
            concurrentBuild: false,
            optimizationLevel: OptimizationLevel.Release,
            specificDiagnosticOptions: [KeyValuePair.Create("SYSLIB5003", ReportDiagnostic.Suppress)]); // Suppress experimental APIs error

    private static readonly CSharpParseOptions s_parseOptions = new(LanguageVersion.Latest);
    private static readonly CSharpParseOptions s_runtimeAsyncParseOptions = s_parseOptions.WithFeatures([KeyValuePair.Create("runtime-async", "on")]);

    public static readonly CompilerOptions DebugOptions = new CompilerOptions("Debug", s_debugCompilationOptions, s_parseOptions);
    public static readonly CompilerOptions ReleaseOptions = new CompilerOptions("Release", s_releaseCompilationOptions, s_parseOptions);
    public static readonly CompilerOptions RuntimeAsyncReleaseOptions = new CompilerOptions("Release with Runtime Async", s_releaseCompilationOptions, s_runtimeAsyncParseOptions);

    public CompilerOptions AsConsoleApp()
    {
        return new CompilerOptions(Name, CSCompilerOptions.WithOutputKind(OutputKind.ConsoleApplication), CSParseOptions);
    }
}

internal class CompileResult(Exception roslynException, ImmutableArray<Diagnostic> compileErrors, byte[] assembly)
{
    public Exception RoslynException { get; } = roslynException;
    public ImmutableArray<Diagnostic> CompileErrors { get; } = compileErrors;
    public byte[] Assembly { get; } = assembly;
}
