using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class UInt32PropertyData : PropertyData<uint>
    {
        public UInt32PropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "UInt32Property";
        }

        public UInt32PropertyData()
        {
            Type = "UInt32Property";
        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadUInt32();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write(Value);
            return sizeof(uint);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = 0;
            if (uint.TryParse(d[0], out uint res)) Value = res;
        }
    }
}