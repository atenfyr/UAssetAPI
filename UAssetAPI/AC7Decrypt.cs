using System;
using System.Diagnostics;
using System.IO;

namespace UAssetAPI
{
    public class AC7XorKey
    {
        public int NameKey;
        public int Offset;
        public int pk1;
        public int pk2;

        public void SkipCount(int count)
        {
            int num = count % 217;
            pk1 += num;
            if (pk1 >= 217)
            {
                pk1 -= 217;
            }
            int num2 = count % 1024;
            pk2 += num2;
            if (pk2 >= 1024)
            {
                pk2 -= 1024;
            }
        }

        public AC7XorKey(int nameKey, int offset)
        {
            NameKey = nameKey;
            Offset = offset;
        }
    }

    public class AC7Decrypt
    {
        private static byte[] AC7FullKey = new byte[0];

        public AC7Decrypt()
        {
            if (AC7FullKey.Length == 0) AC7FullKey = Properties.Resources.AC7Key;
        }

        /// <summary>
        /// Decrypts an Ace Combat 7 encrypted asset on disk.
        /// </summary>
        /// <param name="input">The path to an encrypted asset on disk.</param>
        /// <param name="output">The path that the decrypted asset should be saved to.</param>
        public void Decrypt(string input, string output)
        {
            AC7XorKey xorKey = GetXorKey(Path.GetFileNameWithoutExtension(input));
            byte[] doneData = DecryptUAssetBytes(File.ReadAllBytes(input), xorKey);
            File.WriteAllBytes(output, doneData);
            try
            {
                byte[] doneData2 = DecryptUexpBytes(File.ReadAllBytes(Path.ChangeExtension(input, "uexp")), xorKey);
                File.WriteAllBytes(Path.ChangeExtension(output, "uexp"), doneData2);
            }
            catch { }
        }

        /// <summary>
        /// Encrypts an Ace Combat 7 encrypted asset on disk.
        /// </summary>
        /// <param name="input">The path to a decrypted asset on disk.</param>
        /// <param name="output">The path that the encrypted asset should be saved to.</param>
        public void Encrypt(string input, string output)
        {
            AC7XorKey xorKey = GetXorKey(Path.GetFileNameWithoutExtension(output));
            byte[] doneData = EncryptUAssetBytes(File.ReadAllBytes(input), xorKey);
            File.WriteAllBytes(output, doneData);
            try
            {
                byte[] doneData2 = EncryptUexpBytes(File.ReadAllBytes(Path.ChangeExtension(input, "uexp")), xorKey);
                File.WriteAllBytes(Path.ChangeExtension(output, "uexp"), doneData2);
            }
            catch { }
        }

        public byte[] DecryptUAssetBytes(byte[] uasset, AC7XorKey xorkey)
        {
            if (xorkey == null) throw new NullReferenceException("Null key provided");
            byte[] array = new byte[uasset.Length];
            BitConverter.GetBytes(UAsset.UASSET_MAGIC).CopyTo(array, 0);
            for (int i = 4; i < array.Length; i++)
            {
                array[i] = GetXorByte(uasset[i], ref xorkey);
            }
            return array;
        }

        public byte[] EncryptUAssetBytes(byte[] uasset, AC7XorKey xorkey)
        {
            if (xorkey == null) throw new NullReferenceException("Null key provided");
            byte[] array = new byte[uasset.Length];
            BitConverter.GetBytes(UAsset.ACE7_MAGIC).CopyTo(array, 0);
            for (int i = 4; i < array.Length; i++)
            {
                array[i] = GetXorByte(uasset[i], ref xorkey);
            }
            return array;
        }

        public byte[] DecryptUexpBytes(byte[] uexp, AC7XorKey xorkey)
        {
            if (xorkey == null) throw new NullReferenceException("Null key provided");
            byte[] array = new byte[uexp.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = GetXorByte(uexp[i], ref xorkey);
            }
            BitConverter.GetBytes(UAsset.UASSET_MAGIC).CopyTo(array, array.Length - 4);
            return array;
        }

        public byte[] EncryptUexpBytes(byte[] uexp, AC7XorKey xorkey)
        {
            if (xorkey == null) throw new NullReferenceException("Null key provided");
            byte[] array = new byte[uexp.Length];
            for (int i = 0; i < uexp.Length; i++)
            {
                array[i] = GetXorByte(uexp[i], ref xorkey);
            }
            return array;
        }

        /// <summary>
        /// Generates an encryption key for a particular asset on disk.
        /// </summary>
        /// <param name="fname">The name of the asset being encrypted on disk without the extension.</param>
        /// <returns>An encryption key for the asset.</returns>
        public static AC7XorKey GetXorKey(string fname)
        {
            AC7XorKey key = new AC7XorKey(CalcNameKey(fname), 4);
            CalcPKeyFromNKey(key.NameKey, key.Offset, out key.pk1, out key.pk2);
            return key;
        }

        private static int CalcNameKey(string fname)
        {
            fname = fname.ToUpper();
            int num = 0;
            for (int i = 0; i < fname.Length; i++)
            {
                int num2 = (byte)fname[i];
                num ^= num2;
                num2 = num * 8;
                num2 ^= num;
                int num3 = num + num;
                num2 = ~num2;
                num2 = (num2 >> 7) & 1;
                num = num2 | num3;
            }
            return num;
        }

        private static void CalcPKeyFromNKey(int nkey, int dataoffset, out int pk1, out int pk2)
        {
            long num = (uint)((long)nkey * 7L);
            System.Numerics.BigInteger bigInteger = new System.Numerics.BigInteger(5440514381186227205L);
            num += dataoffset;
            System.Numerics.BigInteger bigInteger2 = bigInteger * num;
            long num2 = (long)(bigInteger2 >> 70);
            long num3 = num2 >> 63;
            num2 += num3;
            num3 = num2 * 217;
            num -= num3;
            pk1 = (int)(num & 0xFFFFFFFFu);
            long num4 = (uint)((long)nkey * 11L);
            num4 += dataoffset;
            num2 = 0L;
            num2 &= 0x3FF;
            num4 += num2;
            num4 &= 0x3FF;
            long num5 = num4 - num2;
            pk2 = (int)(num5 & 0xFFFFFFFFu);
        }

        private static byte GetXorByte(byte tagb, ref AC7XorKey xorkey)
        {
            if (xorkey == null)
            {
                return tagb;
            }
            tagb = (byte)((uint)(tagb ^ AC7FullKey[xorkey.pk1 * 1024 + xorkey.pk2]) ^ 0x77u);
            xorkey.pk1++;
            xorkey.pk2++;
            if (xorkey.pk1 >= 217)
            {
                xorkey.pk1 = 0;
            }
            if (xorkey.pk2 >= 1024)
            {
                xorkey.pk2 = 0;
            }
            return tagb;
        }
    }
}
