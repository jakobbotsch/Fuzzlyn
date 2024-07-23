using Fuzzlyn.ExecutionServer;
using Fuzzlyn.Reduction;
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
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn;

internal class Program
{
    private static void Main(string[] args)
    {
        ulong? seed = null;
        int? numPrograms = null;
        TimeSpan? timeToRun = null;
        string outputEventsTo = null;
        string host = null;
        int? parallelism = null;
        bool help = false;
        bool? output = null;
        bool? enableChecksumming = null;
        bool? reduce = null;
        string reduceDebugGitDir = null;
        string collectSpmiTo = null;
        string outputPath = null;
        bool? stats = null;
        bool? execute = null;
        string knownErrors = null;
        OptionSet optionSet = new()
        {
            { "seed=|s=", "Seed to use when generating a single program", (ulong v) => seed = v },
            { "parallelism=", "Number of cores to use, or -1 to use all cores available. Default value is 1.", (int? p) => parallelism = p },
            { "num-programs=|n=", "Number of programs to generate. Mutually exclusive with seconds-to-run.", (int v) => numPrograms = v },
            { "seconds-to-run=", "Seconds to run Fuzzlyn for. Mutually exclusive with num-programs.", (int v) => timeToRun = new TimeSpan(0, 0, v) },
            {
                "output-events-to=",
                "File to output events to. " +
                "When a new example is found a line will be output " +
                "corresponding to a JSON object describing the test " +
                "case (seed, type, crash info). Also an event will be " +
                "written before exiting that describes how many test " +
                "cases were generated and the time that was run for.",
                v => outputEventsTo = v
            },
            { "host=", "Host to use when executing programs. Required to point to dotnet or corerun", v => host = v },
            { "output-source", "Output program source instead of feeding them directly to Roslyn and execution", v => output = v != null },
            { "checksum", "Enable or disable checksumming in the generated code", v => enableChecksumming = v != null },
            { "reduce", "Reduce program to a minimal example", v => reduce = v != null },
            { "output=", "Output program source to this path. Also enables writing updates in the console during reduction.", v => outputPath = v },
            { "reduce-debug-git-dir=", "Create reduce path in specified dir (must not exists beforehand)", v => reduceDebugGitDir = v },
            { "collect-spmi-to=", "Collect SPMI collections to specified directory", v => collectSpmiTo = v },
            { "stats", "Generate a bunch of programs and record their sizes", v => stats = v != null },
            { "execute", "Whether or not to execute the generated and compiled programs (enabled by default, disable with --execute-) ", v => execute = v != null },
            { "known-errors=", "A JSON file of known error strings that will be ignored, or the string \"dotnet/runtime\" to use a built-in list for the tip of dotnet/runtime.", v => knownErrors = v },
            { "help|h", v => help = v != null }
        };

        string error = null;
        try
        {
            List<string> leftover = optionSet.Parse(args);
            if (leftover.Any())
                error = "Unknown arguments: " + string.Join(" ", leftover);
        }
        catch (OptionException ex)
        {
            error = ex.Message;
        }

        if (timeToRun.HasValue && numPrograms.HasValue)
        {
            error = "--num-programs and --seconds-to-run are mutually exclusive";
        }

        if (error != null)
        {
            Console.WriteLine("Fuzzlyn: {0}", error);
            Console.WriteLine("Use --help for help.");
            return;
        }

        if (help)
        {
            Console.WriteLine("Usage: fuzzlyn.exe");
            optionSet.WriteOptionDescriptions(Console.Out);
            return;
        }

        FuzzlynOptions options = new();

        if (seed.HasValue)
            options.Seed = seed.Value;
        if (numPrograms.HasValue)
            options.NumPrograms = numPrograms.Value;
        if (timeToRun.HasValue)
            options.TimeToRun = timeToRun;
        if (outputEventsTo != null)
            options.OutputEventsTo = outputEventsTo;
        if (host != null)
            options.Host = host;
        if (parallelism.HasValue)
            options.Parallelism = parallelism.Value;
        if (output.HasValue)
            options.Output = output.Value;
        if (enableChecksumming.HasValue)
            options.EnableChecksumming = enableChecksumming.Value;
        if (reduce.HasValue)
            options.Reduce = reduce.Value;
        if (collectSpmiTo != null)
            options.CollectSpmiTo = collectSpmiTo;
        if (stats.HasValue)
            options.Stats = stats.Value;
        if (execute.HasValue)
            options.Execute = execute.Value;

        if (options.NumPrograms != 1 && options.Seed.HasValue)
        {
            Console.WriteLine("Error: Must specify exactly 1 program if a seed is specified.");
            return;
        }

        if (options.Reduce && !options.Seed.HasValue)
        {
            Console.WriteLine("Error: Cannot reduce without a seed.");
            return;
        }

        if (!options.EnableChecksumming && !options.Output)
        {
            Console.WriteLine("Error: Disabling checksumming is only supported when outputting the example");
            return;
        }

        if (options.Output)
        {
            GenerateProgramsAndOutput(options);
        }
        else if (options.Reduce)
        {
            if (!CreateExecutionServerPool(options))
                return;

            ReduceProgram(options, outputPath, reduceDebugGitDir);
        }
        else if (options.Stats)
        {
            GenerateProgramsAndGetStats(options);
        }
        else
        {
            if (options.Execute)
            {
                if (!CreateExecutionServerPool(options))
                    return;

                if (!LoadKnownErrors(options, knownErrors))
                    return;
            }

            GenerateProgramsAndCheck(options);
        }
    }

