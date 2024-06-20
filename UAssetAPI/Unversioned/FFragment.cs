using System;

namespace UAssetAPI.Unversioned
{
    /// <summary>
    /// Unversioned header fragment.
    /// </summary>
    public class FFragment
    {
        /// <summary>
        /// Number of properties to skip before values.
        /// </summary>
        public int SkipNum;

        /// <summary>
        /// Number of subsequent property values stored.
        /// </summary>
        public int ValueNum = 0;

        /// <summary>
        /// Is this the last fragment of the header?
        /// </summary>
        public bool bIsLast = false;

        public int FirstNum = -1;
        public int LastNum
        {
            get
            {
                return FirstNum + ValueNum - 1;
            }
        }

        public bool bHasAnyZeroes = false;

        internal static readonly byte SkipMax = 127;
        internal static readonly byte ValueMax = 127;
        internal static readonly uint SkipNumMask = 0x007fu;
        internal static readonly uint HasZeroMask = 0x0080u;
        internal static readonly int ValueNumShift = 9;
        internal static readonly uint IsLastMask = 0x0100u;

        public override string ToString()
        {
            return "{" + SkipNum + "," + ValueNum + "," + bHasAnyZeroes + "," + bIsLast + "}";
        }

        public ushort Pack()
        {
            if (SkipNum > SkipMax) throw new InvalidOperationException("Skip num " + SkipNum + " is greater than maximum possible value " + SkipMax);
            if (ValueNum > ValueMax) throw new InvalidOperationException("Value num " + ValueNum + " is greater than maximum possible value " + ValueMax);
            return (ushort)((byte)SkipNum | (bHasAnyZeroes ? HasZeroMask : 0) | (ushort)((byte)ValueNum << ValueNumShift) | (bIsLast ? IsLastMask : 0));
        }

        /// <summary>
        /// Used for debugging
        /// </summary>
        internal string PackedForm => BitConverter.ToString(BitConverter.GetBytes(Pack()));

        public static FFragment Unpack(ushort Int)
        {
            FFragment Fragment = new FFragment();
            Fragment.SkipNum = (byte)(Int & SkipNumMask);
            Fragment.bHasAnyZeroes = (Int & HasZeroMask) != 0;
            Fragment.ValueNum = (byte)(Int >> ValueNumShift);
            Fragment.bIsLast = (Int & IsLastMask) != 0;
            return Fragment;
        }

        public static FFragment GetFromBounds(int LastNumBefore, int FirstNum, int LastNum, bool hasAnyZeros, bool isLast) // for 1st fragment: LastNumBefore = -1
        {
            FFragment Fragment = new FFragment();
            Fragment.SkipNum = FirstNum - LastNumBefore - 1;
            Fragment.ValueNum = LastNum - FirstNum + 1;
            Fragment.bHasAnyZeroes = hasAnyZeros;
            Fragment.bIsLast = isLast;

            Fragment.FirstNum = FirstNum;
            return Fragment;
        }

        public FFragment()
        {

        }

        public FFragment(int skipNum, int valueNum, bool bIsLast, bool bHasAnyZeroes, int firstNum = -1) // specifying firstNum is not usually necessary
        {
            SkipNum = skipNum;
            ValueNum = valueNum;
            this.bIsLast = bIsLast;
            this.bHasAnyZeroes = bHasAnyZeroes;
            this.FirstNum = firstNum;
        }
    }
}
