using System.Text;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Objects;

/// <summary>
/// Describes a UTF8 string.
/// </summary>
public class Utf8StrPropertyData : PropertyData<FString>
{
    public Utf8StrPropertyData(FName name) : base(name) { }

    public Utf8StrPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Utf8StrProperty");
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = reader.ReadUtf8String();
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        int here = (int)writer.BaseStream.Position;
        writer.WriteUtf8String(Value);
        return (int)writer.BaseStream.Position - here;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public override void FromString(string[] d, UAsset asset)
    {
        Value = FString.FromString(d[0], Encoding.UTF8);
    }
}
