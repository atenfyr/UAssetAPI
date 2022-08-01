using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

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
        public byte SkipNum;

        public bool bHasAnyZeroes = false;

        /// <summary>
        /// Number of subsequent property values stored.
        /// </summary>
        public byte ValueNum = 0;

        /// <summary>
        /// Is this the last fragment of the header?
        /// </summary>
        public bool bIsLast = false;

        private static readonly uint SkipMax = 127;
        private static readonly uint ValueMax = 127;
        private static readonly uint SkipNumMask = 0x007fu;
        private static readonly uint HasZeroMask = 0x0080u;
        private static readonly int ValueNumShift = 9;
		private static readonly uint IsLastMask  = 0x0100u;

        public ushort Pack()
		{
			return (ushort)(SkipNum | (bHasAnyZeroes ? HasZeroMask : 0) | (ushort)(ValueNum << ValueNumShift) | (bIsLast ? IsLastMask : 0));
		}

        public static FFragment Unpack(ushort Int)
        {
            FFragment Fragment = new FFragment();
            Fragment.SkipNum = (byte)(Int & SkipNumMask);
            Fragment.bHasAnyZeroes = (Int & HasZeroMask) != 0;
            Fragment.ValueNum = (byte)(Int >> ValueNumShift);
            Fragment.bIsLast = (Int & IsLastMask) != 0;
            return Fragment;
        }
    }
}
