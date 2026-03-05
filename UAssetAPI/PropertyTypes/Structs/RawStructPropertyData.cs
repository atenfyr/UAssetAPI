using Newtonsoft.Json;
using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class RawStructPropertyData : PropertyData<byte[]>
{
    [JsonProperty]
    public FName StructType = null;
    [JsonProperty]
    public bool SerializeNone = true;
    [JsonProperty]
    public Guid StructGUID = Guid.Empty; // usually set to 0

    public RawStructPropertyData(FName name) : base(name) { }

    public RawStructPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("RawStructProperty");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader && !reader.Asset.HasUnversionedProperties)
        {
            if (reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.PROPERTY_TAG_COMPLETE_TYPE_NAME)
            {
                if (PropertyTypeName is null) throw new FormatException("PropertyTypeName is required to read MapProperty with complete type names.");
                StructType = PropertyTypeName.GetParameter(0).GetName();
                //StructGUID
            }
            else
            {
                StructType = reader.ReadFName();
                if (reader.Asset.ObjectVersion >= ObjectVersion.VER_UE4_STRUCT_GUID_IN_PROPERTY_TAG) StructGUID = new Guid(reader.ReadBytes(16));
            }
            this.ReadEndPropertyTag(reader);
        }

        Value = reader.ReadBytes((int)leng1);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader && !writer.Asset.HasUnversionedProperties)
        {
            if (writer.Asset.ObjectVersionUE5 < ObjectVersionUE5.PROPERTY_TAG_COMPLETE_TYPE_NAME)
            {
                writer.Write(StructType);
                if (writer.Asset.ObjectVersion >= ObjectVersion.VER_UE4_STRUCT_GUID_IN_PROPERTY_TAG) writer.Write(StructGUID.ToByteArray());
            }
            this.WriteEndPropertyTag(writer);
        }

        writer.Write(Value);
        return Value.Length;
    }
}
