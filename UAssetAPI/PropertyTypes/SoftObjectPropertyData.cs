using Newtonsoft.Json;

namespace UAssetAPI.PropertyTypes
{
    /// <summary>
    /// Describes a reference variable to another object which may be null, and may become valid or invalid at any point. Synonym for <see cref="SoftObjectPropertyData"/>.
    /// </summary>
    public class AssetObjectPropertyData : PropertyData<FString>
    {
        [JsonProperty]
        public uint ID = 0;

        public AssetObjectPropertyData(FName name) : base(name)
        {

        }

        public AssetObjectPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("AssetObjectProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = reader.ReadFString();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            return writer.Write(Value);
        }

        public override string ToString()
        {
            return "(" + Value + ", " + ID + ")";
        }

        public override void FromString(string[] d, UAsset asset)
        {
            asset.AddNameReference(FString.FromString(d[0]));
            Value = FString.FromString(d[0]);
        }
    }

    /// <summary>
    /// Describes a reference variable to another object which may be null, and may become valid or invalid at any point. Synonym for <see cref="AssetObjectPropertyData"/>.
    /// </summary>
    public class SoftObjectPropertyData : PropertyData<FName>
    {
        [JsonProperty]
        public uint ID = 0;

        public SoftObjectPropertyData(FName name) : base(name)
        {

        }

        public SoftObjectPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("SoftObjectProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = reader.ReadFName();
            ID = reader.ReadUInt32();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value);
            writer.Write(ID);
            return sizeof(int) * 3;
        }

        public override string ToString()
        {
            return "(" + Value.ToString() + ", " + ID + ")";
        }

        public override void FromString(string[] d, UAsset asset)
        {
            FName output = FName.FromString(asset, d[0]);
            Value = output;

            if (uint.TryParse(d[1], out uint res2))
            {
                ID = res2;
            }
            else
            {
                ID = 0;
            }
        }
    }
}