# ObjectVersionUE5

Namespace: UAssetAPI.UnrealTypes

An enum used to represent the global object version of UE5.

```csharp
public enum ObjectVersionUE5
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [ObjectVersionUE5](./uassetapi.unrealtypes.objectversionue5.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| UNKNOWN | 0 | Unknown version |
| INITIAL_VERSION | 1000 | The original UE5 version, at the time this was added the UE4 version was 522, so UE5 will start from 1000 to show a clear difference |
| NAMES_REFERENCED_FROM_EXPORT_DATA | 1001 | Support stripping names that are not referenced from export data |
| PAYLOAD_TOC | 1002 | Added a payload table of contents to the package summary |
| OPTIONAL_RESOURCES | 1003 | Added data to identify references from and to optional package |
| LARGE_WORLD_COORDINATES | 1004 | Large world coordinates converts a number of core types to double components by default. |
| REMOVE_OBJECT_EXPORT_PACKAGE_GUID | 1005 | Remove package GUID from FObjectExport |
| TRACK_OBJECT_EXPORT_IS_INHERITED | 1006 | Add IsInherited to the FObjectExport entry |
| FSOFTOBJECTPATH_REMOVE_ASSET_PATH_FNAMES | 1007 | Replace FName asset path in FSoftObjectPath with (package name, asset name) pair FTopLevelAssetPath |
| ADD_SOFTOBJECTPATH_LIST | 1008 | Add a soft object path list to the package summary for fast remap |
| DATA_RESOURCES | 1009 | Added bulk/data resource table |
| SCRIPT_SERIALIZATION_OFFSET | 1010 | Added script property serialization offset to export table entries for saved, versioned packages |
| PROPERTY_TAG_EXTENSION_AND_OVERRIDABLE_SERIALIZATION | 1011 | Adding property tag extension, Support for overridable serialization on UObject, Support for overridable logic in containers |
| PROPERTY_TAG_COMPLETE_TYPE_NAME | 1012 | Added property tag complete type name and serialization type |
| ASSETREGISTRY_PACKAGEBUILDDEPENDENCIES | 1013 | Changed UE::AssetRegistry::WritePackageData to include PackageBuildDependencies |
| METADATA_SERIALIZATION_OFFSET | 1014 | Added meta data serialization offset to for saved, versioned packages |
| VERSE_CELLS | 1015 | Added VCells to the object graph |
| PACKAGE_SAVED_HASH | 1016 | Changed PackageFileSummary to write FIoHash PackageSavedHash instead of FGuid Guid |
| OS_SUB_OBJECT_SHADOW_SERIALIZATION | 1017 | OS shadow serialization of subobjects |
| IMPORT_TYPE_HIERARCHIES | 1018 | Adds a table of hierarchical type information for imports in a package |
| AUTOMATIC_VERSION_PLUS_ONE | 1019 |  |
| AUTOMATIC_VERSION | 1018 | The newest specified version of UE5. |
