using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class MovieSceneSubSectionFieldDataPropertyData : PropertyData<FMovieSceneSubSectionFieldData>
{
    public MovieSceneSubSectionFieldDataPropertyData(FName name) : base(name) { }

    public MovieSceneSubSectionFieldDataPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneSubSectionFieldData");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FMovieSceneSubSectionFieldData(reader);
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

public class MovieSceneEvaluationFieldEntityTreePropertyData : PropertyData<FMovieSceneEvaluationFieldEntityTree>
{
    public MovieSceneEvaluationFieldEntityTreePropertyData(FName name) : base(name) { }

    public MovieSceneEvaluationFieldEntityTreePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneEvaluationFieldEntityTree");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FMovieSceneEvaluationFieldEntityTree(reader);
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

public class MovieSceneSubSequenceTreePropertyData : PropertyData<FMovieSceneSubSequenceTree>
{
    public MovieSceneSubSequenceTreePropertyData(FName name) : base(name) { }

    public MovieSceneSubSequenceTreePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneSubSequenceTree");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FMovieSceneSubSequenceTree(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader) {
            this.WriteEndPropertyTag(writer);
        }

        return Value.Write(writer);
    }
}

public class MovieSceneSequenceInstanceDataPtrPropertyData : PropertyData<FPackageIndex>
{
    public MovieSceneSequenceInstanceDataPtrPropertyData(FName name) : base(name) { }

    public MovieSceneSequenceInstanceDataPtrPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneSequenceInstanceDataPtr");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }
        Value = new FPackageIndex(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        writer.Write(Value.Index);
        return sizeof(int);
    }
}

public class SectionEvaluationDataTreePropertyData : PropertyData<FSectionEvaluationDataTree>
{
    public SectionEvaluationDataTreePropertyData(FName name) : base(name) { }

    public SectionEvaluationDataTreePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("SectionEvaluationDataTree");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FSectionEvaluationDataTree(reader);
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

public class MovieSceneTrackFieldDataPropertyData : PropertyData<FMovieSceneTrackFieldData>
{
    public MovieSceneTrackFieldDataPropertyData(FName name) : base(name) { }

    public MovieSceneTrackFieldDataPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneTrackFieldData");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FMovieSceneTrackFieldData(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader) {
            this.WriteEndPropertyTag(writer);
        }

        return Value.Write(writer);
    }
}