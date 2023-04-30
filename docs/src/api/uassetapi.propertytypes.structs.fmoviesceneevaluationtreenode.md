# FMovieSceneEvaluationTreeNode

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FMovieSceneEvaluationTreeNode
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FMovieSceneEvaluationTreeNode](./uassetapi.propertytypes.structs.fmoviesceneevaluationtreenode.md)

## Fields

### **Range**

The time-range that this node represents

```csharp
public FFrameNumberRange Range;
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

## Methods

### **Read(AssetBinaryReader)**

```csharp
void Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
