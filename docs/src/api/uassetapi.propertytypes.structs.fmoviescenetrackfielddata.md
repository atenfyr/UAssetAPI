# FMovieSceneTrackFieldData

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FMovieSceneTrackFieldData
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FMovieSceneTrackFieldData](./uassetapi.propertytypes.structs.fmoviescenetrackfielddata.md)

## Fields

### **Field**

```csharp
public TMovieSceneEvaluationTree<FMovieSceneTrackIdentifier> Field;
```

## Constructors

### **FMovieSceneTrackFieldData(TMovieSceneEvaluationTree&lt;FMovieSceneTrackIdentifier&gt;)**

```csharp
FMovieSceneTrackFieldData(TMovieSceneEvaluationTree<FMovieSceneTrackIdentifier> field)
```

#### Parameters

`field` [TMovieSceneEvaluationTree&lt;FMovieSceneTrackIdentifier&gt;](./uassetapi.propertytypes.structs.tmoviesceneevaluationtree-1.md)<br>

## Methods

### **Read(AssetBinaryReader)**

```csharp
FMovieSceneTrackFieldData Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FMovieSceneTrackFieldData](./uassetapi.propertytypes.structs.fmoviescenetrackfielddata.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
