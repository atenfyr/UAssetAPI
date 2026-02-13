# UsmapSchemaPropertiesJsonConverter

Namespace: UAssetAPI.Unversioned

```csharp
public class UsmapSchemaPropertiesJsonConverter : Newtonsoft.Json.JsonConverter`1[[System.Collections.Generic.IReadOnlyDictionary`2[[System.Int32],[UAssetAPI.Unversioned.UsmapProperty]]]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → JsonConverter → JsonConverter&lt;IReadOnlyDictionary&lt;Int32, UsmapProperty&gt;&gt; → [UsmapSchemaPropertiesJsonConverter](./uassetapi.unversioned.usmapschemapropertiesjsonconverter.md)

## Properties

### **CanRead**

```csharp
public bool CanRead { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **CanWrite**

```csharp
public bool CanWrite { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **UsmapSchemaPropertiesJsonConverter()**

```csharp
public UsmapSchemaPropertiesJsonConverter()
```

## Methods

### **ReadJson(JsonReader, Type, IReadOnlyDictionary&lt;Int32, UsmapProperty&gt;, Boolean, JsonSerializer)**

```csharp
public IReadOnlyDictionary<int, UsmapProperty> ReadJson(JsonReader reader, Type objectType, IReadOnlyDictionary<int, UsmapProperty> existingValue, bool hasExistingValue, JsonSerializer serializer)
```

#### Parameters

`reader` JsonReader<br>

`objectType` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

`existingValue` [IReadOnlyDictionary&lt;Int32, UsmapProperty&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

`hasExistingValue` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`serializer` JsonSerializer<br>

#### Returns

[IReadOnlyDictionary&lt;Int32, UsmapProperty&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

### **WriteJson(JsonWriter, IReadOnlyDictionary&lt;Int32, UsmapProperty&gt;, JsonSerializer)**

```csharp
public void WriteJson(JsonWriter writer, IReadOnlyDictionary<int, UsmapProperty> value, JsonSerializer serializer)
```

#### Parameters

`writer` JsonWriter<br>

`value` [IReadOnlyDictionary&lt;Int32, UsmapProperty&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

`serializer` JsonSerializer<br>
