﻿using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Fuzzlyn.Statics;

/// <summary>
/// Manages static variables generated automatically in the class containing methods.
/// </summary>
internal class StaticsManager
{
    private readonly List<StaticField> _fields = new();
    private int _counter;

    public StaticsManager(Randomizer random, TypeManager types)
    {
        Random = random;
        Types = types;
    }

    public Randomizer Random { get; }
    public TypeManager Types { get; }
    public IReadOnlyList<StaticField> Fields => _fields;

    public StaticField PickStatic(FuzzType type = null)
    {
        List<StaticField> fields = _fields;
        if (type != null)
            fields = _fields.Where(f => f.Type == type).ToList();

        if (!fields.Any())
            return GenerateNewField(type);

        return Random.NextElement(fields);
    }

    public StaticField GenerateNewField(FuzzType type)
    {
        type = type ?? Types.PickType();

        string name = "s_" + (++_counter);
        StaticField field = new(type, name, LiteralGenerator.GenLiteral(Types, Random, type));
        _fields.Add(field);
        return field;
    }

    public IEnumerable<FieldDeclarationSyntax> OutputStatics()
        => _fields.Select(f => f.Output());
}
