using UAssetAPI.PropertyTypes.Objects;

namespace UAssetAPI.UnrealTypes;

public struct FTwoVectors : IStruct<FTwoVectors>
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

    public static FTwoVectors Read(AssetBinaryReader reader) => new FTwoVectors(reader);

    public override string ToString()
    {
        return $"({V1}, {V2})";
    }

    public static FTwoVectors FromString(string[] d, UAsset asset)
    {
        throw new System.NotImplementedException();
    }
}
