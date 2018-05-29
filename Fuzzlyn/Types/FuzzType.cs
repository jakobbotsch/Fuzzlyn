using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace Fuzzlyn.Types
{
    public abstract class FuzzType
    {
        public abstract SyntaxKind[] AllowedAdditionalAssignmentKinds { get; }

        public abstract TypeSyntax GenReferenceTo();

        public bool IsConvertibleTo(FuzzType other)
            => Equals(other) || (this is PrimitiveType pt && other is PrimitiveType opt && pt.Info.IsIntegral && opt.Info.IsIntegral);

        public ArrayType MakeArrayType(int rank = 1)
            => new ArrayType(this, rank);
    }
}
