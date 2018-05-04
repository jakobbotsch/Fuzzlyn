using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn
{
    partial class RandomSyntaxGenerator
    {
        [Recursive]
        public AnonymousMethodExpressionSyntax GenAnonymousMethodExpression(ParameterListSyntax parameters, CSharpSyntaxNode body)
            => AnonymousMethodExpression(parameters, body);

        /*public AnonymousMethodExpressionSyntax GenAnonymousAsyncMethodExpression(ParameterListSyntax parameters, BlockSyntax body)
            => AnonymousMethodExpression(parameters, body).WithAsyncKeyword(Token(SyntaxKind.AsyncKeyword));*/

        [Recursive]
        public SimpleLambdaExpressionSyntax GenSimpleLambdaExpressionWithBlock(ParameterSyntax parameter, CSharpSyntaxNode body)
            => SimpleLambdaExpression(parameter, body);

        [Recursive]
        public SimpleLambdaExpressionSyntax GenSimpleAsyncLambdaExpressionWithBlock(ParameterSyntax parameter, CSharpSyntaxNode body)
            => SimpleLambdaExpression(parameter, body).WithAsyncKeyword(Token(SyntaxKind.AsyncKeyword));

        /*public SimpleLambdaExpressionSyntax GenSimpleLambdaExpressionWithArrow(ParameterSyntax parameter, ArrowExpressionClauseSyntax body)
            => SimpleLambdaExpression(parameter, body)*/
    }
}
