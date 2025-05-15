using Fuzzlyn.ExecutionServer;
using Fuzzlyn.Statics;
using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fuzzlyn.Methods;

internal class MethodManager(Randomizer random, TypeManager types, StaticsManager statics)
{
    private readonly List<FuncGenerator> _funcs = new();

    public Randomizer Random { get; } = random;
    public TypeManager Types { get; } = types;
    public StaticsManager Statics { get; } = statics;
    public ApiManager Apis { get; } = new ApiManager(random);

    internal void GenerateMethods(Func<string> genChecksumSiteId)
    {
        Apis.Initialize(Types, Random.Options.GenExtensions);

        FuncGenerator gen = new(_funcs, Random, Types, Statics, Apis, genChecksumSiteId);
        gen.Generate(returnType: null, randomizeParams: false);
    }

    internal (List<MethodDeclarationSyntax> StaticFuncs, Dictionary<FuzzType, List<MethodDeclarationSyntax>> TypeMethods) OutputMethods()
    {
        var staticFuncs = new List<MethodDeclarationSyntax>();
        var typeMethods = new Dictionary<FuzzType, List<MethodDeclarationSyntax>>();
        foreach (FuzzType type in ((IEnumerable<FuzzType>)Types.AggregateTypes).Concat(Types.InterfaceTypes))
        {
            typeMethods.Add(type, new List<MethodDeclarationSyntax>());
        }

        foreach (FuncGenerator func in _funcs)
        {
            func.Output(staticFuncs, typeMethods);
        }

        return (staticFuncs, typeMethods);
    }
}
