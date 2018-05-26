using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Fuzzlyn.Statics
{
    /// <summary>
    /// Manages static variables generated automatically in the class containing methods.
    /// </summary>
    internal class StaticsManager
    {
        private readonly List<StaticField> _fields = new List<StaticField>();
        private int _counter;

        public StaticsManager(Randomizer random, TypeManager types)
        {
            Random = random;
            Types = types;
        }

        public Randomizer Random { get; }
        public TypeManager Types { get; }

        public StaticField GenerateNewField()
        {
            FuzzType type = Types.PickType();
            int count = Random.Options.StaticFieldMakeArrayCountDist.Sample(Random.Rng);
            for (int i = 0; i < count; i++)
            {
                type = type.MakeArrayType(Random.Options.ArrayRankDist.Sample(Random.Rng));
            }

            string name = "s_" + (++_counter);
            _fields.Add(new StaticField(type, name, null));
            return null;
        }

        public IEnumerable<MemberDeclarationSyntax> OutputStatics()
            => _fields.Select(f => f.Output());
    }
}
