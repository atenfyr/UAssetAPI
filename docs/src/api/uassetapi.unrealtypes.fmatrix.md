# FMatrix

Namespace: UAssetAPI.UnrealTypes

```csharp
public struct FMatrix
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FMatrix](./uassetapi.unrealtypes.fmatrix.md)

## Fields

### **Row1**

```csharp
public FVector4 Row1;
```

### **Row2**

```csharp
public FVector4 Row2;
```

### **Row3**

```csharp
public FVector4 Row3;
```

### **Row4**

```csharp
public FVector4 Row4;
```

## Constructors

### **FMatrix(FVector4, FVector4, FVector4, FVector4)**

```csharp
FMatrix(FVector4 row1, FVector4 row2, FVector4 row3, FVector4 row4)
```

#### Parameters

`row1` [FVector4](./uassetapi.unrealtypes.fvector4.md)<br>

`row2` [FVector4](./uassetapi.unrealtypes.fvector4.md)<br>

`row3` [FVector4](./uassetapi.unrealtypes.fvector4.md)<br>

`row4` [FVector4](./uassetapi.unrealtypes.fvector4.md)<br>

### **FMatrix(AssetBinaryReader)**

```csharp
FMatrix(AssetBinaryReader reader)
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
