using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Statics;

internal class StaticField(FuzzType type, string name, ExpressionSyntax initializer)
{
    public FuzzType Type { get; } = type;
    public string Name { get; } = name;
    public ExpressionSyntax Initializer { get; } = initializer;

    public ExpressionSyntax CreateAccessor(bool prefixWithClass)
    {
        ExpressionSyntax accessor;
        if (prefixWithClass)
            accessor = MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, IdentifierName(CodeGenerator.ClassNameForStaticMethods), IdentifierName(Name));
        else
            accessor = IdentifierName(Name);

        return accessor;
    }

    public FieldDeclarationSyntax Output()
        => FieldDeclaration(
            VariableDeclaration(
                Type.GenReferenceTo(),
                SingletonSeparatedList(
                    VariableDeclarator(
                        Identifier(Name))
                    .WithInitializer(
                        EqualsValueClause(Initializer)))))
           .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword)));
}
