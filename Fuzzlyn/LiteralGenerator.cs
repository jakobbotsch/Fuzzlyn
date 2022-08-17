using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn;

internal static class LiteralGenerator
{
    public static ExpressionSyntax GenLiteral(TypeManager types, Randomizer random, FuzzType type)
    {
        switch (type)
        {
            case PrimitiveType prim:
                return GenPrimitiveLiteral(random, prim);
            case AggregateType agg:
                ArgumentSyntax GenArg(AggregateField field)
                {
                    Debug.Assert(!(field.Type is RefType));
                    if (field.Type is RefType rt)
                    {
                        Debug.Assert(!rt.InnerType.IsByRefLike);
                        ExpressionSyntax literal = GenLiteral(types, random, rt.InnerType);
                        ExpressionSyntax singleElemArr =
                            ArrayCreationExpression(
                                ArrayType(rt.InnerType.GenReferenceTo())
                                .AddRankSpecifiers(
                                    ArrayRankSpecifier(
                                        SingletonSeparatedList<ExpressionSyntax>(
                                            OmittedArraySizeExpression()))),
                                InitializerExpression(
                                    SyntaxKind.ArrayInitializerExpression,
                                    SingletonSeparatedList(literal)));

                        ExpressionSyntax firstElem =
                            ElementAccessExpression(
                                ParenthesizedExpression(singleElemArr),
                                BracketedArgumentList(
                                    SingletonSeparatedList(
                                        Argument(
                                            LiteralExpression(
                                                SyntaxKind.NumericLiteralExpression,
                                                Literal(0))))));

                        return Argument(firstElem).WithRefKindKeyword(Token(SyntaxKind.RefKeyword));
                    }

                    return Argument(GenLiteral(types, random, field.Type));
                }

                return
                    ObjectCreationExpression(type.GenReferenceTo())
                    .WithArgumentList(
                        ArgumentList(
                            SeparatedList(agg.Fields.Select(GenArg))));
            case ArrayType arr:
                List<int> dims = GenArrayDimensions(random, arr);
                return GenArrayCreation(types, random, arr, dims);
            case InterfaceType it:
                return GenLiteral(types, random, random.NextElement(types.GetImplementingTypes(it)));
            default:
                throw new Exception("Unreachable");
        }
    }

    private static LiteralExpressionSyntax GenPrimitiveLiteral(Randomizer random, PrimitiveType primType)
    {
        if (!primType.Info.IsIntegral || !random.FlipCoin(random.Options.PickLiteralFromTableProb))
            return primType.Info.GenRandomLiteral(random.Rng);

        object minValue = primType.Info.Type.GetField("MinValue").GetValue(null);
        object maxValue = primType.Info.Type.GetField("MaxValue").GetValue(null);
        dynamic val = 0;
        do
        {
            int num = random.Options.LiteralDist.Sample(random.Rng);

            val = num;
            if (num == int.MinValue)
                val = minValue;
            if (num == int.MinValue + 1)
                val = (dynamic)minValue + 1;

            if (num == int.MaxValue)
                val = maxValue;
            if (num == int.MaxValue - 1)
                val = (dynamic)maxValue - 1;
        } while (primType.Info.IsUnsigned && val < 0);

        if (val.GetType() != primType.Info.Type)
            val = Convert.ChangeType(val, primType.Info.Type);

        return LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(val));
    }

    private static List<int> GenArrayDimensions(Randomizer random, ArrayType at)
    {
        int dimsRequired = at.Rank;
        FuzzType elemType = at.ElementType;
        while (elemType is ArrayType innerArr)
        {
            dimsRequired += innerArr.Rank;
            elemType = innerArr.ElementType;
        }

        List<int> dimensions = new(dimsRequired);
        for (int tries = 0; tries < 100; tries++)
        {
            // If we are constructing an aggregate type start out by the number of fields, to limit
            // the number of constants required here.
            int totalSize = elemType is AggregateType elemAgg ? elemAgg.GetTotalNumPrimitiveFields() : 1;

            int maxArrayTotalSize = Math.Max(totalSize, random.Options.MaxArrayTotalSize);

            for (int i = 0; i < dimensions.Capacity; i++)
            {
                int dim = random.Next(1, random.Options.MaxArrayLengthPerDimension + 1);
                if (totalSize * dim > maxArrayTotalSize)
                    break;

                dimensions.Add(dim);
                totalSize *= dim;
            }

            if (dimensions.Count == dimensions.Capacity)
                return dimensions;

            dimensions.Clear();
        }

        for (int i = 0; i < dimsRequired; i++)
            dimensions.Add(1);

        return dimensions;
    }

    private static ExpressionSyntax GenArrayCreation(TypeManager types, Randomizer random, ArrayType at, List<int> dimensions)
    {
        return ArrayCreationExpression(at.GenReferenceToArrayType())
               .WithInitializer(GenArrayInitializer(types, random, at, dimensions, 0));
    }

    private static InitializerExpressionSyntax GenArrayInitializer(TypeManager types, Randomizer random, ArrayType at, List<int> dimensions, int index)
    {
        return
            InitializerExpression(
                SyntaxKind.ArrayInitializerExpression,
                SeparatedList(
                    Enumerable.Range(0, dimensions[index]).Select(_ => GenInner())));

        ExpressionSyntax GenInner()
        {
            if (index != at.Rank - 1)
                return GenArrayInitializer(types, random, at, dimensions, index + 1);

            if (at.ElementType is ArrayType innerArr)
            {
                Debug.Assert(index < dimensions.Count - 1);
                // If inner type is an array, then randomize its length up to the dimension
                // (this means we will create inner arrays of different length)
                List<int> restOfDimensions = dimensions.Skip(index + 1).ToList();
                restOfDimensions[0] = random.Next(1, restOfDimensions[0] + 1);
                ExpressionSyntax creation = GenArrayCreation(types, random, innerArr, restOfDimensions);
                return creation;
            }

            Debug.Assert(index == dimensions.Count - 1);
            return GenLiteral(types, random, at.ElementType);
        }
    }

}
