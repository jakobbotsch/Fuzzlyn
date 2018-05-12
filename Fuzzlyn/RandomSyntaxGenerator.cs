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
    internal partial class RandomSyntaxGenerator
    {
        private Random _rand;

        public RandomSyntaxGenerator(Random rand)
        {
            _rand = rand;
        }

        public CompilationUnitSyntax GenCompilationUnit(
            List<ExternAliasDirectiveSyntax> externs,
            List<UsingDirectiveSyntax> usings,
            [RequireContext(Context.Global)]
            List<AttributeListSyntax> attributeLists,
            [RequireContext(Context.Global)]
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

        public GenericNameSyntax GenGenericName(string identifier, TypeArgumentListSyntax types)
            => GenericName(Identifier(identifier), types);

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

        [AllowIn(Context.Global, Context.Namespace)]
        public NamespaceDeclarationSyntax GenNamespaceDeclaration(
            NameSyntax name,
            List<ExternAliasDirectiveSyntax> externs,
            List<UsingDirectiveSyntax> usings,
            [RequireContext(Context.Namespace)]
            List<MemberDeclarationSyntax> members)
            => NamespaceDeclaration(name, externs.ToSyntaxList(), usings.ToSyntaxList(), members.ToSyntaxList());

        [AllowIn(Context.Global, Context.Namespace, Context.Class, Context.Struct)]
        public ClassDeclarationSyntax GenClassDeclaration(
            List<AttributeListSyntax> attributes,
            string name,
            TypeParameterListSyntax typeParams,
            BaseListSyntax bases,
            List<TypeParameterConstraintClauseSyntax> constraints,
            [RequireContext(Context.Class)]
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

        [AllowIn(Context.Global, Context.Namespace, Context.Class, Context.Struct)]
        public InterfaceDeclarationSyntax GenInterfaceDeclaration(
            List<AttributeListSyntax> attributes,
            string name,
            TypeParameterListSyntax typeParams,
            BaseListSyntax bases,
            List<TypeParameterConstraintClauseSyntax> constraints,
            [RequireContext(Context.Interface)]
            List<MemberDeclarationSyntax> members)
        {
            return InterfaceDeclaration(
                attributes.ToSyntaxList(),
                GenModifiers(),
                Identifier(name),
                typeParams,
                bases,
                constraints.ToSyntaxList(),
                members.ToSyntaxList());
        }

        [AllowIn(Context.Global, Context.Namespace, Context.Class, Context.Struct)]
        public EnumDeclarationSyntax GenEnumDeclaration(
            List<AttributeListSyntax> attributes,
            string name,
            BaseListSyntax bases,
            List<EnumMemberDeclarationSyntax> members)
        {
            return EnumDeclaration(attributes.ToSyntaxList(), GenModifiers(), Identifier(name), bases, SeparatedList(members));
        }

        public EnumMemberDeclarationSyntax GenEnumMemberDeclaration(
            List<AttributeListSyntax> attributes,
            string identifier,
            EqualsValueClauseSyntax equalsValue)
            => EnumMemberDeclaration(attributes.ToSyntaxList(), Identifier(identifier), equalsValue);

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

        [AllowIn(Context.Global, Context.Namespace, Context.Class, Context.Struct)]
        public StructDeclarationSyntax GenStructDeclaration(
            List<AttributeListSyntax> attributes,
            string name,
            TypeParameterListSyntax typeParams,
            BaseListSyntax bases,
            List<TypeParameterConstraintClauseSyntax> constraints,
            [RequireContext(Context.Struct)]
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

        [AllowIn(Context.Class, Context.Interface, Context.Struct)]
        public EventFieldDeclarationSyntax GenEventFieldDeclaration(
            List<AttributeListSyntax> attributes,
            [RequireContext(Context.Interface)]
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

        // Interfaces cannot contain initializers, so special case with list of IDs
        [AllowIn(Context.Interface)]
        public VariableDeclarationSyntax GenInterfaceEventFieldDeclaration(TypeSyntax type, List<string> identifiers)
            => VariableDeclaration(type, SeparatedList(identifiers.Select(VariableDeclarator)));

        public ArgumentSyntax GenArgument(ExpressionSyntax expr)
            => Argument(expr);

        public ArgumentSyntax GenArgument(ExpressionSyntax expr, NameColonSyntax nameColon)
            => Argument(nameColon, GenRefKindKeyword(), expr);

        [AllowIn(Context.Class, Context.Struct)]
        public FieldDeclarationSyntax GenFieldDeclaration(
            List<AttributeListSyntax> attributes,
            VariableDeclarationSyntax declaration)
            => FieldDeclaration(attributes.ToSyntaxList(), GenModifiers(), declaration);

        [AllowIn(Context.Class, Context.Struct)]
        public EventDeclarationSyntax GenEventDeclaration(
            List<AttributeListSyntax> attributes,
            TypeSyntax type,
            ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier,
            string identifier,
            AccessorListSyntax accessorList)
        {
            return EventDeclaration(
                attributes.ToSyntaxList(),
                GenModifiers(),
                type,
                explicitInterfaceSpecifier,
                Identifier(identifier),
                accessorList);
        }

        public ExplicitInterfaceSpecifierSyntax GenExplicitInterfaceSpecifier(NameSyntax name)
            => ExplicitInterfaceSpecifier(name);

        public AccessorListSyntax GenAccessorList(List<AccessorDeclarationSyntax> accessorDecls)
            => AccessorList(accessorDecls.ToSyntaxList());

        public AccessorDeclarationSyntax GenAccessorDecl(List<AttributeListSyntax> attributes, BlockSyntax body)
            => AccessorDeclaration(GenAccessorKind(), attributes.ToSyntaxList(), GenModifiers(), body);

        public AccessorDeclarationSyntax GenAccessorDecl(List<AttributeListSyntax> attributes, ArrowExpressionClauseSyntax expressionBody)
            => AccessorDeclaration(GenAccessorKind(), attributes.ToSyntaxList(), GenModifiers(), expressionBody).WithSemicolonToken(Token(SyntaxKind.SemicolonToken));

        public ArrowExpressionClauseSyntax GenArrowExpressionClause(ExpressionSyntax expression)
            => ArrowExpressionClause(expression);

        public IndexerDeclarationSyntax GenIndexerDeclaration(
            List<AttributeListSyntax> attributes,
            TypeSyntax type,
            ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier,
            BracketedParameterListSyntax parameterList,
            AccessorListSyntax accessorList)
            => IndexerDeclaration(attributes.ToSyntaxList(), GenModifiers(), type, explicitInterfaceSpecifier, parameterList, accessorList);

        public IndexerDeclarationSyntax GenIndexerDeclaration(
            List<AttributeListSyntax> attributes,
            TypeSyntax type,
            ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier,
            BracketedParameterListSyntax parameterList,
            ArrowExpressionClauseSyntax expressionBody)
            => IndexerDeclaration(attributes.ToSyntaxList(), GenModifiers(), type, explicitInterfaceSpecifier, Token(SyntaxKind.ThisKeyword), parameterList, null, expressionBody, Token(SyntaxKind.SemicolonToken));

        public BracketedParameterListSyntax GenBracketedParameterList(List<ParameterSyntax> parameters)
            => BracketedParameterList(SeparatedList(parameters));

        public ParameterSyntax GenParameter(
            List<AttributeListSyntax> attributes,
            TypeSyntax type,
            string identifier,
            EqualsValueClauseSyntax defaultValue)
            => Parameter(attributes.ToSyntaxList(), GenModifiers(), type, Identifier(identifier), defaultValue);

        public PropertyDeclarationSyntax GenPropertyDeclaration(
            List<AttributeListSyntax> attributes,
            TypeSyntax type,
            ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier,
            string identifier,
            AccessorListSyntax accessors,
            EqualsValueClauseSyntax initializer)
            => PropertyDeclaration(attributes.ToSyntaxList(), GenModifiers(), type, explicitInterfaceSpecifier, Identifier(identifier), accessors, null, initializer);

        public PropertyDeclarationSyntax GenPropertyDeclaration(
            List<AttributeListSyntax> attributes,
            TypeSyntax type,
            ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier,
            string identifier,
            ArrowExpressionClauseSyntax expressionBody)
            => PropertyDeclaration(attributes.ToSyntaxList(), GenModifiers(), type, explicitInterfaceSpecifier, Identifier(identifier), null, expressionBody, null, Token(SyntaxKind.SemicolonToken));

        public DelegateDeclarationSyntax GenDelegateDeclaration(
            List<AttributeListSyntax> attributes,
            TypeSyntax returnType,
            string identifier,
            TypeParameterListSyntax typeParameterList,
            ParameterListSyntax parameterList,
            List<TypeParameterConstraintClauseSyntax> constraintClauses)
        {
            return DelegateDeclaration(
                attributes.ToSyntaxList(),
                GenModifiers(),
                returnType,
                Identifier(identifier),
                typeParameterList,
                parameterList,
                constraintClauses.ToSyntaxList());
        }

        public ParameterListSyntax GenParameterList(List<ParameterSyntax> parameters)
            => ParameterList(SeparatedList(parameters));

        public ConstructorDeclarationSyntax GenConstructorDeclaration(
            List<AttributeListSyntax> attributes,
            string identifier,
            ParameterListSyntax parameters,
            ConstructorInitializerSyntax initializer,
            BlockSyntax body)
        {
            return ConstructorDeclaration(
                attributes.ToSyntaxList(),
                GenModifiers(),
                Identifier(identifier),
                parameters,
                initializer,
                body);
        }

        public ConstructorDeclarationSyntax GenConstructorDeclaration(
            List<AttributeListSyntax> attributes,
            string identifier,
            ParameterListSyntax parameters,
            ConstructorInitializerSyntax initializer,
            ArrowExpressionClauseSyntax expressionBody)
        {
            return ConstructorDeclaration(
                attributes.ToSyntaxList(),
                GenModifiers(),
                Identifier(identifier),
                parameters,
                initializer,
                null,
                expressionBody,
                Token(SyntaxKind.SemicolonToken));
        }

        public ConstructorInitializerSyntax GenConstructorInitializer(ArgumentListSyntax args)
            => ConstructorInitializer(RandomKind(s_constructorInitKinds), args);

        public ArgumentListSyntax GenArgumentList(List<ArgumentSyntax> args)
            => ArgumentList(SeparatedList(args));

        public DestructorDeclarationSyntax GenDestructorDeclaration(
            List<AttributeListSyntax> attributes,
            string identifier,
            ParameterListSyntax parameters,
            BlockSyntax body)
        {
            return DestructorDeclaration(
                attributes.ToSyntaxList(),
                GenModifiers(),
                Identifier(identifier),
                parameters,
                body);
        }

        public DestructorDeclarationSyntax GenDestructorDeclaration(
            List<AttributeListSyntax> attributes,
            string identifier,
            ParameterListSyntax parameters,
            ArrowExpressionClauseSyntax expressionBody)
        {
            return DestructorDeclaration(
                attributes.ToSyntaxList(),
                GenModifiers(),
                Token(SyntaxKind.TildeToken),
                Identifier(identifier),
                parameters,
                expressionBody,
                Token(SyntaxKind.SemicolonToken));
        }

        public ConversionOperatorDeclarationSyntax GenConversionOperatorDeclaration(
            List<AttributeListSyntax> attributes,
            TypeSyntax type,
            ParameterListSyntax parameters,
            BlockSyntax body)
        {
            return ConversionOperatorDeclaration(
                attributes.ToSyntaxList(),
                GenModifiers(),
                RandomToken(s_conversionOperatorKind),
                type,
                parameters,
                body,
                null);
        }

        public ConversionOperatorDeclarationSyntax GenConversionOperatorDeclaration(
            List<AttributeListSyntax> attributes,
            TypeSyntax type,
            ParameterListSyntax parameters,
            ArrowExpressionClauseSyntax expressionBody)
        {
            return ConversionOperatorDeclaration(
                attributes.ToSyntaxList(),
                GenModifiers(),
                RandomToken(s_conversionOperatorKind),
                Token(SyntaxKind.OperatorKeyword),
                type,
                parameters,
                null,
                expressionBody,
                Token(SyntaxKind.SemicolonToken));
        }

        public OperatorDeclarationSyntax GenOperatorDeclaration(
            List<AttributeListSyntax> attributes,
            TypeSyntax returnType,
            ParameterListSyntax parameters,
            BlockSyntax body)
        {
            return OperatorDeclaration(
                attributes.ToSyntaxList(),
                GenModifiers(),
                returnType,
                RandomToken(s_operatorKind),
                parameters,
                body,
                null);
        }

        public OperatorDeclarationSyntax GenOperatorDeclaration(
            List<AttributeListSyntax> attributes,
            TypeSyntax returnType,
            ParameterListSyntax parameters,
            ArrowExpressionClauseSyntax expressionBody)
        {
            return OperatorDeclaration(
                attributes.ToSyntaxList(),
                GenModifiers(),
                returnType,
                Token(SyntaxKind.OperatorKeyword),
                RandomToken(s_operatorKind),
                parameters,
                null,
                expressionBody,
                Token(SyntaxKind.SemicolonToken));
        }

        public MethodDeclarationSyntax GenMethodDeclaration(
            List<AttributeListSyntax> attributes,
            TypeSyntax returnType,
            ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier,
            string identifier,
            TypeParameterListSyntax typeParameterList,
            ParameterListSyntax parameters,
            List<TypeParameterConstraintClauseSyntax> constraints,
            BlockSyntax body)
        {
            return MethodDeclaration(
                attributes.ToSyntaxList(),
                GenModifiers(),
                returnType,
                explicitInterfaceSpecifier,
                Identifier(identifier),
                typeParameterList,
                parameters,
                constraints.ToSyntaxList(),
                body,
                null);
        }

        public MethodDeclarationSyntax GenMethodDeclaration(
            List<AttributeListSyntax> attributes,
            TypeSyntax returnType,
            ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier,
            string identifier,
            TypeParameterListSyntax typeParameterList,
            ParameterListSyntax parameters,
            List<TypeParameterConstraintClauseSyntax> constraints,
            ArrowExpressionClauseSyntax expressionBody)
        {
            return MethodDeclaration(
                attributes.ToSyntaxList(),
                GenModifiers(),
                returnType,
                explicitInterfaceSpecifier,
                Identifier(identifier),
                typeParameterList,
                parameters,
                constraints.ToSyntaxList(),
                null,
                expressionBody,
                Token(SyntaxKind.SemicolonToken));
        }

        private SyntaxTokenList GenModifiers()
        {
            int count = _rand.Next(3);
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
        private SyntaxKind GenAccessorKind() => RandomKind(s_accessorKeywords);

        private SyntaxToken RandomToken(SyntaxKind[] arr)
            => Token(RandomKind(arr));

        private SyntaxKind RandomKind(SyntaxKind[] arr)
            => arr[_rand.Next(arr.Length)];

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
        private static readonly SyntaxKind[] s_accessorKeywords = { SyntaxKind.GetAccessorDeclaration, SyntaxKind.SetAccessorDeclaration, SyntaxKind.AddAccessorDeclaration, SyntaxKind.RemoveAccessorDeclaration };
        private static readonly SyntaxKind[] s_constructorInitKinds = { SyntaxKind.BaseConstructorInitializer, SyntaxKind.ThisConstructorInitializer };
        private static readonly SyntaxKind[] s_conversionOperatorKind = { SyntaxKind.ImplicitKeyword, SyntaxKind.ExplicitKeyword };
        private static readonly SyntaxKind[] s_operatorKind =
        {
            SyntaxKind.PlusToken, SyntaxKind.MinusToken,
            SyntaxKind.ExclamationToken, SyntaxKind.TildeToken,
            SyntaxKind.PlusPlusToken, SyntaxKind.MinusMinusToken,
            SyntaxKind.AsteriskToken, SyntaxKind.SlashToken,
            SyntaxKind.PercentToken, SyntaxKind.LessThanLessThanToken,
            SyntaxKind.GreaterThanGreaterThanToken, SyntaxKind.BarToken,
            SyntaxKind.AmpersandToken, SyntaxKind.CaretToken,
            SyntaxKind.EqualsEqualsToken, SyntaxKind.ExclamationEqualsToken,
            SyntaxKind.LessThanToken, SyntaxKind.LessThanEqualsToken,
            SyntaxKind.GreaterThanToken, SyntaxKind.GreaterThanEqualsToken,
            SyntaxKind.FalseKeyword, SyntaxKind.TrueKeyword,
            SyntaxKind.IsKeyword,
        };
    }

    internal static class SyntaxExtensions
    {
        internal static SyntaxList<T> ToSyntaxList<T>(this IEnumerable<T> col) where T : SyntaxNode
            => new SyntaxList<T>(col);
    }
}
