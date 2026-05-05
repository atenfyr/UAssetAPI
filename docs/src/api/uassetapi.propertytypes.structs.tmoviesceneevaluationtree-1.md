# TMovieSceneEvaluationTree&lt;T&gt;

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public class TMovieSceneEvaluationTree<T> : FMovieSceneEvaluationTree
```

#### Type Parameters

`T`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FMovieSceneEvaluationTree](./uassetapi.propertytypes.structs.fmoviesceneevaluationtree.md) → [TMovieSceneEvaluationTree&lt;T&gt;](./uassetapi.propertytypes.structs.tmoviesceneevaluationtree-1.md)

## Fields

### **Data**

Tree data container that corresponds to FMovieSceneEvaluationTreeNode::DataID

```csharp
public TEvaluationTreeEntryContainer<T> Data;
```

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

### **TMovieSceneEvaluationTree(AssetBinaryReader, Func&lt;T&gt;)**

```csharp
public TMovieSceneEvaluationTree(AssetBinaryReader reader, Func<T> valueReader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`valueReader` Func&lt;T&gt;<br>

## Methods

### **Write(AssetBinaryWriter, Action&lt;T&gt;)**

```csharp
public void Write(AssetBinaryWriter writer, Action<T> valueWriter)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`valueWriter` Action&lt;T&gt;<br>
