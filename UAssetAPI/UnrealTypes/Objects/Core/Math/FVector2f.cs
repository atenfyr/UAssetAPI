using System;
using UAssetAPI.PropertyTypes.Objects;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// A vector in 2-D space composed of components (X, Y) with floating point precision.
/// </summary>
public struct FVector2f : ICloneable, IStruct<FVector2f>
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
    public static FVector2f Read(AssetBinaryReader reader) => new FVector2f(reader);

    public int Write(AssetBinaryWriter writer)
    {
        writer.Write(X);
        writer.Write(Y);
        return sizeof(float) * 2;
    }

    public object Clone() => new FVector2f(X, Y);

    public override string ToString() => $"({X}, {Y})";
    public static FVector2f FromString(string[] d, UAsset asset)
    {
        float.TryParse(d[0], out float X);
        float.TryParse(d[1], out float Y);
        return new FVector2f(X, Y);
    }
}