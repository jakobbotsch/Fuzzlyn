using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn
{
    partial class RandomSyntaxGenerator
    {
        [Recursive]
        public AnonymousMethodExpressionSyntax GenAnonymousMethodExpression(ParameterListSyntax parameters, BlockSyntax body)
            => AnonymousMethodExpression(parameters, body);

        public AnonymousMethodExpressionSyntax GenAnonymousAsyncMethodExpression(ParameterListSyntax parameters, BlockSyntax body)
            => AnonymousMethodExpression(parameters, body).WithAsyncKeyword(Token(SyntaxKind.AsyncKeyword));

        [Recursive]
        public SimpleLambdaExpressionSyntax GenSimpleLambdaExpressionWithBlock(ParameterSyntax parameter, BlockSyntax body)
            => SimpleLambdaExpression(parameter, body);

        [Recursive]
        public SimpleLambdaExpressionSyntax GenSimpleAsyncLambdaExpressionWithBlock(ParameterSyntax parameter, BlockSyntax body)
            => SimpleLambdaExpression(parameter, body).WithAsyncKeyword(Token(SyntaxKind.AsyncKeyword));

        public SimpleLambdaExpressionSyntax GenSimpleLambdaExpressionWithArrow(ParameterSyntax parameter, ArrowExpressionClauseSyntax body)
            => SimpleLambdaExpression(parameter, body);

        public SimpleLambdaExpressionSyntax GenSimpleAsyncLambdaExpressionWithArrow(ParameterSyntax parameter, ArrowExpressionClauseSyntax body)
            => SimpleLambdaExpression(parameter, body).WithAsyncKeyword(Token(SyntaxKind.AsyncKeyword));

        public ParenthesizedLambdaExpressionSyntax GenParenthesizedLambdaExpression(CSharpSyntaxNode body)
            => ParenthesizedLambdaExpression(body);

        public ParenthesizedLambdaExpressionSyntax GenParenthesizedLambdaExpression(ParameterListSyntax parameterList, CSharpSyntaxNode body)
            => ParenthesizedLambdaExpression(parameterList, body);

        public AnonymousObjectCreationExpressionSyntax GenAnonymousObjectCreationExpression(List<AnonymousObjectMemberDeclaratorSyntax> initializers)
            => AnonymousObjectCreationExpression(SeparatedList(initializers));

        public ArrayCreationExpressionSyntax GenArrayCreationExpression(ArrayTypeSyntax type)
            => ArrayCreationExpression(type);

        public ArrayCreationExpressionSyntax GenArrayCreationExpression(ArrayTypeSyntax type, InitializerExpressionSyntax initializer)
            => ArrayCreationExpression(type, initializer);

        // On the subject of AssignmentExpressions, are we to make a generator for each assignment kind? Also, how many assignments exist?

        public AwaitExpressionSyntax GenAwaitExpression(ExpressionSyntax expression)
            => AwaitExpression(expression);

        //TODO: Basically this, but for all the other existing kinds of Binops as well
        public BinaryExpressionSyntax GenBinaryExpression(ExpressionSyntax left, ExpressionSyntax right)
            => BinaryExpression(SyntaxKind.AddExpression, left, right);

        public CastExpressionSyntax GenCastExpression(TypeSyntax type, ExpressionSyntax expression)
            => CastExpression(type, expression);

        public CheckedExpressionSyntax GenCheckedExpression(ExpressionSyntax expression)
            => CheckedExpression(SyntaxKind.CheckedExpression, expression);

        public ConditionalExpressionSyntax GenConditionalExpression(ExpressionSyntax condition, ExpressionSyntax whenTrue, ExpressionSyntax whenFalse)
            => ConditionalExpression(condition, whenTrue, whenFalse);

        public ConditionalAccessExpressionSyntax GenConditionalAccessExpression(ExpressionSyntax condition, ExpressionSyntax whenNotNull)
            => ConditionalAccessExpression(condition, whenNotNull);

        public DeclarationExpressionSyntax GenDeclarationExpression(TypeSyntax type, VariableDesignationSyntax designation)
            => DeclarationExpression(type, designation);

        public DefaultExpressionSyntax GenDefaultExpression(TypeSyntax type)
            => DefaultExpression(type);

        public ElementAccessExpressionSyntax GenElementAccessExpression(ExpressionSyntax expression)
            => ElementAccessExpression(expression);

        public ImplicitArrayCreationExpressionSyntax GenImplicitArrayCreationExpression(InitializerExpressionSyntax initializer)
            => ImplicitArrayCreationExpression(initializer);

        public ImplicitArrayCreationExpressionSyntax GenImplicitArrayCreationExpressionWithTokens(InitializerExpressionSyntax initializer)
            => ImplicitArrayCreationExpression(GenModifiers(), initializer);

        public ImplicitElementAccessSyntax GenImplicitElementAccess()
            => ImplicitElementAccess();

        public ImplicitElementAccessSyntax GenImplicitElementAccess(BracketedArgumentListSyntax arguments)
            => ImplicitElementAccess(arguments);

        // TODO: Which kinds possible?
        /*public InitializerExpressionSyntax GenInitializerExpression(List<ExpressionSyntax> expressions)
            => InitializerExpression(null, SeparatedList(expressions));*/

        // NOTE: Alternate constructors for the following two ask for a token. Is this an interesting case?
        public BaseExpressionSyntax GenBaseExpression()
            => BaseExpression();

        public ThisExpressionSyntax GenThisExpression()
            => ThisExpression();

        // TODO: InterpolatedStringExpressionSyntax. Something about it looks weird.

        public InvocationExpressionSyntax GenInvocationExpression(ExpressionSyntax expression)
            => InvocationExpression(expression);

        public InvocationExpressionSyntax GenInvocationExpression(ExpressionSyntax expression, List<ArgumentSyntax> arguments)
            => InvocationExpression(expression, GenArgumentList(arguments));

        public IsPatternExpressionSyntax GenIsPatternExpression(ExpressionSyntax expression, PatternSyntax pattern)
            => IsPatternExpression(expression, pattern);

        //TODO: Making literal expressions is simple, but how do we want to generate them?

        public MakeRefExpressionSyntax GenRefExpressionSyntax(ExpressionSyntax expression)
            => MakeRefExpression(expression);

        // TODO: MemberAccessExpressionSyntax

        public MemberBindingExpressionSyntax GenMemberBindingExpression(SimpleNameSyntax name)
            => MemberBindingExpression(name);

        public ObjectCreationExpressionSyntax GenObjectCreationExpression(TypeSyntax type)
            => ObjectCreationExpression(type);

        public ObjectCreationExpressionSyntax GenObjectCreationExpression(TypeSyntax type, List<ArgumentSyntax> arguments, InitializerExpressionSyntax initializer)
            => ObjectCreationExpression(type, GenArgumentList(arguments), initializer);

        public OmittedArraySizeExpressionSyntax GenOmittedArraySizeExpression()
            => OmittedArraySizeExpression();

        public ParenthesizedExpressionSyntax GenParenthesizedExpression(ExpressionSyntax expression)
            => ParenthesizedExpression(expression);

        //TODO: PostfixUnaryExpressionSyntax. Not sure I remember all the operators *shrug*

        //... same for the Prefix version

        public QueryExpressionSyntax GenQueryExpression(FromClauseSyntax fromClause, QueryBodySyntax body)
            => QueryExpression(fromClause, body);
        //TODO: Those parameters^

        public RefExpressionSyntax GenRefExpression(ExpressionSyntax expression)
            => RefExpression(expression);

        public RefTypeExpressionSyntax GenRefTypeExpression(ExpressionSyntax expression)
            => RefTypeExpression(expression);

        public RefValueExpressionSyntax GenRefValueExpression(ExpressionSyntax expression, TypeSyntax type)
            => RefValueExpression(expression, type);

        public SizeOfExpressionSyntax GenSizeOfExpression(TypeSyntax type)
            => SizeOfExpression(type);

        public StackAllocArrayCreationExpressionSyntax GenStackAllocArrayCreationExpression(TypeSyntax type)
            => StackAllocArrayCreationExpression(type);

        public StackAllocArrayCreationExpressionSyntax GenStackAllocArrayCreationExpression(TypeSyntax type, InitializerExpressionSyntax initializer)
            => StackAllocArrayCreationExpression(type, initializer);

        public ThrowExpressionSyntax GenThrowExpression(ExpressionSyntax expression)
            => ThrowExpression(expression);

        public TupleExpressionSyntax GenTupleExpression(List<ArgumentSyntax> arguments)
            => TupleExpression(SeparatedList(arguments));

        public TypeOfExpressionSyntax GenTypeOfExpression(TypeSyntax type)
            => TypeOfExpression(type);

        // TYPES

        public ArrayTypeSyntax GenArrayType(TypeSyntax elementType)
            => ArrayType(elementType);

        public ArrayTypeSyntax GenArrayType(TypeSyntax elementType, List<ArrayRankSpecifierSyntax> rankSpecifiers)
            => ArrayType(elementType, rankSpecifiers.ToSyntaxList());

        public NullableTypeSyntax GenNullableType(TypeSyntax type)
            => NullableType(type);

        public OmittedTypeArgumentSyntax GenOmittedTypeArgument()
            => OmittedTypeArgument();

        public PointerTypeSyntax GenPointerType(TypeSyntax type)
            => PointerType(type);

        //TODO: PredefinedTypeSyntax, and RefTypeSyntax. Because wat

        public TupleTypeSyntax GenTupleType(List<TupleElementSyntax> tuples)
            => TupleType(SeparatedList(tuples));

        /*
         * Expressionsyntax includes:
         * Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousMethodExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.LambdaExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousObjectCreationExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.ArrayCreationExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.AwaitExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.BinaryExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.CastExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.CheckedExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.ConditionalAccessExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.ConditionalExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.DeclarationExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.DefaultExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.ElementAccessExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.ElementBindingExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitArrayCreationExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.ImplicitElementAccessSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.InitializerExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.BaseExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.ThisExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.InterpolatedStringExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.InvocationExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.IsPatternExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.LiteralExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.MakeRefExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.MemberAccessExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.MemberBindingExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.ObjectCreationExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.OmittedArraySizeExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.ParenthesizedExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.PostfixUnaryExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.PrefixUnaryExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.QueryExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.RefExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.RefTypeExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.RefValueExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.SizeOfExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.StackAllocArrayCreationExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.ThrowExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.TupleExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.TypeOfExpressionSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.ArrayTypeSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.NullableTypeSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.OmittedTypeArgumentSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.PointerTypeSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.PredefinedTypeSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.RefTypeSyntax
         * Microsoft.CodeAnalysis.CSharp.Syntax.TupleTypeSyntax
         */
    }
}
