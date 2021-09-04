using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class IntPointPropertyData : PropertyData<int[]> // X, Y
    {
        public IntPointPropertyData(string name, UAsset asset) : base(name, asset)
        {
            Type = "IntPoint";
        }

        public IntPointPropertyData()
        {
            Type = "IntPoint";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = new int[2];
            for (int i = 0; i < 2; i++)
            {
                Value[i] = reader.ReadInt32();
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
            return sizeof(int) * 2;
        }

        public override void FromString(string[] d)
        {
            Value = new int[2];
            if (int.TryParse(d[0], out int res1)) Value[0] = res1;
            if (int.TryParse(d[1], out int res2)) Value[1] = res2;
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