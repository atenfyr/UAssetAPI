# FMovieSceneDoubleChannel

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public class FMovieSceneDoubleChannel : FMovieSceneChannel`1
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FMovieSceneChannel&lt;Double&gt;](./uassetapi.propertytypes.structs.fmoviescenechannel-1.md) → [FMovieSceneDoubleChannel](./uassetapi.propertytypes.structs.fmoviescenedoublechannel.md)

## Fields

### **PreInfinityExtrap**

```csharp
public ERichCurveExtrapolation PreInfinityExtrap;
```

### **PostInfinityExtrap**

```csharp
public ERichCurveExtrapolation PostInfinityExtrap;
```

### **TimesStructLength**

```csharp
public int TimesStructLength;
```

### **Times**

```csharp
public FFrameNumber[] Times;
```

### **ValuesStructLength**

```csharp
public int ValuesStructLength;
```

### **Values**

```csharp
public FMovieSceneValue`1[] Values;
```

### **DefaultValue**

```csharp
public double DefaultValue;
```

### **bHasDefaultValue**

```csharp
public bool bHasDefaultValue;
```

### **TickResolution**

```csharp
public FFrameRate TickResolution;
```

### **bShowCurve**

```csharp
public bool bShowCurve;
```

## Constructors

### **FMovieSceneDoubleChannel(AssetBinaryReader)**

```csharp
public FMovieSceneDoubleChannel(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
