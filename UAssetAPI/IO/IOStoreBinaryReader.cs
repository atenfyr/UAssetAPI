using System;
using System.IO;
using System.Text;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.IO
{
    public class IOStoreBinaryReader : UnrealBinaryReader
    {
        public IOStoreContainer Asset;

        public IOStoreBinaryReader(Stream stream, IOStoreContainer asset = null) : base(stream)
        {
            Asset = asset;
        }
    }
}
