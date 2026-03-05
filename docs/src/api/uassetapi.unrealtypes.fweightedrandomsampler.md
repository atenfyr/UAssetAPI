# FWeightedRandomSampler

Namespace: UAssetAPI.UnrealTypes

```csharp
public class FWeightedRandomSampler : System.ICloneable, UAssetAPI.PropertyTypes.Objects.IStruct`1[[UAssetAPI.UnrealTypes.FWeightedRandomSampler]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FWeightedRandomSampler](./uassetapi.unrealtypes.fweightedrandomsampler.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable), [IStruct&lt;FWeightedRandomSampler&gt;](./uassetapi.propertytypes.objects.istruct-1.md)

## Fields

### **Prob**

```csharp
public Single[] Prob;
```

### **Alias**

```csharp
public Int32[] Alias;
```

### **TotalWeight**

```csharp
public float TotalWeight;
```

## Constructors

### **FWeightedRandomSampler()**

```csharp
public FWeightedRandomSampler()
```

### **FWeightedRandomSampler(Single[], Int32[], Single)**

```csharp
public FWeightedRandomSampler(Single[] prob, Int32[] alias, float totalWeight)
```

#### Parameters

`prob` [Single[]](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`alias` [Int32[]](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`totalWeight` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **FWeightedRandomSampler(AssetBinaryReader)**

```csharp
public FWeightedRandomSampler(AssetBinaryReader reader)
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

### **Clone()**

```csharp
public object Clone()
```

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **Read(AssetBinaryReader)**

```csharp
public static FWeightedRandomSampler Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FWeightedRandomSampler](./uassetapi.unrealtypes.fweightedrandomsampler.md)<br>

### **ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FromString(String[], UAsset)**

```csharp
public static FWeightedRandomSampler FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[FWeightedRandomSampler](./uassetapi.unrealtypes.fweightedrandomsampler.md)<br>
