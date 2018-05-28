using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

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
