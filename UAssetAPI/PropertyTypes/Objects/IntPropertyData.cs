using System;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Objects;

/// <summary>
/// Describes a 32-bit signed integer variable (<see cref="int"/>).
/// </summary>
public class IntPropertyData : PropertyData<int>
{
    public IntPropertyData(FName name) : base(name) { }

    public IntPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("IntProperty");
    public override FString PropertyType => CurrentPropertyType;
    public override object DefaultValue => (int)0;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = reader.ReadInt32();
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        writer.Write(Value);
        return sizeof(int);
    }

    public override string ToString()
    {
        return Convert.ToString(Value);
    }

    public override void FromString(string[] d, UAsset asset)
    {
        Value = 0;
        if (int.TryParse(d[0], out int res)) Value = res;
    }
}
