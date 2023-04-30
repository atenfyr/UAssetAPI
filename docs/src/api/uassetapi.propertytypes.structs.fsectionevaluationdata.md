# FSectionEvaluationData

Namespace: UAssetAPI.PropertyTypes.Structs

```csharp
public struct FSectionEvaluationData
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FSectionEvaluationData](./uassetapi.propertytypes.structs.fsectionevaluationdata.md)

## Fields

### **ImplIndex**

```csharp
public int ImplIndex;
```

### **ForcedTime**

```csharp
public FFrameNumber ForcedTime;
```

### **Flags**

```csharp
public ESectionEvaluationFlags Flags;
```

## Constructors

### **FSectionEvaluationData(Int32, FFrameNumber, Byte)**

```csharp
FSectionEvaluationData(int implIndex, FFrameNumber forcedTime, byte flags)
```

#### Parameters

`implIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`forcedTime` [FFrameNumber](./uassetapi.unrealtypes.fframenumber.md)<br>

`flags` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
