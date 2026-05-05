# FMovieSceneValue&lt;T&gt;

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public class FMovieSceneValue<T>
```

#### Type Parameters

`T`<br>

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FMovieSceneValue&lt;T&gt;](./uassetapi.propertytypes.structs.fmoviescenevalue-1.md)

## Fields

### **Value**

```csharp
public T Value;
```

### **Tangent**

```csharp
public FMovieSceneTangentData Tangent;
```

### **InterpMode**

```csharp
public ERichCurveInterpMode InterpMode;
```

### **TangentMode**

```csharp
public ERichCurveTangentMode TangentMode;
```

### **padding**

```csharp
public Byte[] padding;
```

## Constructors

### **FMovieSceneValue(AssetBinaryReader, T)**

```csharp
public FMovieSceneValue(AssetBinaryReader reader, T value)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`value` T<br>

## Methods

### **Write(AssetBinaryWriter, Action&lt;T&gt;)**

```csharp
public void Write(AssetBinaryWriter writer, Action<T> valueWriter)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`valueWriter` Action&lt;T&gt;<br>
