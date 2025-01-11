# AssetBinaryReader

Namespace: UAssetAPI

Reads primitive data types from Unreal Engine assets.

```csharp
public class AssetBinaryReader : UnrealBinaryReader, System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BinaryReader](https://docs.microsoft.com/en-us/dotnet/api/system.io.binaryreader) → [UnrealBinaryReader](./uassetapi.unrealbinaryreader.md) → [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Fields

### **Asset**

```csharp
public UAsset Asset;
```

### **LoadUexp**

```csharp
public bool LoadUexp;
```

## Properties

### **BaseStream**

```csharp
public Stream BaseStream { get; }
```

#### Property Value

[Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

## Constructors

### **AssetBinaryReader(Stream, UAsset)**

```csharp
public AssetBinaryReader(Stream stream, UAsset asset)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

### **AssetBinaryReader(Stream, Boolean, UAsset)**

```csharp
public AssetBinaryReader(Stream stream, bool inLoadUexp, UAsset asset)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`inLoadUexp` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

## Methods

### **ReadPropertyGuid()**

```csharp
public Nullable<Guid> ReadPropertyGuid()
```

#### Returns

[Nullable&lt;Guid&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **ReadFName()**

```csharp
public FName ReadFName()
```

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>

### **ReadObjectThumbnail()**

```csharp
public FObjectThumbnail ReadObjectThumbnail()
```

#### Returns

[FObjectThumbnail](./uassetapi.unrealtypes.fobjectthumbnail.md)<br>

### **ReadLocMetadataObject()**

```csharp
public FLocMetadataObject ReadLocMetadataObject()
```

#### Returns

[FLocMetadataObject](./uassetapi.unrealtypes.flocmetadataobject.md)<br>

### **XFERSTRING()**

```csharp
public string XFERSTRING()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **XFERUNICODESTRING()**

```csharp
public string XFERUNICODESTRING()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **XFERTEXT()**

```csharp
public void XFERTEXT()
```

### **XFERNAME()**

```csharp
public FName XFERNAME()
```

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>

### **XFER_FUNC_NAME()**

```csharp
public FName XFER_FUNC_NAME()
```

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>

### **XFERPTR()**

```csharp
public FPackageIndex XFERPTR()
```

#### Returns

[FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

### **XFER_FUNC_POINTER()**

```csharp
public FPackageIndex XFER_FUNC_POINTER()
```

#### Returns

[FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

### **XFER_PROP_POINTER()**

```csharp
public KismetPropertyPointer XFER_PROP_POINTER()
```

#### Returns

[KismetPropertyPointer](./uassetapi.kismet.bytecode.kismetpropertypointer.md)<br>

### **XFER_OBJECT_POINTER()**

```csharp
public FPackageIndex XFER_OBJECT_POINTER()
```

#### Returns

[FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

### **ReadExpressionArray(EExprToken)**

```csharp
public KismetExpression[] ReadExpressionArray(EExprToken endToken)
```

#### Parameters

`endToken` [EExprToken](./uassetapi.kismet.bytecode.eexprtoken.md)<br>

#### Returns

[KismetExpression[]](./uassetapi.kismet.bytecode.kismetexpression.md)<br>
