# FInternalArc

Namespace: UAssetAPI.IO

```csharp
public struct FInternalArc
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FInternalArc](./uassetapi.io.finternalarc.md)

## Fields

### **FromExportBundleIndex**

```csharp
public int FromExportBundleIndex;
```

### **ToExportBundleIndex**

```csharp
public int ToExportBundleIndex;
```

## Methods

### **Read(AssetBinaryReader)**

```csharp
FInternalArc Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FInternalArc](./uassetapi.io.finternalarc.md)<br>

### **Write(AssetBinaryWriter, Int32, Int32)**

```csharp
int Write(AssetBinaryWriter writer, int v2, int v3)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`v2` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

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
