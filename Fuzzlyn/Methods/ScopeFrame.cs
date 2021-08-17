using System.Collections.Generic;

namespace Fuzzlyn.Methods
{
    internal class ScopeFrame
    {
        public List<ScopeValue> Values { get; } = new List<ScopeValue>();
    }
}
