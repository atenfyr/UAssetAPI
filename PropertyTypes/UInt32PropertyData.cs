using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class UInt32PropertyData : PropertyData<uint>
    {
        public UInt32PropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("UInt32Property");
        }

        public UInt32PropertyData()
        {
            Type = new FName("UInt32Property");
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadUInt32();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

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