    private static ExecutionServerPool s_executionServerPool;

    private static bool CreateExecutionServerPool(FuzzlynOptions options)
    {
        if (!File.Exists(options.Host))
        {
            Console.WriteLine("Error: invalid host specified ({0})", options.Host);
            return false;
        }

        SpmiSetupOptions spmiOptions = null;
        if (options.CollectSpmiTo != null)
        {
            spmiOptions = SpmiSetupOptions.Create(options.CollectSpmiTo, options.Host, out string error);
            if (spmiOptions == null)
            {
                Console.WriteLine("Error: {0}", error);
                return false;
            }
        }
        s_executionServerPool = new ExecutionServerPool(options.Host, spmiOptions);
        return true;
    }

    private static bool LoadKnownErrors(FuzzlynOptions options, string knownErrors)
    {
        if (knownErrors == null)
        {
            options.KnownErrors = new KnownErrors([]);
            return true;
        }

        if (knownErrors == "dotnet/runtime")
        {
            options.KnownErrors = KnownErrors.DotnetRuntime;
        }
        else if (File.Exists(knownErrors))
        {
            options.KnownErrors = new KnownErrors(JsonSerializer.Deserialize<List<string>>(File.ReadAllText(knownErrors)));
        }
        else
        {
            Console.WriteLine("Error: unable to read known errors list.");
            return false;
        }

        return true;
    }

    private static void ReduceProgram(FuzzlynOptions options, string outputPath, string reduceDebugGitDir)
    {
        var cg = new CodeGenerator(options);
        CompilationUnitSyntax original = cg.GenerateProgram();

        Reducer reducer = new(s_executionServerPool, original, options.Seed.Value, reduceDebugGitDir);
        CompilationUnitSyntax reduced = reducer.Reduce();
        string source = reduced.NormalizeWhitespace().ToFullString();
        if (outputPath != null)
            File.WriteAllText(outputPath, source);
        else
            Console.WriteLine(source);
    }

    private static void GenerateProgramsAndOutput(FuzzlynOptions options)
    {
        void Output(CompilationUnitSyntax unit, ulong seed)
        {
            Console.WriteLine(unit.NormalizeWhitespace().ToFullString());
        }

        GeneratePrograms(options, Output);
    }

