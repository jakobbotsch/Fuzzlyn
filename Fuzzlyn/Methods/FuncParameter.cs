using Fuzzlyn.Types;

namespace Fuzzlyn.Methods;

public class FuncParameter(FuzzType type, string name)
{
    public FuzzType Type { get; } = type;
    public string Name { get; } = name;
}
