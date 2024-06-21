# FAssetRegistryVersion

Namespace: UAssetAPI.CustomVersions

Version used for serializing asset registry caches, both runtime and editor

```csharp
public enum FAssetRegistryVersion
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [FAssetRegistryVersion](./uassetapi.customversions.fassetregistryversion.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| PreVersioning | 0 | From before file versioning was implemented |
| HardSoftDependencies | 1 | The first version of the runtime asset registry to include file versioning. |
| AddAssetRegistryState | 2 | Added FAssetRegistryState and support for piecemeal serialization |
| ChangedAssetData | 3 | AssetData serialization format changed, versions before this are not readable |
| RemovedMD5Hash | 4 | Removed MD5 hash from package data |
| AddedHardManage | 5 | Added hard/soft manage references |
| AddedCookedMD5Hash | 6 | Added MD5 hash of cooked package to package data |
| AddedDependencyFlags | 7 | Added UE::AssetRegistry::EDependencyProperty to each dependency |
| FixedTags | 8 | Major tag format change that replaces USE_COMPACT_ASSET_REGISTRY: |
| WorkspaceDomain | 9 | Added Version information to AssetPackageData |
| PackageImportedClasses | 10 | Added ImportedClasses to AssetPackageData |
| PackageFileSummaryVersionChange | 11 | A new version number of UE5 was added to FPackageFileSummary |
| ObjectResourceOptionalVersionChange | 12 | Change to linker export/import resource serializationn |
| AddedChunkHashes | 13 | Added FIoHash for each FIoChunkId in the package to the AssetPackageData. |
| ClassPaths | 14 | Classes are serialized as path names rather than short object names, e.g. /Script/Engine.StaticMesh |
| RemoveAssetPathFNames | 15 | Asset bundles are serialized as FTopLevelAssetPath instead of FSoftObjectPath, deprecated FAssetData::ObjectPath |
| AddedHeader | 16 | Added header with bFilterEditorOnlyData flag |
| AssetPackageDataHasExtension | 17 | Added Extension to AssetPackageData. |
