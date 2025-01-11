# Export

Namespace: UAssetAPI.ExportTypes

UObject resource type for objects that are contained within this package and can be referenced by other packages.

```csharp
public class Export : System.ICloneable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Export](./uassetapi.exporttypes.export.md)<br>
Implements [ICloneable](https://docs.microsoft.com/en-us/dotnet/api/system.icloneable)

## Fields

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

## Constructors

### **Export(UAsset, Byte[])**

```csharp
public Export(UAsset asset, Byte[] extras)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>

`extras` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **Export()**

```csharp
public Export()
```

## Methods

### **Read(AssetBinaryReader, Int32)**

```csharp
public void Read(AssetBinaryReader reader, int nextStarting)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`nextStarting` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ResolveAncestries(UAsset, AncestryInfo)**

Resolves the ancestry of all child properties of this export.

```csharp
public void ResolveAncestries(UAsset asset, AncestryInfo ancestrySoFar)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>

`ancestrySoFar` [AncestryInfo](./uassetapi.propertytypes.objects.ancestryinfo.md)<br>

### **Write(AssetBinaryWriter)**

```csharp
public void Write(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

### **ReadExportMapEntry(AssetBinaryReader)**

```csharp
public void ReadExportMapEntry(AssetBinaryReader reader)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

### **GetExportMapEntrySize(UAsset)**

```csharp
public static long GetExportMapEntrySize(UAsset asset)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **WriteExportMapEntry(AssetBinaryWriter)**

```csharp
public void WriteExportMapEntry(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

### **GetAllObjectExportFields(UAsset)**

```csharp
public static MemberInfo[] GetAllObjectExportFields(UAsset asset)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[MemberInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo)<br>

### **GetAllFieldNames(UAsset)**

```csharp
public static String[] GetAllFieldNames(UAsset asset)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>

#### Returns

[String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **GetExportClassType()**

```csharp
public FName GetExportClassType()
```

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>

### **GetClassTypeForAncestry(UAsset, FName&)**

```csharp
public FName GetClassTypeForAncestry(UAsset asset, FName& modulePath)
```

#### Parameters

`asset` [UAsset](./uassetapi.uasset.md)<br>

`modulePath` [FName&](./uassetapi.unrealtypes.fname&.md)<br>

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>

### **GetClassTypeForAncestry(FPackageIndex, UAsset, FName&)**

```csharp
public static FName GetClassTypeForAncestry(FPackageIndex classIndex, UAsset asset, FName& modulePath)
```

#### Parameters

`classIndex` [FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

`asset` [UAsset](./uassetapi.uasset.md)<br>

`modulePath` [FName&](./uassetapi.unrealtypes.fname&.md)<br>

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>

### **ToString()**

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Clone()**

```csharp
public object Clone()
```

#### Returns

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **ConvertToChildExport&lt;T&gt;()**

Creates a child export instance with the same export details as the current export.

```csharp
public T ConvertToChildExport<T>()
```

#### Type Parameters

`T`<br>
The type of child export to create.

#### Returns

T<br>
An instance of the child export type provided with the export details copied over.
