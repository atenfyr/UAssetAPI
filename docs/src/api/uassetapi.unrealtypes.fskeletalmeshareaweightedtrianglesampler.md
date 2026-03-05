# FSkeletalMeshAreaWeightedTriangleSampler

Namespace: UAssetAPI.UnrealTypes

Allows area weighted sampling of triangles on a skeletal mesh.

```csharp
public class FSkeletalMeshAreaWeightedTriangleSampler : FWeightedRandomSampler, System.ICloneable, UAssetAPI.PropertyTypes.Objects.IStruct`1[[UAssetAPI.UnrealTypes.FWeightedRandomSampler]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FWeightedRandomSampler](./uassetapi.unrealtypes.fweightedrandomsampler.md) → [FSkeletalMeshAreaWeightedTriangleSampler](./uassetapi.unrealtypes.fskeletalmeshareaweightedtrianglesampler.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable), [IStruct&lt;FWeightedRandomSampler&gt;](./uassetapi.propertytypes.objects.istruct-1.md)

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
