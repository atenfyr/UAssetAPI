# Usmap

Namespace: UAssetAPI.Unversioned

```csharp
public class Usmap
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Usmap](./uassetapi.unversioned.usmap.md)

## Fields

### **FilePath**

The path of the file on disk. This does not need to be specified for regular parsing.

```csharp
public string FilePath;
```

### **FileVersionUE4**

Game UE4 object version

```csharp
public ObjectVersion FileVersionUE4;
```

### **FileVersionUE5**

Game UE5 object version

```csharp
public ObjectVersionUE5 FileVersionUE5;
```

### **CustomVersionContainer**

All the custom versions stored in the archive.

```csharp
public List<CustomVersion> CustomVersionContainer;
```

### **NetCL**

```csharp
public uint NetCL;
```

### **SkipBlueprintSchemas**

Whether or not to skip blueprint schemas serialized in this mappings file. Only useful for testing.

```csharp
public bool SkipBlueprintSchemas;
```

### **NameMap**

.usmap name map

```csharp
public List<string> NameMap;
```

### **EnumMap**

.usmap enum map

```csharp
public IDictionary<string, UsmapEnum> EnumMap;
```

### **Schemas**

.usmap schema map

```csharp
public IDictionary<string, UsmapSchema> Schemas;
```

### **FailedExtensions**

List of extensions that failed to parse.

```csharp
public List<string> FailedExtensions;
```

### **PathsAlreadyProcessedForSchemas**

```csharp
public ConcurrentDictionary<string, byte> PathsAlreadyProcessedForSchemas;
```

### **USMAP_MAGIC**

Magic number for the .usmap format

```csharp
public static ushort USMAP_MAGIC;
```

## Properties

### **AreFNamesCaseInsensitive**

Whether or not FNames are case insensitive. Modifying this property is an expensive operation, and will re-construct several dictionaries.

```csharp
public bool AreFNamesCaseInsensitive { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **Usmap(String)**

Reads a .usmap file from disk and initializes a new instance of the [Usmap](./uassetapi.unversioned.usmap.md) class to store its data in memory.

```csharp
public Usmap(string path)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path of the file file on disk that this instance will read from.

#### Exceptions

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the file cannot be parsed correctly.

### **Usmap(UsmapBinaryReader)**

Reads a .usmap file from a UsmapBinaryReader and initializes a new instance of the [Usmap](./uassetapi.unversioned.usmap.md) class to store its data in memory.

```csharp
public Usmap(UsmapBinaryReader reader)
```

#### Parameters

`reader` [UsmapBinaryReader](./uassetapi.usmapbinaryreader.md)<br>
The file's UsmapBinaryReader that this instance will read from.

#### Exceptions

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **Usmap()**

Initializes a new instance of the [Usmap](./uassetapi.unversioned.usmap.md) class. This instance will store no data and does not represent any file in particular until the [Usmap.ReadHeader(UsmapBinaryReader)](./uassetapi.unversioned.usmap.md#readheaderusmapbinaryreader) method is manually called.

```csharp
public Usmap()
```

## Methods

### **GetSchemaFromStructExport(String, UAsset)**

```csharp
public static UsmapSchema GetSchemaFromStructExport(string exportName, UAsset asset)
```

#### Parameters

`exportName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[UsmapSchema](./uassetapi.unversioned.usmapschema.md)<br>

### **GetSchemaFromStructExport(StructExport, Boolean)**

```csharp
public static UsmapSchema GetSchemaFromStructExport(StructExport exp, bool isCaseInsensitive)
```

#### Parameters

`exp` [StructExport](./uassetapi.exporttypes.structexport.md)<br>

`isCaseInsensitive` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[UsmapSchema](./uassetapi.unversioned.usmapschema.md)<br>

### **GetAllProperties(String, String, UAsset)**

Retrieve all the properties that a particular schema can reference.

```csharp
public IList<UsmapProperty> GetAllProperties(string schemaName, string modulePath, UAsset asset)
```

#### Parameters

`schemaName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the schema of interest.

`modulePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Module path of the schema of interest.

`asset` [UAsset](./uassetapi.uasset.md)<br>
An asset to also search for schemas within.

#### Returns

[IList&lt;UsmapProperty&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1)<br>
All the properties that the schema can reference.

### **GetAllPropertiesAnnotated(String, UAsset, IDictionary&lt;String, String&gt;, Boolean, String, String)**

Retrieve all the properties that a particular schema can reference as an annotated, human-readable text file.

```csharp
public string GetAllPropertiesAnnotated(string schemaName, UAsset asset, IDictionary<string, string> customAnnotations, bool recursive, string headerPrefix, string headerSuffix)
```

#### Parameters

`schemaName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the schema of interest.

`asset` [UAsset](./uassetapi.uasset.md)<br>
An asset to also search for schemas within.

`customAnnotations` [IDictionary&lt;String, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.idictionary-2)<br>
A map of strings to give custom annotations.

`recursive` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not to dump data for parent schemas as well.

`headerPrefix` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The prefix of the subheader for each relevant schema.

`headerSuffix` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The suffix of the subheader for each relevant schema.

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
An annotated, human-readable text file containing the properties that the schema can reference.

### **GetSchemaFromName(String, UAsset, String, Boolean)**

```csharp
public UsmapSchema GetSchemaFromName(string nm, UAsset asset, string modulePath, bool throwExceptions)
```

#### Parameters

`nm` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

`modulePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`throwExceptions` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[UsmapSchema](./uassetapi.unversioned.usmapschema.md)<br>

### **TryGetProperty&lt;T&gt;(FName, AncestryInfo, Int32, UAsset, T&, Int32&)**

Attempts to retrieve the corresponding .usmap property, given its ancestry.

```csharp
public bool TryGetProperty<T>(FName propertyName, AncestryInfo ancestry, int dupIndex, UAsset asset, T& propDat, Int32& idx)
```

#### Type Parameters

`T`<br>
The type of property to output.

#### Parameters

`propertyName` [FName](./uassetapi.unrealtypes.fname.md)<br>
The name of the property to search for.

`ancestry` [AncestryInfo](./uassetapi.propertytypes.objects.ancestryinfo.md)<br>
The ancestry of the property to search for.

`dupIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The duplication index of the property to search for. If unknown, set to 0.

`asset` [UAsset](./uassetapi.uasset.md)<br>
An asset to also search for schemas within.

`propDat` T&<br>
The property.

`idx` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>
The index of the property.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not the property was successfully found.

### **TryGetPropertyData&lt;T&gt;(FName, AncestryInfo, UAsset, T&)**

Attempts to retrieve the corresponding .usmap property data corresponding to a specific property, given its ancestry.

```csharp
public bool TryGetPropertyData<T>(FName propertyName, AncestryInfo ancestry, UAsset asset, T& propDat)
```

#### Type Parameters

`T`<br>
The type of property data to output.

#### Parameters

`propertyName` [FName](./uassetapi.unrealtypes.fname.md)<br>
The name of the property to search for.

`ancestry` [AncestryInfo](./uassetapi.propertytypes.objects.ancestryinfo.md)<br>
The ancestry of the property to search for.

`asset` [UAsset](./uassetapi.uasset.md)<br>
An asset to also search for schemas within.

`propDat` T&<br>
The property data.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not the property data was successfully found.

### **PathToStream(String)**

Creates a MemoryStream from an asset path.

```csharp
public static MemoryStream PathToStream(string p)
```

#### Parameters

`p` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the input file.

#### Returns

[MemoryStream](https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream)<br>
A new MemoryStream that stores the binary data of the input file.

### **PathToReader(String)**

Creates a BinaryReader from an asset path.

```csharp
public UsmapBinaryReader PathToReader(string p)
```

#### Parameters

`p` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the input file.

#### Returns

[UsmapBinaryReader](./uassetapi.usmapbinaryreader.md)<br>
A new BinaryReader that stores the binary data of the input file.

### **ReadHeader(UsmapBinaryReader)**

```csharp
public UsmapBinaryReader ReadHeader(UsmapBinaryReader reader)
```

#### Parameters

`reader` [UsmapBinaryReader](./uassetapi.usmapbinaryreader.md)<br>

#### Returns

[UsmapBinaryReader](./uassetapi.usmapbinaryreader.md)<br>

### **Read(UsmapBinaryReader)**

```csharp
public void Read(UsmapBinaryReader compressedReader)
```

#### Parameters

`compressedReader` [UsmapBinaryReader](./uassetapi.usmapbinaryreader.md)<br>
