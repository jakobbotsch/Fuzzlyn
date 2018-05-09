using System;

namespace Fuzzlyn
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class AllowInAttribute : Attribute
    {
        public AllowInAttribute(params Context[] contexts)
        {
            Contexts = contexts;
        }

        public Context[] Contexts { get; }
    }
}
