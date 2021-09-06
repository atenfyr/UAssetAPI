using System;
using System.Diagnostics;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class NamePropertyData : PropertyData<FName>
    {
        public NamePropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("NameProperty");
        }

        public NamePropertyData()
        {
            Type = new FName("NameProperty");
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadFName(Asset);
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.WriteFName(Value, Asset);
            return sizeof(int) * 2;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            int.TryParse(d[1], out int number);
            Value = new FName(d[0], number);
        }
    }
}