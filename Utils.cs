using System;
using System.IO;
using System.Text;

namespace UAssetAPI
{
    public static class Utils
    {
        public static string ReadUString(this BinaryReader reader)
        {
            int length = reader.ReadInt32();
            switch (length)
            {
                case 0:
                    return null;
                case 1:
                    return "";
                default:
                    if (length < 0)
                    {
                        throw new FormatException("Invalid string");
                    }
                    else
                    {
                        byte[] data = reader.ReadBytes(length);
                        return Encoding.UTF8.GetString(data, 0, data.Length - 1);
                    }
            }
        }

        public static string ReadUStringWithGUID(this BinaryReader reader, out int guid)
        {
            string str = reader.ReadUString();
            if (string.IsNullOrEmpty(str))
            {
                guid = reader.ReadInt32();
            }
            else
            {
                guid = 0;
            }
            return str;
        }

        public static void WriteUString(this BinaryWriter writer, string str)
        {
            writer.Write(str.Length + 1);
            writer.Write(Encoding.UTF8.GetBytes(str));
            writer.Write((byte)0);
        }

        public static int IndexToUIndex(int index)
        {
            return -(index + 1);
        }

        public static int UIndexToIndex(int uindex)
        {
            return -(uindex + 1);
        }
    }
}
