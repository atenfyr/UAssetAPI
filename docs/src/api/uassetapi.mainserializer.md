# MainSerializer

Namespace: UAssetAPI

The main serializer for most property types in UAssetAPI.

```csharp
public static class MainSerializer
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MainSerializer](./uassetapi.mainserializer.md)

## Fields

### **AdditionalPropertyRegistry**

```csharp
public static String[] AdditionalPropertyRegistry;
```

## Methods

### **GetNamesOfAssembliesReferencedBy(Assembly)**

```csharp
public static IEnumerable<string> GetNamesOfAssembliesReferencedBy(Assembly assembly)
```

#### Parameters

`assembly` [Assembly](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.assembly)<br>

#### Returns

[IEnumerable&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

### **GenerateUnversionedHeader(List`1&, FName, FName, UAsset)**

Generates an unversioned header based on a list of properties, and sorts the list in the correct order to be serialized.

```csharp
public static FUnversionedHeader GenerateUnversionedHeader(List`1& data, FName parentName, FName parentModulePath, UAsset asset)
```

#### Parameters

`data` [List`1&](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1&)<br>
The list of properties to sort and generate an unversioned header from.

`parentName` [FName](./uassetapi.unrealtypes.fname.md)<br>
The name of the parent of all the properties.

`parentModulePath` [FName](./uassetapi.unrealtypes.fname.md)<br>
The path to the module that the parent class/struct of this property is contained within.

`asset` [UAsset](./uassetapi.uasset.md)<br>
The UAsset which the properties are contained within.

#### Returns

[FUnversionedHeader](./uassetapi.unversioned.funversionedheader.md)<br>

### **TypeToClass(FName, FName, AncestryInfo, FName, FName, UAsset, AssetBinaryReader, Int32, EPropertyTagFlags, Int32, Boolean, Boolean)**

Initializes the correct PropertyData class based off of serialized name, type, etc.

```csharp
public static PropertyData TypeToClass(FName type, FName name, AncestryInfo ancestry, FName parentName, FName parentModulePath, UAsset asset, AssetBinaryReader reader, int leng, EPropertyTagFlags propertyTagFlags, int ArrayIndex, bool includeHeader, bool isZero)
```

#### Parameters

`type` [FName](./uassetapi.unrealtypes.fname.md)<br>
The serialized type of this property.

`name` [FName](./uassetapi.unrealtypes.fname.md)<br>
The serialized name of this property.

`ancestry` [AncestryInfo](./uassetapi.propertytypes.objects.ancestryinfo.md)<br>
The ancestry of the parent of this property.

`parentName` [FName](./uassetapi.unrealtypes.fname.md)<br>
The name of the parent class/struct of this property.

`parentModulePath` [FName](./uassetapi.unrealtypes.fname.md)<br>
The path to the module that the parent class/struct of this property is contained within.

