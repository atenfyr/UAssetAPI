# FMatrix

Namespace: UAssetAPI.UnrealTypes

4x4 matrix of floating point values.

```csharp
public struct FMatrix
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FMatrix](./uassetapi.unrealtypes.fmatrix.md)<br>
Implements [IStruct&lt;FMatrix&gt;](./uassetapi.propertytypes.objects.istruct-1.md)

## Fields

### **XPlane**

```csharp
public FPlane XPlane;
```

### **YPlane**

```csharp
public FPlane YPlane;
```

### **ZPlane**

```csharp
public FPlane ZPlane;
```

### **WPlane**

```csharp
public FPlane WPlane;
```

## Constructors

### **FMatrix(FPlane, FPlane, FPlane, FPlane)**

```csharp
FMatrix(FPlane xPlane, FPlane yPlane, FPlane zPlane, FPlane wPlane)
```

#### Parameters

`xPlane` [FPlane](./uassetapi.unrealtypes.fplane.md)<br>

`yPlane` [FPlane](./uassetapi.unrealtypes.fplane.md)<br>

`zPlane` [FPlane](./uassetapi.unrealtypes.fplane.md)<br>

`wPlane` [FPlane](./uassetapi.unrealtypes.fplane.md)<br>

### **FMatrix(AssetBinaryReader)**

```csharp
FMatrix(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Read(AssetBinaryReader)**

```csharp
FMatrix Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FMatrix](./uassetapi.unrealtypes.fmatrix.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FromString(String[], UAsset)**

```csharp
FMatrix FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[FMatrix](./uassetapi.unrealtypes.fmatrix.md)<br>
