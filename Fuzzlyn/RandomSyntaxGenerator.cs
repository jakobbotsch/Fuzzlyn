using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn
{
    internal class RandomSyntaxGenerator
    {
        private Random _rand;

        public RandomSyntaxGenerator(Random rand)
        {
            _rand = rand;
        }

        public CompilationUnitSyntax GenCompilationUnit(
            List<ExternAliasDirectiveSyntax> externs,
            List<UsingDirectiveSyntax> usings,
            List<AttributeListSyntax> attributeLists,
            List<MemberDeclarationSyntax> members)
            => CompilationUnit(externs.ToSyntaxList(), usings.ToSyntaxList(), attributeLists.ToSyntaxList(), members.ToSyntaxList());

        public ExternAliasDirectiveSyntax GenExternAlias(string identifier)
            => ExternAliasDirective(identifier);

        public UsingDirectiveSyntax GenStaticUsing(NameSyntax ns)
            => UsingDirective(ns).WithStaticKeyword(Token(SyntaxKind.StaticKeyword));

        public UsingDirectiveSyntax GenAliasUsing(NameEqualsSyntax alias, NameSyntax name)
            => UsingDirective(alias, name);

        public UsingDirectiveSyntax GenUsing(NameSyntax name)
            => UsingDirective(name);

        // NameSyntax
        public IdentifierNameSyntax GenIdentifierName(string name)
            => IdentifierName(name);

        public GenericNameSyntax GenGenericName(string identifier)
            => GenericName(identifier);

        [Recursive]
        public GenericNameSyntax GenGenericName(string identifier, TypeArgumentListSyntax types)
            => GenericName(Identifier(identifier), types);

        [Recursive]
        public QualifiedNameSyntax GenQualifiedName(NameSyntax left, SimpleNameSyntax right)
            => QualifiedName(left, right);

        public AliasQualifiedNameSyntax GenAliasQualifiedName(IdentifierNameSyntax alias, SimpleNameSyntax name)
            => AliasQualifiedName(alias, name);
        //

        public NameEqualsSyntax GenNameEquals(IdentifierNameSyntax name)
            => NameEquals(name);

        public NameColonSyntax GenNameColon(IdentifierNameSyntax name)
            => NameColon(name);

        public TypeArgumentListSyntax GenTypeArgumentList(List<TypeSyntax> types)
            => TypeArgumentList(SeparatedList(types));

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

        public AttributeArgumentSyntax GenAttributeArgument(ExpressionSyntax expression)
            => AttributeArgument(expression);

        public AttributeArgumentSyntax GenAttributeArgument(NameEqualsSyntax nameEquals, ExpressionSyntax expression)
            => AttributeArgument(expression).WithNameEquals(nameEquals);

        public AttributeArgumentSyntax GenAttributeArgument(NameColonSyntax nameColon, ExpressionSyntax expression)
            => AttributeArgument(expression).WithNameColon(nameColon);

        public AttributeTargetSpecifierSyntax GenAttributeTargetSpecifier(string target)
            => AttributeTargetSpecifier(Identifier(target));

        [Recursive]
        public NamespaceDeclarationSyntax GenNamespaceDeclaration(
            NameSyntax name,
            List<ExternAliasDirectiveSyntax> externs,
            List<UsingDirectiveSyntax> usings,
            List<MemberDeclarationSyntax> members)
            => NamespaceDeclaration(name, externs.ToSyntaxList(), usings.ToSyntaxList(), members.ToSyntaxList());

        [Recursive]
        public ClassDeclarationSyntax GenClassDeclaration(
            List<AttributeListSyntax> attributes,
            string name,
            TypeParameterListSyntax typeParams,
            BaseListSyntax bases,
            List<TypeParameterConstraintClauseSyntax> constraints,
            List<MemberDeclarationSyntax> members)
        {
            return ClassDeclaration(
                attributes.ToSyntaxList(),
                GenModifiers(),
                Identifier(name),
                typeParams,
                bases,
                constraints.ToSyntaxList(),
                members.ToSyntaxList());
        }

        public TypeParameterListSyntax GenTypeParameterList(List<TypeParameterSyntax> parameters)
            => TypeParameterList(SeparatedList(parameters));

        public TypeParameterSyntax GenTypeParameter(string identifier)
            => TypeParameter(identifier);

        public TypeParameterSyntax GenTypeParameter(List<AttributeListSyntax> attributes, string identifier)
            => TypeParameter(attributes.ToSyntaxList(), GenVarianceKeyword(), Identifier(identifier));

        public BaseListSyntax GenBaseList(List<BaseTypeSyntax> baseTypes)
            => BaseList(SeparatedList(baseTypes));

        public SimpleBaseTypeSyntax GenBaseType(TypeSyntax type)
            => SimpleBaseType(type);

        public TypeParameterConstraintClauseSyntax GenTypeParameterConstraintClause(IdentifierNameSyntax name)
            => TypeParameterConstraintClause(name);

        public TypeParameterConstraintClauseSyntax GenTypeParameterConstraintClause(IdentifierNameSyntax name, List<TypeParameterConstraintSyntax> typeParamConstraints)
            => TypeParameterConstraintClause(name, SeparatedList(typeParamConstraints));

        public ClassOrStructConstraintSyntax GenClassConstraint() => ClassOrStructConstraint(SyntaxKind.ClassConstraint);
        public ClassOrStructConstraintSyntax GenStructConstraint() => ClassOrStructConstraint(SyntaxKind.StructConstraint);
        public ConstructorConstraintSyntax GenConstructorConstraint() => ConstructorConstraint();
        public TypeConstraintSyntax GenTypeConstraint(TypeSyntax type) => TypeConstraint(type);

        [Recursive]
        public StructDeclarationSyntax GenStructDeclaration(
            List<AttributeListSyntax> attributes,
            string name,
            TypeParameterListSyntax typeParams,
            BaseListSyntax bases,
            List<TypeParameterConstraintClauseSyntax> constraints,
            List<MemberDeclarationSyntax> members)
        {
            return StructDeclaration(attributes.ToSyntaxList(),
                GenModifiers(),
                Identifier(name),
                typeParams,
                bases,
                constraints.ToSyntaxList(),
                members.ToSyntaxList());
        }

        public EventFieldDeclarationSyntax GenEventFieldDeclaration(
            List<AttributeListSyntax> attributes,
            VariableDeclarationSyntax declaration)
            => EventFieldDeclaration(attributes.ToSyntaxList(), GenModifiers(), declaration);

        public VariableDeclarationSyntax GenVariableDeclaration(TypeSyntax type, List<VariableDeclaratorSyntax> declarator)
            => VariableDeclaration(type, SeparatedList(declarator));

        public VariableDeclaratorSyntax GenVariableDeclarator(string identifier)
            => VariableDeclarator(identifier);

        public VariableDeclaratorSyntax GenVariableDeclarator(string identifier, EqualsValueClauseSyntax initializer)
            => VariableDeclarator(Identifier(identifier)).WithInitializer(initializer);

        public VariableDeclaratorSyntax GenVariableDeclarator(string identifier, BracketedArgumentListSyntax args, EqualsValueClauseSyntax initializer)
            => VariableDeclarator(Identifier(identifier), args, initializer);

        public BracketedArgumentListSyntax GenBracketedArgumentList(List<ArgumentSyntax> args)
            => BracketedArgumentList(SeparatedList(args));

        public EqualsValueClauseSyntax GenEqualsValueClause(ExpressionSyntax expr)
            => EqualsValueClause(expr);

        public ArgumentSyntax GenArgument(ExpressionSyntax expr)
            => Argument(expr);

        public ArgumentSyntax GenArgument(ExpressionSyntax expr, NameColonSyntax nameColon)
            => Argument(nameColon, GenRefKindKeyword(), expr);

        public FieldDeclarationSyntax GenFieldDeclaration(
            List<AttributeListSyntax> attributes,
            VariableDeclarationSyntax declaration)
            => FieldDeclaration(attributes.ToSyntaxList(), GenModifiers(), declaration);

        public ExpressionSyntax GenConstant()
            => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(5));

        private SyntaxTokenList GenModifiers()
        {
            int count = _rand.Next(10);
            List<SyntaxToken> tokens = new List<SyntaxToken>();
            for (int i = 0; i < count; i++)
            {
                int modifier = _rand.Next(s_modifiers.Length + 1);
                // Heavily prefer actual modifiers by only using random token
                // when modifier == 0
                if (modifier == 0)
                    tokens.Add(Token(s_tokens[_rand.Next(s_tokens.Length)]));
                else
                    tokens.Add(Token(s_modifiers[modifier - 1]));
            }

            return new SyntaxTokenList(tokens);
        }

        private SyntaxToken GenToken() => Token(s_tokens[_rand.Next(s_tokens.Length)]);
        private SyntaxToken GenVarianceKeyword() => RandomToken(s_varianceKeywords);
        private SyntaxToken GenRefKindKeyword() => RandomToken(s_refKindKeywords);

        private SyntaxToken RandomToken(SyntaxKind[] arr)
            => Token(arr[_rand.Next(arr.Length)]);

        private static readonly SyntaxKind[] s_modifiers =
        {
            SyntaxKind.PublicKeyword, SyntaxKind.PrivateKeyword,
            SyntaxKind.InternalKeyword, SyntaxKind.ProtectedKeyword,
            SyntaxKind.AbstractKeyword, SyntaxKind.AsyncKeyword,
            SyntaxKind.ConstKeyword, SyntaxKind.EventKeyword,
            SyntaxKind.ExternKeyword, SyntaxKind.NewKeyword,
            SyntaxKind.OverrideKeyword, SyntaxKind.PartialKeyword,
            SyntaxKind.ReadOnlyKeyword, SyntaxKind.SealedKeyword,
            SyntaxKind.StaticKeyword, SyntaxKind.UnsafeKeyword,
            SyntaxKind.VirtualKeyword, SyntaxKind.VolatileKeyword,
        };

        private static readonly SyntaxKind[] s_tokens =
            ((SyntaxKind[])Enum.GetValues(typeof(SyntaxKind)))
            .Where(sk => sk.ToString().EndsWith("Keyword") || sk.ToString().EndsWith("Token")).ToArray();

        private static readonly SyntaxKind[] s_varianceKeywords = { SyntaxKind.InKeyword, SyntaxKind.OutKeyword, SyntaxKind.None };

        private static readonly SyntaxKind[] s_refKindKeywords = { SyntaxKind.RefKeyword, SyntaxKind.InKeyword, SyntaxKind.OutKeyword, SyntaxKind.None };
    }

    internal static class SyntaxExtensions
    {
        internal static SyntaxList<T> ToSyntaxList<T>(this IEnumerable<T> col) where T : SyntaxNode
            => new SyntaxList<T>(col);
    }
}
