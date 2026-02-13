# FTwoVectors

Namespace: UAssetAPI.UnrealTypes

```csharp
public struct FTwoVectors
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FTwoVectors](./uassetapi.unrealtypes.ftwovectors.md)<br>
Implements [IStruct&lt;FTwoVectors&gt;](./uassetapi.propertytypes.objects.istruct-1.md)

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

### **Read(AssetBinaryReader)**

```csharp
FTwoVectors Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FTwoVectors](./uassetapi.unrealtypes.ftwovectors.md)<br>

### **ToString()**

```csharp
string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FromString(String[], UAsset)**

```csharp
FTwoVectors FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[FTwoVectors](./uassetapi.unrealtypes.ftwovectors.md)<br>
