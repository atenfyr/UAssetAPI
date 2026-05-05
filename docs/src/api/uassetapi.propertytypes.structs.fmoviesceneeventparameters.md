# FMovieSceneEventParameters

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FMovieSceneEventParameters
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FMovieSceneEventParameters](./uassetapi.propertytypes.structs.fmoviesceneeventparameters.md)

## Fields

### **StructType**

```csharp
public FSoftObjectPath StructType;
```

### **StructBytes**

```csharp
public Byte[] StructBytes;
```

## Constructors

### **FMovieSceneEventParameters(FSoftObjectPath, Byte[])**

```csharp
FMovieSceneEventParameters(FSoftObjectPath structType, Byte[] structBytes)
```

#### Parameters

`structType` [FSoftObjectPath](./uassetapi.propertytypes.objects.fsoftobjectpath.md)<br>

`structBytes` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **FMovieSceneEventParameters(AssetBinaryReader)**

```csharp
FMovieSceneEventParameters(AssetBinaryReader reader)
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
