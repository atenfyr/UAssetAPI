# UsmapSchema

Namespace: UAssetAPI.Unversioned

```csharp
public class UsmapSchema
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UsmapSchema](./uassetapi.unversioned.usmapschema.md)

## Fields

### **Name**

```csharp
public string Name;
```

### **SuperType**

```csharp
public string SuperType;
```

### **PropCount**

```csharp
public ushort PropCount;
```

### **ModulePath**

```csharp
public string ModulePath;
```

### **FromAsset**

Whether or not this schema was retrieved from a .uasset file.

```csharp
public bool FromAsset;
```

### **StructKind**

```csharp
public UsmapStructKind StructKind;
```

### **StructOrClassFlags**

```csharp
public int StructOrClassFlags;
```

## Properties

### **Properties**

```csharp
public IReadOnlyDictionary<int, UsmapProperty> Properties { get; }
```

#### Property Value

[IReadOnlyDictionary&lt;Int32, UsmapProperty&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

## Constructors

### **UsmapSchema(String, String, UInt16, ConcurrentDictionary&lt;Int32, UsmapProperty&gt;, Boolean, Boolean)**

```csharp
public UsmapSchema(string name, string superType, ushort propCount, ConcurrentDictionary<int, UsmapProperty> props, bool isCaseInsensitive, bool fromAsset)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`superType` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`propCount` [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

`props` ConcurrentDictionary&lt;Int32, UsmapProperty&gt;<br>

`isCaseInsensitive` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

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
