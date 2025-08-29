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
            "isScalableVectorSize(size)",
            "(attrSize == EA_4BYTE) || (attrSize == EA_PTRSIZE) || (attrSize == EA_16BYTE) || (attrSize == EA_32BYTE) || (attrSize == EA_64BYTE) || (ins == INS_movzx) || (ins == INS_movsx) || (ins == INS_cmpxchg) || isPrefetch(ins)",
        ]);
}
