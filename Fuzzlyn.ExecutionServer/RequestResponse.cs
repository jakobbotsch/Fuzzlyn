namespace Fuzzlyn.ExecutionServer;

public enum RequestKind
{
    RunPair,
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
}
