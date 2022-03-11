using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Fuzzlyn.UnitTests;

public class ArrayTypes
{
    private readonly PrimitiveType _int = new(SyntaxKind.IntKeyword);

    [Fact]
    public void TestArrayOfArray()
    {
        ArrayType arr = _int.MakeArrayType().MakeArrayType().MakeArrayType(5);
        Assert.Equal("int[,,,,][][]", arr.GenReferenceTo().ToFullString());
    }

    [Fact]
    public void TestArrayOfMultidimensionalArray()
    {
        ArrayType arr = _int.MakeArrayType(2).MakeArrayType();
        Assert.Equal("int[][,]", arr.GenReferenceTo().ToFullString());
    }

    [Fact]
    public void TestArrayTypeEquality()
    {
        Assert.True(_int.MakeArrayType() == _int.MakeArrayType());
    }
}
