# RePakInterop

Namespace: UAssetAPI

```csharp
public static class RePakInterop
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [RePakInterop](./uassetapi.repakinterop.md)

## Fields

### **NativeLib**

```csharp
public static string NativeLib;
```

## Methods

### **pak_setup_allocator()**

```csharp
public static IntPtr pak_setup_allocator()
```

#### Returns

[IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

### **pak_teardown_allocator()**

```csharp
public static IntPtr pak_teardown_allocator()
```

#### Returns

[IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

### **pak_builder_new()**

```csharp
public static IntPtr pak_builder_new()
```

#### Returns

[IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

### **pak_builder_drop(IntPtr)**

```csharp
public static void pak_builder_drop(IntPtr builder)
```

#### Parameters

`builder` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

### **pak_reader_drop(IntPtr)**

```csharp
public static void pak_reader_drop(IntPtr reader)
```

#### Parameters

`reader` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

### **pak_writer_drop(IntPtr)**

```csharp
public static void pak_writer_drop(IntPtr writer)
```

#### Parameters

`writer` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

### **pak_buffer_drop(IntPtr, UInt64)**

```csharp
public static void pak_buffer_drop(IntPtr buffer, ulong length)
```

#### Parameters

`buffer` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`length` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **pak_cstring_drop(IntPtr)**

```csharp
public static void pak_cstring_drop(IntPtr cstrign)
```

#### Parameters

`cstrign` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

### **pak_builder_key(IntPtr, Byte[])**

```csharp
public static IntPtr pak_builder_key(IntPtr builder, Byte[] key)
```

#### Parameters

`builder` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`key` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

#### Returns

[IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

### **pak_builder_compression(IntPtr, Byte[], Int32)**

```csharp
public static IntPtr pak_builder_compression(IntPtr builder, Byte[] compressions, int length)
```

#### Parameters

`builder` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`compressions` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

`length` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

### **pak_builder_reader(IntPtr, StreamCallbacks)**

```csharp
public static IntPtr pak_builder_reader(IntPtr builder, StreamCallbacks ctx)
```

#### Parameters

`builder` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`ctx` [StreamCallbacks](./uassetapi.repakinterop.streamcallbacks.md)<br>

#### Returns

[IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

### **pak_builder_writer(IntPtr, StreamCallbacks, PakVersion, String, UInt64)**

```csharp
public static IntPtr pak_builder_writer(IntPtr builder, StreamCallbacks ctx, PakVersion version, string mount_point, ulong path_hash_seed)
```

#### Parameters

`builder` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`ctx` [StreamCallbacks](./uassetapi.repakinterop.streamcallbacks.md)<br>

`version` [PakVersion](./uassetapi.pakversion.md)<br>

`mount_point` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`path_hash_seed` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

#### Returns

[IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

### **pak_reader_version(IntPtr)**

```csharp
public static PakVersion pak_reader_version(IntPtr reader)
```

#### Parameters

`reader` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

#### Returns

[PakVersion](./uassetapi.pakversion.md)<br>

### **pak_reader_mount_point(IntPtr)**

```csharp
public static IntPtr pak_reader_mount_point(IntPtr reader)
```

#### Parameters

`reader` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

#### Returns

[IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

### **pak_reader_get(IntPtr, String, StreamCallbacks, IntPtr&, UInt64&)**

```csharp
public static int pak_reader_get(IntPtr reader, string path, StreamCallbacks ctx, IntPtr& buffer, UInt64& length)
```

#### Parameters

`reader` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`ctx` [StreamCallbacks](./uassetapi.repakinterop.streamcallbacks.md)<br>

`buffer` [IntPtr&](https://docs.microsoft.com/en-us/dotnet/api/system.intptr&)<br>

`length` [UInt64&](https://docs.microsoft.com/en-us/dotnet/api/system.uint64&)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **pak_reader_files(IntPtr, UInt64&)**

```csharp
public static IntPtr pak_reader_files(IntPtr reader, UInt64& length)
```

#### Parameters

`reader` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`length` [UInt64&](https://docs.microsoft.com/en-us/dotnet/api/system.uint64&)<br>

#### Returns

[IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

### **pak_drop_files(IntPtr, UInt64)**

```csharp
public static IntPtr pak_drop_files(IntPtr buffer, ulong length)
```

#### Parameters

`buffer` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`length` [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

#### Returns

[IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

### **pak_writer_write_file(IntPtr, String, Byte[], Int32)**

```csharp
public static int pak_writer_write_file(IntPtr writer, string path, Byte[] data, int data_len)
```

#### Parameters

`writer` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

`path` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`data` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

`data_len` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **pak_writer_write_index(IntPtr)**

```csharp
public static int pak_writer_write_index(IntPtr writer)
```

#### Parameters

`writer` [IntPtr](https://docs.microsoft.com/en-us/dotnet/api/system.intptr)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
