# KismetPropertyPointer

Namespace: UAssetAPI.Kismet.Bytecode

Represents a Kismet bytecode pointer to an FProperty or FField.

```csharp
public class KismetPropertyPointer
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [KismetPropertyPointer](./uassetapi.kismet.bytecode.kismetpropertypointer.md)

## Fields

### **Old**

The pointer serialized as an FPackageIndex. Used in versions older than [FReleaseObjectVersion.FFieldPathOwnerSerialization](./uassetapi.customversions.freleaseobjectversion.md#ffieldpathownerserialization).

```csharp
public FPackageIndex Old;
```

### **New**

The pointer serialized as an FFieldPath. Used in versions newer than [FReleaseObjectVersion.FFieldPathOwnerSerialization](./uassetapi.customversions.freleaseobjectversion.md#ffieldpathownerserialization).

```csharp
public FFieldPath New;
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
