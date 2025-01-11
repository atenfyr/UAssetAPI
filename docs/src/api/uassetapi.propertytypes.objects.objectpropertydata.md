# ObjectPropertyData

Namespace: UAssetAPI.PropertyTypes.Objects

Describes a reference variable to another object (import/export) which may be null ([FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)).

```csharp
public class ObjectPropertyData : PropertyData`1, System.ICloneable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PropertyData](./uassetapi.propertytypes.objects.propertydata.md) → [PropertyData&lt;FPackageIndex&gt;](./uassetapi.propertytypes.objects.propertydata-1.md) → [ObjectPropertyData](./uassetapi.propertytypes.objects.objectpropertydata.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable)

## Fields

### **Name**

The name of this property.

```csharp
public FName Name;
```

### **Ancestry**

The ancestry of this property. Contains information about all the classes/structs that this property is contained within. Not serialized.

```csharp
public AncestryInfo Ancestry;
```

### **ArrayIndex**

The array index of this property. Used to distinguish properties with the same name in the same struct.

```csharp
public int ArrayIndex;
```

### **PropertyGuid**

An optional property GUID. Nearly always null.

```csharp
public Nullable<Guid> PropertyGuid;
```

### **IsZero**

Whether or not this property is "zero," meaning that its body can be skipped during unversioned property serialization because it consists solely of null bytes.



This field will always be treated as if it is false if [PropertyData.CanBeZero(UAsset)](./uassetapi.propertytypes.objects.propertydata.md#canbezerouasset) does not return true.

```csharp
public bool IsZero;
```

### **PropertyTagFlags**

```csharp
public EPropertyTagFlags PropertyTagFlags;
```

### **PropertyTagExtensions**

Optional extensions to serialize with this property.

```csharp
public EPropertyTagExtension PropertyTagExtensions;
```

### **OverrideOperation**

```csharp
public EOverriddenPropertyOperation OverrideOperation;
```

### **bExperimentalOverridableLogic**

```csharp
public bool bExperimentalOverridableLogic;
```

### **Offset**

The offset of this property on disk. This is for the user only, and has no bearing in the API itself.

```csharp
public long Offset;
```

### **Tag**

An optional tag which can be set on any property in memory. This is for the user only, and has no bearing in the API itself.

```csharp
public object Tag;
```

## Properties

### **PropertyType**

```csharp
public FString PropertyType { get; }
```

#### Property Value

[FString](./uassetapi.unrealtypes.fstring.md)<br>

### **DefaultValue**

```csharp
public object DefaultValue { get; }
```

#### Property Value

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **Value**

The "main value" of this property, if such a concept is applicable to the property in question. Properties may contain other values as well, in which case they will be present as other fields in the child class.

```csharp
public FPackageIndex Value { get; set; }
```

#### Property Value

[FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

### **RawValue**

```csharp
public object RawValue { get; set; }
```

#### Property Value

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **ShouldBeRegistered**

Determines whether or not this particular property should be registered in the property registry and automatically used when parsing assets.

```csharp
public bool ShouldBeRegistered { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **HasCustomStructSerialization**

Determines whether or not this particular property has custom serialization within a StructProperty.

```csharp
public bool HasCustomStructSerialization { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **ObjectPropertyData(FName)**

```csharp
public ObjectPropertyData(FName name)
```

#### Parameters

`name` [FName](./uassetapi.unrealtypes.fname.md)<br>

### **ObjectPropertyData()**

```csharp
public ObjectPropertyData()
```

## Methods

### **IsImport()**

Returns true if this ObjectProperty represents an import.

```csharp
public bool IsImport()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Is this ObjectProperty an import?

### **IsExport()**

Returns true if this ObjectProperty represents an export.

```csharp
public bool IsExport()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Is this ObjectProperty an export?

### **IsNull()**

Return true if this ObjectProperty represents null (i.e. neither an import nor an export)

```csharp
public bool IsNull()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Does this ObjectProperty represent null?

### **ToImport(UAsset)**

Check that this ObjectProperty is an import index and return the corresponding import.

```csharp
public Import ToImport(UAsset asset)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[Import](./uassetapi.import.md)<br>
The import that this ObjectProperty represents in the import map.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when this is not an index into the import map.

### **ToExport(UAsset)**

Check that this ObjectProperty is an export index and return the corresponding export.

```csharp
public Export ToExport(UAsset asset)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[Export](./uassetapi.exporttypes.export.md)<br>
The export that this ObjectProperty represents in the the export map.

#### Exceptions

[InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/system.invalidoperationexception)<br>
Thrown when this is not an index into the export map.

### **Read(AssetBinaryReader, Boolean, Int64, Int64, PropertySerializationContext)**

```csharp
public void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2, PropertySerializationContext serializationContext)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`includeHeader` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`leng1` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

`leng2` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

`serializationContext` [PropertySerializationContext](./uassetapi.propertytypes.objects.propertyserializationcontext.md)<br>

### **Write(AssetBinaryWriter, Boolean, PropertySerializationContext)**

```csharp
public int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`includeHeader` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`serializationContext` [PropertySerializationContext](./uassetapi.propertytypes.objects.propertyserializationcontext.md)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **FromString(String[], UAsset)**

```csharp
public void FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>
