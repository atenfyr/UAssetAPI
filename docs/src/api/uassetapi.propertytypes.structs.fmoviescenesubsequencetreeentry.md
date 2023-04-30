# FMovieSceneSubSequenceTreeEntry

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FMovieSceneSubSequenceTreeEntry
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FMovieSceneSubSequenceTreeEntry](./uassetapi.propertytypes.structs.fmoviescenesubsequencetreeentry.md)

## Fields

### **SequenceID**

```csharp
public FMovieSceneSequenceID SequenceID;
```

### **Flags**

```csharp
public ESectionEvaluationFlags Flags;
```

## Constructors

### **FMovieSceneSubSequenceTreeEntry(FMovieSceneSequenceID, Byte)**

```csharp
FMovieSceneSubSequenceTreeEntry(FMovieSceneSequenceID sequenceID, byte flags)
```

#### Parameters

`sequenceID` [FMovieSceneSequenceID](./uassetapi.propertytypes.structs.fmoviescenesequenceid.md)<br>

`flags` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
