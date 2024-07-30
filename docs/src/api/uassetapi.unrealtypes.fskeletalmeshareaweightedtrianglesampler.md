# FSkeletalMeshAreaWeightedTriangleSampler

Namespace: UAssetAPI.UnrealTypes

Allows area weighted sampling of triangles on a skeletal mesh.

```csharp
public class FSkeletalMeshAreaWeightedTriangleSampler : FWeightedRandomSampler, System.ICloneable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FWeightedRandomSampler](./uassetapi.unrealtypes.fweightedrandomsampler.md) → [FSkeletalMeshAreaWeightedTriangleSampler](./uassetapi.unrealtypes.fskeletalmeshareaweightedtrianglesampler.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable)

## Fields

### **Prob**

```csharp
public Single[] Prob;
```

### **Alias**

```csharp
public Int32[] Alias;
```

### **TotalWeight**

```csharp
public float TotalWeight;
```

## Constructors

### **FSkeletalMeshAreaWeightedTriangleSampler(AssetBinaryReader)**

```csharp
public FSkeletalMeshAreaWeightedTriangleSampler(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

### **FSkeletalMeshAreaWeightedTriangleSampler()**

```csharp
public FSkeletalMeshAreaWeightedTriangleSampler()
```
