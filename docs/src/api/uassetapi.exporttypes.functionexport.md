# FunctionExport

Namespace: UAssetAPI.ExportTypes

Export data for a blueprint function.

```csharp
public class FunctionExport : StructExport, System.ICloneable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Export](./uassetapi.exporttypes.export.md) → [NormalExport](./uassetapi.exporttypes.normalexport.md) → [FieldExport](./uassetapi.exporttypes.fieldexport.md) → [StructExport](./uassetapi.exporttypes.structexport.md) → [FunctionExport](./uassetapi.exporttypes.functionexport.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable)

## Fields

### **FunctionFlags**

```csharp
public EFunctionFlags FunctionFlags;
```

### **SuperStruct**

Struct this inherits from, may be null

```csharp
public FPackageIndex SuperStruct;
```

### **Children**

List of child fields

```csharp
public FPackageIndex[] Children;
```

### **LoadedProperties**

Properties serialized with this struct definition

```csharp
public FProperty[] LoadedProperties;
```

### **ScriptBytecode**

The bytecode instructions contained within this struct.

```csharp
public KismetExpression[] ScriptBytecode;
```

### **ScriptBytecodeSize**

Bytecode size in total in deserialized memory. Filled out in lieu of [StructExport.ScriptBytecode](./uassetapi.exporttypes.structexport.md#scriptbytecode) if an error occurs during bytecode parsing.

```csharp
public int ScriptBytecodeSize;
```

### **ScriptBytecodeRaw**

Raw binary bytecode data. Filled out in lieu of [StructExport.ScriptBytecode](./uassetapi.exporttypes.structexport.md#scriptbytecode) if an error occurs during bytecode parsing.

```csharp
public Byte[] ScriptBytecodeRaw;
```

### **Field**

```csharp
public UField Field;
```

### **Data**

```csharp
public List<PropertyData> Data;
```

### **ObjectGuid**

```csharp
public Nullable<Guid> ObjectGuid;
```

### **SerializationControl**

```csharp
public EClassSerializationControlExtension SerializationControl;
```

### **Operation**

```csharp
public EOverriddenPropertyOperation Operation;
```

### **ObjectName**

The name of the UObject represented by this resource.

```csharp
public FName ObjectName;
```

### **OuterIndex**

Location of the resource for this resource's Outer (import/other export). 0 = this resource is a top-level UPackage

```csharp
public FPackageIndex OuterIndex;
```

### **ClassIndex**

Location of this export's class (import/other export). 0 = this export is a UClass

```csharp
public FPackageIndex ClassIndex;
```

### **SuperIndex**

Location of this export's parent class (import/other export). 0 = this export is not derived from UStruct

```csharp
public FPackageIndex SuperIndex;
```

### **TemplateIndex**

Location of this export's template (import/other export). 0 = there is some problem

```csharp
public FPackageIndex TemplateIndex;
```

### **ObjectFlags**

The object flags for the UObject represented by this resource. Only flags that match the RF_Load combination mask will be loaded from disk and applied to the UObject.

```csharp
public EObjectFlags ObjectFlags;
```

### **SerialSize**

The number of bytes to serialize when saving/loading this export's UObject.

```csharp
public long SerialSize;
```

### **SerialOffset**

The location (into the FLinker's underlying file reader archive) of the beginning of the data for this export's UObject. Used for verification only.

```csharp
public long SerialOffset;
```

### **ScriptSerializationStartOffset**

The location (relative to SerialOffset) of the beginning of the portion of this export's data that is serialized using tagged property serialization.
 Serialized into packages using tagged property serialization as of [ObjectVersionUE5.SCRIPT_SERIALIZATION_OFFSET](./uassetapi.unrealtypes.objectversionue5.md#script_serialization_offset) (5.4).

```csharp
public long ScriptSerializationStartOffset;
```

### **ScriptSerializationEndOffset**

The location (relative to SerialOffset) of the end of the portion of this export's data that is serialized using tagged property serialization.
 Serialized into packages using tagged property serialization as of [ObjectVersionUE5.SCRIPT_SERIALIZATION_OFFSET](./uassetapi.unrealtypes.objectversionue5.md#script_serialization_offset) (5.4)

```csharp
public long ScriptSerializationEndOffset;
```

### **bForcedExport**

Was this export forced into the export table via OBJECTMARK_ForceTagExp?

```csharp
public bool bForcedExport;
```

### **bNotForClient**

Should this export not be loaded on clients?

```csharp
public bool bNotForClient;
```

### **bNotForServer**

Should this export not be loaded on servers?

```csharp
public bool bNotForServer;
```

### **PackageGuid**

If this object is a top level package (which must have been forced into the export table via OBJECTMARK_ForceTagExp), this is the GUID for the original package file. Deprecated

```csharp
public Guid PackageGuid;
```

### **IsInheritedInstance**



```csharp
public bool IsInheritedInstance;
```

### **PackageFlags**

If this export is a top-level package, this is the flags for the original package

```csharp
public EPackageFlags PackageFlags;
```

### **bNotAlwaysLoadedForEditorGame**

Should this export be always loaded in editor game?

```csharp
public bool bNotAlwaysLoadedForEditorGame;
```

### **bIsAsset**

Is this export an asset?

```csharp
public bool bIsAsset;
```

### **GeneratePublicHash**



```csharp
public bool GeneratePublicHash;
```

### **SerializationBeforeSerializationDependencies**

```csharp
public List<FPackageIndex> SerializationBeforeSerializationDependencies;
```

### **CreateBeforeSerializationDependencies**

```csharp
public List<FPackageIndex> CreateBeforeSerializationDependencies;
```

### **SerializationBeforeCreateDependencies**

```csharp
public List<FPackageIndex> SerializationBeforeCreateDependencies;
```

### **CreateBeforeCreateDependencies**

```csharp
public List<FPackageIndex> CreateBeforeCreateDependencies;
```

### **Extras**

Miscellaneous, unparsed export data, stored as a byte array.

```csharp
public Byte[] Extras;
```

### **Asset**

The asset that this export is parsed with.

```csharp
public UAsset Asset;
```

## Properties

## Constructors

### **FunctionExport(Export)**

```csharp
public FunctionExport(Export super)
```

#### Parameters

`super` [Export](./uassetapi.exporttypes.export.md)<br>

### **FunctionExport(UAsset, Byte[])**

```csharp
public FunctionExport(UAsset asset, Byte[] extras)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>

`extras` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **FunctionExport()**

```csharp
public FunctionExport()
```

## Methods

### **Read(AssetBinaryReader, Int32)**

```csharp
public void Read(AssetBinaryReader reader, int nextStarting)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`nextStarting` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Write(AssetBinaryWriter)**

```csharp
public void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>
