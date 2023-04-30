# FPackageObjectIndex

Namespace: UAssetAPI.UnrealTypes

```csharp
public struct FPackageObjectIndex
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FPackageObjectIndex](./uassetapi.unrealtypes.fpackageobjectindex.md)

## Fields

### **Type**

```csharp
public EPackageObjectIndexType Type;
```

### **Hash**

```csharp
public ulong Hash;
```

### **Invalid**

```csharp
public static ulong Invalid;
```

## Properties

### **Export**

```csharp
public uint Export { get; set; }
```

#### Property Value

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **ImportedPackageIndex**

```csharp
public uint ImportedPackageIndex { get; set; }
```

#### Property Value

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **ImportedPublicExportHashIndex**

```csharp
public uint ImportedPublicExportHashIndex { get; set; }
```

#### Property Value

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **IsNull**

```csharp
public bool IsNull { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsExport**

```csharp
public bool IsExport { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsImport**

```csharp
public bool IsImport { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsScriptImport**

```csharp
public bool IsScriptImport { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsPackageImport**

```csharp
public bool IsPackageImport { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Methods

### **ToFPackageIndex(ZenAsset)**

```csharp
FPackageIndex ToFPackageIndex(ZenAsset asset)
```

#### Parameters

`asset` [ZenAsset](./uassetapi.io.zenasset.md)<br>

#### Returns

[FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

### **ToImport(ZenAsset)**

```csharp
Import ToImport(ZenAsset asset)
```

#### Parameters

`asset` [ZenAsset](./uassetapi.io.zenasset.md)<br>

#### Returns

[Import](./uassetapi.import.md)<br>

### **GetHashCode()**

```csharp
int GetHashCode()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Equals(Object)**

```csharp
bool Equals(object obj)
```

#### Parameters

`obj` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Unpack(UInt64)**

```csharp
FPackageObjectIndex Unpack(ulong packed)
```

#### Parameters

`packed` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

#### Returns

[FPackageObjectIndex](./uassetapi.unrealtypes.fpackageobjectindex.md)<br>

### **Pack(EPackageObjectIndexType, UInt64)**

```csharp
ulong Pack(EPackageObjectIndexType typ, ulong hash)
```

#### Parameters

`typ` [EPackageObjectIndexType](./uassetapi.unrealtypes.epackageobjectindextype.md)<br>

`hash` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **Pack(FPackageObjectIndex)**

```csharp
ulong Pack(FPackageObjectIndex unpacked)
```

#### Parameters

`unpacked` [FPackageObjectIndex](./uassetapi.unrealtypes.fpackageobjectindex.md)<br>

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **Read(UnrealBinaryReader)**

```csharp
FPackageObjectIndex Read(UnrealBinaryReader reader)
```

#### Parameters

`reader` [UnrealBinaryReader](./uassetapi.unrealbinaryreader.md)<br>

#### Returns

[FPackageObjectIndex](./uassetapi.unrealtypes.fpackageobjectindex.md)<br>

### **Write(UnrealBinaryWriter, EPackageObjectIndexType, UInt64)**

```csharp
int Write(UnrealBinaryWriter writer, EPackageObjectIndexType typ, ulong hash)
```

#### Parameters

`writer` [UnrealBinaryWriter](./uassetapi.unrealbinarywriter.md)<br>

`typ` [EPackageObjectIndexType](./uassetapi.unrealtypes.epackageobjectindextype.md)<br>

`hash` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Write(UnrealBinaryWriter)**

```csharp
int Write(UnrealBinaryWriter writer)
```

#### Parameters

`writer` [UnrealBinaryWriter](./uassetapi.unrealbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
