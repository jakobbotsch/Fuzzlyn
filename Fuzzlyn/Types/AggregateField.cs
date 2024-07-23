namespace Fuzzlyn.Types;

public class AggregateField(FuzzType type, string name)
{
    public FuzzType Type { get; } = type;
    public string Name { get; } = name;
}
