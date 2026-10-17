using Newtonsoft.Json;
using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// Preserves the native serialization header of an empty FInstancedPropertyBag.
/// Non-empty bags have a version-dependent descriptor table and payload and are
/// deliberately rejected until that format is implemented.
/// </summary>
public class InstancedPropertyBagPropertyData : PropertyData<int>
{
    [JsonProperty]
    public int RawHasData;

    public InstancedPropertyBagPropertyData(FName name) : base(name) { }
    public InstancedPropertyBagPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("InstancedPropertyBag");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0,
        PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            ReadEndPropertyTag(reader);
        }

        RawHasData = reader.ReadInt32();
        Value = RawHasData;
        if (RawHasData != 0)
        {
            throw new NotSupportedException("Non-empty InstancedPropertyBag native serialization is not implemented");
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader,
        PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            WriteEndPropertyTag(writer);
        }

        writer.Write(RawHasData);
        return sizeof(int);
    }
}
