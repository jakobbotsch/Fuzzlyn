using System;

namespace Fuzzlyn
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class RecursiveAttribute : Attribute
    {
    }
}
