namespace Fuzzlyn.ExecutionServer;

public class ProgramPairResults(
    ProgramResult debugResult, ProgramResult releaseResult,
    long debugNumChecksumCalls, long releaseNumChecksumCalls,
    ChecksumSite debugFirstUnmatch, ChecksumSite releaseFirstUnmatch)
{
    public ProgramResult DebugResult { get; } = debugResult;
    public ProgramResult ReleaseResult { get; } = releaseResult;
    public long DebugNumChecksumCalls { get; } = debugNumChecksumCalls;
    public long ReleaseNumChecksumCalls { get; } = releaseNumChecksumCalls;
    public ChecksumSite DebugFirstUnmatch { get; } = debugFirstUnmatch;
    public ChecksumSite ReleaseFirstUnmatch { get; } = releaseFirstUnmatch;
}
