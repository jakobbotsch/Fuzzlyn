namespace Fuzzlyn.ExecutionServer;

public class ProgramPair(bool trackOutput, byte[] debug, byte[] release)
{
    public bool TrackOutput { get; } = trackOutput;
    public byte[] Debug { get; } = debug;
    public byte[] Release { get; } = release;
}
