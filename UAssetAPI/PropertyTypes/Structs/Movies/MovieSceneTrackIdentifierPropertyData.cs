using System;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs.Movies
{
    public class MovieSceneTrackIdentifierPropertyData : PropertyData<uint>
    {
        public MovieSceneTrackIdentifierPropertyData(FName name) : base(name)
        {

        }

        public MovieSceneTrackIdentifierPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneTrackIdentifier");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = reader.ReadUInt32();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value);
            return sizeof(uint);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = uint.Parse(d[0]);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}