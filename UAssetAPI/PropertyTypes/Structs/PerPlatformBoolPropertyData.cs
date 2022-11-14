using System;
using System.Collections.Generic;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /// <summary>
    /// <see cref="BoolPropertyData"/> (<see cref="bool"/>) property with per-platform overrides.
    /// </summary>
    public class PerPlatformBoolPropertyData : PropertyData<bool[]>
    {
        public PerPlatformBoolPropertyData(FName name) : base(name)
        {

        }

        public PerPlatformBoolPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("PerPlatformBool");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            int numEntries = reader.ReadInt32();
            Value = new bool[numEntries];
            for (int i = 0; i < numEntries; i++)
            {
                Value[i] = reader.ReadBoolean();
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
            return sizeof(int) + sizeof(bool) * Value.Length;
        }

        public override void FromString(string[] d, UAsset asset)
        {
            var valueList = new List<bool>();
            if (bool.TryParse(d[0], out bool res1)) valueList.Add(res1);
            if (bool.TryParse(d[1], out bool res2)) valueList.Add(res2);
            if (bool.TryParse(d[2], out bool res3)) valueList.Add(res3);
            if (bool.TryParse(d[3], out bool res4)) valueList.Add(res4);
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