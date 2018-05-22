using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Fuzzlyn
{
    internal static class SyntaxExtensions
    {
        public static SyntaxList<T> ToSyntaxList<T>(this IEnumerable<T> vals) where T : SyntaxNode
            => new SyntaxList<T>(vals);
    }
}
