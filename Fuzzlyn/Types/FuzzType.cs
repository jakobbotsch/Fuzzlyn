using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Fuzzlyn.Types
{
    public abstract class FuzzType
    {
        public abstract TypeSyntax GenReferenceTo();

        public ArrayType MakeArrayType(int rank = 1)
            => new ArrayType(this, rank);
    }
}
