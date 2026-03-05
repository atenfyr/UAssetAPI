# FVector4f

Namespace: UAssetAPI.UnrealTypes

A vector in 4-D space composed of components (X, Y, Z, W) with floating point precision.

```csharp
public struct FVector4f
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FVector4f](./uassetapi.unrealtypes.fvector4f.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable), [IStruct&lt;FVector4f&gt;](./uassetapi.propertytypes.objects.istruct-1.md)

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

### **W**

```csharp
public float W;
```

## Constructors

### **FVector4f(Single, Single, Single, Single)**

```csharp
FVector4f(float x, float y, float z, float w)
```

#### Parameters

`x` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`y` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`z` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`w` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **FVector4f(AssetBinaryReader)**

```csharp
FVector4f(AssetBinaryReader reader)
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

### **Read(AssetBinaryReader)**

```csharp
FVector4f Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FVector4f](./uassetapi.unrealtypes.fvector4f.md)<br>

### **ToString()**

```csharp
string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FromString(String[], UAsset)**

```csharp
FVector4f FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[FVector4f](./uassetapi.unrealtypes.fvector4f.md)<br>
