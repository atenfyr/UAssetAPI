# UnrealBinaryWriter

Namespace: UAssetAPI

Any binary writer used in the parsing of Unreal file types.

```csharp
public class UnrealBinaryWriter : System.IO.BinaryWriter, System.IDisposable, System.IAsyncDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BinaryWriter](https://docs.microsoft.com/en-us/dotnet/api/system.io.binarywriter) → [UnrealBinaryWriter](./uassetapi.unrealbinarywriter.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable), [IAsyncDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncdisposable)

## Properties

### **BaseStream**

```csharp
public Stream BaseStream { get; }
```

#### Property Value

[Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

## Constructors

### **UnrealBinaryWriter()**

```csharp
public UnrealBinaryWriter()
```

### **UnrealBinaryWriter(Stream)**

```csharp
public UnrealBinaryWriter(Stream stream)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

### **UnrealBinaryWriter(Stream, Encoding)**

```csharp
public UnrealBinaryWriter(Stream stream, Encoding encoding)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`encoding` [Encoding](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding)<br>

### **UnrealBinaryWriter(Stream, Encoding, Boolean)**

```csharp
public UnrealBinaryWriter(Stream stream, Encoding encoding, bool leaveOpen)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`encoding` [Encoding](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding)<br>

`leaveOpen` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Methods

### **ReverseIfBigEndian(Byte[])**

```csharp
protected Byte[] ReverseIfBigEndian(Byte[] data)
```

#### Parameters

`data` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **Write(Int16)**

```csharp
public void Write(short value)
```

#### Parameters

`value` [Int16](https://docs.microsoft.com/en-us/dotnet/api/system.int16)<br>

### **Write(UInt16)**

```csharp
public void Write(ushort value)
```

#### Parameters

`value` [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

### **Write(Int32)**

```csharp
public void Write(int value)
```

#### Parameters

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Write(UInt32)**

```csharp
public void Write(uint value)
```

#### Parameters

`value` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **Write(Int64)**

```csharp
public void Write(long value)
```

#### Parameters

`value` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **Write(UInt64)**

```csharp
public void Write(ulong value)
```

#### Parameters

`value` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **Write(Single)**

```csharp
public void Write(float value)
```

#### Parameters

`value` [Single](https://docs.microsoft.com/en-us/dotnet/api/system.single)<br>

### **Write(Double)**

```csharp
public void Write(double value)
```

#### Parameters

`value` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **Write(String)**

```csharp
public void Write(string value)
```

#### Parameters

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Write(FString)**

```csharp
public int Write(FString value)
```

#### Parameters

`value` [FString](./uassetapi.unrealtypes.fstring.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **WriteCustomVersionContainer(ECustomVersionSerializationFormat, List&lt;CustomVersion&gt;)**

```csharp
public void WriteCustomVersionContainer(ECustomVersionSerializationFormat format, List<CustomVersion> CustomVersionContainer)
```

#### Parameters

`format` [ECustomVersionSerializationFormat](./uassetapi.unversioned.ecustomversionserializationformat.md)<br>

`CustomVersionContainer` [List&lt;CustomVersion&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
