# FMovieSceneSubSequenceTreeEntry

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FMovieSceneSubSequenceTreeEntry
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FMovieSceneSubSequenceTreeEntry](./uassetapi.propertytypes.structs.fmoviescenesubsequencetreeentry.md)

## Fields

### **SequenceID**

```csharp
public uint SequenceID;
```

### **Flags**

```csharp
public ESectionEvaluationFlags Flags;
```

### **RootToSequenceWarpCounter**

```csharp
public StructPropertyData RootToSequenceWarpCounter;
```

## Constructors

### **FMovieSceneSubSequenceTreeEntry(UInt32, Byte, StructPropertyData)**

```csharp
FMovieSceneSubSequenceTreeEntry(uint sequenceID, byte flags, StructPropertyData _struct)
```

#### Parameters

`sequenceID` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`flags` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

`_struct` [StructPropertyData](./uassetapi.propertytypes.structs.structpropertydata.md)<br>

### **FMovieSceneSubSequenceTreeEntry(AssetBinaryReader)**

```csharp
FMovieSceneSubSequenceTreeEntry(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
