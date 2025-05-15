namespace Fuzzlyn.ExecutionServer;

public class ProgramPair(bool trackOutput, byte[] @base, byte[] diff)
{
    public bool TrackOutput { get; } = trackOutput;
    public byte[] Base { get; } = @base;
    public byte[] Diff { get; } = diff;
}
