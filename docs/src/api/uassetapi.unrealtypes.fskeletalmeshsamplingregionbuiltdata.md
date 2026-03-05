# FSkeletalMeshSamplingRegionBuiltData

Namespace: UAssetAPI.UnrealTypes

Built data for sampling a single region of a skeletal mesh

```csharp
public class FSkeletalMeshSamplingRegionBuiltData : UAssetAPI.PropertyTypes.Objects.IStruct`1[[UAssetAPI.UnrealTypes.FSkeletalMeshSamplingRegionBuiltData]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FSkeletalMeshSamplingRegionBuiltData](./uassetapi.unrealtypes.fskeletalmeshsamplingregionbuiltdata.md)<br>
Implements [IStruct&lt;FSkeletalMeshSamplingRegionBuiltData&gt;](./uassetapi.propertytypes.objects.istruct-1.md)

## Fields

### **TriangleIndices**

```csharp
public Int32[] TriangleIndices;
```

### **Vertices**

```csharp
public Int32[] Vertices;
```

### **BoneIndices**

```csharp
public Int32[] BoneIndices;
```

## Constructors

### **FSkeletalMeshSamplingRegionBuiltData()**

```csharp
public FSkeletalMeshSamplingRegionBuiltData()
```

### **FSkeletalMeshSamplingRegionBuiltData(AssetBinaryReader)**

```csharp
public FSkeletalMeshSamplingRegionBuiltData(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Read(AssetBinaryReader)**

```csharp
public static FSkeletalMeshSamplingRegionBuiltData Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FSkeletalMeshSamplingRegionBuiltData](./uassetapi.unrealtypes.fskeletalmeshsamplingregionbuiltdata.md)<br>

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
public static FSkeletalMeshSamplingRegionBuiltData FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[FSkeletalMeshSamplingRegionBuiltData](./uassetapi.unrealtypes.fskeletalmeshsamplingregionbuiltdata.md)<br>
