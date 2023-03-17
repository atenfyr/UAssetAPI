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

        public virtual FName ReadFName(INameMap nameMap)
        {
            uint Index = this.ReadUInt32();
            uint Number = this.ReadUInt32();

            var res = new FName(nameMap, (int)(Index & FName.IndexMask), (int)Number);
            res.Type = (EMappedNameType)((Index & FName.TypeMask) >> FName.TypeShift);
            return res;
        }
    }
}
