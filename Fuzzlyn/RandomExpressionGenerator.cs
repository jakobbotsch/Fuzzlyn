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

        public AnonymousObjectMemberDeclaratorSyntax GetAnonymousObjectMemberDeclarator(NameEqualsSyntax nameEquals, ExpressionSyntax expression)
            => AnonymousObjectMemberDeclarator(nameEquals, expression);

        public ArrayCreationExpressionSyntax GenArrayCreationExpression(ArrayTypeSyntax type)
            => ArrayCreationExpression(type);

        public ArrayCreationExpressionSyntax GenArrayCreationExpression(ArrayTypeSyntax type, InitializerExpressionSyntax initializer)
            => ArrayCreationExpression(type, initializer);

        public AssignmentExpressionSyntax GenAssignmentExpression(ExpressionSyntax left, ExpressionSyntax right)
            => AssignmentExpression(RandomKind(s_assignmentKinds), left, right);

        public AwaitExpressionSyntax GenAwaitExpression(ExpressionSyntax expression)
            => AwaitExpression(expression);

        public BinaryExpressionSyntax GenBinaryExpression(ExpressionSyntax left, ExpressionSyntax right)
            => BinaryExpression(RandomKind(s_binopKinds), left, right);

        public CastExpressionSyntax GenCastExpression(TypeSyntax type, ExpressionSyntax expression)
            => CastExpression(type, expression);

        public CheckedExpressionSyntax GenCheckedExpression(ExpressionSyntax expression)
            => CheckedExpression(SyntaxKind.CheckedExpression, expression);

        public CheckedExpressionSyntax GenUncheckedExpression(ExpressionSyntax expression)
            => CheckedExpression(SyntaxKind.UncheckedExpression, expression);

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

        public InitializerExpressionSyntax GenInitializerExpression(List<ExpressionSyntax> expressions)
            => InitializerExpression(RandomKind(s_initializerKinds), SeparatedList(expressions));

        public BaseExpressionSyntax GenBaseExpression()
            => BaseExpression();

        public ThisExpressionSyntax GenThisExpression()
            => ThisExpression();

        public InterpolatedStringExpressionSyntax GenInterpolatedStringExpression(List<InterpolatedStringContentSyntax> contents)
            => InterpolatedStringExpression(Token(SyntaxKind.InterpolatedStringStartToken), contents.ToSyntaxList());

        public InterpolatedStringExpressionSyntax GenInterpolatedVerbatimStringExpression(List<InterpolatedStringContentSyntax> contents)
            => InterpolatedStringExpression(Token(SyntaxKind.InterpolatedVerbatimStringStartToken), contents.ToSyntaxList());

        public InterpolatedStringTextSyntax GenInterpolatedStringText(string text)
            => InterpolatedStringText(Token(TriviaList(), SyntaxKind.InterpolatedStringTextToken, text, text, TriviaList()));

        public InterpolationSyntax GenInterpolation(ExpressionSyntax expression)
            => Interpolation(expression);

        public InterpolationSyntax GenInterpolation(ExpressionSyntax expression, InterpolationAlignmentClauseSyntax alignment, InterpolationFormatClauseSyntax format)
            => Interpolation(expression, alignment, format);

        public InterpolationAlignmentClauseSyntax GenInterpolationAlignmentClause(ExpressionSyntax expr)
            => InterpolationAlignmentClause(Token(SyntaxKind.CommaToken), expr);

        public InterpolationFormatClauseSyntax GenInterpolationFormatClause(string format)
            => InterpolationFormatClause(Token(SyntaxKind.ColonToken), Token(TriviaList(), SyntaxKind.InterpolatedStringTextToken, format, format, TriviaList()));

        public InvocationExpressionSyntax GenInvocationExpression(ExpressionSyntax expression)
            => InvocationExpression(expression);

        public InvocationExpressionSyntax GenInvocationExpression(ExpressionSyntax expression, List<ArgumentSyntax> arguments)
            => InvocationExpression(expression, GenArgumentList(arguments));

        public IsPatternExpressionSyntax GenIsPatternExpression(ExpressionSyntax expression, PatternSyntax pattern)
            => IsPatternExpression(expression, pattern);

        //TODO: Making literal expressions is simple, but how do we want to generate them?
        public LiteralExpressionSyntax GenLiteralInt()
            => LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(_rand.Next()));

        public MakeRefExpressionSyntax GenMakeRefExpression(ExpressionSyntax expression)
            => MakeRefExpression(expression);

        public MemberAccessExpressionSyntax GenMemberAccessExpression(ExpressionSyntax expr, SimpleNameSyntax name)
            => MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, expr, name);

        public MemberAccessExpressionSyntax GenPointerMemberAccessExpression(ExpressionSyntax expr, SimpleNameSyntax name)
            => MemberAccessExpression(SyntaxKind.PointerMemberAccessExpression, expr, name);

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

        public PostfixUnaryExpressionSyntax GenPostfixUnaryExpression(ExpressionSyntax expression)
            => PostfixUnaryExpression(RandomKind(s_postfixUnaryOperatorKinds), expression);

        public PrefixUnaryExpressionSyntax GenPrefixUnaryExpression(ExpressionSyntax expression)
            => PrefixUnaryExpression(RandomKind(s_prefixUnaryOperatorKinds), expression);

        /*public QueryExpressionSyntax GenQueryExpression(FromClauseSyntax fromClause, QueryBodySyntax body)
            => QueryExpression(fromClause, body);

        public FromClauseSyntax GenFromClause(string identifier, ExpressionSyntax expression)
            => FromClause(identifier, expression);

        public QueryBodySyntax GenQueryBody()*/

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

        public ArrayRankSpecifierSyntax GenArrayRankSpecifier(List<ExpressionSyntax> ranks)
            => ArrayRankSpecifier(SeparatedList(ranks));

        public NullableTypeSyntax GenNullableType(TypeSyntax type)
            => NullableType(type);

        public OmittedTypeArgumentSyntax GenOmittedTypeArgument()
            => OmittedTypeArgument();

        public PointerTypeSyntax GenPointerType(TypeSyntax type)
            => PointerType(type);

        //TODO: PredefinedTypeSyntax, and RefTypeSyntax. Because wat

        public TupleTypeSyntax GenTupleType(List<TupleElementSyntax> tuples)
            => TupleType(SeparatedList(tuples));

        public TupleElementSyntax GenTupleElement(TypeSyntax type)
            => TupleElement(type);

        private static readonly SyntaxKind[] s_assignmentKinds =
        {
            SyntaxKind.SimpleAssignmentExpression, SyntaxKind.AddAssignmentExpression,
            SyntaxKind.SubtractAssignmentExpression, SyntaxKind.MultiplyAssignmentExpression,
            SyntaxKind.DivideAssignmentExpression, SyntaxKind.ModuloAssignmentExpression,
            SyntaxKind.AndAssignmentExpression, SyntaxKind.ExclusiveOrAssignmentExpression,
            SyntaxKind.OrAssignmentExpression, SyntaxKind.LeftShiftAssignmentExpression,
            SyntaxKind.RightShiftAssignmentExpression,
        };

        private static readonly SyntaxKind[] s_binopKinds =
        {
            SyntaxKind.AddExpression, SyntaxKind.SubtractExpression,
            SyntaxKind.MultiplyExpression, SyntaxKind.DivideExpression,
            SyntaxKind.ModuloExpression, SyntaxKind.LeftShiftExpression,
            SyntaxKind.RightShiftExpression, SyntaxKind.LogicalOrExpression,
            SyntaxKind.LogicalAndExpression, SyntaxKind.BitwiseOrExpression,
            SyntaxKind.BitwiseAndExpression, SyntaxKind.ExclusiveOrExpression,
            SyntaxKind.EqualsExpression, SyntaxKind.NotEqualsExpression,
            SyntaxKind.LessThanExpression, SyntaxKind.LessThanOrEqualExpression,
            SyntaxKind.GreaterThanExpression, SyntaxKind.GreaterThanOrEqualExpression,
            SyntaxKind.IsExpression, SyntaxKind.AsExpression,
            SyntaxKind.CoalesceExpression,
        };

        private static readonly SyntaxKind[] s_initializerKinds =
        {
            SyntaxKind.ObjectInitializerExpression, SyntaxKind.CollectionInitializerExpression,
            SyntaxKind.ArrayInitializerExpression, SyntaxKind.ComplexElementInitializerExpression
        };

        private static readonly SyntaxKind[] s_prefixUnaryOperatorKinds =
        {
            SyntaxKind.UnaryPlusExpression, SyntaxKind.UnaryMinusExpression,
            SyntaxKind.BitwiseNotExpression, SyntaxKind.LogicalNotExpression,
            SyntaxKind.PreIncrementExpression, SyntaxKind.PreDecrementExpression,
            SyntaxKind.AddressOfExpression, SyntaxKind.PointerIndirectionExpression,
        };

        private static readonly SyntaxKind[] s_postfixUnaryOperatorKinds =
        {
            SyntaxKind.PostIncrementExpression, SyntaxKind.PostDecrementExpression,
        };

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
