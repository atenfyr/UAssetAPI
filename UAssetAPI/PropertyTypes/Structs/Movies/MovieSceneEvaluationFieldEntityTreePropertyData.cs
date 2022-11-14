using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    public class MovieSceneEvaluationFieldEntityTreePropertyData : PropertyData<FMovieSceneEvaluationFieldEntityTree>
    {
        public MovieSceneEvaluationFieldEntityTreePropertyData(FName name) : base(name)
        {

        }

        public MovieSceneEvaluationFieldEntityTreePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneEvaluationFieldEntityTree");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FMovieSceneEvaluationFieldEntityTree().Read(reader);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }
            int here = (int)writer.BaseStream.Position;
            Value.Write(writer);

            return (int)writer.BaseStream.Position - here;
        }
    }

    public class MovieSceneSubSequenceTreePropertyData : PropertyData<FMovieSceneSubSequenceTree>
    {

        public MovieSceneSubSequenceTreePropertyData(FName name) : base(name)
        {

        }

        public MovieSceneSubSequenceTreePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneSubSequenceTree");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0) {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FMovieSceneSubSequenceTree().Read(reader);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader) {
            if (includeHeader) {
                writer.WritePropertyGuid(PropertyGuid);
            }
            int here = (int)writer.BaseStream.Position;
            Value.Write(writer);

            return (int)writer.BaseStream.Position - here;
        }
    }

    public class MovieSceneSequenceInstanceDataPtrPropertyData : PropertyData<FPackageIndex>
    {

        public MovieSceneSequenceInstanceDataPtrPropertyData(FName name) : base(name)
        {

        }

        public MovieSceneSequenceInstanceDataPtrPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneSequenceInstanceDataPtr");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }
            Value = reader.XFER_OBJECT_POINTER();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }
            int here = (int)writer.BaseStream.Position;
            writer.XFER_OBJECT_POINTER(Value);
            return (int)writer.BaseStream.Position - here;
        }
    }

    public class SectionEvaluationDataTreePropertyData : PropertyData<FSectionEvaluationDataTree>
    {

        public SectionEvaluationDataTreePropertyData(FName name) : base(name) {

        }

        public SectionEvaluationDataTreePropertyData() {

        }

        private static readonly FString CurrentPropertyType = new FString("SectionEvaluationDataTree");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FSectionEvaluationDataTree().Read(reader);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }
            int here = (int)writer.BaseStream.Position;
            Value.Write(writer);

            return (int)writer.BaseStream.Position - here;
        }
    }

    public class MovieSceneTrackFieldDataPropertyData : PropertyData<FMovieSceneTrackFieldData>
    {

        public MovieSceneTrackFieldDataPropertyData(FName name) : base(name)
        {

        }

        public MovieSceneTrackFieldDataPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneTrackFieldData");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FMovieSceneTrackFieldData().Read(reader);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader) {
                writer.WritePropertyGuid(PropertyGuid);
            }
            int here = (int)writer.BaseStream.Position;
            Value.Write(writer);

            return (int)writer.BaseStream.Position - here;
        }
    }
}