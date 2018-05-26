using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Types
{
    public class ArrayType : FuzzType
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

        private TypeSyntax _type;
        public override TypeSyntax GenReferenceTo()
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
    }
}
