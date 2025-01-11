# UAsset

Namespace: UAssetAPI

Represents an Unreal Engine asset.

```csharp
public class UAsset : INameMap
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UAsset](./uassetapi.uasset.md)<br>
Implements [INameMap](./uassetapi.inamemap.md)

## Fields

### **Info**

Agent string to provide context in serialized JSON.

```csharp
public string Info;
```

### **FilePath**

The path of the file on disk that this asset represents. This does not need to be specified for regular parsing.

```csharp
public string FilePath;
```

### **Mappings**

The corresponding mapping data for the game that this asset is from. Optional unless unversioned properties are present.

```csharp
public Usmap Mappings;
```

### **CustomSerializationFlags**

List of custom serialization flags, used to override certain optional behavior in how UAssetAPI serializes assets.

```csharp
public CustomSerializationFlags CustomSerializationFlags;
```

### **UseSeparateBulkDataFiles**

Should the asset be split into separate .uasset, .uexp, and .ubulk files, as opposed to one single .uasset file?

```csharp
public bool UseSeparateBulkDataFiles;
```

### **IsUnversioned**

Should this asset not serialize its engine and custom versions?

```csharp
public bool IsUnversioned;
```

### **FileVersionLicenseeUE**

The licensee file version. Used by some games to add their own Engine-level versioning.

```csharp
public int FileVersionLicenseeUE;
```

### **ObjectVersion**

The object version of UE4 that will be used to parse this asset.

```csharp
public ObjectVersion ObjectVersion;
```

### **ObjectVersionUE5**

The object version of UE5 that will be used to parse this asset. Set to [ObjectVersionUE5.UNKNOWN](./uassetapi.unrealtypes.objectversionue5.md#unknown) for UE4 games.

```csharp
public ObjectVersionUE5 ObjectVersionUE5;
```

### **CustomVersionContainer**

All the custom versions stored in the archive.

```csharp
public List<CustomVersion> CustomVersionContainer;
```

### **GatherableTextData**

Map of the gatherable text data.

```csharp
public List<FGatherableTextData> GatherableTextData;
```

### **Exports**

Map of object exports. UAssetAPI used to call these "categories."

```csharp
public List<Export> Exports;
```

### **SearchableNames**

List of Searchable Names, by object containing them. Sorted to keep order consistent.

```csharp
public SortedDictionary<FPackageIndex, List<FName>> SearchableNames;
```

### **Thumbnails**

Map of object full names to the thumbnails

```csharp
public Dictionary<string, FObjectThumbnail> Thumbnails;
```

### **WorldTileInfo**

Tile information used by WorldComposition.
 Defines properties necessary for tile positioning in the world.

```csharp
public FWorldTileInfo WorldTileInfo;
```

### **MapStructTypeOverride**

In MapProperties that have StructProperties as their keys or values, there is no universal, context-free way to determine the type of the struct.



To that end, this dictionary maps MapProperty names to the type of the structs within them (tuple of key struct type and value struct type) if they are not None-terminated property lists.

```csharp
public Dictionary<string, Tuple<FString, FString>> MapStructTypeOverride;
```

### **ArrayStructTypeOverride**

IN ENGINE VERSIONS BEFORE [ObjectVersion.VER_UE4_INNER_ARRAY_TAG_INFO](./uassetapi.unrealtypes.objectversion.md#ver_ue4_inner_array_tag_info):



In ArrayProperties that have StructProperties as their keys or values, there is no universal, context-free way to determine the type of the struct. To that end, this dictionary maps ArrayProperty names to the type of the structs within them.

```csharp
public Dictionary<string, FString> ArrayStructTypeOverride;
```

### **OtherAssetsFailedToAccess**

```csharp
public ISet<FName> OtherAssetsFailedToAccess;
```

### **LegacyFileVersion**

The package file version number when this package was saved.

```csharp
public int LegacyFileVersion;
```

**Remarks:**

The lower 16 bits stores the UE3 engine version, while the upper 16 bits stores the UE4/licensee version. For newer packages this is -7.
 VersionDescription-2indicates presence of enum-based custom versions-3indicates guid-based custom versions-4indicates removal of the UE3 version. Packages saved with this ID cannot be loaded in older engine versions-5indicates the replacement of writing out the "UE3 version" so older versions of engine can gracefully fail to open newer packages-6indicates optimizations to how custom versions are being serialized-7indicates the texture allocation info has been removed from the summary-8indicates that the UE5 version has been added to the summary

### **DataResourceVersion**

The version to use for serializing data resources.

```csharp
public EObjectDataResourceVersion DataResourceVersion;
```

### **DataResources**

List of serialized UObject binary/bulk data resources.

```csharp
public List<FObjectDataResource> DataResources;
```

### **UsesEventDrivenLoader**

Whether or not this asset is loaded with the Event Driven Loader.

```csharp
public bool UsesEventDrivenLoader;
```

### **WillSerializeNameHashes**

Whether or not this asset serializes hashes in the name map.
 If null, this will be automatically determined based on the object version.

```csharp
public Nullable<bool> WillSerializeNameHashes;
```

### **Imports**

Map of object imports. UAssetAPI used to call these "links."

```csharp
public List<Import> Imports;
```

### **DependsMap**

List of dependency lists for each export.

```csharp
public List<Int32[]> DependsMap;
```

### **SoftPackageReferenceList**

List of packages that are soft referenced by this package.

```csharp
public List<FString> SoftPackageReferenceList;
```

### **AssetRegistryData**

Uncertain

```csharp
public Byte[] AssetRegistryData;
```

### **BulkData**

Any bulk data that is not stored in the export map.

```csharp
public Byte[] BulkData;
```

### **ValorantGarbageData**

Some garbage data that appears to be present in certain games (e.g. Valorant)

```csharp
public Byte[] ValorantGarbageData;
```

### **SeaOfThievesGarbageData**

Some garbage data that appears to be present in certain games (e.g. Sea of Thieves)
 null = not present
 empty array = present, but serialize as offset = 0, length = 0

```csharp
public Byte[] SeaOfThievesGarbageData;
```

### **Generations**

Data about previous versions of this package.

```csharp
public List<FGenerationInfo> Generations;
```

### **PackageGuid**

Current ID for this package. Effectively unused.

```csharp
public Guid PackageGuid;
```

### **PersistentGuid**

Current persistent ID for this package.

```csharp
public Guid PersistentGuid;
```

### **RecordedEngineVersion**

Engine version this package was saved with. This may differ from CompatibleWithEngineVersion for assets saved with a hotfix release.

```csharp
public FEngineVersion RecordedEngineVersion;
```

### **RecordedCompatibleWithEngineVersion**

Engine version this package is compatible with. Assets saved by Hotfix releases and engine versions that maintain binary compatibility will have
 a CompatibleWithEngineVersion.Patch that matches the original release (as opposed to SavedByEngineVersion which will have a patch version of the new release).

```csharp
public FEngineVersion RecordedCompatibleWithEngineVersion;
```

### **ChunkIDs**

Streaming install ChunkIDs

```csharp
public Int32[] ChunkIDs;
```

### **PackageSource**

Value that is used by the Unreal Engine to determine if the package was saved by Epic, a licensee, modder, etc.

```csharp
public uint PackageSource;
```

### **FolderName**

In UE4: "FolderName": The Generic Browser folder name that this package lives in. Usually "None" in cooked assets.
 In UE5: "PackageName": The package name the file was last saved with.

```csharp
public FString FolderName;
```

### **OverrideNameMapHashes**

A map of name map entries to hashes to use when serializing instead of the default engine hash algorithm. Useful when external programs improperly specify name map hashes and binary equality must be maintained.

```csharp
public Dictionary<FString, uint> OverrideNameMapHashes;
```

### **LocalizationId**

Localization ID of this package

```csharp
public FString LocalizationId;
```

### **UASSET_MAGIC**

Magic number for the .uasset format

```csharp
public static uint UASSET_MAGIC;
```

### **ACE7_MAGIC**

Magic number for Ace Combat 7 encrypted .uasset format

```csharp
public static uint ACE7_MAGIC;
```

## Properties

### **PackageFlags**

The flags for this package.

```csharp
public EPackageFlags PackageFlags { get; set; }
```

#### Property Value

[EPackageFlags](./uassetapi.unrealtypes.epackageflags.md)<br>

### **HasUnversionedProperties**

Whether or not this asset uses unversioned properties.

```csharp
public bool HasUnversionedProperties { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsFilterEditorOnly**

Whether or not this asset has PKG_FilterEditorOnly flag.

```csharp
public bool IsFilterEditorOnly { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **UAsset(String, EngineVersion, Usmap, CustomSerializationFlags)**

Reads an asset from disk and initializes a new instance of the [UAsset](./uassetapi.uasset.md) class to store its data in memory.

```csharp
public UAsset(string path, EngineVersion engineVersion, Usmap mappings, CustomSerializationFlags customSerializationFlags)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path of the asset file on disk that this instance will read from.

`engineVersion` [EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>
The version of the Unreal Engine that will be used to parse this asset. If the asset is versioned, this can be left unspecified.

`mappings` [Usmap](./uassetapi.unversioned.usmap.md)<br>
A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.

`customSerializationFlags` [CustomSerializationFlags](./uassetapi.customserializationflags.md)<br>
A set of custom serialization flags, which can be used to override certain optional behavior in how UAssetAPI serializes assets.

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when this is an unversioned asset and [UAsset.ObjectVersion](./uassetapi.uasset.md#objectversion) is unspecified.

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **UAsset(String, Boolean, EngineVersion, Usmap, CustomSerializationFlags)**

Reads an asset from disk and initializes a new instance of the [UAsset](./uassetapi.uasset.md) class to store its data in memory.

```csharp
public UAsset(string path, bool loadUexp, EngineVersion engineVersion, Usmap mappings, CustomSerializationFlags customSerializationFlags)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path of the asset file on disk that this instance will read from.

`loadUexp` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to load the .uexp file. False only reads the .uasset file.

`engineVersion` [EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>
The version of the Unreal Engine that will be used to parse this asset. If the asset is versioned, this can be left unspecified.

`mappings` [Usmap](./uassetapi.unversioned.usmap.md)<br>
A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.

`customSerializationFlags` [CustomSerializationFlags](./uassetapi.customserializationflags.md)<br>
A set of custom serialization flags, which can be used to override certain optional behavior in how UAssetAPI serializes assets.

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when this is an unversioned asset and [UAsset.ObjectVersion](./uassetapi.uasset.md#objectversion) is unspecified.

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **UAsset(AssetBinaryReader, EngineVersion, Usmap, Boolean, CustomSerializationFlags)**

Reads an asset from a BinaryReader and initializes a new instance of the [UAsset](./uassetapi.uasset.md) class to store its data in memory.

```csharp
public UAsset(AssetBinaryReader reader, EngineVersion engineVersion, Usmap mappings, bool useSeparateBulkDataFiles, CustomSerializationFlags customSerializationFlags)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The asset's BinaryReader that this instance will read from. If a .uexp file exists, the .uexp file's data should be appended to the end of the .uasset file's data.

`engineVersion` [EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>
The version of the Unreal Engine that will be used to parse this asset. If the asset is versioned, this can be left unspecified.

`mappings` [Usmap](./uassetapi.unversioned.usmap.md)<br>
A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.

`useSeparateBulkDataFiles` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Does this asset uses separate bulk data files (.uexp, .ubulk)?

`customSerializationFlags` [CustomSerializationFlags](./uassetapi.customserializationflags.md)<br>
A set of custom serialization flags, which can be used to override certain optional behavior in how UAssetAPI serializes assets.

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when this is an unversioned asset and [UAsset.ObjectVersion](./uassetapi.uasset.md#objectversion) is unspecified.

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **UAsset(EngineVersion, Usmap, CustomSerializationFlags)**

Initializes a new instance of the [UAsset](./uassetapi.uasset.md) class. This instance will store no asset data and does not represent any asset in particular until the [UAsset.Read(AssetBinaryReader, Int32[], Int32[])](./uassetapi.uasset.md#readassetbinaryreader-int32-int32) method is manually called.

```csharp
public UAsset(EngineVersion engineVersion, Usmap mappings, CustomSerializationFlags customSerializationFlags)
```

#### Parameters

`engineVersion` [EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>
The version of the Unreal Engine that will be used to parse this asset. If the asset is versioned, this can be left unspecified.

`mappings` [Usmap](./uassetapi.unversioned.usmap.md)<br>
A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.

`customSerializationFlags` [CustomSerializationFlags](./uassetapi.customserializationflags.md)<br>
A set of custom serialization flags, which can be used to override certain optional behavior in how UAssetAPI serializes assets.

### **UAsset(String, ObjectVersion, ObjectVersionUE5, List&lt;CustomVersion&gt;, Usmap, CustomSerializationFlags)**

Reads an asset from disk and initializes a new instance of the [UAsset](./uassetapi.uasset.md) class to store its data in memory.

```csharp
public UAsset(string path, ObjectVersion objectVersion, ObjectVersionUE5 objectVersionUE5, List<CustomVersion> customVersionContainer, Usmap mappings, CustomSerializationFlags customSerializationFlags)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path of the asset file on disk that this instance will read from.

`objectVersion` [ObjectVersion](./uassetapi.unrealtypes.objectversion.md)<br>
The UE4 object version of the Unreal Engine that will be used to parse this asset.

`objectVersionUE5` [ObjectVersionUE5](./uassetapi.unrealtypes.objectversionue5.md)<br>
The UE5 object version of the Unreal Engine that will be used to parse this asset.

`customVersionContainer` [List&lt;CustomVersion&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
A list of custom versions to parse this asset with.

`mappings` [Usmap](./uassetapi.unversioned.usmap.md)<br>
A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.

`customSerializationFlags` [CustomSerializationFlags](./uassetapi.customserializationflags.md)<br>
A set of custom serialization flags, which can be used to override certain optional behavior in how UAssetAPI serializes assets.

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when this is an unversioned asset and [UAsset.ObjectVersion](./uassetapi.uasset.md#objectversion) is unspecified.

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **UAsset(AssetBinaryReader, ObjectVersion, ObjectVersionUE5, List&lt;CustomVersion&gt;, Usmap, Boolean, CustomSerializationFlags)**

Reads an asset from a BinaryReader and initializes a new instance of the [UAsset](./uassetapi.uasset.md) class to store its data in memory.

```csharp
public UAsset(AssetBinaryReader reader, ObjectVersion objectVersion, ObjectVersionUE5 objectVersionUE5, List<CustomVersion> customVersionContainer, Usmap mappings, bool useSeparateBulkDataFiles, CustomSerializationFlags customSerializationFlags)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The asset's BinaryReader that this instance will read from.

`objectVersion` [ObjectVersion](./uassetapi.unrealtypes.objectversion.md)<br>
The UE4 object version of the Unreal Engine that will be used to parse this asset.

`objectVersionUE5` [ObjectVersionUE5](./uassetapi.unrealtypes.objectversionue5.md)<br>
The UE5 object version of the Unreal Engine that will be used to parse this asset.

`customVersionContainer` [List&lt;CustomVersion&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
A list of custom versions to parse this asset with.

`mappings` [Usmap](./uassetapi.unversioned.usmap.md)<br>
A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.

`useSeparateBulkDataFiles` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Does this asset uses separate bulk data files (.uexp, .ubulk)?

`customSerializationFlags` [CustomSerializationFlags](./uassetapi.customserializationflags.md)<br>
A set of custom serialization flags, which can be used to override certain optional behavior in how UAssetAPI serializes assets.

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when this is an unversioned asset and [UAsset.ObjectVersion](./uassetapi.uasset.md#objectversion) is unspecified.

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **UAsset(ObjectVersion, ObjectVersionUE5, List&lt;CustomVersion&gt;, Usmap, CustomSerializationFlags)**

Initializes a new instance of the [UAsset](./uassetapi.uasset.md) class. This instance will store no asset data and does not represent any asset in particular until the [UAsset.Read(AssetBinaryReader, Int32[], Int32[])](./uassetapi.uasset.md#readassetbinaryreader-int32-int32) method is manually called.

```csharp
public UAsset(ObjectVersion objectVersion, ObjectVersionUE5 objectVersionUE5, List<CustomVersion> customVersionContainer, Usmap mappings, CustomSerializationFlags customSerializationFlags)
```

#### Parameters

`objectVersion` [ObjectVersion](./uassetapi.unrealtypes.objectversion.md)<br>
The UE4 object version of the Unreal Engine that will be used to parse this asset.

`objectVersionUE5` [ObjectVersionUE5](./uassetapi.unrealtypes.objectversionue5.md)<br>
The UE5 object version of the Unreal Engine that will be used to parse this asset.

`customVersionContainer` [List&lt;CustomVersion&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
A list of custom versions to parse this asset with.

`mappings` [Usmap](./uassetapi.unversioned.usmap.md)<br>
A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.

`customSerializationFlags` [CustomSerializationFlags](./uassetapi.customserializationflags.md)<br>
A set of custom serialization flags, which can be used to override certain optional behavior in how UAssetAPI serializes assets.

### **UAsset()**

Initializes a new instance of the [UAsset](./uassetapi.uasset.md) class. This instance will store no asset data and does not represent any asset in particular until the [UAsset.Read(AssetBinaryReader, Int32[], Int32[])](./uassetapi.uasset.md#readassetbinaryreader-int32-int32) method is manually called.

```csharp
public UAsset()
```

## Methods

### **FixNameMapLookupIfNeeded()**

```csharp
internal void FixNameMapLookupIfNeeded()
```

### **GetNameMapIndexList()**

Returns the name map as a read-only list of FStrings.

```csharp
public IReadOnlyList<FString> GetNameMapIndexList()
```

#### Returns

[IReadOnlyList&lt;FString&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>
The name map as a read-only list of FStrings.

### **ClearNameIndexList()**

Clears the name map. This method should be used with extreme caution, as it may break unparsed references to the name map.

```csharp
public void ClearNameIndexList()
```

### **SetNameReference(Int32, FString)**

Replaces a value in the name map at a particular index.

```csharp
public void SetNameReference(int index, FString value)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index to overwrite in the name map.

`value` [FString](./uassetapi.unrealtypes.fstring.md)<br>
The value that will be replaced in the name map.

### **GetNameReference(Int32)**

Gets a value in the name map at a particular index.

```csharp
public FString GetNameReference(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index to return the value at.

#### Returns

[FString](./uassetapi.unrealtypes.fstring.md)<br>
The value at the index provided.

### **GetNameReferenceWithoutZero(Int32)**

Gets a value in the name map at a particular index, but with the index zero being treated as if it is not valid.

```csharp
public FString GetNameReferenceWithoutZero(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index to return the value at.

#### Returns

[FString](./uassetapi.unrealtypes.fstring.md)<br>
The value at the index provided.

### **ContainsNameReference(FString)**

Checks whether or not the value exists in the name map.

```csharp
public bool ContainsNameReference(FString search)
```

#### Parameters

`search` [FString](./uassetapi.unrealtypes.fstring.md)<br>
The value to search the name map for.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the value appears in the name map, otherwise false.

### **SearchNameReference(FString)**

Searches the name map for a particular value.

```csharp
public int SearchNameReference(FString search)
```

#### Parameters

`search` [FString](./uassetapi.unrealtypes.fstring.md)<br>
The value to search the name map for.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index at which the value appears in the name map.

#### Exceptions

[NameMapOutOfRangeException](./uassetapi.namemapoutofrangeexception.md)<br>
Thrown when the value provided does not appear in the name map.

### **AddNameReference(FString, Boolean)**

Adds a new value to the name map.

```csharp
public int AddNameReference(FString name, bool forceAddDuplicates)
```

#### Parameters

`name` [FString](./uassetapi.unrealtypes.fstring.md)<br>
The value to add to the name map.

`forceAddDuplicates` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not to add a new entry if the value provided already exists in the name map.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index of the new value in the name map. If the value already existed in the name map beforehand, that index will be returned instead.

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when forceAddDuplicates is false and the value provided is null or empty.

### **CanCreateDummies()**

Whether or not we can create dummies in this name map. If false, attempting to define a dummy will append to the name map instead.

```csharp
public bool CanCreateDummies()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
A boolean.

### **PathToStream(String, Boolean)**

Creates a MemoryStream from an asset path.

```csharp
public MemoryStream PathToStream(string p, bool loadUEXP)
```

#### Parameters

`p` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the input file.

`loadUEXP` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to load the UEXP file. False only reads the UASSET.

#### Returns

[MemoryStream](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream)<br>
A new MemoryStream that stores the binary data of the input file.

### **PathToReader(String, Boolean)**

Creates a BinaryReader from an asset path.

```csharp
public AssetBinaryReader PathToReader(string p, bool loadUEXP)
```

#### Parameters

`p` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the input file.

`loadUEXP` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to load the .uexp file. False only reads the .uasset file.

#### Returns

[AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
A new BinaryReader that stores the binary data of the input file.

### **GetClassExport()**

Searches for and returns this asset's ClassExport, if one exists.

```csharp
public ClassExport GetClassExport()
```

#### Returns

[ClassExport](./uassetapi.exporttypes.classexport.md)<br>
The asset's ClassExport if one exists, otherwise null.

### **ResolveAncestries()**

Resolves the ancestry of all properties present in this asset.

```csharp
public void ResolveAncestries()
```

### **FindAssetOnDiskFromPath(String)**

Attempt to find another asset on disk given an asset path (i.e. one starting with /Game/).

```csharp
public string FindAssetOnDiskFromPath(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The asset path.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the file on disk, or null if none could be found.

### **SetEngineVersion(EngineVersion)**

Sets the version of the Unreal Engine to use in serialization.

```csharp
public void SetEngineVersion(EngineVersion newVersion)
```

#### Parameters

`newVersion` [EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>
The new version of the Unreal Engine to use in serialization.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when an invalid EngineVersion is specified.

### **GetEngineVersion(ObjectVersion, ObjectVersionUE5, List&lt;CustomVersion&gt;)**

```csharp
public static EngineVersion GetEngineVersion(ObjectVersion objectVersion, ObjectVersionUE5 objectVersionUE5, List<CustomVersion> customVersionContainer)
```

#### Parameters

`objectVersion` [ObjectVersion](./uassetapi.unrealtypes.objectversion.md)<br>

`objectVersionUE5` [ObjectVersionUE5](./uassetapi.unrealtypes.objectversionue5.md)<br>

`customVersionContainer` [List&lt;CustomVersion&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

#### Returns

[EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>

### **GetEngineVersion()**

Estimates the retail version of the Unreal Engine based on the object and custom versions.

```csharp
public EngineVersion GetEngineVersion()
```

#### Returns

[EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>
The estimated retail version of the Unreal Engine.

### **GetCustomVersion(Guid)**

Fetches the version of a custom version in this asset.

```csharp
public int GetCustomVersion(Guid key)
```

#### Parameters

`key` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>
The GUID of the custom version to retrieve.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The version of the retrieved custom version.

### **GetCustomVersion(String)**

Fetches the version of a custom version in this asset.

```csharp
public int GetCustomVersion(string friendlyName)
```

#### Parameters

`friendlyName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The friendly name of the custom version to retrieve.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The version of the retrieved custom version.

### **GetCustomVersion&lt;T&gt;()**

Fetches a custom version's enum value based off of its type.

```csharp
public T GetCustomVersion<T>()
```

#### Type Parameters

`T`<br>
The enum type of the custom version to retrieve.

#### Returns

T<br>
The enum value of the requested custom version.

#### Exceptions

[ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/system.argumentexception)<br>
Thrown when T is not an enumerated type.

### **GuessCustomVersionFromTypeAndEngineVersion(EngineVersion, Type)**

```csharp
public static int GuessCustomVersionFromTypeAndEngineVersion(EngineVersion chosenVersion, Type typ)
```

#### Parameters

`chosenVersion` [EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>

`typ` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **GetDefaultCustomVersionContainer(EngineVersion)**

Fetches a list of all default custom versions for a specific Unreal version.

```csharp
public static List<CustomVersion> GetDefaultCustomVersionContainer(EngineVersion chosenVersion)
```

#### Parameters

`chosenVersion` [EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>
The version of the engine to check against.

#### Returns

[List&lt;CustomVersion&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
A list of all the default custom version values for the given engine version.

### **ParseExport(AssetBinaryReader, Int32, Boolean)**

Read an export from disk.

```csharp
public void ParseExport(AssetBinaryReader reader, int i, bool read)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The binary reader.

`i` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index of the export in the export map to read.

`read` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not to serialize the body of the export. If false, simply converts to the respective sub-type.

### **ConvertExportToChildExportAndRead(AssetBinaryReader, Int32, Boolean)**

```csharp
public void ConvertExportToChildExportAndRead(AssetBinaryReader reader, int i, bool read)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`i` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`read` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **VerifyBinaryEquality()**

Checks whether or not this asset maintains binary equality when serialized.

```csharp
public bool VerifyBinaryEquality()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not the asset maintained binary equality.

### **GetParentClass(FName&, FName&)**

Finds the class path and export name of the SuperStruct of this asset, if it exists.

```csharp
public void GetParentClass(FName& parentClassPath, FName& parentClassExportName)
```

#### Parameters

`parentClassPath` [FName&](./uassetapi.unrealtypes.fname&.md)<br>
The class path of the SuperStruct of this asset, if it exists.

`parentClassExportName` [FName&](./uassetapi.unrealtypes.fname&.md)<br>
The export name of the SuperStruct of this asset, if it exists.

### **GetParentClassExportName(FName&)**

```csharp
internal FName GetParentClassExportName(FName& modulePath)
```

#### Parameters

`modulePath` [FName&](./uassetapi.unrealtypes.fname&.md)<br>

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>

### **AddImport(Import)**

Adds a new import to the import map. This is equivalent to adding directly to the [UAsset.Imports](./uassetapi.uasset.md#imports) list.

```csharp
public FPackageIndex AddImport(Import li)
```

#### Parameters

`li` [Import](./uassetapi.import.md)<br>
The new import to add to the import map.

#### Returns

[FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>
The FPackageIndex corresponding to the newly-added import.

### **SearchForImport(FName, FName, FPackageIndex, FName)**

Searches for an import in the import map based off of certain parameters.

```csharp
public int SearchForImport(FName classPackage, FName className, FPackageIndex outerIndex, FName objectName)
```

#### Parameters

`classPackage` [FName](./uassetapi.unrealtypes.fname.md)<br>
The ClassPackage that the requested import will have.

`className` [FName](./uassetapi.unrealtypes.fname.md)<br>
The ClassName that the requested import will have.

`outerIndex` [FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>
The CuterIndex that the requested import will have.

`objectName` [FName](./uassetapi.unrealtypes.fname.md)<br>
The ObjectName that the requested import will have.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index of the requested import in the name map, or zero if one could not be found.

### **SearchForImport(FName, FName, FName)**

Searches for an import in the import map based off of certain parameters.

```csharp
public int SearchForImport(FName classPackage, FName className, FName objectName)
```

#### Parameters

`classPackage` [FName](./uassetapi.unrealtypes.fname.md)<br>
The ClassPackage that the requested import will have.

`className` [FName](./uassetapi.unrealtypes.fname.md)<br>
The ClassName that the requested import will have.

`objectName` [FName](./uassetapi.unrealtypes.fname.md)<br>
The ObjectName that the requested import will have.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index of the requested import in the name map, or zero if one could not be found.

### **SearchForImport(FName)**

Searches for an import in the import map based off of certain parameters.

```csharp
public int SearchForImport(FName objectName)
```

#### Parameters

`objectName` [FName](./uassetapi.unrealtypes.fname.md)<br>
The ObjectName that the requested import will have.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index of the requested import in the name map, or zero if one could not be found.

### **PullSchemasFromAnotherAsset(FName, FName)**

```csharp
public bool PullSchemasFromAnotherAsset(FName path, FName desiredObject)
```

#### Parameters

`path` [FName](./uassetapi.unrealtypes.fname.md)<br>

`desiredObject` [FName](./uassetapi.unrealtypes.fname.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **CopySplitUp(Stream, Stream, Int32, Int32)**

Copies a portion of a stream to another stream.

```csharp
internal static void CopySplitUp(Stream input, Stream output, int start, int leng)
```

#### Parameters

`input` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
The input stream.

`output` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
The output stream.

`start` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The offset in the input stream to start copying from.

`leng` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The length in bytes of the data to be copied.

### **Read(AssetBinaryReader, Int32[], Int32[])**

Reads an asset into memory.

```csharp
public void Read(AssetBinaryReader reader, Int32[] manualSkips, Int32[] forceReads)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The input reader.

`manualSkips` [Int32[]](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
An array of export indices to skip parsing. For most applications, this should be left blank.

`forceReads` [Int32[]](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
An array of export indices that must be read, overriding entries in the manualSkips parameter. For most applications, this should be left blank.

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when this is an unversioned asset and [UAsset.ObjectVersion](./uassetapi.uasset.md#objectversion) is unspecified.

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **WriteData()**

Serializes an asset from memory.

```csharp
public MemoryStream WriteData()
```

#### Returns

[MemoryStream](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream)<br>
A new MemoryStream containing the full binary data of the serialized asset.

### **Write(MemoryStream&, MemoryStream&)**

Serializes and writes an asset to two split streams (.uasset and .uexp) from memory.

```csharp
public void Write(MemoryStream& uassetStream, MemoryStream& uexpStream)
```

#### Parameters

`uassetStream` [MemoryStream&](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream&)<br>
A stream containing the contents of the .uasset file.

`uexpStream` [MemoryStream&](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream&)<br>
A stream containing the contents of the .uexp file, if needed, otherwise null.

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when [UAsset.ObjectVersion](./uassetapi.uasset.md#objectversion) is unspecified.

### **Write(String)**

Serializes and writes an asset to disk from memory.

```csharp
public void Write(string outputPath)
```

#### Parameters

`outputPath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path on disk to write the asset to.

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when [UAsset.ObjectVersion](./uassetapi.uasset.md#objectversion) is unspecified.

### **SerializeJson(Boolean)**

Serializes this asset as JSON.

```csharp
public string SerializeJson(bool isFormatted)
```

#### Parameters

`isFormatted` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not the returned JSON string should be indented.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A serialized JSON string that represents the asset.

### **SerializeJson(Formatting)**

Serializes this asset as JSON.

```csharp
public string SerializeJson(Formatting jsonFormatting)
```

#### Parameters

`jsonFormatting` Formatting<br>
The formatting to use for the returned JSON string.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A serialized JSON string that represents the asset.

### **SerializeJsonObject(Object, Boolean)**

Serializes an object as JSON.

```csharp
public string SerializeJsonObject(object value, bool isFormatted)
```

#### Parameters

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The object to serialize as JSON.

`isFormatted` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not the returned JSON string should be indented.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A serialized JSON string that represents the object.

### **SerializeJsonObject(Object, Formatting)**

Serializes an object as JSON.

```csharp
public string SerializeJsonObject(object value, Formatting jsonFormatting)
```

#### Parameters

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
The object to serialize as JSON.

`jsonFormatting` Formatting<br>
The formatting to use for the returned JSON string.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A serialized JSON string that represents the object.

### **DeserializeJsonObject&lt;T&gt;(String)**

Deserializes an object from JSON.

```csharp
public T DeserializeJsonObject<T>(string json)
```

#### Type Parameters

`T`<br>

#### Parameters

`json` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A serialized JSON string to parse.

#### Returns

T<br>

### **DeserializeJson(String)**

Reads an asset from serialized JSON and initializes a new instance of the [UAsset](./uassetapi.uasset.md) class to store its data in memory.

```csharp
public static UAsset DeserializeJson(string json)
```

#### Parameters

`json` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
A serialized JSON string to parse.

#### Returns

[UAsset](./uassetapi.uasset.md)<br>

### **DeserializeJson(Stream)**

Reads an asset from serialized JSON and initializes a new instance of the [UAsset](./uassetapi.uasset.md) class to store its data in memory.

```csharp
public static UAsset DeserializeJson(Stream stream)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
A stream containing serialized JSON string to parse.

#### Returns

[UAsset](./uassetapi.uasset.md)<br>
