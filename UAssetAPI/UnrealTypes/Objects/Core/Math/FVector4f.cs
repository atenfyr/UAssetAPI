using System;

namespace UAssetAPI.UnrealTypes;

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
