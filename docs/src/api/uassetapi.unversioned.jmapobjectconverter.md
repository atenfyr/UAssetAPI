# JmapObjectConverter

Namespace: UAssetAPI.Unversioned

```csharp
public class JmapObjectConverter : System.Text.Json.Serialization.JsonConverter`1[[UAssetAPI.Unversioned.JmapObjectBase]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → JsonConverter → JsonConverter&lt;JmapObjectBase&gt; → [JmapObjectConverter](./uassetapi.unversioned.jmapobjectconverter.md)

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

### **JmapObjectConverter()**

```csharp
public JmapObjectConverter()
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
public JmapObjectBase Read(Utf8JsonReader& reader, Type typeToConvert, JsonSerializerOptions options)
```

#### Parameters

`reader` Utf8JsonReader&<br>

`typeToConvert` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

`options` JsonSerializerOptions<br>

#### Returns

[JmapObjectBase](./uassetapi.unversioned.jmapobjectbase.md)<br>

### **Write(Utf8JsonWriter, JmapObjectBase, JsonSerializerOptions)**

```csharp
public void Write(Utf8JsonWriter writer, JmapObjectBase value, JsonSerializerOptions options)
```

#### Parameters

`writer` Utf8JsonWriter<br>

`value` [JmapObjectBase](./uassetapi.unversioned.jmapobjectbase.md)<br>

`options` JsonSerializerOptions<br>
