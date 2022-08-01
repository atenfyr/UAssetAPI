using System;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs.Movies
{
    /// <summary>
    /// Information about a single segment of an evaluation track
    /// </summary>
    public class MovieSceneSegmentPropertyData : PropertyData
    {
        /// <summary>
        /// The segment's range
        /// </summary>
        public Range<float> Range;

        public MovieSceneSegmentIdentifierPropertyData ID;

        /// <summary>
        /// Whether this segment has been generated yet or not
        /// </summary>
        public bool bAllowEmpty;

        /// <summary>
        /// Array of implementations that reside at the segment's range
        /// </summary>
        public StructPropertyData[] Impls;

        public MovieSceneSegmentPropertyData(FName name) : base(name)
        {

        }

        public MovieSceneSegmentPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneSegment");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        // Note: FFrameNumber is serialized as a single int32
        // TODO: doesn't seem to really match how these are actually serialized, double-check serialization
        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            if (reader.Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.FloatToIntConversion)
            {
                Range = new Range<float>(reader.ReadSingle(), reader.ReadSingle());
            }
            else
            {
                Range = new Range<float>(reader.ReadInt32(), reader.ReadInt32());
            }

            if (reader.Asset.GetCustomVersion<FSequencerObjectVersion>() >= FSequencerObjectVersion.EvaluationTree)
            {
                ID = new MovieSceneSegmentIdentifierPropertyData();
                ID.Offset = reader.BaseStream.Position;
                ID.Read(reader, false, 0);

                bAllowEmpty = reader.ReadBoolean();
            }

            int numStructs = reader.ReadInt32();
            Impls = new StructPropertyData[numStructs];
            for (int i = 0; i < numStructs; i++)
            {
                Impls[i] = new StructPropertyData(FName.DefineDummy(reader.Asset, "Impls"));
                Impls[i].Offset = reader.BaseStream.Position;
                Impls[i].StructType = FName.DefineDummy(reader.Asset, "SectionEvaluationData");
                Impls[i].Read(reader, false, 1);
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            int res = 0;
            if (writer.Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.FloatToIntConversion)
            {
                writer.Write((float)Range.LowerBound);
                writer.Write((float)Range.UpperBound);
                res += sizeof(float) * 2;
            }
            else
            {
                writer.Write((int)Range.LowerBound);
                writer.Write((int)Range.UpperBound);
                res += sizeof(int) * 2;
            }

            if (writer.Asset.GetCustomVersion<FSequencerObjectVersion>() >= FSequencerObjectVersion.EvaluationTree)
            {
                ID.Write(writer, false);

                writer.Write(bAllowEmpty);
            }

            writer.Write(Impls.Length);
            for (int i = 0; i < Impls.Length; i++)
            {
                Impls[i].Write(writer, false);
            }

            return res;
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Range = new Range<float>(float.Parse(d[0]), float.Parse(d[1]));
            ID = new MovieSceneSegmentIdentifierPropertyData()
            {
                Value = int.Parse(d[2])
            };
            bAllowEmpty = d[3].ToLowerInvariant() == "true" || d[3] == "1";
        }
    }
}