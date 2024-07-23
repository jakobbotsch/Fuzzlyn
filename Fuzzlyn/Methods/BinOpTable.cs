using Microsoft.CodeAnalysis.CSharp;
using System;

namespace Fuzzlyn.Methods;

internal class BinOpTable
{
    private readonly SyntaxKind[,] _table;
    private BinOpTable(SyntaxKind[,] table)
    {
        _table = table;
    }

    public SyntaxKind? GetResultType(SyntaxKind left, SyntaxKind right)
    {
        SyntaxKind type = _table[GetTypeIndex(left), GetTypeIndex(right)];
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
            case SyntaxKind.FloatKeyword:
                return 8;
            case SyntaxKind.DoubleKeyword:
                return 9;
            case SyntaxKind.BoolKeyword:
                return 10;
            default:
                throw new ArgumentOutOfRangeException(nameof(kind));
        }
    }

    private const SyntaxKind ERR = SyntaxKind.None;
    private const SyntaxKind I32 = SyntaxKind.IntKeyword;
    private const SyntaxKind I64 = SyntaxKind.LongKeyword;
    private const SyntaxKind U32 = SyntaxKind.UIntKeyword;
    private const SyntaxKind U64 = SyntaxKind.ULongKeyword;
    private const SyntaxKind FLT = SyntaxKind.FloatKeyword;
    private const SyntaxKind DBL = SyntaxKind.DoubleKeyword;
    private const SyntaxKind BOL = SyntaxKind.BoolKeyword;

    // Generated with Fuzzlyn.TableGen

    private static readonly SyntaxKind[,] s_arithmeticTable =
    {
//        i08  i16  i32  i64  u08  u16  u32  u64  flt  dbl  bol
/*i08*/ { I32, I32, I32, I64, I32, I32, I64, ERR, FLT, DBL, ERR },
/*i16*/ { I32, I32, I32, I64, I32, I32, I64, ERR, FLT, DBL, ERR },
/*i32*/ { I32, I32, I32, I64, I32, I32, I64, ERR, FLT, DBL, ERR },
/*i64*/ { I64, I64, I64, I64, I64, I64, I64, ERR, FLT, DBL, ERR },
/*u08*/ { I32, I32, I32, I64, I32, I32, U32, U64, FLT, DBL, ERR },
/*u16*/ { I32, I32, I32, I64, I32, I32, U32, U64, FLT, DBL, ERR },
/*u32*/ { I64, I64, I64, I64, U32, U32, U32, U64, FLT, DBL, ERR },
/*u64*/ { ERR, ERR, ERR, ERR, U64, U64, U64, U64, FLT, DBL, ERR },
/*flt*/ { FLT, FLT, FLT, FLT, FLT, FLT, FLT, FLT, FLT, DBL, ERR },
/*dbl*/ { DBL, DBL, DBL, DBL, DBL, DBL, DBL, DBL, DBL, DBL, ERR },
/*bol*/ { ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR },
    };

    private static readonly SyntaxKind[,] s_bitwiseOpTable =
    {
//        i08  i16  i32  i64  u08  u16  u32  u64  flt  dbl  bol
/*i08*/ { I32, I32, I32, I64, I32, I32, I64, ERR, ERR, ERR, ERR },
/*i16*/ { I32, I32, I32, I64, I32, I32, I64, ERR, ERR, ERR, ERR },
/*i32*/ { I32, I32, I32, I64, I32, I32, I64, ERR, ERR, ERR, ERR },
/*i64*/ { I64, I64, I64, I64, I64, I64, I64, ERR, ERR, ERR, ERR },
/*u08*/ { I32, I32, I32, I64, I32, I32, U32, U64, ERR, ERR, ERR },
/*u16*/ { I32, I32, I32, I64, I32, I32, U32, U64, ERR, ERR, ERR },
/*u32*/ { I64, I64, I64, I64, U32, U32, U32, U64, ERR, ERR, ERR },
/*u64*/ { ERR, ERR, ERR, ERR, U64, U64, U64, U64, ERR, ERR, ERR },
/*flt*/ { ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR },
/*dbl*/ { ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR },
/*bol*/ { ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, BOL },
    };

    private static readonly SyntaxKind[,] s_shiftTable =
    {
//        i08  i16  i32  i64  u08  u16  u32  u64  flt  dbl  bol
/*i08*/ { I32, I32, I32, ERR, I32, I32, ERR, ERR, ERR, ERR, ERR },
/*i16*/ { I32, I32, I32, ERR, I32, I32, ERR, ERR, ERR, ERR, ERR },
/*i32*/ { I32, I32, I32, ERR, I32, I32, ERR, ERR, ERR, ERR, ERR },
/*i64*/ { I64, I64, I64, ERR, I64, I64, ERR, ERR, ERR, ERR, ERR },
/*u08*/ { I32, I32, I32, ERR, I32, I32, ERR, ERR, ERR, ERR, ERR },
/*u16*/ { I32, I32, I32, ERR, I32, I32, ERR, ERR, ERR, ERR, ERR },
/*u32*/ { U32, U32, U32, ERR, U32, U32, ERR, ERR, ERR, ERR, ERR },
/*u64*/ { U64, U64, U64, ERR, U64, U64, ERR, ERR, ERR, ERR, ERR },
/*flt*/ { ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR },
/*dbl*/ { ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR },
/*bol*/ { ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR },
        };

    private static readonly SyntaxKind[,] s_equalityTable =
    {
//        i08  i16  i32  i64  u08  u16  u32  u64  flt  dbl  bol
/*i08*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR, BOL, BOL, ERR },
/*i16*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR, BOL, BOL, ERR },
/*i32*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR, BOL, BOL, ERR },
/*i64*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR, BOL, BOL, ERR },
/*u08*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR },
/*u16*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR },
/*u32*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR },
/*u64*/ { ERR, ERR, ERR, ERR, BOL, BOL, BOL, BOL, BOL, BOL, ERR },
/*flt*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR },
/*dbl*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR },
/*bol*/ { ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, BOL },
        };

    private static readonly SyntaxKind[,] s_relopTable =
    {
//        i08  i16  i32  i64  u08  u16  u32  u64  flt  dbl  bol
/*i08*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR, BOL, BOL, ERR },
/*i16*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR, BOL, BOL, ERR },
/*i32*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR, BOL, BOL, ERR },
/*i64*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR, BOL, BOL, ERR },
/*u08*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR },
/*u16*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR },
/*u32*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR },
/*u64*/ { ERR, ERR, ERR, ERR, BOL, BOL, BOL, BOL, BOL, BOL, ERR },
/*flt*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR },
/*dbl*/ { BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, BOL, ERR },
/*bol*/ { ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR, ERR },
        };

    public static BinOpTable Arithmetic { get; } = new BinOpTable(s_arithmeticTable);
    public static BinOpTable BitwiseOps { get; } = new BinOpTable(s_bitwiseOpTable);
    public static BinOpTable Shifts { get; } = new BinOpTable(s_shiftTable);
    public static BinOpTable Equality { get; } = new BinOpTable(s_equalityTable);
    public static BinOpTable Relop { get; } = new BinOpTable(s_relopTable);
}
