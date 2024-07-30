# FSectionEvaluationDataTree

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FSectionEvaluationDataTree
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FSectionEvaluationDataTree](./uassetapi.propertytypes.structs.fsectionevaluationdatatree.md)

## Fields

### **Tree**

```csharp
public TMovieSceneEvaluationTree<StructPropertyData> Tree;
```

## Constructors

### **FSectionEvaluationDataTree(AssetBinaryReader)**

```csharp
FSectionEvaluationDataTree(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **&lt;.ctor&gt;g__ReadTree|1_1(AssetBinaryReader)**

```csharp
StructPropertyData <.ctor>g__ReadTree|1_1(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[StructPropertyData](./uassetapi.propertytypes.structs.structpropertydata.md)<br>

### **&lt;Write&gt;g__WriteTree|2_0(AssetBinaryWriter, StructPropertyData)**

```csharp
void <Write>g__WriteTree|2_0(AssetBinaryWriter writer, StructPropertyData data)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`data` [StructPropertyData](./uassetapi.propertytypes.structs.structpropertydata.md)<br>
