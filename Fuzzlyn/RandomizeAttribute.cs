using System;

namespace Fuzzlyn
{
    [AttributeUsage(AttributeTargets.Parameter)]
    internal class RandomizationAttribute : Attribute
    {
        public RandomizationAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
