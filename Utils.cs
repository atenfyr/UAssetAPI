using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace UAssetAPI
{
    public static class Utils
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
                    int realLen = str.Length + 1;
                    if (encoding.Equals(Encoding.Unicode)) realLen = -realLen;

                    byte[] orig = encoding.GetBytes(str);
                    byte[] finalResult = new byte[orig.Length + 1 + (realLen >= 0 ? 0 : 1)];
                    Buffer.BlockCopy(orig, 0, finalResult, 0, orig.Length);

                    writer.Write(realLen);
                    writer.Write(finalResult);

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

        public static int GetLinkIndex(int index)
        {
            return -(index + 1);
        }

        public static int GetNormalIndex(int index)
        {
            return -(index + 1);
        }
    }
}
