using System;
using System.Collections.Generic;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class PerPlatformFloatPropertyData : PropertyData<float[]>
    {
        public PerPlatformFloatPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public PerPlatformFloatPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("PerPlatformFloat");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            int numEntries = reader.ReadInt32();
            Value = new float[numEntries];
            for (int i = 0; i < numEntries; i++)
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

            writer.Write(Value.Length);
            for (int i = 0; i < Value.Length; i++)
            {
                writer.Write(Value[i]);
            }
            return sizeof(float) * Value.Length;
        }

        public override void FromString(string[] d)
        {
            var valueList = new List<float>();
            if (float.TryParse(d[0], out float res1)) valueList.Add(res1);
            if (float.TryParse(d[1], out float res2)) valueList.Add(res2);
            if (float.TryParse(d[2], out float res3)) valueList.Add(res3);
            if (float.TryParse(d[3], out float res4)) valueList.Add(res4);
            Value = valueList.ToArray();
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