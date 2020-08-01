using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class EnumPropertyData : PropertyData<string>
    {
        public string EnumType;

        public EnumPropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "EnumProperty";
        }

        public EnumPropertyData()
        {
            Type = "EnumProperty";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng)
        {
            if (includeHeader)
            {
                EnumType = Asset.GetHeaderReference((int)reader.ReadInt64());
                reader.ReadByte(); // null byte
            }
            Value = Asset.GetHeaderReference((int)reader.ReadInt64());
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((long)Asset.SearchHeaderReference(EnumType));
                writer.Write((byte)0);
            }
            writer.Write((long)Asset.SearchHeaderReference(Value));
            return sizeof(long);
        }

        public override string ToString()
        {
            return Value;
        }

        public override void FromString(string[] d)
        {
            Asset.AddHeaderReference(d[0]);
            Asset.AddHeaderReference(d[1]);
            EnumType = d[0];
            Value = d[1];
        }
    }
}