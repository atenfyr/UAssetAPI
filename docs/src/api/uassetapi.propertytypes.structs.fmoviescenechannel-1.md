# FMovieSceneChannel&lt;T&gt;

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public class FMovieSceneChannel<T>
```

#### Type Parameters

`T`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FMovieSceneChannel&lt;T&gt;](./uassetapi.propertytypes.structs.fmoviescenechannel-1.md)

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
public T DefaultValue;
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

### **FMovieSceneChannel()**

```csharp
public FMovieSceneChannel()
```

### **FMovieSceneChannel(AssetBinaryReader, Func&lt;T&gt;)**

```csharp
public FMovieSceneChannel(AssetBinaryReader reader, Func<T> valueReader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`valueReader` Func&lt;T&gt;<br>

## Methods

### **Write(AssetBinaryWriter, Action&lt;T&gt;)**

```csharp
public void Write(AssetBinaryWriter writer, Action<T> valueWriter)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`valueWriter` Action&lt;T&gt;<br>
