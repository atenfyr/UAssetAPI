# UsmapSchema

Namespace: UAssetAPI.Unversioned

```csharp
public class UsmapSchema
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UsmapSchema](./uassetapi.unversioned.usmapschema.md)

## Fields

### **FromAsset**

```csharp
public bool FromAsset;
```

## Properties

### **Name**

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **SuperType**

```csharp
public string SuperType { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **SuperTypeModulePath**

```csharp
public string SuperTypeModulePath { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **PropCount**

```csharp
public int PropCount { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ModulePath**

```csharp
public string ModulePath { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **StructKind**

```csharp
public UsmapStructKind StructKind { get; set; }
```

#### Property Value

[UsmapStructKind](./uassetapi.unversioned.usmapstructkind.md)<br>

### **StructOrClassFlags**

```csharp
public int StructOrClassFlags { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Properties**

```csharp
public IReadOnlyDictionary<int, UsmapProperty> Properties { get; }
```

#### Property Value

[IReadOnlyDictionary&lt;Int32, UsmapProperty&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

## Constructors

### **UsmapSchema(String, String, Int32, ConcurrentDictionary&lt;Int32, UsmapProperty&gt;, Boolean, String, Boolean)**

```csharp
public UsmapSchema(string name, string superType, int propCount, ConcurrentDictionary<int, UsmapProperty> props, bool isCaseInsensitive, string superTypeModulePath, bool fromAsset)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`superType` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`propCount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`props` ConcurrentDictionary&lt;Int32, UsmapProperty&gt;<br>

`isCaseInsensitive` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`superTypeModulePath` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`fromAsset` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **UsmapSchema()**

```csharp
public UsmapSchema()
```

## Methods

### **GetProperty(String, Int32)**

```csharp
public UsmapProperty GetProperty(string key, int dupIndex)
```

#### Parameters

`key` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`dupIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

#### Returns

[UsmapProperty](./uassetapi.unversioned.usmapproperty.md)<br>

### **ConstructPropertiesMap(Boolean)**

```csharp
public void ConstructPropertiesMap(bool isCaseInsensitive)
```

#### Parameters

`isCaseInsensitive` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
