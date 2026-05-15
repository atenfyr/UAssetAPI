using Newtonsoft.Json;
using System;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Objects;

/// <summary>
/// Describes a property which UAssetAPI has no specific serialization for, and is instead represented as an array of bytes as a fallback.
/// </summary>
public class UnknownPropertyData : PropertyData<byte[]>
{
    [JsonProperty]
    public FString SerializingPropertyType = CurrentPropertyType;

    public UnknownPropertyData(FName name) : base(name) { }

    public UnknownPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("UnknownProperty");
    public override FString PropertyType { get { return CurrentPropertyType; } }

    public void SetSerializingPropertyType(FString newType)
    {
        SerializingPropertyType = newType;
    }

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = reader.ReadBytes((int)leng1);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        writer.Write(Value);
        return Value.Length;
    }

    public override string ToString()
    {
        return Convert.ToString(Value);
    }

    protected override void HandleCloned(PropertyData res)
    {
        UnknownPropertyData cloningProperty = (UnknownPropertyData)res;

        cloningProperty.SerializingPropertyType = (FString)SerializingPropertyType?.Clone();
    }
}
