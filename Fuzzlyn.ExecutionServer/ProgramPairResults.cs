namespace Fuzzlyn.ExecutionServer;

public class ProgramPairResults(
    ProgramResult baseResult, ProgramResult diffResult,
    long baseNumChecksumCalls, long diffNumChecksumCalls,
    ChecksumSite baseFirstUnmatch, ChecksumSite diffFirstUnmatch)
{
    public ProgramResult BaseResult { get; } = baseResult;
    public ProgramResult DiffResult { get; } = diffResult;
    public long BaseNumChecksumCalls { get; } = baseNumChecksumCalls;
    public long DiffNumChecksumCalls { get; } = diffNumChecksumCalls;
    public ChecksumSite BaseFirstUnmatch { get; } = baseFirstUnmatch;
    public ChecksumSite DiffFirstUnmatch { get; } = diffFirstUnmatch;
}
