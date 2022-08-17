using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Fuzzlyn.Types;

internal class TypeManager
{
    private readonly List<PrimitiveType> _primitiveTypes = new();
    private readonly List<InterfaceType> _interfaceTypes = new();
    private readonly List<AggregateType> _aggTypes = new();
    private readonly List<AggregateType> _refStructs = new();
    private readonly Dictionary<InterfaceType, List<AggregateType>> _implementingTypes = new();

    public TypeManager(Randomizer random)
    {
        Random = random;
    }

    public Randomizer Random { get; }
    public FuzzlynOptions Options => Random.Options;

    public AggregateType PickAggregateType()
    {
        if (_aggTypes.Count <= 0 && _refStructs.Count <= 0)
        {
            return null;
        }

        int index = Random.Next(_aggTypes.Count + _refStructs.Count);
        return index >= _aggTypes.Count ? _refStructs[index - _aggTypes.Count] : _aggTypes[index];
    }

    public InterfaceType PickInterfaceType()
        => _interfaceTypes.Count > 0 ? Random.NextElement(_interfaceTypes) : null;

    public PrimitiveType PickPrimitiveType(Func<PrimitiveType, bool> filter)
    {
        PrimitiveType type;
        do
        {
            type = Random.NextElement(_primitiveTypes);
        } while (!filter(type));

        return type;
    }

    private FuzzType PickExistingNonRefStructType()
    {
        int num = Random.Next(_primitiveTypes.Count + _aggTypes.Count + _interfaceTypes.Count);
        if (num < _primitiveTypes.Count)
            return _primitiveTypes[num];

        num -= _primitiveTypes.Count;
        if (num < _aggTypes.Count)
            return _aggTypes[num];

        num -= _aggTypes.Count;
        return _interfaceTypes[num];
    }

    private FuzzType PickExistingType()
    {
        int num = Random.Next(_primitiveTypes.Count + _aggTypes.Count + _interfaceTypes.Count + _refStructs.Count);
        if (num < _primitiveTypes.Count)
            return _primitiveTypes[num];

        num -= _primitiveTypes.Count;
        if (num < _aggTypes.Count)
            return _aggTypes[num];

        num -= _aggTypes.Count;
        if (num < _interfaceTypes.Count)
            return _interfaceTypes[num];

        num -= _interfaceTypes.Count;
        return _refStructs[num];
    }

    public FuzzType PickType(bool allowRefStructs, double byRefProb = 0)
    {
        FuzzType type = allowRefStructs ? PickExistingType() : PickExistingNonRefStructType();
        if (!type.IsByRefLike)
        {
            int count = Options.MakeArrayCountDist.Sample(Random.Rng);
            for (int i = 0; i < count; i++)
                type = type.MakeArrayType(Options.ArrayRankDist.Sample(Random.Rng));

            // TODO: Support ref-to-byref-structs.
            // 
            bool byRef = byRefProb > 0 && Random.FlipCoin(byRefProb);
            if (byRef)
                type = new RefType(type);
        }

        return type;
    }

    public List<AggregateType> GetImplementingTypes(InterfaceType type)
        => _implementingTypes[type];

    public IEnumerable<AggregateType> NonRefAggregateTypes => _aggTypes;
    public IEnumerable<AggregateType> RefStructs => _refStructs;
    public IEnumerable<AggregateType> AggregateTypes => _aggTypes.Concat(_refStructs);
    public IEnumerable<InterfaceType> InterfaceTypes => _interfaceTypes;

    public IEnumerable<TypeDeclarationSyntax> OutputTypes(Dictionary<FuzzType, List<MethodDeclarationSyntax>> typeMethods)
    {
        foreach (InterfaceType type in _interfaceTypes)
        {
            yield return type.Output(typeMethods.GetValueOrDefault(type) ?? new List<MethodDeclarationSyntax>());
        }

        foreach (AggregateType type in _aggTypes.Concat(_refStructs).OrderBy(s => int.Parse(s.Name[1..])))
        {
            yield return type.Output(typeMethods.GetValueOrDefault(type) ?? new List<MethodDeclarationSyntax>());
        }
    }

