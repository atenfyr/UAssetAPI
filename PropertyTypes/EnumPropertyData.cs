using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class EnumPropertyData : PropertyData<string>
    {
        public string FullEnum = string.Empty;

        public EnumPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "EnumProperty";
        }

        public EnumPropertyData()
        {
            Type = "EnumProperty";
        }

        public override void Read(BinaryReader reader, long leng)
        {
            Value = Asset.GetHeaderReference((int)reader.ReadInt64());
            if (Value.Contains("::") || Value.Equals("None")) return;
            if (ForceReadNull) reader.ReadByte(); // null byte
            FullEnum = Asset.GetHeaderReference((int)reader.ReadInt64());
        }

        public override int Write(BinaryWriter writer)
        {
            writer.Write((long)Asset.SearchHeaderReference(Value));
            if (Value.Contains("::") || Value.Equals("None")) return sizeof(long);
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write((long)Asset.SearchHeaderReference(FullEnum));
            return sizeof(long);
        }

        public string GetEnumBase()
        {
            return Value;
        }

        public string GetEnumFull()
        {
            return FullEnum;
        }

        public override string ToString()
        {
            if (Value.Contains("::") || Value.Equals("None")) return Value;
            return FullEnum;
        }

        public override void FromString(string[] d)
        {
            Asset.AddHeaderReference(d[0]);
            Asset.AddHeaderReference(d[1]);
            Value = d[0];
            FullEnum = d[1];
        }
    }
}