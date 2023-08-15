using Microsoft.CodeAnalysis.CSharp;
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
    private readonly List<AggregateField> _fields;

    public AggregateType(bool isClass, string name, List<AggregateField> fields)
    {
        IsClass = isClass;
        Name = name;
        _fields = fields;
        (Layout, FlattenedLayout) = ComputeLayouts();
    }

    public string Name { get; }
    public IReadOnlyList<AggregateField> Fields => _fields;
    public bool IsClass { get; }
    public HashSet<InterfaceType> ImplementedInterfaces { get; } = new();

    /// <summary> If this is a struct type with a target independent fixed
    /// layout, then this is its layout. Otherwise null.</summary>
    public TypeLayout Layout { get; }
    /// <summary> If this is a struct type with a target independent fixed
    /// layout, then this is its flattened layout. Otherwise null.</summary>
    public FlattenedTypeLayout FlattenedLayout { get; }

    public override SyntaxKind[] AllowedAdditionalAssignmentKinds => Array.Empty<SyntaxKind>();

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

    private (TypeLayout, FlattenedTypeLayout) ComputeLayouts()
    {
        if (IsClass)
        {
            return (null, null);
        }

        int alignment = 0;
        foreach (AggregateField field in _fields)
        {
            if (field.Type is AggregateType at)
            {
                if (at.Layout == null)
                {
                    return (null, null);
                }

                alignment = Math.Max(alignment, at.FlattenedLayout.Alignment);
            }
            else
            {
                Debug.Assert(field.Type is PrimitiveType);
                alignment = Math.Max(alignment, ((PrimitiveType)field.Type).Info.Size);
            }
        }

        Debug.Assert(alignment > 0);
        List<LayoutField> fields = new();
        List<FlattenedLayoutField> flattenedFields = new();
        int offset = 0;
        foreach (AggregateField field in _fields)
        {
            fields.Add(new LayoutField(offset, field));

            if (field.Type is AggregateType at)
            {
                offset = RoundUp(offset, at.FlattenedLayout.Alignment);

                foreach (FlattenedLayoutField fieldLayoutField in at.FlattenedLayout.Fields)
                {
                    flattenedFields.Add(new FlattenedLayoutField(offset + fieldLayoutField.Offset, fieldLayoutField.Type));
                }

                offset += at.FlattenedLayout.Size;
            }
            else
            {
                PrimitiveType pt = (PrimitiveType)field.Type;
                offset = RoundUp(offset, pt.Info.Size);
                flattenedFields.Add(new FlattenedLayoutField(offset, pt));

                offset += pt.Info.Size;
            }
        }

        int size = RoundUp(offset, alignment);
        return (new TypeLayout(alignment, fields.ToArray(), size),
                new FlattenedTypeLayout(alignment, flattenedFields.ToArray(), size));
    }

    private static int RoundUp(int val, int alignment)
    {
        int rem = val % alignment;
        if (rem != 0)
            return val + (alignment - rem);
        return val;
    }

    public bool Implements(InterfaceType it)
        => ImplementedInterfaces.Contains(it);

    public TypeDeclarationSyntax Output(List<MethodDeclarationSyntax> methods)
    {
        TypeDeclarationSyntax ty = IsClass ? ClassDeclaration(Name) : StructDeclaration(Name);

        ty = ty.WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)));
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
            SeparatedList(_fields.Select(f => Parameter(Identifier(f.Name.ToLowerInvariant())).WithType(f.Type.GenReferenceTo()))));

        BlockSyntax initBlock =
            Block(_fields.Select(
                f =>
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName(f.Name),
                        IdentifierName(f.Name.ToLowerInvariant())))));

        return
            ConstructorDeclaration(Name)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(pms)
            .WithBody(initBlock);
    }
}
