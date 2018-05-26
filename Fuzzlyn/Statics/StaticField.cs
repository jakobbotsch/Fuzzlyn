using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Statics
{
    internal class StaticField
    {
        public StaticField(VariableIdentifier var, ExpressionSyntax initializer)
        {
            Var = var;
            Initializer = initializer;
        }

        public VariableIdentifier Var { get; }
        public ExpressionSyntax Initializer { get; }

        public FieldDeclarationSyntax Output()
            => FieldDeclaration(
                VariableDeclaration(
                    Var.Type.GenReferenceTo(),
                    SingletonSeparatedList(
                        VariableDeclarator(
                            Identifier(Var.Name))
                        /*.WithInitializer(
                            EqualsValueClause(Initializer))*/)))
               .WithModifiers(TokenList(Token(SyntaxKind.StaticKeyword)));
    }
}
