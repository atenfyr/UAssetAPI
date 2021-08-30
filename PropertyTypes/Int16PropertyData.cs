using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class Int16PropertyData : PropertyData<short>
    {
        public Int16PropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "Int16Property";
        }

        public Int16PropertyData()
        {
            Type = "Int16Property";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadInt16();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Value);
            return sizeof(short);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = 0;
            if (short.TryParse(d[0], out short res)) Value = res;
        }
    }
}