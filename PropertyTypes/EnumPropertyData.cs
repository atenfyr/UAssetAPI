using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class EnumPropertyData : PropertyData<FName>
    {
        public FName EnumType;

        public EnumPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("EnumProperty");
        }

        public EnumPropertyData()
        {
            Type = new FName("EnumProperty");
        }

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
                Asset.AddNameReference(new FString(d[0]));
                EnumType = new FName(d[0]);
            }
            else
            {
                EnumType = null;
            }

            if (d[1] != "null" && d[1] != null)
            {
                Asset.AddNameReference(new FString(d[1]));
                Value = new FName(d[1]);
            }
            else
            {
                Value = null;
            }
        }
    }
}