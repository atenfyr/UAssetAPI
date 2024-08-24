using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class MovieSceneDoubleChannelPropertyData : PropertyData<FMovieSceneDoubleChannel>
{
    public MovieSceneDoubleChannelPropertyData(FName name) : base(name) { }

    public MovieSceneDoubleChannelPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneDoubleChannel");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FMovieSceneDoubleChannel(reader);
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