# FSoftObjectPath

Namespace: UAssetAPI.PropertyTypes.Objects

A reference variable to another object which may be null, and may become valid or invalid at any point.

```csharp
public struct FSoftObjectPath
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FSoftObjectPath](./uassetapi.propertytypes.objects.fsoftobjectpath.md)

## Fields

### **AssetPath**

Asset path, patch to a top level object in a package. This is /package/path.assetname/

```csharp
public FTopLevelAssetPath AssetPath;
```

### **SubPathString**

Optional FString for subobject within an asset. This is the sub path after the :

```csharp
public FString SubPathString;
```

## Constructors

### **FSoftObjectPath(FName, FName, FString)**

```csharp
FSoftObjectPath(FName packageName, FName assetName, FString subPathString)
```

#### Parameters

`packageName` [FName](./uassetapi.unrealtypes.fname.md)<br>

`assetName` [FName](./uassetapi.unrealtypes.fname.md)<br>

`subPathString` [FString](./uassetapi.unrealtypes.fstring.md)<br>

### **FSoftObjectPath(FTopLevelAssetPath, FString)**

```csharp
FSoftObjectPath(FTopLevelAssetPath assetPath, FString subPathString)
```

#### Parameters

`assetPath` [FTopLevelAssetPath](./uassetapi.propertytypes.objects.ftoplevelassetpath.md)<br>

`subPathString` [FString](./uassetapi.unrealtypes.fstring.md)<br>

### **FSoftObjectPath(AssetBinaryReader)**

```csharp
FSoftObjectPath(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

## Methods

### **Write(AssetBinaryWriter)**

```csharp
int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
