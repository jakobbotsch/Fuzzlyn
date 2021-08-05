using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;

namespace Fuzzlyn.Types
{
    public abstract class FuzzType
    {
        public abstract SyntaxKind[] AllowedAdditionalAssignmentKinds { get; }
        public abstract TypeSyntax GenReferenceTo();

        public bool IsCastableTo(FuzzType other)
            => Equals(other) || (this is PrimitiveType pt && other is PrimitiveType opt && pt.Info.IsIntegral && opt.Info.IsIntegral);

        public ArrayType MakeArrayType(int rank = 1)
            => new ArrayType(this, rank);

        public abstract override int GetHashCode();
        public abstract override bool Equals(object obj);

        public static bool operator ==(FuzzType left, FuzzType right)
        {
            if ((object)left == null)
                return (object)right == null;

            return left.Equals(right);
        }

        public static bool operator !=(FuzzType left, FuzzType right)
            => !(left == right);
    }
}
