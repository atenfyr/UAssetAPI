# FMeshToMeshVertData

Namespace: UAssetAPI.PropertyTypes.Structs

A structure for holding mesh-to-mesh triangle influences to skin one mesh to another (similar to a wrap deformer)

```csharp
public class FMeshToMeshVertData
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FMeshToMeshVertData](./uassetapi.propertytypes.structs.fmeshtomeshvertdata.md)

## Fields

### **PositionBaryCoordsAndDist**

Barycentric coords and distance along normal for the position of the final vert

```csharp
public Vector4fPropertyData PositionBaryCoordsAndDist;
```

### **NormalBaryCoordsAndDist**

Barycentric coords and distance along normal for the location of the unit normal endpoint.
 Actual normal = ResolvedNormalPosition - ResolvedPosition

```csharp
public Vector4fPropertyData NormalBaryCoordsAndDist;
```

### **TangentBaryCoordsAndDist**

Barycentric coords and distance along normal for the location of the unit Tangent endpoint.
 Actual normal = ResolvedNormalPosition - ResolvedPosition

```csharp
public Vector4fPropertyData TangentBaryCoordsAndDist;
```

### **SourceMeshVertIndices**

Contains the 3 indices for verts in the source mesh forming a triangle, the last element
 is a flag to decide how the skinning works, 0xffff uses no simulation, and just normal
 skinning, anything else uses the source mesh and the above skin data to get the final position

```csharp
public UInt16[] SourceMeshVertIndices;
```

### **Weight**

For weighted averaging of multiple triangle influences

```csharp
public float Weight;
```

### **Padding**

Dummy for alignment

```csharp
public uint Padding;
```

## Constructors

### **FMeshToMeshVertData(AssetBinaryReader)**

```csharp
public FMeshToMeshVertData(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

### **FMeshToMeshVertData(Vector4fPropertyData, Vector4fPropertyData, Vector4fPropertyData, UInt16[], Single, UInt32)**

```csharp
public FMeshToMeshVertData(Vector4fPropertyData positionBaryCoordsAndDist, Vector4fPropertyData normalBaryCoordsAndDist, Vector4fPropertyData tangentBaryCoordsAndDist, UInt16[] sourceMeshVertIndices, float weight, uint padding)
```

#### Parameters

`positionBaryCoordsAndDist` [Vector4fPropertyData](./uassetapi.propertytypes.structs.vector4fpropertydata.md)<br>

`normalBaryCoordsAndDist` [Vector4fPropertyData](./uassetapi.propertytypes.structs.vector4fpropertydata.md)<br>

`tangentBaryCoordsAndDist` [Vector4fPropertyData](./uassetapi.propertytypes.structs.vector4fpropertydata.md)<br>

`sourceMeshVertIndices` [UInt16[]](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

`weight` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`padding` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **FMeshToMeshVertData()**

```csharp
public FMeshToMeshVertData()
```

## Methods

### **Read(AssetBinaryReader)**

```csharp
public void Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
public int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
