namespace Fuzzlyn.ExecutionServer;

public enum RequestKind
{
    RunSingle,
    GetSupportedIntrinsicExtensions,
    Shutdown,
}

public class Request
{
    public RequestKind Kind { get; set; }
    public ProgramSingle Program { get; set; }
}

public class Response
{
    public ProgramResult RunResult { get; set; }
    public Extension[] Extensions { get; set; }
}
