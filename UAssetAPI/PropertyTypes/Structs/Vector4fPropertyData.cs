using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A vector in 4-D space composed of components (X, Y, Z, W) with floating point precision.
/// </summary>
public struct FVector4f : ICloneable
{
    public float X;
    public float Y;
    public float Z;
    public float W;

    public FVector4f(float x, float y, float z, float w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public FVector4f(AssetBinaryReader reader)
    {
        X = reader.ReadSingle();
        Y = reader.ReadSingle();
        Z = reader.ReadSingle();
        W = reader.ReadSingle();
    }

    public int Write(AssetBinaryWriter writer)
    {
        writer.Write(X);
        writer.Write(Y);
        writer.Write(Z);
        writer.Write(W);
        return sizeof(float) * 4;
    }

    public object Clone() => new FVector4f(X, Y, Z, W);
}


public class Vector4fPropertyData : PropertyData<FVector4f>
{
    public Vector4fPropertyData(FName name) : base(name) { }

    public Vector4fPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector4f");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            PropertyGuid = reader.ReadPropertyGuid();
        }

        Value = new FVector4f(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            writer.WritePropertyGuid(PropertyGuid);
        }

        return Value.Write(writer);
    }

    public override void FromString(string[] d, UAsset asset)
    {
        float.TryParse(d[0], out float X);
        float.TryParse(d[1], out float Y);
        float.TryParse(d[2], out float Z);
        float.TryParse(d[3], out float W);
        Value = new FVector4f(X, Y, Z, W);
    }

    public override string ToString()
    {
        return "(" + Value.X + ", " + Value.Y + ", " + Value.Z + ", " + Value.W + ")";
    }
}