# JmapPropertyConverter

Namespace: UAssetAPI.Unversioned

```csharp
public class JmapPropertyConverter : System.Text.Json.Serialization.JsonConverter`1[[UAssetAPI.Unversioned.JmapPropertyBase]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → JsonConverter → JsonConverter&lt;JmapPropertyBase&gt; → [JmapPropertyConverter](./uassetapi.unversioned.jmappropertyconverter.md)

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

### **JmapPropertyConverter()**

```csharp
public JmapPropertyConverter()
```

## Methods

### **CanConvert(Type)**

```csharp
public bool CanConvert(Type typeToConvert)
```

#### Parameters

`typeToConvert` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Read(Utf8JsonReader&, Type, JsonSerializerOptions)**

```csharp
public JmapPropertyBase Read(Utf8JsonReader& reader, Type typeToConvert, JsonSerializerOptions options)
```

#### Parameters

`reader` Utf8JsonReader&<br>

`typeToConvert` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

`options` JsonSerializerOptions<br>

#### Returns

[JmapPropertyBase](./uassetapi.unversioned.jmappropertybase.md)<br>

### **Write(Utf8JsonWriter, JmapPropertyBase, JsonSerializerOptions)**

```csharp
public void Write(Utf8JsonWriter writer, JmapPropertyBase value, JsonSerializerOptions options)
```

#### Parameters

`writer` Utf8JsonWriter<br>

`value` [JmapPropertyBase](./uassetapi.unversioned.jmappropertybase.md)<br>

`options` JsonSerializerOptions<br>
