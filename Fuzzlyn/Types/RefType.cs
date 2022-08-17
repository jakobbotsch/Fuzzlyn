using System;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Types;

internal class RefType : FuzzType
{
    public RefType(FuzzType innerType)
    {
        Debug.Assert(!(innerType is RefType), "Cannot create ref to ref type");
        InnerType = innerType;
    }

    public FuzzType InnerType { get; }

    public override SyntaxKind[] AllowedAdditionalAssignmentKinds
        => InnerType.AllowedAdditionalAssignmentKinds;

    public override bool IsByRefLike => true;

    public override TypeSyntax GenReferenceTo()
        => RefType(InnerType.GenReferenceTo());

    public override ParameterSyntax GenParameter(string identifier)
        => InnerType.GenParameter(identifier).WithModifiers(TokenList(Token(SyntaxKind.RefKeyword)));

    public bool Equals(RefType other)
        => other != null && InnerType == other.InnerType;

    public override bool Equals(object obj)
        => Equals(obj as RefType);

    public override int GetHashCode()
        => HashCode.Combine(3, InnerType);
}
