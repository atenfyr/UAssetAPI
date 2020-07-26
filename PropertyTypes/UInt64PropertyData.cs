using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class UInt64PropertyData : PropertyData<ulong>
    {
        public UInt64PropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "UInt64Property";
        }

        public UInt64PropertyData()
        {
            Type = "UInt64Property";
        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadUInt64();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write(Value);
            return sizeof(ulong);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = 0;
            if (ulong.TryParse(d[0], out ulong res)) Value = res;
        }
    }
}