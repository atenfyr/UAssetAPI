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

### **NameMap**

.usmap name map

```csharp
public List<string> NameMap;
```

### **EnumMap**

.usmap enum map

```csharp
public Dictionary<string, List<string>> EnumMap;
```

### **Schemas**

.usmap schema map

```csharp
public Dictionary<string, UsmapSchema> Schemas;
```

### **CityHash64Map**

Pre-computed CityHash64 map for all relevant strings

```csharp
public Dictionary<ulong, string> CityHash64Map;
```

### **USMAP_MAGIC**

Magic number for the .usmap format

```csharp
public static ushort USMAP_MAGIC;
```

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

Reads a .usmap file from a BinaryReader and initializes a new instance of the [Usmap](./uassetapi.unversioned.usmap.md) class to store its data in memory.

```csharp
public Usmap(UsmapBinaryReader reader)
```

#### Parameters

`reader` [UsmapBinaryReader](./uassetapi.usmapbinaryreader.md)<br>
The file's BinaryReader that this instance will read from.

#### Exceptions

[FormatException](https://docs.microsoft.com/en-us/dotnet/api/system.formatexception)<br>
Throw when the asset cannot be parsed correctly.

### **Usmap()**

Initializes a new instance of the [Usmap](./uassetapi.unversioned.usmap.md) class. This instance will store no data and does not represent any file in particular until the [Usmap.ReadHeader(UsmapBinaryReader)](./uassetapi.unversioned.usmap.md#readheaderusmapbinaryreader) method is manually called.

```csharp
public Usmap()
```

## Methods

### **GetAllProperties(String)**

Retrieve all the properties that a particular schema can reference.

```csharp
public IList<UsmapProperty> GetAllProperties(string schemaName)
```

#### Parameters

`schemaName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the schema of interest.

#### Returns

[IList&lt;UsmapProperty&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1)<br>
All the properties that the schema can reference.

### **GetAllPropertiesAnnotated(String, IDictionary&lt;String, String&gt;, Boolean, String, String)**

Retrieve all the properties that a particular schema can reference as an annotated, human-readable text file.

```csharp
public string GetAllPropertiesAnnotated(string schemaName, IDictionary<string, string> customAnnotations, bool recursive, string headerPrefix, string headerSuffix)
```

#### Parameters

`schemaName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the schema of interest.

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

### **TryGetProperty&lt;T&gt;(FName, AncestryInfo, Int32, T&, Int32&)**

Attempts to retrieve the corresponding .usmap property, given its ancestry.

```csharp
public bool TryGetProperty<T>(FName propertyName, AncestryInfo ancestry, int dupIndex, T& propDat, Int32& idx)
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

`propDat` T&<br>
The property.

`idx` [Int32&](https://docs.microsoft.com/en-us/dotnet/api/system.int32&)<br>
The index of the property.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not the property was successfully found.

### **TryGetPropertyData&lt;T&gt;(FName, AncestryInfo, T&)**

Attempts to retrieve the corresponding .usmap property data corresponding to a specific property, given its ancestry.

```csharp
public bool TryGetPropertyData<T>(FName propertyName, AncestryInfo ancestry, T& propDat)
```

#### Type Parameters

`T`<br>
The type of property data to output.

#### Parameters

`propertyName` [FName](./uassetapi.unrealtypes.fname.md)<br>
The name of the property to search for.

`ancestry` [AncestryInfo](./uassetapi.propertytypes.objects.ancestryinfo.md)<br>
The ancestry of the property to search for.

`propDat` T&<br>
The property data.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not the property data was successfully found.

### **PathToStream(String)**

Creates a MemoryStream from an asset path.

```csharp
public MemoryStream PathToStream(string p)
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
