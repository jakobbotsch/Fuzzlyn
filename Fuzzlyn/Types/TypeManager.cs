using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fuzzlyn.Types
{
    internal class TypeManager
    {
        private readonly List<PrimitiveType> _primitiveTypes = new List<PrimitiveType>();
        private readonly List<AggregateType> _aggTypes = new List<AggregateType>();

        public TypeManager(Randomizer random)
        {
            Random = random;
        }

        public Randomizer Random { get; }

        public FuzzType PickType(Func<FuzzType, bool> filter = null)
        {
            FuzzType type;
            do
            {
                int num = Random.Next(_primitiveTypes.Count + _aggTypes.Count);
                type = num < _primitiveTypes.Count
                    ? (FuzzType)_primitiveTypes[num]
                    : _aggTypes[num - _primitiveTypes.Count];
            } while (filter != null && !filter(type));

            return type;
        }

        public IEnumerable<MemberDeclarationSyntax> OutputTypes()
        {
            foreach (AggregateType type in _aggTypes)
                yield return type.Output();
        }

        public void GenerateTypes()
        {
            SyntaxKind[] primitiveTypes =
            {
                SyntaxKind.BoolKeyword, SyntaxKind.CharKeyword,
                SyntaxKind.SByteKeyword, SyntaxKind.ByteKeyword,
                SyntaxKind.ShortKeyword, SyntaxKind.UShortKeyword,
                SyntaxKind.IntKeyword, SyntaxKind.UIntKeyword,
                SyntaxKind.LongKeyword, SyntaxKind.ULongKeyword,
                // string, floats, IntPtr/UIntPtr?
            };

            _primitiveTypes.AddRange(primitiveTypes.Select(pt => new PrimitiveType(pt)));

            int count = Random.Options.MakeAggregateTypeCountDist.Sample(Random.Rng);
            for (int i = 0; i < count; i++)
            {
                bool isClass = Random.FlipCoin(Random.Options.MakeClassProb);
                string name = isClass ? "C" : "S";
                name += _aggTypes.Count(t => t.IsClass == isClass);
                _aggTypes.Add(GenerateAggregateType(isClass, name));
            }
        }

        private AggregateType GenerateAggregateType(bool isClass, string name)
        {
            int numFields = (isClass ? Random.Options.MaxClassFieldsDist : Random.Options.MaxStructFieldsDist).Sample(Random.Rng);
            List<AggregateField> fields = new List<AggregateField>(numFields);
            for (int i = 0; i < numFields; i++)
            {
                FuzzType type;
                if (_aggTypes.Count > 0 && !Random.FlipCoin(Random.Options.PrimitiveFieldProb))
                    type = _aggTypes[Random.Next(_aggTypes.Count)];
                else
                    type = _primitiveTypes[Random.Next(_primitiveTypes.Count)];

                fields.Add(new AggregateField(type, $"F{i}"));
            }

            return new AggregateType(isClass, name, fields);
        }
    }
}
