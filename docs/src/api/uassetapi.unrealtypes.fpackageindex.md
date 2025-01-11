# FPackageIndex

Namespace: UAssetAPI.UnrealTypes

Wrapper for index into an ImportMap or ExportMap.
 
 Values greater than zero indicate that this is an index into the ExportMap.
 The actual array index will be (FPackageIndex - 1).
 
 Values less than zero indicate that this is an index into the ImportMap.
 The actual array index will be (-FPackageIndex - 1)

```csharp
public class FPackageIndex
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)

## Fields

### **Index**

Values greater than zero indicate that this is an index into the ExportMap.
 The actual array index will be (FPackageIndex - 1).
 
 Values less than zero indicate that this is an index into the ImportMap.
 The actual array index will be (-FPackageIndex - 1)

```csharp
public int Index;
```

## Constructors

### **FPackageIndex(Int32)**

```csharp
public FPackageIndex(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FPackageIndex(AssetBinaryReader)**

```csharp
public FPackageIndex(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **FromRawIndex(Int32)**

Returns an FPackageIndex based off of the index provided. Equivalent to [FPackageIndex.FPackageIndex(Int32)](./uassetapi.unrealtypes.fpackageindex.md#fpackageindexint32).

```csharp
public static FPackageIndex FromRawIndex(int index)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The index to create a new FPackageIndex with.

#### Returns

[FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>
A new FPackageIndex with the index provided.

### **IsImport()**

Returns true if this is an index into the import map.

```csharp
public bool IsImport()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if this is an index into the import map, false otherwise

### **IsExport()**

Returns true if this is an index into the export map.

```csharp
public bool IsExport()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if this is an index into the export map, false otherwise

### **IsNull()**

Return true if this represents null (i.e. neither an import nor an export)

```csharp
public bool IsNull()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if this index represents null, false otherwise

### **FromImport(Int32)**

Creates a FPackageIndex from an index in the import map.

```csharp
public static FPackageIndex FromImport(int importIndex)
```

#### Parameters

`importIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
An import index to create an FPackageIndex from.

#### Returns

[FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>
An FPackageIndex created from the import index.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when the provided import index is less than zero.

### **FromExport(Int32)**

Creates a FPackageIndex from an index in the export map.

```csharp
public static FPackageIndex FromExport(int exportIndex)
```

#### Parameters

`exportIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
An export index to create an FPackageIndex from.

#### Returns

[FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>
An FPackageIndex created from the export index.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when the provided export index is less than zero.

### **ToImport(UAsset)**

Check that this is an import index and return the corresponding import.

```csharp
public Import ToImport(UAsset asset)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>
The asset that this index is used in.

#### Returns

[Import](./uassetapi.import.md)<br>
The import that this index represents in the import map.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when this is not an index into the import map.

### **ToExport(UAsset)**

Check that this is an export index and return the corresponding export.

```csharp
public Export ToExport(UAsset asset)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>
The asset that this index is used in.

#### Returns

[Export](./uassetapi.exporttypes.export.md)<br>
The export that this index represents in the the export map.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when this is not an index into the export map.

### **ToExport&lt;T&gt;(UAsset)**

```csharp
public T ToExport<T>(UAsset asset)
```

#### Type Parameters

`T`<br>

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

T<br>

### **Equals(Object)**

```csharp
public bool Equals(object obj)
```

#### Parameters

`obj` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **GetHashCode()**

```csharp
public int GetHashCode()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Write(AssetBinaryWriter)**

```csharp
public int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
