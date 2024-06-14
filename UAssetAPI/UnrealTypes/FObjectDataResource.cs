using System;
using System.Collections.Generic;
using System.Text;

namespace UAssetAPI.UnrealTypes
{
    public enum EObjectDataResourceVersion : uint
    {
        Invalid,
		Initial,
		LatestPlusOne,
		Latest = LatestPlusOne - 1
	}

    [Flags]
    public enum EObjectDataResourceFlags
    {
        None					= 0,
	    Inline					= (1 << 0),
	    Streaming				= (1 << 1),
	    Optional				= (1 << 2),
	    Duplicate				= (1 << 3),
	    MemoryMapped			= (1 << 4),
	    DerivedDataReference	= (1 << 5),
    }

    /// <summary>
    /// UObject binary/bulk data resource type.
    /// </summary>
    public struct FObjectDataResource
    {
        public EObjectDataResourceFlags Flags;
        public long SerialOffset;
        public long DuplicateSerialOffset;
        public long SerialSize;
        public long RawSize;
        public FPackageIndex OuterIndex;
        public uint LegacyBulkDataFlags;

        public FObjectDataResource(EObjectDataResourceFlags Flags, long SerialOffset, long DuplicateSerialOffset, long SerialSize, long RawSize, FPackageIndex OuterIndex, uint LegacyBulkDataFlags)
        {
            this.Flags = Flags;
            this.SerialOffset = SerialOffset;
            this.DuplicateSerialOffset = DuplicateSerialOffset;
            this.SerialSize = SerialSize;
            this.RawSize = RawSize;
            this.OuterIndex = OuterIndex;
            this.LegacyBulkDataFlags = LegacyBulkDataFlags;
        }
    }
}
