using Newtonsoft.Json.Linq;
using System;
using UAssetAPI.PropertyTypes.Objects;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// A vector in 3-D space composed of components (X, Y, Z) with floating point precision.
/// </summary>
public struct FVector3f : ICloneable, IStruct<FVector3f>
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

    public static FVector3f Read(AssetBinaryReader reader) => new FVector3f(reader);

    public override string ToString() => $"({X}, {Y}, {Z})";

    public static FVector3f FromString(string[] d, UAsset asset)
    {
        float.TryParse(d[0], out float X);
        float.TryParse(d[1], out float Y);
        float.TryParse(d[2], out float Z);
        return new FVector3f(X, Y, Z);
    }
}
