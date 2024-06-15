using Newtonsoft.Json;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /*
        The code within this file is modified from LongerWarrior's UEAssetToolkitGenerator project, which is licensed under the Apache License 2.0.
        Please see the NOTICE.md file distributed with UAssetAPI and UAssetGUI for more information.
    */

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

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
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

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
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