    private static void GenerateProgramsAndGetStats(FuzzlynOptions options)
    {
        List<double> sizes = new();
        void AddProgramSize(CompilationUnitSyntax unit, ulong seed)
        {
            double size = unit.NormalizeWhitespace().ToFullString().Length;
            lock (sizes)
            {
                sizes.Add(size);
            }
        }

        GeneratePrograms(options, AddProgramSize);

        Console.WriteLine("Stats of {0} examples", sizes.Count);
        Console.WriteLine("Average program size: {0:F2} KiB", sizes.Average() / 1024.0);
        Console.WriteLine("Min program size: {0} B", sizes.Min());
        Console.WriteLine("Max program size: {0:F2} KiB", sizes.Max() / 1024.0);
        Console.WriteLine("# programs < 2 KiB: {0}", sizes.Count(s => s < 2048));
        Console.WriteLine("10th percentiles:");
        List<double> sortedSizes = sizes.OrderBy(s => s).ToList();
        int programsPerPercentile = sortedSizes.Count / 10;
        for (int i = 0; i < 10; i++)
        {
            double min = sortedSizes[i * programsPerPercentile];
            double max = i == 9 ? sortedSizes.Last() : sortedSizes[(i + 1) * programsPerPercentile];
            Console.WriteLine("{0:F2} KiB <= size <{1} {2:F2} KiB: {3} programs", min / 1024, i == 9 ? "=" : "", max / 1024, programsPerPercentile);
        }

        Console.WriteLine();
    }

    private static void GenerateProgramsAndCheck(FuzzlynOptions options)
    {
        int numProgramsWithKnownErrors = 0;
        GenerateProgramsResult result = GeneratePrograms(options, (unit, seed) => CompileAndCheck(options, unit, seed, ref numProgramsWithKnownErrors));
        if (options.OutputEventsTo != null)
            AddEvent(options.OutputEventsTo, new Event(EventKind.RunSummary, DateTimeOffset.UtcNow, null, new RunSummaryEvent(result.DegreeOfParallelism, result.TotalGenerated, numProgramsWithKnownErrors, result.TimeTaken)));
    }

    private static GenerateProgramsResult GeneratePrograms(FuzzlynOptions options, Action<CompilationUnitSyntax, ulong> action)
    {
        int numThreads = options.Parallelism == -1 ? Environment.ProcessorCount : options.Parallelism;

        Stopwatch timer = Stopwatch.StartNew();
        int numProgramsGenerated = 0;

        if (numThreads == 1)
        {
            RunWorker();
        }
        else
        {
            Task[] tasks =
                Enumerable
                .Range(0, numThreads)
                .Select(_ => Task.Factory.StartNew(RunWorker, TaskCreationOptions.LongRunning))
                .ToArray();

            Task.WaitAll(tasks);
        }

        return new GenerateProgramsResult(numThreads, numProgramsGenerated, timer.Elapsed);

        void RunWorker()
        {
            while (true)
            {
                int programIndex = Interlocked.Increment(ref numProgramsGenerated);

                if (options.TimeToRun.HasValue)
                {
                    if (timer.Elapsed > options.TimeToRun)
                    {
                        Interlocked.Decrement(ref numProgramsGenerated);
                        return;
                    }
                }
                else
                {
                    if (programIndex > options.NumPrograms)
                    {
                        Interlocked.Decrement(ref numProgramsGenerated);
                        return;
                    }
                }

                CodeGenerator gen = new(options);
                CompilationUnitSyntax unit = gen.GenerateProgram();
                action(unit, gen.Random.Seed);
                if (programIndex % 100 == 0)
                {
                    string elapsedOutOf = options.TimeToRun.HasValue ? $"/{options.TimeToRun.Value:c}" : "";
                    string numProgramsOutOf = options.TimeToRun.HasValue ? "" : $"/{options.NumPrograms}";
                    TimeSpan elapsedWithoutMs = new TimeSpan(0, 0, (int)timer.Elapsed.TotalSeconds);
                    Console.WriteLine($"{elapsedWithoutMs:c}{elapsedOutOf} elapsed, {programIndex}{numProgramsOutOf} programs generated, {s_numDeviating} examples found");
                }
            }
        }
    }

    private record class GenerateProgramsResult(int DegreeOfParallelism, int TotalGenerated, TimeSpan TimeTaken);

