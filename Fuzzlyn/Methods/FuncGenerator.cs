﻿using Fuzzlyn.Statics;
using Fuzzlyn.Types;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Methods
{
    internal class FuncGenerator
    {
        private readonly List<ScopeFrame> _scope = new List<ScopeFrame>();
        private readonly List<FuncGenerator> _funcs;
        private readonly Func<string> _genChecksumSiteId;
        private readonly List<(AggregateType Type, FuncBodyGenerator Body)> _bodies = new();

        public FuncGenerator(
            List<FuncGenerator> funcs,
            Randomizer random,
            TypeManager types,
            StaticsManager statics,
            Func<string> genChecksumSiteId)
        {
            _funcs = funcs;
            Random = random;
            Types = types;
            Statics = statics;
            _genChecksumSiteId = genChecksumSiteId;

            FuncIndex = funcs.Count;
            Name = $"M{funcs.Count}";

            funcs.Add(this);
        }

        public Randomizer Random { get; }
        public FuzzlynOptions Options => Random.Options;
        public TypeManager Types { get; }
        public StaticsManager Statics { get; }
        public int FuncIndex { get; }

        // If non-null, this is an instance method on the specified type
        public AggregateType InstanceType { get; private set; }
        // If non-null, this is an interface method and has bodies for all implementing types.
        public InterfaceType InterfaceType { get; private set; }
        public FuzzType ReturnType { get; private set; }
        public FuncParameter[] Parameters { get; private set; }
        public string Name { get; }
        // Sum of number of all statements for all bodies of this function
        public int NumStatements { get; private set; }
        // Max (over all bodies) transitive call count from this function to other functions
        public Dictionary<int, long> CallCounts { get; } = new Dictionary<int, long>();

        public override string ToString()
            => OutputSignature(false, false).NormalizeWhitespace().ToFullString();

        public void Output(List<MethodDeclarationSyntax> staticMethods, Dictionary<FuzzType, List<MethodDeclarationSyntax>> typeMethods)
        {
            if (InterfaceType != null)
                typeMethods[InterfaceType].Add(OutputSignature(visibilityMod: false, staticMod: false).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));

            foreach ((AggregateType type, FuncBodyGenerator body) in _bodies)
            {
                List<MethodDeclarationSyntax> list = type == null ? staticMethods : typeMethods[type];
                list.Add(OutputSignature(visibilityMod: true, staticMod: type == null).WithBody(body.Block));
            }
        }

        private MethodDeclarationSyntax OutputSignature(bool visibilityMod, bool staticMod)
        {
            TypeSyntax retType;
            if (ReturnType == null)
                retType = PredefinedType(Token(SyntaxKind.VoidKeyword));
            else
                retType = ReturnType.GenReferenceTo();

            IEnumerable<ParameterSyntax> GenParameters()
            {
                foreach (FuncParameter pm in Parameters)
                {
                    if (pm.Type is RefType rt)
                    {
                        yield return
                            Parameter(Identifier(pm.Name))
                            .WithType(rt.InnerType.GenReferenceTo())
                            .WithModifiers(TokenList(Token(SyntaxKind.RefKeyword)));
                    }
                    else
                    {
                        yield return
                            Parameter(Identifier(pm.Name))
                            .WithType(pm.Type.GenReferenceTo());
                    }
                }
            }

            ParameterListSyntax parameters = ParameterList(SeparatedList(GenParameters()));

            SyntaxTokenList memberMods = TokenList();
            if (visibilityMod)
                memberMods = memberMods.Add(Token(SyntaxKind.PublicKeyword));
            if (staticMod)
                memberMods = memberMods.Add(Token(SyntaxKind.StaticKeyword));

            return
                MethodDeclaration(retType, Name)
                .WithModifiers(memberMods)
                .WithParameterList(parameters);
        }

        public void Generate(FuzzType returnType, bool randomizeParams)
        {
            ReturnType = returnType;

            List<ScopeValue> initialVars = new();
            if (randomizeParams)
            {
                switch ((FuncKind)Options.FuncKindDist.Sample(Random.Rng))
                {
                    case FuncKind.StaticMethod:
                        break;
                    case FuncKind.InstanceMethod:
                        InstanceType = Types.PickAggregateType();
                        break;
                    case FuncKind.InterfaceMethod:
                        InterfaceType = Types.PickInterfaceType();
                        break;
                }

                int numArgs = Options.MethodParameterCountDist.Sample(Random.Rng);
                Parameters = new FuncParameter[numArgs];
                for (int i = 0; i < Parameters.Length; i++)
                {
                    FuzzType type = Types.PickType(Options.ParameterIsByRefProb);
                    string name = $"arg{i}";
                    Parameters[i] = new FuncParameter(type, name);
                }

                foreach (FuncParameter param in Parameters)
                {
                    // A ref to a by-ref parameter can escape to at least its parent method
                    int refEscapeScope = param.Type is RefType ? 1 : 0;
                    initialVars.Add(new ScopeValue(param.Type, IdentifierName(param.Name), refEscapeScope, false));
                }
            }
            else
            {
                Parameters = Array.Empty<FuncParameter>();
            }

            List<AggregateType> typesNeedingBodies = new();
            if (InstanceType != null)
            {
                typesNeedingBodies.Add(InstanceType);
            }
            else if (InterfaceType != null)
            {
                foreach (AggregateType agg in Types.GetImplementingTypes(InterfaceType))
                    typesNeedingBodies.Add(agg);
            }
            else
                typesNeedingBodies.Add(null);

            foreach (AggregateType type in typesNeedingBodies)
            {
                FuncBodyGenerator gen = new(
                    _funcs,
                    Random,
                    Types,
                    Statics,
                    _genChecksumSiteId,
                    FuncIndex,
                    ReturnType,
                    type == null);

                if (type != null)
                {
                    // We cannot return refs to 'this', but we can pass them to funcs.
                    // For classes this is quite limited since 'this' is readonly.
                    initialVars.Add(new ScopeValue(type, ThisExpression(), refEscapeScope: 0, readOnly: type.IsClass));
                }

                gen.Generate(initialVars);
                _bodies.Add((type, gen));

                NumStatements += gen.NumStatements;
                foreach ((int index, long count) in gen.CallCounts)
                {
                    CallCounts.TryGetValue(index, out long existingCount);
                    if (count > existingCount)
                        CallCounts[index] = count;
                }

                if (type != null)
                    initialVars.RemoveAt(initialVars.Count - 1);
            }
        }
    }

    internal enum FuncKind
    {
        StaticMethod,
        InstanceMethod,
        InterfaceMethod,
    }
}
