using Newtonsoft.Json;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    public struct FInt32RangeBound
    {
        public ERangeBoundTypes Type; // 0x00(0x01)
        public int Value; // 0x04(0x04)
    }

    public class MovieSceneFrameRangePropertyData : PropertyData
    {
        [JsonProperty]
        public FInt32RangeBound LowerBound;
        [JsonProperty]
        public FInt32RangeBound UpperBound;

        public MovieSceneFrameRangePropertyData(FName name) : base(name)
        {

        }

        public MovieSceneFrameRangePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneFrameRange");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            LowerBound.Type = (ERangeBoundTypes)reader.ReadSByte();
            LowerBound.Value = reader.ReadInt32();

            UpperBound.Type = (ERangeBoundTypes)reader.ReadSByte();
            UpperBound.Value = reader.ReadInt32();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write((sbyte)LowerBound.Type);
            writer.Write(LowerBound.Value);
            writer.Write((sbyte)UpperBound.Type);
            writer.Write(UpperBound.Value);

            return sizeof(float) * 2 + sizeof(sbyte) * 2;
        }
    }
}
