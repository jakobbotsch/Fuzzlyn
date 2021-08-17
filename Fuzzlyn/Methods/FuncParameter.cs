using Fuzzlyn.Types;

namespace Fuzzlyn.Methods
{
    public class FuncParameter
    {
        public FuncParameter(FuzzType type, string name)
        {
            Type = type;
            Name = name;
        }

        public FuzzType Type { get; }
        public string Name { get; }
    }
}
