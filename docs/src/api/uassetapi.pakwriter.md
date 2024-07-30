# PakWriter

Namespace: UAssetAPI

```csharp
public class PakWriter : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid, System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CriticalFinalizerObject](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.constrainedexecution.criticalfinalizerobject) → [SafeHandle](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.safehandle) → [SafeHandleZeroOrMinusOneIsInvalid](https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.safehandles.safehandlezeroorminusoneisinvalid) → [PakWriter](./uassetapi.pakwriter.md)<br>
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

### **PakWriter(IntPtr, IntPtr)**

```csharp
public PakWriter(IntPtr handle, IntPtr streamCtx)
```

#### Parameters

`handle` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`streamCtx` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

## Methods

### **ReleaseHandle()**

```csharp
protected bool ReleaseHandle()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

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
