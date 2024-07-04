using System;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// Axis-aligned box collision geometry. Consists of a core AABB with a margin.
/// The margin should be considered physically part of the * box - it pads the faces and rounds the corners.
/// </summary>
/// <typeparam name="T"></typeparam>
public struct TBox<T> : ICloneable
{
    public T Min;
    public T Max;
    public byte IsValid;

    public TBox(T min, T max, byte isValid)
    {
        Min = min;
        Max = max;
        IsValid = isValid;
    }

    public TBox(AssetBinaryReader reader, Func<T> valueReader)
    {
        Min = valueReader();
        Max = valueReader();
        IsValid = reader.ReadByte();
    }

    public int Write(AssetBinaryWriter writer, Action<T> valueWriter)
    {
        var offset = writer.BaseStream.Position;
        valueWriter(Min);
        valueWriter(Max);
        writer.Write(IsValid);
        return (int)(writer.BaseStream.Position - offset);
    }

    public object Clone() => new TBox<T>(Min, Max, IsValid);
}
