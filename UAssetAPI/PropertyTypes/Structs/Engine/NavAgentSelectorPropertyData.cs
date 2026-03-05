using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public struct FNavAgentSelector : IStruct<FNavAgentSelector>
{
    public uint PackedBits;

    public FNavAgentSelector(uint packedBits)
    {
        PackedBits = packedBits;
    }

    public FNavAgentSelector(AssetBinaryReader reader)
    {
        PackedBits = reader.ReadUInt32();
    }

    public static FNavAgentSelector Read(AssetBinaryReader reader) => new FNavAgentSelector(reader);

    public int Write(AssetBinaryWriter writer)
    {
        writer.Write(PackedBits);
        return sizeof(uint);
    }

    public override string ToString()
    {
        return PackedBits.ToString();
    }

    public static FNavAgentSelector FromString(string[] d, UAsset asset)
    {
        uint.TryParse(d[0], out uint res);
        return new FNavAgentSelector(res);
    }
}

public class NavAgentSelectorPropertyData : BasePropertyData<FNavAgentSelector>
{
    public NavAgentSelectorPropertyData(FName name) : base(name) { }

    public NavAgentSelectorPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("NavAgentSelector");
    public override FString PropertyType => CurrentPropertyType;
}