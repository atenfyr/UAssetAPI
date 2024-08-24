using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class MovieSceneFloatChannelPropertyData : PropertyData<FMovieSceneFloatChannel>
{
    public MovieSceneFloatChannelPropertyData(FName name) : base(name) { }

    public MovieSceneFloatChannelPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneFloatChannel");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FMovieSceneFloatChannel(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        var offset = writer.BaseStream.Position;

        Value.Write(writer, writer.Write);

        return (int)(writer.BaseStream.Position - offset);
    }
}