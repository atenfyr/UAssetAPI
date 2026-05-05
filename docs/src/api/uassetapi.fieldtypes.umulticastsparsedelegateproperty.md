# UMulticastSparseDelegateProperty

Namespace: UAssetAPI.FieldTypes

```csharp
public class UMulticastSparseDelegateProperty : UMulticastDelegateProperty
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [UField](./uassetapi.fieldtypes.ufield.md) → [UProperty](./uassetapi.fieldtypes.uproperty.md) → [UDelegateProperty](./uassetapi.fieldtypes.udelegateproperty.md) → [UMulticastDelegateProperty](./uassetapi.fieldtypes.umulticastdelegateproperty.md) → [UMulticastSparseDelegateProperty](./uassetapi.fieldtypes.umulticastsparsedelegateproperty.md)

## Fields

### **SignatureFunction**

```csharp
public FPackageIndex SignatureFunction;
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

### **UMulticastSparseDelegateProperty()**

```csharp
public UMulticastSparseDelegateProperty()
```
