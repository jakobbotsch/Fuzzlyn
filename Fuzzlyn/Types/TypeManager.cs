using Fuzzlyn.ProbabilityDistributions;
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
        public FuzzlynOptions Options => Random.Options;

        public AggregateType PickAggregateType()
            => _aggTypes.Count > 0 ? Random.NextElement(_aggTypes) : null;

        public PrimitiveType PickPrimitiveType(Func<PrimitiveType, bool> filter)
        {
            PrimitiveType type;
            do
            {
                type = Random.NextElement(_primitiveTypes);
            } while (!filter(type));

            return type;
        }

        public FuzzType PickExistingType()
        {
            int num = Random.Next(_primitiveTypes.Count + _aggTypes.Count);
            FuzzType type = num < _primitiveTypes.Count
                ? (FuzzType)_primitiveTypes[num]
                : _aggTypes[num - _primitiveTypes.Count];

            return type;
        }

        public FuzzType PickType(double byRefProb = 0)
        {
            FuzzType type = PickExistingType();
            int count = Options.MakeArrayCountDist.Sample(Random.Rng);
            for (int i = 0; i < count; i++)
                type = type.MakeArrayType(Options.ArrayRankDist.Sample(Random.Rng));

            if (Random.FlipCoin(byRefProb))
                type = new RefType(type);

            return type;
        }

        public IEnumerable<MemberDeclarationSyntax> OutputTypes()
        {
            foreach (AggregateType type in _aggTypes)
                yield return type.Output();
        }

        public void GenerateTypes()
        {
            SyntaxKind[] standardAssignments = new SyntaxKind[0];

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

            int count = Options.MakeAggregateTypeCountDist.Sample(Random.Rng);
            for (int i = 0; i < count; i++)
            {
                bool isClass = Random.FlipCoin(Options.MakeClassProb);
                string name = isClass ? "C" : "S";
                name += _aggTypes.Count(t => t.IsClass == isClass);
                _aggTypes.Add(GenerateAggregateType(isClass, name));
            }
        }

        private AggregateType GenerateAggregateType(bool isClass, string name)
        {
            int numFields = (isClass ? Options.MaxClassFieldsDist : Options.MaxStructFieldsDist).Sample(Random.Rng);
            List<AggregateField> fields = new List<AggregateField>(numFields);
            for (int i = 0; i < numFields; i++)
            {
                FuzzType type;
                if (_aggTypes.Count > 0 && !Random.FlipCoin(Options.PrimitiveFieldProb))
                    type = Random.NextElement(_aggTypes);
                else
                    type = Random.NextElement(_primitiveTypes);

                fields.Add(new AggregateField(type, $"F{i}"));
            }

            return new AggregateType(isClass, name, fields);
        }

        internal PrimitiveType GetPrimitiveType(SyntaxKind kind) => _primitiveTypes.First(pt => pt.Keyword == kind);
    }
}
