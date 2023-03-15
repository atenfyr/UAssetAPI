using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml.Linq;

namespace UAssetAPI.UnrealTypes
{
    public enum EPackageObjectIndexType
    {
        Export,
        ScriptImport,
        PackageImport,
        Null,
        TypeCount = Null
    }

    public struct FPackageObjectIndex
    {
        private const int IndexBits = 62;
        private const ulong IndexMask = (1UL << IndexBits) - 1UL;
        private const int TypeShift = IndexBits;
        public const ulong Invalid = ~0UL;

        public EPackageObjectIndexType Type;

        // Export
        public uint Export
        {
            get
            {
                return ImportedPublicExportHashIndex;
            }
            set
            {
                ImportedPublicExportHashIndex = value;
            }
        }

        // ScriptImport
        // This is usually the CityHash64 of the lowercase path
        // The first 2 bits will be discarded if set
        public ulong Hash
        {
            get
            {
                return ((ulong)ImportedPackageIndex << sizeof(uint)) | (ulong)ImportedPublicExportHashIndex;
            }
            set
            {
                ImportedPackageIndex = (uint)((value & IndexMask) >> sizeof(uint));
                ImportedPublicExportHashIndex = (uint)value;
            }
        }

        // PackageImport
        public uint ImportedPackageIndex;
        public uint ImportedPublicExportHashIndex;

        public bool IsNull => Type == EPackageObjectIndexType.Null;
        public bool IsExport => Type == EPackageObjectIndexType.Export;
        public bool IsImport => IsScriptImport || IsPackageImport;
        public bool IsScriptImport => Type == EPackageObjectIndexType.ScriptImport;
        public bool IsPackageImport => Type == EPackageObjectIndexType.PackageImport;

        public static FPackageObjectIndex Read(AssetBinaryReader reader)
        {
            ulong rawVal = reader.ReadUInt64();

            // TODO: is this interpretation actually right? results don't always make sense
            var res = new FPackageObjectIndex();
            res.Type = (EPackageObjectIndexType)(rawVal >> TypeShift);
            res.Hash = rawVal;
            return res;
        }

        public static int Write(AssetBinaryWriter writer, EPackageObjectIndexType typ, ulong hash)
        {
            writer.Write(typ == EPackageObjectIndexType.Null ? ulong.MaxValue : (((ulong)typ << TypeShift) | hash));
            return sizeof(ulong);
        }

        public int Write(AssetBinaryWriter writer)
        {
            return FPackageObjectIndex.Write(writer, Type, Hash);
        }
    }
}
