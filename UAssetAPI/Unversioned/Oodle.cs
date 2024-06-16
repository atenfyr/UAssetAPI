using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace UAssetAPI.Unversioned
{
    public class Oodle
    {
        // oodle download link is broken
        public const string OODLE_DOWNLOAD_LINK = "https://cdn.discordapp.com/attachments/817251677086285848/992648087371792404/oo2core_9_win64.dll";
        public const string OODLE_DLL_NAME = @"oo2core_9_win64.dll";
        private static Regex RemoveFilePrefixRegex = new Regex(@"^file:?[\\\/]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        [DllImport(OODLE_DLL_NAME)]
        private static extern int OodleLZ_Decompress(byte[] buffer, long bufferSize, byte[] outputBuffer, long outputBufferSize, uint a, uint b, ulong c, uint d, uint e, uint f, uint g, uint h, uint i, uint threadModule);

        public static byte[] Decompress(byte[] buffer, int size, int uncompressedSize)
        {
            throw new NotImplementedException("Oodle decompression is no longer supported");

            /*var targetPath = Path.Combine(Path.GetDirectoryName(RemoveFilePrefixRegex.Replace(Assembly.GetAssembly(typeof(Oodle)).CodeBase, string.Empty)), OODLE_DLL_NAME);
            if (!File.Exists(targetPath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(targetPath));
                using (var client = new WebClient())
                {
                    client.DownloadFile(OODLE_DOWNLOAD_LINK, targetPath);
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

            return new byte[0];*/
        }
    }
}
