using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class MovieSceneSegmentPropertyData : PropertyData<FMovieSceneSegment>
{
    public MovieSceneSegmentPropertyData(FName name) : base(name) { }
    
    public MovieSceneSegmentPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneSegment");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FMovieSceneSegment(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        return Value.Write(writer);
    }
}

public class MovieSceneSegmentIdentifierPropertyData : PropertyData<int>
{
    public MovieSceneSegmentIdentifierPropertyData(FName name) : base(name) { }

    public MovieSceneSegmentIdentifierPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneSegmentIdentifier");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

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
}
