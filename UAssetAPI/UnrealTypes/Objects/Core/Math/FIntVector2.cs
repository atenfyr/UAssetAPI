using System;
using UAssetAPI.PropertyTypes.Objects;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// Structure for integer vectors in 2-d space.
/// </summary>
public struct FIntVector2 : ICloneable, IStruct<FIntVector2>
{
    public int X;
    public int Y;

    public FIntVector2(int x, int y)
    {
        X = x;
        Y = y;
    }

    public FIntVector2(AssetBinaryReader reader)
    {
        X = reader.ReadInt32();
        Y = reader.ReadInt32();
    }

    public static FIntVector2 Read(AssetBinaryReader reader) => new FIntVector2(reader);

    public int Write(AssetBinaryWriter writer)
    {
        writer.Write(X);
        writer.Write(Y);
        return sizeof(int) * 2;
    }

    public object Clone() => new FIntVector2(X, Y);

    public override string ToString() => "(" + X + ", " + Y + ")";

    public static FIntVector2 FromString(string[] d, UAsset asset)
    {
        int.TryParse(d[0], out int X);
        int.TryParse(d[1], out int Y);
        return new FIntVector2(X, Y);
    }
}
