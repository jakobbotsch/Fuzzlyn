using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Types;

// Represents an interface type.
// Note that functions will be associated with interfaces during function generation inside of FuncGenerator.
// We don't generate the methods that will be on interface types beforehand.
// However, we do figure out which interfaces a specified aggregate type implements beforehand.
// This is necessary to be able to figure out which types are assignable-to.
public class InterfaceType(string name) : FuzzType
{
    public string Name { get; } = name;

    public override SyntaxKind[] AllowedAdditionalAssignmentKinds => Array.Empty<SyntaxKind>();

    public override bool IsReferenceType => true;

    public override TypeSyntax GenReferenceTo()
        => IdentifierName(Name);

    public bool Equals(InterfaceType other)
        => ReferenceEquals(this, other);

    public override bool Equals(object obj)
        => Equals(obj as InterfaceType);

    public override int GetHashCode()
#pragma warning disable RS1024 // Compare symbols correctly
            => RuntimeHelpers.GetHashCode(this);
#pragma warning restore RS1024 // Compare symbols correctly

    public TypeDeclarationSyntax Output(List<MethodDeclarationSyntax> methods)
    {
        TypeDeclarationSyntax type = InterfaceDeclaration(Name);
        type = type.WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)));
        type = type.WithMembers(methods.Cast<MemberDeclarationSyntax>().ToSyntaxList());
        return type;
    }
}
