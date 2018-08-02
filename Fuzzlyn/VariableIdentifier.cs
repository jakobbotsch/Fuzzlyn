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
        /// If taking a ref, what scope can that ref return to?
        /// * Ref parameters have 1
        /// * Globals have int.MaxValue
        /// * Values in the first scope in a function have 0 (and value params have 0)
        /// * Values in the first nested scope in a function have -1
        /// * etc.
        /// </summary>
        public int RefEscapeScope { get; }
    }
}
