using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace UAssetAPI
{
    public static class UAPUtils
    {
        public static FName ReadFName(this BinaryReader reader, UAsset asset)
        {
            int nameMapPointer = reader.ReadInt32();
            int number = reader.ReadInt32();
            return new FName(asset.GetNameReference(nameMapPointer), number);
        }

        public static void WriteFName(this BinaryWriter writer, FName name, UAsset asset)
        {
            writer.Write(asset.SearchNameReference(name.Value));
            writer.Write(name.Number);
        }

        public static FString ReadFStringWithEncoding(this BinaryReader reader)
        {
            int length = reader.ReadInt32();
            switch (length)
            {
                case 0:
                    return null;
                default:
                    if (length < 0)
                    {
                        byte[] data = reader.ReadBytes(-length * 2);
                        return new FString(Encoding.Unicode.GetString(data, 0, data.Length - 2), Encoding.Unicode);
                    }
                    else
                    {
                        byte[] data = reader.ReadBytes(length);
                        return new FString(Encoding.ASCII.GetString(data, 0, data.Length - 1), Encoding.ASCII);
                    }
            }
        }

        public static string ReadFString(this BinaryReader reader)
        {
            return ReadFStringWithEncoding(reader)?.Value;
        }

        public static FString ReadNameMapString(this BinaryReader reader, out uint hashes)
        {
            FString str = reader.ReadFStringWithEncoding();
            if (!string.IsNullOrEmpty(str.Value))
            {
                hashes = reader.ReadUInt32();
            }
            else
            {
                hashes = 0;
            }
            return str;
        }

        public static int WriteFString(this BinaryWriter writer, string str, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.ASCII;

            switch (str)
            {
                case null:
                    writer.Write((int)0);
                    return 4;
                default:
                    string nullTerminatedStr = str + "\0";
                    writer.Write(encoding is UnicodeEncoding ? -nullTerminatedStr.Length : nullTerminatedStr.Length);
                    byte[] actualStrData = encoding.GetBytes(nullTerminatedStr);
                    writer.Write(actualStrData);
                    return actualStrData.Length + 4;
            }
        }

        public static int WriteFString(this BinaryWriter writer, FString str)
        {
            return WriteFString(writer, str?.Value, str?.Encoding);
        }

        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static int GetImportIndex(int index)
        {
            return -(index + 1);
        }

        public static int GetNormalIndex(int index)
        {
            return -(index + 1);
        }

        public static FString GetImportNameReferenceWithoutZero(int j, UAsset asset)
        {
            FString refer = asset.GetImportObjectName(j).Value;
            if (!asset.NameReferenceContains(refer)) return refer;
            return asset.GetNameReferenceWithoutZero(asset.SearchNameReference(refer));
        }

        public static uint[] InterpretAsGuidAndConvertToUnsignedInts(this string value)
        {
            Guid.TryParse(value.Trim(), out Guid res);
            return res.ToUnsignedInts();
        }

        public static uint[] ToUnsignedInts(this Guid value)
        {
            byte[] vals = value.ToByteArray();
            uint[] res = new uint[4];
            res[0] = BitConverter.ToUInt32(vals, 0);
            res[1] = BitConverter.ToUInt32(vals, sizeof(uint));
            res[2] = BitConverter.ToUInt32(vals, sizeof(uint) * 2);
            res[3] = BitConverter.ToUInt32(vals, sizeof(uint) * 3);
            return res;
        }

        public static Guid GUID(uint value1, uint value2, uint value3, uint value4)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value1).CopyTo(bytes, 0);
            BitConverter.GetBytes(value2).CopyTo(bytes, sizeof(uint));
            BitConverter.GetBytes(value3).CopyTo(bytes, sizeof(uint) * 2);
            BitConverter.GetBytes(value4).CopyTo(bytes, sizeof(uint) * 3);
            return new Guid(bytes);
        }
    }
}
