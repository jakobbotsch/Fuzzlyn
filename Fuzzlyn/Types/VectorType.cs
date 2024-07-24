using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Types;
public enum VectorTypeWidth
{
    Width64,
    Width128,
    Width256,
    Width512,
}

public class VectorType(VectorTypeWidth width, PrimitiveType elementType) : FuzzType
{
    public VectorTypeWidth Width { get; } = width;
    public PrimitiveType ElementType { get; } = elementType;

    public override SyntaxKind[] AllowedAdditionalAssignmentKinds { get; } = [];

    private TypeSyntax _type;
    public override TypeSyntax GenReferenceTo()
    {
        if (_type != null)
            return _type;

        _type =
            GenericName(GetBaseName())
            .WithTypeArgumentList(
                TypeArgumentList(
                    SingletonSeparatedList(
                        ElementType.GenReferenceTo())));

        return _type;
    }

    public int NumElements()
        => GetWidthInBytes() / ElementType.Info.Size;

    public int GetWidthInBytes()
        => Width switch
        {
            VectorTypeWidth.Width64 => 8,
            VectorTypeWidth.Width256 => 32,
            VectorTypeWidth.Width512 => 64,
            _ => 16,
        };

    public string GetBaseName()
        => Width switch
        {
            VectorTypeWidth.Width64 => "Vector64",
            VectorTypeWidth.Width256 => "Vector256",
            VectorTypeWidth.Width512 => "Vector512",
            _ => "Vector128",
        };

    public override int GetHashCode()
        => HashCode.Combine(4, Width, ElementType.GetHashCode());

    public bool Equals(VectorType other)
    {
        return other != null && Width == other.Width && ElementType == other.ElementType;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as VectorType);
    }
}
