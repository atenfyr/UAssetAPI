# FSoftObjectPath

Namespace: UAssetAPI.PropertyTypes.Objects

A reference variable to another object which may be null, and may become valid or invalid at any point.

```csharp
public struct FSoftObjectPath
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [FSoftObjectPath](./uassetapi.propertytypes.objects.fsoftobjectpath.md)<br>
Implements [IEquatable&lt;FSoftObjectPath&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1), [IStruct&lt;FSoftObjectPath&gt;](./uassetapi.propertytypes.objects.istruct-1.md)

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

### **FSoftObjectPath(AssetBinaryReader, Boolean)**

```csharp
FSoftObjectPath(AssetBinaryReader reader, bool allowIndex)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`allowIndex` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Methods

### **Read(AssetBinaryReader)**

```csharp
FSoftObjectPath Read(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

#### Returns

[FSoftObjectPath](./uassetapi.propertytypes.objects.fsoftobjectpath.md)<br>

### **Write(AssetBinaryWriter, Boolean)**

```csharp
int Write(AssetBinaryWriter writer, bool allowIndex)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`allowIndex` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Write(AssetBinaryWriter)**

```csharp
int Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Equals(FSoftObjectPath)**

```csharp
bool Equals(FSoftObjectPath other)
```

#### Parameters

`other` [FSoftObjectPath](./uassetapi.propertytypes.objects.fsoftobjectpath.md)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Equals(Object)**

```csharp
bool Equals(object obj)
```

#### Parameters

`obj` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **GetHashCode()**

```csharp
int GetHashCode()
```

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ToString()**

```csharp
string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FromString(String[], UAsset)**

```csharp
FSoftObjectPath FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[FSoftObjectPath](./uassetapi.propertytypes.objects.fsoftobjectpath.md)<br>
