using Fuzzlyn.ExecutionServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuzzlyn;

internal sealed class ExecutionManager
{
    private readonly IExecutionServerPool _basePool;
    private readonly IExecutionServerPool _diffPool;

    public ExecutionManager(ExecutionServerConfiguration baseConfiguration, ExecutionServerConfiguration diffConfiguration)
        : this(new ExecutionServerPool(baseConfiguration), new ExecutionServerPool(diffConfiguration))
    {
    }

    internal ExecutionManager(IExecutionServerPool basePool, IExecutionServerPool diffPool)
    {
        _basePool = basePool;
        _diffPool = diffPool;
    }

    public ExecutionServerConfiguration BaseConfiguration => _basePool.Configuration;
    public ExecutionServerConfiguration DiffConfiguration => _diffPool.Configuration;

    public RunSeparatelyResults RunPair(ProgramPair pair, TimeSpan timeout, bool keepPoolNonEmptyEagerly)
    {
        Task<RunSingleResults> baseTask = Task.Run(
            () => _basePool.RunOnPool(
                new ProgramSingle(pair.TrackOutput, pair.Base),
                timeout,
                keepPoolNonEmptyEagerly));
        Task<RunSingleResults> diffTask = Task.Run(
            () => _diffPool.RunOnPool(
                new ProgramSingle(pair.TrackOutput, pair.Diff),
                timeout,
                keepPoolNonEmptyEagerly));

        Task.WaitAll(baseTask, diffTask);
        return Aggregate(baseTask.Result, diffTask.Result, pair.TrackOutput);
    }

    internal static RunSeparatelyResults Aggregate(
        RunSingleResults baseResult,
        RunSingleResults diffResult,
        bool trackOutput)
    {
        if (baseResult.Kind == RunSingleResultsKind.Timeout ||
            diffResult.Kind == RunSingleResultsKind.Timeout)
        {
            return new RunSeparatelyResults(RunSeparatelyResultsKind.Timeout, null, null);
        }

        if (baseResult.Kind == RunSingleResultsKind.Crash ||
            diffResult.Kind == RunSingleResultsKind.Crash)
        {
            List<string> errors = new();
            AddCrashError("Base", baseResult);
            AddCrashError("Diff", diffResult);
            return new RunSeparatelyResults(
                RunSeparatelyResultsKind.Crash,
                null,
                errors.Count == 0 ? null : string.Join(Environment.NewLine, errors));

            void AddCrashError(string side, RunSingleResults result)
            {
                if (result.Kind == RunSingleResultsKind.Crash && !string.IsNullOrWhiteSpace(result.CrashError))
                    errors.Add($"[{side}]{Environment.NewLine}{result.CrashError}");
            }
        }

        return new RunSeparatelyResults(
            RunSeparatelyResultsKind.Success,
            ProgramPairResults.Create(baseResult.Result, diffResult.Result, trackOutput),
            null);
    }

    public Extension[] GetSupportedIntrinsicExtensions()
    {
        Extension[] baseExtensions = _basePool.GetSupportedIntrinsicExtensions();
        Extension[] diffExtensions = _diffPool.GetSupportedIntrinsicExtensions();
        return baseExtensions.Intersect(diffExtensions).ToArray();
    }
}