`asset` [UAsset](./uassetapi.uasset.md)<br>
The UAsset which this property is contained within.

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The BinaryReader to read from. If left unspecified, you must call the [PropertyData.Read(AssetBinaryReader, Boolean, Int64, Int64, PropertySerializationContext)](./uassetapi.propertytypes.objects.propertydata.md#readassetbinaryreader-boolean-int64-int64-propertyserializationcontext) method manually.

`leng` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The length of this property on disk in bytes.

`propertyTagFlags` [EPropertyTagFlags](./uassetapi.propertytypes.objects.epropertytagflags.md)<br>
Property tag flags, if available.

`ArrayIndex` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The duplication index of this property.

`includeHeader` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Does this property serialize its header in the current context?

`isZero` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Is the body of this property empty?

#### Returns

[PropertyData](./uassetapi.propertytypes.objects.propertydata.md)<br>
A new PropertyData instance based off of the passed parameters.

### **Read(AssetBinaryReader, AncestryInfo, FName, FName, FUnversionedHeader, Boolean)**

Reads a property into memory.

```csharp
public static PropertyData Read(AssetBinaryReader reader, AncestryInfo ancestry, FName parentName, FName parentModulePath, FUnversionedHeader header, bool includeHeader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The BinaryReader to read from. The underlying stream should be at the position of the property to be read.

`ancestry` [AncestryInfo](./uassetapi.propertytypes.objects.ancestryinfo.md)<br>
The ancestry of the parent of this property.

`parentName` [FName](./uassetapi.unrealtypes.fname.md)<br>
The name of the parent class/struct of this property.

`parentModulePath` [FName](./uassetapi.unrealtypes.fname.md)<br>
The path to the module that the parent class/struct of this property is contained within.

`header` [FUnversionedHeader](./uassetapi.unversioned.funversionedheader.md)<br>
The unversioned header to be used when reading this property. Leave null if none exists.

`includeHeader` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Does this property serialize its header in the current context?

#### Returns

[PropertyData](./uassetapi.propertytypes.objects.propertydata.md)<br>
The property read from disk.

### **ReadFProperty(AssetBinaryReader)**

Reads an FProperty into memory. Primarily used as a part of [StructExport](./uassetapi.exporttypes.structexport.md) serialization.

```csharp
public static FProperty ReadFProperty(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The BinaryReader to read from. The underlying stream should be at the position of the FProperty to be read.

#### Returns

[FProperty](./uassetapi.fieldtypes.fproperty.md)<br>
The FProperty read from disk.

### **WriteFProperty(FProperty, AssetBinaryWriter)**

Serializes an FProperty from memory.

```csharp
public static void WriteFProperty(FProperty prop, AssetBinaryWriter writer)
```

#### Parameters

`prop` [FProperty](./uassetapi.fieldtypes.fproperty.md)<br>
The FProperty to serialize.

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
The BinaryWriter to serialize the FProperty to.

### **ReadUProperty(AssetBinaryReader, FName)**

Reads a UProperty into memory. Primarily used as a part of [PropertyExport](./uassetapi.exporttypes.propertyexport.md) serialization.

```csharp
public static UProperty ReadUProperty(AssetBinaryReader reader, FName serializedType)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The BinaryReader to read from. The underlying stream should be at the position of the UProperty to be read.

`serializedType` [FName](./uassetapi.unrealtypes.fname.md)<br>
The type of UProperty to be read.

#### Returns

[UProperty](./uassetapi.fieldtypes.uproperty.md)<br>
The FProperty read from disk.

### **ReadUProperty(AssetBinaryReader, Type)**

Reads a UProperty into memory. Primarily used as a part of [PropertyExport](./uassetapi.exporttypes.propertyexport.md) serialization.

```csharp
public static UProperty ReadUProperty(AssetBinaryReader reader, Type requestedType)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The BinaryReader to read from. The underlying stream should be at the position of the UProperty to be read.

`requestedType` [Type](https://docs.microsoft.com/en-us/dotnet/api/system.type)<br>
The type of UProperty to be read.

#### Returns

[UProperty](./uassetapi.fieldtypes.uproperty.md)<br>
The FProperty read from disk.

### **ReadUProperty&lt;T&gt;(AssetBinaryReader)**

Reads a UProperty into memory. Primarily used as a part of [PropertyExport](./uassetapi.exporttypes.propertyexport.md) serialization.

```csharp
public static T ReadUProperty<T>(AssetBinaryReader reader)
```

#### Type Parameters

`T`<br>

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The BinaryReader to read from. The underlying stream should be at the position of the UProperty to be read.

#### Returns

T<br>
The FProperty read from disk.

### **WriteUProperty(UProperty, AssetBinaryWriter)**

Serializes a UProperty from memory.

```csharp
public static void WriteUProperty(UProperty prop, AssetBinaryWriter writer)
```

#### Parameters

`prop` [UProperty](./uassetapi.fieldtypes.uproperty.md)<br>
The UProperty to serialize.

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
The BinaryWriter to serialize the UProperty to.

### **Write(PropertyData, AssetBinaryWriter, Boolean)**

Serializes a property from memory.

```csharp
public static int Write(PropertyData property, AssetBinaryWriter writer, bool includeHeader)
```

#### Parameters

`property` [PropertyData](./uassetapi.propertytypes.objects.propertydata.md)<br>
The property to serialize.

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
The BinaryWriter to serialize the property to.

`includeHeader` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Does this property serialize its header in the current context?

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The serial offset where the length of the property is stored.
