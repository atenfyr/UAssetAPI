using Newtonsoft.Json;
using System.Linq;
using UAssetAPI.UnrealTypes;
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

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (reader.Asset.HasUnversionedProperties)
            {
                if (reader.Asset.Mappings.TryGetPropertyData(Name, Ancestry, reader.Asset, out UsmapEnumData enumDat1))
                {
                    EnumType = FName.DefineDummy(reader.Asset, enumDat1.Name);
                    InnerType = FName.DefineDummy(reader.Asset, enumDat1.InnerType.Type.ToString());
                }

                if (InnerType?.Value.Value == "ByteProperty")
                {
                    int enumIndice = reader.ReadByte();
                    Value = enumIndice == byte.MaxValue ? null : FName.DefineDummy(reader.Asset, reader.Asset.Mappings.EnumMap[EnumType.Value.Value].Values[enumIndice]);
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
                if (InnerType?.Value?.Value == "ByteProperty")
                {
                    int enumIndice = Value == null ? byte.MaxValue : (byte)writer.Asset.Mappings.EnumMap[EnumType.Value.Value].Values.Where(pair => pair.Value == Value.Value.Value).Select(pair => pair.Key).FirstOrDefault(); // wow this code is stupid
                    writer.Write((byte)enumIndice);
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
                EnumType = asset.HasUnversionedProperties ? FName.DefineDummy(asset, d[0]) : FName.FromString(asset, d[0]);
            }
            else
            {
                EnumType = null;
            }

            if (d[1] != "null" && d[1] != null)
            {
                Value = asset.HasUnversionedProperties ? FName.DefineDummy(asset, d[1]) : FName.FromString(asset, d[1]);
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