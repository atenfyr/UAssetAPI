using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class FloatPropertyData : PropertyData<float>
    {
        public FloatPropertyData(string name, UAsset asset) : base(name, asset)
        {
            Type = "FloatProperty";
        }

        public FloatPropertyData()
        {
            Type = "FloatProperty";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadSingle();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Value);
            return sizeof(float);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = 0;
            if (float.TryParse(d[0], out float res)) Value = res;
        }
    }
}