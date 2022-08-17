using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Fuzzlyn.Methods;

internal record class RefStructValue(
    ExpressionSyntax Expression,
    AggregateType Type,
    int SafeToEscapeScope);
