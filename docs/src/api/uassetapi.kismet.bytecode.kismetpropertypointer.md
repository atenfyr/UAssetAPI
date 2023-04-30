# KismetPropertyPointer

Namespace: UAssetAPI.Kismet.Bytecode

Represents a Kismet bytecode pointer to an FProperty or FField.

```csharp
public class KismetPropertyPointer
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [KismetPropertyPointer](./uassetapi.kismet.bytecode.kismetpropertypointer.md)

## Fields

### **Old**

The pointer serialized as an FPackageIndex. Used in versions older than [KismetPropertyPointer.XFER_PROP_POINTER_SWITCH_TO_SERIALIZING_AS_FIELD_PATH_VERSION](./uassetapi.kismet.bytecode.kismetpropertypointer.md#xfer_prop_pointer_switch_to_serializing_as_field_path_version).

```csharp
public FPackageIndex Old;
```

### **New**

The pointer serialized as an FFieldPath. Used in versions newer than [KismetPropertyPointer.XFER_PROP_POINTER_SWITCH_TO_SERIALIZING_AS_FIELD_PATH_VERSION](./uassetapi.kismet.bytecode.kismetpropertypointer.md#xfer_prop_pointer_switch_to_serializing_as_field_path_version).

```csharp
public FFieldPath New;
```

### **XFER_PROP_POINTER_SWITCH_TO_SERIALIZING_AS_FIELD_PATH_VERSION**

```csharp
public static ObjectVersion XFER_PROP_POINTER_SWITCH_TO_SERIALIZING_AS_FIELD_PATH_VERSION;
```

## Constructors

### **KismetPropertyPointer(FPackageIndex)**

```csharp
public KismetPropertyPointer(FPackageIndex older)
```

#### Parameters

`older` [FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

### **KismetPropertyPointer(FFieldPath)**

```csharp
public KismetPropertyPointer(FFieldPath newer)
```

#### Parameters

`newer` [FFieldPath](./uassetapi.unrealtypes.ffieldpath.md)<br>

### **KismetPropertyPointer()**

```csharp
public KismetPropertyPointer()
```

## Methods

### **ShouldSerializeOld()**

```csharp
public bool ShouldSerializeOld()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ShouldSerializeNew()**

```csharp
public bool ShouldSerializeNew()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
