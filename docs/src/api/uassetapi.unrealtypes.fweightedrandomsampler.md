# FWeightedRandomSampler

Namespace: UAssetAPI.UnrealTypes

```csharp
public class FWeightedRandomSampler : System.ICloneable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FWeightedRandomSampler](./uassetapi.unrealtypes.fweightedrandomsampler.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable)

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
