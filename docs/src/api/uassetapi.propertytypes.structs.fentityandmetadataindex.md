# FEntityAndMetaDataIndex

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FEntityAndMetaDataIndex
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FEntityAndMetaDataIndex](./uassetapi.propertytypes.structs.fentityandmetadataindex.md)

## Fields

### **EntityIndex**

```csharp
public int EntityIndex;
```

### **MetaDataIndex**

```csharp
public int MetaDataIndex;
```

## Constructors

### **FEntityAndMetaDataIndex(Int32, Int32)**

```csharp
FEntityAndMetaDataIndex(int entityIndex, int metaDataIndex)
```

#### Parameters

`entityIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`metaDataIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FEntityAndMetaDataIndex(AssetBinaryReader)**

```csharp
FEntityAndMetaDataIndex(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
