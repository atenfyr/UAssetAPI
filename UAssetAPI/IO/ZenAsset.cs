using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.IO
{
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

        public static FSerializedNameHeader Read(AssetBinaryReader reader)
        {
            var b1 = reader.ReadByte();
            var b2 = reader.ReadByte();

            var res = new FSerializedNameHeader();
            res.bIsWide = (b1 & (byte)0x80) > 0;
            res.Len = ((b1 & (byte)0x7F) << 8) + b2;
            return res;
        }

        public static void Write(AssetBinaryWriter writer, bool bIsWideVal, int lenVal)
        {
            byte b1 = (byte)(((byte)(bIsWideVal ? 1 : 0)) << 7 | (byte)(lenVal >> 8));
            byte b2 = (byte)lenVal;
            writer.Write(b1); writer.Write(b2);
        }

        public void Write(AssetBinaryWriter writer)
        {
            FSerializedNameHeader.Write(writer, bIsWide, Len);
        }
    }

    public class ZenAsset : UnrealPackage
    {
        public EZenPackageVersion ZenVersion;
        public FName Name;
        public FName SourceName;
        public ulong HashVersion = CityHash64;

        private const ulong CityHash64 = 0x00000000C1640000;
        public void ReadNameBatch(AssetBinaryReader reader)
        {
            int numStrings = reader.ReadInt32();
            if (numStrings == 0) return;
            int numBytesOfStrings = reader.ReadInt32();

            // read hashes
            HashVersion = reader.ReadUInt64();
            switch (HashVersion)
            {
                case CityHash64:
                    for (int i = 0; i < numStrings; i++) reader.ReadUInt64(); // CityHash64 of str.ToLowerCase();
                    break;
                default:
                    throw new InvalidOperationException("Unknown algorithm ID " + HashVersion);
            }

            // read headers
            FSerializedNameHeader[] nameHeaders = new FSerializedNameHeader[numStrings];
            for (int i = 0; i < numStrings; i++) nameHeaders[i] = FSerializedNameHeader.Read(reader);

            // read strings
            ClearNameIndexList();
            for (int i = 0; i < numStrings; i++)
            {
                AddNameReference(reader.ReadNameMapString(nameHeaders[i], out _), true);
            }
        }

        public void WriteNameBatch(AssetBinaryWriter writer)
        {
            writer.Write(this.nameMapIndexList.Count);
            if (this.nameMapIndexList.Count == 0) return;
            long numBytesOfStringsPos = writer.BaseStream.Position;
            writer.Write(0);

            // write hashes
            writer.Write(HashVersion);
            switch (HashVersion)
            {
                case CityHash64:
                    for (int i = 0; i < this.nameMapIndexList.Count; i++)
                    {
                        writer.Write(CRCGenerator.CityHash64(CRCGenerator.ToLower(this.nameMapIndexList[i].Value), this.nameMapIndexList[i].Encoding));
                    }
                    break;
                default:
                    throw new InvalidOperationException("Unknown algorithm ID " + HashVersion);
            }

            // write headers
            for (int i = 0; i < this.nameMapIndexList.Count; i++)
            {
                FSerializedNameHeader.Write(writer, this.nameMapIndexList[i].Encoding is UnicodeEncoding, this.nameMapIndexList[i].Value.Length);
            }

            // write strings
            long stringsStartPos = writer.BaseStream.Position;
            for (int i = 0; i < this.nameMapIndexList.Count; i++)
            {
                writer.Write(this.nameMapIndexList[i].Encoding.GetBytes(this.nameMapIndexList[i].Value));
            }
            long stringsEndPos = writer.BaseStream.Position;

            // fix length
            writer.Seek((int)numBytesOfStringsPos, SeekOrigin.Begin);
            writer.Write((int)(stringsEndPos - stringsStartPos));
            writer.Seek((int)stringsEndPos, SeekOrigin.Begin);
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
            if (ObjectVersion == ObjectVersion.UNKNOWN) throw new UnknownEngineVersionException("Cannot begin serialization before an object version is specified");

            if (ObjectVersionUE5 >= ObjectVersionUE5.INITIAL_VERSION)
            {
                IsUnversioned = reader.ReadUInt32() == 0;
                uint HeaderSize = reader.ReadUInt32();
                Name = reader.ReadFName();
                PackageFlags = (EPackageFlags)reader.ReadUInt32();
                uint CookedHeaderSize = reader.ReadUInt32();
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
                ReadNameBatch(reader);
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
