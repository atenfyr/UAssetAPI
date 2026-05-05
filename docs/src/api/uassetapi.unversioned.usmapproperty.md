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
public ushort SchemaIndex;
```

### **ArrayIndex**

```csharp
public ushort ArrayIndex;
```

### **ArraySize**

```csharp
public byte ArraySize;
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

### **UsmapProperty(String, UInt16, UInt16, Byte, UsmapPropertyData)**

```csharp
public UsmapProperty(string name, ushort schemaIndex, ushort arrayIndex, byte arraySize, UsmapPropertyData propertyData)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`schemaIndex` [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

`arrayIndex` [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16)<br>

`arraySize` [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

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
