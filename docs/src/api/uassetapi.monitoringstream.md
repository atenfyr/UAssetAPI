# MonitoringStream

Namespace: UAssetAPI

Pass-through stream for debugging.

```csharp
public class MonitoringStream : System.IO.Stream, System.IDisposable, System.IAsyncDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MarshalByRefObject](https://docs.microsoft.com/en-us/dotnet/api/system.marshalbyrefobject) → [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream) → [MonitoringStream](./uassetapi.monitoringstream.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable), [IAsyncDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncdisposable)

## Fields

### **InnerStream**

```csharp
public Stream InnerStream;
```

### **Asset**

```csharp
public UAsset Asset;
```

### **Enabled**

Whether or not to enable monitoring.

```csharp
public static bool Enabled;
```

### **StopOffset**

Offset of a byte to place a breakpoint at for debugging purposes. Set to -1 to disable.

```csharp
public static long StopOffset;
```

### **IsUexpOffset**

If true, StopOffset is interpreted as an offset relative to the start of the .uexp file.

```csharp
public static bool IsUexpOffset;
```

## Properties

### **Position**

```csharp
public long Position { get; set; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **Length**

```csharp
public long Length { get; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **CanRead**

```csharp
public bool CanRead { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **CanSeek**

```csharp
public bool CanSeek { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **CanWrite**

```csharp
public bool CanWrite { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **CanTimeout**

```csharp
public bool CanTimeout { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ReadTimeout**

```csharp
public int ReadTimeout { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **WriteTimeout**

```csharp
public int WriteTimeout { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **MonitoringStream(Stream, UAsset)**

```csharp
public MonitoringStream(Stream innerStream, UAsset asset)
```

#### Parameters

`innerStream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

## Methods

### **Read(Byte[], Int32, Int32)**

```csharp
public int Read(Byte[] buffer, int offset, int count)
```

#### Parameters

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

`offset` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`count` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Write(Byte[], Int32, Int32)**

```csharp
public void Write(Byte[] buffer, int offset, int count)
```

#### Parameters

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

`offset` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`count` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Flush()**

```csharp
public void Flush()
```

### **Seek(Int64, SeekOrigin)**

```csharp
public long Seek(long offset, SeekOrigin origin)
```

#### Parameters

`offset` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

`origin` [SeekOrigin](https://docs.microsoft.com/en-us/dotnet/api/system.io.seekorigin)<br>

#### Returns

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **SetLength(Int64)**

```csharp
public void SetLength(long value)
```

#### Parameters

`value` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **Dispose(Boolean)**

```csharp
protected void Dispose(bool disposing)
```

#### Parameters

`disposing` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ReadAsync(Byte[], Int32, Int32, CancellationToken)**

```csharp
public Task<int> ReadAsync(Byte[] buffer, int offset, int count, CancellationToken cancellationToken)
```

#### Parameters

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

`offset` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`count` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`cancellationToken` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task&lt;Int32&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **WriteAsync(Byte[], Int32, Int32, CancellationToken)**

```csharp
public Task WriteAsync(Byte[] buffer, int offset, int count, CancellationToken cancellationToken)
```

#### Parameters

`buffer` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

`offset` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`count` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`cancellationToken` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>

### **FlushAsync(CancellationToken)**

```csharp
public Task FlushAsync(CancellationToken cancellationToken)
```

#### Parameters

`cancellationToken` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
