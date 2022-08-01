using System;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs.Movies
{
    /// <summary>
    /// A unique identifier for a segment within a FMovieSceneEvaluationTrackSegments container (IdentifierIndex)
    /// </summary>
    public class MovieSceneSegmentIdentifierPropertyData : PropertyData<int>
    {
        public MovieSceneSegmentIdentifierPropertyData(FName name) : base(name)
        {

        }

        public MovieSceneSegmentIdentifierPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneSegmentIdentifier");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = reader.ReadInt32();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value);
            return sizeof(int);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = int.Parse(d[0]);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}