using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Fuzzlyn.Types;

public abstract class FuzzType
{
    public abstract SyntaxKind[] AllowedAdditionalAssignmentKinds { get; }
    public abstract TypeSyntax GenReferenceTo();

    public bool IsCastableTo(FuzzType other)
        => Equals(other) ||
           (this is PrimitiveType pt && other is PrimitiveType opt && pt.Info.IsNumeric && opt.Info.IsNumeric) ||
           (this is AggregateType agg && other is InterfaceType it && agg.Implements(it));

    public ArrayType MakeArrayType(int rank = 1)
        => new(this, rank);

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
