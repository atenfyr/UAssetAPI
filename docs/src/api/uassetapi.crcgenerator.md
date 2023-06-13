# CRCGenerator

Namespace: UAssetAPI

```csharp
public static class CRCGenerator
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CRCGenerator](./uassetapi.crcgenerator.md)

## Fields

### **CRCTable_DEPRECATED**

```csharp
public static UInt32[] CRCTable_DEPRECATED;
```

### **CRCTablesSB8**

```csharp
public static UInt32[,] CRCTablesSB8;
```

## Methods

### **GenerateImportHashFromObjectPath(FString)**

```csharp
public static ulong GenerateImportHashFromObjectPath(FString text)
```

#### Parameters

`text` [FString](./uassetapi.unrealtypes.fstring.md)<br>

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **GenerateImportHashFromObjectPath(String)**

```csharp
public static ulong GenerateImportHashFromObjectPath(string text)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **CityHash64WithLower(FString)**

```csharp
public static ulong CityHash64WithLower(FString text)
```

#### Parameters

`text` [FString](./uassetapi.unrealtypes.fstring.md)<br>

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **CityHash64WithLower(String, Encoding)**

```csharp
public static ulong CityHash64WithLower(string text, Encoding encoding)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`encoding` [Encoding](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding)<br>

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **CityHash64(FString)**

```csharp
public static ulong CityHash64(FString text)
```

#### Parameters

`text` [FString](./uassetapi.unrealtypes.fstring.md)<br>

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **CityHash64(String, Encoding)**

```csharp
public static ulong CityHash64(string text, Encoding encoding)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`encoding` [Encoding](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding)<br>

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **CityHash64(Byte[])**

```csharp
public static ulong CityHash64(Byte[] data)
```

#### Parameters

`data` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

#### Returns

[UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64)<br>

### **GenerateHash(FString, Boolean)**

```csharp
public static uint GenerateHash(FString text, bool disableCasePreservingHash)
```

#### Parameters

`text` [FString](./uassetapi.unrealtypes.fstring.md)<br>

`disableCasePreservingHash` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **GenerateHash(String, Boolean)**

```csharp
public static uint GenerateHash(string text, bool disableCasePreservingHash)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`disableCasePreservingHash` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **GenerateHash(String, Encoding, Boolean)**

```csharp
public static uint GenerateHash(string text, Encoding encoding, bool disableCasePreservingHash)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`encoding` [Encoding](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding)<br>

`disableCasePreservingHash` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **ToUpper(Char)**

```csharp
public static char ToUpper(char input)
```

#### Parameters

`input` [Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

#### Returns

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **ToUpper(String)**

```csharp
public static string ToUpper(string input)
```

#### Parameters

`input` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ToLower(Char)**

```csharp
public static char ToLower(char input)
```

#### Parameters

`input` [Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

#### Returns

[Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>

### **ToLower(String, Boolean)**

```csharp
public static string ToLower(string input, bool coalesceToSlash)
```

#### Parameters

`input` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`coalesceToSlash` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ToLower(FString, Boolean)**

```csharp
public static FString ToLower(FString input, bool coalesceToSlash)
```

#### Parameters

`input` [FString](./uassetapi.unrealtypes.fstring.md)<br>

`coalesceToSlash` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[FString](./uassetapi.unrealtypes.fstring.md)<br>

### **Strihash_DEPRECATED(String, Encoding)**

```csharp
public static uint Strihash_DEPRECATED(string text, Encoding encoding)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`encoding` [Encoding](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding)<br>

#### Returns

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **StrCrc32(String, UInt32)**

```csharp
public static uint StrCrc32(string text, uint CRC)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`CRC` [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

#### Returns

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>
