namespace Fuzzlyn.ExecutionServer;

public class ProgramSingle(bool trackOutput, byte[] assembly)
{
    public bool TrackOutput { get; } = trackOutput;
    public byte[] Assembly { get; } = assembly;
}