    public void GenerateTypes()
    {
        SyntaxKind[] primitiveTypes =
        {
            SyntaxKind.BoolKeyword,
            SyntaxKind.SByteKeyword,
            SyntaxKind.ByteKeyword,
            SyntaxKind.ShortKeyword,
            SyntaxKind.UShortKeyword,
            SyntaxKind.IntKeyword,
            SyntaxKind.UIntKeyword,
            SyntaxKind.LongKeyword,
            SyntaxKind.ULongKeyword,
            // string, floats, IntPtr/UIntPtr?

            // if expanding fix PrimitiveType static ctor and GenPrimitiveLiteral as well
        };

        _primitiveTypes.AddRange(primitiveTypes.Select(pt => new PrimitiveType(pt)));

        int numAggs = Options.NumAggregateTypesDist.Sample(Random.Rng);
        for (int i = 0; i < numAggs; i++)
        {
            bool isClass = Random.FlipCoin(Options.MakeClassProb);
            string name = isClass ? "C" : "S";
            name += _aggTypes.Count(t => t.IsClass == isClass) + (isClass ? 0 : _refStructs.Count);
            AggregateType aggType = GenerateAggregateType(isClass, name);
            (aggType.IsByRefLike ? _refStructs : _aggTypes).Add(aggType);
        }

        int numInterfaces = _aggTypes.Count <= 0 ? 0 : Options.NumInterfaceTypesDist.Sample(Random.Rng);
        for (int i = 0; i < numInterfaces; i++)
        {
            InterfaceType it = new($"I{_interfaceTypes.Count}");
            _interfaceTypes.Add(it);

            int numImplementors = Options.NumImplementorsDist.Sample(Random.Rng);
            Debug.Assert(numImplementors > 0);

            numImplementors = Math.Min(numImplementors, _aggTypes.Count);

            List<AggregateType> implementors = new(_aggTypes);
            Random.Shuffle(implementors);
            implementors.RemoveRange(numImplementors, implementors.Count - numImplementors);

            foreach (AggregateType implementor in implementors)
                implementor.ImplementedInterfaces.Add(it);

            _implementingTypes.Add(it, implementors);
        }
    }

    private AggregateType GenerateAggregateType(bool isClass, string name)
    {
        int numFields = (isClass ? Options.NumClassFieldsDist : Options.NumStructFieldsDist).Sample(Random.Rng);
        bool byRefLike = false;

        List<AggregateField> fields = new(numFields);
        for (int i = 0; i < numFields; i++)
        {
            FuzzType type;
            if (!isClass && Random.FlipCoin(Options.RefFieldProb))
            {
                if ((_aggTypes.Count + _refStructs.Count) > 0 && !Random.FlipCoin(Options.PrimitiveFieldProb))
                {
                    int index = Random.Next(_aggTypes.Count + _refStructs.Count);
                    if (index >= _aggTypes.Count)
                        type = _refStructs[index - _aggTypes.Count];
                    else
                        type = new RefType(_aggTypes[index]);
                }
                else
                {
                    type = new RefType(Random.NextElement(_primitiveTypes));
                }

                byRefLike = true;
            }
            else
            {
                if (_aggTypes.Count > 0 && !Random.FlipCoin(Options.PrimitiveFieldProb))
                    type = Random.NextElement(_aggTypes);
                else
                    type = Random.NextElement(_primitiveTypes);
            }

            fields.Add(new AggregateField(type, $"F{i}"));
        }

        return new AggregateType(isClass, byRefLike, name, fields);
    }

    internal PrimitiveType GetPrimitiveType(SyntaxKind kind) => _primitiveTypes.First(pt => pt.Keyword == kind);
}
