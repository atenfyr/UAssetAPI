using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public abstract class TPerQualityLevelPropertyData<T> : PropertyData<TPerQualityLevel<T>>
{
    public TPerQualityLevelPropertyData(FName name) : base(name) { }

    public TPerQualityLevelPropertyData() { }
}

public class PerQualityLevelFloatPropertyData : TPerQualityLevelPropertyData<float>
{
    public PerQualityLevelFloatPropertyData(FName name) : base(name) { }

    public PerQualityLevelFloatPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("PerQualityLevelFloat");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new(reader, reader.ReadSingle);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        return Value.Write(writer, writer.Write);
    }
}

public class PerQualityLevelIntPropertyData : TPerQualityLevelPropertyData<int>
{
    public PerQualityLevelIntPropertyData(FName name) : base(name) { }

    public PerQualityLevelIntPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("PerQualityLevelInt");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new(reader, reader.ReadInt32);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        return Value.Write(writer, writer.Write);
    }
}