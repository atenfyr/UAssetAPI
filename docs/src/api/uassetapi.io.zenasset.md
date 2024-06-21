# ZenAsset

Namespace: UAssetAPI.IO

```csharp
public class ZenAsset : UAssetAPI.UnrealPackage, INameMap
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UnrealPackage](./uassetapi.unrealpackage.md) → [ZenAsset](./uassetapi.io.zenasset.md)<br>
Implements [INameMap](./uassetapi.io.inamemap.md)

## Fields

### **GlobalData**

The global data of the game that this asset is from.

```csharp
public IOGlobalData GlobalData;
```

### **ZenVersion**

```csharp
public EZenPackageVersion ZenVersion;
```

### **Name**

```csharp
public FName Name;
```

### **SourceName**

```csharp
public FName SourceName;
```

### **VerifyHashes**

Should serialized hashes be verified on read?

```csharp
public bool VerifyHashes;
```

### **HashVersion**

```csharp
public ulong HashVersion;
```

### **BulkDataMap**

```csharp
public Byte[] BulkDataMap;
```

### **ImportedPublicExportHashes**

```csharp
public UInt64[] ImportedPublicExportHashes;
```

### **Imports**

Map of object imports. UAssetAPI used to call these "links."

```csharp
public List<FPackageObjectIndex> Imports;
```

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

### **ZenAsset(String, EngineVersion, Usmap)**

Reads an asset from disk and initializes a new instance of the [UAsset](./uassetapi.uasset.md) class to store its data in memory.

```csharp
public ZenAsset(string path, EngineVersion engineVersion, Usmap mappings)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path of the asset file on disk that this instance will read from.

