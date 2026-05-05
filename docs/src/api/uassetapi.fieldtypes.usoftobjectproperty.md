# USoftObjectProperty

Namespace: UAssetAPI.FieldTypes

```csharp
public class USoftObjectProperty : UObjectProperty
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UField](./uassetapi.fieldtypes.ufield.md) → [UProperty](./uassetapi.fieldtypes.uproperty.md) → [UObjectProperty](./uassetapi.fieldtypes.uobjectproperty.md) → [USoftObjectProperty](./uassetapi.fieldtypes.usoftobjectproperty.md)

## Fields

### **PropertyClass**

```csharp
public FPackageIndex PropertyClass;
```

### **ArrayDim**

```csharp
public EArrayDim ArrayDim;
```

### **ElementSize**

```csharp
public int ElementSize;
```

### **PropertyFlags**

```csharp
public EPropertyFlags PropertyFlags;
```

### **RepNotifyFunc**

```csharp
public FName RepNotifyFunc;
```

### **BlueprintReplicationCondition**

```csharp
public ELifetimeCondition BlueprintReplicationCondition;
```

### **RawValue**

```csharp
public object RawValue;
```

### **Next**

Next Field in the linked list. Removed entirely in the custom version FFrameworkObjectVersion::RemoveUField_Next in favor of a regular array

```csharp
public FPackageIndex Next;
```

## Constructors

### **USoftObjectProperty()**

```csharp
public USoftObjectProperty()
```
