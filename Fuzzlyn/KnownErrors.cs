using System;
using System.Collections.Generic;

namespace Fuzzlyn;

internal class KnownErrors(IEnumerable<string> errors)
{
    private readonly HashSet<string> _errors = new(errors);

    public bool Contains(string fullError)
    {
        foreach (string err in _errors)
        {
            if (fullError.Contains(err, StringComparison.Ordinal))
                return true;
        }

        return false;
    }

    public static KnownErrors DotnetRuntime { get; } = new KnownErrors(
        [
            "inVarToRegMap[varIndex] == REG_STK"
        ]);
}
