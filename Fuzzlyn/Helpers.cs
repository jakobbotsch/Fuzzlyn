using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Specialized;
using System.Text;

namespace Fuzzlyn
{
    internal static class Helpers
    {
        internal static string GenerateString(Random rand)
        {
            const string pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder sb = new(rand.Next(5, 11));
            for (int i = 0; i < sb.Capacity; i++)
                sb.Append(pool[rand.Next(pool.Length)]);

            return sb.ToString();
        }

        /// <summary>
        /// Determines whether an expression needs to be parenthesized to roundtrip between
        /// parsing and not parsing in all possible contexts.
        /// </summary>
        internal static bool RequiresParentheses(ExpressionSyntax expr)
        {
            // We can produce a tree that won't roundtrip in the parser if we aren't careful.
            // For example, (ushort)(a | 0) will be represented by:
            //  Cast
            //   |
            // Paren
            //   |
            //  Bin
            //  / \
            // a   0
            // But removing 'Paren' and outputting this gives (ushort)a | 0, which parses differently.

            if (expr is IdentifierNameSyntax ||
                expr is LiteralExpressionSyntax ||
                expr is MemberAccessExpressionSyntax ||
                expr is PostfixUnaryExpressionSyntax ||
                expr is ElementAccessExpressionSyntax ||
                expr is InvocationExpressionSyntax ||
                expr is CastExpressionSyntax ||
                expr is ParenthesizedExpressionSyntax)
            {
                return false;
            }

            if (expr is PrefixUnaryExpressionSyntax)
            {
                // Some prefix unary operators conflict as tokens if they are
                // repeated, e.g. -(-a) needs parentheses to not be confused
                // with --a. So we only permit omitting parentheses for the
                // following cases.
                switch (expr.Kind())
                {
                    case SyntaxKind.BitwiseNotExpression:
                    case SyntaxKind.LogicalNotExpression:
                    case SyntaxKind.PreIncrementExpression:
                    case SyntaxKind.PreDecrementExpression:
                    case SyntaxKind.PointerIndirectionExpression:
                    case SyntaxKind.IndexExpression:
                        return false;
                }
            }

            return true;
        }

        public static void SetExecutionEnvironmentVariables(StringDictionary envVars)
        {
            envVars["COMPlus_TieredCompilation"] = "0";
            envVars["COMPlus_JitThrowOnAssertionFailure"] = "1";
        }
    }
}
