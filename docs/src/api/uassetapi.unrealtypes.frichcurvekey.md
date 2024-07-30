# FRichCurveKey

Namespace: UAssetAPI.UnrealTypes

One key in a rich, editable float curve

```csharp
public struct FRichCurveKey
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FRichCurveKey](./uassetapi.unrealtypes.frichcurvekey.md)

## Fields

### **InterpMode**

```csharp
public ERichCurveInterpMode InterpMode;
```

### **TangentMode**

```csharp
public ERichCurveTangentMode TangentMode;
```

### **TangentWeightMode**

```csharp
public ERichCurveTangentWeightMode TangentWeightMode;
```

### **Time**

```csharp
public float Time;
```

### **Value**

```csharp
public float Value;
```

### **ArriveTangent**

```csharp
public float ArriveTangent;
```

### **ArriveTangentWeight**

```csharp
public float ArriveTangentWeight;
```

### **LeaveTangent**

```csharp
public float LeaveTangent;
```

### **LeaveTangentWeight**

```csharp
public float LeaveTangentWeight;
```

## Constructors

### **FRichCurveKey()**

```csharp
FRichCurveKey()
```

### **FRichCurveKey(ERichCurveInterpMode, ERichCurveTangentMode, ERichCurveTangentWeightMode, Single, Single, Single, Single, Single, Single)**

```csharp
FRichCurveKey(ERichCurveInterpMode interpMode, ERichCurveTangentMode tangentMode, ERichCurveTangentWeightMode tangentWeightMode, float time, float value, float arriveTangent, float arriveTangentWeight, float leaveTangent, float leaveTangentWeight)
```

#### Parameters

`interpMode` [ERichCurveInterpMode](./uassetapi.unrealtypes.engineenums.erichcurveinterpmode.md)<br>

`tangentMode` [ERichCurveTangentMode](./uassetapi.unrealtypes.engineenums.erichcurvetangentmode.md)<br>

`tangentWeightMode` [ERichCurveTangentWeightMode](./uassetapi.unrealtypes.engineenums.erichcurvetangentweightmode.md)<br>

`time` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`value` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`arriveTangent` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`arriveTangentWeight` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`leaveTangent` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`leaveTangentWeight` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **FRichCurveKey(AssetBinaryReader)**

```csharp
FRichCurveKey(AssetBinaryReader reader)
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
