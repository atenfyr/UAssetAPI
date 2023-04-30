# FExportBundleHeader

Namespace: UAssetAPI.IO

```csharp
public struct FExportBundleHeader
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FExportBundleHeader](./uassetapi.io.fexportbundleheader.md)

## Fields

### **SerialOffset**

```csharp
public ulong SerialOffset;
```

### **FirstEntryIndex**

```csharp
public uint FirstEntryIndex;
```

### **EntryCount**

```csharp
public uint EntryCount;
```

## Methods

### **Read(AssetBinaryReader)**

```csharp
FExportBundleHeader Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FExportBundleHeader](./uassetapi.io.fexportbundleheader.md)<br>

### **Write(AssetBinaryWriter, UInt64, UInt32, UInt32)**

```csharp
int Write(AssetBinaryWriter writer, ulong v1, uint v2, uint v3)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`v1` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

`v2` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`v3` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Write(AssetBinaryWriter)**

```csharp
int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
