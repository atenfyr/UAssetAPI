# FPlane

Namespace: UAssetAPI.UnrealTypes

Structure for three dimensional planes.
 Stores the coeffecients as Xx+Yy+Zz=W.
 This is different from many other Plane classes that use Xx+Yy+Zz+W=0.

```csharp
public struct FPlane
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FPlane](./uassetapi.unrealtypes.fplane.md)

## Properties

### **X**

The plane's X-component.

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

The plane's Y-component.

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

The plane's Z-component.

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

The plane's W-component.

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

### **FPlane(Double, Double, Double, Double)**

```csharp
FPlane(double x, double y, double z, double w)
```

#### Parameters

`x` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`y` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`z` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

`w` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **FPlane(Single, Single, Single, Single)**

```csharp
FPlane(float x, float y, float z, float w)
```

#### Parameters

`x` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`y` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`z` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

`w` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **FPlane(AssetBinaryReader)**

```csharp
FPlane(AssetBinaryReader reader)
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
