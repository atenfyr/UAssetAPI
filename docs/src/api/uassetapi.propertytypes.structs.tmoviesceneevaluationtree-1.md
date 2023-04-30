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

```csharp
public FMovieSceneEvaluationTreeNode RootNode;
```

### **ChildNodes**

```csharp
public TEvaluationTreeEntryContainer<FMovieSceneEvaluationTreeNode> ChildNodes;
```

## Constructors

### **TMovieSceneEvaluationTree()**

```csharp
public TMovieSceneEvaluationTree()
```
