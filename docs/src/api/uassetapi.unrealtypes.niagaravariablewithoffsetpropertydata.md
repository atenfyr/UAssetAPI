# NiagaraVariableWithOffsetPropertyData

Namespace: UAssetAPI.UnrealTypes

```csharp
public class NiagaraVariableWithOffsetPropertyData : NiagaraVariablePropertyData, System.ICloneable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [PropertyData](./uassetapi.propertytypes.objects.propertydata.md) → [PropertyData&lt;List&lt;PropertyData&gt;&gt;](./uassetapi.propertytypes.objects.propertydata-1.md) → [StructPropertyData](./uassetapi.propertytypes.structs.structpropertydata.md) → [NiagaraVariablePropertyData](./uassetapi.unrealtypes.niagaravariablepropertydata.md) → [NiagaraVariableWithOffsetPropertyData](./uassetapi.unrealtypes.niagaravariablewithoffsetpropertydata.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable)

## Fields

### **VariableName**

```csharp
public FName VariableName;
```

### **VariableOffset**

```csharp
public int VariableOffset;
```

### **StructType**

```csharp
public FName StructType;
```

### **SerializeNone**

```csharp
public bool SerializeNone;
```

### **StructGUID**

```csharp
public Guid StructGUID;
```

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

### **HasCustomStructSerialization**

```csharp
public bool HasCustomStructSerialization { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **PropertyType**

```csharp
public FString PropertyType { get; }
```

#### Property Value

[FString](./uassetapi.unrealtypes.fstring.md)<br>

### **Value**

The "main value" of this property, if such a concept is applicable to the property in question. Properties may contain other values as well, in which case they will be present as other fields in the child class.

```csharp
public List<PropertyData> Value { get; set; }
```

#### Property Value

[List&lt;PropertyData&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

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

### **DefaultValue**

The default value of this property, used as a fallback when no value is defined. Null by default.

```csharp
public object DefaultValue { get; }
```

#### Property Value

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

## Constructors

### **NiagaraVariableWithOffsetPropertyData(FName)**

```csharp
public NiagaraVariableWithOffsetPropertyData(FName name)
```

#### Parameters

`name` [FName](./uassetapi.unrealtypes.fname.md)<br>

### **NiagaraVariableWithOffsetPropertyData(FName, FName)**

```csharp
public NiagaraVariableWithOffsetPropertyData(FName name, FName forcedType)
```

#### Parameters

`name` [FName](./uassetapi.unrealtypes.fname.md)<br>

`forcedType` [FName](./uassetapi.unrealtypes.fname.md)<br>

### **NiagaraVariableWithOffsetPropertyData()**

```csharp
public NiagaraVariableWithOffsetPropertyData()
```

## Methods

### **Read(AssetBinaryReader, Boolean, Int64, Int64)**

```csharp
public void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`includeHeader` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

`leng1` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

`leng2` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **Write(AssetBinaryWriter, Boolean)**

```csharp
public int Write(AssetBinaryWriter writer, bool includeHeader)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

`includeHeader` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
