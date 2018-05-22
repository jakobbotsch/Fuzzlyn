namespace Fuzzlyn.Types
{
    internal class AggregateField
    {
        public AggregateField(FuzzType type, string name)
        {
            Type = type;
            Name = name;
        }

        public FuzzType Type { get; }
        public string Name { get; }
    }
}
