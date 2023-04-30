# FFieldPath

Namespace: UAssetAPI.UnrealTypes

```csharp
public class FFieldPath
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [FFieldPath](./uassetapi.unrealtypes.ffieldpath.md)

## Fields

### **Path**

Path to the FField object from the innermost FField to the outermost UObject (UPackage)

```csharp
public FName[] Path;
```

### **ResolvedOwner**

The cached owner of this field.

```csharp
public FPackageIndex ResolvedOwner;
```

## Constructors

### **FFieldPath(FName[], FPackageIndex)**

```csharp
public FFieldPath(FName[] path, FPackageIndex resolvedOwner)
```

#### Parameters

`path` [FName[]](./uassetapi.unrealtypes.fname.md)<br>

`resolvedOwner` [FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

### **FFieldPath()**

```csharp
public FFieldPath()
```
