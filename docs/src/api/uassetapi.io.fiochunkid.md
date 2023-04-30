# FIoChunkId

Namespace: UAssetAPI.IO

Identifier to a chunk of data.

```csharp
public struct FIoChunkId
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FIoChunkId](./uassetapi.io.fiochunkid.md)

## Fields

### **ChunkId**

```csharp
public ulong ChunkId;
```

### **ChunkIndex**

```csharp
public ushort ChunkIndex;
```

### **ChunkType**

```csharp
public byte ChunkType;
```

## Properties

### **ChunkType4**

```csharp
public EIoChunkType4 ChunkType4 { get; }
```

#### Property Value

[EIoChunkType4](./uassetapi.io.eiochunktype4.md)<br>

### **ChunkType5**

```csharp
public EIoChunkType5 ChunkType5 { get; }
```

#### Property Value

[EIoChunkType5](./uassetapi.io.eiochunktype5.md)<br>

## Constructors

### **FIoChunkId(UInt64, UInt16, Byte)**

```csharp
FIoChunkId(ulong chunkId, ushort chunkIndex, byte chunkType)
```

#### Parameters

`chunkId` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

`chunkIndex` [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

`chunkType` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **FIoChunkId(UInt64, UInt16, EIoChunkType4)**

```csharp
FIoChunkId(ulong chunkId, ushort chunkIndex, EIoChunkType4 chunkType)
```

#### Parameters

`chunkId` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

`chunkIndex` [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

`chunkType` [EIoChunkType4](./uassetapi.io.eiochunktype4.md)<br>

### **FIoChunkId(UInt64, UInt16, EIoChunkType5)**

```csharp
FIoChunkId(ulong chunkId, ushort chunkIndex, EIoChunkType5 chunkType)
```

#### Parameters

`chunkId` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

`chunkIndex` [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

`chunkType` [EIoChunkType5](./uassetapi.io.eiochunktype5.md)<br>

## Methods

### **Read(IOStoreBinaryReader)**

```csharp
FIoChunkId Read(IOStoreBinaryReader reader)
```

#### Parameters

`reader` [IOStoreBinaryReader](./uassetapi.io.iostorebinaryreader.md)<br>

#### Returns

[FIoChunkId](./uassetapi.io.fiochunkid.md)<br>

### **Pack(UInt64, UInt16, Byte)**

```csharp
Byte[] Pack(ulong v1, ushort v2_, byte v3)
```

#### Parameters

`v1` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

`v2_` [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

`v3` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **Pack()**

```csharp
Byte[] Pack()
```

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **Write(IOStoreBinaryWriter, UInt64, UInt16, Byte)**

```csharp
int Write(IOStoreBinaryWriter writer, ulong v1, ushort v2, byte v3)
```

#### Parameters

`writer` [IOStoreBinaryWriter](./uassetapi.io.iostorebinarywriter.md)<br>

`v1` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

`v2` [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

`v3` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Write(IOStoreBinaryWriter)**

```csharp
int Write(IOStoreBinaryWriter writer)
```

#### Parameters

`writer` [IOStoreBinaryWriter](./uassetapi.io.iostorebinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
