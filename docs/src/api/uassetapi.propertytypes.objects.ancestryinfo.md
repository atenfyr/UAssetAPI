# AncestryInfo

Namespace: UAssetAPI.PropertyTypes.Objects

```csharp
public class AncestryInfo : System.ICloneable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [AncestryInfo](./uassetapi.propertytypes.objects.ancestryinfo.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable)

## Fields

### **Ancestors**

```csharp
public List<FName> Ancestors;
```

## Properties

### **Parent**

```csharp
public FName Parent { get; set; }
```

#### Property Value

[FName](./uassetapi.unrealtypes.fname.md)<br>

## Constructors

### **AncestryInfo()**

```csharp
public AncestryInfo()
```

## Methods

### **Clone()**

```csharp
public object Clone()
```

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **CloneWithoutParent()**

```csharp
public AncestryInfo CloneWithoutParent()
```

#### Returns

[AncestryInfo](./uassetapi.propertytypes.objects.ancestryinfo.md)<br>

### **Initialize(AncestryInfo, FName, FName)**

```csharp
public void Initialize(AncestryInfo ancestors, FName dad, FName modulePath)
```

#### Parameters

`ancestors` [AncestryInfo](./uassetapi.propertytypes.objects.ancestryinfo.md)<br>

`dad` [FName](./uassetapi.unrealtypes.fname.md)<br>

`modulePath` [FName](./uassetapi.unrealtypes.fname.md)<br>

### **SetAsParent(FName, FName)**

```csharp
public void SetAsParent(FName dad, FName modulePath)
```

#### Parameters

`dad` [FName](./uassetapi.unrealtypes.fname.md)<br>

`modulePath` [FName](./uassetapi.unrealtypes.fname.md)<br>
