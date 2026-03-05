# FPropertyTypeNameConverter

Namespace: UAssetAPI.UnrealTypes

```csharp
public class FPropertyTypeNameConverter : Newtonsoft.Json.JsonConverter`1[[UAssetAPI.UnrealTypes.FPropertyTypeName]]
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → JsonConverter → JsonConverter&lt;FPropertyTypeName&gt; → [FPropertyTypeNameConverter](./uassetapi.unrealtypes.fpropertytypenameconverter.md)

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

### **FPropertyTypeNameConverter()**

```csharp
public FPropertyTypeNameConverter()
```

## Methods

### **WriteJson(JsonWriter, FPropertyTypeName, JsonSerializer)**

```csharp
public void WriteJson(JsonWriter writer, FPropertyTypeName value, JsonSerializer serializer)
```

#### Parameters

`writer` JsonWriter<br>

`value` [FPropertyTypeName](./uassetapi.unrealtypes.fpropertytypename.md)<br>

`serializer` JsonSerializer<br>

### **ReadJson(JsonReader, Type, FPropertyTypeName, Boolean, JsonSerializer)**

```csharp
public FPropertyTypeName ReadJson(JsonReader reader, Type objectType, FPropertyTypeName existingValue, bool hasExistingValue, JsonSerializer serializer)
```

#### Parameters

`reader` JsonReader<br>

`objectType` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

`existingValue` [FPropertyTypeName](./uassetapi.unrealtypes.fpropertytypename.md)<br>

`hasExistingValue` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`serializer` JsonSerializer<br>

#### Returns

[FPropertyTypeName](./uassetapi.unrealtypes.fpropertytypename.md)<br>
