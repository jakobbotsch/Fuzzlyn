using System;
using System.Collections.Generic;

namespace Fuzzlyn;

internal class KnownErrors
{
    private readonly HashSet<string> _errors;

    public KnownErrors(IEnumerable<string> errors)
    {
        _errors = new HashSet<string>(errors);
    }

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
        new string[]
        {
            "!\"Too many unreachable block removal loops\"",
            "inVarToRegMap[varIndex] == REG_STK",
            "interval->isSpilled",
            "!foundMismatch",
        });
}
