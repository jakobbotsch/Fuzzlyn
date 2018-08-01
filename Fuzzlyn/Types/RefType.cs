using System;
using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Fuzzlyn.Types
{
    internal class RefType : FuzzType
    {
        public RefType(FuzzType innerType)
        {
            Debug.Assert(!(innerType is RefType), "Cannot create ref to ref type");
            InnerType = innerType;
        }

        public FuzzType InnerType { get; }

        public override SyntaxKind[] AllowedAdditionalAssignmentKinds
            => InnerType.AllowedAdditionalAssignmentKinds;

        public override TypeSyntax GenReferenceTo()
            => SyntaxFactory.RefType(InnerType.GenReferenceTo());

        public bool Equals(RefType other)
            => other != null && InnerType == other.InnerType;

        public override bool Equals(object obj)
            => Equals(obj as RefType);

        public override int GetHashCode()
            => HashCode.Combine(3, InnerType);
    }
}
