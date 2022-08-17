using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Fuzzlyn.Methods;

/// <summary>
/// Represents information about an l-value, which is an expression that can be assigned to,
/// or have a reference taken to.
/// </summary>
internal class LValueInfo
{
    public LValueInfo(ExpressionSyntax expression, FuzzType type, int refSafeToEscapeScope, int safeToEscapeScope, bool readOnly)
    {
        Expression = expression;
        Type = type;
        RefSafeToEscapeScope = refSafeToEscapeScope;
        SafeToEscapeScope = safeToEscapeScope;
        ReadOnly = readOnly;
    }

    public ExpressionSyntax Expression { get; }
    public FuzzType Type { get; }
    /// <summary>See <see cref="ScopeValue.RefSafeToEscapeScope"/>.</summary>
    public int RefSafeToEscapeScope { get; }
    /// <summary>See <see cref="ScopeValue.SafeToEscapeScope"/>.</summary>
    public int SafeToEscapeScope { get; }
    public bool ReadOnly { get; }

    public override string ToString() => Expression.ToString();
}
