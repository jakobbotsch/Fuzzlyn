using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fuzzlyn
{
    internal static class Helpers
    {
        internal static string GenerateString(Random rand)
        {
            const string pool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder sb = new StringBuilder(rand.Next(5, 11));
            for (int i = 0; i < sb.Capacity; i++)
                sb.Append(pool[rand.Next(pool.Length)]);

            return sb.ToString();
        }
    }
}
