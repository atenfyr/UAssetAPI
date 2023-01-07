using Newtonsoft.Json;
using System.IO;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes an enumeration value.
    /// </summary>
    public class EnumPropertyData : PropertyData<FName>
    {
        [JsonProperty]
        public FName EnumType;
        /// <summary>
        /// Only used with unversioned properties.
        /// </summary>
        [JsonProperty]
        public FName InnerType;


        public EnumPropertyData(FName name) : base(name)
        {

        }

        public EnumPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("EnumProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (reader.Asset.HasUnversionedProperties)
            {
                bool hasTypeBeenFound = false;
                var schemaName = parentName.Value.Value;
                while (!hasTypeBeenFound && schemaName != null)
                {
                    var relevantSchema = reader.Asset.Mappings.Schemas[schemaName];
                    UsmapEnumData enumDat1 = relevantSchema.GetProperty(Name.Value.Value)?.PropertyData as UsmapEnumData;
                    if (enumDat1 != null)
                    {
                        EnumType = FName.DefineDummy(reader.Asset, enumDat1.Name);
                        InnerType = FName.DefineDummy(reader.Asset, enumDat1.InnerType.Type.ToString());
                        hasTypeBeenFound = true;
                    }
                    schemaName = relevantSchema.SuperType;
                }

                if (InnerType?.Value.Value == "ByteProperty")
                {
                    int enumIndice = (int)reader.ReadByte();
                    Value = FName.DefineDummy(reader.Asset, reader.Asset.Mappings.EnumMap[EnumType.Value.Value][enumIndice]);
                    return;
                }
            }

            if (includeHeader)
            {
                EnumType = reader.ReadFName();
                PropertyGuid = reader.ReadPropertyGuid();
            }
            Value = reader.ReadFName();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (writer.Asset.HasUnversionedProperties)
            {
                if (InnerType.Value.Value == "ByteProperty")
                {
                    // TODO: re-serialize as byte here
                    //writer.Write()
                    return sizeof(byte);
                }
            }

            if (includeHeader)
            {
                writer.Write(EnumType);
                writer.WritePropertyGuid(PropertyGuid);
            }
            writer.Write(Value);
            return sizeof(int) * 2;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override void FromString(string[] d, UAsset asset)
        {
            if (d[0] != "null" && d[0] != null)
            {
                EnumType = FName.FromString(asset, d[0]);
            }
            else
            {
                EnumType = null;
            }

            if (d[1] != "null" && d[1] != null)
            {
                Value = FName.FromString(asset, d[1]);
            }
            else
            {
                Value = null;
            }
        }

        protected override void HandleCloned(PropertyData res)
        {
            EnumPropertyData cloningProperty = (EnumPropertyData)res;
            cloningProperty.EnumType = (FName)this.EnumType?.Clone();
        }
    }
}