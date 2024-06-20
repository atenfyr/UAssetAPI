using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace UAssetAPI.Unversioned
{
    // https://github.com/EpicGames/UnrealEngine/blob/master/Engine/Source/Runtime/CoreUObject/Private/Serialization/UnversionedPropertySerialization.cpp#L414

    /// <summary>
    /// List of serialized property indices and which of them are non-zero.
    /// Serialized as a stream of 16-bit skip-x keep-y fragments and a zero bitmask.
    /// </summary>
    public class FUnversionedHeader
    {
        public LinkedList<FFragment> Fragments;
        public LinkedListNode<FFragment> CurrentFragment;
        public int UnversionedPropertyIndex = 0;
        public int ZeroMaskIndex = 0;
        public uint ZeroMaskNum = 0;
        public BitArray ZeroMask;
        public bool bHasNonZeroValues = false;

        public void Read(AssetBinaryReader reader)
        {
            if (!reader.Asset.HasUnversionedProperties) return;
            Fragments = new LinkedList<FFragment>();

            FFragment Fragment;
            uint UnmaskedNum = 0;
            int firstNum = 0;
            do
            {
                Fragment = FFragment.Unpack(reader.ReadUInt16());
                Fragment.FirstNum = firstNum + Fragment.SkipNum;
                firstNum = firstNum + Fragment.SkipNum + Fragment.ValueNum;
                Fragments.AddLast(Fragment);
#if DEBUGVERBOSE
                Debug.WriteLine("R: " + Fragment);
#endif

                if (Fragment.bHasAnyZeroes)
                {
                    ZeroMaskNum += (uint)Fragment.ValueNum;
                }
                else
                {
                    UnmaskedNum += (uint)Fragment.ValueNum;
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
                ZeroMask = new BitArray(0);
                bHasNonZeroValues = UnmaskedNum > 0;
            }

            CurrentFragment = Fragments.First;
            UnversionedPropertyIndex = CurrentFragment.Value.FirstNum;
        }

        public void LoadZeroMaskData(AssetBinaryReader reader, uint NumBits)
        {
            if (NumBits <= 8)
            {
                ZeroMask = new BitArray(reader.ReadBytes(1));
            }
            else if (NumBits <= 16)
            {
                ZeroMask = new BitArray(reader.ReadBytes(2));
            }
            else
            {
                int numWords = UAPUtils.DivideAndRoundUp((int)NumBits, 32);
                int[] intData = new int[numWords];
                for (int i = 0; i < numWords; i++)
                {
                    intData[i] = reader.ReadInt32();
                }
                ZeroMask = new BitArray(intData);
            }
        }

        public byte[] SaveZeroMaskData()
        {
            int NumBits = ZeroMask.Length;

            byte[] res;
            if (NumBits <= 8)
            {
                res = new byte[1];
            }
            else if (NumBits <= 16)
            {
                res = new byte[2];
            }
            else
            {
                int numWords = UAPUtils.DivideAndRoundUp(NumBits, 32);
                res = new byte[numWords * sizeof(int)];
            }

            ZeroMask.CopyTo(res, 0);
            return res;
        }

        public bool CheckIfZeroMaskIsAllOnes()
        {
            for (int i = 0; i < ZeroMask.Length; i++)
            {
                if (!ZeroMask[i]) return false;
            }
            return true;
        }

        public void Write(AssetBinaryWriter writer)
        {
            if (!writer.Asset.HasUnversionedProperties) return;
            foreach (FFragment Fragment in Fragments)
            {
                writer.Write(Fragment.Pack());
            }

            if (ZeroMask.Length > 0)
            {
                writer.Write(SaveZeroMaskData());
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

        public FUnversionedHeader(AssetBinaryReader reader)
        {
            Read(reader);
        }

        public FUnversionedHeader()
        {

        }
    }
}
