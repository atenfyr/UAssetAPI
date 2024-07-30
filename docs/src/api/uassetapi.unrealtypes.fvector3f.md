# FVector3f

Namespace: UAssetAPI.UnrealTypes

A vector in 3-D space composed of components (X, Y, Z) with floating point precision.

```csharp
public struct FVector3f
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FVector3f](./uassetapi.unrealtypes.fvector3f.md)<br>
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

### **Z**

```csharp
public float Z;
```

## Constructors

### **FVector3f(Single, Single, Single)**

```csharp
FVector3f(float x, float y, float z)
```

#### Parameters

`x` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`y` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`z` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **FVector3f(AssetBinaryReader)**

```csharp
FVector3f(AssetBinaryReader reader)
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
