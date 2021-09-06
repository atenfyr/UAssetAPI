using System;
using System.Collections.Generic;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class PerPlatformBoolPropertyData : PropertyData<bool[]>
    {
        public PerPlatformBoolPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("PerPlatformBool");
        }

        public PerPlatformBoolPropertyData()
        {
            Type = new FName("PerPlatformBool");
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            int numEntries = reader.ReadInt32();
            Value = new bool[numEntries];
            for (int i = 0; i < numEntries; i++)
            {
                Value[i] = reader.ReadBoolean();
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
            return sizeof(bool) * Value.Length;
        }

        public override void FromString(string[] d)
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