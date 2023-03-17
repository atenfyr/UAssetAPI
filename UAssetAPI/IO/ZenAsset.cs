using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using UAssetAPI.ExportTypes;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.IO
{
    public struct FInternalArc
    {
        public int FromExportBundleIndex;
        public int ToExportBundleIndex;

        public static FInternalArc Read(AssetBinaryReader reader)
        {
            var res = new FInternalArc();
            res.FromExportBundleIndex = reader.ReadInt32();
            res.ToExportBundleIndex = reader.ReadInt32();
            return res;
        }

        public static int Write(AssetBinaryWriter writer, int v2, int v3)
        {
            writer.Write(v2);
            writer.Write(v3);
            return sizeof(int) * 2;
        }

        public int Write(AssetBinaryWriter writer)
        {
            return FInternalArc.Write(writer, FromExportBundleIndex, ToExportBundleIndex);
        }
    }

    public struct FExternalArc
    {
        public int FromImportIndex;
        EExportCommandType FromCommandType;
        public int ToExportBundleIndex;

        public static FExternalArc Read(AssetBinaryReader reader)
        {
            var res = new FExternalArc();
            res.FromImportIndex = reader.ReadInt32();
            res.FromCommandType = (EExportCommandType)reader.ReadByte();
            res.ToExportBundleIndex = reader.ReadInt32();
            return res;
        }

        public static int Write(AssetBinaryWriter writer, int v1, EExportCommandType v2, int v3)
        {
            writer.Write(v1);
            writer.Write((byte)v2);
            writer.Write(v3);
            return sizeof(int) + sizeof(uint) + sizeof(int);
        }

        public int Write(AssetBinaryWriter writer)
        {
            return FExternalArc.Write(writer, FromImportIndex, FromCommandType, ToExportBundleIndex);
        }
    }

    public enum EExportCommandType : uint
    {
        ExportCommandType_Create,
        ExportCommandType_Serialize,
        ExportCommandType_Count
    }

    public struct FExportBundleHeader
    {
        public ulong SerialOffset;
        public uint FirstEntryIndex;
        public uint EntryCount;

        public static FExportBundleHeader Read(AssetBinaryReader reader)
        {
            var res = new FExportBundleHeader();
            res.SerialOffset = reader.Asset.ObjectVersionUE5 > ObjectVersionUE5.UNKNOWN ? reader.ReadUInt64() : ulong.MaxValue;
            res.FirstEntryIndex = reader.ReadUInt32();
            res.EntryCount = reader.ReadUInt32();
            return res;
        }

        public static int Write(AssetBinaryWriter writer, ulong v1, uint v2, uint v3)
        {
            if (writer.Asset.ObjectVersionUE5 > ObjectVersionUE5.UNKNOWN) writer.Write(v1);
            writer.Write(v2);
            writer.Write(v3);
            return (writer.Asset.ObjectVersionUE5 > ObjectVersionUE5.UNKNOWN ? sizeof(ulong) : 0) + sizeof(uint) * 2;
        }

        public int Write(AssetBinaryWriter writer)
        {
            return FExportBundleHeader.Write(writer, SerialOffset, FirstEntryIndex, EntryCount);
        }
    }    

    public struct FExportBundleEntry
    {
        public uint LocalExportIndex;
        public EExportCommandType CommandType;

        public static FExportBundleEntry Read(AssetBinaryReader reader)
        {
            var res = new FExportBundleEntry();
            res.LocalExportIndex = reader.ReadUInt32();
            res.CommandType = (EExportCommandType)reader.ReadUInt32();
            return res;
        }

        public static int Write(AssetBinaryWriter writer, uint lei, EExportCommandType typ)
        {
            writer.Write((uint)lei);
            writer.Write((uint)typ);
            return sizeof(uint) * 2;
        }

        public int Write(AssetBinaryWriter writer)
        {
            return FExportBundleEntry.Write(writer, LocalExportIndex, CommandType);
        }
    }

    public enum EZenPackageVersion : uint
    {
        Initial,

        LatestPlusOne,
        Latest = LatestPlusOne - 1
    }

    public class FSerializedNameHeader
    {
        public bool bIsWide;
        public int Len;

        public static FSerializedNameHeader Read(BinaryReader reader)
        {
            var b1 = reader.ReadByte();
            var b2 = reader.ReadByte();

            var res = new FSerializedNameHeader();
            res.bIsWide = (b1 & (byte)0x80) > 0;
            res.Len = ((b1 & (byte)0x7F) << 8) + b2;
            return res;
        }

        public static void Write(BinaryWriter writer, bool bIsWideVal, int lenVal)
        {
            byte b1 = (byte)(((byte)(bIsWideVal ? 1 : 0)) << 7 | (byte)(lenVal >> 8));
            byte b2 = (byte)lenVal;
            writer.Write(b1); writer.Write(b2);
        }

        public void Write(BinaryWriter writer)
        {
            FSerializedNameHeader.Write(writer, bIsWide, Len);
        }
    }

    public class ZenAsset : UnrealPackage
    {
        /// <summary>
        /// The global data of the game that this asset is from.
        /// </summary>
        public IOStoreContainer GlobalData;

        public EZenPackageVersion ZenVersion;
        public FName Name;
        public FName SourceName;
        /// <summary>
        /// Should serialized hashes be verified on read?
        /// </summary>
        public bool VerifyHashes = false;
        public ulong HashVersion = UnrealBinaryReader.CityHash64;
        public byte[] BulkDataMap;

        public ulong[] ImportedPublicExportHashes;

        /// <summary>
        /// Map of object imports. UAssetAPI used to call these "links."
        /// </summary>
        public List<FPackageObjectIndex> Imports;

        private Dictionary<ulong, string> CityHash64Map = new Dictionary<ulong, string>();
        private void AddCityHash64MapEntryRaw(string val)
        {
            ulong hsh = CRCGenerator.GenerateImportHashFromObjectPath(val);
            if (CityHash64Map.ContainsKey(hsh))
            {
                if (CRCGenerator.ToLower(CityHash64Map[hsh]) == CRCGenerator.ToLower(val)) return;
                throw new FormatException("CityHash64 hash collision between \"" + CityHash64Map[hsh] + "\" and \"" + val + "\"");
            }
            CityHash64Map.Add(hsh, val);
        }
        public string GetStringFromCityHash64(ulong val)
        {
            if (CityHash64Map.ContainsKey(val)) return CityHash64Map[val];
            if (Mappings.CityHash64Map.ContainsKey(val)) return Mappings.CityHash64Map[val];
            return null;
        }

        /// <summary>
        /// Finds the class path and export name of the SuperStruct of this asset, if it exists.
        /// </summary>
        /// <param name="parentClassPath">The class path of the SuperStruct of this asset, if it exists.</param>
        /// <param name="parentClassExportName">The export name of the SuperStruct of this asset, if it exists.</param>
        public override void GetParentClass(out FName parentClassPath, out FName parentClassExportName)
        {
            throw new NotImplementedException("Unimplemented method ZenAsset.GetParentClass");
        }

        internal override FName GetParentClassExportName()
        {
            throw new NotImplementedException("Unimplemented method ZenAsset.GetParentClassExportName");
        }

        /// <summary>
        /// Reads an asset into memory.
        /// </summary>
        /// <param name="reader">The input reader.</param>
        /// <param name="manualSkips">An array of export indexes to skip parsing. For most applications, this should be left blank.</param>
        /// <param name="forceReads">An array of export indexes that must be read, overriding entries in the manualSkips parameter. For most applications, this should be left blank.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public override void Read(AssetBinaryReader reader, int[] manualSkips = null, int[] forceReads = null)
        {
            if (Mappings == null) throw new InvalidOperationException();
            if (ObjectVersion == ObjectVersion.UNKNOWN) throw new UnknownEngineVersionException("Cannot begin serialization before an object version is specified");

            FExportBundleEntry[] exportBundleEntries = null;
            FExportBundleHeader[] exportBundleHeaders = null;
            FInternalArc[] internalArcs = null;
            FExternalArc[][] externalArcs = null; // the index is the same as the index into the ImportedPackageIds map
            if (ObjectVersionUE5 >= ObjectVersionUE5.INITIAL_VERSION)
            {
                IsUnversioned = reader.ReadUInt32() == 0;
                uint HeaderSize = reader.ReadUInt32();
                Name = reader.ReadFName();
                PackageFlags = (EPackageFlags)reader.ReadUInt32();
                uint CookedHeaderSize = reader.ReadUInt32(); // where does this number come from?
                int ImportedPublicExportHashesOffset = reader.ReadInt32();
                int ImportMapOffset = reader.ReadInt32();
                int ExportMapOffset = reader.ReadInt32();
                int ExportBundleEntriesOffset = reader.ReadInt32();
                int GraphDataOffset = reader.ReadInt32();

                if (!IsUnversioned)
                {
                    ZenVersion = (EZenPackageVersion)reader.ReadUInt32();
                    ObjectVersion = (ObjectVersion)reader.ReadInt32();
                    ObjectVersionUE5 = (ObjectVersionUE5)reader.ReadInt32();
                    FileVersionLicenseeUE = reader.ReadInt32();
                    ReadCustomVersionContainer(reader);
                }

                // name map batch
                reader.ReadNameBatch(VerifyHashes, out HashVersion, out List<FString> tempNameMap);
                ClearNameIndexList();
                foreach (var entry in tempNameMap)
                {
                    AddCityHash64MapEntryRaw(entry.Value);
                    AddNameReference(entry, true);
                }

                // bulk data map
                if (ObjectVersionUE5 >= ObjectVersionUE5.DATA_RESOURCES)
                {
                    ulong bulkDataMapSize = reader.ReadUInt64();
                    BulkDataMap = reader.ReadBytes((int)bulkDataMapSize); // i don't like this cast; if it becomes a problem, we can use a workaround instead
                }

                // imported public export hashes
                reader.BaseStream.Seek(ImportedPublicExportHashesOffset, SeekOrigin.Begin);
                ImportedPublicExportHashes = new ulong[(ImportMapOffset - ImportedPublicExportHashesOffset) / sizeof(ulong)];
                for (int i = 0; i < ImportedPublicExportHashes.Length; i++) ImportedPublicExportHashes[i] = reader.ReadUInt64();

                // import map
                reader.BaseStream.Seek(ImportMapOffset, SeekOrigin.Begin);
                Imports = new List<FPackageObjectIndex>();
                for (int i = 0; i < (ExportMapOffset - ImportMapOffset) / sizeof(ulong); i++)
                {
                    Imports.Add(FPackageObjectIndex.Read(reader));
                }

                // export map
                reader.BaseStream.Seek(ExportMapOffset, SeekOrigin.Begin);
                Exports = new List<Export>();
                int exportMapEntrySize = (int)Export.GetExportMapEntrySize(this);
                for (int i = 0; i < (ExportBundleEntriesOffset - ExportMapOffset) / exportMapEntrySize; i++)
                {
                    var newExport = new Export(this, new byte[0]);
                    newExport.ReadExportMapEntry(reader);
                    Exports.Add(newExport);
                }

                // export bundle entries; gives order that exports should be serialized
                reader.BaseStream.Seek(ExportBundleEntriesOffset, SeekOrigin.Begin);
                long startPos = reader.BaseStream.Position;
                exportBundleEntries = new FExportBundleEntry[Exports.Count * (int)(uint)EExportCommandType.ExportCommandType_Count];
                for (int i = 0; i < exportBundleEntries.Length; i++) exportBundleEntries[i] = FExportBundleEntry.Read(reader);
                long endPos = reader.BaseStream.Position;
                if (endPos != GraphDataOffset) throw new FormatException("extra padding is needed; please report if you see this error message");
                //reader.ReadBytes((int)UAPUtils.AlignPadding(endPos - startPos, 8));

                // graph data, once this is implemented fix FPackageIndex ToImport & GetParentClass
                reader.BaseStream.Seek(GraphDataOffset, SeekOrigin.Begin);
                var exportBundleHeadersList = new List<FExportBundleHeader>();
                int numEntriesTotal = 0;
                while (numEntriesTotal < exportBundleEntries.Length)
                {
                    var nxt = FExportBundleHeader.Read(reader);
                    numEntriesTotal += (int)nxt.EntryCount;
                    exportBundleHeadersList.Add(nxt);
                }
                exportBundleHeaders = exportBundleHeadersList.ToArray();

                int numInternalArcs = reader.ReadInt32();
                internalArcs = new FInternalArc[numInternalArcs];
                for (int i = 0; i < numInternalArcs; i++) internalArcs[i] = FInternalArc.Read(reader);

                var externalArcsList = new List<FExternalArc[]>();
                while (reader.BaseStream.Position < HeaderSize)
                {
                    int numExternalArcs = reader.ReadInt32();
                    var externalArcsThese = new FExternalArc[numExternalArcs];
                    for (int i = 0; i < numExternalArcs; i++) externalArcsThese[i] = FExternalArc.Read(reader);
                    externalArcsList.Add(externalArcsThese);
                }
                externalArcs = externalArcsList.ToArray();
            }
            else
            {
                Name = reader.ReadFName();
                SourceName = reader.ReadFName();
                PackageFlags = (EPackageFlags)reader.ReadUInt32();
                uint CookedHeaderSize = reader.ReadUInt32();
                int NameMapNamesOffset = reader.ReadInt32();
                int NameMapNamesSize = reader.ReadInt32();
                int NameMapHashesOffset = reader.ReadInt32();
                int NameMapHashesSize = reader.ReadInt32();
                int ImportMapOffset = reader.ReadInt32();
                int ExportMapOffset = reader.ReadInt32();
                int ExportBundlesOffset = reader.ReadInt32();
                int GraphDataOffset = reader.ReadInt32();
                int GraphDataSize = reader.ReadInt32();

                // name map batch
                reader.ReadNameBatch(VerifyHashes, out HashVersion, out List<FString> tempNameMap);
                foreach (var entry in tempNameMap)
                {
                    AddCityHash64MapEntryRaw(entry.Value);
                    AddNameReference(entry, true);
                }

                // import map
                reader.BaseStream.Seek(ImportMapOffset, SeekOrigin.Begin);
                Imports = new List<FPackageObjectIndex>();
                for (int i = 0; i < (ExportMapOffset - ImportMapOffset) / sizeof(ulong); i++)
                {
                    Imports.Add(FPackageObjectIndex.Read(reader));
                }

                // export map
                reader.BaseStream.Seek(ExportMapOffset, SeekOrigin.Begin);
                Exports = new List<Export>();
                int exportMapEntrySize = (int)Export.GetExportMapEntrySize(this);
                for (int i = 0; i < (ExportBundlesOffset - ExportMapOffset) / exportMapEntrySize; i++)
                {
                    var newExport = new Export(this, new byte[0]);
                    newExport.ReadExportMapEntry(reader);
                    Exports.Add(newExport);
                }

                // export bundle entries
                reader.BaseStream.Seek(ExportBundlesOffset, SeekOrigin.Begin);
                // weird parsing here, combine both bundles and headers

                // graph data (?)

            }

            // end summary
            
            foreach (FExportBundleHeader headr in exportBundleHeaders)
            {
                for (uint i = 0u; i < headr.EntryCount; i++)
                {
                    FExportBundleEntry entry = exportBundleEntries[headr.FirstEntryIndex + i];
                    switch (entry.CommandType)
                    {
                        case EExportCommandType.ExportCommandType_Serialize:
                            ConvertExportToChildExportAndRead(reader, (int)entry.LocalExportIndex);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Serializes an asset from memory.
        /// </summary>
        /// <returns>A stream that the asset has been serialized to.</returns>
        public override MemoryStream WriteData()
        {
            if (ObjectVersionUE5 >= ObjectVersionUE5.INITIAL_VERSION)
            {
                throw new NotImplementedException("UE5 IO store parsing is not implemented");
            }
            else
            {
                // i dont know if pre-5.0 io store assets are just equivalent to regular uassets or not... investigate further
                throw new NotImplementedException("Pre-UE5 IO store parsing is not implemented");
            }
            return null;
        }

        /// <summary>
        /// Serializes and writes an asset to disk from memory.
        /// </summary>
        /// <param name="outputPath">The path on disk to write the asset to.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when <see cref="ObjectVersion"/> is unspecified.</exception>
        public override void Write(string outputPath)
        {
            if (Mappings == null) throw new InvalidOperationException();
            if (ObjectVersion == ObjectVersion.UNKNOWN) throw new UnknownEngineVersionException("Cannot begin serialization before an object version is specified");

            MemoryStream newData = WriteData();
            using (FileStream f = File.Open(outputPath, FileMode.Create, FileAccess.Write))
            {
                newData.CopyTo(f);
            }
        }

        /// <summary>
        /// Reads an asset from disk and initializes a new instance of the <see cref="UAsset"/> class to store its data in memory.
        /// </summary>
        /// <param name="path">The path of the asset file on disk that this instance will read from.</param>
        /// <param name="engineVersion">The version of the Unreal Engine that will be used to parse this asset. If the asset is versioned, this can be left unspecified.</param>
        /// <param name="mappings">A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public ZenAsset(string path, EngineVersion engineVersion = EngineVersion.UNKNOWN, Usmap mappings = null)
        {
            this.FilePath = path;
            this.Mappings = mappings;
            SetEngineVersion(engineVersion);

            Read(PathToReader(path));
        }

        /// <summary>
        /// Reads an asset from a BinaryReader and initializes a new instance of the <see cref="ZenAsset"/> class to store its data in memory.
        /// </summary>
        /// <param name="reader">The asset's BinaryReader that this instance will read from.</param>
        /// <param name="engineVersion">The version of the Unreal Engine that will be used to parse this asset. If the asset is versioned, this can be left unspecified.</param>
        /// <param name="mappings">A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.</param>
        /// <param name="useSeparateBulkDataFiles">Does this asset uses separate bulk data files (.uexp, .ubulk)?</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public ZenAsset(AssetBinaryReader reader, EngineVersion engineVersion = EngineVersion.UNKNOWN, Usmap mappings = null, bool useSeparateBulkDataFiles = false)
        {
            this.Mappings = mappings;
            UseSeparateBulkDataFiles = useSeparateBulkDataFiles;
            SetEngineVersion(engineVersion);
            Read(reader);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZenAsset"/> class. This instance will store no asset data and does not represent any asset in particular until the <see cref="Read"/> method is manually called.
        /// </summary>
        /// <param name="engineVersion">The version of the Unreal Engine that will be used to parse this asset. If the asset is versioned, this can be left unspecified.</param>
        /// <param name="mappings">A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.</param>
        public ZenAsset(EngineVersion engineVersion = EngineVersion.UNKNOWN, Usmap mappings = null)
        {
            this.Mappings = mappings;
            SetEngineVersion(engineVersion);
        }

        /// <summary>
        /// Reads an asset from disk and initializes a new instance of the <see cref="ZenAsset"/> class to store its data in memory.
        /// </summary>
        /// <param name="path">The path of the asset file on disk that this instance will read from.</param>
        /// <param name="objectVersion">The object version of the Unreal Engine that will be used to parse this asset</param>
        /// <param name="customVersionContainer">A list of custom versions to parse this asset with.</param>
        /// <param name="mappings">A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public ZenAsset(string path, ObjectVersion objectVersion, List<CustomVersion> customVersionContainer, Usmap mappings = null)
        {
            this.FilePath = path;
            this.Mappings = mappings;
            ObjectVersion = objectVersion;
            CustomVersionContainer = customVersionContainer;

            Read(PathToReader(path));
        }

        /// <summary>
        /// Reads an asset from a BinaryReader and initializes a new instance of the <see cref="ZenAsset"/> class to store its data in memory.
        /// </summary>
        /// <param name="reader">The asset's BinaryReader that this instance will read from.</param>
        /// <param name="objectVersion">The object version of the Unreal Engine that will be used to parse this asset</param>
        /// <param name="customVersionContainer">A list of custom versions to parse this asset with.</param>
        /// <param name="mappings">A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.</param>
        /// <param name="useSeparateBulkDataFiles">Does this asset uses separate bulk data files (.uexp, .ubulk)?</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public ZenAsset(AssetBinaryReader reader, ObjectVersion objectVersion, List<CustomVersion> customVersionContainer, Usmap mappings = null, bool useSeparateBulkDataFiles = false)
        {
            this.Mappings = mappings;
            UseSeparateBulkDataFiles = useSeparateBulkDataFiles;
            ObjectVersion = objectVersion;
            CustomVersionContainer = customVersionContainer;
            Read(reader);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZenAsset"/> class. This instance will store no asset data and does not represent any asset in particular until the <see cref="Read"/> method is manually called.
        /// </summary>
        /// <param name="objectVersion">The object version of the Unreal Engine that will be used to parse this asset</param>
        /// <param name="customVersionContainer">A list of custom versions to parse this asset with.</param>
        /// <param name="mappings">A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.</param>
        public ZenAsset(ObjectVersion objectVersion, List<CustomVersion> customVersionContainer, Usmap mappings = null)
        {
            this.Mappings = mappings;
            ObjectVersion = objectVersion;
            CustomVersionContainer = customVersionContainer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZenAsset"/> class. This instance will store no asset data and does not represent any asset in particular until the <see cref="Read"/> method is manually called.
        /// </summary>
        public ZenAsset()
        {

        }
    }
}
