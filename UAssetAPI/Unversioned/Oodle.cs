using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace UAssetAPI
{
    public class Oodle
    {
        public const string OODLE_DOWNLOAD_LINK = "https://cdn.discordapp.com/attachments/817251677086285848/992648087371792404/oo2core_9_win64.dll";
        public const string OODLE_DLL_NAME = @"oo2core_9_win64.dll";

        [DllImport(OODLE_DLL_NAME)]
        private static extern int OodleLZ_Decompress(byte[] buffer, long bufferSize, byte[] outputBuffer, long outputBufferSize, uint a, uint b, ulong c, uint d, uint e, uint f, uint g, uint h, uint i, uint threadModule);

        public static byte[] Decompress(byte[] buffer, int size, int uncompressedSize)
        {
            if (!File.Exists(OODLE_DLL_NAME))
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile(OODLE_DOWNLOAD_LINK, OODLE_DLL_NAME);
                }
            }

            byte[] decompressedBuffer = new byte[uncompressedSize];
            int decompressedCount = OodleLZ_Decompress(buffer, size, decompressedBuffer, uncompressedSize, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3);

            if (decompressedCount == uncompressedSize)
            {
                return decompressedBuffer;
            }
            else if (decompressedCount < uncompressedSize)
            {
                return decompressedBuffer.Take(decompressedCount).ToArray();
            }

            return new byte[0];
        }
    }
}
