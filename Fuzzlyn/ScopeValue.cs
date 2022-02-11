using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Fuzzlyn
{
    /// <summary>
    /// Value accessible in scope. Either a variable or 'this'.
    /// </summary>
    internal class ScopeValue
    {
        public ScopeValue(FuzzType type, ExpressionSyntax expr, int refEscapeScope, bool readOnly)
        {
            Type = type;
            Expression = expr;
            RefEscapeScope = refEscapeScope;
            ReadOnly = readOnly;
        }

        public FuzzType Type { get; }
        public ExpressionSyntax Expression { get; }
        /// <summary>
        /// If taking a ref, what scope can that ref return to?
        /// * Ref parameters have 1
        /// * Globals or class fields have int.MaxValue
        /// * Values in the first scope in a function have 0 (and value params have 0)
        /// * Values in the first nested scope in a function have -1
        /// * etc.
        /// For example, a positive ref escape scope indicates that a ref to that variable can be returned to the caller of a function.
        /// </summary>
        public int RefEscapeScope { get; }
        public bool ReadOnly { get; }
    }
}
