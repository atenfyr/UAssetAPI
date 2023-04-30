# FFrameNumberRange

Namespace: UAssetAPI.UnrealTypes

```csharp
public struct FFrameNumberRange
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FFrameNumberRange](./uassetapi.unrealtypes.fframenumberrange.md)

## Fields

### **LowerBound**

```csharp
public FFrameNumberRangeBound LowerBound;
```

### **UpperBound**

```csharp
public FFrameNumberRangeBound UpperBound;
```

## Constructors

### **FFrameNumberRange(AssetBinaryReader)**

```csharp
FFrameNumberRange(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Read(AssetBinaryReader)**

```csharp
void Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
