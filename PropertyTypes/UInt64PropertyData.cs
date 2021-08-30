using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class UInt64PropertyData : PropertyData<ulong>
    {
        public UInt64PropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "UInt64Property";
        }

        public UInt64PropertyData()
        {
            Type = "UInt64Property";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadUInt64();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

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