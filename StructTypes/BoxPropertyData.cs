using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class BoxPropertyData : PropertyData<VectorPropertyData[]> // Min, Max, IsValid
    {
        public bool IsValid;

        public BoxPropertyData(string name, AssetReader asset, bool forceReadNull = false) : base(name, asset, forceReadNull)
        {
            Type = "Box";
        }

        public BoxPropertyData()
        {
            Type = "Box";
        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = new VectorPropertyData[2];
            for (int i = 0; i < 2; i++)
            {
                var next = new VectorPropertyData(Name, Asset, false);
                next.Read(reader, 0);
                Value[i] = next;
            }

            IsValid = reader.ReadBoolean();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            for (int i = 0; i < 2; i++)
            {
               Value[i].Write(writer);
            }
            writer.Write(IsValid);
            return 0;
        }

        public override void FromString(string[] d)
        {
            IsValid = d[0].Equals("1") || d[0].ToLower().Equals("true");
        }

        public override string ToString()
        {
            string oup = "(";
            for (int i = 0; i < Value.Length; i++)
            {
                oup += Value[i] + ", ";
            }
            return oup.Remove(oup.Length - 2) + ")";
        }
    }
}