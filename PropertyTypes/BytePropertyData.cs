using System;
using System.Diagnostics;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public enum BytePropertyType
    {
        Byte,
        Long,
    }

    public class BytePropertyData : PropertyData<int>
    {
        public BytePropertyType ByteType;
        public int EnumType = 0;

        public BytePropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "ByteProperty";
        }

        public BytePropertyData()
        {
            Type = "ByteProperty";
        }

        // TODO: Use the leng parameter to determine the type of ByteProperty, we aren't going to get anywhere with anything else
        public override void Read(BinaryReader reader, bool includeHeader, long leng)
        {
            if (includeHeader)
            {
                EnumType = (int)reader.ReadInt64();
                reader.ReadByte(); // null byte
            }

            switch (leng)
            {
                case 1:
                    ByteType = BytePropertyType.Byte;
                    Value = (int)reader.ReadByte();
                    break;
                case 8:
                    ByteType = BytePropertyType.Long;
                    Value = (int)reader.ReadInt64();
                    break;
                default:
                    throw new FormatException("Invalid length " + leng + " for ByteProperty");
            }
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((long)EnumType);
                writer.Write((byte)0);
            }

            switch (ByteType)
            {
                case BytePropertyType.Byte:
                    writer.Write((byte)Value);
                    return 1;
                case BytePropertyType.Long:
                    writer.Write((long)Value);
                    return 8;
                default:
                    throw new FormatException("Invalid BytePropertyType " + ByteType);
            }
        }

        public string GetEnumBase()
        {
            if (EnumType <= 0) return "null";
            return Asset.GetHeaderReference(EnumType);
        }

        public string GetEnumFull()
        {
            if (Value <= 0) return "null";
            return Asset.GetHeaderReference(Value);
        }

        public override string ToString()
        {
            if (ByteType == BytePropertyType.Byte) return Convert.ToString(Value);
            return GetEnumFull();
        }

        public override void FromString(string[] d)
        {
            EnumType = Asset.AddHeaderReference(d[0]);
            if (byte.TryParse(d[1], out byte res))
            {
                ByteType = BytePropertyType.Byte;
                Value = res;
            }
            else
            {
                ByteType = BytePropertyType.Long;
                Value = Asset.AddHeaderReference(d[1]);
            }
        }
    }
}