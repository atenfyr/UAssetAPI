using UAssetAPI.PropertyTypes.Objects;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// 4x4 matrix of floating point values.
/// </summary>
public struct FMatrix : IStruct<FMatrix>
{
    public FPlane XPlane;
    public FPlane YPlane;
    public FPlane ZPlane;
    public FPlane WPlane;

    public FMatrix(FPlane xPlane, FPlane yPlane, FPlane zPlane, FPlane wPlane)
    {
        XPlane = xPlane;
        YPlane = yPlane;
        ZPlane = zPlane;
        WPlane = wPlane;
    }

    public FMatrix(AssetBinaryReader reader)
    {
        XPlane = new FPlane(reader);
        YPlane = new FPlane(reader);
        ZPlane = new FPlane(reader);
        WPlane = new FPlane(reader);
    }

    public static FMatrix Read(AssetBinaryReader reader) => new FMatrix(reader);

    public int Write(AssetBinaryWriter writer)
    {
        var size = XPlane.Write(writer);
        size += YPlane.Write(writer);
        size += ZPlane.Write(writer);
        size += WPlane.Write(writer);
        return size;
    }

    public static FMatrix FromString(string[] d, UAsset asset)
    {
        throw new System.NotImplementedException();
    }
}
