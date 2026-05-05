# UsmapBinaryReader

Namespace: UAssetAPI

Reads primitive data types from .usmap files.

```csharp
public class UsmapBinaryReader : System.IO.BinaryReader, System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BinaryReader](https://docs.microsoft.com/en-us/dotnet/api/system.io.binaryreader) → [UsmapBinaryReader](./uassetapi.usmapbinaryreader.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Fields

### **File**

```csharp
public Usmap File;
```

## Properties

### **BaseStream**

```csharp
public Stream BaseStream { get; }
```

#### Property Value

[Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

## Constructors

### **UsmapBinaryReader(Stream, Usmap)**

```csharp
public UsmapBinaryReader(Stream stream, Usmap file)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`file` [Usmap](./uassetapi.unversioned.usmap.md)<br>

## Methods

### **ReadInt16()**

```csharp
public short ReadInt16()
```

#### Returns

[Int16](https://docs.microsoft.com/en-us/dotnet/api/system.int16)<br>

### **ReadUInt16()**

```csharp
public ushort ReadUInt16()
```

#### Returns

[UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

### **ReadInt32()**

```csharp
public int ReadInt32()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ReadUInt32()**

```csharp
public uint ReadUInt32()
```

#### Returns

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **ReadInt64()**

```csharp
public long ReadInt64()
```

#### Returns

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **ReadUInt64()**

```csharp
public ulong ReadUInt64()
```

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **ReadSingle()**

```csharp
public float ReadSingle()
```

#### Returns

[Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **ReadDouble()**

```csharp
public double ReadDouble()
```

#### Returns

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **ReadString()**

```csharp
public string ReadString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ReadString(Int32)**

```csharp
public string ReadString(int fixedLength)
```

#### Parameters

`fixedLength` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ReadName()**

```csharp
public string ReadName()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
