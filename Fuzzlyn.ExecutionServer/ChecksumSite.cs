using System;
using System.Collections.Generic;

namespace Fuzzlyn.ExecutionServer;

public class ChecksumSite(string id, string value) : IEquatable<ChecksumSite>
{
    public string Id { get; } = id;
    public string Value { get; } = value;

    public override bool Equals(object obj)
    {
        return Equals(obj as ChecksumSite);
    }

    public bool Equals(ChecksumSite other)
    {
        return other != null &&
               Id == other.Id &&
               Value == other.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Value);
    }

    public override string ToString()
        => $"{Id}: {Value}";

    public static bool operator ==(ChecksumSite site1, ChecksumSite site2)
    {
        return EqualityComparer<ChecksumSite>.Default.Equals(site1, site2);
    }

    public static bool operator !=(ChecksumSite site1, ChecksumSite site2)
    {
        return !(site1 == site2);
    }
}
