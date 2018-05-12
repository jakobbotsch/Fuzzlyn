using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn
{
    partial class RandomSyntaxGenerator
    {
        public AttributeListSyntax GenAttributeList(List<AttributeSyntax> attributes)
            => AttributeList(SeparatedList(attributes));

        public AttributeListSyntax GenAttributeList(AttributeTargetSpecifierSyntax target, List<AttributeSyntax> attributes)
            => AttributeList(target, SeparatedList(attributes));

        public AttributeSyntax GenAttribute(NameSyntax name)
            => Attribute(name);

        public AttributeSyntax GenAttribute(NameSyntax name, AttributeArgumentListSyntax args)
            => Attribute(name, args);

        public AttributeArgumentListSyntax GenAttributeArgumentList(List<AttributeArgumentSyntax> args)
            => AttributeArgumentList(SeparatedList(args));

        public AttributeArgumentSyntax GenAttributeArgument(
            [RequireContext(Context.AttributeArgument)]
            ExpressionSyntax expression)
            => AttributeArgument(expression);

        public AttributeArgumentSyntax GenAttributeArgument(
            NameEqualsSyntax nameEquals,
            [RequireContext(Context.AttributeArgument)]
            ExpressionSyntax expression)
            => AttributeArgument(expression).WithNameEquals(nameEquals);

        public AttributeArgumentSyntax GenAttributeArgument(
            NameColonSyntax nameColon,
            [RequireContext(Context.AttributeArgument)]
            ExpressionSyntax expression)
            => AttributeArgument(expression).WithNameColon(nameColon);

        public AttributeTargetSpecifierSyntax GenAttributeTargetSpecifier(string target)
            => AttributeTargetSpecifier(Identifier(target));

        [AllowIn(Context.Global)]
        public AttributeListSyntax GenAssemblyTargetedAttributeList(List<AttributeSyntax> attributes)
            => AttributeList(AttributeTargetSpecifier(Token(SyntaxKind.AssemblyKeyword)), SeparatedList(attributes));

        [AllowIn(Context.AttributeArgument)]
        public ImplicitArrayCreationExpressionSyntax GenImplicitArrayCreationExpressionForAttribute(
            [RequireContext(Context.AttributeArgument)] InitializerExpressionSyntax initializer)
            => ImplicitArrayCreationExpression(initializer);

        [AllowIn(Context.AttributeArgument)]
        public InitializerExpressionSyntax GenInitializerExpressionForAttribute(
            [RequireContext(Context.AttributeArgument)]
            List<ExpressionSyntax> expressions)
            => InitializerExpression(RandomKind(s_initializerKinds), SeparatedList(expressions));

        [AllowIn(Context.AttributeArgument)]
        public ArrayCreationExpressionSyntax GenArrayCreationExpressionForAttribute(
            ArrayTypeSyntax type)
            => ArrayCreationExpression(type);

        [AllowIn(Context.AttributeArgument)]
        public ArrayCreationExpressionSyntax GenArrayCreationExpressionForAttribute(
            ArrayTypeSyntax type,
            [RequireContext(Context.AttributeArgument)]
            InitializerExpressionSyntax initializer)
            => ArrayCreationExpression(type, initializer);
    }
}
