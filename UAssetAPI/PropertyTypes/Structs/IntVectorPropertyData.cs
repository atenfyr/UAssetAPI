using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// Structure for integer vectors in 3-d space.
/// </summary>
public struct IntVector : ICloneable
{
    public int X;
    public int Y;
    public int Z;

    public IntVector(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public IntVector(AssetBinaryReader reader)
    {
        X = reader.ReadInt32();
        Y = reader.ReadInt32();
        Z = reader.ReadInt32();
    }

    public int Write(AssetBinaryWriter writer)
    {
        writer.Write(X);
        writer.Write(Y);
        writer.Write(Z);
        return sizeof(int) * 3;
    }

    public object Clone() => new IntVector(X, Y, Z);
}

public class IntVectorPropertyData : PropertyData<IntVector>
{
    public IntVectorPropertyData(FName name) : base(name) { }

    public IntVectorPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("IntVector");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            PropertyGuid = reader.ReadPropertyGuid();
        }

        Value = new IntVector(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            writer.WritePropertyGuid(PropertyGuid);
        }

        return Value.Write(writer);
    }
}