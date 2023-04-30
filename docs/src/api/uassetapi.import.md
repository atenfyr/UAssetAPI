# Import

Namespace: UAssetAPI

UObject resource type for objects that are referenced by this package, but contained within another package.

```csharp
public class Import
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Import](./uassetapi.import.md)

## Fields

### **ObjectName**

The name of the UObject represented by this resource.

```csharp
public FName ObjectName;
```

### **OuterIndex**

Location of the resource for this resource's Outer (import/other export). 0 = this resource is a top-level UPackage

```csharp
public FPackageIndex OuterIndex;
```

### **ClassPackage**

```csharp
public FName ClassPackage;
```

### **ClassName**

```csharp
public FName ClassName;
```

## Constructors

### **Import(String, String, FPackageIndex, String, UAsset)**

```csharp
public Import(string classPackage, string className, FPackageIndex outerIndex, string objectName, UAsset asset)
```

#### Parameters

`classPackage` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`className` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`outerIndex` [FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

`objectName` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

### **Import(FName, FName, FPackageIndex, FName)**

```csharp
public Import(FName classPackage, FName className, FPackageIndex outerIndex, FName objectName)
```

#### Parameters

`classPackage` [FName](./uassetapi.unrealtypes.fname.md)<br>

`className` [FName](./uassetapi.unrealtypes.fname.md)<br>

`outerIndex` [FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

`objectName` [FName](./uassetapi.unrealtypes.fname.md)<br>

### **Import(AssetBinaryReader)**

```csharp
public Import(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

### **Import()**

```csharp
public Import()
```
