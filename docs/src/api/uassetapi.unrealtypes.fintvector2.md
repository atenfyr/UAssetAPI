# FIntVector

Namespace: UAssetAPI.UnrealTypes

Structure for integer vectors in 2-d space.

```csharp
public struct FIntVector2
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FIntVector2](./uassetapi.unrealtypes.fintvector2.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable)

## Fields

### **X**

```csharp
public int X;
```

### **Y**

```csharp
public int Y;
```

## Constructors

### **FIntVector(Int32, Int32)**

```csharp
FIntVector(int x, int y)
```

#### Parameters

`x` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`y` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br

### **FIntVector(AssetBinaryReader)**

```csharp
FIntVector(AssetBinaryReader reader)
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

### **Clone()**

```csharp
object Clone()
```

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
