# FSerializedNameHeader

Namespace: UAssetAPI.IO

```csharp
public class FSerializedNameHeader
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FSerializedNameHeader](./uassetapi.io.fserializednameheader.md)

## Fields

### **bIsWide**

```csharp
public bool bIsWide;
```

### **Len**

```csharp
public int Len;
```

## Constructors

### **FSerializedNameHeader()**

```csharp
public FSerializedNameHeader()
```

## Methods

### **Read(BinaryReader)**

```csharp
public static FSerializedNameHeader Read(BinaryReader reader)
```

#### Parameters

`reader` [BinaryReader](https://docs.microsoft.com/en-us/dotnet/api/system.io.binaryreader)<br>

#### Returns

[FSerializedNameHeader](./uassetapi.io.fserializednameheader.md)<br>

### **Write(BinaryWriter, Boolean, Int32)**

```csharp
public static void Write(BinaryWriter writer, bool bIsWideVal, int lenVal)
```

#### Parameters

`writer` [BinaryWriter](https://docs.microsoft.com/en-us/dotnet/api/system.io.binarywriter)<br>

`bIsWideVal` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`lenVal` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Write(BinaryWriter)**

```csharp
public void Write(BinaryWriter writer)
```

#### Parameters

`writer` [BinaryWriter](https://docs.microsoft.com/en-us/dotnet/api/system.io.binarywriter)<br>
