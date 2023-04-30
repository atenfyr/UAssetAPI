# IOStoreBinaryWriter

Namespace: UAssetAPI.IO

```csharp
public class IOStoreBinaryWriter : System.IO.BinaryWriter, System.IDisposable, System.IAsyncDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BinaryWriter](https://docs.microsoft.com/en-us/dotnet/api/system.io.binarywriter) → [IOStoreBinaryWriter](./uassetapi.io.iostorebinarywriter.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable), [IAsyncDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncdisposable)

## Fields

### **Asset**

```csharp
public IOStoreContainer Asset;
```

## Properties

### **BaseStream**

```csharp
public Stream BaseStream { get; }
```

#### Property Value

[Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

## Constructors

### **IOStoreBinaryWriter(IOStoreContainer)**

```csharp
public IOStoreBinaryWriter(IOStoreContainer asset)
```

#### Parameters

`asset` [IOStoreContainer](./uassetapi.io.iostorecontainer.md)<br>

### **IOStoreBinaryWriter(Stream, IOStoreContainer)**

```csharp
public IOStoreBinaryWriter(Stream stream, IOStoreContainer asset)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`asset` [IOStoreContainer](./uassetapi.io.iostorecontainer.md)<br>

### **IOStoreBinaryWriter(Stream, Encoding, IOStoreContainer)**

```csharp
public IOStoreBinaryWriter(Stream stream, Encoding encoding, IOStoreContainer asset)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`encoding` [Encoding](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding)<br>

`asset` [IOStoreContainer](./uassetapi.io.iostorecontainer.md)<br>

### **IOStoreBinaryWriter(Stream, Encoding, Boolean, IOStoreContainer)**

```csharp
public IOStoreBinaryWriter(Stream stream, Encoding encoding, bool leaveOpen, IOStoreContainer asset)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`encoding` [Encoding](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding)<br>

`leaveOpen` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`asset` [IOStoreContainer](./uassetapi.io.iostorecontainer.md)<br>

## Methods

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
