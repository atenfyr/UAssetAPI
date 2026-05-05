# FFragment

Namespace: UAssetAPI.Unversioned

Unversioned header fragment.

```csharp
public class FFragment
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FFragment](./uassetapi.unversioned.ffragment.md)

## Fields

### **SkipNum**

Number of properties to skip before values.

```csharp
public int SkipNum;
```

### **ValueNum**

Number of subsequent property values stored.

```csharp
public int ValueNum;
```

### **bIsLast**

Is this the last fragment of the header?

```csharp
public bool bIsLast;
```

### **FirstNum**

```csharp
public int FirstNum;
```

### **bHasAnyZeroes**

```csharp
public bool bHasAnyZeroes;
```

## Properties

### **LastNum**

```csharp
public int LastNum { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **FFragment()**

```csharp
public FFragment()
```

### **FFragment(Int32, Int32, Boolean, Boolean, Int32)**

```csharp
public FFragment(int skipNum, int valueNum, bool bIsLast, bool bHasAnyZeroes, int firstNum)
```

#### Parameters

`skipNum` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`valueNum` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`bIsLast` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`bHasAnyZeroes` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`firstNum` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Methods

### **ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Pack()**

```csharp
public ushort Pack()
```

#### Returns

[UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

### **Unpack(UInt16)**

```csharp
public static FFragment Unpack(ushort Int)
```

#### Parameters

`Int` [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

#### Returns

[FFragment](./uassetapi.unversioned.ffragment.md)<br>

### **GetFromBounds(Int32, Int32, Int32, Boolean, Boolean)**

```csharp
public static FFragment GetFromBounds(int LastNumBefore, int FirstNum, int LastNum, bool hasAnyZeros, bool isLast)
```

#### Parameters

`LastNumBefore` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`FirstNum` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`LastNum` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`hasAnyZeros` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`isLast` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[FFragment](./uassetapi.unversioned.ffragment.md)<br>
