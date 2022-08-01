using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fuzzlyn.ExecutionServer;

public class ProgramResult
{
    public ProgramResultKind Kind { get; init; }
    // Always valid
    public string Checksum { get; init; }
    [JsonIgnore]
    public List<ChecksumSite> ChecksumSites { get; init; }
    public long NumChecksumCalls { get; init; }
    // Next fields are valid for ThrowsException
    public string ExceptionType { get; init; }
    public string ExceptionText { get; init; }
    public string ExceptionStackTrace { get; init; }
    // Valid for HitsJitAssert
    public string JitAssertError { get; init; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProgramResultKind
{
    RunsSuccessfully,
    ThrowsException,
    HitsJitAssert,
}
