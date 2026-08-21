using Fuzzlyn.ExecutionServer;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using Xunit;

namespace Fuzzlyn.UnitTests;

public class ExecutionManagerTests
{
    [Fact]
    public void PairResultsFindFirstChecksumMismatch()
    {
        ProgramResult @base = Successful(
            "base",
            [new ChecksumSite("same", "1"), new ChecksumSite("base", "2")]);
        ProgramResult diff = Successful(
            "diff",
            [new ChecksumSite("same", "1"), new ChecksumSite("diff", "3")]);

        ProgramPairResults results = ProgramPairResults.Create(@base, diff, trackOutput: true);

        Assert.Equal("base", results.BaseFirstUnmatch.Id);
        Assert.Equal("diff", results.DiffFirstUnmatch.Id);
    }

    [Fact]
    public void PairResultsHandleDifferentChecksumSiteCounts()
    {
        ProgramResult @base = Successful("base", [new ChecksumSite("extra", "1")]);
        ProgramResult diff = Successful("diff", []);

        ProgramPairResults results = ProgramPairResults.Create(@base, diff, trackOutput: true);

        Assert.Equal("extra", results.BaseFirstUnmatch.Id);
        Assert.Null(results.DiffFirstUnmatch);
    }

    [Fact]
    public void PairResultsDoNotRequireSitesWhenOutputIsNotTracked()
    {
        ProgramPairResults results = ProgramPairResults.Create(
            Successful("base", null),
            Successful("diff", null),
            trackOutput: false);

        Assert.Null(results.BaseFirstUnmatch);
        Assert.Null(results.DiffFirstUnmatch);
    }

    [Fact]
    public void ProgramResultChecksumSitesRoundTripThroughJson()
    {
        ProgramResult original = Successful("checksum", [new ChecksumSite("site", "value")]);

        ProgramResult deserialized =
            JsonSerializer.Deserialize<ProgramResult>(JsonSerializer.Serialize(original));

        Assert.Equal("site", Assert.Single(deserialized.ChecksumSites).Id);
    }

    [Fact]
    public void TimeoutTakesPrecedenceOverCrash()
    {
        RunSeparatelyResults results = ExecutionManager.Aggregate(
            new RunSingleResults(RunSingleResultsKind.Crash, null, "crash"),
            new RunSingleResults(RunSingleResultsKind.Timeout, null, null),
            trackOutput: false);

        Assert.Equal(RunSeparatelyResultsKind.Timeout, results.Kind);
    }

    [Fact]
    public void CrashErrorsIdentifyEachSide()
    {
        RunSeparatelyResults results = ExecutionManager.Aggregate(
            new RunSingleResults(RunSingleResultsKind.Crash, null, "base error"),
            new RunSingleResults(RunSingleResultsKind.Crash, null, "diff error"),
            trackOutput: false);

        Assert.Equal(RunSeparatelyResultsKind.Crash, results.Kind);
        Assert.Contains("[Base]", results.CrashError);
        Assert.Contains("base error", results.CrashError);
        Assert.Contains("[Diff]", results.CrashError);
        Assert.Contains("diff error", results.CrashError);
    }

    [Fact]
    public void SupportedExtensionsAreIntersected()
    {
        FakePool basePool = new(
            [Extension.Vector128, Extension.Vector256],
            _ => throw new InvalidOperationException());
        FakePool diffPool = new(
            [Extension.Vector128, Extension.X86Sse2],
            _ => throw new InvalidOperationException());
        ExecutionManager manager = new(basePool, diffPool);

        Assert.Equal([Extension.Vector128], manager.GetSupportedIntrinsicExtensions());
    }

    [Fact]
    public void PairExecutionsAreDispatchedConcurrently()
    {
        using CountdownEvent bothStarted = new(2);
        RunSingleResults Run(ProgramSingle program)
        {
            bothStarted.Signal();
            Assert.True(bothStarted.Wait(TimeSpan.FromSeconds(5)));
            return new RunSingleResults(
                RunSingleResultsKind.Success,
                Successful(Convert.ToBase64String(program.Assembly), null),
                null);
        }

        ExecutionManager manager = new(
            new FakePool([], Run),
            new FakePool([], Run));

        RunSeparatelyResults results = manager.RunPair(
            new ProgramPair(false, [1], [2]),
            TimeSpan.FromSeconds(5),
            keepPoolNonEmptyEagerly: false);

        Assert.Equal(RunSeparatelyResultsKind.Success, results.Kind);
        Assert.Equal("AQ==", results.Results.BaseResult.Checksum);
        Assert.Equal("Ag==", results.Results.DiffResult.Checksum);
    }

    private static ProgramResult Successful(string checksum, List<ChecksumSite> sites)
        => new()
        {
            Kind = ProgramResultKind.RunsSuccessfully,
            Checksum = checksum,
            ChecksumSites = sites,
            NumChecksumCalls = sites?.Count ?? 0,
        };

    private sealed class FakePool(
        Extension[] extensions,
        Func<ProgramSingle, RunSingleResults> run) : IExecutionServerPool
    {
        public ExecutionServerConfiguration Configuration { get; } =
            new("test", "host", "server", new Dictionary<string, string>(), null, null);

        public RunSingleResults RunOnPool(
            ProgramSingle program,
            TimeSpan timeout,
            bool keepPoolNonEmptyEagerly)
            => run(program);

        public Extension[] GetSupportedIntrinsicExtensions() => extensions;
    }
}
