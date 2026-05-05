# UAssetContractResolver

Namespace: UAssetAPI.JSON

```csharp
public class UAssetContractResolver : Newtonsoft.Json.Serialization.DefaultContractResolver, Newtonsoft.Json.Serialization.IContractResolver
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → DefaultContractResolver → [UAssetContractResolver](./uassetapi.json.uassetcontractresolver.md)<br>
Implements IContractResolver

## Fields

### **ToBeFilled**

```csharp
public Dictionary<FName, string> ToBeFilled;
```

## Properties

### **DynamicCodeGeneration**

```csharp
public bool DynamicCodeGeneration { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **DefaultMembersSearchFlags**

#### Caution

DefaultMembersSearchFlags is obsolete. To modify the members serialized inherit from DefaultContractResolver and override the GetSerializableMembers method instead.

---

```csharp
public BindingFlags DefaultMembersSearchFlags { get; set; }
```

#### Property Value

[BindingFlags](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags)<br>

### **SerializeCompilerGeneratedMembers**

```csharp
public bool SerializeCompilerGeneratedMembers { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IgnoreSerializableInterface**

```csharp
public bool IgnoreSerializableInterface { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IgnoreSerializableAttribute**

```csharp
public bool IgnoreSerializableAttribute { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IgnoreIsSpecifiedMembers**

```csharp
public bool IgnoreIsSpecifiedMembers { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **IgnoreShouldSerializeMembers**

```csharp
public bool IgnoreShouldSerializeMembers { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **NamingStrategy**

```csharp
public NamingStrategy NamingStrategy { get; set; }
```

#### Property Value

NamingStrategy<br>

## Constructors

### **UAssetContractResolver(Dictionary&lt;FName, String&gt;)**

```csharp
public UAssetContractResolver(Dictionary<FName, string> toBeFilled)
```

#### Parameters

`toBeFilled` [Dictionary&lt;FName, String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

## Methods

### **ResolveContractConverter(Type)**

```csharp
protected JsonConverter ResolveContractConverter(Type objectType)
```

#### Parameters

`objectType` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>

#### Returns

JsonConverter<br>
