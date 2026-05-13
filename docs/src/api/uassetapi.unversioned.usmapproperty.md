# UsmapProperty

Namespace: UAssetAPI.Unversioned

```csharp
public class UsmapProperty : System.ICloneable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UsmapProperty](./uassetapi.unversioned.usmapproperty.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable)

## Fields

### **Name**

```csharp
public string Name;
```

### **SchemaIndex**

```csharp
public int SchemaIndex;
```

### **ArrayIndex**

```csharp
public int ArrayIndex;
```

### **ArraySize**

```csharp
public int ArraySize;
```

### **PropertyFlags**

```csharp
public EPropertyFlags PropertyFlags;
```

### **PropertyData**

```csharp
public UsmapPropertyData PropertyData;
```

## Constructors

### **UsmapProperty(String, Int32, Int32, Int32, UsmapPropertyData)**

```csharp
public UsmapProperty(string name, int schemaIndex, int arrayIndex, int arraySize, UsmapPropertyData propertyData)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`schemaIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`arrayIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`arraySize` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

`propertyData` [UsmapPropertyData](./uassetapi.unversioned.usmappropertydata.md)<br>

## Methods

### **Clone()**

```csharp
public object Clone()
```

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
