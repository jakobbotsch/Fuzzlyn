using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Types
{
    public class ArrayType : FuzzType, IEquatable<ArrayType>
    {
        public ArrayType(FuzzType elementType, int rank = 1)
        {
            if (rank < 1)
                throw new ArgumentException("Invalid rank " + rank, nameof(rank));

            ElementType = elementType;
            Rank = rank;


        }

        public FuzzType ElementType { get; }
        public int Rank { get; }

        public override SyntaxKind[] AllowedAdditionalAssignmentKinds { get; } = new SyntaxKind[0];

        public override TypeSyntax GenReferenceTo() => GenReferenceToArrayType();

        private ArrayTypeSyntax _type;
        public ArrayTypeSyntax GenReferenceToArrayType()
        {
            if (_type != null)
                return _type;

            List<int> ranks = new List<int> { Rank };
            // int[][] => ArrayType(ArrayType(Int, 1), 1)
            // int[][,] => ArrayType(ArrayType(Int, 2), 1)
            FuzzType innerElem = ElementType;
            while (innerElem is ArrayType at)
            {
                ranks.Add(at.Rank);
                innerElem = at.ElementType;
            }

            _type = ArrayType(
                innerElem.GenReferenceTo(),
                ranks.Select(
                    r =>
                    ArrayRankSpecifier(
                        SeparatedList(
                            Enumerable.Repeat((ExpressionSyntax)OmittedArraySizeExpression(), r)))).ToSyntaxList());

            return _type;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ArrayType);
        }

        public bool Equals(ArrayType other)
        {
            return other != null &&
                   EqualityComparer<FuzzType>.Default.Equals(ElementType, other.ElementType) &&
                   Rank == other.Rank;
        }

        public override int GetHashCode()
        {
            var hashCode = -1266642290;
            hashCode = hashCode * -1521134295 + EqualityComparer<FuzzType>.Default.GetHashCode(ElementType);
            hashCode = hashCode * -1521134295 + Rank.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(ArrayType type1, ArrayType type2)
        {
            return EqualityComparer<ArrayType>.Default.Equals(type1, type2);
        }

        public static bool operator !=(ArrayType type1, ArrayType type2)
        {
            return !(type1 == type2);
        }
    }
}
