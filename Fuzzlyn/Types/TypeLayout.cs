using System;
using System.Diagnostics;
using System.Linq;

namespace Fuzzlyn.Types;

public class TypeLayout
{
    private int[] _fieldOffsets;

    public TypeLayout(int alignment, LayoutField[] fields, int size)
    {
        Alignment = alignment;
        Fields = fields;
        Size = size;

        _fieldOffsets = Fields.Select(f => f.Offset).ToArray();
    }

    public int Alignment { get; }
    public LayoutField[] Fields { get; }
    public int Size { get; }

    /// <summary>
    /// Get index of the first field after an offset. Returns Fields.Length if no fields are after.
    /// </summary>
    public int FirstFieldAfter(int offset)
    {
        int index = Array.BinarySearch(_fieldOffsets, offset);

        if (index < 0)
        {
            index = ~index;
        }
        else
        {
            index++;
        }

        return index;
    }
}

public struct LayoutField
{
    public LayoutField(int offset, AggregateField field)
    {
        Offset = offset;
        Field = field;
        Debug.Assert(Type is PrimitiveType || Type is AggregateType { Layout: not null });
    }

    public int Offset { get; }
    public AggregateField Field { get; }
    public FuzzType Type => Field.Type;
    public int Size => Type is PrimitiveType pt ? pt.Info.Size : ((AggregateType)Type).Layout.Size;
}