`engineVersion` [EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>
The version of the Unreal Engine that will be used to parse this asset. If the asset is versioned, this can be left unspecified.

`mappings` [Usmap](./uassetapi.unversioned.usmap.md)<br>
A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when this is an unversioned asset and [ObjectVersion](./uassetapi.unrealtypes.objectversion.md) is unspecified.

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **ZenAsset(AssetBinaryReader, EngineVersion, Usmap, Boolean)**

Reads an asset from a BinaryReader and initializes a new instance of the [ZenAsset](./uassetapi.io.zenasset.md) class to store its data in memory.

```csharp
public ZenAsset(AssetBinaryReader reader, EngineVersion engineVersion, Usmap mappings, bool useSeparateBulkDataFiles)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The asset's BinaryReader that this instance will read from.

`engineVersion` [EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>
The version of the Unreal Engine that will be used to parse this asset. If the asset is versioned, this can be left unspecified.

`mappings` [Usmap](./uassetapi.unversioned.usmap.md)<br>
A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.

`useSeparateBulkDataFiles` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Does this asset uses separate bulk data files (.uexp, .ubulk)?

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when this is an unversioned asset and [ObjectVersion](./uassetapi.unrealtypes.objectversion.md) is unspecified.

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **ZenAsset(EngineVersion, Usmap)**

Initializes a new instance of the [ZenAsset](./uassetapi.io.zenasset.md) class. This instance will store no asset data and does not represent any asset in particular until the [ZenAsset.Read(AssetBinaryReader, Int32[], Int32[])](./uassetapi.io.zenasset.md#readassetbinaryreader-int32-int32) method is manually called.

```csharp
public ZenAsset(EngineVersion engineVersion, Usmap mappings)
```

#### Parameters

`engineVersion` [EngineVersion](./uassetapi.unrealtypes.engineversion.md)<br>
The version of the Unreal Engine that will be used to parse this asset. If the asset is versioned, this can be left unspecified.

`mappings` [Usmap](./uassetapi.unversioned.usmap.md)<br>
A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.

### **ZenAsset(String, ObjectVersion, List&lt;CustomVersion&gt;, Usmap)**

Reads an asset from disk and initializes a new instance of the [ZenAsset](./uassetapi.io.zenasset.md) class to store its data in memory.

```csharp
public ZenAsset(string path, ObjectVersion objectVersion, List<CustomVersion> customVersionContainer, Usmap mappings)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path of the asset file on disk that this instance will read from.

`objectVersion` [ObjectVersion](./uassetapi.unrealtypes.objectversion.md)<br>
The object version of the Unreal Engine that will be used to parse this asset

`customVersionContainer` [List&lt;CustomVersion&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
A list of custom versions to parse this asset with.

`mappings` [Usmap](./uassetapi.unversioned.usmap.md)<br>
A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when this is an unversioned asset and [ObjectVersion](./uassetapi.unrealtypes.objectversion.md) is unspecified.

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **ZenAsset(AssetBinaryReader, ObjectVersion, List&lt;CustomVersion&gt;, Usmap, Boolean)**

Reads an asset from a BinaryReader and initializes a new instance of the [ZenAsset](./uassetapi.io.zenasset.md) class to store its data in memory.

```csharp
public ZenAsset(AssetBinaryReader reader, ObjectVersion objectVersion, List<CustomVersion> customVersionContainer, Usmap mappings, bool useSeparateBulkDataFiles)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The asset's BinaryReader that this instance will read from.

`objectVersion` [ObjectVersion](./uassetapi.unrealtypes.objectversion.md)<br>
The object version of the Unreal Engine that will be used to parse this asset

`customVersionContainer` [List&lt;CustomVersion&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
A list of custom versions to parse this asset with.

`mappings` [Usmap](./uassetapi.unversioned.usmap.md)<br>
A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.

`useSeparateBulkDataFiles` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Does this asset uses separate bulk data files (.uexp, .ubulk)?

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when this is an unversioned asset and [ObjectVersion](./uassetapi.unrealtypes.objectversion.md) is unspecified.

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **ZenAsset(ObjectVersion, List&lt;CustomVersion&gt;, Usmap)**

Initializes a new instance of the [ZenAsset](./uassetapi.io.zenasset.md) class. This instance will store no asset data and does not represent any asset in particular until the [ZenAsset.Read(AssetBinaryReader, Int32[], Int32[])](./uassetapi.io.zenasset.md#readassetbinaryreader-int32-int32) method is manually called.

```csharp
public ZenAsset(ObjectVersion objectVersion, List<CustomVersion> customVersionContainer, Usmap mappings)
```

#### Parameters

`objectVersion` [ObjectVersion](./uassetapi.unrealtypes.objectversion.md)<br>
The object version of the Unreal Engine that will be used to parse this asset

`customVersionContainer` [List&lt;CustomVersion&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
A list of custom versions to parse this asset with.

`mappings` [Usmap](./uassetapi.unversioned.usmap.md)<br>
A valid set of mappings for the game that this asset is from. Not required unless unversioned properties are used.

### **ZenAsset()**

Initializes a new instance of the [ZenAsset](./uassetapi.io.zenasset.md) class. This instance will store no asset data and does not represent any asset in particular until the [ZenAsset.Read(AssetBinaryReader, Int32[], Int32[])](./uassetapi.io.zenasset.md#readassetbinaryreader-int32-int32) method is manually called.

```csharp
public ZenAsset()
```

## Methods

### **GetStringFromCityHash64(UInt64)**

```csharp
public string GetStringFromCityHash64(ulong val)
```

#### Parameters

`val` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

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

### **GetParentClassExportName()**

```csharp
internal FName GetParentClassExportName()
```

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>

### **Read(AssetBinaryReader, Int32[], Int32[])**

Reads an asset into memory.

```csharp
public void Read(AssetBinaryReader reader, Int32[] manualSkips, Int32[] forceReads)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The input reader.

`manualSkips` [Int32[]](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
An array of export indexes to skip parsing. For most applications, this should be left blank.

`forceReads` [Int32[]](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
An array of export indexes that must be read, overriding entries in the manualSkips parameter. For most applications, this should be left blank.

#### Exceptions

[UnknownEngineVersionException](./uassetapi.unknownengineversionexception.md)<br>
Thrown when [ObjectVersion](./uassetapi.unrealtypes.objectversion.md) is unspecified.

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **WriteData()**

Serializes an asset from memory.

```csharp
public MemoryStream WriteData()
```

#### Returns

[MemoryStream](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream)<br>
A stream that the asset has been serialized to.

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
Thrown when [ObjectVersion](./uassetapi.unrealtypes.objectversion.md) is unspecified.
