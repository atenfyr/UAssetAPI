using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class EnumPropertyData : PropertyData<string>
    {
        public string EnumType;

        public EnumPropertyData(string name, UAsset asset) : base(name, asset)
        {
            Type = "EnumProperty";
        }

        public EnumPropertyData()
        {
            Type = "EnumProperty";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                EnumType = Asset.GetNameReference((int)reader.ReadInt64());
                reader.ReadByte(); // null byte
            }
            Value = Asset.GetNameReference((int)reader.ReadInt64());
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((long)Asset.SearchNameReference(EnumType));
                writer.Write((byte)0);
            }
            writer.Write((long)Asset.SearchNameReference(Value));
            return sizeof(long);
        }

        public override string ToString()
        {
            return Value;
        }

        public override void FromString(string[] d)
        {
            Asset.AddNameReference(d[0]);
            Asset.AddNameReference(d[1]);
            EnumType = d[0];
            Value = d[1];
        }
    }
}