# FVector2D

Namespace: UAssetAPI.UnrealTypes

A vector in 2-D space composed of components (X, Y) with floating/double point precision.

```csharp
public struct FVector2D
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FVector2D](./uassetapi.unrealtypes.fvector2d.md)

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

## Constructors

### **FVector2D(Double, Double)**

```csharp
FVector2D(double x, double y)
```

#### Parameters

`x` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`y` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **FVector2D(Single, Single, Single)**

```csharp
FVector2D(float x, float y, float z)
```

#### Parameters

`x` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`y` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`z` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **FVector2D(AssetBinaryReader)**

```csharp
FVector2D(AssetBinaryReader reader)
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
