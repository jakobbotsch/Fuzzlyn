using System.Collections.Generic;

namespace Fuzzlyn.Methods
{
    internal class ScopeFrame
    {
        public List<VariableIdentifier> Variables { get; } = new List<VariableIdentifier>();
    }
}
