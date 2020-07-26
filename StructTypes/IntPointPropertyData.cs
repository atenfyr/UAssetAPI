using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class IntPointPropertyData : PropertyData<int[]> // X, Y
    {
        public IntPointPropertyData(string name, AssetReader asset, bool forceReadNull = false) : base(name, asset, forceReadNull)
        {
            Type = "IntPoint";
        }

        public IntPointPropertyData()
        {
            Type = "IntPoint";
        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = new int[2];
            for (int i = 0; i < 2; i++)
            {
                Value[i] = reader.ReadInt32();
            }
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            for (int i = 0; i < 2; i++)
            {
                writer.Write(Value[i]);
            }
            return 0;
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