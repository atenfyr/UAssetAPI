# FVector2f

Namespace: UAssetAPI.UnrealTypes

A vector in 2-D space composed of components (X, Y) with floating point precision.

```csharp
public struct FVector2f
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FVector2f](./uassetapi.unrealtypes.fvector2f.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable)

## Fields

### **X**

```csharp
public float X;
```

### **Y**

```csharp
public float Y;
```

## Constructors

### **FVector2f(Single, Single)**

```csharp
FVector2f(float x, float y)
```

#### Parameters

`x` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`y` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **FVector2f(AssetBinaryReader)**

```csharp
FVector2f(AssetBinaryReader reader)
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
