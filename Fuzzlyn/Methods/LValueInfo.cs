using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Fuzzlyn.Methods;

/// <summary>
/// Represents information about an l-value, which is an expression that can be assigned to,
/// or have a reference taken to.
/// </summary>
internal class LValueInfo(ExpressionSyntax expression, FuzzType type, int refEscapeScope, bool readOnly, bool asyncHoistable, ScopeValue baseLocal)
{
    public ExpressionSyntax Expression { get; } = expression;
    public FuzzType Type { get; } = type;
    /// <summary>See <see cref="ScopeValue.RefEscapeScope"/>.</summary>
    public int RefEscapeScope { get; } = refEscapeScope;
    public bool ReadOnly { get; } = readOnly;
    public bool AsyncHoistable { get; } = asyncHoistable;
    public ScopeValue BaseLocal { get; } = baseLocal;

    public override string ToString() => Expression.ToString();
}
