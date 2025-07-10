using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Statics;

internal class AsyncLocalField(FuzzType innerType, string name, string getterName, ExpressionSyntax initializer)
{
    public FuzzType InnerType { get; } = innerType;
    public string Name { get; } = name;
    public string GetterName { get; } = getterName;
    public ExpressionSyntax Initializer { get; } = initializer;

    public ExpressionSyntax CreateGetAccess(bool prefixWithClass)
    {
        ExpressionSyntax accessor;

        if (prefixWithClass)
        {
            accessor =
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName(CodeGenerator.ClassNameForStaticMethods),
                    IdentifierName(GetterName));
        }
        else
        {
            accessor = IdentifierName(GetterName);
        }

        return InvocationExpression(accessor).WithArgumentList(ArgumentList());
    }

    public ExpressionSyntax CreateLhsAccess(bool prefixWithClass)
    {
        ExpressionSyntax accessor;

        if (prefixWithClass)
        {
            accessor =
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName(CodeGenerator.ClassNameForStaticMethods),
                    IdentifierName(Name));
        }
        else
        {
            accessor = IdentifierName(Name);
        }

        return
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                accessor,
                IdentifierName("Value"));
    }

    public (FieldDeclarationSyntax field, MethodDeclarationSyntax getter) Output()
    {
        TypeSyntax innerType = InnerType.GenReferenceTo();
        TypeSyntax innerTypeStored = innerType;
        if (!InnerType.IsReferenceType)
        {
            innerTypeStored = NullableType(innerType);
        }

        TypeSyntax asyncLocalType =
            GenericName("AsyncLocal")
            .WithTypeArgumentList(
                TypeArgumentList(SingletonSeparatedList(innerTypeStored)));

        // Create initializer for the field - just a new AsyncLocal without setting Value
        EqualsValueClauseSyntax fieldInitializer = EqualsValueClause(
            ObjectCreationExpression(asyncLocalType)
                .WithArgumentList(ArgumentList()));

        FieldDeclarationSyntax field =
            FieldDeclaration(
                VariableDeclaration(
                    asyncLocalType,
                    SingletonSeparatedList(
                        VariableDeclarator(
                            Identifier(Name)).WithInitializer(fieldInitializer))))
               .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword)));

        BlockSyntax getterBody = Block(
            ReturnStatement(
                AssignmentExpression(
                    SyntaxKind.CoalesceAssignmentExpression,
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName(Name),
                        IdentifierName("Value")),
                    Initializer)));

        MethodDeclarationSyntax getter =
            MethodDeclaration(innerType, GetterName)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword)))
            .WithBody(getterBody);

        return (field, getter);
    }
}
