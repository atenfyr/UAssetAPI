# FLinearColor

Namespace: UAssetAPI.UnrealTypes

A linear, 32-bit/component floating point RGBA color.

```csharp
public struct FLinearColor
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FLinearColor](./uassetapi.unrealtypes.flinearcolor.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable)

## Fields

### **R**

```csharp
public float R;
```

### **G**

```csharp
public float G;
```

### **B**

```csharp
public float B;
```

### **A**

```csharp
public float A;
```

## Constructors

### **FLinearColor(Single, Single, Single, Single)**

```csharp
FLinearColor(float R, float G, float B, float A)
```

#### Parameters

`R` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`G` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`B` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`A` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **FLinearColor(AssetBinaryReader)**

```csharp
FLinearColor(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Clone()**

```csharp
object Clone()
```

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **Write(AssetBinaryWriter)**

```csharp
int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
