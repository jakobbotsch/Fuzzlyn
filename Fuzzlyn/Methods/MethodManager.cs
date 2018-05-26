using Fuzzlyn.Statics;
using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Methods
{
    internal class MethodManager
    {
        public MethodManager(Randomizer random, TypeManager types, StaticsManager statics)
        {
            Random = random;
            Types = types;
            Statics = statics;
        }

        public Randomizer Random { get; }
        public TypeManager Types { get; }
        public StaticsManager Statics { get; }

        internal void GenerateMethods()
        {
        }

        internal IEnumerable<MemberDeclarationSyntax> OutputMethods()
        {
            yield break;
        }
    }
}
