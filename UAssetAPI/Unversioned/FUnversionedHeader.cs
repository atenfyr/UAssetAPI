using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Unversioned
{
    // https://github.com/EpicGames/UnrealEngine/blob/master/Engine/Source/Runtime/CoreUObject/Private/Serialization/UnversionedPropertySerialization.cpp#L414

    /// <summary>
    /// List of serialized property indices and which of them are non-zero.
    /// Serialized as a stream of 16-bit skip-x keep-y fragments and a zero bitmask.
    /// </summary>
    public class FUnversionedHeader
    {
        public List<FFragment> Fragments;
        public byte[] ZeroMask;
        public bool bHasNonZeroValues = false;

        public void Read(AssetBinaryReader reader)
        {
            Fragments = new List<FFragment>(32);

            FFragment Fragment;
            uint ZeroMaskNum = 0;
            uint UnmaskedNum = 0;
            do
            {
                Fragment = FFragment.Unpack(reader.ReadUInt16());
                Fragments.Add(Fragment);

                if (Fragment.bHasAnyZeroes)
                {
                    ZeroMaskNum += Fragment.ValueNum;
                }
                else
                {
                    UnmaskedNum += Fragment.ValueNum;
                }
            }
            while (!Fragment.bIsLast);

            if (ZeroMaskNum > 0)
            {
                LoadZeroMaskData(reader, ZeroMaskNum);
                bHasNonZeroValues = UnmaskedNum > 0 || !CheckIfZeroMaskIsAllOnes();
            }
            else
            {
                bHasNonZeroValues = UnmaskedNum > 0;
            }
        }

        public void LoadZeroMaskData(AssetBinaryReader reader, uint NumBits)
        {
            if (NumBits <= 8)
            {
                ZeroMask = reader.ReadBytes(1);
            }
            else if (NumBits <= 16)
            {
                ZeroMask = reader.ReadBytes(2);
            }
            else
            {
                int numWords = (int)(NumBits / 32) + 1;
                ZeroMask = reader.ReadBytes(numWords * 4);
            }
        }

        public bool CheckIfZeroMaskIsAllOnes()
        {
            foreach (byte x in ZeroMask)
            {
                if (x != 0xFF) return false;
            }
            return true;
        }

        public void Write(AssetBinaryWriter writer)
        {
            foreach (FFragment Fragment in Fragments)
            {
                writer.Write(Fragment.Pack());
            }

            if (ZeroMask.Length > 0)
            {
                writer.Write(ZeroMask);
            }
        }

        public bool HasValues()
	    {
		    return bHasNonZeroValues | (ZeroMask.Length > 0);
	    }

        public bool HasNonZeroValues()
	    {
		    return bHasNonZeroValues;
	    }
    }
}
