using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Types
{
    // Class or struct type.
    internal class AggregateType : FuzzType
    {
        private readonly List<AggregateField> _fields = new List<AggregateField>();

        public AggregateType(bool isClass, string name, List<AggregateField> fields)
        {
            IsClass = isClass;
            Name = name;
            _fields = fields;
        }

        public string Name { get; }
        public IReadOnlyList<AggregateField> Fields => _fields;
        public bool IsClass { get; }

        public TypeDeclarationSyntax Output()
        {
            if (IsClass)
                return ClassDeclaration(Name).WithMembers(OutputMembers().ToSyntaxList());

            return StructDeclaration(Name).WithMembers(OutputMembers().ToSyntaxList());
        }

        private IEnumerable<MemberDeclarationSyntax> OutputMembers()
        {
            foreach (AggregateField field in _fields)
            {
                yield return
                    FieldDeclaration(
                        VariableDeclaration(
                            type: field.Type.GenReferenceTo(),
                            variables: SingletonSeparatedList(VariableDeclarator(field.Name))))
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)));
            }

            yield return OutputCtor();
        }

        private ConstructorDeclarationSyntax OutputCtor()
        {
            ParameterListSyntax pms = ParameterList(
                SeparatedList(_fields.Select(f => Parameter(Identifier(f.Name.ToLowerInvariant())).WithType(f.Type.GenReferenceTo()))));

            BlockSyntax initBlock =
                Block(_fields.Select(
                    f =>
                    ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName(f.Name),
                            IdentifierName(f.Name.ToLowerInvariant())))));

            return ConstructorDeclaration(Name).WithParameterList(pms).WithBody(initBlock);
        }

        public override TypeSyntax GenReferenceTo()
            => IdentifierName(Name);
    }
}
