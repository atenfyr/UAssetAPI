# FIntVector2

Namespace: UAssetAPI.UnrealTypes

Structure for integer vectors in 2-d space.

```csharp
public struct FIntVector2
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FIntVector2](./uassetapi.unrealtypes.fintvector2.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable), [IStruct&lt;FIntVector2&gt;](./uassetapi.propertytypes.objects.istruct-1.md)

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

### **FIntVector2(Int32, Int32)**

```csharp
FIntVector2(int x, int y)
```

#### Parameters

`x` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`y` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FIntVector2(AssetBinaryReader)**

```csharp
FIntVector2(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Read(AssetBinaryReader)**

```csharp
FIntVector2 Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FIntVector2](./uassetapi.unrealtypes.fintvector2.md)<br>

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

### **ToString()**

```csharp
string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FromString(String[], UAsset)**

```csharp
FIntVector2 FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[FIntVector2](./uassetapi.unrealtypes.fintvector2.md)<br>
