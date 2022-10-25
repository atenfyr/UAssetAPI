using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UAssetAPI.FieldTypes;
using UAssetAPI.JSON;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

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

        public void Write(AssetBinaryWriter writer)
        {
            writer.Write(Major);
            writer.Write(Minor);
            writer.Write(Patch);
            writer.Write(Changelist);
            writer.Write(Branch);
        }

        public FEngineVersion(AssetBinaryReader reader)
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
    public class UAsset
    {
        /// <summary>
        /// Agent string to provide context in serialized JSON.
        /// </summary>
        [JsonProperty(Order = -99)]
        public string Info = "Serialized with UAssetAPI";

        /// <summary>
        /// The path of the file on disk that this asset represents. This does not need to be specified for regular parsing.
        /// </summary>
        [JsonIgnore]
        public string FilePath;

        /// <summary>
        /// Should the asset be split into separate .uasset, .uexp, and .ubulk files, as opposed to one single .uasset file?
        /// </summary>
        public bool UseSeparateBulkDataFiles = false;

        /// <summary>
        /// The object version of the Unreal Engine that will be used to parse this asset.
        /// </summary>
        public ObjectVersion ObjectVersion = ObjectVersion.UNKNOWN;

        /// <summary>
        /// All the custom versions stored in the archive.
        /// </summary>
        public List<CustomVersion> CustomVersionContainer = null;

        /// <summary>
        /// Sets the version of the Unreal Engine to use in serialization.
        /// </summary>
        /// <param name="newVersion">The new version of the Unreal Engine to use in serialization.</param>
        /// <exception cref="InvalidOperationException">Thrown when an invalid UE4Version is specified.</exception>
        public void SetEngineVersion(UE4Version newVersion)
        {
            if (newVersion == UE4Version.UNKNOWN) return;
            if (!Enum.TryParse(Enum.GetName(typeof(UE4Version), newVersion), out UE4VersionToObjectVersion bridgeVer)) throw new InvalidOperationException("Invalid engine version specified");
            ObjectVersion = (ObjectVersion)(int)bridgeVer;
            CustomVersionContainer = GetDefaultCustomVersionContainer(newVersion);
        }

        /// <summary>
        /// Estimates the retail version of the Unreal Engine based on the object and custom versions.
        /// </summary>
        /// <returns>The estimated retail version of the Unreal Engine.</returns>
        public UE4Version GetEngineVersion()
        {
            // analyze all possible versions based off of the object version alone
            List<UE4Version> allPossibleVersions = new List<UE4Version>();
            int targetVer = (int)ObjectVersion;
            while (allPossibleVersions.Count == 0 && targetVer > (int)ObjectVersion.VER_UE4_OLDEST_LOADABLE_PACKAGE)
            {
                allPossibleVersions = Enum.GetNames(typeof(UE4VersionToObjectVersion)).Where(n => ((int)Enum.Parse(typeof(UE4VersionToObjectVersion), n)).Equals(targetVer)).Select(str => (UE4Version)Enum.Parse(typeof(UE4Version), str)).ToList();
                targetVer -= 1;
            }

            if (allPossibleVersions.Count == 0) return UE4Version.UNKNOWN;
            if (allPossibleVersions.Count == 1) return allPossibleVersions[0];

            // multiple possible versions; use custom versions to eliminate some
            UE4Version minIntroduced = UE4Version.VER_UE4_OLDEST_LOADABLE_PACKAGE;
            UE4Version maxIntroduced = UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE;
            foreach (CustomVersion entry in CustomVersionContainer)
            {
                Type customVersionType = Type.GetType("UAssetAPI." + MainSerializer.allNonLetters.Replace(entry.FriendlyName, string.Empty));
                if (customVersionType == null) continue;
                UE4Version minIntroducedThis = GetIntroducedFromCustomVersionValue(customVersionType, entry.Version); // inclusive
                UE4Version maxIntroducedThis = GetIntroducedFromCustomVersionValue(customVersionType, entry.Version + 1); // exclusive

                if (minIntroducedThis != UE4Version.UNKNOWN && minIntroducedThis > minIntroduced) minIntroduced = minIntroducedThis;
                if (maxIntroducedThis != UE4Version.UNKNOWN && maxIntroducedThis < maxIntroduced) maxIntroduced = maxIntroducedThis;
            }

            List<UE4Version> finalPossibleVersions = new List<UE4Version>();
            foreach (UE4Version entry in allPossibleVersions)
            {
                if (entry >= minIntroduced && entry < maxIntroduced) finalPossibleVersions.Add(entry);
            }
            finalPossibleVersions.Sort();

            if (finalPossibleVersions.Count == 0) return allPossibleVersions[0]; // there must be a special set of custom versions; we'll just ignore our intuitions and go with the object version alone
            if (finalPossibleVersions.Count >= 1) return finalPossibleVersions[0];
            return UE4Version.UNKNOWN;
        }

        private UE4Version GetIntroducedFromCustomVersionValue(Type customVersionType, int val)
        {
            var nm = Enum.GetName(customVersionType, val);
            if (nm == null) return UE4Version.UNKNOWN;
            var attributes = customVersionType.GetMember(nm)?[0]?.GetCustomAttributes(typeof(IntroducedAttribute), false);
            if (attributes == null || attributes.Length <= 0) return UE4Version.UNKNOWN;
            return ((IntroducedAttribute)attributes[0]).IntroducedVersion;
        }

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

        private void FixNameMapLookupIfNeeded()
        {
            if (nameMapIndexList.Count > 0 && nameMapLookup.Count == 0)
            {
                for (int i = 0; i < nameMapIndexList.Count; i++)
                {
                    nameMapLookup[nameMapIndexList[i].Value] = i;
                }
            }
        }

        /// <summary>
        /// Returns the name map as a read-only list of FStrings.
        /// </summary>
        /// <returns>The name map as a read-only list of FStrings.</returns>
        public IReadOnlyList<FString> GetNameMapIndexList()
        {
            FixNameMapLookupIfNeeded();
            return nameMapIndexList.AsReadOnly();
        }

        /// <summary>
        /// Clears the name map. This method should be used with extreme caution, as it may break unparsed references to the name map.
        /// </summary>
        public void ClearNameIndexList()
        {
            nameMapIndexList = new List<FString>();
            nameMapLookup = new Dictionary<string, int>();
        }

        /// <summary>
        /// Replaces a value in the name map at a particular index.
        /// </summary>
        /// <param name="index">The index to overwrite in the name map.</param>
        /// <param name="value">The value that will be replaced in the name map.</param>
        public void SetNameReference(int index, FString value)
        {
            FixNameMapLookupIfNeeded();
            nameMapIndexList[index] = value;
            nameMapLookup[value.Value] = index;
        }

        /// <summary>
        /// Gets a value in the name map at a particular index.
        /// </summary>
        /// <param name="index">The index to return the value at.</param>
        /// <returns>The value at the index provided.</returns>
        public FString GetNameReference(int index)
        {
            FixNameMapLookupIfNeeded();
            if (index < 0) return new FString(Convert.ToString(-index));
            if (index >= nameMapIndexList.Count) return new FString(Convert.ToString(index));
            return nameMapIndexList[index];
        }

        /// <summary>
        /// Gets a value in the name map at a particular index, but with the index zero being treated as if it is not valid.
        /// </summary>
        /// <param name="index">The index to return the value at.</param>
        /// <returns>The value at the index provided.</returns>
        public FString GetNameReferenceWithoutZero(int index)
        {
            FixNameMapLookupIfNeeded();
            if (index <= 0) return new FString(Convert.ToString(-index));
            if (index >= nameMapIndexList.Count) return new FString(Convert.ToString(index));
            return nameMapIndexList[index];
        }

        /// <summary>
        /// Checks whether or not the value exists in the name map.
        /// </summary>
        /// <param name="search">The value to search the name map for.</param>
        /// <returns>true if the value appears in the name map, otherwise false.</returns>
        public bool ContainsNameReference(FString search)
        {
            FixNameMapLookupIfNeeded();
            return nameMapLookup.ContainsKey(search.Value);
        }

        /// <summary>
        /// Searches the name map for a particular value.
        /// </summary>
        /// <param name="search">The value to search the name map for.</param>
        /// <returns>The index at which the value appears in the name map.</returns>
        /// <exception cref="UAssetAPI.NameMapOutOfRangeException">Thrown when the value provided does not appear in the name map.</exception>
        public int SearchNameReference(FString search)
        {
            FixNameMapLookupIfNeeded();
            if (ContainsNameReference(search)) return nameMapLookup[search.Value];
            throw new NameMapOutOfRangeException(search);
        }

        /// <summary>
        /// Adds a new value to the name map.
        /// </summary>
        /// <param name="name">The value to add to the name map.</param>
        /// <param name="forceAddDuplicates">Whether or not to add a new entry if the value provided already exists in the name map.</param>
        /// <returns>The index of the new value in the name map. If the value already existed in the name map beforehand, that index will be returned instead.</returns>
        /// <exception cref="ArgumentException">Thrown when forceAddDuplicates is false and the value provided is null or empty.</exception>
        public int AddNameReference(FString name, bool forceAddDuplicates = false)
        {
            FixNameMapLookupIfNeeded();

            if (!forceAddDuplicates)
            {
                if (name?.Value == null) throw new ArgumentException("Cannot add a null FString to the name map");
                if (name.Value == string.Empty) throw new ArgumentException("Cannot add an empty FString to the name map");
                if (ContainsNameReference(name)) return SearchNameReference(name);
            }

            if (isSerializationTime) throw new InvalidOperationException("Attempt to add name \"" + name + "\" to name map during serialization time");
            nameMapIndexList.Add(name);
            nameMapLookup[name.Value] = nameMapIndexList.Count - 1;
            return nameMapIndexList.Count - 1;
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
        /// Searches for and returns this asset's ClassExport, if one exists.
        /// </summary>
        /// <returns>The asset's ClassExport if one exists, otherwise null.</returns>
        public ClassExport GetClassExport()
        {
            foreach (Export cat in Exports)
            {
                if (cat is ClassExport bgcCat) return bgcCat;
            }
            return null;
        }

        /// <summary>
        /// Finds the class path and export name of the SuperStruct of this asset, if it exists.
        /// </summary>
        /// <param name="parentClassPath">The class path of the SuperStruct of this asset, if it exists.</param>
        /// <param name="parentClassExportName">The export name of the SuperStruct of this asset, if it exists.</param>
        public void GetParentClass(out FName parentClassPath, out FName parentClassExportName)
        {
            parentClassPath = null;
            parentClassExportName = null;

            var bgcCat = GetClassExport();
            if (bgcCat == null) return;

            Import parentClassLink = bgcCat.SuperStruct.ToImport(this);
            if (parentClassLink == null) return;
            if (parentClassLink.OuterIndex.Index >= 0) return;

            parentClassExportName = parentClassLink.ObjectName;
            parentClassPath = parentClassLink.OuterIndex.ToImport(this).ObjectName;
        }

        /// <summary>
        /// Fetches the version of a custom version in this asset.
        /// </summary>
        /// <param name="key">The GUID of the custom version to retrieve.</param>
        /// <returns>The version of the retrieved custom version.</returns>
        public int GetCustomVersion(Guid key)
        {
            for (int i = 0; i < CustomVersionContainer.Count; i++)
            {
                CustomVersion custVer = CustomVersionContainer[i];
                if (custVer.Key == key)
                {
                    return custVer.Version;
                }
            }

            return -1; // https://github.com/EpicGames/UnrealEngine/blob/99b6e203a15d04fc7bbbf554c421a985c1ccb8f1/Engine/Source/Runtime/Core/Private/Serialization/Archive.cpp#L578
        }

        /// <summary>
        /// Fetches the version of a custom version in this asset.
        /// </summary>
        /// <param name="friendlyName">The friendly name of the custom version to retrieve.</param>
        /// <returns>The version of the retrieved custom version.</returns>
        public int GetCustomVersion(string friendlyName)
        {
            for (int i = 0; i < CustomVersionContainer.Count; i++)
            {
                CustomVersion custVer = CustomVersionContainer[i];
                if (custVer.FriendlyName == friendlyName)
                {
                    return custVer.Version;
                }
            }

            return -1;
        }

        /// <summary>
        /// Fetches a custom version's enum value based off of its type.
        /// </summary>
        /// <typeparam name="T">The enum type of the custom version to retrieve.</typeparam>
        /// <returns>The enum value of the requested custom version.</returns>
        /// <exception cref="ArgumentException">Thrown when T is not an enumerated type.</exception>
        public T GetCustomVersion<T>()
        {
            Type customVersionEnumType = typeof(T);
            if (!customVersionEnumType.IsEnum) throw new ArgumentException("T must be an enumerated type");

            for (int i = 0; i < CustomVersionContainer.Count; i++)
            {
                CustomVersion custVer = CustomVersionContainer[i];
                if (custVer.FriendlyName == customVersionEnumType.Name)
                {
                    return (T)(object)custVer.Version;
                }
            }

            return (T)(object)-1;
        }

        private static int GuessCustomVersionFromTypeAndEngineVersion(UE4Version chosenVersion, Type typ)
        {
            string[] allVals = Enum.GetNames(typ);
            for (int i = allVals.Length - 1; i >= 0; i--)
            {
                string val = allVals[i];
                var attributes = typ.GetMember(val)?[0]?.GetCustomAttributes(typeof(IntroducedAttribute), false);
                if (attributes == null || attributes.Length <= 0) continue;
                if (chosenVersion >= ((IntroducedAttribute)attributes[0]).IntroducedVersion) return i;
            }
            return -1;
        }

        /// <summary>
        /// Fetches a list of all default custom versions for a specific Unreal version.
        /// </summary>
        /// <param name="chosenVersion">The version of the engine to check against.</param>
        /// <returns>A list of all the default custom version values for the given engine version.</returns>
        public static List<CustomVersion> GetDefaultCustomVersionContainer(UE4Version chosenVersion)
        {
            List<CustomVersion> res = new List<CustomVersion>();
            foreach (KeyValuePair<Guid, string> entry in CustomVersion.GuidToCustomVersionStringMap)
            {
                Type customVersionType = Type.GetType("UAssetAPI." + entry.Value);
                if (customVersionType == null) continue;
                int guessedCustomVersion = GuessCustomVersionFromTypeAndEngineVersion(chosenVersion, customVersionType);
                if (guessedCustomVersion < 0) continue;
                res.Add(new CustomVersion(entry.Key, guessedCustomVersion));
            }
            return res;
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
        ///     </list>
        /// </remarks>
        public int LegacyFileVersion;

        /// <summary>
        /// Should this asset not serialize its engine and custom versions?
        /// </summary>
        public bool IsUnversioned;

        /// <summary>
        /// The licensee file version. Used by some games to add their own Engine-level versioning.
        /// </summary>
        public int FileVersionLicenseeUE4;

        /// <summary>
        /// Map of object imports. UAssetAPI used to call these "links."
        /// </summary>
        public List<Import> Imports;

        /// <summary>
        /// Map of object exports. UAssetAPI used to call these "categories."
        /// </summary>
        public List<Export> Exports;

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
        /// Tile information used by WorldComposition.
        /// Defines properties necessary for tile positioning in the world.
        /// </summary>
        public FWorldTileInfo WorldTileInfo;

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
        /// The flags for this package.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EPackageFlags PackageFlags;

        /// <summary>
        /// Value that is used by the Unreal Engine to determine if the package was saved by Epic, a licensee, modder, etc.
        /// </summary>
        public uint PackageSource;

        /// <summary>
        /// The Generic Browser folder name that this package lives in. Usually "None" in cooked assets.
        /// </summary>
        public FString FolderName;

        /// <summary>
        /// In MapProperties that have StructProperties as their keys or values, there is no universal, context-free way to determine the type of the struct.
        /// <para />
        /// To that end, this dictionary maps MapProperty names to the type of the structs within them (tuple of key struct type and value struct type) if they are not None-terminated property lists.
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, Tuple<FString, FString>> MapStructTypeOverride = new Dictionary<string, Tuple<FString, FString>>()
        {
            { "ColorDatabase", new Tuple<FString, FString>(null, new FString("LinearColor")) },
            { "PlayerCharacterIDs", new Tuple<FString, FString>(new FString("Guid"), null) },
            { "m_PerConditionValueToNodeMap", new Tuple<FString, FString>(new FString("Guid"), null) },
            { "BindingIdToReferences", new Tuple<FString, FString>(new FString("Guid"), null) },
            { "UserParameterRedirects", new Tuple<FString, FString>(new FString("NiagaraVariable"), new FString("NiagaraVariable"))},
            { "Tracks", new Tuple<FString, FString>(new FString("MovieSceneTrackIdentifier"), null)},
            { "SubSequences", new Tuple<FString, FString>(new FString("MovieSceneSequenceID"), null)},
            { "Hierarchy", new Tuple<FString, FString>(new FString("MovieSceneSequenceID"), null)},
            { "TrackSignatureToTrackIdentifier", new Tuple<FString, FString>(new FString("Guid"), new FString("MovieSceneTrackIdentifier"))},
            { "ItemsToRefund", new Tuple<FString, FString>(new FString("Guid"), null) },
            { "PlayerCharacterIDMap", new Tuple<FString, FString>(new FString("Guid"), null) },
            { "RainChanceMinMaxPerWeatherState", new Tuple<FString, FString>(null, new FString("FloatRange")) }
        };

        /// <summary>
        /// IN ENGINE VERSIONS BEFORE <see cref="ObjectVersion.VER_UE4_INNER_ARRAY_TAG_INFO"/>:
        /// <para />
        /// In ArrayProperties that have StructProperties as their keys or values, there is no universal, context-free way to determine the type of the struct. To that end, this dictionary maps ArrayProperty names to the type of the structs within them.
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, FString> ArrayStructTypeOverride = new Dictionary<string, FString>()
        {
            { "Keys", new FString("RichCurveKey") }
        };

        /// <summary>
        /// External programs often improperly specify name map hashes, so in this map we can preserve those changes to avoid confusion.
        /// </summary>
        [JsonIgnore]
        public Dictionary<FString, uint> OverrideNameMapHashes;

        /// <summary>This is called "TotalHeaderSize" in UE4 where header refers to the whole summary, whereas in UAssetAPI "header" refers to just the data before the start of the name map</summary>
        internal int SectionSixOffset = 0;

        /// <summary>Number of names used in this package</summary>
        internal int NameCount = 0;

        /// <summary>Location into the file on disk for the name data</summary>
        internal int NameOffset;

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
        internal bool doWeHaveDependsMap = true;
        [JsonProperty]
        internal bool doWeHaveSoftPackageReferences = true;
        [JsonProperty]
        internal bool doWeHaveAssetRegistryData = true;
        [JsonProperty]
        internal bool doWeHaveWorldTileInfo = true;
        [JsonIgnore]
        internal bool isSerializationTime = false;

        /// <summary>
        /// Internal list of name map entries. Do not directly add values to here under any circumstances; use <see cref="AddNameReference"/> instead
        /// </summary>
        [JsonProperty("NameMap", Order = -2)]
        private List<FString> nameMapIndexList;

        /// <summary>
        /// Internal lookup for name map entries. Do not directly add values to here under any circumstances; use <see cref="AddNameReference"/> instead
        /// </summary>
        private Dictionary<string, int> nameMapLookup = new Dictionary<string, int>();

        /// <summary>
        /// Copies a portion of a stream to another stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="output">The output stream.</param>
        /// <param name="start">The offset in the input stream to start copying from.</param>
        /// <param name="leng">The length in bytes of the data to be copied.</param>
        private static void CopySplitUp(Stream input, Stream output, int start, int leng)
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
                if (ObjectVersion == ObjectVersion.UNKNOWN) throw new UnknownEngineVersionException("Cannot begin serialization of an unversioned asset before an object version is manually specified");
            }

            FileVersionLicenseeUE4 = reader.ReadInt32();

            // Custom versions container
            if (LegacyFileVersion <= -2)
            {
                // TODO: support for enum-based custom versions
                var newCustomVersionContainer = new List<CustomVersion>();
                var existingCustomVersions = new HashSet<Guid>();
                int numCustomVersions = reader.ReadInt32();
                for (int i = 0; i < numCustomVersions; i++)
                {
                    var customVersionID = new Guid(reader.ReadBytes(16));
                    var customVersionNumber = reader.ReadInt32();
                    newCustomVersionContainer.Add(new CustomVersion(customVersionID, customVersionNumber));
                    existingCustomVersions.Add(customVersionID);
                }

                if (CustomVersionContainer != null)
                {
                    foreach (CustomVersion entry in CustomVersionContainer)
                    {
                        if (!existingCustomVersions.Contains(entry.Key)) newCustomVersionContainer.Add(entry);
                    }
                }

                CustomVersionContainer = newCustomVersionContainer;
            }

            SectionSixOffset = reader.ReadInt32(); // 24
            FolderName = reader.ReadFString();
            PackageFlags = (EPackageFlags)reader.ReadUInt32();
            NameCount = reader.ReadInt32();
            NameOffset = reader.ReadInt32();
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

            PackageGuid = new Guid(reader.ReadBytes(16));

            Generations = new List<FGenerationInfo>();
            int generationCount = reader.ReadInt32();
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
        }

        /// <summary>
        /// Reads an asset into memory.
        /// </summary>
        /// <param name="reader">The input reader.</param>
        /// <param name="manualSkips">An array of export indexes to skip parsing. For most applications, this should be left blank.</param>
        /// <param name="forceReads">An array of export indexes that must be read, overriding entries in the manualSkips parameter. For most applications, this should be left blank.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public void Read(AssetBinaryReader reader, int[] manualSkips = null, int[] forceReads = null)
        {
            // Header
            ReadHeader(reader);

            // Name map
            reader.BaseStream.Seek(NameOffset, SeekOrigin.Begin);

            OverrideNameMapHashes = new Dictionary<FString, uint>();
            ClearNameIndexList();
            for (int i = 0; i < NameCount; i++)
            {
                FString nameInMap = reader.ReadNameMapString(out uint hashes);
                if (hashes == 0) OverrideNameMapHashes[nameInMap] = 0;
                AddNameReference(nameInMap, true);
            }

            // Imports
            Imports = new List<Import>();
            if (ImportOffset > 0)
            {
                reader.BaseStream.Seek(ImportOffset, SeekOrigin.Begin);
                for (int i = 0; i < ImportCount; i++)
                {
                    Imports.Add(new Import(reader.ReadFName(), reader.ReadFName(), new FPackageIndex(reader.ReadInt32()), reader.ReadFName()));
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
                    newExport.ClassIndex = new FPackageIndex(reader.ReadInt32());
                    newExport.SuperIndex = new FPackageIndex(reader.ReadInt32());
                    if (ObjectVersion >= ObjectVersion.VER_UE4_TemplateIndex_IN_COOKED_EXPORTS)
                    {
                        newExport.TemplateIndex = new FPackageIndex(reader.ReadInt32());
                    }
                    newExport.OuterIndex = new FPackageIndex(reader.ReadInt32());
                    newExport.ObjectName = reader.ReadFName();
                    newExport.ObjectFlags = (EObjectFlags)reader.ReadUInt32();
                    if (ObjectVersion < ObjectVersion.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)
                    {
                        newExport.SerialSize = reader.ReadInt32();
                        newExport.SerialOffset = reader.ReadInt32();
                    }
                    else
                    {
                        newExport.SerialSize = reader.ReadInt64();
                        newExport.SerialOffset = reader.ReadInt64();
                    }
                    newExport.bForcedExport = reader.ReadInt32() == 1;
                    newExport.bNotForClient = reader.ReadInt32() == 1;
                    newExport.bNotForServer = reader.ReadInt32() == 1;
                    newExport.PackageGuid = new Guid(reader.ReadBytes(16));
                    newExport.PackageFlags = (EPackageFlags)reader.ReadUInt32();
                    if (ObjectVersion >= ObjectVersion.VER_UE4_LOAD_FOR_EDITOR_GAME)
                    {
                        newExport.bNotAlwaysLoadedForEditorGame = reader.ReadInt32() == 1;
                    }
                    if (ObjectVersion >= ObjectVersion.VER_UE4_COOKED_ASSETS_IN_EDITOR_SUPPORT)
                    {
                        newExport.bIsAsset = reader.ReadInt32() == 1;
                    }
                    if (ObjectVersion >= ObjectVersion.VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS)
                    {
                        newExport.FirstExportDependencyOffset = reader.ReadInt32();
                        newExport.SerializationBeforeSerializationDependenciesSize = reader.ReadInt32();
                        newExport.CreateBeforeSerializationDependenciesSize = reader.ReadInt32();
                        newExport.SerializationBeforeCreateDependenciesSize = reader.ReadInt32();
                        newExport.CreateBeforeCreateDependenciesSize = reader.ReadInt32();
                    }

                    Exports.Add(newExport);
                }
            }

            // DependsMap
            DependsMap = new List<int[]>();
            if (DependsOffset > 0)
            {
                reader.BaseStream.Seek(DependsOffset, SeekOrigin.Begin);
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
                if (this.UseSeparateBulkDataFiles && nextOffset <= 0) nextOffset = this.PreloadDependencyOffset;
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
            if (this.UseSeparateBulkDataFiles)
            {
                for (int i = 0; i < Exports.Count; i++)
                {
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
            }

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

                    try
                    {
                        long nextStarting = reader.BaseStream.Length - 4;
                        if ((Exports.Count - 1) > i) nextStarting = Exports[i + 1].SerialOffset;

                        FName exportClassTypeName = Exports[i].GetExportClassType();
                        string exportClassType = exportClassTypeName.Value.Value;
                        switch (exportClassType)
                        {
                            case "Level":
                                Exports[i] = Exports[i].ConvertToChildExport<LevelExport>();
                                Exports[i].Read(reader, (int)nextStarting);
                                break;
                            case "StringTable":
                                Exports[i] = Exports[i].ConvertToChildExport<StringTableExport>();
                                Exports[i].Read(reader, (int)nextStarting);
                                break;
                            case "Enum":
                            case "UserDefinedEnum":
                                Exports[i] = Exports[i].ConvertToChildExport<EnumExport>();
                                Exports[i].Read(reader, (int)nextStarting);
                                break;
                            case "Function":
                                Exports[i] = Exports[i].ConvertToChildExport<FunctionExport>();
                                Exports[i].Read(reader, (int)nextStarting);
                                break;
                            default:
                                if (exportClassType.EndsWith("DataTable"))
                                {
                                    Exports[i] = Exports[i].ConvertToChildExport<DataTableExport>();
                                    Exports[i].Read(reader, (int)nextStarting);
                                }
                                else if (exportClassType.EndsWith("BlueprintGeneratedClass"))
                                {
                                    var bgc = Exports[i].ConvertToChildExport<ClassExport>();
                                    Exports[i] = bgc;
                                    Exports[i].Read(reader, (int)nextStarting);

                                    // Check to see if we can add some new map type overrides
                                    if (bgc.LoadedProperties != null)
                                    {
                                        foreach (FProperty entry in bgc.LoadedProperties)
                                        {
                                            if (entry is FMapProperty fMapEntry)
                                            {
                                                FString keyOverride = null;
                                                FString valueOverride = null;
                                                if (fMapEntry.KeyProp is FStructProperty keyPropStruc && keyPropStruc.Struct.IsImport()) keyOverride = keyPropStruc.Struct.ToImport(this).ObjectName.Value;
                                                if (fMapEntry.ValueProp is FStructProperty valuePropStruc && valuePropStruc.Struct.IsImport()) valueOverride = valuePropStruc.Struct.ToImport(this).ObjectName.Value;

                                                this.MapStructTypeOverride.Add(fMapEntry.Name.Value.Value, new Tuple<FString, FString>(keyOverride, valueOverride));
                                            }
                                        }
                                    }
                                }
                                else if (MainSerializer.PropertyTypeRegistry.ContainsKey(exportClassType) || exportClassType == "ClassProperty")
                                {
                                    Exports[i] = Exports[i].ConvertToChildExport<PropertyExport>();
                                    Exports[i].Read(reader, (int)nextStarting);
                                }
                                else
                                {
                                    Exports[i] = Exports[i].ConvertToChildExport<NormalExport>();
                                    Exports[i].Read(reader, (int)nextStarting);
                                }
                                break;
                        }

                        long extrasLen = nextStarting - reader.BaseStream.Position;
                        if (extrasLen < 0)
                        {
                            throw new FormatException("Invalid padding at end of export " + (i + 1) + ": " + extrasLen + " bytes");
                        }
                        else
                        {
                            Exports[i].Extras = reader.ReadBytes((int)extrasLen);
                        }
                    }
                    catch (Exception ex)
                    {
#if DEBUG
                        Debug.WriteLine("\nFailed to parse export " + (i + 1) + ": " + ex.ToString());
#endif
                        reader.BaseStream.Seek(Exports[i].SerialOffset, SeekOrigin.Begin);
                        Exports[i] = Exports[i].ConvertToChildExport<RawExport>();
                        ((RawExport)Exports[i]).Data = reader.ReadBytes((int)Exports[i].SerialSize);
                    }
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

            writer.Write(FileVersionLicenseeUE4);
            if (LegacyFileVersion <= -2)
            {
                if (IsUnversioned)
                {
                    writer.Write(0);
                }
                else
                { 
                    // TODO: support for enum-based custom versions
                    writer.Write(CustomVersionContainer.Count);
                    for (int i = 0; i < CustomVersionContainer.Count; i++)
                    {
                        writer.Write(CustomVersionContainer[i].Key.ToByteArray());
                        writer.Write(CustomVersionContainer[i].Version);
                    }
                }
            }

            writer.Write(SectionSixOffset);
            writer.Write(FolderName);
            writer.Write((uint)PackageFlags);
            writer.Write(NameCount);
            writer.Write(NameOffset);
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

            return stre.ToArray();
        }

        /// <summary>
        /// Serializes an asset from memory.
        /// </summary>
        /// <returns>A stream that the asset has been serialized to.</returns>
        public MemoryStream WriteData()
        {
            isSerializationTime = true;
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
                    writer.Write(nameMapIndexList[i]);

                    if (ObjectVersion >= ObjectVersion.VER_UE4_NAME_HASHES_SERIALIZED)
                    {
                        if (OverrideNameMapHashes != null && OverrideNameMapHashes.ContainsKey(nameMapIndexList[i]))
                        {
                            writer.Write(OverrideNameMapHashes[nameMapIndexList[i]]);
                        }
                        else
                        {
                            writer.Write(CRCGenerator.GenerateHash(nameMapIndexList[i]));
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
                        writer.Write(us.ClassIndex.Index);
                        writer.Write(us.SuperIndex.Index);
                        if (ObjectVersion >= ObjectVersion.VER_UE4_TemplateIndex_IN_COOKED_EXPORTS)
                        {
                            writer.Write(us.TemplateIndex.Index);
                        }
                        writer.Write(us.OuterIndex.Index);
                        writer.Write(us.ObjectName);
                        writer.Write((uint)us.ObjectFlags);
                        if (ObjectVersion < ObjectVersion.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)
                        {
                            writer.Write((int)us.SerialSize);
                            writer.Write((int)us.SerialOffset);
                        }
                        else
                        {
                            writer.Write(us.SerialSize);
                            writer.Write(us.SerialOffset);
                        }
                        writer.Write(us.bForcedExport ? 1 : 0);
                        writer.Write(us.bNotForClient ? 1 : 0);
                        writer.Write(us.bNotForServer ? 1 : 0);
                        writer.Write(us.PackageGuid.ToByteArray());
                        writer.Write((uint)us.PackageFlags);
                        if (ObjectVersion >= ObjectVersion.VER_UE4_LOAD_FOR_EDITOR_GAME)
                        {
                            writer.Write(us.bNotAlwaysLoadedForEditorGame ? 1 : 0);
                        }
                        if (ObjectVersion >= ObjectVersion.VER_UE4_COOKED_ASSETS_IN_EDITOR_SUPPORT)
                        {
                            writer.Write(us.bIsAsset ? 1 : 0);
                        }
                        if (ObjectVersion >= ObjectVersion.VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS)
                        {
                            writer.Write(us.FirstExportDependencyOffset);
                            writer.Write(us.SerializationBeforeSerializationDependenciesSize);
                            writer.Write(us.CreateBeforeSerializationDependenciesSize);
                            writer.Write(us.SerializationBeforeCreateDependenciesSize);
                            writer.Write(us.CreateBeforeCreateDependenciesSize);
                        }
                    }
                }
                else
                {
                    this.ExportOffset = 0;
                }

                // DependsMap
                if (this.doWeHaveDependsMap)
                {
                    this.DependsOffset = (int)writer.BaseStream.Position;
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
                if (this.UseSeparateBulkDataFiles)
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
                writer.Write(new byte[] { 0xC1, 0x83, 0x2A, 0x9E });

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

                        writer.Write(us.ClassIndex.Index);
                        writer.Write(us.SuperIndex.Index);
                        if (ObjectVersion >= ObjectVersion.VER_UE4_TemplateIndex_IN_COOKED_EXPORTS)
                        {
                            writer.Write(us.TemplateIndex.Index);
                        }
                        writer.Write(us.OuterIndex.Index);
                        writer.Write(us.ObjectName);
                        writer.Write((uint)us.ObjectFlags);
                        if (ObjectVersion < ObjectVersion.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)
                        {
                            writer.Write((int)us.SerialSize);
                            writer.Write((int)us.SerialOffset);
                        }
                        else
                        {
                            writer.Write(us.SerialSize);
                            writer.Write(us.SerialOffset);
                        }
                        writer.Write(us.bForcedExport ? 1 : 0);
                        writer.Write(us.bNotForClient ? 1 : 0);
                        writer.Write(us.bNotForServer ? 1 : 0);
                        writer.Write(us.PackageGuid.ToByteArray());
                        writer.Write((uint)us.PackageFlags);
                        if (ObjectVersion >= ObjectVersion.VER_UE4_LOAD_FOR_EDITOR_GAME)
                        {
                            writer.Write(us.bNotAlwaysLoadedForEditorGame ? 1 : 0);
                        }
                        if (ObjectVersion >= ObjectVersion.VER_UE4_COOKED_ASSETS_IN_EDITOR_SUPPORT)
                        {
                            writer.Write(us.bIsAsset ? 1 : 0);
                        }
                        if (ObjectVersion >= ObjectVersion.VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS)
                        {
                            writer.Write(us.FirstExportDependencyOffset);
                            writer.Write(us.SerializationBeforeSerializationDependenciesSize);
                            writer.Write(us.CreateBeforeSerializationDependenciesSize);
                            writer.Write(us.SerializationBeforeCreateDependenciesSize);
                            writer.Write(us.CreateBeforeCreateDependenciesSize);
                        }
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
        /// Creates a MemoryStream from an asset path.
        /// </summary>
        /// <param name="p">The path to the input file.</param>
        /// <returns>A new MemoryStream that stores the binary data of the input file.</returns>
        public MemoryStream PathToStream(string p)
        {
            using (FileStream origStream = File.Open(p, FileMode.Open))
            {
                MemoryStream completeStream = new MemoryStream();
                origStream.CopyTo(completeStream);

                UseSeparateBulkDataFiles = false;
                try
                {
                    var targetFile = Path.ChangeExtension(p, "uexp");
                    if (File.Exists(targetFile))
                    {
                        using (FileStream newStream = File.Open(targetFile, FileMode.Open))
                        {
                            completeStream.Seek(0, SeekOrigin.End);
                            newStream.CopyTo(completeStream);
                            UseSeparateBulkDataFiles = true;
                        }
                    }
                }
                catch (FileNotFoundException) { }

                completeStream.Seek(0, SeekOrigin.Begin);
                return completeStream;
            }
        }

        /// <summary>
        /// Creates a BinaryReader from an asset path.
        /// </summary>
        /// <param name="p">The path to the input file.</param>
        /// <returns>A new BinaryReader that stores the binary data of the input file.</returns>
        public AssetBinaryReader PathToReader(string p)
        {
            return new AssetBinaryReader(PathToStream(p), this);
        }

        /// <summary>
        /// Serializes and writes an asset to disk from memory.
        /// </summary>
        /// <param name="outputPath">The path on disk to write the asset to.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when <see cref="ObjectVersion"/> is unspecified.</exception>
        public void Write(string outputPath)
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

        internal static JsonSerializerSettings jsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
            NullValueHandling = NullValueHandling.Include,
            FloatParseHandling = FloatParseHandling.Double,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new UAssetContractResolver(null),
            Converters = new List<JsonConverter>()
            {
                new FSignedZeroJsonConverter(),
                new FNameJsonConverter(null),
                new FStringTableJsonConverter(),
                new FStringJsonConverter(),
                new FPackageIndexJsonConverter(),
                new StringEnumConverter()
            }
        };

        /// <summary>
        /// Serializes this asset as JSON.
        /// </summary>
        /// <param name="jsonFormatting">The formatting to use for the returned JSON string.</param>
        /// <returns>A serialized JSON string that represents the asset.</returns>
        public string SerializeJson(Formatting jsonFormatting = Formatting.None)
        {
            Info = "Serialized with UAssetAPI " + typeof(PropertyData).Assembly.GetName().Version + (string.IsNullOrEmpty(UAPUtils.CurrentCommit) ? "" : (" (" + UAPUtils.CurrentCommit + ")"));
            return JsonConvert.SerializeObject(this, jsonFormatting, jsonSettings);
        }

        /// <summary>
        /// Serializes an object as JSON.
        /// </summary>
        /// <param name="value">The object to serialize as JSON.</param>
        /// <param name="jsonFormatting">The formatting to use for the returned JSON string.</param>
        /// <returns>A serialized JSON string that represents the object.</returns>
        public string SerializeJsonObject(object value, Formatting jsonFormatting = Formatting.None)
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
                    new StringEnumConverter()
                }
            });

            foreach (KeyValuePair<FName, string> entry in toBeFilled)
            {
                entry.Key.Asset = this;
                if (entry.Value == string.Empty)
                {
                    entry.Key.DummyValue = new FString(entry.Value);
                }
                else
                {
                    var dummy = FName.FromString(this, entry.Value);
                    entry.Key.Value = dummy.Value;
                    entry.Key.Number = dummy.Number;
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
                    new StringEnumConverter()
                }
            });

            foreach (KeyValuePair<FName, string> entry in toBeFilled)
            {
                entry.Key.Asset = res;
                if (entry.Value == string.Empty)
                {
                    entry.Key.DummyValue = new FString(entry.Value);
                }
                else
                {
                    var dummy = FName.FromString(res, entry.Value);
                    entry.Key.Value = dummy.Value;
                    entry.Key.Number = dummy.Number;
                }
            }
            toBeFilled.Clear();

            foreach (Export ex in res.Exports) ex.Asset = res;
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
                    new StringEnumConverter()
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
                if (entry.Value == string.Empty)
                {
                    entry.Key.DummyValue = new FString(entry.Value);
                }
                else
                {
                    var dummy = FName.FromString(res, entry.Value);
                    entry.Key.Value = dummy.Value;
                    entry.Key.Number = dummy.Number;
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
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public UAsset(string path, UE4Version engineVersion = UE4Version.UNKNOWN)
        {
            this.FilePath = path;
            SetEngineVersion(engineVersion);

            Read(PathToReader(path));
        }

        /// <summary>
        /// Reads an asset from a BinaryReader and initializes a new instance of the <see cref="UAsset"/> class to store its data in memory.
        /// </summary>
        /// <param name="reader">The asset's BinaryReader that this instance will read from.</param>
        /// <param name="engineVersion">The version of the Unreal Engine that will be used to parse this asset. If the asset is versioned, this can be left unspecified.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public UAsset(AssetBinaryReader reader, UE4Version engineVersion = UE4Version.UNKNOWN)
        {
            SetEngineVersion(engineVersion);
            Read(reader);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UAsset"/> class. This instance will store no asset data and does not represent any asset in particular until the <see cref="Read"/> method is manually called.
        /// </summary>
        /// <param name="engineVersion">The version of the Unreal Engine that will be used to parse this asset. If the asset is versioned, this can be left unspecified.</param>
        public UAsset(UE4Version engineVersion = UE4Version.UNKNOWN)
        {
            SetEngineVersion(engineVersion);
        }

        /// <summary>
        /// Reads an asset from disk and initializes a new instance of the <see cref="UAsset"/> class to store its data in memory.
        /// </summary>
        /// <param name="path">The path of the asset file on disk that this instance will read from.</param>
        /// <param name="objectVersion">The object version of the Unreal Engine that will be used to parse this asset</param>
        /// <param name="customVersionContainer">A list of custom versions to parse this asset with.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public UAsset(string path, ObjectVersion objectVersion, List<CustomVersion> customVersionContainer)
        {
            this.FilePath = path;
            ObjectVersion = objectVersion;
            CustomVersionContainer = customVersionContainer;

            Read(PathToReader(path));
        }

        /// <summary>
        /// Reads an asset from a BinaryReader and initializes a new instance of the <see cref="UAsset"/> class to store its data in memory.
        /// </summary>
        /// <param name="reader">The asset's BinaryReader that this instance will read from.</param>
        /// <param name="objectVersion">The object version of the Unreal Engine that will be used to parse this asset</param>
        /// <param name="customVersionContainer">A list of custom versions to parse this asset with.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public UAsset(AssetBinaryReader reader, ObjectVersion objectVersion, List<CustomVersion> customVersionContainer)
        {
            ObjectVersion = objectVersion;
            CustomVersionContainer = customVersionContainer;
            Read(reader);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UAsset"/> class. This instance will store no asset data and does not represent any asset in particular until the <see cref="Read"/> method is manually called.
        /// </summary>
        /// <param name="objectVersion">The object version of the Unreal Engine that will be used to parse this asset</param>
        /// <param name="customVersionContainer">A list of custom versions to parse this asset with.</param>
        public UAsset(ObjectVersion objectVersion, List<CustomVersion> customVersionContainer)
        {
            ObjectVersion = objectVersion;
            CustomVersionContainer = customVersionContainer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UAsset"/> class. This instance will store no asset data and does not represent any asset in particular until the <see cref="Read"/> method is manually called.
        /// </summary>
        public UAsset()
        {

        }
    }
}
