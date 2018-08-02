using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Types
{
    // Class or struct type.
    public class AggregateType : FuzzType
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

        public override SyntaxKind[] AllowedAdditionalAssignmentKinds { get; } = new SyntaxKind[0];

        public override TypeSyntax GenReferenceTo()
            => IdentifierName(Name);

        public bool Equals(AggregateType other)
            => ReferenceEquals(this, other);

        public override bool Equals(object obj)
            => Equals(obj as AggregateType);

        public override int GetHashCode()
            => HashCode.Combine(0, RuntimeHelpers.GetHashCode(this));

        /// <summary>
        /// Count recursively how many primitive fields are in this aggregate type.
        /// </summary>
        public int GetTotalNumPrimitiveFields()
        {
            int count = 0;
            foreach (AggregateField field in _fields)
            {
                if (field.Type is AggregateType at)
                {
                    count += at.GetTotalNumPrimitiveFields();
                }
                else
                {
                    Debug.Assert(field.Type is PrimitiveType);
                    count++;
                }
            }

            return count;
        }

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

            return
                ConstructorDeclaration(Name)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithParameterList(pms)
                .WithBody(initBlock);
        }
    }
}
