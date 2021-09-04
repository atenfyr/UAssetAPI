using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class IntPropertyData : PropertyData<int>
    {
        public IntPropertyData(string name, UAsset asset) : base(name, asset)
        {
            Type = "IntProperty";
        }

        public IntPropertyData()
        {
            Type = "IntProperty";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadInt32();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

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
