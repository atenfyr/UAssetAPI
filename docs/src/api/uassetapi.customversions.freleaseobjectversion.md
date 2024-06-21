# FReleaseObjectVersion

Namespace: UAssetAPI.CustomVersions

Custom serialization version for changes made in Release streams.

```csharp
public enum FReleaseObjectVersion
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [FReleaseObjectVersion](./uassetapi.customversions.freleaseobjectversion.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| BeforeCustomVersionWasAdded | 0 | Before any version changes were made |
| StaticMeshExtendedBoundsFix | 1 | Static Mesh extended bounds radius fix |
| NoSyncAsyncPhysAsset | 2 | Physics asset bodies are either in the sync scene or the async scene, but not both |
| LevelTransArrayConvertedToTArray | 3 | ULevel was using TTransArray incorrectly (serializing the entire array in addition to individual mutations). converted to a TArray |
| AddComponentNodeTemplateUniqueNames | 4 | Add Component node templates now use their own unique naming scheme to ensure more reliable archetype lookups. |
| UPropertryForMeshSectionSerialize | 5 | Fix a serialization issue with static mesh FMeshSectionInfoMap FProperty |
| ConvertHLODScreenSize | 6 | Existing HLOD settings screen size to screen area conversion |
| SpeedTreeBillboardSectionInfoFixup | 7 | Adding mesh section info data for existing billboard LOD models |
| EventSectionParameterStringAssetRef | 8 | Change FMovieSceneEventParameters::StructType to be a string asset reference from a TWeakObjectPtr UScriptStruct |
| SkyLightRemoveMobileIrradianceMap | 9 | Remove serialized irradiance map data from skylight. |
| RenameNoTwistToAllowTwistInTwoBoneIK | 10 | rename bNoTwist to bAllowTwist |
| MaterialLayersParameterSerializationRefactor | 11 | Material layers serialization refactor |
| AddSkeletalMeshSectionDisable | 12 | Added disable flag to skeletal mesh data |
| RemovedMaterialSharedInputCollection | 13 | Removed objects that were serialized as part of this material feature |
| HISMCClusterTreeMigration | 14 | HISMC Cluster Tree migration to add new data |
| PinDefaultValuesVerified | 15 | Default values on pins in blueprints could be saved incoherently |
| FixBrokenStateMachineReferencesInTransitionGetters | 16 | During copy and paste transition getters could end up with broken state machine references |
| MeshDescriptionNewSerialization | 17 | Change to MeshDescription serialization |
| UnclampRGBColorCurves | 18 | Change to not clamp RGB values &gt; 1 on linear color curves |
| LinkTimeAnimBlueprintRootDiscoveryBugFix | 19 | BugFix for FAnimObjectVersion::LinkTimeAnimBlueprintRootDiscovery. |
| TrailNodeBlendVariableNameChange | 20 | Change trail anim node variable deprecation |
| PropertiesSerializeRepCondition | 21 | Make sure the Blueprint Replicated Property Conditions are actually serialized properly. |
| FocalDistanceDisablesDOF | 22 | DepthOfFieldFocalDistance at 0 now disables DOF instead of DepthOfFieldFstop at 0. |
| Unused_SoundClass2DReverbSend | 23 | Removed versioning, but version entry must still exist to keep assets saved with this version loadable |
| GroomAssetVersion1 | 24 | Groom asset version |
| GroomAssetVersion2 | 25 | Groom asset version |
| SerializeAnimModifierState | 26 | Store applied version of Animation Modifier to use when reverting |
| GroomAssetVersion3 | 27 | Groom asset version |
| DeprecateFilmbackSettings | 28 | Upgrade filmback |
| CustomImplicitCollisionType | 29 | custom collision type |
| FFieldPathOwnerSerialization | 30 | FFieldPath will serialize the owner struct reference and only a short path to its property |
| ReleaseUE4VersionFixup | 31 | Dummy version to allow us to Fix up the fact that ReleaseObjectVersion was changed elsewhere |
| PinTypeIncludesUObjectWrapperFlag | 32 | Pin types include a flag that propagates the 'CPF_UObjectWrapper' flag to generated properties |
| WeightFMeshToMeshVertData | 33 | Added Weight member to FMeshToMeshVertData |
| AnimationGraphNodeBindingsDisplayedAsPins | 34 | Animation graph node bindings displayed as pins |
| SerializeRigVMOffsetSegmentPaths | 35 | Serialized rigvm offset segment paths |
| AbcVelocitiesSupport | 36 | Upgrade AbcGeomCacheImportSettings for velocities |
| MarginAddedToConvexAndBox | 37 | Add margin support to Chaos Convex |
| StructureDataAddedToConvex | 38 | Add structure data to Chaos Convex |
| AddedFrontRightUpAxesToLiveLinkPreProcessor | 39 | Changed axis UI for LiveLink AxisSwitch Pre Processor |
| FixupCopiedEventSections | 40 | Some sequencer event sections that were copy-pasted left broken links to the director BP |
| RemoteControlSerializeFunctionArgumentsSize | 41 | Serialize the number of bytes written when serializing function arguments |
| AddedSubSequenceEntryWarpCounter | 42 | Add loop counters to sequencer's compiled sub-sequence data |
| LonglatTextureCubeDefaultMaxResolution | 43 | Remove default resolution limit of 512 pixels for cubemaps generated from long-lat sources |
