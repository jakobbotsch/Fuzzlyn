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

        internal void GenerateMethods(Func<string> genChecksumSiteId)
        {
            FuncGenerator gen = new FuncGenerator(_funcs, Random, Types, Statics, genChecksumSiteId);
            gen.Generate(null, false);
        }

        internal (List<MethodDeclarationSyntax> StaticFuncs, Dictionary<AggregateType, List<MethodDeclarationSyntax>> TypeMethods) OutputMethods()
        {
            var staticFuncs = new List<MethodDeclarationSyntax>();
            var methodLists = new Dictionary<AggregateType, List<MethodDeclarationSyntax>>();
            foreach (FuncGenerator func in _funcs)
            {
                if (func.InstanceType == null)
                {
                    staticFuncs.Add(func.Output());
                }
                else
                {
                    if (!methodLists.TryGetValue(func.InstanceType, out var methods))
                        methodLists.Add(func.InstanceType, methods = new List<MethodDeclarationSyntax>());

                    methods.Add(func.Output());
                }
            }

            return (staticFuncs, methodLists);
        }
    }
}
