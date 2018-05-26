using Fuzzlyn.Types;

namespace Fuzzlyn
{
    internal class VariableIdentifier
    {
        public VariableIdentifier(FuzzType type, string name)
        {
            Type = type;
            Name = name;
        }

        public FuzzType Type { get; }
        public string Name { get; }
    }
}
