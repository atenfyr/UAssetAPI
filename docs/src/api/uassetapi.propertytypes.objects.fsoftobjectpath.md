# FSoftObjectPath

Namespace: UAssetAPI.PropertyTypes.Objects

A reference variable to another object which may be null, and may become valid or invalid at any point.

```csharp
public struct FSoftObjectPath
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FSoftObjectPath](./uassetapi.propertytypes.objects.fsoftobjectpath.md)

## Fields

### **AssetPathName**

```csharp
public FName AssetPathName;
```

### **SubPathString**

```csharp
public FString SubPathString;
```

## Constructors

### **FSoftObjectPath(FName, FString)**

```csharp
FSoftObjectPath(FName assetPathName, FString subPathString)
```

#### Parameters

`assetPathName` [FName](./uassetapi.unrealtypes.fname.md)<br>

`subPathString` [FString](./uassetapi.unrealtypes.fstring.md)<br>
