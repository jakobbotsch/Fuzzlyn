namespace Fuzzlyn.ExecutionServer;

public interface IRuntime
{
    void Checksum<T>(string id, T val) where T : unmanaged;
    void ChecksumSingle(string id, float val);
    void ChecksumDouble(string id, double val);
    void ChecksumSingles<T>(string id, T val) where T : unmanaged;
    void ChecksumDoubles<T>(string id, T val) where T : unmanaged;
}
