namespace Fuzzlyn.ExecutionServer;

public class ProgramPairResults
{
    public ProgramPairResults(
        ProgramResult debugResult, ProgramResult releaseResult,
        long debugNumChecksumCalls, long releaseNumChecksumCalls,
        ChecksumSite debugFirstUnmatch, ChecksumSite releaseFirstUnmatch)
    {
        DebugResult = debugResult;
        ReleaseResult = releaseResult;
        DebugNumChecksumCalls = debugNumChecksumCalls;
        ReleaseNumChecksumCalls = releaseNumChecksumCalls;
        DebugFirstUnmatch = debugFirstUnmatch;
        ReleaseFirstUnmatch = releaseFirstUnmatch;
    }

    public ProgramResult DebugResult { get; }
    public ProgramResult ReleaseResult { get; }
    public long DebugNumChecksumCalls { get; }
    public long ReleaseNumChecksumCalls { get; }
    public ChecksumSite DebugFirstUnmatch { get; }
    public ChecksumSite ReleaseFirstUnmatch { get; }
}
