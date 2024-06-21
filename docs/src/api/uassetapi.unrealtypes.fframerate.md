# FFrameRate

Namespace: UAssetAPI.UnrealTypes

```csharp
public struct FFrameRate
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FFrameRate](./uassetapi.unrealtypes.fframerate.md)

## Fields

### **Numerator**

```csharp
public int Numerator;
```

### **Denominator**

```csharp
public int Denominator;
```

## Constructors

### **FFrameRate()**

```csharp
FFrameRate()
```

### **FFrameRate(Int32, Int32)**

```csharp
FFrameRate(int numerator, int denominator)
```

#### Parameters

`numerator` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`denominator` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Methods

### **ToString()**

```csharp
string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **TryParse(String, FFrameRate&)**

```csharp
bool TryParse(string s, FFrameRate& result)
```

#### Parameters

`s` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`result` [FFrameRate&](./uassetapi.unrealtypes.fframerate&.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
