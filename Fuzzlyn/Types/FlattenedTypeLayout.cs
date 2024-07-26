using System;
using System.Diagnostics;
using System.Linq;

namespace Fuzzlyn.Types;

public class FlattenedTypeLayout
{
    private int[] _fieldOffsets;

    public FlattenedTypeLayout(int alignment, FlattenedLayoutField[] fields, int size)
    {
        Alignment = alignment;
        Fields = fields;
        Size = size;
        _fieldOffsets = Fields.Select(f => f.Offset).ToArray();
    }

    public int Alignment { get; }
    public FlattenedLayoutField[] Fields { get; }
    public int Size { get; }

    public bool TouchesPadding(int offset, int size)
    {
        if (offset + size > Size)
            return true;

        FlattenedLayoutField[] fields = Fields;
        int index = Array.BinarySearch(_fieldOffsets, offset);
        int prevEnd = offset;
        if (index < 0)
        {
            index = ~index;
            Debug.Assert(index > 0); // Should always have a field at Offset == 0
            ref FlattenedLayoutField prevField = ref fields[index - 1];
            prevEnd = prevField.Offset + prevField.Size;
        }

        while (index <= fields.Length)
        {
            int start = index < fields.Length ? fields[index].Offset : Size;
            // We have padding from [prevEnd..start)

            Debug.Assert(start >= offset);

            if (offset + size <= prevEnd)
            {
                return false;
            }

            if (start > prevEnd)
            {
                return true;
            }

            if (index < fields.Length)
            {
                prevEnd = start + fields[index].Size;
            }

            index++;
        }

        return false;
    }

    /// <summary>
    /// Check if this layout is compatible with another layout starting at a specified offset.
    /// Compatibility here means that no padding is accessed as part of the reinterpretation.
    /// </summary>
    public bool IsSubsetCompatible(FlattenedTypeLayout other, int offset)
    {
        int otherSize = other.Size - offset;
        if (otherSize < Size)
            return false;

        foreach (FlattenedLayoutField field in Fields)
        {
            if (other.TouchesPadding(offset + field.Offset, field.Size))
            {
                return true;
            }
        }

        return false;
    }
}

public struct FlattenedLayoutField(int offset, int size)
{
    public int Offset { get; } = offset;
    public int Size { get; } = size;
}
