# FMovieSceneSubSectionData

Namespace: UAssetAPI.PropertyTypes.Structs

Data that represents a single sub-section

```csharp
public struct FMovieSceneSubSectionData
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FMovieSceneSubSectionData](./uassetapi.propertytypes.structs.fmoviescenesubsectiondata.md)

## Fields

### **Section**

The sub section itself

```csharp
public FPackageIndex Section;
```

### **ObjectBindingId**

The object binding that the sub section belongs to (usually zero)

```csharp
public Guid ObjectBindingId;
```

### **Flags**

Evaluation flags for the section

```csharp
public ESectionEvaluationFlags Flags;
```

## Constructors

### **FMovieSceneSubSectionData(FPackageIndex, Guid, ESectionEvaluationFlags)**

```csharp
FMovieSceneSubSectionData(FPackageIndex section, Guid objectBindingId, ESectionEvaluationFlags flags)
```

#### Parameters

`section` [FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

`objectBindingId` [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid)<br>

`flags` [ESectionEvaluationFlags](./uassetapi.propertytypes.structs.esectionevaluationflags.md)<br>

### **FMovieSceneSubSectionData(AssetBinaryReader)**

```csharp
FMovieSceneSubSectionData(AssetBinaryReader reader)
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
