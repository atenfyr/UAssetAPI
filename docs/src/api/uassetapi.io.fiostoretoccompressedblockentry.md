# FIoStoreTocCompressedBlockEntry

Namespace: UAssetAPI.IO

Compression block entry.

```csharp
public struct FIoStoreTocCompressedBlockEntry
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FIoStoreTocCompressedBlockEntry](./uassetapi.io.fiostoretoccompressedblockentry.md)

## Fields

### **Offset**

```csharp
public ulong Offset;
```

### **CompressedSize**

```csharp
public uint CompressedSize;
```

### **UncompressedSize**

```csharp
public uint UncompressedSize;
```

### **CompressionMethodIndex**

```csharp
public byte CompressionMethodIndex;
```

### **OffsetBits**

```csharp
public static int OffsetBits;
```

### **OffsetMask**

```csharp
public static ulong OffsetMask;
```

### **SizeBits**

```csharp
public static int SizeBits;
```

### **SizeMask**

```csharp
public static uint SizeMask;
```

### **SizeShift**

```csharp
public static int SizeShift;
```

## Methods

### **Read(IOStoreBinaryReader)**

```csharp
FIoStoreTocCompressedBlockEntry Read(IOStoreBinaryReader reader)
```

#### Parameters

`reader` [IOStoreBinaryReader](./uassetapi.io.iostorebinaryreader.md)<br>

#### Returns

[FIoStoreTocCompressedBlockEntry](./uassetapi.io.fiostoretoccompressedblockentry.md)<br>

### **Write(IOStoreBinaryWriter, UInt64, UInt32, UInt32, Byte)**

```csharp
int Write(IOStoreBinaryWriter writer, ulong v1, uint v2, uint v3, byte v4)
```

#### Parameters

`writer` [IOStoreBinaryWriter](./uassetapi.io.iostorebinarywriter.md)<br>

`v1` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

`v2` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`v3` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

`v4` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

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
