# FTopLevelAssetPath

Namespace: UAssetAPI.PropertyTypes.Objects

```csharp
public struct FTopLevelAssetPath
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FTopLevelAssetPath](./uassetapi.propertytypes.objects.ftoplevelassetpath.md)

## Fields

### **PackageName**

Name of the package containing the asset e.g. /Path/To/Package
 If less than 5.1, this is null

```csharp
public FName PackageName;
```

### **AssetName**

Name of the asset within the package e.g. 'AssetName'.
 If less than 5.1, this contains the full path instead

```csharp
public FName AssetName;
```

## Constructors

### **FTopLevelAssetPath(FName, FName)**

```csharp
FTopLevelAssetPath(FName packageName, FName assetName)
```

#### Parameters

`packageName` [FName](./uassetapi.unrealtypes.fname.md)<br>

`assetName` [FName](./uassetapi.unrealtypes.fname.md)<br>
