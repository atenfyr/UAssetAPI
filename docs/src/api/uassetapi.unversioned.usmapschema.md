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

## Properties

### **Properties**

```csharp
public IReadOnlyDictionary<int, UsmapProperty> Properties { get; }
```

#### Property Value

[IReadOnlyDictionary&lt;Int32, UsmapProperty&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlydictionary-2)<br>

## Constructors

### **UsmapSchema(String, String, UInt16, Dictionary&lt;Int32, UsmapProperty&gt;)**

```csharp
public UsmapSchema(string name, string superType, ushort propCount, Dictionary<int, UsmapProperty> props)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`superType` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`propCount` [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

`props` [Dictionary&lt;Int32, UsmapProperty&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2)<br>

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
