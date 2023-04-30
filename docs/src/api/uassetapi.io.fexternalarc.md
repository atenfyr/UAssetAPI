# FExternalArc

Namespace: UAssetAPI.IO

```csharp
public struct FExternalArc
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FExternalArc](./uassetapi.io.fexternalarc.md)

## Fields

### **FromImportIndex**

```csharp
public int FromImportIndex;
```

### **ToExportBundleIndex**

```csharp
public int ToExportBundleIndex;
```

## Methods

### **Read(AssetBinaryReader)**

```csharp
FExternalArc Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FExternalArc](./uassetapi.io.fexternalarc.md)<br>

### **Write(AssetBinaryWriter, Int32, EExportCommandType, Int32)**

```csharp
int Write(AssetBinaryWriter writer, int v1, EExportCommandType v2, int v3)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`v1` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`v2` [EExportCommandType](./uassetapi.io.eexportcommandtype.md)<br>

`v3` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

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
