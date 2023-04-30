# FIoOffsetAndLength

Namespace: UAssetAPI.IO

```csharp
public struct FIoOffsetAndLength
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FIoOffsetAndLength](./uassetapi.io.fiooffsetandlength.md)

## Fields

### **Offset**

```csharp
public ulong Offset;
```

### **Length**

```csharp
public ulong Length;
```

## Methods

### **Read(IOStoreBinaryReader)**

```csharp
FIoOffsetAndLength Read(IOStoreBinaryReader reader)
```

#### Parameters

`reader` [IOStoreBinaryReader](./uassetapi.io.iostorebinaryreader.md)<br>

#### Returns

[FIoOffsetAndLength](./uassetapi.io.fiooffsetandlength.md)<br>

### **Write(IOStoreBinaryWriter, UInt64, UInt64)**

```csharp
int Write(IOStoreBinaryWriter writer, ulong v1, ulong v2)
```

#### Parameters

`writer` [IOStoreBinaryWriter](./uassetapi.io.iostorebinarywriter.md)<br>

`v1` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

`v2` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

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