    private static readonly object s_fileLock = new();
    private static void CompileAndCheck(FuzzlynOptions options, CompilationUnitSyntax program, ulong seed, ref int numProgramsWithKnownErrors)
    {
        byte[] debug = Compile(Compiler.DebugOptions);
        byte[] release = Compile(Compiler.ReleaseOptions);

        if (debug == null || release == null)
        {
            return;
        }

        if (!options.Execute)
        {
            return;
        }

        RunSeparatelyResults results = s_executionServerPool.RunPairOnPool(new ProgramPair(false, debug, release), TimeSpan.FromSeconds(20), false);

        switch (results.Kind)
        {
            case RunSeparatelyResultsKind.Crash:
                if (options.KnownErrors.Contains(results.CrashError))
                    Interlocked.Increment(ref numProgramsWithKnownErrors);
                else
                    AddExample(new ExampleEvent(seed, ExampleKind.Crash, results.CrashError));
                break;
            case RunSeparatelyResultsKind.Success:
                CheckExample(seed, results.Results, ref numProgramsWithKnownErrors);
                break;
        }

        byte[] Compile(CSharpCompilationOptions opts)
        {
            CompileResult comp = Compiler.Compile(program, opts);
            if (comp.RoslynException != null)
            {
                lock (s_fileLock)
                    File.AppendAllText("Crashes.txt", "seed: " + seed + Environment.NewLine);

                return null;
            }

            if (comp.CompileErrors.Length > 0)
            {
                IEnumerable<Diagnostic> errors = comp.CompileErrors.Where(d => d.Severity == DiagnosticSeverity.Error);
                string logEntry =
                    "seed: " + seed + Environment.NewLine +
                    string.Join(Environment.NewLine, errors.Select(d => "  " + d));
                lock (s_fileLock)
                    File.AppendAllText("Errors.txt", logEntry + Environment.NewLine);

                return null;
            }

            return comp.Assembly;
        }

        void CheckExample(ulong seed, ProgramPairResults result, ref int numProgramsWithKnownErrors)
        {
            ProgramResult assertRes;
            if ((assertRes = result.DebugResult).Kind == ProgramResultKind.HitsJitAssert ||
                (assertRes = result.ReleaseResult).Kind == ProgramResultKind.HitsJitAssert)
            {
                if (options.KnownErrors.Contains(assertRes.JitAssertError))
                    Interlocked.Increment(ref numProgramsWithKnownErrors);
                else
                    AddExample(new ExampleEvent(seed, ExampleKind.HitsJitAssert, assertRes.JitAssertError));
            }
            else
            {
                bool checksumMismatch = result.DebugResult.Checksum != result.ReleaseResult.Checksum;
                bool exceptionMismatch = result.DebugResult.ExceptionType != result.ReleaseResult.ExceptionType;

                if (checksumMismatch || exceptionMismatch)
                    AddExample(new ExampleEvent(seed, ExampleKind.BadResult, null));
            }
        }

        void AddExample(ExampleEvent example)
        {
            switch (example)
            {
                case { Seed: ulong seed, Kind: ExampleKind.Crash or ExampleKind.HitsJitAssert, Message: string error }:
                    Console.WriteLine("Found example with seed {0} that hits error{1}{2}", seed, Environment.NewLine, error);
                    break;
                case { Seed: ulong seed, Kind: ExampleKind.BadResult }:
                    Console.WriteLine("Found example with seed {0}", seed);
                    break;
            }

            if (options.OutputEventsTo != null)
                AddEvent(options.OutputEventsTo, new Event(EventKind.ExampleFound, DateTimeOffset.UtcNow, example, null));

            Interlocked.Increment(ref s_numDeviating);
        }
    }

    private static void AddEvent(string file, Event evt)
    {
        string line = JsonSerializer.Serialize(evt) + Environment.NewLine;
        lock (s_fileLock)
            File.AppendAllText(file, line);
    }

    private static int s_numDeviating;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    private enum EventKind
    {
        ExampleFound,
        RunSummary,
    }

    private record class Event(EventKind Kind, DateTimeOffset Timestamp, ExampleEvent Example, RunSummaryEvent RunSummary);

    [JsonConverter(typeof(JsonStringEnumConverter))]
    private enum ExampleKind
    {
        BadResult,
        HitsJitAssert,
        Crash,
    }

    private record class ExampleEvent(ulong Seed, ExampleKind Kind, string Message);
    private record class RunSummaryEvent(int DegreeOfParallelism, int TotalProgramsGenerated, int TotalProgramsWithKnownErrors, TimeSpan TotalRunTime);
}
