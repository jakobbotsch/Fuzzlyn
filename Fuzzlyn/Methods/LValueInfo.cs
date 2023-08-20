using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Fuzzlyn.Methods;

/// <summary>
/// Represents information about an l-value, which is an expression that can be assigned to,
/// or have a reference taken to.
/// </summary>
internal class LValueInfo
{
    public LValueInfo(ExpressionSyntax expression, FuzzType type, int refEscapeScope, bool isOnStack, bool readOnly, ScopeValue baseLocal)
    {
        Expression = expression;
        Type = type;
        RefEscapeScope = refEscapeScope;
        IsOnStack = isOnStack;
        ReadOnly = readOnly;
        BaseLocal = baseLocal;
    }

    public ExpressionSyntax Expression { get; }
    public FuzzType Type { get; }
    /// <summary>See <see cref="ScopeValue.RefEscapeScope"/>.</summary>
    public int RefEscapeScope { get; }
    public bool IsOnStack { get; }
    public bool ReadOnly { get; }
    public ScopeValue BaseLocal { get; }

    public override string ToString() => Expression.ToString();
}
