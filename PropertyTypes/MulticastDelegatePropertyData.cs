using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class MulticastDelegatePropertyData : PropertyData<int[]>
    {
        public string Value2;

        public MulticastDelegatePropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "MulticastDelegateProperty";
        }

        public MulticastDelegatePropertyData()
        {
            Type = "MulticastDelegateProperty";
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
            Value2 = Asset.GetHeaderReference((int)reader.ReadUInt64());
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
            writer.Write((long)Asset.SearchHeaderReference(Value2));
            return (sizeof(int) * 2) + sizeof(long);
        }

        public override string ToString()
        {
            string oup = "(";
            for (int i = 0; i < Value.Length; i++)
            {
                oup += Convert.ToString(Value[i]) + ", ";
            }
            oup += Value2;
            return oup + ")";
        }

        public override void FromString(string[] d)
        {
            Value = new int[] { 0, 0 };
            if (int.TryParse(d[0], out int res)) Value[0] = res;
            if (int.TryParse(d[1], out int res2)) Value[1] = res2;

            Asset.AddHeaderReference(d[2]);
            Value2 = d[2];
        }
    }
}