# FRotator

Namespace: UAssetAPI.UnrealTypes

Implements a container for rotation information.
 All rotation values are stored in degrees.

```csharp
public struct FRotator
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FRotator](./uassetapi.unrealtypes.frotator.md)

## Properties

### **Pitch**

Rotation around the right axis (around Y axis), Looking up and down (0=Straight Ahead, +Up, -Down)

```csharp
public double Pitch { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **PitchFloat**

```csharp
public float PitchFloat { get; }
```

#### Property Value

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **Yaw**

Rotation around the up axis (around Z axis), Running in circles 0=East, +North, -South.

```csharp
public double Yaw { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **YawFloat**

```csharp
public float YawFloat { get; }
```

#### Property Value

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **Roll**

Rotation around the forward axis (around X axis), Tilting your head, 0=Straight, +Clockwise, -CCW.

```csharp
public double Roll { get; set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **RollFloat**

```csharp
public float RollFloat { get; }
```

#### Property Value

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

## Constructors

### **FRotator(Double, Double, Double)**

```csharp
FRotator(double pitch, double yaw, double roll)
```

#### Parameters

`pitch` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`yaw` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`roll` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **FRotator(Single, Single, Single)**

```csharp
FRotator(float pitch, float yaw, float roll)
```

#### Parameters

`pitch` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`yaw` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`roll` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **FRotator(AssetBinaryReader)**

```csharp
FRotator(AssetBinaryReader reader)
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
