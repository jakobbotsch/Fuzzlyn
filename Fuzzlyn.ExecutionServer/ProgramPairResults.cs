namespace Fuzzlyn.ExecutionServer;
using System;

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

    public static ProgramPairResults Create(ProgramResult baseResult, ProgramResult diffResult, bool trackOutput)
    {
        ChecksumSite unmatch1 = null;
        ChecksumSite unmatch2 = null;
        if (baseResult.Checksum != diffResult.Checksum && trackOutput)
        {
            int index;
            int count = Math.Min(baseResult.ChecksumSites.Count, diffResult.ChecksumSites.Count);
            for (index = 0; index < count; index++)
            {
                ChecksumSite val1 = baseResult.ChecksumSites[index];
                ChecksumSite val2 = diffResult.ChecksumSites[index];
                if (val1 != val2)
                    break;
            }

            if (index < baseResult.ChecksumSites.Count)
                unmatch1 = baseResult.ChecksumSites[index];
            if (index < diffResult.ChecksumSites.Count)
                unmatch2 = diffResult.ChecksumSites[index];
        }
        return new ProgramPairResults(baseResult, diffResult, baseResult.NumChecksumCalls, diffResult.NumChecksumCalls, unmatch1, unmatch2);
    }
}
