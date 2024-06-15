# FObjectDataResource

Namespace: UAssetAPI.UnrealTypes

UObject binary/bulk data resource type.

```csharp
public struct FObjectDataResource
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FObjectDataResource](./uassetapi.unrealtypes.fobjectdataresource.md)

## Fields

### **Flags**

```csharp
public EObjectDataResourceFlags Flags;
```

### **SerialOffset**

```csharp
public long SerialOffset;
```

### **DuplicateSerialOffset**

```csharp
public long DuplicateSerialOffset;
```

### **SerialSize**

```csharp
public long SerialSize;
```

### **RawSize**

```csharp
public long RawSize;
```

### **OuterIndex**

```csharp
public FPackageIndex OuterIndex;
```

### **LegacyBulkDataFlags**

```csharp
public uint LegacyBulkDataFlags;
```

## Constructors

### **FObjectDataResource(EObjectDataResourceFlags, Int64, Int64, Int64, Int64, FPackageIndex, UInt32)**

```csharp
FObjectDataResource(EObjectDataResourceFlags Flags, long SerialOffset, long DuplicateSerialOffset, long SerialSize, long RawSize, FPackageIndex OuterIndex, uint LegacyBulkDataFlags)
```

#### Parameters

`Flags` [EObjectDataResourceFlags](./uassetapi.unrealtypes.eobjectdataresourceflags.md)<br>

`SerialOffset` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

`DuplicateSerialOffset` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

`SerialSize` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

`RawSize` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

`OuterIndex` [FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

`LegacyBulkDataFlags` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
