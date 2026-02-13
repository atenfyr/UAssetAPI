# FIntVector

Namespace: UAssetAPI.UnrealTypes

Structure for integer vectors in 3-d space.

```csharp
public struct FIntVector
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FIntVector](./uassetapi.unrealtypes.fintvector.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable), [IStruct&lt;FIntVector&gt;](./uassetapi.propertytypes.objects.istruct-1.md)

## Fields

### **X**

```csharp
public int X;
```

### **Y**

```csharp
public int Y;
```

### **Z**

```csharp
public int Z;
```

## Constructors

### **FIntVector(Int32, Int32, Int32)**

```csharp
FIntVector(int x, int y, int z)
```

#### Parameters

`x` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`y` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`z` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

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

### **Read(AssetBinaryReader)**

```csharp
FIntVector Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FIntVector](./uassetapi.unrealtypes.fintvector.md)<br>

### **Clone()**

```csharp
object Clone()
```

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **FromString(String[], UAsset)**

```csharp
FIntVector FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[FIntVector](./uassetapi.unrealtypes.fintvector.md)<br>

### **ToString()**

```csharp
string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
