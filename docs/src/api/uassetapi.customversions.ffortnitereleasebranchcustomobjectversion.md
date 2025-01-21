# FFortniteReleaseBranchCustomObjectVersion

Namespace: UAssetAPI.CustomVersions

```csharp
public enum FFortniteReleaseBranchCustomObjectVersion
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [FFortniteReleaseBranchCustomObjectVersion](./uassetapi.customversions.ffortnitereleasebranchcustomobjectversion.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| BeforeCustomVersionWasAdded | 0 | Before any version changes were made |
| DisableLevelset_v14_10 | 1 | Custom 14.10 File Object Version |
| ChaosClothAddTethersToCachedData | 2 | Add the long range attachment tethers to the cloth asset to avoid a large hitch during the cloth's initialization. |
| ChaosKinematicTargetRemoveScale | 3 | Chaos::TKinematicTarget no longer stores a full transform, only position/rotation. |
| ActorComponentUCSModifiedPropertiesSparseStorage | 4 | Move UCSModifiedProperties out of ActorComponent and in to sparse storage |
| FixupNaniteLandscapeMeshes | 5 | Fixup Nanite meshes which were using the wrong material and didn't have proper UVs : |
| RemoveUselessLandscapeMeshesCookedCollisionData | 6 | Remove any cooked collision data from nanite landscape / editor spline meshes since collisions are not needed there : |
| SerializeAnimCurveCompressionCodecGuidOnCook | 7 | Serialize out UAnimCurveCompressionCodec::InstanceGUID to maintain deterministic DDC key generation in cooked-editor |
| FixNaniteLandscapeMeshNames | 8 | Fix the Nanite landscape mesh being reused because of a bad name |
| LandscapeSharedPropertiesEnforcement | 9 | Fixup and synchronize shared properties modified before the synchronicity enforcement |
| WorldPartitionRuntimeCellGuidWithCellSize | 10 | Include the cell size when computing the cell guid |
| NaniteMaterialOverrideUsesEditorOnly | 11 | Enable SkipOnlyEditorOnly style cooking of NaniteOverrideMaterial |
| SinglePrecisonParticleData | 12 | Store game thread particles data in single precision |
| PCGPointStructuredSerializer | 13 | UPCGPoint custom serialization |
| NavMovementComponentMovingPropertiesToStruct | 14 | Deprecation of Nav Movement Properties and moving them to a new struct |
| DynamicMeshAttributesSerializeBones | 15 | Add bone serialization for dynamic mesh attributes |
| VersionPlusOne | 16 | -----new versions can be added above this line------------------------------------------------- |
