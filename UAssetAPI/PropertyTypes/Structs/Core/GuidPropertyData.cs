using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// Describes a 128-bit <see cref="Guid"/>.
/// </summary>
public class GuidPropertyData : PropertyData<Guid>
{
    public GuidPropertyData(FName name) : base(name) { }

    public GuidPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Guid");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new Guid(reader.ReadBytes(16));
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        writer.Write(Value.ToByteArray());
        return 16;
    }

    public override string ToString()
    {
        return Value.ConvertToString();
    }

    public override void FromString(string[] d, UAsset asset)
    {
        Value = d[0].ConvertToGUID();
    }

    protected override void HandleCloned(PropertyData res)
    {
        GuidPropertyData cloningProperty = (GuidPropertyData)res;

        cloningProperty.Value = new Guid(Value.ToByteArray());
    }
}