# FieldExport

Namespace: UAssetAPI.ExportTypes

Export data for a [UField](./uassetapi.fieldtypes.ufield.md).

```csharp
public class FieldExport : NormalExport, System.ICloneable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Export](./uassetapi.exporttypes.export.md) → [NormalExport](./uassetapi.exporttypes.normalexport.md) → [FieldExport](./uassetapi.exporttypes.fieldexport.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable)

## Fields

### **Field**

```csharp
public UField Field;
```

### **Data**

```csharp
public List<PropertyData> Data;
```

### **ObjectName**

The name of the UObject represented by this resource.

```csharp
public FName ObjectName;
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
 Serialized into versioned packages as of [ObjectVersionUE5.SCRIPT_SERIALIZATION_OFFSET](./uassetapi.unrealtypes.objectversionue5.md#script_serialization_offset) (5.4).



Assumed to be always zero for now; if you find an asset where it is not, submit an issue on the UAssetAPI repository

```csharp
public long ScriptSerializationStartOffset;
```

### **ScriptSerializationEndOffset**

The location (relative to SerialOffset) of the end of the portion of this export's data that is serialized using tagged property serialization.
 Serialized into versioned packages as of [ObjectVersionUE5.SCRIPT_SERIALIZATION_OFFSET](./uassetapi.unrealtypes.objectversionue5.md#script_serialization_offset) (5.4)



Assumed to be always zero for now; if you find an asset where it is not, submit an issue on the UAssetAPI repository

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

### **Zen_OuterIndex**

```csharp
public FPackageObjectIndex Zen_OuterIndex;
```

### **Zen_ClassIndex**

```csharp
public FPackageObjectIndex Zen_ClassIndex;
```

### **Zen_SuperIndex**

```csharp
public FPackageObjectIndex Zen_SuperIndex;
```

### **Zen_TemplateIndex**

```csharp
public FPackageObjectIndex Zen_TemplateIndex;
```

### **PublicExportHash**

PublicExportHash. Interpreted as a global import FPackageObjectIndex in UE4 assets.

```csharp
public ulong PublicExportHash;
```

### **Padding**

```csharp
public Byte[] Padding;
```

### **Extras**

Miscellaneous, unparsed export data, stored as a byte array.

```csharp
public Byte[] Extras;
```

### **Asset**

The asset that this export is parsed with.

```csharp
public UnrealPackage Asset;
```

## Properties

### **Item**

```csharp
public PropertyData Item { get; set; }
```

#### Property Value

[PropertyData](./uassetapi.propertytypes.objects.propertydata.md)<br>

### **Item**

```csharp
public PropertyData Item { get; set; }
```

#### Property Value

[PropertyData](./uassetapi.propertytypes.objects.propertydata.md)<br>

### **Item**

```csharp
public PropertyData Item { get; set; }
```

#### Property Value

[PropertyData](./uassetapi.propertytypes.objects.propertydata.md)<br>

### **OuterIndex**

Location of the resource for this resource's Outer (import/other export). 0 = this resource is a top-level UPackage

```csharp
public FPackageIndex OuterIndex { get; set; }
```

#### Property Value

[FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

### **ClassIndex**

Location of this export's class (import/other export). 0 = this export is a UClass

```csharp
public FPackageIndex ClassIndex { get; set; }
```

#### Property Value

[FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

### **SuperIndex**

Location of this export's parent class (import/other export). 0 = this export is not derived from UStruct

```csharp
public FPackageIndex SuperIndex { get; set; }
```

#### Property Value

[FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

### **TemplateIndex**

Location of this export's template (import/other export). 0 = there is some problem

```csharp
public FPackageIndex TemplateIndex { get; set; }
```

#### Property Value

[FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

## Constructors

### **FieldExport(Export)**

```csharp
public FieldExport(Export super)
```

#### Parameters

`super` [Export](./uassetapi.exporttypes.export.md)<br>

### **FieldExport(UAsset, Byte[])**

```csharp
public FieldExport(UAsset asset, Byte[] extras)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>

`extras` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **FieldExport()**

```csharp
public FieldExport()
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
