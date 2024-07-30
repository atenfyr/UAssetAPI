# PakReader

Namespace: UAssetAPI

```csharp
public class PakReader : Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid, System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CriticalFinalizerObject](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.constrainedexecution.criticalfinalizerobject) → [SafeHandle](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.safehandle) → [SafeHandleZeroOrMinusOneIsInvalid](https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.safehandles.safehandlezeroorminusoneisinvalid) → [PakReader](./uassetapi.pakreader.md)<br>
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

### **PakReader(IntPtr, Stream)**

```csharp
public PakReader(IntPtr handle, Stream stream)
```

#### Parameters

`handle` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

## Methods

### **ReleaseHandle()**

```csharp
protected bool ReleaseHandle()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **GetMountPoint()**

```csharp
public string GetMountPoint()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **GetVersion()**

```csharp
public PakVersion GetVersion()
```

#### Returns

[PakVersion](./uassetapi.pakversion.md)<br>

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
