# FDelegate

Namespace: UAssetAPI.PropertyTypes.Objects

Describes a function bound to an Object.

```csharp
public class FDelegate
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FDelegate](./uassetapi.propertytypes.objects.fdelegate.md)

## Fields

### **Object**

References the main actor export

```csharp
public FPackageIndex Object;
```

### **Delegate**

The name of the delegate

```csharp
public FName Delegate;
```

## Constructors

### **FDelegate(FPackageIndex, FName)**

```csharp
public FDelegate(FPackageIndex _object, FName delegate)
```

#### Parameters

`_object` [FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

`delegate` [FName](./uassetapi.unrealtypes.fname.md)<br>

### **FDelegate()**

```csharp
public FDelegate()
```
