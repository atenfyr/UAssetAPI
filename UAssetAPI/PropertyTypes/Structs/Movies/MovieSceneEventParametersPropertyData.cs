using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public struct FMovieSceneEventParameters
{
    /** Soft object path to the type of this parameter payload */
    public FSoftObjectPath StructType;

    /** Serialized bytes that represent the payload. Serialized internally with FEventParameterArchive */
    public byte[] StructBytes;

    public FMovieSceneEventParameters(FSoftObjectPath structType, byte[] structBytes)
    {
        StructType = structType;
        StructBytes = structBytes;
    }

    public FMovieSceneEventParameters(AssetBinaryReader reader)
    {
        if (reader != null)
        {
            StructType = new FSoftObjectPath(reader);
            var length = reader.ReadInt32();
            StructBytes = reader.ReadBytes(length);
        }
    }

    public int Write(AssetBinaryWriter writer)
    {
        var size = StructType.Write(writer);
        writer.Write(StructBytes.Length);
        size += sizeof(int);
        writer.Write(StructBytes);
        size += StructBytes.Length;
        return size;
    }
}

public class MovieSceneEventParametersPropertyData : PropertyData<FMovieSceneEventParameters>
{
    public MovieSceneEventParametersPropertyData(FName name) : base(name) { }

    public MovieSceneEventParametersPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneEventParameters");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FMovieSceneEventParameters(reader); 
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
