using Fuzzlyn.Types;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Fuzzlyn
{
    internal class TypeManager
    {
        private readonly List<PrimitiveType> _primitiveTypes = new List<PrimitiveType>();
        private readonly List<AggregateType> _aggTypes = new List<AggregateType>();

        public IEnumerable<MemberDeclarationSyntax> OutputTypes()
        {
            foreach (AggregateType type in _aggTypes)
                yield return type.Output();
        }

        public void GenerateTypes(Randomizer random)
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

            while (random.FlipCoin(random.Options.MoreTypesProbability))
            {
                bool isClass = random.FlipCoin(random.Options.ClassProbability);
                string name = isClass ? "C" : "S";
                name += _aggTypes.Count(t => t.IsClass == isClass);
                _aggTypes.Add(GenerateAggregateType(random, isClass, name));
            }
        }

        private AggregateType GenerateAggregateType(Randomizer random, bool isClass, string name)
        {
            int max = isClass ? random.Options.MaxClassFields : random.Options.MaxStructFields;
            int numFields = random.Rng.Next(1, max + 1);
            List<AggregateField> fields = new List<AggregateField>(numFields);
            for (int i = 0; i < numFields; i++)
            {
                FuzzType type;
                if (_aggTypes.Count > 0 && !random.FlipCoin(random.Options.PrimitiveFieldProbability))
                    type = _aggTypes[random.Rng.Next(_aggTypes.Count)];
                else
                    type = _primitiveTypes[random.Rng.Next(_primitiveTypes.Count)];

                fields.Add(new AggregateField(type, $"F{i}"));
            }

            return new AggregateType(isClass, name, fields);
        }
    }
}
