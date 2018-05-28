using Fuzzlyn.Statics;
using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Methods
{
    internal class MethodManager
    {
        private readonly List<FuncGenerator> _funcs = new List<FuncGenerator>();
        private int _counter;

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
            FuncGenerator gen = new FuncGenerator($"M{_counter++}", Random, Types, Statics);
            gen.Generate(false);
            _funcs.Add(gen);
        }

        internal IEnumerable<MethodDeclarationSyntax> OutputMethods()
        {
            foreach (FuncGenerator gen in _funcs)
            {
                yield return
                    MethodDeclaration(
                        PredefinedType(
                            Token(SyntaxKind.VoidKeyword)),
                        gen.Name)
                    .WithModifiers(TokenList(Token(SyntaxKind.StaticKeyword)))
                    .WithBody(gen.Body);
            }
        }
    }
}
