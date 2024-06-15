using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /*
        The code within this file is modified from LongerWarrior's UEAssetToolkitGenerator project, which is licensed under the Apache License 2.0.
        Please see the NOTICE.md file distributed with UAssetAPI and UAssetGUI for more information.
    */

    public class MovieSceneTrackIdentifierPropertyData : PropertyData<FMovieSceneTrackIdentifier> {
       

        public MovieSceneTrackIdentifierPropertyData(FName name) : base(name)
        {

        }

        public MovieSceneTrackIdentifierPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneTrackIdentifier");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FMovieSceneTrackIdentifier(reader.ReadUInt32());
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value.Value);
            return sizeof(uint);
        }
    }

    public class MovieSceneSequenceIDPropertyData : PropertyData<FMovieSceneSequenceID>
    {
        public MovieSceneSequenceIDPropertyData(FName name) : base(name) {

        }

        public MovieSceneSequenceIDPropertyData() {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneSequenceID");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FMovieSceneSequenceID(reader.ReadUInt32());
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value.Value);

            return sizeof(uint);
        }
    }

    public class MovieSceneEvaluationKeyPropertyData : PropertyData<FMovieSceneEvaluationKey>
    {
        public MovieSceneEvaluationKeyPropertyData(FName name) : base(name) {

        }

        public MovieSceneEvaluationKeyPropertyData() {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneEvaluationKey");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FMovieSceneEvaluationKey(reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32());

        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value.SequenceID.Value);
            writer.Write(Value.TrackIdentifier.Value);
            writer.Write(Value.SectionIndex);

            return 3 * sizeof(uint);
        }
    }
}