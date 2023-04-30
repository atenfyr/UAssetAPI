# FSectionEvaluationDataTree

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FSectionEvaluationDataTree
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FSectionEvaluationDataTree](./uassetapi.propertytypes.structs.fsectionevaluationdatatree.md)

## Fields

### **Tree**

```csharp
public TMovieSceneEvaluationTree<List<PropertyData>> Tree;
```

## Constructors

### **FSectionEvaluationDataTree(TMovieSceneEvaluationTree&lt;List&lt;PropertyData&gt;&gt;)**

```csharp
FSectionEvaluationDataTree(TMovieSceneEvaluationTree<List<PropertyData>> tree)
```

#### Parameters

`tree` [TMovieSceneEvaluationTree&lt;List&lt;PropertyData&gt;&gt;](./uassetapi.propertytypes.structs.tmoviesceneevaluationtree-1.md)<br>

## Methods

### **Read(AssetBinaryReader)**

```csharp
FSectionEvaluationDataTree Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FSectionEvaluationDataTree](./uassetapi.propertytypes.structs.fsectionevaluationdatatree.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
