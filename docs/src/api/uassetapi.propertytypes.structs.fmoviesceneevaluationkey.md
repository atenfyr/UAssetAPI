# FMovieSceneEvaluationKey

Namespace: UAssetAPI.PropertyTypes.Structs

Keyable struct that represents a particular entity within an evaluation template (either a section/template or a track)

```csharp
public struct FMovieSceneEvaluationKey
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FMovieSceneEvaluationKey](./uassetapi.propertytypes.structs.fmoviesceneevaluationkey.md)

## Fields

### **SequenceID**

ID of the sequence that the entity is contained within

```csharp
public uint SequenceID;
```

### **TrackIdentifier**

ID of the track this key relates to

```csharp
public uint TrackIdentifier;
```

### **SectionIndex**

Index of the section template within the track this key relates to (or -1 where this key relates to a track)

```csharp
public uint SectionIndex;
```

## Constructors

### **FMovieSceneEvaluationKey(UInt32, UInt32, UInt32)**

```csharp
FMovieSceneEvaluationKey(uint _SequenceID, uint _TrackIdentifier, uint _SectionIndex)
```

#### Parameters

`_SequenceID` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`_TrackIdentifier` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`_SectionIndex` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **FMovieSceneEvaluationKey(AssetBinaryReader)**

```csharp
FMovieSceneEvaluationKey(AssetBinaryReader reader)
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
