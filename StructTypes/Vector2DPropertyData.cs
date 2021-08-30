using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class Vector2DPropertyData : PropertyData<float[]> // X, Y
    {
        public Vector2DPropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "Vector2D";
        }

        public Vector2DPropertyData()
        {
            Type = "Vector2D";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = new float[2];
            for (int i = 0; i < 2; i++)
            {
                Value[i] = reader.ReadSingle();
            }
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            for (int i = 0; i < 2; i++)
            {
                writer.Write(Value[i]);
            }
            return sizeof(float) * 2;
        }

        public override void FromString(string[] d)
        {
            Value = new float[2];
            if (float.TryParse(d[0], out float res1)) Value[0] = res1;
            if (float.TryParse(d[1], out float res2)) Value[1] = res2;
        }

        public override string ToString()
        {
            string oup = "(";
            for (int i = 0; i < Value.Length; i++)
            {
                oup += Convert.ToString(Value[i]) + ", ";
            }
            return oup.Remove(oup.Length - 2) + ")";
        }
    }
}