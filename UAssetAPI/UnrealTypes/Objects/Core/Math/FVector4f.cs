using System;
using UAssetAPI.PropertyTypes.Objects;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// A vector in 4-D space composed of components (X, Y, Z, W) with floating point precision.
/// </summary>
public struct FVector4f : ICloneable, IStruct<FVector4f>
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

    public static FVector4f Read(AssetBinaryReader reader) => new FVector4f(reader);

    public override string ToString() => $"({X}, {Y}, {Z}, {W})";

    public static FVector4f FromString(string[] d, UAsset asset)
    {
        float.TryParse(d[0], out float X);
        float.TryParse(d[1], out float Y);
        float.TryParse(d[2], out float Z);
        float.TryParse(d[3], out float W);
        return new FVector4f(X, Y, Z, W);
    }
}
