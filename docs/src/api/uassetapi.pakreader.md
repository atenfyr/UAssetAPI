# PakReader

Namespace: UAssetAPI

```csharp
public class PakReader : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PakReader](./uassetapi.pakreader.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Constructors

### **PakReader(IntPtr, Stream)**

```csharp
public PakReader(IntPtr handle, Stream stream)
```

#### Parameters

`handle` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

## Methods

### **Get(Stream, String)**

```csharp
public Byte[] Get(Stream stream, string path)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **Files()**

```csharp
public String[] Files()
```

#### Returns

[String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Dispose()**

```csharp
public void Dispose()
```
