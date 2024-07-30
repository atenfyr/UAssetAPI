# FSkeletalMeshSamplingRegionBuiltData

Namespace: UAssetAPI.UnrealTypes

Built data for sampling a single region of a skeletal mesh

```csharp
public class FSkeletalMeshSamplingRegionBuiltData
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FSkeletalMeshSamplingRegionBuiltData](./uassetapi.unrealtypes.fskeletalmeshsamplingregionbuiltdata.md)

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

### **FSkeletalMeshSamplingRegionBuiltData(AssetBinaryReader)**

```csharp
public FSkeletalMeshSamplingRegionBuiltData(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
public int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
