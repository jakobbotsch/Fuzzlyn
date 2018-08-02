using Fuzzlyn.Types;

namespace Fuzzlyn
{
    internal class VariableIdentifier
    {
        public VariableIdentifier(FuzzType type, string name, int refEscapeScope)
        {
            Type = type;
            Name = name;
            RefEscapeScope = refEscapeScope;
        }

        public FuzzType Type { get; }
        public string Name { get; }
        /// <summary>
        /// If taking a ref, what scope can that ref return to? If 0, cannot escape from local function.
        /// </summary>
        public int RefEscapeScope { get; }
    }
}
