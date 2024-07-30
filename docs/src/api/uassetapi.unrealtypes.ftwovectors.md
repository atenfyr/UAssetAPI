# FTwoVectors

Namespace: UAssetAPI.UnrealTypes

```csharp
public struct FTwoVectors
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FTwoVectors](./uassetapi.unrealtypes.ftwovectors.md)

## Fields

### **V1**

```csharp
public FVector V1;
```

### **V2**

```csharp
public FVector V2;
```

## Constructors

### **FTwoVectors(FVector, FVector)**

```csharp
FTwoVectors(FVector v1, FVector v2)
```

#### Parameters

`v1` [FVector](./uassetapi.unrealtypes.fvector.md)<br>

`v2` [FVector](./uassetapi.unrealtypes.fvector.md)<br>

### **FTwoVectors(AssetBinaryReader)**

```csharp
FTwoVectors(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
