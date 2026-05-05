# FTransform

Namespace: UAssetAPI.UnrealTypes

Transform composed of Scale, Rotation (as a quaternion), and Translation.
 Transforms can be used to convert from one space to another, for example by transforming
 positions and directions from local space to world space.
 
 Transformation of position vectors is applied in the order: Scale -&gt; Rotate -&gt; Translate.
 Transformation of direction vectors is applied in the order: Scale -&gt; Rotate.
 
 Order matters when composing transforms: C = A * B will yield a transform C that logically
 first applies A then B to any subsequent transformation. Note that this is the opposite order of quaternion (FQuat) multiplication.
 
 Example: LocalToWorld = (DeltaRotation * LocalToWorld) will change rotation in local space by DeltaRotation.
 Example: LocalToWorld = (LocalToWorld * DeltaRotation) will change rotation in world space by DeltaRotation.

```csharp
public struct FTransform
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FTransform](./uassetapi.unrealtypes.ftransform.md)

## Fields

### **Rotation**

Rotation of this transformation, as a quaternion

```csharp
public FQuat Rotation;
```

### **Translation**

Translation of this transformation, as a vector.

```csharp
public FVector Translation;
```

### **Scale3D**

3D scale (always applied in local space) as a vector.

```csharp
public FVector Scale3D;
```

## Constructors

### **FTransform(FQuat, FVector, FVector)**

```csharp
FTransform(FQuat rotation, FVector translation, FVector scale3D)
```

#### Parameters

`rotation` [FQuat](./uassetapi.unrealtypes.fquat.md)<br>

`translation` [FVector](./uassetapi.unrealtypes.fvector.md)<br>

`scale3D` [FVector](./uassetapi.unrealtypes.fvector.md)<br>

### **FTransform(AssetBinaryReader)**

```csharp
FTransform(AssetBinaryReader reader)
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
