using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class FrameNumberPropertyData : PropertyData<FFrameNumber>
{
    public FrameNumberPropertyData(FName name) : base(name) { }

    public FrameNumberPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("FrameNumber");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FFrameNumber(reader.ReadInt32());
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        writer.Write(Value.Value);
        return sizeof(int);
    }

    public override void FromString(string[] d, UAsset asset)
    {
        if (int.TryParse(d[0], out int val)) Value = new FFrameNumber(val);
    }
}