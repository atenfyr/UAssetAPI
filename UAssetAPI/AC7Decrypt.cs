using System;
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

		public void Encrypt(string input, string output)
        {
			AC7XorKey xorKey = GetXorKey(Path.GetFileNameWithoutExtension(input));
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
			uint num = BitConverter.ToUInt32(uasset, 0);
			if (num != UAsset.ACE7_MAGIC)
			{
				return uasset;
			}
			byte[] array = new byte[uasset.Length];
			array[0] = 193;
			array[1] = 131;
			array[2] = 42;
			array[3] = 158;
			for (int i = 4; i < array.Length; i++)
			{
				array[i] = GetXorByte(uasset[i], ref xorkey);
			}
			return array;
		}

		// Unsure if this works or not, feel free to modify & PR
		public byte[] EncryptUAssetBytes(byte[] uasset, AC7XorKey xorkey)
		{
			uint num = BitConverter.ToUInt32(uasset, 0);
			if (num != UAsset.UASSET_MAGIC)
			{
				return uasset;
			}
			byte[] array = new byte[uasset.Length];
			array[0] = 65;
			array[1] = 67;
			array[2] = 69;
			array[3] = 55;
			for (int i = 4; i < array.Length; i++)
			{
				array[i] = GetXorByte(uasset[i], ref xorkey);
			}
			return array;
		}

		public byte[] DecryptUexpBytes(byte[] uexp, AC7XorKey xorkey)
		{
			if (xorkey == null)
			{
				return uexp;
			}
			byte[] array = new byte[uexp.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = GetXorByte(uexp[i], ref xorkey);
			}
			array[array.Length - 4] = 193;
			array[array.Length - 3] = 131;
			array[array.Length - 2] = 42;
			array[array.Length - 1] = 158;
			return array;
		}

		// Unsure if this works or not, feel free to modify & PR
		public byte[] EncryptUexpBytes(byte[] uexp, AC7XorKey xorkey)
		{
			byte[] array = new byte[uexp.Length + 4];
			for (int i = 0; i < uexp.Length; i++)
			{
				array[i] = GetXorByte(uexp[i], ref xorkey);
			}
			for (int j = uexp.Length; j < array.Length; j++)
			{
				array[j] = GetXorByte(0, ref xorkey);
			}
			return array;
		}

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
