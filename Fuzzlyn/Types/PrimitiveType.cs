using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Types;

public class PrimitiveType(SyntaxKind keyword) : FuzzType, IEquatable<PrimitiveType>
{
    public SyntaxKind Keyword { get; } = keyword;
    internal PrimitiveTypeInfo Info => s_infoTable[Keyword];

    public override TypeSyntax GenReferenceTo() => PredefinedType(Token(Keyword));
    public override SyntaxKind[] AllowedAdditionalAssignmentKinds => Info.AllowedAdditionalAssignments;

    public bool Equals(PrimitiveType other)
    {
        return other != null &&
               Keyword == other.Keyword;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as PrimitiveType);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(2, Keyword);
    }

    public static bool operator ==(PrimitiveType type1, PrimitiveType type2)
    {
        return EqualityComparer<PrimitiveType>.Default.Equals(type1, type2);
    }

    public static bool operator !=(PrimitiveType type1, PrimitiveType type2)
    {
        return !(type1 == type2);
    }

    private static readonly Dictionary<SyntaxKind, PrimitiveTypeInfo> s_infoTable;

    static PrimitiveType()
    {
        SyntaxKind[] intAssigns =
        [
            SyntaxKind.AddAssignmentExpression,
            SyntaxKind.SubtractAssignmentExpression,
            SyntaxKind.MultiplyAssignmentExpression,
            SyntaxKind.DivideAssignmentExpression,
            SyntaxKind.ModuloAssignmentExpression,
            SyntaxKind.AndAssignmentExpression,
            SyntaxKind.ExclusiveOrAssignmentExpression,
            SyntaxKind.OrAssignmentExpression,
            SyntaxKind.LeftShiftAssignmentExpression,
            SyntaxKind.RightShiftAssignmentExpression,
            SyntaxKind.PreIncrementExpression,
            SyntaxKind.PostIncrementExpression,
            SyntaxKind.PreDecrementExpression,
            SyntaxKind.PostDecrementExpression,
        ];

        SyntaxKind[] floatAssigns =
        [
            SyntaxKind.AddAssignmentExpression,
            SyntaxKind.SubtractAssignmentExpression,
            SyntaxKind.MultiplyAssignmentExpression,
            SyntaxKind.DivideAssignmentExpression,
            SyntaxKind.ModuloAssignmentExpression,
            SyntaxKind.PreIncrementExpression,
            SyntaxKind.PostIncrementExpression,
            SyntaxKind.PreDecrementExpression,
            SyntaxKind.PostDecrementExpression,
        ];

        SyntaxKind[] boolAssigns =
        [
            SyntaxKind.AndAssignmentExpression,
            SyntaxKind.ExclusiveOrAssignmentExpression,
            SyntaxKind.OrAssignmentExpression,
        ];

        s_infoTable = new Dictionary<SyntaxKind, PrimitiveTypeInfo>
        {
            [SyntaxKind.ULongKeyword] = new PrimitiveTypeInfo
            {
                AllowedAdditionalAssignments = intAssigns,
                GenRandomLiteral = rng => CreateLiteralWithSuffix<ulong>(rng.NextUInt64(), null, "UL", (val, str) => Literal(val, str)),
                GenTypicalLiteral = literal => CreateLiteralWithSuffix<ulong>(FromTypicalLiteral<ulong>(literal), null, "UL", (val, str) => Literal(val, str)),
                GenInRange = (min, max, rng) => CreateLiteralWithSuffix<ulong>(RandomMinMax<ulong>(min, max, rng), null, "UL", (val, str) => Literal(val, str)),
                IsUnsigned = true,
                IsNumeric = true,
                Size = sizeof(ulong),
            },
            [SyntaxKind.LongKeyword] = new PrimitiveTypeInfo
            {
                AllowedAdditionalAssignments = intAssigns,
                GenRandomLiteral = rng => CreateLiteralWithSuffix<long>((long)rng.NextUInt64(), null, "L", (val, str) => Literal(val, str)),
                GenTypicalLiteral = literal => CreateLiteralWithSuffix<long>(FromTypicalLiteral<long>(literal), null, "L", (val, str) => Literal(val, str)),
                GenInRange = (min, max, rng) => CreateLiteralWithSuffix<long>(RandomMinMax<long>(min, max, rng), null, "L", (val, str) => Literal(val, str)),
                IsNumeric = true,
                Size = sizeof(long),
            },
            [SyntaxKind.UIntKeyword] = new PrimitiveTypeInfo
            {
                AllowedAdditionalAssignments = intAssigns,
                GenRandomLiteral = rng => CreateLiteralWithSuffix<uint>((uint)rng.NextUInt64(), null, "U", (val, str) => Literal(val, str)),
                GenTypicalLiteral = literal => CreateLiteralWithSuffix<uint>(FromTypicalLiteral<uint>(literal), null, "U", (val, str) => Literal(val, str)),
                GenInRange = (min, max, rng) => CreateLiteralWithSuffix<uint>(RandomMinMax<uint>(min, max, rng), null, "U", (val, str) => Literal(val, str)),
                IsUnsigned = true,
                IsNumeric = true,
                Size = sizeof(uint),
            },
            [SyntaxKind.IntKeyword] = new PrimitiveTypeInfo
            {
                AllowedAdditionalAssignments = intAssigns,
                GenRandomLiteral = rng => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((int)rng.NextUInt64())),
                GenTypicalLiteral = literal => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(FromTypicalLiteral<int>(literal))),
                GenInRange = (min, max, rng) => LiteralExpression(SyntaxKind.NumericLiteralToken, Literal(RandomMinMax<uint>(min, max, rng))),
                IsNumeric = true,
                Size = sizeof(int),
            },
            [SyntaxKind.UShortKeyword] = new PrimitiveTypeInfo
            {
                AllowedAdditionalAssignments = intAssigns,
                GenRandomLiteral = rng => CreateLiteralWithCast<ushort>(Literal((ushort)rng.NextUInt64()), SyntaxKind.UShortKeyword),
                GenTypicalLiteral = literal => CreateLiteralWithCast<ushort>(Literal(FromTypicalLiteral<ushort>(literal)), SyntaxKind.UShortKeyword),
                GenInRange = (min, max, rng) => CreateLiteralWithCast<ushort>(Literal(RandomMinMax<ushort>(min, max, rng)), SyntaxKind.UShortKeyword),
                IsUnsigned = true,
                IsNumeric = true,
                Size = sizeof(ushort),
            },
            [SyntaxKind.ShortKeyword] = new PrimitiveTypeInfo
            {
                AllowedAdditionalAssignments = intAssigns,
                GenRandomLiteral = rng => CreateLiteralWithCast<short>(Literal((short)rng.NextUInt64()), SyntaxKind.ShortKeyword),
                GenTypicalLiteral = literal => CreateLiteralWithCast<short>(Literal(FromTypicalLiteral<short>(literal)), SyntaxKind.ShortKeyword),
                GenInRange = (min, max, rng) => CreateLiteralWithCast<short>(Literal(RandomMinMax<short>(min, max, rng)), SyntaxKind.ShortKeyword),
                IsNumeric = true,
                Size = sizeof(short),
            },
            [SyntaxKind.ByteKeyword] = new PrimitiveTypeInfo
            {
                AllowedAdditionalAssignments = intAssigns,
                GenRandomLiteral = rng => CreateLiteralWithCast<byte>(Literal((byte)rng.NextUInt64()), SyntaxKind.ByteKeyword),
                GenTypicalLiteral = literal => CreateLiteralWithCast<byte>(Literal(FromTypicalLiteral<byte>(literal)), SyntaxKind.ByteKeyword),
                GenInRange = (min, max, rng) => CreateLiteralWithCast<byte>(Literal(RandomMinMax<byte>(min, max, rng)), SyntaxKind.ByteKeyword),
                IsUnsigned = true,
                IsNumeric = true,
                Size = sizeof(byte),
            },
            [SyntaxKind.SByteKeyword] = new PrimitiveTypeInfo
            {
                AllowedAdditionalAssignments = intAssigns,
                GenRandomLiteral = rng => CreateLiteralWithCast<sbyte>(Literal((sbyte)rng.NextUInt64()), SyntaxKind.SByteKeyword),
                GenTypicalLiteral = literal => CreateLiteralWithCast<sbyte>(Literal(FromTypicalLiteral<sbyte>(literal)), SyntaxKind.SByteKeyword),
                GenInRange = (min, max, rng) => CreateLiteralWithCast<sbyte>(Literal(RandomMinMax<sbyte>(min, max, rng)), SyntaxKind.SByteKeyword),
                IsNumeric = true,
                Size = sizeof(sbyte),
            },
            [SyntaxKind.FloatKeyword] = new PrimitiveTypeInfo
            {
                AllowedAdditionalAssignments = floatAssigns,
                GenRandomLiteral = rng => CreateLiteralWithSuffix<float>((float)((rng.NextDouble() - 0.5) * 1000), "R", "f", (val, str) => Literal(val, str)),
                GenTypicalLiteral = literal => CreateLiteralWithSuffix<float>(FromTypicalLiteral<float>(literal), "R", "f", (val, str) => Literal(val, str)),
                GenInRange = (min, max, rng) => throw new InvalidOperationException(),
                IsNumeric = true,
                IsFloat = true,
                Size = sizeof(float),
            },
            [SyntaxKind.DoubleKeyword] = new PrimitiveTypeInfo
            {
                AllowedAdditionalAssignments = floatAssigns,
                GenRandomLiteral = rng => CreateLiteralWithSuffix<double>((double)((rng.NextDouble() - 0.5) * 10000), "R", "d", (val, str) => Literal(val, str)),
                GenTypicalLiteral = literal => CreateLiteralWithSuffix<double>(FromTypicalLiteral<double>(literal), "R", "d", (val, str) => Literal(val, str)),
                GenInRange = (min, max, rng) => throw new InvalidOperationException(),
                IsNumeric = true,
                IsFloat = true,
                Size = sizeof(double),
            },
            [SyntaxKind.BoolKeyword] = new PrimitiveTypeInfo
            {
                AllowedAdditionalAssignments = boolAssigns,
                GenRandomLiteral = rng => LiteralExpression(rng.Next(2) == 0 ? SyntaxKind.TrueLiteralExpression : SyntaxKind.FalseLiteralExpression),
                GenTypicalLiteral = literal => throw new InvalidOperationException(),
                GenInRange = (min, max, rng) => throw new InvalidOperationException(),
                Size = sizeof(bool),
            },
        };
    }

    private static ExpressionSyntax CreateLiteralWithSuffix<T>(T value, string format, string suffix, Func<string, T, SyntaxToken> create) where T : IFormattable
    {
        string str = value.ToString(format, CultureInfo.InvariantCulture);
        str += suffix;
        return LiteralExpression(SyntaxKind.NumericLiteralExpression, create(str, value));
    }

    private static ExpressionSyntax CreateLiteralWithCast<T>(SyntaxToken token, SyntaxKind cast)
    {
        ExpressionSyntax literal = LiteralExpression(SyntaxKind.NumericLiteralExpression, token);
        return CastExpression(PredefinedType(Token(cast)), literal);
    }

    private static T FromTypicalLiteral<T>(TypicalLiteralKind literal) where T : INumber<T>, IMinMaxValue<T>
    {
        return literal switch
        {
            TypicalLiteralKind.MinValue => T.MinValue,
            TypicalLiteralKind.MinValuePlusOne => T.MinValue + T.One,
            TypicalLiteralKind.MinusTen => T.CreateChecked(-10),
            TypicalLiteralKind.MinusTwo => T.CreateChecked(-2),
            TypicalLiteralKind.MinusOne => T.CreateChecked(-1),
            TypicalLiteralKind.Zero => T.Zero,
            TypicalLiteralKind.One => T.One,
            TypicalLiteralKind.Two => T.CreateChecked(2),
            TypicalLiteralKind.Ten => T.CreateChecked(10),
            TypicalLiteralKind.MaxValueMinusOne => T.MaxValue - T.One,
            TypicalLiteralKind.MaxValue => T.MaxValue,
            _ => throw new ArgumentOutOfRangeException(nameof(literal)),
        };
    }

    private static T RandomMinMax<T>(int? min, int? max, Rng rng) where T : INumber<T>, IMinMaxValue<T>
    {
        T minValue = T.MinValue;
        if (min.HasValue)
            minValue = T.CreateChecked(min.Value);

        T maxValue = T.MaxValue;
        if (max.HasValue)
            maxValue = T.CreateChecked(max.Value);

        ulong rnd = rng.NextUInt64();
        ulong numValues = ulong.CreateChecked(maxValue - minValue);
        if (numValues == ulong.MaxValue)
        {
            if (typeof(T) == typeof(long))
            {
                return (T)(object)(long)rng.NextUInt64();
            }
            if (typeof(T) == typeof(ulong))
            {
                return (T)(object)rng.NextUInt64();
            }

            throw new InvalidOperationException("Invalid min and max");
        }

        numValues++;
        ulong bits = rng.NextUInt64();
        ulong index = bits % numValues;
        return minValue + T.CreateChecked(index);
    }
}

internal class PrimitiveTypeInfo
{
    public SyntaxKind[] AllowedAdditionalAssignments { get; set; }
    public Func<Rng, ExpressionSyntax> GenRandomLiteral { get; set; }
    public Func<TypicalLiteralKind, ExpressionSyntax> GenTypicalLiteral { get; set; }
    public Func<int?, int?, Rng, ExpressionSyntax> GenInRange { get; set; }
    public bool IsUnsigned { get; set; }
    public bool IsNumeric { get; set; }
    public bool IsFloat { get; set; }
    public int Size { get; set; }
}
