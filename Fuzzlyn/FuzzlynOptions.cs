namespace Fuzzlyn
{
    public class FuzzlynOptions
    {
        public ulong? Seed { get; set; }
        public double MoreTypesProbability { get; set; } = 0.5;
        /// <summary>
        /// Probability that we pick a class when generating a new type. Otherwise we make a struct.
        /// </summary>
        public double ClassProbability { get; set; } = 0.5;
        public int MaxStructFields { get; set; } = 10;
        public int MaxClassFields { get; set; } = 10;
        // Probability that a field gets a primitive type vs a aggregate type.
        public double PrimitiveFieldProbability { get; set; } = 0.8;
    }
}
