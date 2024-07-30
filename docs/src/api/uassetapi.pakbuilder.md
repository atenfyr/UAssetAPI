# PakBuilder

Namespace: UAssetAPI

```csharp
public class PakBuilder : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid, System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CriticalFinalizerObject](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.constrainedexecution.criticalfinalizerobject) → [SafeHandle](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.safehandle) → [SafeHandleZeroOrMinusOneIsInvalid](https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.safehandles.safehandlezeroorminusoneisinvalid) → [PakBuilder](./uassetapi.pakbuilder.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Properties

### **IsInvalid**

```csharp
public bool IsInvalid { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IsClosed**

```csharp
public bool IsClosed { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **PakBuilder()**

```csharp
public PakBuilder()
```

## Methods

### **ReleaseHandle()**

```csharp
protected bool ReleaseHandle()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Key(Byte[])**

```csharp
public PakBuilder Key(Byte[] key)
```

#### Parameters

`key` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

#### Returns

[PakBuilder](./uassetapi.pakbuilder.md)<br>

### **Compression(PakCompression[])**

```csharp
public PakBuilder Compression(PakCompression[] compressions)
```

#### Parameters

`compressions` [PakCompression[]](./uassetapi.pakcompression.md)<br>

#### Returns

[PakBuilder](./uassetapi.pakbuilder.md)<br>

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
