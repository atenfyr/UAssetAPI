# FMovieSceneEvaluationTreeNode

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public class FMovieSceneEvaluationTreeNode
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FMovieSceneEvaluationTreeNode](./uassetapi.propertytypes.structs.fmoviesceneevaluationtreenode.md)

## Fields

### **Range**

The time-range that this node represents

```csharp
public TRange<FFrameNumber> Range;
```

### **Parent**

```csharp
public FMovieSceneEvaluationTreeNodeHandle Parent;
```

### **ChildrenID**

Identifier for the child node entries associated with this node (FMovieSceneEvaluationTree::ChildNodes)

```csharp
public FEvaluationTreeEntryHandle ChildrenID;
```

### **DataID**

Identifier for externally stored data entries associated with this node

```csharp
public FEvaluationTreeEntryHandle DataID;
```

## Constructors

### **FMovieSceneEvaluationTreeNode(AssetBinaryReader)**

```csharp
public FMovieSceneEvaluationTreeNode(AssetBinaryReader reader)
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
