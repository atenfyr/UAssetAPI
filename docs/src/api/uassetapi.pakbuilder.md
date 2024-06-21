# PakBuilder

Namespace: UAssetAPI

```csharp
public class PakBuilder
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PakBuilder](./uassetapi.pakbuilder.md)

## Constructors

### **PakBuilder()**

```csharp
public PakBuilder()
```

## Methods

### **Finalize()**

```csharp
protected void Finalize()
```

### **Writer(Stream, PakVersion, String, UInt64)**

```csharp
public PakWriter Writer(Stream stream, PakVersion version, string mountPoint, ulong pathHashSeed)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`version` [PakVersion](./uassetapi.pakversion.md)<br>

`mountPoint` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`pathHashSeed` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

#### Returns

[PakWriter](./uassetapi.pakwriter.md)<br>

### **Reader(Stream)**

```csharp
public PakReader Reader(Stream stream)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

#### Returns

[PakReader](./uassetapi.pakreader.md)<br>
