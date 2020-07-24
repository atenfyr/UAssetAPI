using System;
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
                case 1:
                    return new UString("", Encoding.UTF8);
                default:
                    if (length < 0)
                    {
                        byte[] data = reader.ReadBytes(-length * 2);
                        return new UString(Encoding.Unicode.GetString(data, 0, data.Length - 2), Encoding.Unicode);
                    }
                    else
                    {
                        byte[] data = reader.ReadBytes(length);
                        return new UString(Encoding.UTF8.GetString(data, 0, data.Length - 1), Encoding.UTF8);
                    }
            }
        }

        public static string ReadUString(this BinaryReader reader)
        {
            return ReadUStringWithEncoding(reader).Value;
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

        public static void WriteUString(this BinaryWriter writer, string str, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;

            int realLen = str.Length + 1;
            if (encoding.Equals(Encoding.Unicode)) realLen = -realLen;

            writer.Write(realLen);
            writer.Write(encoding.GetBytes(str));
            for (int k = 0; k < encoding.GetByteCount(new char[] { 'a' }); k++)
            {
                writer.Write((byte)0);
            }
        }

        public static void WriteUString(this BinaryWriter writer, UString str)
        {
            WriteUString(writer, str.Value, str.Encoding);
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
