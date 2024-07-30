# FUE5ReleaseStreamObjectVersion

Namespace: UAssetAPI.CustomVersions

```csharp
public enum FUE5ReleaseStreamObjectVersion
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [FUE5ReleaseStreamObjectVersion](./uassetapi.customversions.fue5releasestreamobjectversion.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| BeforeCustomVersionWasAdded | 0 | Before any version changes were made |
| ReflectionMethodEnum | 1 | Added Lumen reflections to new reflection enum, changed defaults |
| WorldPartitionActorDescSerializeHLODInfo | 2 | Serialize HLOD info in WorldPartitionActorDesc |
| RemovingTessellation | 3 | Removing Tessellation from materials and meshes. |
| LevelInstanceSerializeRuntimeBehavior | 4 | LevelInstance serialize runtime behavior |
| PoseAssetRuntimeRefactor | 5 | Refactoring Pose Asset runtime data structures |
| WorldPartitionActorDescSerializeActorFolderPath | 6 | Serialize the folder path of actor descs |
| HairStrandsVertexFormatChange | 7 | Change hair strands vertex format |
| AddChaosMaxLinearAngularSpeed | 8 | Added max linear and angular speed to Chaos bodies |
| PackedLevelInstanceVersion | 9 | PackedLevelInstance version |
| PackedLevelInstanceBoundsFix | 10 | PackedLevelInstance bounds fix |
| CustomPropertyAnimGraphNodesUseOptionalPinManager | 11 | Custom property anim graph nodes (linked anim graphs, control rig etc.) now use optional pin manager |
| TextFormatArgumentData64bitSupport | 12 | Add native double and int64 support to FFormatArgumentData |
| MaterialLayerStacksAreNotParameters | 13 | Material layer stacks are no longer considered 'static parameters' |
| MaterialInterfaceSavedCachedData | 14 | CachedExpressionData is moved from UMaterial to UMaterialInterface |
| AddClothMappingLODBias | 15 | Add support for multiple cloth deformer LODs to be able to raytrace cloth with a different LOD than the one it is rendered with |
| AddLevelActorPackagingScheme | 16 | Add support for different external actor packaging schemes |
| WorldPartitionActorDescSerializeAttachParent | 17 | Add support for linking to the attached parent actor in WorldPartitionActorDesc |
| ConvertedActorGridPlacementToSpatiallyLoadedFlag | 18 | Converted AActor GridPlacement to bIsSpatiallyLoaded flag |
| ActorGridPlacementDeprecateDefaultValueFixup | 19 | Fixup for bad default value for GridPlacement_DEPRECATED |
| PackedLevelActorUseWorldPartitionActorDesc | 20 | PackedLevelActor started using FWorldPartitionActorDesc (not currently checked against but added as a security) |
| AddLevelActorFolders | 21 | Add support for actor folder objects |
| RemoveSkeletalMeshLODModelBulkDatas | 22 | Remove FSkeletalMeshLODModel bulk datas |
| ExcludeBrightnessFromEncodedHDRCubemap | 23 | Exclude brightness from the EncodedHDRCubemap, |
| VolumetricCloudSampleCountUnification | 24 | Unified volumetric cloud component quality sample count slider between main and reflection views for consistency |
| PoseAssetRawDataGUID | 25 | Pose asset GUID generated from source AnimationSequence |
| ConvolutionBloomIntensity | 26 | Convolution bloom now take into account FPostProcessSettings::BloomIntensity for scatter dispersion. |
| WorldPartitionHLODActorDescSerializeHLODSubActors | 27 | Serialize FHLODSubActors instead of FGuids in WorldPartition HLODActorDesc |
| LargeWorldCoordinates | 28 | Large Worlds - serialize double types as doubles |
| BlueprintPinsUseRealNumbers | 29 | Deserialize old BP float and double types as real numbers for pins |
| UpdatedDirectionalLightShadowDefaults | 30 | Changed shadow defaults for directional light components, version needed to not affect old things |
| GeometryCollectionConvexDefaults | 31 | Refresh geometry collections that had not already generated convex bodies. |
| ChaosClothFasterDamping | 32 | Add faster damping calculations to the cloth simulation and rename previous Damping parameter to LocalDamping. |
| WorldPartitionLandscapeActorDescSerializeLandscapeActorGuid | 33 | Serialize LandscapeActorGuid in FLandscapeActorDesc sub class. |
| AddedInertiaTensorAndRotationOfMassAddedToConvex | 34 | add inertia tensor and rotation of mass to convex |
| ChaosInertiaConvertedToVec3 | 35 | Storing inertia tensor as vec3 instead of matrix. |
| SerializeFloatPinDefaultValuesAsSinglePrecision | 36 | For Blueprint real numbers, ensure that legacy float data is serialized as single-precision |
| AnimLayeredBoneBlendMasks | 37 | Upgrade the BlendMasks array in existing LayeredBoneBlend nodes |
| StoreReflectionCaptureEncodedHDRDataInRG11B10Format | 38 | Uses RG11B10 format to store the encoded reflection capture data on mobile |
| RawAnimSequenceTrackSerializer | 39 | Add WithSerializer type trait and implementation for FRawAnimSequenceTrack |
| RemoveDuplicatedStyleInfo | 40 | Removed font from FEditableTextBoxStyle, and added FTextBlockStyle instead. |
| LinkedAnimGraphMemberReference | 41 | Added member reference to linked anim graphs |
| DynamicMeshComponentsDefaultUseExternalTangents | 42 | Changed default tangent behavior for new dynamic mesh components |
| MediaCaptureNewResizeMethods | 43 | Added resize methods to media capture |
| RigVMSaveDebugMapInGraphFunctionData | 44 | Function data stores a map from work to debug operands |
| LocalExposureDefaultChangeFrom1 | 45 | Changed default Local Exposure Contrast Scale from 1.0 to 0.8 |
| WorldPartitionActorDescSerializeActorIsListedInSceneOutliner | 46 | Serialize bActorIsListedInSceneOutliner in WorldPartitionActorDesc |
| OpenColorIODisabledDisplayConfigurationDefault | 47 | Disabled opencolorio display configuration by default |
| WorldPartitionExternalDataLayers | 48 | Serialize ExternalDataLayerAsset in WorldPartitionActorDesc |
| ChaosClothFictitiousAngularVelocitySubframeFix | 49 | Fix Chaos Cloth fictitious angular scale bug that requires existing parameter rescaling. |
| SinglePrecisonParticleDataPT | 50 | Store physics thread particles data in single precision |
| OrthographicAutoNearFarPlane | 51 | Orthographic Near and Far Plane Auto-resolve enabled by default |
| VersionPlusOne | 52 | -----new versions can be added above this line------------------------------------------------- |
