namespace Fuzzlyn.ExecutionServer;

public interface IRuntime
{
    void Checksum<T>(string id, T val);
}
