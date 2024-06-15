# AssetBinaryWriter

Namespace: UAssetAPI

Writes primitive data types from Unreal Engine assets.

```csharp
public class AssetBinaryWriter : UnrealBinaryWriter, System.IDisposable, System.IAsyncDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [BinaryWriter](https://docs.microsoft.com/en-us/dotnet/api/system.io.binarywriter) → [UnrealBinaryWriter](./uassetapi.unrealbinarywriter.md) → [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable), [IAsyncDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.iasyncdisposable)

## Fields

### **Asset**

```csharp
public UnrealPackage Asset;
```

## Properties

### **BaseStream**

```csharp
public Stream BaseStream { get; }
```

#### Property Value

[Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

## Constructors

### **AssetBinaryWriter(UnrealPackage)**

```csharp
public AssetBinaryWriter(UnrealPackage asset)
```

#### Parameters

`asset` [UnrealPackage](./uassetapi.unrealpackage.md)<br>

### **AssetBinaryWriter(Stream, UnrealPackage)**

```csharp
public AssetBinaryWriter(Stream stream, UnrealPackage asset)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`asset` [UnrealPackage](./uassetapi.unrealpackage.md)<br>

### **AssetBinaryWriter(Stream, Encoding, UnrealPackage)**

```csharp
public AssetBinaryWriter(Stream stream, Encoding encoding, UnrealPackage asset)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`encoding` [Encoding](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding)<br>

`asset` [UnrealPackage](./uassetapi.unrealpackage.md)<br>

### **AssetBinaryWriter(Stream, Encoding, Boolean, UnrealPackage)**

```csharp
public AssetBinaryWriter(Stream stream, Encoding encoding, bool leaveOpen, UnrealPackage asset)
```

#### Parameters

`stream` [Stream](https://docs.microsoft.com/en-us/dotnet/api/system.io.stream)<br>

`encoding` [Encoding](https://docs.microsoft.com/en-us/dotnet/api/system.text.encoding)<br>

`leaveOpen` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`asset` [UnrealPackage](./uassetapi.unrealpackage.md)<br>

## Methods

### **Write(FName)**

```csharp
public void Write(FName name)
```

#### Parameters

`name` [FName](./uassetapi.unrealtypes.fname.md)<br>

### **WritePropertyGuid(Nullable&lt;Guid&gt;)**

```csharp
public void WritePropertyGuid(Nullable<Guid> guid)
```

#### Parameters

`guid` [Nullable&lt;Guid&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Write(FObjectThumbnail)**

```csharp
public void Write(FObjectThumbnail thumbnail)
```

#### Parameters

`thumbnail` [FObjectThumbnail](./uassetapi.unrealtypes.fobjectthumbnail.md)<br>

### **Write(FLocMetadataObject)**

```csharp
public void Write(FLocMetadataObject metadataObject)
```

#### Parameters

`metadataObject` [FLocMetadataObject](./uassetapi.unrealtypes.flocmetadataobject.md)<br>

### **XFERSTRING(String)**

This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!

```csharp
public int XFERSTRING(string val)
```

#### Parameters

`val` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **XFERUNICODESTRING(String)**

This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!

```csharp
public int XFERUNICODESTRING(string val)
```

#### Parameters

`val` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **XFERNAME(FName)**

This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!

```csharp
public int XFERNAME(FName val)
```

#### Parameters

`val` [FName](./uassetapi.unrealtypes.fname.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **XFER_FUNC_NAME(FName)**

This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!

```csharp
public int XFER_FUNC_NAME(FName val)
```

#### Parameters

`val` [FName](./uassetapi.unrealtypes.fname.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **XFERPTR(FPackageIndex)**

This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!

```csharp
public int XFERPTR(FPackageIndex val)
```

#### Parameters

`val` [FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **XFER_FUNC_POINTER(FPackageIndex)**

This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!

```csharp
public int XFER_FUNC_POINTER(FPackageIndex val)
```

#### Parameters

`val` [FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **XFER_PROP_POINTER(KismetPropertyPointer)**

This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!

```csharp
public int XFER_PROP_POINTER(KismetPropertyPointer val)
```

#### Parameters

`val` [KismetPropertyPointer](./uassetapi.kismet.bytecode.kismetpropertypointer.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **XFER_OBJECT_POINTER(FPackageIndex)**

This method is intended only to be used in parsing Kismet bytecode; please do not use it for any other purpose!

```csharp
public int XFER_OBJECT_POINTER(FPackageIndex val)
```

#### Parameters

`val` [FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
