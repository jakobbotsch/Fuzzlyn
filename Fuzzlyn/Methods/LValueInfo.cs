using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Fuzzlyn.Methods
{
    /// <summary>
    /// Represents information about an l-value, which is an expression that can be assigned to,
    /// or have a reference taken to.
    /// </summary>
    internal class LValueInfo
    {
        public LValueInfo(ExpressionSyntax expression, FuzzType type, int refEscapeScope)
        {
            Expression = expression;
            Type = type;
            RefEscapeScope = refEscapeScope;
        }

        public ExpressionSyntax Expression { get; }
        public FuzzType Type { get; }
        /// <summary>
        /// A value indicating where a ref taken to this lvalue may escape to.
        /// If 0, a ref may not escape to a parent. If above 0, a ref to this lvalue can be returned.
        /// </summary>
        public int RefEscapeScope { get; }

        public override string ToString() => Expression.ToString();
    }
}
