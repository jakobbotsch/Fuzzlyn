using System;

namespace Fuzzlyn
{
    // Specifies the context for a factory parameter.
    // For example, we use this mechanism to ensure that a StatementExpressionSyntax
    // is only given supported expressions (function calls, increment/decrement, etc.)
    // by allowing those expressions in the context.
    [AttributeUsage(AttributeTargets.Parameter)]
    internal class RequireContextAttribute : Attribute
    {
        public RequireContextAttribute(Context context)
        {
            Context = context;
        }

        public Context Context { get; }
    }
}
