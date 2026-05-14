# CRCGenerator

Namespace: UAssetAPI

Helper class for generating CRC hashes for use in uasset name maps.

```csharp
public static class CRCGenerator
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [CRCGenerator](./uassetapi.crcgenerator.md)

## Methods

### **GenerateHash(FString, Boolean, Boolean)**

```csharp
public static uint GenerateHash(FString text, bool disableCasePreservingHash, bool version420)
```

#### Parameters

`text` [FString](./uassetapi.unrealtypes.fstring.md)<br>

`disableCasePreservingHash` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`version420` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **GenerateHash(String, Boolean, Boolean)**

```csharp
public static uint GenerateHash(string text, bool disableCasePreservingHash, bool version420)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`disableCasePreservingHash` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`version420` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32)<br>

### **GenerateHash(String, Encoding, Boolean, Boolean)**

```csharp
public static uint GenerateHash(string text, Encoding encoding, bool disableCasePreservingHash, bool version420)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`encoding` [Encoding](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding)<br>

`disableCasePreservingHash` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`version420` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

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

### **ToUpperVersion420(Char)**

```csharp
public static char ToUpperVersion420(char input)
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
