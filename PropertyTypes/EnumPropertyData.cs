using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class EnumPropertyData : PropertyData<FName>
    {
        public FName EnumType;

        public EnumPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public EnumPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("EnumProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                EnumType = reader.ReadFName(Asset);
                reader.ReadByte(); // null byte
            }
            Value = reader.ReadFName(Asset);
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WriteFName(EnumType, Asset);
                writer.Write((byte)0);
            }
            writer.WriteFName(Value, Asset);
            return sizeof(int) * 2;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override void FromString(string[] d)
        {
            if (d[0] != "null" && d[0] != null)
            {
                FName output = FName.FromString(d[0]);
                Asset.AddNameReference(output.Value);
                EnumType = output;
            }
            else
            {
                EnumType = null;
            }

            if (d[1] != "null" && d[1] != null)
            {
                FName output = FName.FromString(d[1]);
                Asset.AddNameReference(output.Value);
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