using System;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// Structure for integer vectors in 3-d space.
/// </summary>
public struct FIntVector : ICloneable
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

    public object Clone() => new FIntVector(X, Y, Z);
}
