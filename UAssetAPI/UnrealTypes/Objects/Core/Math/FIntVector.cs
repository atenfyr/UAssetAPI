using System;
using UAssetAPI.PropertyTypes.Objects;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// Structure for integer vectors in 3-d space.
/// </summary>
public struct FIntVector : ICloneable, IStruct<FIntVector>
{
    public int X;
    public int Y;
    public int Z;

    public FIntVector(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public FIntVector(AssetBinaryReader reader)
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

    public static FIntVector Read(AssetBinaryReader reader) => new FIntVector(reader);

    public object Clone() => new FIntVector(X, Y, Z);

    public static FIntVector FromString(string[] d, UAsset asset)
    {
        int.TryParse(d[0], out int X);
        int.TryParse(d[1], out int Y);
        int.TryParse(d[2], out int Z);
        return new FIntVector(X, Y, Z);
    }

    public override string ToString() => "(" + X + ", " + Y + ", " + Z + ")";
}
