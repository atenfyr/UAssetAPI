using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UAssetAPI.ExportTypes;
using UAssetAPI.FieldTypes;
using UAssetAPI.IO;
using UAssetAPI.JSON;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI
{
    public abstract class UnrealPackage : INameMap
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
        /// The corresponding mapping data for the game that this asset is from. Optional unless unversioned properties are present.
        /// </summary>
        [JsonIgnore]
        public Usmap Mappings;

        /// <summary>
        /// Should the asset be split into separate .uasset, .uexp, and .ubulk files, as opposed to one single .uasset file?
        /// </summary>
        public bool UseSeparateBulkDataFiles = false;

        /// <summary>
        /// Should this asset not serialize its engine and custom versions?
        /// </summary>
        public bool IsUnversioned;

        /// <summary>
        /// The licensee file version. Used by some games to add their own Engine-level versioning.
        /// </summary>
        public int FileVersionLicenseeUE;

        /// <summary>
        /// The object version of UE4 that will be used to parse this asset.
        /// </summary>
        public ObjectVersion ObjectVersion = ObjectVersion.UNKNOWN;

        /// <summary>
        /// The object version of UE5 that will be used to parse this asset. Set to <see cref="ObjectVersionUE5.UNKNOWN"/> for UE4 games.
        /// </summary>
        public ObjectVersionUE5 ObjectVersionUE5 = ObjectVersionUE5.UNKNOWN;

        /// <summary>
        /// All the custom versions stored in the archive.
        /// </summary>
        public List<CustomVersion> CustomVersionContainer = null;

        private EPackageFlags _packageFlags;
        /// <summary>
        /// The flags for this package.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EPackageFlags PackageFlags
        {
            get
            {
                return _packageFlags;
            }
            set
            {
                _packageFlags = value;
                _hasUnversionedPropertiesCacheDirty = true;
            }
        }

        private bool _hasUnversionedPropertiesCacheDirty = true;
        private bool _hasUnversionedPropertiesCache; // HasFlag is a little bit expensive so we cache the bool value for good measure
        /// <summary>
        /// Whether or not this asset uses unversioned properties.
        /// </summary>
        public bool HasUnversionedProperties
        {
            get
            {
                if (_hasUnversionedPropertiesCacheDirty)
                {
                    _hasUnversionedPropertiesCache = PackageFlags.HasFlag(EPackageFlags.PKG_UnversionedProperties);
                    _hasUnversionedPropertiesCacheDirty = false;
                }
                return _hasUnversionedPropertiesCache;
            }
        }

        [JsonIgnore]
        internal bool isSerializationTime = false;

        /// <summary>
        /// Internal list of name map entries. Do not directly add values to here under any circumstances; use <see cref="AddNameReference"/> instead
        /// </summary>
        [JsonProperty("NameMap", Order = -2)]
        internal List<FString> nameMapIndexList;

        /// <summary>
        /// Internal lookup for name map entries. Do not directly add values to here under any circumstances; use <see cref="AddNameReference"/> instead
        /// </summary>
        internal Dictionary<string, int> nameMapLookup = new Dictionary<string, int>();

        /// <summary>
        /// Map of object exports. UAssetAPI used to call these "categories."
        /// </summary>
        public List<Export> Exports;

        /// <summary>
        /// Tile information used by WorldComposition.
        /// Defines properties necessary for tile positioning in the world.
        /// </summary>
        public FWorldTileInfo WorldTileInfo;

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

        internal void FixNameMapLookupIfNeeded()
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
            //FixNameMapLookupIfNeeded();
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
        /// Creates a MemoryStream from an asset path.
        /// </summary>
        /// <param name="p">The path to the input file.</param>
        /// <returns>A new MemoryStream that stores the binary data of the input file.</returns>
        public MemoryStream PathToStream(string p)
        {
            using (FileStream origStream = File.Open(p, FileMode.Open, new FileInfo(p).IsReadOnly ? FileAccess.Read : FileAccess.ReadWrite))
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
        public virtual void GetParentClass(out FName parentClassPath, out FName parentClassExportName)
        {
            parentClassPath = null;
            parentClassExportName = null;
        }

        internal virtual FName GetParentClassExportName()
        {
            return null;
        }

        /// <summary>
        /// Resolves the ancestry of all properties present in this asset.
        /// </summary>
        public virtual void ResolveAncestries()
        {
            if (WorldTileInfo != null) WorldTileInfo.ResolveAncestries(this, new AncestryInfo());
            if (Exports != null)
            {
                for (int i = 0; i < Exports.Count; i++) Exports[i]?.ResolveAncestries(this, new AncestryInfo());
            }
        }

        /// <summary>
        /// Sets the version of the Unreal Engine to use in serialization.
        /// </summary>
        /// <param name="newVersion">The new version of the Unreal Engine to use in serialization.</param>
        /// <exception cref="InvalidOperationException">Thrown when an invalid UE4Version is specified.</exception>
        public void SetEngineVersion(EngineVersion newVersion)
        {
            if (newVersion == EngineVersion.UNKNOWN) return;
            if (!Enum.TryParse(Enum.GetName(typeof(EngineVersion), newVersion), out UE4VersionToObjectVersion bridgeVer)) throw new InvalidOperationException("Invalid engine version specified");
            ObjectVersion = (ObjectVersion)(int)bridgeVer;

            if (Enum.TryParse(Enum.GetName(typeof(EngineVersion), newVersion), out UE5VersionToObjectVersion bridgeVer2)) ObjectVersionUE5 = (ObjectVersionUE5)(int)bridgeVer2;

            CustomVersionContainer = GetDefaultCustomVersionContainer(newVersion);
        }

        public static EngineVersion GetEngineVersion(ObjectVersion objectVersion, ObjectVersionUE5 objectVersionUE5, List<CustomVersion> customVersionContainer)
        {
            // analyze all possible versions based off of the object version alone
            List<EngineVersion> allPossibleVersions = new List<EngineVersion>();
            int targetVer = (int)objectVersionUE5;
            while (allPossibleVersions.Count == 0 && targetVer >= (int)ObjectVersionUE5.INITIAL_VERSION)
            {
                allPossibleVersions = Enum.GetNames(typeof(UE5VersionToObjectVersion)).Where(n => ((int)Enum.Parse(typeof(UE5VersionToObjectVersion), n)).Equals(targetVer)).Select(str => (EngineVersion)Enum.Parse(typeof(EngineVersion), str)).ToList();
                targetVer -= 1;
            }
            targetVer = (int)objectVersion;
            while (allPossibleVersions.Count == 0 && targetVer > (int)ObjectVersion.VER_UE4_OLDEST_LOADABLE_PACKAGE)
            {
                allPossibleVersions = Enum.GetNames(typeof(UE4VersionToObjectVersion)).Where(n => ((int)Enum.Parse(typeof(UE4VersionToObjectVersion), n)).Equals(targetVer)).Select(str => (EngineVersion)Enum.Parse(typeof(EngineVersion), str)).ToList();
                targetVer -= 1;
            }

            if (allPossibleVersions.Count == 0) return EngineVersion.UNKNOWN;
            if (allPossibleVersions.Count == 1 || customVersionContainer == null) return allPossibleVersions[0];

            // multiple possible versions; use custom versions to eliminate some
            EngineVersion minIntroduced = EngineVersion.VER_UE4_OLDEST_LOADABLE_PACKAGE;
            EngineVersion maxIntroduced = EngineVersion.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE;
            foreach (CustomVersion entry in customVersionContainer)
            {
                Type customVersionType = Type.GetType("UAssetAPI." + MainSerializer.allNonLetters.Replace(entry.FriendlyName, string.Empty));
                if (customVersionType == null) continue;
                EngineVersion minIntroducedThis = GetIntroducedFromCustomVersionValue(customVersionType, entry.Version); // inclusive
                EngineVersion maxIntroducedThis = GetIntroducedFromCustomVersionValue(customVersionType, entry.Version + 1); // exclusive

                if (minIntroducedThis != EngineVersion.UNKNOWN && minIntroducedThis > minIntroduced) minIntroduced = minIntroducedThis;
                if (maxIntroducedThis != EngineVersion.UNKNOWN && maxIntroducedThis < maxIntroduced) maxIntroduced = maxIntroducedThis;
            }

            List<EngineVersion> finalPossibleVersions = new List<EngineVersion>();
            foreach (EngineVersion entry in allPossibleVersions)
            {
                if (entry >= minIntroduced && entry < maxIntroduced) finalPossibleVersions.Add(entry);
            }
            finalPossibleVersions.Sort();

            if (finalPossibleVersions.Count == 0) return allPossibleVersions[0]; // there must be a special set of custom versions; we'll just ignore our intuitions and go with the object version alone
            if (finalPossibleVersions.Count >= 1) return finalPossibleVersions[0];
            return EngineVersion.UNKNOWN;
        }

        /// <summary>
        /// Estimates the retail version of the Unreal Engine based on the object and custom versions.
        /// </summary>
        /// <returns>The estimated retail version of the Unreal Engine.</returns>
        public EngineVersion GetEngineVersion()
        {
            return UnrealPackage.GetEngineVersion(ObjectVersion, ObjectVersionUE5, CustomVersionContainer);
        }

        private static EngineVersion GetIntroducedFromCustomVersionValue(Type customVersionType, int val)
        {
            var nm = Enum.GetName(customVersionType, val);
            if (nm == null) return EngineVersion.UNKNOWN;
            var attributes = customVersionType.GetMember(nm)?[0]?.GetCustomAttributes(typeof(IntroducedAttribute), false);
            if (attributes == null || attributes.Length <= 0) return EngineVersion.UNKNOWN;
            return ((IntroducedAttribute)attributes[0]).IntroducedVersion;
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

        private static ConcurrentDictionary<string, EngineVersion> cachedCustomVersionReflectionData = new ConcurrentDictionary<string, EngineVersion>();
        public static int GuessCustomVersionFromTypeAndEngineVersion(EngineVersion chosenVersion, Type typ)
        {
            string typeString = typ.ToString();
            string[] allVals = Enum.GetNames(typ);
            for (int i = allVals.Length - 1; i >= 0; i--)
            {
                string val = allVals[i];
                string cacheKey = typeString + val;

                var attributeIntroducedVersion = EngineVersion.UNKNOWN;
                if (cachedCustomVersionReflectionData.ContainsKey(cacheKey))
                {
                    attributeIntroducedVersion = cachedCustomVersionReflectionData[cacheKey];
                }
                else
                {
                    var attributes = typ.GetMember(val)?[0]?.GetCustomAttributes(typeof(IntroducedAttribute), false);
                    attributeIntroducedVersion = (attributes == null || attributes.Length <= 0) ? EngineVersion.UNKNOWN : ((IntroducedAttribute)attributes[0]).IntroducedVersion;
                    cachedCustomVersionReflectionData[cacheKey] = attributeIntroducedVersion;
                }

                if (attributeIntroducedVersion != EngineVersion.UNKNOWN && chosenVersion >= attributeIntroducedVersion) return i;
            }
            return -1;
        }

        /// <summary>
        /// Fetches a list of all default custom versions for a specific Unreal version.
        /// </summary>
        /// <param name="chosenVersion">The version of the engine to check against.</param>
        /// <returns>A list of all the default custom version values for the given engine version.</returns>
        public static List<CustomVersion> GetDefaultCustomVersionContainer(EngineVersion chosenVersion)
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

        protected void ConvertExportToChildExportAndRead(AssetBinaryReader reader, int i)
        {
#pragma warning disable CS0168 // Variable is declared but never used
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
                        else if (exportClassType.EndsWith("StringTable"))
                        {
                            Exports[i] = Exports[i].ConvertToChildExport<StringTableExport>();
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
#if DEBUG_VERBOSE
                        Debug.WriteLine("\nFailed to parse export " + (i + 1) + ": " + ex.ToString());
#endif
                reader.BaseStream.Seek(Exports[i].SerialOffset, SeekOrigin.Begin);
                Exports[i] = Exports[i].ConvertToChildExport<RawExport>();
                ((RawExport)Exports[i]).Data = reader.ReadBytes((int)Exports[i].SerialSize);
            }
#pragma warning restore CS0168 // Variable is declared but never used
        }

        /// <summary>
        /// Reads an asset into memory.
        /// </summary>
        /// <param name="reader">The input reader.</param>
        /// <param name="manualSkips">An array of export indexes to skip parsing. For most applications, this should be left blank.</param>
        /// <param name="forceReads">An array of export indexes that must be read, overriding entries in the manualSkips parameter. For most applications, this should be left blank.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when this is an unversioned asset and <see cref="ObjectVersion"/> is unspecified.</exception>
        /// <exception cref="FormatException">Throw when the asset cannot be parsed correctly.</exception>
        public virtual void Read(AssetBinaryReader reader, int[] manualSkips = null, int[] forceReads = null)
        {

        }

        /// <summary>
        /// Serializes an asset from memory.
        /// </summary>
        /// <returns>A stream that the asset has been serialized to.</returns>
        public virtual MemoryStream WriteData()
        {
            return null;
        }

        /// <summary>
        /// Serializes and writes an asset to disk from memory.
        /// </summary>
        /// <param name="outputPath">The path on disk to write the asset to.</param>
        /// <exception cref="UnknownEngineVersionException">Thrown when <see cref="ObjectVersion"/> is unspecified.</exception>
        public virtual void Write(string outputPath)
        {

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
                new StringEnumConverter(),
                new GuidJsonConverter(),
            }
        };
    }
}
