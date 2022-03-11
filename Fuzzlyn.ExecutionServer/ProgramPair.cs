namespace Fuzzlyn.ExecutionServer;

public class ProgramPair
{
    public ProgramPair(bool trackOutput, byte[] debug, byte[] release)
    {
        TrackOutput = trackOutput;
        Debug = debug;
        Release = release;
    }

    public bool TrackOutput { get; }
    public byte[] Debug { get; }
    public byte[] Release { get; }
}
