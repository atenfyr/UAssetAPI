# FMovieSceneEvaluationFieldEntityTree

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FMovieSceneEvaluationFieldEntityTree
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FMovieSceneEvaluationFieldEntityTree](./uassetapi.propertytypes.structs.fmoviesceneevaluationfieldentitytree.md)

## Fields

### **SerializedData**

```csharp
public TMovieSceneEvaluationTree<FEntityAndMetaDataIndex> SerializedData;
```

## Constructors

### **FMovieSceneEvaluationFieldEntityTree(TMovieSceneEvaluationTree&lt;FEntityAndMetaDataIndex&gt;)**

```csharp
FMovieSceneEvaluationFieldEntityTree(TMovieSceneEvaluationTree<FEntityAndMetaDataIndex> serializedData)
```

#### Parameters

`serializedData` [TMovieSceneEvaluationTree&lt;FEntityAndMetaDataIndex&gt;](./uassetapi.propertytypes.structs.tmoviesceneevaluationtree-1.md)<br>

## Methods

### **Read(AssetBinaryReader)**

```csharp
FMovieSceneEvaluationFieldEntityTree Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FMovieSceneEvaluationFieldEntityTree](./uassetapi.propertytypes.structs.fmoviesceneevaluationfieldentitytree.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
