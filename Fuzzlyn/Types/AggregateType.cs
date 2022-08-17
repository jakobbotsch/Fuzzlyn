﻿using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Types;

// Class or struct type.
public class AggregateType : FuzzType
{
    private readonly List<AggregateField> _fields = new();

    public AggregateType(bool isClass, bool isRefStruct, string name, List<AggregateField> fields)
    {
        IsClass = isClass;
        Name = name;
        _fields = fields;
        IsByRefLike = isRefStruct;
    }

    public string Name { get; }
    public IReadOnlyList<AggregateField> Fields => _fields;
    public bool IsClass { get; }
    public HashSet<InterfaceType> ImplementedInterfaces { get; } = new();

    public override SyntaxKind[] AllowedAdditionalAssignmentKinds => Array.Empty<SyntaxKind>();

    public override bool IsByRefLike { get; }

    public override TypeSyntax GenReferenceTo()
        => IdentifierName(Name);

    public bool Equals(AggregateType other)
        => ReferenceEquals(this, other);

    public override bool Equals(object obj)
        => Equals(obj as AggregateType);

    public override int GetHashCode()
#pragma warning disable RS1024 // Compare symbols correctly
            => RuntimeHelpers.GetHashCode(this);
#pragma warning restore RS1024 // Compare symbols correctly

    /// <summary>
    /// Count recursively how many primitive fields are in this aggregate type.
    /// </summary>
    public int GetTotalNumPrimitiveFields()
    {
        int count = 0;
        foreach (AggregateField field in _fields)
        {
            if (field.Type is AggregateType at)
            {
                count += at.GetTotalNumPrimitiveFields();
            }
            else
            {
                Debug.Assert(field.Type is PrimitiveType);
                count++;
            }
        }

        return count;
    }

    public bool Implements(InterfaceType it)
        => ImplementedInterfaces.Contains(it);

    public TypeDeclarationSyntax Output(List<MethodDeclarationSyntax> methods)
    {
        TypeDeclarationSyntax ty = IsClass ? ClassDeclaration(Name) : StructDeclaration(Name);

        ty = ty.WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)));
        if (IsByRefLike)
            ty = ty.AddModifiers(Token(SyntaxKind.RefKeyword));

        if (ImplementedInterfaces.Count > 0)
        {
            ty = ty.WithBaseList(
                BaseList(
                    SeparatedList(
                        ImplementedInterfaces.Select(it => (BaseTypeSyntax)SimpleBaseType(it.GenReferenceTo())))));
        }

        ty = ty.WithMembers(OutputMembers(methods).ToSyntaxList());

        return ty;
    }

    private IEnumerable<MemberDeclarationSyntax> OutputMembers(List<MethodDeclarationSyntax> methods)
    {
        foreach (AggregateField field in _fields)
        {
            yield return
                FieldDeclaration(
                    VariableDeclaration(
                        type: field.Type.GenReferenceTo(),
                        variables: SingletonSeparatedList(VariableDeclarator(field.Name))))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)));
        }

        yield return OutputCtor();

        foreach (MethodDeclarationSyntax method in methods)
            yield return method;
    }

    private ConstructorDeclarationSyntax OutputCtor()
    {
        ParameterListSyntax pms = ParameterList(
            SeparatedList(_fields.Select(f => f.Type.GenParameter(f.Name.ToLowerInvariant()))));

        static StatementSyntax AssignField(AggregateField f)
        {
            ExpressionSyntax rhs = IdentifierName(f.Name.ToLowerInvariant());
            if (f.Type is RefType)
                rhs = RefExpression(rhs);

            return
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName(f.Name),
                        rhs));
        }

        BlockSyntax initBlock =
            Block(_fields.Select(AssignField));

        return
            ConstructorDeclaration(Name)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(pms)
            .WithBody(initBlock);
    }
}
