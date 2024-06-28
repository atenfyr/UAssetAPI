using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class MovieSceneFrameRangePropertyData : PropertyData<TRange<FFrameNumber>>
{
    public MovieSceneFrameRangePropertyData(FName name) : base(name) { }

    public MovieSceneFrameRangePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneFrameRange");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            PropertyGuid = reader.ReadPropertyGuid();
        }

        Value = new(reader, () => new FFrameNumber(reader));
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            writer.WritePropertyGuid(PropertyGuid);
        }

        var offset = writer.BaseStream.Position;

        Value.Write(writer, frame => frame.Write(writer));

        return (int)(writer.BaseStream.Position - offset);
    }
}