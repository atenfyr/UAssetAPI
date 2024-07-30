# FVector

Namespace: UAssetAPI.UnrealTypes

A vector in 3-D space composed of components (X, Y, Z) with floating/double point precision.

```csharp
public struct FVector
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FVector](./uassetapi.unrealtypes.fvector.md)

## Properties

### **X**

The vector's X-component.

```csharp
public double X { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **XFloat**

```csharp
public float XFloat { get; }
```

#### Property Value

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **Y**

The vector's Y-component.

```csharp
public double Y { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **YFloat**

```csharp
public float YFloat { get; }
```

#### Property Value

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **Z**

The vector's Z-component.

```csharp
public double Z { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **ZFloat**

```csharp
public float ZFloat { get; }
```

#### Property Value

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

## Constructors

### **FVector(Double, Double, Double)**

```csharp
FVector(double x, double y, double z)
```

#### Parameters

`x` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`y` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`z` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **FVector(Single, Single, Single)**

```csharp
FVector(float x, float y, float z)
```

#### Parameters

`x` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`y` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`z` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **FVector(AssetBinaryReader)**

```csharp
FVector(AssetBinaryReader reader)
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
