using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public struct Vector2f : ICloneable
{
    public float X;
    public float Y;

    public Vector2f(float x, float y)
    {
        X = x;
        Y = y;
    }

    public Vector2f(AssetBinaryReader reader)
    {
        X = reader.ReadSingle();
        Y = reader.ReadSingle();
    }

    public int Write(AssetBinaryWriter writer)
    {
        writer.Write(X);
        writer.Write(Y);
        return sizeof(float) * 2;
    }

    public object Clone() => new Vector2f(X, Y);
}

public class DeprecateSlateVector2DPropertyData : PropertyData<Vector2f>
{
    public DeprecateSlateVector2DPropertyData(FName name) : base(name) { }

    public DeprecateSlateVector2DPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("DeprecateSlateVector2D");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            PropertyGuid = reader.ReadPropertyGuid();
        }

        Value = new Vector2f(reader);
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