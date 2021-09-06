using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class MulticastDelegatePropertyData : PropertyData<int[]>
    {
        public FName Value2;

        public MulticastDelegatePropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("MulticastDelegateProperty");
        }

        public MulticastDelegatePropertyData()
        {
            Type = new FName("MulticastDelegateProperty");
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
            Value2 = reader.ReadFName(Asset);
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
            writer.WriteFName(Value2, Asset);
            return sizeof(int) * 4;
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

            Asset.AddNameReference(new FString(d[2]));
            Value2 = new FName(d[2]);
        }
    }
}