using Fuzzlyn.Statics;
using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Methods
{
    internal class MethodManager
    {
        private readonly List<FuncGenerator> _funcs = new List<FuncGenerator>();

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
            FuncGenerator gen = new FuncGenerator(_funcs, Random, Types, Statics);
            gen.Generate(null, false);
        }

        internal IEnumerable<MethodDeclarationSyntax> OutputMethods()
            => _funcs.Select(f => f.Output());
    }
}
