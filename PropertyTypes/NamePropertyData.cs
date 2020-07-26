using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class NamePropertyData : PropertyData<string>
    {
        public int Value2 = 0;

        public NamePropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "NameProperty";
        }

        public NamePropertyData()
        {
            Type = "NameProperty";
        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = Asset.GetHeaderReference(reader.ReadInt32());
            Value2 = reader.ReadInt32();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write((int)Asset.SearchHeaderReference(Value));
            writer.Write(Value2);
            return sizeof(int) * 2;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = d[0];
            Value2 = 0;
            if (d[1] != null && int.TryParse(d[1], out int res)) Value2 = res;
        }
    }
}