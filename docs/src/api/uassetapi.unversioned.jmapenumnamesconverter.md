# JmapEnumNamesConverter

Namespace: UAssetAPI.Unversioned

```csharp
public class JmapEnumNamesConverter : System.Text.Json.Serialization.JsonConverter`1[[System.Collections.Generic.Dictionary`2[[System.Int64],[System.String]]]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → JsonConverter → JsonConverter&lt;Dictionary&lt;Int64, String&gt;&gt; → [JmapEnumNamesConverter](./uassetapi.unversioned.jmapenumnamesconverter.md)

## Properties

### **HandleNull**

```csharp
public bool HandleNull { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Type**

```csharp
public Type Type { get; }
```

#### Property Value

[Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

## Constructors

### **JmapEnumNamesConverter()**

```csharp
public JmapEnumNamesConverter()
```

## Methods

### **Read(Utf8JsonReader&, Type, JsonSerializerOptions)**

```csharp
public Dictionary<long, string> Read(Utf8JsonReader& reader, Type typeToConvert, JsonSerializerOptions options)
```

#### Parameters

`reader` Utf8JsonReader&<br>

`typeToConvert` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

`options` JsonSerializerOptions<br>

#### Returns

[Dictionary&lt;Int64, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

### **Write(Utf8JsonWriter, Dictionary&lt;Int64, String&gt;, JsonSerializerOptions)**

```csharp
public void Write(Utf8JsonWriter writer, Dictionary<long, string> value, JsonSerializerOptions options)
```

#### Parameters

`writer` Utf8JsonWriter<br>

`value` [Dictionary&lt;Int64, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

`options` JsonSerializerOptions<br>
