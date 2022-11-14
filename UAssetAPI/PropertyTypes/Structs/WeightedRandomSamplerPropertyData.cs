using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    public class WeightedRandomSamplerPropertyData : PropertyData
    {
        [JsonProperty]
        public float[] Prob;
        [JsonProperty]
        public int[] Alias;
        [JsonProperty]
        public float TotalWeight;

        public WeightedRandomSamplerPropertyData(FName name) : base(name)
        {

        }

        public WeightedRandomSamplerPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("WeightedRandomSampler");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
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

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
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

        public override void FromString(string[] d, UAsset asset)
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

        protected override void HandleCloned(PropertyData res)
        {
            WeightedRandomSamplerPropertyData cloningProperty = (WeightedRandomSamplerPropertyData)res;
            cloningProperty.Prob = (float[])this.Prob.Clone();
            cloningProperty.Alias = (int[])this.Alias.Clone();
        }
    }
}