# FMovieSceneSubSequenceTree

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FMovieSceneSubSequenceTree
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FMovieSceneSubSequenceTree](./uassetapi.propertytypes.structs.fmoviescenesubsequencetree.md)

## Fields

### **Data**

```csharp
public TMovieSceneEvaluationTree<FMovieSceneSubSequenceTreeEntry> Data;
```

## Constructors

### **FMovieSceneSubSequenceTree(TMovieSceneEvaluationTree&lt;FMovieSceneSubSequenceTreeEntry&gt;)**

```csharp
FMovieSceneSubSequenceTree(TMovieSceneEvaluationTree<FMovieSceneSubSequenceTreeEntry> data)
```

#### Parameters

`data` [TMovieSceneEvaluationTree&lt;FMovieSceneSubSequenceTreeEntry&gt;](./uassetapi.propertytypes.structs.tmoviesceneevaluationtree-1.md)<br>

## Methods

### **Read(AssetBinaryReader)**

```csharp
FMovieSceneSubSequenceTree Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FMovieSceneSubSequenceTree](./uassetapi.propertytypes.structs.fmoviescenesubsequencetree.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
