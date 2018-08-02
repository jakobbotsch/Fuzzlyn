using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Types
{
    public class PrimitiveType : FuzzType, IEquatable<PrimitiveType>
    {
        public PrimitiveType(SyntaxKind keyword)
        {
            Keyword = keyword;
        }

        public SyntaxKind Keyword { get; }
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
            {
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
            };

            SyntaxKind[] boolAssigns =
            {
                SyntaxKind.AndAssignmentExpression,
                SyntaxKind.ExclusiveOrAssignmentExpression,
                SyntaxKind.OrAssignmentExpression,
            };

            s_infoTable = new Dictionary<SyntaxKind, PrimitiveTypeInfo>
            {
                [SyntaxKind.ULongKeyword] = new PrimitiveTypeInfo
                {
                    AllowedAdditionalAssignments = intAssigns,
                    GenRandomLiteral = rng => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(rng.NextUInt64())),
                    IsUnsigned = true,
                    Type = typeof(ulong),
                    IsIntegral = true,
                },
                [SyntaxKind.LongKeyword] = new PrimitiveTypeInfo
                {
                    AllowedAdditionalAssignments = intAssigns,
                    GenRandomLiteral = rng => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((long)rng.NextUInt64())),
                    Type = typeof(long),
                    IsIntegral = true,
                },
                [SyntaxKind.UIntKeyword] = new PrimitiveTypeInfo
                {
                    AllowedAdditionalAssignments = intAssigns,
                    GenRandomLiteral = rng => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((uint)rng.NextUInt64())),
                    IsUnsigned = true,
                    Type = typeof(uint),
                    IsIntegral = true,
                },
                [SyntaxKind.IntKeyword] = new PrimitiveTypeInfo
                {
                    AllowedAdditionalAssignments = intAssigns,
                    GenRandomLiteral = rng => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((int)rng.NextUInt64())),
                    Type = typeof(int),
                    IsIntegral = true,
                },
                [SyntaxKind.UShortKeyword] = new PrimitiveTypeInfo
                {
                    AllowedAdditionalAssignments = intAssigns,
                    GenRandomLiteral = rng => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((ushort)rng.NextUInt64())),
                    IsUnsigned = true,
                    Type = typeof(ushort),
                    IsIntegral = true,
                },
                [SyntaxKind.ShortKeyword] = new PrimitiveTypeInfo
                {
                    AllowedAdditionalAssignments = intAssigns,
                    GenRandomLiteral = rng => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((short)rng.NextUInt64())),
                    Type = typeof(short),
                    IsIntegral = true,
                },
                [SyntaxKind.ByteKeyword] = new PrimitiveTypeInfo
                {
                    AllowedAdditionalAssignments = intAssigns,
                    GenRandomLiteral = rng => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((byte)rng.NextUInt64())),
                    IsUnsigned = true,
                    Type = typeof(byte),
                    IsIntegral = true,
                },
                [SyntaxKind.SByteKeyword] = new PrimitiveTypeInfo
                {
                    AllowedAdditionalAssignments = intAssigns,
                    GenRandomLiteral = rng => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal((sbyte)rng.NextUInt64())),
                    Type = typeof(sbyte),
                    IsIntegral = true,
                },
                [SyntaxKind.BoolKeyword] = new PrimitiveTypeInfo
                {
                    AllowedAdditionalAssignments = boolAssigns,
                    GenRandomLiteral = rng => LiteralExpression(rng.Next(2) == 0 ? SyntaxKind.TrueLiteralExpression : SyntaxKind.FalseLiteralExpression),
                    Type = typeof(bool),
                },
            };
        }
    }

    internal class PrimitiveTypeInfo
    {
        public SyntaxKind[] AllowedAdditionalAssignments { get; set; }
        public Func<Rng, LiteralExpressionSyntax> GenRandomLiteral { get; set; }
        public bool IsUnsigned { get; set; }
        public Type Type { get; set; }
        public bool IsIntegral { get; set; }
    }
}
