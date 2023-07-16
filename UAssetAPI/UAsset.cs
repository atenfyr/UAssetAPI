using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UAssetAPI.CustomVersions;
using UAssetAPI.ExportTypes;
using UAssetAPI.FieldTypes;
using UAssetAPI.JSON;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI
{
    public class NameMapOutOfRangeException : FormatException
    {
        public FString RequiredName;

        public NameMapOutOfRangeException(FString requiredName) : base("Requested name \"" + requiredName + "\" not found in name map")
        {
            RequiredName = requiredName;
        }
    }

    public class InvalidMappingsException : InvalidOperationException
    {
        public InvalidMappingsException(string message = "Unversioned properties cannot be serialized without valid mappings") : base(message)
        {

        }
    }

    public class UnknownEngineVersionException : InvalidOperationException
    {
        public UnknownEngineVersionException(string message) : base(message)
        {

        }
    }

    /// <summary>
    /// Holds basic Unreal version numbers.
    /// </summary>
    public struct FEngineVersion
    {
        /// <summary>Major version number.</summary>
        public ushort Major;
        /// <summary>Minor version number.</summary>
        public ushort Minor;
        /// <summary>Patch version number.</summary>
        public ushort Patch;
        /// <summary>Changelist number. This is used by the engine to arbitrate when Major/Minor/Patch version numbers match.</summary>
        public uint Changelist;
        /// <summary>Branch name.</summary>
        public FString Branch;

        public void Write(UnrealBinaryWriter writer)
        {
            writer.Write(Major);
            writer.Write(Minor);
            writer.Write(Patch);
            writer.Write(Changelist);
            writer.Write(Branch);
        }

        public FEngineVersion(UnrealBinaryReader reader)
        {
            Major = reader.ReadUInt16();
            Minor = reader.ReadUInt16();
            Patch = reader.ReadUInt16();
            Changelist = reader.ReadUInt32();
            Branch = reader.ReadFString();
        }

        public FEngineVersion(ushort major, ushort minor, ushort patch, uint changelist, FString branch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            Changelist = changelist;
            Branch = branch;
        }
    }

    /// <summary>
    /// Revision data for an Unreal package file.
    /// </summary>
    public class FGenerationInfo
    {
        /// <summary>Number of exports in the export map for this generation.</summary>
        public int ExportCount;
        /// <summary>Number of names in the name map for this generation.</summary>
        public int NameCount;

        public FGenerationInfo(int exportCount, int nameCount)
        {
            ExportCount = exportCount;
            NameCount = nameCount;
        }
    }

    /// <summary>
    /// Represents an Unreal Engine asset.
    /// </summary>
    public class UAsset : UnrealPackage
    {
        /// <summary>
        /// Checks whether or not this asset maintains binary equality when serialized.
        /// </summary>
        /// <returns>Whether or not the asset maintained binary equality.</returns>
        public bool VerifyBinaryEquality()
        {
            MemoryStream f = this.PathToStream(FilePath);
            f.Seek(0, SeekOrigin.Begin);
            MemoryStream newDataStream = WriteData();
            f.Seek(0, SeekOrigin.Begin);

            if (f.Length != newDataStream.Length) return false;

            const int CHUNK_SIZE = 1024;
            byte[] buffer = new byte[CHUNK_SIZE];
            byte[] buffer2 = new byte[CHUNK_SIZE];
            int lastRead1;
            while ((lastRead1 = f.Read(buffer, 0, buffer.Length)) > 0)
            {
                int lastRead2 = newDataStream.Read(buffer2, 0, buffer2.Length);
                if (lastRead1 != lastRead2) return false;
                if (!buffer.SequenceEqual(buffer2)) return false;
            }

            return true;
        }


        /// <summary>
        /// Finds the class path and export name of the SuperStruct of this asset, if it exists.
        /// </summary>
        /// <param name="parentClassPath">The class path of the SuperStruct of this asset, if it exists.</param>
        /// <param name="parentClassExportName">The export name of the SuperStruct of this asset, if it exists.</param>
        public override void GetParentClass(out FName parentClassPath, out FName parentClassExportName)
        {
            parentClassPath = null;
            parentClassExportName = null;

            var bgcCat = GetClassExport();
            if (bgcCat == null) return;
            if (bgcCat.SuperStruct == null) return;

            Import parentClassLink = bgcCat.SuperStruct.ToImport(this);
            if (parentClassLink == null) return;
            if (parentClassLink.OuterIndex.Index >= 0) return;

            parentClassExportName = parentClassLink.ObjectName;
            parentClassPath = parentClassLink.OuterIndex.ToImport(this).ObjectName;
        }

        internal bool hasFoundParentClassExportName = false;
        internal FName parentClassExportNameCache = null;
        internal override FName GetParentClassExportName()
        {
            if (!hasFoundParentClassExportName)
            {
                hasFoundParentClassExportName = true;
                GetParentClass(out _, out parentClassExportNameCache);
            }
            return parentClassExportNameCache;
        }

        /// <summary>
        /// Adds a new import to the import map. This is equivalent to adding directly to the <see cref="Imports"/> list.
        /// </summary>
        /// <param name="li">The new import to add to the import map.</param>
        /// <returns>The FPackageIndex corresponding to the newly-added import.</returns>
        public FPackageIndex AddImport(Import li)
        {
            Imports.Add(li);
            return FPackageIndex.FromImport(Imports.Count - 1);
        }

        /// <summary>
        /// Searches for an import in the import map based off of certain parameters.
        /// </summary>
        /// <param name="classPackage">The ClassPackage that the requested import will have.</param>
        /// <param name="className">The ClassName that the requested import will have.</param>
        /// <param name="outerIndex">The CuterIndex that the requested import will have.</param>
        /// <param name="objectName">The ObjectName that the requested import will have.</param>
        /// <returns>The index of the requested import in the name map, or zero if one could not be found.</returns>
        public int SearchForImport(FName classPackage, FName className, FPackageIndex outerIndex, FName objectName)
        {
            int currentPos = 0;
            for (int i = 0; i < Imports.Count; i++)
            {
                currentPos--;
                if (classPackage == Imports[i].ClassPackage
                    && className == Imports[i].ClassName
                    && outerIndex == Imports[i].OuterIndex
                    && objectName == Imports[i].ObjectName)
                {
                    return currentPos;
                }

            }

            return 0;
        }

        /// <summary>
        /// Searches for an import in the import map based off of certain parameters.
        /// </summary>
        /// <param name="classPackage">The ClassPackage that the requested import will have.</param>
        /// <param name="className">The ClassName that the requested import will have.</param>
        /// <param name="objectName">The ObjectName that the requested import will have.</param>
        /// <returns>The index of the requested import in the name map, or zero if one could not be found.</returns>
        public int SearchForImport(FName classPackage, FName className, FName objectName)
        {
            int currentPos = 0;
            for (int i = 0; i < Imports.Count; i++)
            {
                currentPos--;
                if (classPackage == Imports[i].ClassPackage
                    && className == Imports[i].ClassName
                    && objectName == Imports[i].ObjectName)
                {
                    return currentPos;
                }

            }

            return 0;
        }

        /// <summary>
        /// Searches for an import in the import map based off of certain parameters.
        /// </summary>
        /// <param name="objectName">The ObjectName that the requested import will have.</param>
        /// <returns>The index of the requested import in the name map, or zero if one could not be found.</returns>
        public int SearchForImport(FName objectName)
        {
            int currentPos = 0;
            for (int i = 0; i < Imports.Count; i++)
            {
                currentPos--;
                if (objectName == Imports[i].ObjectName) return currentPos;
            }

            return 0;
        }

        /// <summary>
        /// The package file version number when this package was saved.
        /// </summary>
        /// <remarks>
        ///     The lower 16 bits stores the UE3 engine version, while the upper 16 bits stores the UE4/licensee version. For newer packages this is -7.
        ///     <list type="table">
        ///         <listheader>
        ///             <version>Version</version>
        ///             <description>Description</description>
        ///         </listheader>
        ///         <item>
        ///             <version>-2</version>
        ///             <description>indicates presence of enum-based custom versions</description>
        ///         </item>
        ///         <item>
        ///             <version>-3</version>
        ///             <description>indicates guid-based custom versions</description>
        ///         </item>
        ///         <item>
        ///             <version>-4</version>
        ///             <description>indicates removal of the UE3 version. Packages saved with this ID cannot be loaded in older engine versions</description>
        ///         </item>
        ///         <item>
        ///             <version>-5</version>
        ///             <description>indicates the replacement of writing out the "UE3 version" so older versions of engine can gracefully fail to open newer packages</description>
        ///         </item>
        ///         <item>
        ///             <version>-6</version>
        ///             <description>indicates optimizations to how custom versions are being serialized</description>
        ///         </item>
        ///         <item>
        ///             <version>-7</version>
        ///             <description>indicates the texture allocation info has been removed from the summary</description>
        ///         </item>
        ///         <item>
        ///             <version>-8</version>
        ///             <description>indicates that the UE5 version has been added to the summary</description>
        ///         </item>
        ///     </list>
        /// </remarks>
        public int LegacyFileVersion;

        internal ECustomVersionSerializationFormat CustomVersionSerializationFormat
        {
            get
            {
                if (LegacyFileVersion > -3) return ECustomVersionSerializationFormat.Enums;
                if (LegacyFileVersion > -6) return ECustomVersionSerializationFormat.Guids;
                return ECustomVersionSerializationFormat.Optimized;
            }
        }

        /// <summary>
        /// Whether or not this asset is loaded with the Event Driven Loader.
        /// </summary>
        public bool UsesEventDrivenLoader;

        /// <summary>
        /// Whether or not this asset serializes hashes in the name map.
        /// If null, this will be automatically determined based on the object version.
        /// </summary>
        public bool? WillSerializeNameHashes = null;

        /// <summary>
        /// Map of object imports. UAssetAPI used to call these "links."
        /// </summary>
        public List<Import> Imports;

        /// <summary>
        /// List of dependency lists for each export.
        /// </summary>
        public List<int[]> DependsMap;

        /// <summary>
        /// List of packages that are soft referenced by this package.
        /// </summary>
        public List<FString> SoftPackageReferenceList;

        /// <summary>
        /// Uncertain
        /// </summary>
        public byte[] AssetRegistryData;

        /// <summary>
        /// Some garbage data that appears to be present in certain games (e.g. Valorant)
        /// </summary>
        public byte[] ValorantGarbageData;

        /// <summary>
        /// Data about previous versions of this package.
        /// </summary>
        public List<FGenerationInfo> Generations;

        /// <summary>
        /// Current ID for this package. Effectively unused.
        /// </summary>
        public Guid PackageGuid;

        /// <summary>
        /// Engine version this package was saved with. This may differ from CompatibleWithEngineVersion for assets saved with a hotfix release.
        /// </summary>
        public FEngineVersion RecordedEngineVersion;

        /// <summary>
        /// Engine version this package is compatible with. Assets saved by Hotfix releases and engine versions that maintain binary compatibility will have
        /// a CompatibleWithEngineVersion.Patch that matches the original release (as opposed to SavedByEngineVersion which will have a patch version of the new release).
        /// </summary>
        public FEngineVersion RecordedCompatibleWithEngineVersion;

        /// <summary>
        /// Streaming install ChunkIDs
        /// </summary>
        public int[] ChunkIDs;

        /// <summary>
        /// Value that is used by the Unreal Engine to determine if the package was saved by Epic, a licensee, modder, etc.
        /// </summary>
        public uint PackageSource;

        /// <summary>
        /// The Generic Browser folder name that this package lives in. Usually "None" in cooked assets.
        /// </summary>
        public FString FolderName;

        /// <summary>
        /// A map of name map entries to hashes to use when serializing instead of the default engine hash algorithm. Useful when external programs improperly specify name map hashes and binary equality must be maintained.
        /// </summary>
        [JsonIgnore]
        public Dictionary<FString, uint> OverrideNameMapHashes;

        /// <summary>This is called "TotalHeaderSize" in UE4 where header refers to the whole summary, whereas in UAssetAPI "header" refers to just the data before the start of the name map</summary>
        internal int SectionSixOffset = 0;

        /// <summary>Number of names used in this package</summary>
        internal int NameCount = 0;

        /// <summary>Location into the file on disk for the name data</summary>
        internal int NameOffset;

        /// <summary>Number of names used in this package</summary>
        internal int SoftObjectPathsCount = 0;

        /// <summary>Location into the file on disk for the name data</summary>
        internal int SoftObjectPathsOffset = 0;

        /// <summary>Number of gatherable text data items in this package</summary>
        [JsonProperty]
        internal int GatherableTextDataCount;

        /// <summary>Location into the file on disk for the gatherable text data items</summary>
        [JsonProperty]
        internal int GatherableTextDataOffset;

        /// <summary>Number of exports contained in this package</summary>
        internal int ExportCount = 0;

        /// <summary>Location into the file on disk for the "Export Details" data</summary>
        internal int ExportOffset = 0;

        /// <summary>Number of imports contained in this package</summary>
        internal int ImportCount = 0;

        /// <summary>Location into the file on disk for the ImportMap data</summary>
        internal int ImportOffset = 0;

        /// <summary>Location into the file on disk for the DependsMap data</summary>
        internal int DependsOffset = 0;

        /// <summary>Number of soft package references contained in this package</summary>
        internal int SoftPackageReferencesCount = 0;

        /// <summary>Location into the file on disk for the soft package reference list</summary>
        internal int SoftPackageReferencesOffset = 0;

        /// <summary>Location into the file on disk for the SearchableNamesMap data</summary>
        [JsonProperty]
        internal int SearchableNamesOffset;

        /// <summary>Thumbnail table offset</summary>
        [JsonProperty]
        internal int ThumbnailTableOffset;

        /// <summary>Should be zero</summary>
        [JsonProperty]
        internal uint CompressionFlags;

        /// <summary>List of additional packages that are needed to be cooked for this package. No longer used</summary>
        [JsonProperty]
        internal List<FString> AdditionalPackagesToCook;

        /// <summary>Location into the file on disk for the asset registry tag data</summary>
        internal int AssetRegistryDataOffset;

        /// <summary>Offset to the location in the file where the bulkdata starts</summary>
        internal long BulkDataStartOffset;

        /// <summary>Offset to the location in the file where the FWorldTileInfo data start</summary>
        internal int WorldTileInfoDataOffset;

        /// <summary>Number of preload dependencies contained in this package</summary>
        internal int PreloadDependencyCount;

        /// <summary>Location into the file on disk for the preload dependency data</summary>
        internal int PreloadDependencyOffset;

        [JsonProperty]
        internal int NamesReferencedFromExportDataCount;
        [JsonProperty]
        internal long PayloadTocOffset;
        [JsonProperty]
        internal int DataResourceOffset;

        [JsonProperty]
        internal bool doWeHaveDependsMap = true;
        [JsonProperty]
        internal bool doWeHaveSoftPackageReferences = true;
        [JsonProperty]
        internal bool doWeHaveAssetRegistryData = true;
        [JsonProperty]
        internal bool doWeHaveWorldTileInfo = true;

        /// <summary>
        /// Copies a portion of a stream to another stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="output">The output stream.</param>
        /// <param name="start">The offset in the input stream to start copying from.</param>
        /// <param name="leng">The length in bytes of the data to be copied.</param>
        internal static void CopySplitUp(Stream input, Stream output, int start, int leng)
        {
            input.Seek(start, SeekOrigin.Begin);
            output.Seek(0, SeekOrigin.Begin);

            byte[] buffer = new byte[32768];
            int read;
            while (leng > 0 && (read = input.Read(buffer, 0, Math.Min(buffer.Length, leng))) > 0)
            {
                output.Write(buffer, 0, read);
                leng -= read;
            }
        }

        /// <summary>
        /// Magic number for the .uasset format
        /// </summary>
        public static readonly uint UASSET_MAGIC = 0x9E2A83C1;

        /// <summary>
        /// Magic number for Ace Combat 7 encrypted .uasset format
        /// </summary>
        public static readonly uint ACE7_MAGIC = 0x37454341;

        /// <summary>
        /// Reads the initial portion of the asset (everything before the name map).
        /// </summary>
        /// <param name="reader"></param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        private void ReadHeader(AssetBinaryReader reader)
        {
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            uint fileSignature = reader.ReadUInt32();
            if (fileSignature != UASSET_MAGIC) throw new FormatException("File signature mismatch");

            LegacyFileVersion = reader.ReadInt32();
            if (LegacyFileVersion != -4)
            {
                reader.ReadInt32(); // LegacyUE3Version for backwards-compatibility with UE3 games: always 864 in versioned assets, always 0 in unversioned assets
            }

            ObjectVersion fileVersionUE4 = (ObjectVersion)reader.ReadInt32();
            if (fileVersionUE4 > ObjectVersion.UNKNOWN)
            {
                IsUnversioned = false;
                ObjectVersion = fileVersionUE4;
            }
            else
            {
                IsUnversioned = true;
                if (Mappings != null && Mappings.FileVersionUE4 > 0) ObjectVersion = Mappings.FileVersionUE4;
                if (ObjectVersion == ObjectVersion.UNKNOWN) throw new UnknownEngineVersionException("Cannot begin serialization of an unversioned asset before an object version is manually specified");
            }

            if (LegacyFileVersion <= -8)
            {
                ObjectVersionUE5 fileVersionUE5 = (ObjectVersionUE5)reader.ReadInt32();
                if (fileVersionUE5 > ObjectVersionUE5.UNKNOWN) ObjectVersionUE5 = fileVersionUE5;
            }
            if (ObjectVersionUE5 == ObjectVersionUE5.UNKNOWN && Mappings != null && Mappings.FileVersionUE5 > 0) ObjectVersionUE5 = Mappings.FileVersionUE5;

            FileVersionLicenseeUE = reader.ReadInt32();

            // Custom versions container
            if (LegacyFileVersion <= -2)
            {
                CustomVersionContainer = reader.ReadCustomVersionContainer(CustomVersionSerializationFormat, CustomVersionContainer, Mappings);
            }

            SectionSixOffset = reader.ReadInt32(); // 24
            FolderName = reader.ReadFString();
            PackageFlags = (EPackageFlags)reader.ReadUInt32();
            NameCount = reader.ReadInt32();
            NameOffset = reader.ReadInt32();
            if (ObjectVersionUE5 >= ObjectVersionUE5.ADD_SOFTOBJECTPATH_LIST)
            {
                SoftObjectPathsCount = reader.ReadInt32();
                SoftObjectPathsOffset = reader.ReadInt32();
            }
            if (ObjectVersion >= ObjectVersion.VER_UE4_SERIALIZE_TEXT_IN_PACKAGES)
            {
                GatherableTextDataCount = reader.ReadInt32();
                GatherableTextDataOffset = reader.ReadInt32();
            }

            ExportCount = reader.ReadInt32();
            ExportOffset = reader.ReadInt32(); // 61
            ImportCount = reader.ReadInt32(); // 65
            ImportOffset = reader.ReadInt32(); // 69 (haha funny)
            DependsOffset = reader.ReadInt32(); // 73
            if (ObjectVersion >= ObjectVersion.VER_UE4_ADD_STRING_ASSET_REFERENCES_MAP)
            {
                SoftPackageReferencesCount = reader.ReadInt32(); // 77
                SoftPackageReferencesOffset = reader.ReadInt32(); // 81
            }
            if (ObjectVersion >= ObjectVersion.VER_UE4_ADDED_SEARCHABLE_NAMES)
            {
                SearchableNamesOffset = reader.ReadInt32();
            }
            ThumbnailTableOffset = reader.ReadInt32();

            // valorant garbage data is here

            PackageGuid = new Guid(reader.ReadBytes(16));

            Generations = new List<FGenerationInfo>();
            int generationCount = reader.ReadInt32();
            if (generationCount < 0 || generationCount > 1e5) // failsafe for some specific games
            {
                reader.BaseStream.Position -= sizeof(int) + 16;
                ValorantGarbageData = reader.ReadBytes(8); // garbage data
                PackageGuid = new Guid(reader.ReadBytes(16));
                generationCount = reader.ReadInt32();
            }
            for (int i = 0; i < generationCount; i++)
            {
                int genNumExports = reader.ReadInt32();
                int genNumNames = reader.ReadInt32();
                Generations.Add(new FGenerationInfo(genNumExports, genNumNames));
            }

            if (ObjectVersion >= ObjectVersion.VER_UE4_ENGINE_VERSION_OBJECT)
            {
                RecordedEngineVersion = new FEngineVersion(reader);
            }
            else
            {
                RecordedEngineVersion = new FEngineVersion(4, 0, 0, reader.ReadUInt32(), FString.FromString(""));
            }

            if (ObjectVersion >= ObjectVersion.VER_UE4_PACKAGE_SUMMARY_HAS_COMPATIBLE_ENGINE_VERSION)
            {
                RecordedCompatibleWithEngineVersion = new FEngineVersion(reader);
            }
            else
            {
                RecordedCompatibleWithEngineVersion = RecordedEngineVersion;
            }

            CompressionFlags = reader.ReadUInt32();
            int numCompressedChunks = reader.ReadInt32();
            if (numCompressedChunks > 0) throw new FormatException("Asset has package-level compression and is likely too old to be parsed");

            PackageSource = reader.ReadUInt32();

            AdditionalPackagesToCook = new List<FString>();
            int numAdditionalPackagesToCook = reader.ReadInt32();
            for (int i = 0; i < numAdditionalPackagesToCook; i++)
            {
                AdditionalPackagesToCook.Add(reader.ReadFString());
            }

            if (LegacyFileVersion > -7)
            {
                int numTextureAllocations = reader.ReadInt32(); // unused
                if (numTextureAllocations > 0) throw new FormatException("Asset has texture allocation info and is likely too old to be parsed");
            }

            AssetRegistryDataOffset = reader.ReadInt32();
            BulkDataStartOffset = reader.ReadInt64();

            if (ObjectVersion >= ObjectVersion.VER_UE4_WORLD_LEVEL_INFO)
            {
                WorldTileInfoDataOffset = reader.ReadInt32();
            }

            if (ObjectVersion >= ObjectVersion.VER_UE4_CHANGED_CHUNKID_TO_BE_AN_ARRAY_OF_CHUNKIDS)
            {
                int numChunkIDs = reader.ReadInt32();
                ChunkIDs = new int[numChunkIDs];
                for (int i = 0; i < numChunkIDs; i++)
                {
                    ChunkIDs[i] = reader.ReadInt32();
                }
            }
            else if (ObjectVersion >= ObjectVersion.VER_UE4_ADDED_CHUNKID_TO_ASSETDATA_AND_UPACKAGE)
            {
                ChunkIDs = new int[1];
                ChunkIDs[0] = reader.ReadInt32();
            }

            if (ObjectVersion >= ObjectVersion.VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS)
            {
                PreloadDependencyCount = reader.ReadInt32();
                PreloadDependencyOffset = reader.ReadInt32();
            }

            // ue5 stuff
            NamesReferencedFromExportDataCount = ObjectVersionUE5 >= ObjectVersionUE5.NAMES_REFERENCED_FROM_EXPORT_DATA ? reader.ReadInt32() : NameCount;
            PayloadTocOffset = ObjectVersionUE5 >= ObjectVersionUE5.PAYLOAD_TOC ? reader.ReadInt64() : -1;
            DataResourceOffset = ObjectVersionUE5 >= ObjectVersionUE5.DATA_RESOURCES ? reader.ReadInt32() : -1;
        }

        /// <summary>
        /// Reads an asset into memory.
        /// </summary>
        /// <param name="reader">The input reader.</param>
        /// <param name="manualSkips">An array of export indexes to skip parsing. For most applications, this should be left blank.</param>
        /// <param name="forceReads">An array of export indexes that must be read, overriding entries in the manualSkips parameter. For most applications, this should be left blank.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public override void Read(AssetBinaryReader reader, int[] manualSkips = null, int[] forceReads = null)
        {
            reader.Asset = this;
            hasFoundParentClassExportName = false;

            // Header
            ReadHeader(reader);

            // Name map
            reader.BaseStream.Seek(NameOffset, SeekOrigin.Begin);

            OverrideNameMapHashes = new Dictionary<FString, uint>();
            ClearNameIndexList();
            for (int i = 0; i < NameCount; i++)
            {
                FString nameInMap = reader.ReadNameMapString(null, out uint hashes);
                if (hashes == 0)
                {
                    OverrideNameMapHashes[nameInMap] = 0;
                }
                else if (hashes >> 16 == 0 && nameInMap.Value == nameInMap.Value.ToLowerInvariant()) // WITH_CASE_PRESERVING_NAME = 0; if pre-4.23, do not serialize CasePreservingHash 
                {
                    nameInMap.IsCasePreserving = false;
                }
                AddNameReference(nameInMap, true);
            }

            // Imports
            Imports = new List<Import>();
            if (ImportOffset > 0)
            {
                reader.BaseStream.Seek(ImportOffset, SeekOrigin.Begin);
                for (int i = 0; i < ImportCount; i++)
                {
                    Imports.Add(new Import(reader));
                }
            }

            // Export details
            Exports = new List<Export>();
            if (ExportOffset > 0)
            {
                reader.BaseStream.Seek(ExportOffset, SeekOrigin.Begin);
                for (int i = 0; i < ExportCount; i++)
                {
                    var newExport = new Export(this, new byte[0]);
                    newExport.ReadExportMapEntry(reader);
                    Exports.Add(newExport);
                }
            }

            // DependsMap
            DependsMap = new List<int[]>();
            if (DependsOffset > 0 || (ObjectVersion > ObjectVersion.VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS && ObjectVersion < ObjectVersion.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)) // 4.14-4.15 the depends offset wasnt updated so always serialized as 0
            {
                if (DependsOffset > 0) reader.BaseStream.Seek(DependsOffset, SeekOrigin.Begin);
                for (int i = 0; i < ExportCount; i++)
                {
                    int size = reader.ReadInt32();
                    int[] data = new int[size];
                    for (int j = 0; j < size; j++)
                    {
                        data[j] = reader.ReadInt32();
                    }
                    DependsMap.Add(data);
                }
            }
            else
            {
                doWeHaveDependsMap = false;
            }

            // SoftPackageReferenceList
            SoftPackageReferenceList = new List<FString>();
            if (SoftPackageReferencesOffset > 0)
            {
                reader.BaseStream.Seek(SoftPackageReferencesOffset, SeekOrigin.Begin);
                for (int i = 0; i < SoftPackageReferencesCount; i++)
                {
                    SoftPackageReferenceList.Add(reader.ReadFString());
                }
            }
            else
            {
                doWeHaveSoftPackageReferences = false;
            }

            // AssetRegistryData
            AssetRegistryData = new byte[0];
            if (AssetRegistryDataOffset > 0)
            {
                reader.BaseStream.Seek(AssetRegistryDataOffset, SeekOrigin.Begin);
                /*
                int numAssets = reader.ReadInt32();
                for (int i = 0; i < numAssets; i++)
                {
                    throw new NotImplementedException("Asset registry data is not yet supported. Please let me know if you see this error message");
                }
                */

                // For now: read binary data until next offset
                int nextOffset = this.WorldTileInfoDataOffset;
                if (this.PreloadDependencyOffset >= 0 && nextOffset <= 0) nextOffset = this.PreloadDependencyOffset;
                if (SectionSixOffset > 0 && Exports.Count > 0 && nextOffset <= 0) nextOffset = (int)Exports[0].SerialOffset;
                if (nextOffset <= 0) nextOffset = (int)this.BulkDataStartOffset;
                AssetRegistryData = reader.ReadBytes(nextOffset - AssetRegistryDataOffset);
            }
            else
            {
                doWeHaveAssetRegistryData = false;
            }

            // WorldTileInfoDataOffset
            WorldTileInfo = null;
            if (WorldTileInfoDataOffset > 0)
            {
                //reader.BaseStream.Seek(WorldTileInfoDataOffset, SeekOrigin.Begin);
                WorldTileInfo = new FWorldTileInfo();
                WorldTileInfo.Read(reader, this);
            }
            else
            {
                doWeHaveWorldTileInfo = false;
            }

            // PreloadDependencies
            for (int i = 0; i < Exports.Count; i++)
            {
                if (Exports[i].FirstExportDependencyOffset < 0) continue;
                this.UsesEventDrivenLoader = true;

                reader.BaseStream.Seek(PreloadDependencyOffset, SeekOrigin.Begin);
                reader.BaseStream.Seek(Exports[i].FirstExportDependencyOffset * sizeof(int), SeekOrigin.Current);

                Exports[i].SerializationBeforeSerializationDependencies = new List<FPackageIndex>(Exports[i].SerializationBeforeSerializationDependenciesSize);
                for (int j = 0; j < Exports[i].SerializationBeforeSerializationDependenciesSize; j++) Exports[i].SerializationBeforeSerializationDependencies.Add(FPackageIndex.FromRawIndex(reader.ReadInt32()));

                Exports[i].CreateBeforeSerializationDependencies = new List<FPackageIndex>(Exports[i].CreateBeforeSerializationDependenciesSize);
                for (int j = 0; j < Exports[i].CreateBeforeSerializationDependenciesSize; j++) Exports[i].CreateBeforeSerializationDependencies.Add(FPackageIndex.FromRawIndex(reader.ReadInt32()));

                Exports[i].SerializationBeforeCreateDependencies = new List<FPackageIndex>(Exports[i].SerializationBeforeCreateDependenciesSize);
                for (int j = 0; j < Exports[i].SerializationBeforeCreateDependenciesSize; j++) Exports[i].SerializationBeforeCreateDependencies.Add(FPackageIndex.FromRawIndex(reader.ReadInt32()));

                Exports[i].CreateBeforeCreateDependencies = new List<FPackageIndex>(Exports[i].CreateBeforeCreateDependenciesSize);
                for (int j = 0; j < Exports[i].CreateBeforeCreateDependenciesSize; j++) Exports[i].CreateBeforeCreateDependencies.Add(FPackageIndex.FromRawIndex(reader.ReadInt32()));
            }

            // TODO: read DataResources for 5.3+

            // Export data
            if (SectionSixOffset > 0 && Exports.Count > 0)
            {
                for (int i = 0; i < Exports.Count; i++)
                {
                    reader.BaseStream.Seek(Exports[i].SerialOffset, SeekOrigin.Begin);
                    if (manualSkips != null && manualSkips.Contains(i))
                    {
                        if (forceReads == null || !forceReads.Contains(i))
                        {
                            Exports[i] = Exports[i].ConvertToChildExport<RawExport>();
                            ((RawExport)Exports[i]).Data = reader.ReadBytes((int)Exports[i].SerialSize);
                            continue;
                        }
                    }

                    ConvertExportToChildExportAndRead(reader, i);
                }
            }
        }

        /// <summary>
        /// Serializes the initial portion of the asset from memory.
        /// </summary>
        /// <returns>A byte array which represents the serialized binary data of the initial portion of the asset.</returns>
        private byte[] MakeHeader()
        {
            var stre = new MemoryStream(this.NameOffset);
            AssetBinaryWriter writer = new AssetBinaryWriter(stre, this);

            writer.Write(UAsset.UASSET_MAGIC);
            writer.Write(LegacyFileVersion);
            if (LegacyFileVersion != 4)
            {
                writer.Write(IsUnversioned ? 0 : 864);
            }

            if (IsUnversioned)
            {
                writer.Write(0);
            }
            else
            {
                writer.Write((int)ObjectVersion);
            }

            if (LegacyFileVersion <= -8)
            {
                if (IsUnversioned)
                {
                    writer.Write(0);
                }
                else
                {
                    writer.Write((int)ObjectVersionUE5);
                }
            }

            writer.Write(FileVersionLicenseeUE);
            if (LegacyFileVersion <= -2)
            {
                if (IsUnversioned)
                {
                    writer.Write(0);
                }
                else
                {
                    writer.WriteCustomVersionContainer(CustomVersionSerializationFormat, CustomVersionContainer);
                }
            }

            writer.Write(SectionSixOffset);
            writer.Write(FolderName);
            writer.Write((uint)PackageFlags);
            writer.Write(NameCount);
            writer.Write(NameOffset);
            if (ObjectVersionUE5 >= ObjectVersionUE5.ADD_SOFTOBJECTPATH_LIST)
            {
                writer.Write(SoftObjectPathsCount);
                writer.Write(SoftObjectPathsOffset);
            }
            if (ObjectVersion >= ObjectVersion.VER_UE4_SERIALIZE_TEXT_IN_PACKAGES)
            {
                writer.Write(GatherableTextDataCount);
                writer.Write(GatherableTextDataOffset);
            }
            writer.Write(ExportCount);
            writer.Write(ExportOffset); // 61
            writer.Write(ImportCount); // 65
            writer.Write(ImportOffset); // 69 (haha funny)
            writer.Write(DependsOffset); // 73
            if (ObjectVersion >= ObjectVersion.VER_UE4_ADD_STRING_ASSET_REFERENCES_MAP)
            {
                writer.Write(SoftPackageReferencesCount); // 77
                writer.Write(SoftPackageReferencesOffset); // 81
            }
            if (ObjectVersion >= ObjectVersion.VER_UE4_ADDED_SEARCHABLE_NAMES)
            {
                writer.Write(SearchableNamesOffset);
            }
            writer.Write(ThumbnailTableOffset);

            if (ValorantGarbageData != null && ValorantGarbageData.Length > 0) writer.Write(ValorantGarbageData);

            writer.Write(PackageGuid.ToByteArray());
            writer.Write(Generations.Count);
            for (int i = 0; i < Generations.Count; i++)
            {
                Generations[i].ExportCount = ExportCount;
                Generations[i].NameCount = NameCount;
                writer.Write(Generations[i].ExportCount);
                writer.Write(Generations[i].NameCount);
            }

            if (ObjectVersion >= ObjectVersion.VER_UE4_ENGINE_VERSION_OBJECT)
            {
                RecordedEngineVersion.Write(writer);
            }
            else
            {
                writer.Write(RecordedEngineVersion.Changelist);
            }

            if (ObjectVersion >= ObjectVersion.VER_UE4_PACKAGE_SUMMARY_HAS_COMPATIBLE_ENGINE_VERSION)
            {
                RecordedCompatibleWithEngineVersion.Write(writer);
            }

            writer.Write(CompressionFlags);
            writer.Write((int)0); // numCompressedChunks
            writer.Write(PackageSource);
            writer.Write(AdditionalPackagesToCook.Count);
            for (int i = 0; i < AdditionalPackagesToCook.Count; i++)
            {
                writer.Write(AdditionalPackagesToCook[i]);
            }

            if (LegacyFileVersion > -7)
            {
                writer.Write((int)0); // numTextureAllocations
            }

            writer.Write(AssetRegistryDataOffset);
            writer.Write(BulkDataStartOffset);

            if (ObjectVersion >= ObjectVersion.VER_UE4_WORLD_LEVEL_INFO)
            {
                writer.Write(WorldTileInfoDataOffset);
            }

            if (ObjectVersion >= ObjectVersion.VER_UE4_CHANGED_CHUNKID_TO_BE_AN_ARRAY_OF_CHUNKIDS)
            {
                writer.Write(ChunkIDs.Length);
                for (int i = 0; i < ChunkIDs.Length; i++)
                {
                    writer.Write(ChunkIDs[i]);
                }
            }
            else if (ObjectVersion >= ObjectVersion.VER_UE4_ADDED_CHUNKID_TO_ASSETDATA_AND_UPACKAGE)
            {
                writer.Write(ChunkIDs[0]);
            }

            if (ObjectVersion >= ObjectVersion.VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS)
            {
                writer.Write(PreloadDependencyCount);
                writer.Write(PreloadDependencyOffset);
            }

            // ue5 stuff
            if (ObjectVersionUE5 >= ObjectVersionUE5.NAMES_REFERENCED_FROM_EXPORT_DATA)
            {
                writer.Write(NamesReferencedFromExportDataCount);
            }

            if (ObjectVersionUE5 >= ObjectVersionUE5.PAYLOAD_TOC)
            {
                writer.Write(PayloadTocOffset);
            }

            if (ObjectVersionUE5 >= ObjectVersionUE5.DATA_RESOURCES)
            {
                writer.Write(DataResourceOffset);
            }

            return stre.ToArray();
        }

        /// <summary>
        /// Serializes an asset from memory.
        /// </summary>
        /// <returns>A new MemoryStream containing the full binary data of the serialized asset.</returns>
        public override MemoryStream WriteData()
        {
            isSerializationTime = true;

            // resolve ancestries
            ResolveAncestries();

            var stre = new MemoryStream();
            try
            {
                AssetBinaryWriter writer = new AssetBinaryWriter(stre, this);

                // Header
                writer.Seek(0, SeekOrigin.Begin);
                writer.Write(MakeHeader());

                // Name map
                this.NameOffset = (int)writer.BaseStream.Position;
                this.NameCount = this.nameMapIndexList.Count;
                for (int i = 0; i < this.nameMapIndexList.Count; i++)
                {
                    // this is not really the right custom version, i don't think the change was documented but it was in 4.23
                    bool disableCasePreservingHash = !nameMapIndexList[i].IsCasePreserving && this.GetCustomVersion<FReleaseObjectVersion>() < FReleaseObjectVersion.PropertiesSerializeRepCondition;
                    writer.Write(disableCasePreservingHash ? CRCGenerator.ToLower(nameMapIndexList[i], false) : nameMapIndexList[i]);

                    if (WillSerializeNameHashes == true || (WillSerializeNameHashes == null && ObjectVersion >= ObjectVersion.VER_UE4_NAME_HASHES_SERIALIZED))
                    {
                        if (OverrideNameMapHashes != null && OverrideNameMapHashes.ContainsKey(nameMapIndexList[i]))
                        {
                            writer.Write(OverrideNameMapHashes[nameMapIndexList[i]]);
                        }
                        else
                        {
                            writer.Write(CRCGenerator.GenerateHash(nameMapIndexList[i], disableCasePreservingHash));
                        }
                    }
                }

                // Imports
                if (this.Imports.Count > 0)
                {
                    this.ImportOffset = (int)writer.BaseStream.Position;
                    this.ImportCount = this.Imports.Count;
                    for (int i = 0; i < this.Imports.Count; i++)
                    {
                        writer.Write(this.Imports[i].ClassPackage);
                        writer.Write(this.Imports[i].ClassName);
                        writer.Write(this.Imports[i].OuterIndex.Index);
                        writer.Write(this.Imports[i].ObjectName);
                        if (writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.OPTIONAL_RESOURCES) writer.Write(this.Imports[i].bImportOptional ? 1 : 0);
                    }
                }
                else
                {
                    this.ImportOffset = 0;
                }

                // Export details
                if (this.Exports.Count > 0)
                {
                    this.ExportOffset = (int)writer.BaseStream.Position;
                    this.ExportCount = this.Exports.Count;
                    for (int i = 0; i < this.Exports.Count; i++)
                    {
                        Export us = this.Exports[i];
                        us.WriteExportMapEntry(writer);
                    }
                }
                else
                {
                    this.ExportOffset = 0;
                }

                // DependsMap
                if (this.doWeHaveDependsMap)
                {
                    this.DependsOffset = (ObjectVersion > ObjectVersion.VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS && ObjectVersion < ObjectVersion.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES) ? 0 : (int)writer.BaseStream.Position;
                    for (int i = 0; i < this.Exports.Count; i++)
                    {
                        if (i >= this.DependsMap.Count) this.DependsMap.Add(new int[0]);

                        int[] currentData = this.DependsMap[i];
                        writer.Write(currentData.Length);
                        for (int j = 0; j < currentData.Length; j++)
                        {
                            writer.Write(currentData[j]);
                        }
                    }
                }
                else
                {
                    this.DependsOffset = 0;
                    writer.Write((int)0);
                }

                // SoftPackageReferenceList
                if (this.doWeHaveSoftPackageReferences)
                {
                    this.SoftPackageReferencesOffset = (int)writer.BaseStream.Position;
                    this.SoftPackageReferencesCount = this.SoftPackageReferenceList.Count;
                    for (int i = 0; i < this.SoftPackageReferenceList.Count; i++)
                    {
                        writer.Write(this.SoftPackageReferenceList[i]);
                    }
                }
                else
                {
                    this.SoftPackageReferencesOffset = 0;
                }

                // AssetRegistryData
                if (this.doWeHaveAssetRegistryData)
                {
                    this.AssetRegistryDataOffset = (int)writer.BaseStream.Position;

                    /*writer.Write(this.AssetRegistryData.Count);
                    for (int i = 0; i < this.AssetRegistryData.Count; i++)
                    {
                        throw new NotImplementedException("Asset registry data is not yet supported. Please let me know if you see this error message");
                    }*/

                    writer.Write(AssetRegistryData);
                }
                else
                {
                    this.AssetRegistryDataOffset = 0;
                }

                // WorldTileInfo
                if (this.doWeHaveWorldTileInfo)
                {
                    this.WorldTileInfoDataOffset = (int)writer.BaseStream.Position;
                    WorldTileInfo.Write(writer, this);
                }
                else
                {
                    this.WorldTileInfoDataOffset = 0;
                }

                // PreloadDependencies
                this.PreloadDependencyOffset = (int)writer.BaseStream.Position;
                if (this.UseSeparateBulkDataFiles) this.UsesEventDrivenLoader = true;
                if (this.UsesEventDrivenLoader)
                {
                    this.PreloadDependencyCount = 0;
                    for (int i = 0; i < this.Exports.Count; i++)
                    {
                        Exports[i].FirstExportDependencyOffset = this.PreloadDependencyCount;

                        Exports[i].SerializationBeforeSerializationDependenciesSize = Exports[i].SerializationBeforeSerializationDependencies.Count;
                        for (int j = 0; j < Exports[i].SerializationBeforeSerializationDependenciesSize; j++) writer.Write(Exports[i].SerializationBeforeSerializationDependencies[j].Index);

                        Exports[i].CreateBeforeSerializationDependenciesSize = Exports[i].CreateBeforeSerializationDependencies.Count;
                        for (int j = 0; j < Exports[i].CreateBeforeSerializationDependenciesSize; j++) writer.Write(Exports[i].CreateBeforeSerializationDependencies[j].Index);

                        Exports[i].SerializationBeforeCreateDependenciesSize = Exports[i].SerializationBeforeCreateDependencies.Count;
                        for (int j = 0; j < Exports[i].SerializationBeforeCreateDependenciesSize; j++) writer.Write(Exports[i].SerializationBeforeCreateDependencies[j].Index);

                        Exports[i].CreateBeforeCreateDependenciesSize = Exports[i].CreateBeforeCreateDependencies.Count;
                        for (int j = 0; j < Exports[i].CreateBeforeCreateDependenciesSize; j++) writer.Write(Exports[i].CreateBeforeCreateDependencies[j].Index);

                        this.PreloadDependencyCount +=
                            Exports[i].SerializationBeforeSerializationDependencies.Count +
                            Exports[i].CreateBeforeSerializationDependencies.Count +
                            Exports[i].SerializationBeforeCreateDependencies.Count +
                            Exports[i].CreateBeforeCreateDependencies.Count;
                    }
                }
                else
                {
                    this.PreloadDependencyCount = -1;
                    for (int i = 0; i < this.Exports.Count; i++) Exports[i].FirstExportDependencyOffset = -1;
                }

                // Export data
                int oldOffset = this.SectionSixOffset;
                this.SectionSixOffset = (int)writer.BaseStream.Position;
                long[] categoryStarts = new long[this.Exports.Count];
                if (this.Exports.Count > 0)
                {
                    for (int i = 0; i < this.Exports.Count; i++)
                    {
                        categoryStarts[i] = writer.BaseStream.Position;
                        Export us = this.Exports[i];
                        us.Write(writer);
                        writer.Write(us.Extras);
                    }
                }
                writer.Write(UAsset.UASSET_MAGIC);

                this.BulkDataStartOffset = (int)stre.Length - 4;

                // Rewrite Section 3
                if (this.Exports.Count > 0)
                {
                    writer.Seek(this.ExportOffset, SeekOrigin.Begin);
                    for (int i = 0; i < this.Exports.Count; i++)
                    {
                        Export us = this.Exports[i];
                        long nextLoc = this.BulkDataStartOffset;
                        if ((this.Exports.Count - 1) > i) nextLoc = categoryStarts[i + 1];

                        us.SerialOffset = categoryStarts[i];
                        us.SerialSize = nextLoc - categoryStarts[i];

                        us.WriteExportMapEntry(writer);
                    }
                }

                // Rewrite Header
                writer.Seek(0, SeekOrigin.Begin);
                writer.Write(MakeHeader());

                writer.Seek(0, SeekOrigin.Begin);
            }
            finally
            {
                isSerializationTime = false;
            }
            return stre;
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

            if (this.UseSeparateBulkDataFiles && this.Exports.Count > 0)
            {
                long breakingOffPoint = this.Exports[0].SerialOffset;
                using (FileStream f = File.Open(outputPath, FileMode.Create, FileAccess.Write))
                {
                    CopySplitUp(newData, f, 0, (int)breakingOffPoint);
                }

                using (FileStream f = File.Open(Path.ChangeExtension(outputPath, "uexp"), FileMode.Create, FileAccess.Write))
                {
                    CopySplitUp(newData, f, (int)breakingOffPoint, (int)(newData.Length - breakingOffPoint));
                }
            }
            else
            {
                using (FileStream f = File.Open(outputPath, FileMode.Create, FileAccess.Write))
                {
                    newData.CopyTo(f);
                }
            }
        }

        /// <summary>
        /// Serializes this asset as JSON.
        /// </summary>
        /// <param name="isFormatted">Whether or not the returned JSON string should be indented.</param>
        /// <returns>A serialized JSON string that represents the asset.</returns>
        public string SerializeJson(bool isFormatted = false)
        {
            return SerializeJson(isFormatted ? Formatting.Indented : Formatting.None);
        }

        /// <summary>
        /// Serializes this asset as JSON.
        /// </summary>
        /// <param name="jsonFormatting">The formatting to use for the returned JSON string.</param>
        /// <returns>A serialized JSON string that represents the asset.</returns>
        public string SerializeJson(Formatting jsonFormatting)
        {
            Info = "Serialized with UAssetAPI " + typeof(PropertyData).Assembly.GetName().Version + (string.IsNullOrEmpty(UAPUtils.CurrentCommit) ? "" : (" (" + UAPUtils.CurrentCommit + ")"));
            return JsonConvert.SerializeObject(this, jsonFormatting, jsonSettings);
        }

        /// <summary>
        /// Serializes an object as JSON.
        /// </summary>
        /// <param name="value">The object to serialize as JSON.</param>
        /// <param name="isFormatted">Whether or not the returned JSON string should be indented.</param>
        /// <returns>A serialized JSON string that represents the object.</returns>
        public string SerializeJsonObject(object value, bool isFormatted = false)
        {
            return SerializeJsonObject(value, isFormatted ? Formatting.Indented : Formatting.None);
        }

        /// <summary>
        /// Serializes an object as JSON.
        /// </summary>
        /// <param name="value">The object to serialize as JSON.</param>
        /// <param name="jsonFormatting">The formatting to use for the returned JSON string.</param>
        /// <returns>A serialized JSON string that represents the object.</returns>
        public string SerializeJsonObject(object value, Formatting jsonFormatting)
        {
            return JsonConvert.SerializeObject(value, jsonFormatting, jsonSettings);
        }

        /// <summary>
        /// Deserializes an object from JSON.
        /// </summary>
        /// <param name="json">A serialized JSON string to parse.</param>
        public object DeserializeJsonObject(string json)
        {
            Dictionary<FName, string> toBeFilled = new Dictionary<FName, string>();
            object res = JsonConvert.DeserializeObject(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                NullValueHandling = NullValueHandling.Include,
                FloatParseHandling = FloatParseHandling.Double,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new UAssetContractResolver(toBeFilled),
                Converters = new List<JsonConverter>()
                {
                    new FSignedZeroJsonConverter(),
                    new FNameJsonConverter(null),
                    new FStringTableJsonConverter(),
                    new FStringJsonConverter(),
                    new FPackageIndexJsonConverter(),
                    new StringEnumConverter(),
                    new GuidJsonConverter(),
                }
            });

            foreach (KeyValuePair<FName, string> entry in toBeFilled)
            {
                entry.Key.Asset = this;
                if (FName.IsFromStringValid(this, entry.Value))
                {
                    var dummy = FName.FromString(this, entry.Value);
                    entry.Key.Value = dummy.Value;
                    entry.Key.Number = dummy.Number;
                }
                else
                {
                    entry.Key.DummyValue = FString.FromString(entry.Value);
                    entry.Key.Number = 0;
                }
            }
            toBeFilled.Clear();

            return res;
        }

        /// <summary>
        /// Reads an asset from serialized JSON and initializes a new instance of the <see cref="UAsset"/> class to store its data in memory.
        /// </summary>
        /// <param name="json">A serialized JSON string to parse.</param>
        public static UAsset DeserializeJson(string json)
        {
            Dictionary<FName, string> toBeFilled = new Dictionary<FName, string>();
            UAsset res = JsonConvert.DeserializeObject<UAsset>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                NullValueHandling = NullValueHandling.Include,
                FloatParseHandling = FloatParseHandling.Double,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new UAssetContractResolver(toBeFilled),
                Converters = new List<JsonConverter>()
                {
                    new FSignedZeroJsonConverter(),
                    new FNameJsonConverter(null),
                    new FStringTableJsonConverter(),
                    new FStringJsonConverter(),
                    new FPackageIndexJsonConverter(),
                    new StringEnumConverter(),
                    new GuidJsonConverter(),
                }
            });

            foreach (KeyValuePair<FName, string> entry in toBeFilled)
            {
                entry.Key.Asset = res;
                if (FName.IsFromStringValid(res, entry.Value))
                {
                    var dummy = FName.FromString(res, entry.Value);
                    entry.Key.Value = dummy.Value;
                    entry.Key.Number = dummy.Number;
                }
                else
                {
                    entry.Key.DummyValue = FString.FromString(entry.Value);
                    entry.Key.Number = 0;
                }
            }
            toBeFilled.Clear();

            foreach (Export ex in res.Exports) ex.Asset = res;

            res.ResolveAncestries();
            return res;
        }

        /// <summary>
        /// Reads an asset from serialized JSON and initializes a new instance of the <see cref="UAsset"/> class to store its data in memory.
        /// </summary>
        /// <param name="stream">A stream containing serialized JSON string to parse.</param>
        public static UAsset DeserializeJson(Stream stream)
        {
            Dictionary<FName, string> toBeFilled = new Dictionary<FName, string>();
            var serializer = JsonSerializer.Create(new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                NullValueHandling = NullValueHandling.Include,
                FloatParseHandling = FloatParseHandling.Double,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new UAssetContractResolver(toBeFilled),
                Converters = new List<JsonConverter>()
                {
                    new FSignedZeroJsonConverter(),
                    new FNameJsonConverter(null),
                    new FStringTableJsonConverter(),
                    new FStringJsonConverter(),
                    new FPackageIndexJsonConverter(),
                    new StringEnumConverter(),
                    new GuidJsonConverter(),
                }
            });

            UAsset res;

            using (var sr = new StreamReader(stream))
            {
                using (var jsonTextReader = new JsonTextReader(sr))
                {
                    res = serializer.Deserialize<UAsset>(jsonTextReader);
                }
            }

            foreach (KeyValuePair<FName, string> entry in toBeFilled)
            {
                entry.Key.Asset = res;
                if (FName.IsFromStringValid(res, entry.Value))
                {
                    var dummy = FName.FromString(res, entry.Value);
                    entry.Key.Value = dummy.Value;
                    entry.Key.Number = dummy.Number;
                }
                else
                {
                    entry.Key.DummyValue = FString.FromString(entry.Value);
                    entry.Key.Number = 0;
                }
            }
            toBeFilled.Clear();

            foreach (Export ex in res.Exports) ex.Asset = res;
            return res;
        }

        /// <summary>
        /// Reads an asset from disk and initializes a new instance of the <see cref="UAsset"/> class to store its data in memory.
        /// </summary>
        /// <param name="path">The path of the asset file on disk that this instance will read from.</param>
        /// <param name="engineVersion">The version of the Unreal Engine that will be used to parse this asset. If the asset is versioned, this can be left unspecified.</param>
        /// <param name="mappings">A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public UAsset(string path, EngineVersion engineVersion = EngineVersion.UNKNOWN, Usmap mappings = null)
        {
            this.FilePath = path;
            this.Mappings = mappings;
            SetEngineVersion(engineVersion);

            Read(PathToReader(path));
        }

        /// <summary>
        /// Reads an asset from a BinaryReader and initializes a new instance of the <see cref="UAsset"/> class to store its data in memory.
        /// </summary>
        /// <param name="reader">The asset's BinaryReader that this instance will read from. If a .uexp file exists, the .uexp file's data should be appended to the end of the .uasset file's data.</param>
        /// <param name="engineVersion">The version of the Unreal Engine that will be used to parse this asset. If the asset is versioned, this can be left unspecified.</param>
        /// <param name="mappings">A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.</param>
        /// <param name="useSeparateBulkDataFiles">Does this asset uses separate bulk data files (.uexp, .ubulk)?</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public UAsset(AssetBinaryReader reader, EngineVersion engineVersion = EngineVersion.UNKNOWN, Usmap mappings = null, bool useSeparateBulkDataFiles = false)
        {
            this.Mappings = mappings;
            UseSeparateBulkDataFiles = useSeparateBulkDataFiles;
            SetEngineVersion(engineVersion);
            Read(reader);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UAsset"/> class. This instance will store no asset data and does not represent any asset in particular until the <see cref="Read"/> method is manually called.
        /// </summary>
        /// <param name="engineVersion">The version of the Unreal Engine that will be used to parse this asset. If the asset is versioned, this can be left unspecified.</param>
        /// <param name="mappings">A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.</param>
        public UAsset(EngineVersion engineVersion = EngineVersion.UNKNOWN, Usmap mappings = null)
        {
            this.Mappings = mappings;
            SetEngineVersion(engineVersion);
        }

        /// <summary>
        /// Reads an asset from disk and initializes a new instance of the <see cref="UAsset"/> class to store its data in memory.
        /// </summary>
        /// <param name="path">The path of the asset file on disk that this instance will read from.</param>
        /// <param name="objectVersion">The UE4 object version of the Unreal Engine that will be used to parse this asset.</param>
        /// <param name="objectVersionUE5">The UE5 object version of the Unreal Engine that will be used to parse this asset.</param>
        /// <param name="customVersionContainer">A list of custom versions to parse this asset with.</param>
        /// <param name="mappings">A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public UAsset(string path, ObjectVersion objectVersion, ObjectVersionUE5 objectVersionUE5, List<CustomVersion> customVersionContainer, Usmap mappings = null)
        {
            this.FilePath = path;
            this.Mappings = mappings;
            ObjectVersion = objectVersion;
            ObjectVersionUE5 = objectVersionUE5;
            if (customVersionContainer != null) CustomVersionContainer = customVersionContainer;

            Read(PathToReader(path));
        }

        /// <summary>
        /// Reads an asset from a BinaryReader and initializes a new instance of the <see cref="UAsset"/> class to store its data in memory.
        /// </summary>
        /// <param name="reader">The asset's BinaryReader that this instance will read from.</param>
        /// <param name="objectVersion">The UE4 object version of the Unreal Engine that will be used to parse this asset.</param>
        /// <param name="objectVersionUE5">The UE5 object version of the Unreal Engine that will be used to parse this asset.</param>
        /// <param name="customVersionContainer">A list of custom versions to parse this asset with.</param>
        /// <param name="mappings">A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.</param>
        /// <param name="useSeparateBulkDataFiles">Does this asset uses separate bulk data files (.uexp, .ubulk)?</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public UAsset(AssetBinaryReader reader, ObjectVersion objectVersion, ObjectVersionUE5 objectVersionUE5, List<CustomVersion> customVersionContainer, Usmap mappings = null, bool useSeparateBulkDataFiles = false)
        {
            this.Mappings = mappings;
            UseSeparateBulkDataFiles = useSeparateBulkDataFiles;
            ObjectVersion = objectVersion;
            ObjectVersionUE5 = objectVersionUE5;
            if (customVersionContainer != null) CustomVersionContainer = customVersionContainer;

            Read(reader);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UAsset"/> class. This instance will store no asset data and does not represent any asset in particular until the <see cref="Read"/> method is manually called.
        /// </summary>
        /// <param name="objectVersion">The UE4 object version of the Unreal Engine that will be used to parse this asset.</param>
        /// <param name="objectVersionUE5">The UE5 object version of the Unreal Engine that will be used to parse this asset.</param>
        /// <param name="customVersionContainer">A list of custom versions to parse this asset with.</param>
        /// <param name="mappings">A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.</param>
        public UAsset(ObjectVersion objectVersion, ObjectVersionUE5 objectVersionUE5, List<CustomVersion> customVersionContainer, Usmap mappings = null)
        {
            this.Mappings = mappings;
            ObjectVersion = objectVersion;
            ObjectVersionUE5 = objectVersionUE5;
            if (customVersionContainer != null) CustomVersionContainer = customVersionContainer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UAsset"/> class. This instance will store no asset data and does not represent any asset in particular until the <see cref="Read"/> method is manually called.
        /// </summary>
        public UAsset()
        {

        }
    }
}
