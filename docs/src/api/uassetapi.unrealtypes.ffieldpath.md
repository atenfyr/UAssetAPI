# FFieldPath

Namespace: UAssetAPI.UnrealTypes

```csharp
public class FFieldPath : UAssetAPI.PropertyTypes.Objects.IStruct`1[[UAssetAPI.UnrealTypes.FFieldPath]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FFieldPath](./uassetapi.unrealtypes.ffieldpath.md)<br>
Implements [IStruct&lt;FFieldPath&gt;](./uassetapi.propertytypes.objects.istruct-1.md)

## Fields

### **Path**

Path to the FField object from the innermost FField to the outermost UObject (UPackage)

```csharp
public FName[] Path;
```

### **ResolvedOwner**

The cached owner of this field.

```csharp
public FPackageIndex ResolvedOwner;
```

## Constructors

### **FFieldPath(FName[], FPackageIndex, Int32)**

```csharp
public FFieldPath(FName[] path, FPackageIndex resolvedOwner, int numExports)
```

#### Parameters

`path` [FName[]](./uassetapi.unrealtypes.fname.md)<br>

`resolvedOwner` [FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

`numExports` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FFieldPath()**

```csharp
public FFieldPath()
```

### **FFieldPath(AssetBinaryReader)**

```csharp
public FFieldPath(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Read(AssetBinaryReader)**

```csharp
public static FFieldPath Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FFieldPath](./uassetapi.unrealtypes.ffieldpath.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
public int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **FromString(String[], UAsset)**

```csharp
public static FFieldPath FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[FFieldPath](./uassetapi.unrealtypes.ffieldpath.md)<br>
