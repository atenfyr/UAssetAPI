using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class KeyHandleMapPropertyData : PropertyData
{
    public KeyHandleMapPropertyData(FName name) : base(name) { }

    public KeyHandleMapPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("KeyHandleMap");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        // map is only saved to the transaction buffer, otherwise completely empty
        return;
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader && !writer.Asset.HasUnversionedProperties)
        {
            this.WriteEndPropertyTag(writer);
        }

        // map is only saved to the transaction buffer, otherwise completely empty
        return 0;
    }

    public override void FromString(string[] d, UAsset asset)
    {

    }
}
