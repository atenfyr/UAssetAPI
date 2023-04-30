# FQuat

Namespace: UAssetAPI.UnrealTypes

Floating point quaternion that can represent a rotation about an axis in 3-D space.
 The X, Y, Z, W components also double as the Axis/Angle format.

```csharp
public class FQuat
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FQuat](./uassetapi.unrealtypes.fquat.md)

## Properties

### **X**

The quaternion's X-component.

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

The quaternion's Y-component.

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

The quaternion's Z-component.

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

### **W**

The quaternion's W-component.

```csharp
public double W { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **WFloat**

```csharp
public float WFloat { get; }
```

#### Property Value

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

## Constructors

### **FQuat(Double, Double, Double, Double)**

```csharp
public FQuat(double x, double y, double z, double w)
```

#### Parameters

`x` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`y` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`z` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`w` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **FQuat(Single, Single, Single, Single)**

```csharp
public FQuat(float x, float y, float z, float w)
```

#### Parameters

`x` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`y` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`z` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`w` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **FQuat()**

```csharp
public FQuat()
```
