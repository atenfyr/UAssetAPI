using UAssetAPI.UnrealTypes;

namespace UAssetAPI.UnrealTypes;

public struct FTwoVectors
{
    public FVector V1;
    public FVector V2;

    public FTwoVectors(FVector v1, FVector v2)
    {
        V1 = v1;
        V2 = v2;
    }

    public FTwoVectors(AssetBinaryReader reader)
    {
        V1 = new FVector(reader);
        V2 = new FVector(reader);
    }

    public int Write(AssetBinaryWriter writer)
    {
        var size = V1.Write(writer);
        size += V2.Write(writer);
        return size;
    }
}
