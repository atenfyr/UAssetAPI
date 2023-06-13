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

### **Export(UnrealPackage, Byte[])**

```csharp
public Export(UnrealPackage asset, Byte[] extras)
```

#### Parameters

`asset` [UnrealPackage](./uassetapi.unrealpackage.md)<br>

`extras` [Byte[]](https://docs.microsoft.com/en-us/dotnet/api/system.byte)<br>

### **Export()**

```csharp
public Export()
```

## Methods

### **ShouldSerializeOuterIndex()**

```csharp
public bool ShouldSerializeOuterIndex()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ShouldSerializeClassIndex()**

```csharp
public bool ShouldSerializeClassIndex()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ShouldSerializeSuperIndex()**

```csharp
public bool ShouldSerializeSuperIndex()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ShouldSerializeTemplateIndex()**

```csharp
public bool ShouldSerializeTemplateIndex()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ShouldSerializeZen_OuterIndex()**

```csharp
public bool ShouldSerializeZen_OuterIndex()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ShouldSerializeZen_ClassIndex()**

```csharp
public bool ShouldSerializeZen_ClassIndex()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ShouldSerializeZen_SuperIndex()**

```csharp
public bool ShouldSerializeZen_SuperIndex()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ShouldSerializeZen_TemplateIndex()**

```csharp
public bool ShouldSerializeZen_TemplateIndex()
```

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Read(AssetBinaryReader, Int32)**

```csharp
public void Read(AssetBinaryReader reader, int nextStarting)
```

#### Parameters

`reader` [AssetBinaryReader](./uassetapi.assetbinaryreader.md)<br>

`nextStarting` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **ResolveAncestries(UnrealPackage, AncestryInfo)**

Resolves the ancestry of all child properties of this export.

```csharp
public void ResolveAncestries(UnrealPackage asset, AncestryInfo ancestrySoFar)
```

#### Parameters

`asset` [UnrealPackage](./uassetapi.unrealpackage.md)<br>

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

### **GetExportMapEntrySize(UnrealPackage)**

```csharp
public static long GetExportMapEntrySize(UnrealPackage asset)
```

#### Parameters

`asset` [UnrealPackage](./uassetapi.unrealpackage.md)<br>

#### Returns

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **WriteExportMapEntry(AssetBinaryWriter)**

```csharp
public void WriteExportMapEntry(AssetBinaryWriter writer)
```

#### Parameters

`writer` [AssetBinaryWriter](./uassetapi.assetbinarywriter.md)<br>

### **GetAllObjectExportFields(UnrealPackage)**

```csharp
public static MemberInfo[] GetAllObjectExportFields(UnrealPackage asset)
```

#### Parameters

`asset` [UnrealPackage](./uassetapi.unrealpackage.md)<br>

#### Returns

[MemberInfo[]](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.memberinfo)<br>

### **GetAllFieldNames(UnrealPackage)**

```csharp
public static String[] GetAllFieldNames(UnrealPackage asset)
```

#### Parameters

`asset` [UnrealPackage](./uassetapi.unrealpackage.md)<br>

#### Returns

[String[]](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **GetExportClassType()**

```csharp
public FName GetExportClassType()
```

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>

### **GetClassTypeForAncestry(UnrealPackage)**

```csharp
public FName GetClassTypeForAncestry(UnrealPackage asset)
```

#### Parameters

`asset` [UnrealPackage](./uassetapi.unrealpackage.md)<br>

#### Returns

[FName](./uassetapi.unrealtypes.fname.md)<br>

### **GetClassTypeForAncestry(FPackageIndex, UnrealPackage)**

```csharp
public static FName GetClassTypeForAncestry(FPackageIndex classIndex, UnrealPackage asset)
```

#### Parameters

`classIndex` [FPackageIndex](./uassetapi.unrealtypes.fpackageindex.md)<br>

`asset` [UnrealPackage](./uassetapi.unrealpackage.md)<br>

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
