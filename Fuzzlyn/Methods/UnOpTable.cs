using Microsoft.CodeAnalysis.CSharp;
using System;

namespace Fuzzlyn.Methods;

internal class UnOpTable
{
    private readonly SyntaxKind[] _table;
    private UnOpTable(SyntaxKind[] table)
    {
        _table = table;
    }

    public SyntaxKind? GetResultType(SyntaxKind kind)
    {
        SyntaxKind type = _table[GetTypeIndex(kind)];
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

    private static readonly SyntaxKind[] s_unaryPlus =
    //i08  i16  i32  i64  u08  u16  u32  u64  bol
    [I32, I32, I32, I64, I32, I32, U32, U64, ERR];

    private static readonly SyntaxKind[] s_unaryMinus =
    //i08  i16  i32  i64  u08  u16  u32  u64  bol
    [I32, I32, I32, I64, I32, I32, I64, ERR, ERR];

    private static readonly SyntaxKind[] s_bitwiseNot =
    //i08  i16  i32  i64  u08  u16  u32  u64  bol
    [I32, I32, I32, I64, I32, I32, U32, U64, ERR];

    public static UnOpTable UnaryPlus { get; } = new UnOpTable(s_unaryPlus);
    public static UnOpTable UnaryMinus { get; } = new UnOpTable(s_unaryMinus);
    public static UnOpTable BitwiseNot { get; } = new UnOpTable(s_bitwiseNot);
}
