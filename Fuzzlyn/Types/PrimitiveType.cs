using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Types
{
    public class PrimitiveType : FuzzType
    {
        public PrimitiveType(SyntaxKind keyword)
        {
            Keyword = keyword;
        }

        public SyntaxKind Keyword { get; }

        public override TypeSyntax GenReferenceTo() => PredefinedType(Token(Keyword));
    }
}
