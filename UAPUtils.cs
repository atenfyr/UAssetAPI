using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace UAssetAPI
{
    public static class UAPUtils
    {
        public static UString ReadUStringWithEncoding(this BinaryReader reader)
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
                        return new UString(Encoding.Unicode.GetString(data, 0, data.Length - 2), Encoding.Unicode);
                    }
                    else
                    {
                        byte[] data = reader.ReadBytes(length);
                        return new UString(Encoding.ASCII.GetString(data, 0, data.Length - 1), Encoding.ASCII);
                    }
            }
        }

        public static string ReadUString(this BinaryReader reader)
        {
            return ReadUStringWithEncoding(reader)?.Value;
        }

        public static string ReadUStringWithGUID(this BinaryReader reader, out uint guid)
        {
            string str = reader.ReadUString();
            if (!string.IsNullOrEmpty(str))
            {
                guid = reader.ReadUInt32();
            }
            else
            {
                guid = 0;
            }
            return str;
        }

        public static UString ReadUStringWithGUIDAndEncoding(this BinaryReader reader, out uint guid)
        {
            UString str = reader.ReadUStringWithEncoding();
            if (!string.IsNullOrEmpty(str.Value))
            {
                guid = reader.ReadUInt32();
            }
            else
            {
                guid = 0;
            }
            return str;
        }

        public static void WriteUString(this BinaryWriter writer, string str, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.ASCII;

            switch (str)
            {
                case null:
                    writer.Write((int)0);
                    break;
                default:
                    string nullTerminatedStr = str + "\0";
                    writer.Write(encoding is UnicodeEncoding ? -nullTerminatedStr.Length : nullTerminatedStr.Length);
                    writer.Write(encoding.GetBytes(nullTerminatedStr));
                    break;
            }
        }

        public static void WriteUString(this BinaryWriter writer, UString str)
        {
            WriteUString(writer, str?.Value, str?.Encoding);
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

        public static string GetImportNameReferenceWithoutZero(int j, UAsset asset)
        {
            string refer = asset.GetImportReference(j);
            if (!asset.NameReferenceContains(refer)) return refer;
            return asset.GetNameReferenceWithoutZero(asset.SearchNameReference(refer));
        }
    }
}
