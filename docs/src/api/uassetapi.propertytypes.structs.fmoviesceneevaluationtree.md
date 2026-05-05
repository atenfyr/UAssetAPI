# FMovieSceneEvaluationTree

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public class FMovieSceneEvaluationTree
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FMovieSceneEvaluationTree](./uassetapi.propertytypes.structs.fmoviesceneevaluationtree.md)

## Fields

### **RootNode**

This tree's root node

```csharp
public FMovieSceneEvaluationTreeNode RootNode;
```

### **ChildNodes**

Segmented array of all child nodes within this tree (in no particular order)

```csharp
public TEvaluationTreeEntryContainer<FMovieSceneEvaluationTreeNode> ChildNodes;
```

## Constructors

### **FMovieSceneEvaluationTree(AssetBinaryReader)**

```csharp
public FMovieSceneEvaluationTree(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
public void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
