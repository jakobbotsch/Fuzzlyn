using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Fuzzlyn.Types
{
    internal abstract class FuzzType
    {
        public abstract TypeSyntax GenReferenceTo();
    }
}
