using System;
using System.Collections.Generic;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /// <summary>
    /// <see cref="FloatPropertyData"/> (<see cref="float"/>) property with per-platform overrides.
    /// </summary>
    public class PerPlatformFloatPropertyData : PropertyData<float[]>
    {
        public PerPlatformFloatPropertyData(FName name) : base(name)
        {

        }

        public PerPlatformFloatPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("PerPlatformFloat");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            int numEntries = reader.ReadInt32();
            Value = new float[numEntries];
            for (int i = 0; i < numEntries; i++)
            {
                Value[i] = reader.ReadSingle();
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value.Length);
            for (int i = 0; i < Value.Length; i++)
            {
                writer.Write(Value[i]);
            }
            return sizeof(int) + sizeof(float) * Value.Length;
        }

        public override void FromString(string[] d, UAsset asset)
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