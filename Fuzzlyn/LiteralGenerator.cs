﻿using Fuzzlyn.Methods;
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
                return
                    ObjectCreationExpression(type.GenReferenceTo())
                    .WithArgumentList(
                        ArgumentList(
                            SeparatedList(agg.Fields.Select(af => Argument(GenLiteral(types, random, af.Type))))));
            case ArrayType arr:
                List<int> dims = GenArrayDimensions(random, arr);
                return GenArrayCreation(types, random, arr, dims);
            case InterfaceType it:
                return GenLiteral(types, random, random.NextElement(types.GetImplementingTypes(it)));
            case VectorType vt:
                return GenVectorCreation(random, vt);
            default:
                throw new Exception("Unreachable");
        }
    }

    private static ExpressionSyntax GenPrimitiveLiteral(Randomizer random, PrimitiveType primType)
    {
        if (!primType.Info.IsNumeric || !random.FlipCoin(random.Options.PickLiteralFromTableProb))
            return primType.Info.GenRandomLiteral(random.Rng);

        TypicalLiteralKind kind;
        if (primType.Info.IsFloat)
        {
            kind = (TypicalLiteralKind)random.Options.FloatTypicalLiteralDist.Sample(random.Rng);
        }
        else if (primType.Info.IsUnsigned)
        {
            kind = (TypicalLiteralKind)random.Options.UnsignedIntegerTypicalLiteralDist.Sample(random.Rng);
        }
        else
        {
            kind = (TypicalLiteralKind)random.Options.SignedIntegerTypicalLiteralDist.Sample(random.Rng);
        }

        return primType.Info.GenTypicalLiteral(kind);
    }

    // REVIEW: should this be private?
    public static (ExpressionSyntax, ExpressionSyntax) GenPrimitiveLiteralLoopBounds(Randomizer random, PrimitiveType primType)
    {
        TypicalLiteralKind kind;
        if (primType.Info.IsFloat)
        {
            kind = (TypicalLiteralKind)random.Options.FloatTypicalLiteralDist.Sample(random.Rng);
        }
        else if (primType.Info.IsUnsigned)
        {
            kind = (TypicalLiteralKind)random.Options.UnsignedIntegerTypicalLiteralDist.Sample(random.Rng);
        }
        else
        {
            kind = (TypicalLiteralKind)random.Options.SignedIntegerTypicalLiteralDist.Sample(random.Rng);
        }

        return primType.Info.GenTypicalLiteralLoopBounds(kind, random);
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

    private static ExpressionSyntax GenVectorCreation(Randomizer random, VectorType vt)
    {
        VectorCreationKind creationKind;
        do
        {
            creationKind = (VectorCreationKind)random.Options.CreateVectorKindDist.Sample(random.Rng);
        } while (creationKind == VectorCreationKind.Create && vt.Width == VectorTypeWidth.WidthUnknown);

        return FuncBodyGenerator.GenVectorCreation(creationKind, vt, () => GenPrimitiveLiteral(random, vt.ElementType));
    }
}
