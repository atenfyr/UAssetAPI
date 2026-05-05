# FMovieSceneSegment

Namespace: UAssetAPI.PropertyTypes.Structs

Information about a single segment of an evaluation track

```csharp
public class FMovieSceneSegment
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FMovieSceneSegment](./uassetapi.propertytypes.structs.fmoviescenesegment.md)

## Fields

### **RangeOld**

The segment's range

```csharp
public TRange<float> RangeOld;
```

### **Range**

```csharp
public TRange<FFrameNumber> Range;
```

### **ID**

```csharp
public int ID;
```

### **bAllowEmpty**

Whether this segment has been generated yet or not

```csharp
public bool bAllowEmpty;
```

### **Impls**

Array of implementations that reside at the segment's range

```csharp
public StructPropertyData[] Impls;
```

## Constructors

### **FMovieSceneSegment()**

```csharp
public FMovieSceneSegment()
```

### **FMovieSceneSegment(AssetBinaryReader)**

```csharp
public FMovieSceneSegment(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
public int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
