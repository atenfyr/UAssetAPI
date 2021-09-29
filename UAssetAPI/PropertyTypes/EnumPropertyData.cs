using Newtonsoft.Json;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    /// <summary>
    /// Describes an enumeration value.
    /// </summary>
    public class EnumPropertyData : PropertyData<FName>
    {
        [JsonProperty]
        public FName EnumType;

        public EnumPropertyData(FName name) : base(name)
        {

        }

        public EnumPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("EnumProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                EnumType = reader.ReadFName();
                reader.ReadByte(); // null byte
            }
            Value = reader.ReadFName();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write(EnumType);
                writer.Write((byte)0);
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
                FName output = FName.FromString(d[0]);
                asset.AddNameReference(output.Value);
                EnumType = output;
            }
            else
            {
                EnumType = null;
            }

            if (d[1] != "null" && d[1] != null)
            {
                FName output = FName.FromString(d[1]);
                asset.AddNameReference(output.Value);
                Value = output;
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