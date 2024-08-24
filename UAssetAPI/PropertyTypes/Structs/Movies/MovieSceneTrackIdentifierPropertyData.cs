using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class MovieSceneTrackIdentifierPropertyData : PropertyData<uint>
{
    public MovieSceneTrackIdentifierPropertyData(FName name) : base(name) { }

    public MovieSceneTrackIdentifierPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneTrackIdentifier");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = reader.ReadUInt32();
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        writer.Write(Value);
        return sizeof(uint);
    }
}

public class MovieSceneSequenceIDPropertyData : PropertyData<uint>
{
    public MovieSceneSequenceIDPropertyData(FName name) : base(name) { }

    public MovieSceneSequenceIDPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneSequenceID");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = reader.ReadUInt32();
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        writer.Write(Value);
        return sizeof(uint);
    }
}

public class MovieSceneEvaluationKeyPropertyData : PropertyData<FMovieSceneEvaluationKey>
{
    public MovieSceneEvaluationKeyPropertyData(FName name) : base(name) { }

    public MovieSceneEvaluationKeyPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneEvaluationKey");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FMovieSceneEvaluationKey(reader);
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