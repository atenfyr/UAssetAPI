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

    /// <summary>
    /// Describes a byte or an enumeration value.
    /// </summary>
    public class BytePropertyData : PropertyData<int>
    {
        public BytePropertyType ByteType;
        public int EnumType = 0;

        public BytePropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public BytePropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("ByteProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            ReadCustom(reader, includeHeader, leng1, leng2, true);
        }

        private void ReadCustom(BinaryReader reader, bool includeHeader, long leng1, long leng2, bool canRepeat)
        {
            if (includeHeader)
            {
                EnumType = (int)reader.ReadInt64();
                reader.ReadByte(); // null byte
            }

            switch (leng1)
            {
                case 1:
                    ByteType = BytePropertyType.Byte;
                    Value = (int)reader.ReadByte();
                    break;
                case 0: // Should be only seen in maps
                case 8:
                    ByteType = BytePropertyType.Long;
                    Value = (int)reader.ReadInt64();
                    break;
                default:
                    if (canRepeat)
                    {
                        ReadCustom(reader, false, leng2, 0, false);
                        return;
                    }
                    throw new FormatException("Invalid length " + leng1 + " for ByteProperty");
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

        public FString GetEnumBase()
        {
            if (EnumType <= 0) return new FString("null");
            return Asset.GetNameReference(EnumType);
        }

        public FString GetEnumFull()
        {
            if (Value <= 0) return new FString("null");
            return Asset.GetNameReference(Value);
        }

        public override string ToString()
        {
            if (ByteType == BytePropertyType.Byte) return Convert.ToString(Value);
            return GetEnumFull().Value;
        }

        public override void FromString(string[] d)
        {
            EnumType = Asset.AddNameReference(new FString(d[0]));
            if (byte.TryParse(d[1], out byte res))
            {
                ByteType = BytePropertyType.Byte;
                Value = res;
            }
            else
            {
                ByteType = BytePropertyType.Long;
                Value = Asset.AddNameReference(new FString(d[1]));
            }
        }
    }
}