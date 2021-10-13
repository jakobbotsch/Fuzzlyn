namespace Fuzzlyn.ExecutionServer
{
    public class ProgramPairResults
    {
        public ProgramPairResults(
            ProgramResult debugResult, ProgramResult releaseResult,
            ChecksumSite debugFirstUnmatch, ChecksumSite releaseFirstUnmatch)
        {
            DebugResult = debugResult;
            ReleaseResult = releaseResult;
            DebugFirstUnmatch = debugFirstUnmatch;
            ReleaseFirstUnmatch = releaseFirstUnmatch;
        }

        public ProgramResult DebugResult { get; }
        public ProgramResult ReleaseResult { get; }
        public ChecksumSite DebugFirstUnmatch { get; }
        public ChecksumSite ReleaseFirstUnmatch { get; }
    }
}
