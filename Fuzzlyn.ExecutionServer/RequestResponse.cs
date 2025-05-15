namespace Fuzzlyn.ExecutionServer;

public enum RequestKind
{
    RunPair,
    GetSupportedIntrinsicExtensions,
    Shutdown,
}

public class Request
{
    public RequestKind Kind { get; set; }
    public ProgramPair Pair { get; set; }
}

public class Response
{
    public ProgramPairResults RunPairResult { get; set; }
    public Extension[] Extensions { get; set; }
}

