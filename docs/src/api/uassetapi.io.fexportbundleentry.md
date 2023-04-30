# FExportBundleEntry

Namespace: UAssetAPI.IO

```csharp
public struct FExportBundleEntry
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FExportBundleEntry](./uassetapi.io.fexportbundleentry.md)

## Fields

### **LocalExportIndex**

```csharp
public uint LocalExportIndex;
```

### **CommandType**

```csharp
public EExportCommandType CommandType;
```

## Methods

### **Read(AssetBinaryReader)**

```csharp
FExportBundleEntry Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FExportBundleEntry](./uassetapi.io.fexportbundleentry.md)<br>

### **Write(AssetBinaryWriter, UInt32, EExportCommandType)**

```csharp
int Write(AssetBinaryWriter writer, uint lei, EExportCommandType typ)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`lei` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`typ` [EExportCommandType](./uassetapi.io.eexportcommandtype.md)<br>

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
