using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;

namespace Fuzzlyn.Methods
{
    internal static class BinOpTable
    {
        public static SyntaxKind? GetImplicitlyConvertedToType(SyntaxKind left, SyntaxKind right)
        {
            SyntaxKind type = s_table[GetTypeIndex(left), GetTypeIndex(right)];
            if (type == SyntaxKind.None)
                return null;
            return type;
        }

        private static int GetTypeIndex(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.SByteKeyword:
                    return 0;
                case SyntaxKind.ShortKeyword:
                    return 1;
                case SyntaxKind.IntKeyword:
                    return 2;
                case SyntaxKind.LongKeyword:
                    return 3;
                case SyntaxKind.ByteKeyword:
                    return 4;
                case SyntaxKind.UShortKeyword:
                    return 5;
                case SyntaxKind.UIntKeyword:
                    return 6;
                case SyntaxKind.ULongKeyword:
                    return 7;
                case SyntaxKind.BoolKeyword:
                    return 8;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind));
            }
        }

        private const SyntaxKind ERR = SyntaxKind.None;
        private const SyntaxKind I32 = SyntaxKind.IntKeyword;
        private const SyntaxKind I64 = SyntaxKind.LongKeyword;
        private const SyntaxKind U32 = SyntaxKind.UIntKeyword;
        private const SyntaxKind U64 = SyntaxKind.ULongKeyword;
        private const SyntaxKind BOL = SyntaxKind.BoolKeyword;

        private static SyntaxKind[,] s_table =
        {
            // Generated with Fuzzlyn.TableGen
//        i08  i16  i32  i64  u08  u16  u32  u64  bol
/*i08*/ { I32, I32, I32, I64, I32, I32, I64, ERR, ERR },
/*i16*/ { I32, I32, I32, I64, I32, I32, I64, ERR, ERR },
/*i32*/ { I32, I32, I32, I64, I32, I32, I64, ERR, ERR },
/*i64*/ { I64, I64, I64, I64, I64, I64, I64, ERR, ERR },
/*u08*/ { I32, I32, I32, I64, I32, I32, U32, U64, ERR },
/*u16*/ { I32, I32, I32, I64, I32, I32, U32, U64, ERR },
/*u32*/ { I64, I64, I64, I64, U32, U32, U32, U64, ERR },
/*u64*/ { ERR, ERR, ERR, ERR, U64, U64, U64, U64, ERR },
/*bol*/ { ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, BOL },
        };

    }
}
