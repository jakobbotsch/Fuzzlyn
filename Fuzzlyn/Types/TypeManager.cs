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

        public FuzzType PickExistingType(Func<FuzzType, bool> filter = null)
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

        public FuzzType PickType()
        {
            FuzzType type = PickExistingType();
            int count = Random.Options.MakeArrayCountDist.Sample(Random.Rng);
            for (int i = 0; i < count; i++)
                type = type.MakeArrayType(Random.Options.ArrayRankDist.Sample(Random.Rng));

            return type;
        }

        public IReadOnlyList<FuzzType> GetTypesInGroup(TypeGroup group)
        {
            switch (group.Kind)
            {
                case TypeGroupKind.None:
                    return Array.Empty<FuzzType>();
                case TypeGroupKind.Single:
                    return new[] { group.SingleType };
                case TypeGroupKind.Multiple:
                    return group.MultipleTypes;
                case TypeGroupKind.Integral:
                    return _primitiveTypes.Where(pt => pt.Info.IsIntegral).ToList();
                default:
                    throw new ArgumentException("Cannot retrieve types in group of kind " + group.Kind, nameof(group));
            }
        }

        public FuzzType GetRandomTypeInGroup(TypeGroup group)
        {
            if (group.Kind == TypeGroupKind.None)
                throw new ArgumentException("Cannot get random type from None TypeGroup", nameof(group));

            if (group.Kind == TypeGroupKind.Any)
                return PickType();

            if (group.Kind == TypeGroupKind.Single)
                return group.SingleType;

            return Random.NextElement(GetTypesInGroup(group));
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
                SyntaxKind.CharKeyword,
                SyntaxKind.SByteKeyword,
                SyntaxKind.ByteKeyword,
                SyntaxKind.ShortKeyword,
                SyntaxKind.UShortKeyword,
                SyntaxKind.IntKeyword,
                SyntaxKind.UIntKeyword,
                SyntaxKind.LongKeyword,
                SyntaxKind.ULongKeyword,
                // string, floats, IntPtr/UIntPtr?

                // if expanding fix PrimitiveType ctor and GenLiteralInt as well
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
