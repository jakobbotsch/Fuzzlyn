namespace Fuzzlyn.Execution
{
    public interface IRuntime
    {
        void Checksum<T>(string id, T val);
    }
}
