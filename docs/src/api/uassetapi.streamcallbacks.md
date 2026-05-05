# StreamCallbacks

Namespace: UAssetAPI

```csharp
public static class StreamCallbacks
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [StreamCallbacks](./uassetapi.streamcallbacks.md)

## Methods

### **Create(Stream)**

```csharp
public static StreamCallbacks Create(Stream stream)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

#### Returns

[StreamCallbacks](./uassetapi.repakinterop.streamcallbacks.md)<br>

### **Free(IntPtr)**

```csharp
public static void Free(IntPtr streamCtx)
```

#### Parameters

`streamCtx` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

### **ReadCallback(IntPtr, IntPtr, UInt64)**

```csharp
public static long ReadCallback(IntPtr context, IntPtr buffer, ulong bufferLen)
```

#### Parameters

`context` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`buffer` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`bufferLen` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

#### Returns

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **WriteCallback(IntPtr, IntPtr, Int32)**

```csharp
public static int WriteCallback(IntPtr context, IntPtr buffer, int bufferLen)
```

#### Parameters

`context` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`buffer` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`bufferLen` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **SeekCallback(IntPtr, Int64, Int32)**

```csharp
public static ulong SeekCallback(IntPtr context, long offset, int origin)
```

#### Parameters

`context` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`offset` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

`origin` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **FlushCallback(IntPtr)**

```csharp
public static int FlushCallback(IntPtr context)
```

#### Parameters

`context` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
