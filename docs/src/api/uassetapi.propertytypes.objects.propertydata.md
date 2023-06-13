# PropertyData

Namespace: UAssetAPI.PropertyTypes.Objects

Generic Unreal property class.

```csharp
public abstract class PropertyData : System.ICloneable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PropertyData](./uassetapi.propertytypes.objects.propertydata.md)<br>
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

### **DuplicationIndex**

The duplication index of this property. Used to distinguish properties with the same name in the same struct.

```csharp
public int DuplicationIndex;
```

### **PropertyGuid**

An optional property GUID. Nearly always null.

```csharp
public Nullable<Guid> PropertyGuid;
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

### **PropertyType**

The type of this property as an FString.

```csharp
public FString PropertyType { get; }
```

#### Property Value

[FString](./uassetapi.unrealtypes.fstring.md)<br>

### **DefaultValue**

The default value of this property, used as a fallback when no value is defined. Null by default.

```csharp
public object DefaultValue { get; }
```

#### Property Value

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

## Constructors

### **PropertyData(FName)**

```csharp
public PropertyData(FName name)
```

#### Parameters

`name` [FName](./uassetapi.unrealtypes.fname.md)<br>

### **PropertyData()**

```csharp
public PropertyData()
```

## Methods

### **SetObject(Object)**

```csharp
public void SetObject(object value)
```

#### Parameters

`value` [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **GetObject&lt;T&gt;()**

```csharp
public T GetObject<T>()
```

#### Type Parameters

`T`<br>

#### Returns

T<br>

### **Read(AssetBinaryReader, Boolean, Int64, Int64)**

Reads out a property from a BinaryReader.

```csharp
public void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>
The BinaryReader to read from.

`includeHeader` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not to also read the "header" of the property, which is data considered by the Unreal Engine to be data that is part of the PropertyData base class rather than any particular child class.

`leng1` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>
An estimate for the length of the data being read out.

`leng2` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>
A second estimate for the length of the data being read out.

### **ResolveAncestries(UnrealPackage, AncestryInfo)**

Resolves the ancestry of all child properties of this property.

```csharp
public void ResolveAncestries(UnrealPackage asset, AncestryInfo ancestrySoFar)
```

#### Parameters

`asset` [UnrealPackage](./uassetapi.unrealpackage.md)<br>

`ancestrySoFar` [AncestryInfo](./uassetapi.propertytypes.objects.ancestryinfo.md)<br>

### **Write(AssetBinaryWriter, Boolean)**

Writes a property to a BinaryWriter.

```csharp
public int Write(AssetBinaryWriter writer, bool includeHeader)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
The BinaryWriter to write from.

`includeHeader` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not to also write the "header" of the property, which is data considered by the Unreal Engine to be data that is part of the PropertyData base class rather than any particular child class.

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
The length in bytes of the data that was written.

### **IsZero(UnrealPackage)**

Does the body of this property entirely consist of null bytes? If so, the body will not be serialized in unversioned properties.

```csharp
public bool IsZero(UnrealPackage asset)
```

#### Parameters

`asset` [UnrealPackage](./uassetapi.unrealpackage.md)<br>
The asset to test serialization within.

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether or not the property is zero.

### **FromString(String[], UAsset)**

Sets certain fields of the property based off of an array of strings.

```csharp
public void FromString(String[] d, UAsset asset)
```

#### Parameters

`d` [String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
An array of strings to derive certain fields from.

`asset` [UAsset](./uassetapi.uasset.md)<br>
The asset that the property belongs to.

### **Clone()**

Performs a deep clone of the current PropertyData instance.

```csharp
public object Clone()
```

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
A deep copy of the current property.

### **HandleCloned(PropertyData)**

```csharp
protected void HandleCloned(PropertyData res)
```

#### Parameters

`res` [PropertyData](./uassetapi.propertytypes.objects.propertydata.md)<br>
