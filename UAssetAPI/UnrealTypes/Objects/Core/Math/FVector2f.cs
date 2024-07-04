using System;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// A vector in 2-D space composed of components (X, Y) with floating point precision.
/// </summary>
public struct FVector2f : ICloneable
{
    public float X;
    public float Y;

    public FVector2f(float x, float y)
    {
        X = x;
        Y = y;
    }

    public FVector2f(AssetBinaryReader reader)
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

    public object Clone() => new FVector2f(X, Y);
}