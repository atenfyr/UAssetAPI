using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class WeightedRandomSamplerPropertyData : PropertyData
    {
        public float[] Prob;
        public int[] Alias;
        public float TotalWeight;

        public WeightedRandomSamplerPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public WeightedRandomSamplerPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("WeightedRandomSampler");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            int size = reader.ReadInt32();
            Prob = new float[size];
            for (int i = 0; i < size; i++)
            {
                Prob[i] = reader.ReadSingle();
            }

            size = reader.ReadInt32();
            Alias = new int[size];
            for (int i = 0; i < size; i++)
            {
                Alias[i] = reader.ReadInt32();
            }

            TotalWeight = reader.ReadSingle();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Prob.Length);
            for (int i = 0; i < Prob.Length; i++)
            {
                writer.Write(Prob[i]);
            }

            writer.Write(Alias.Length);
            for (int i = 0; i < Alias.Length; i++)
            {
                writer.Write(Alias[i]);
            }

            writer.Write(TotalWeight);

            return sizeof(int) + sizeof(float) * Prob.Length + sizeof(int) + sizeof(int) * Alias.Length + sizeof(float);
        }

        public override void FromString(string[] d)
        {

        }

        public override string ToString()
        {
            string oup = "(";

            oup += "(";
            for (int i = 0; i < Prob.Length; i++)
            {
                oup += Convert.ToString(Prob[i]) + ", ";
            }
            oup = oup.Remove(oup.Length - 2) + ")";

            oup += "(";
            for (int i = 0; i < Alias.Length; i++)
            {
                oup += Convert.ToString(Alias[i]) + ", ";
            }
            oup = oup.Remove(oup.Length - 2) + ")";

            oup += ", " + TotalWeight + ")";

            return oup;
        }
    }
}