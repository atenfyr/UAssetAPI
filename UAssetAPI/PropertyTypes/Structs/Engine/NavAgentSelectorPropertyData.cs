using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public struct FNavAgentSelector(uint packedBits)
{
    public uint PackedBits = packedBits;
}

public class NavAgentSelectorPropertyData : PropertyData<FNavAgentSelector>
{
    public NavAgentSelectorPropertyData(FName name) : base(name) { }

    public NavAgentSelectorPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("NavAgentSelector");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FNavAgentSelector(reader.ReadUInt32()); 
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        writer.Write(Value.PackedBits);
        return sizeof(uint);
    }

    public override void FromString(string[] d, UAsset asset)
    {
        if (uint.TryParse(d[0], out uint res)) Value = new FNavAgentSelector(res);
    }

    public override string ToString()
    {
        return Value.PackedBits.ToString();
    }
}