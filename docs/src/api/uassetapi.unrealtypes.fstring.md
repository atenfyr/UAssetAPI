# FString

Namespace: UAssetAPI.UnrealTypes

Unreal string - consists of a string and an encoding

```csharp
public class FString : System.ICloneable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FString](./uassetapi.unrealtypes.fstring.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable)

## Fields

### **Value**

```csharp
public string Value;
```

### **Encoding**

```csharp
public Encoding Encoding;
```

### **IsCasePreserving**

Is this FString case preserving?

```csharp
public bool IsCasePreserving;
```

### **NullCase**

```csharp
public static string NullCase;
```

## Constructors

### **FString(String, Encoding)**

```csharp
public FString(string value, Encoding encoding)
```

#### Parameters

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`encoding` [Encoding](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding)<br>

### **FString()**

```csharp
public FString()
```

## Methods

### **ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Equals(Object)**

```csharp
public bool Equals(object obj)
```

#### Parameters

`obj` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **GetHashCode()**

```csharp
public int GetHashCode()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Clone()**

```csharp
public object Clone()
```

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **FromString(String, Encoding)**

```csharp
public static FString FromString(string value, Encoding encoding)
```

#### Parameters

`value` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`encoding` [Encoding](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding)<br>

#### Returns

[FString](./uassetapi.unrealtypes.fstring.md)<br>
