using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
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
    }

    public class MovieSceneEventParametersPropertyData : PropertyData<FMovieSceneEventParameters>
    {


        public MovieSceneEventParametersPropertyData(FName name) : base(name)
        {

        }

        public MovieSceneEventParametersPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneEventParameters");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            FSoftObjectPath structref = new FSoftObjectPath(reader.ReadFName(), reader.ReadFString());
            int length = reader.ReadInt32();
            byte[] bytes = reader.ReadBytes(length);

            Value = new FMovieSceneEventParameters(structref, bytes); 

        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            int here = (int)writer.BaseStream.Position;

            writer.Write(Value.StructType.AssetPathName);
            writer.Write(Value.StructType.SubPathString);
            writer.Write(Value.StructBytes.Length);
            writer.Write(Value.StructBytes);

            return (int)writer.BaseStream.Position - here;
        }
    }
}
