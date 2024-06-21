# FMovieSceneDoubleValue

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public class FMovieSceneDoubleValue
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FMovieSceneDoubleValue](./uassetapi.propertytypes.structs.fmoviescenedoublevalue.md)

## Fields

### **Value**

```csharp
public double Value;
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

### **FMovieSceneDoubleValue()**

```csharp
public FMovieSceneDoubleValue()
```

## Methods

### **Read(AssetBinaryReader)**

```csharp
public void Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
public void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
