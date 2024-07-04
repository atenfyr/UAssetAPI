using System;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// A vector in 3-D space composed of components (X, Y, Z) with floating point precision.
/// </summary>
public struct FVector3f : ICloneable
{
    public float X;
    public float Y;
    public float Z;

    public FVector3f(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public FVector3f(AssetBinaryReader reader)
    {
        X = reader.ReadSingle();
        Y = reader.ReadSingle();
        Z = reader.ReadSingle();
    }

    public int Write(AssetBinaryWriter writer)
    {
        writer.Write(X);
        writer.Write(Y);
        writer.Write(Z);
        return sizeof(float) * 3;
    }

    public object Clone() => new FVector3f(X, Y, Z);
}
