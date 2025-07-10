using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Fuzzlyn.Statics;

/// <summary>
/// Manages static variables generated automatically in the class containing methods.
/// </summary>
internal class StaticsManager(Randomizer random, TypeManager types)
{
    private readonly List<StaticField> _fields = new();
    private readonly List<AsyncLocalField> _asyncLocals = new();
    private int _counter;

    public Randomizer Random { get; } = random;
    public TypeManager Types { get; } = types;
    public IReadOnlyList<StaticField> Fields => _fields;
    public IReadOnlyList<AsyncLocalField> AsyncLocalFields => _asyncLocals;

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

    public IEnumerable<MemberDeclarationSyntax> OutputMembers()
    {
        foreach (StaticField stat in _fields)
        {
            yield return stat.Output();
        }

        List<MethodDeclarationSyntax> asyncLocalGetters = new();
        foreach (AsyncLocalField asyncLocal in _asyncLocals)
        {
            (FieldDeclarationSyntax field, MethodDeclarationSyntax getter) = asyncLocal.Output();
            asyncLocalGetters.Add(getter);
            yield return field;
        }

        foreach (MethodDeclarationSyntax getter in asyncLocalGetters)
        {
            yield return getter;
        }
    }

    public AsyncLocalField GenerateNewAsyncLocal(FuzzType type)
    {
        type = type ?? Types.PickType();

        string name = "s_asyncLocal" + (++_counter);
        string getterName = "GetAsyncLocal" + _counter;
        AsyncLocalField field = new(type, name, getterName, LiteralGenerator.GenLiteral(Types, Random, type));
        _asyncLocals.Add(field);
        return field;
    }
}
