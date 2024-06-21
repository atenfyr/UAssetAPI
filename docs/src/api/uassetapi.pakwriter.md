# PakWriter

Namespace: UAssetAPI

```csharp
public class PakWriter : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PakWriter](./uassetapi.pakwriter.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Constructors

### **PakWriter(IntPtr, Stream)**

```csharp
public PakWriter(IntPtr handle, Stream stream)
```

#### Parameters

`handle` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

## Methods

### **WriteFile(String, Byte[])**

```csharp
public void WriteFile(string path, Byte[] data)
```

#### Parameters

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`data` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **WriteIndex()**

```csharp
public void WriteIndex()
```

### **Dispose()**

```csharp
public void Dispose()
```
