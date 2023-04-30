# FMovieSceneEvaluationTreeNodeHandle

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FMovieSceneEvaluationTreeNodeHandle
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FMovieSceneEvaluationTreeNodeHandle](./uassetapi.propertytypes.structs.fmoviesceneevaluationtreenodehandle.md)

## Fields

### **ChildrenHandle**

Entry handle for the parent's children in FMovieSceneEvaluationTree::ChildNodes

```csharp
public FEvaluationTreeEntryHandle ChildrenHandle;
```

### **Index**

The index of this child within its parent's children

```csharp
public int Index;
```

## Constructors

### **FMovieSceneEvaluationTreeNodeHandle(Int32, Int32)**

```csharp
FMovieSceneEvaluationTreeNodeHandle(int _ChildrenHandle, int _Index)
```

#### Parameters

`_ChildrenHandle` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`_Index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
