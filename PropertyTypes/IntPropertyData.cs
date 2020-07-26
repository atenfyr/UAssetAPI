using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class IntPropertyData : PropertyData<int>
    {
        public IntPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "IntProperty";
        }

        public IntPropertyData()
        {
            Type = "IntProperty";
        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadInt32();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write(Value);
            return sizeof(int);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = 0;
            if (int.TryParse(d[0], out int res)) Value = res;
        }
    }
}
