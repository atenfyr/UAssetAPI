# FFrameworkObjectVersion

Namespace: UAssetAPI.CustomVersions

Custom serialization version for changes made in Dev-Framework stream.

```csharp
public enum FFrameworkObjectVersion
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [FFrameworkObjectVersion](./uassetapi.customversions.fframeworkobjectversion.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| BeforeCustomVersionWasAdded | 0 | Before any version changes were made |
| UseBodySetupCollisionProfile | 1 | BodySetup's default instance collision profile is used by default when creating a new instance. |
| AnimBlueprintSubgraphFix | 2 | Regenerate subgraph arrays correctly in animation blueprints to remove duplicates and add missing graphs that appear read only when edited |
| MeshSocketScaleUtilization | 3 | Static and skeletal mesh sockets now use the specified scale |
| ExplicitAttachmentRules | 4 | Attachment rules are now explicit in how they affect location, rotation and scale |
| MoveCompressedAnimDataToTheDDC | 5 | Moved compressed anim data from uasset to the DDC |
| FixNonTransactionalPins | 6 | Some graph pins created using legacy code seem to have lost the RF_Transactional flag, which causes issues with undo. Restore the flag at this version |
| SmartNameRefactor | 7 | Create new struct for SmartName, and use that for CurveName |
| AddSourceReferenceSkeletonToRig | 8 | Add Reference Skeleton to Rig |
| ConstraintInstanceBehaviorParameters | 9 | Refactor ConstraintInstance so that we have an easy way to swap behavior paramters |
| PoseAssetSupportPerBoneMask | 10 | Pose Asset support mask per bone |
| PhysAssetUseSkeletalBodySetup | 11 | Physics Assets now use SkeletalBodySetup instead of BodySetup |
| RemoveSoundWaveCompressionName | 12 | Remove SoundWave CompressionName |
| AddInternalClothingGraphicalSkinning | 13 | Switched render data for clothing over to unreal data, reskinned to the simulation mesh |
| WheelOffsetIsFromWheel | 14 | Wheel force offset is now applied at the wheel instead of vehicle COM |
| MoveCurveTypesToSkeleton | 15 | Move curve metadata to be saved in skeleton. Individual asset still saves some flag - i.e. disabled curve and editable or not, but major flag - i.e. material types - moves to skeleton and handle in one place |
| CacheDestructibleOverlaps | 16 | Cache destructible overlaps on save |
| GeometryCacheMissingMaterials | 17 | Added serialization of materials applied to geometry cache objects |
| LODsUseResolutionIndependentScreenSize | 18 | Switch static and skeletal meshes to calculate LODs based on resolution-independent screen size |
| BlendSpacePostLoadSnapToGrid | 19 | Blend space post load verification |
| SupportBlendSpaceRateScale | 20 | Addition of rate scales to blend space samples |
| LODHysteresisUseResolutionIndependentScreenSize | 21 | LOD hysteresis also needs conversion from the LODsUseResolutionIndependentScreenSize version |
| ChangeAudioComponentOverrideSubtitlePriorityDefault | 22 | AudioComponent override subtitle priority default change |
| HardSoundReferences | 23 | Serialize hard references to sound files when possible |
| EnforceConstInAnimBlueprintFunctionGraphs | 24 | Enforce const correctness in Animation Blueprint function graphs |
| InputKeySelectorTextStyle | 25 | Upgrade the InputKeySelector to use a text style |
| EdGraphPinContainerType | 26 | Represent a pins container type as an enum not 3 independent booleans |
| ChangeAssetPinsToString | 27 | Switch asset pins to store as string instead of hard object reference |
| LocalVariablesBlueprintVisible | 28 | Fix Local Variables so that the properties are correctly flagged as blueprint visible |
| RemoveUField_Next | 29 | Stopped serializing UField_Next so that UFunctions could be serialized in dependently of a UClass in order to allow us to do all UFunction loading in a single pass (after classes and CDOs are created) |
| UserDefinedStructsBlueprintVisible | 30 | Fix User Defined structs so that all members are correct flagged blueprint visible |
| PinsStoreFName | 31 | FMaterialInput and FEdGraphPin store their name as FName instead of FString |
| UserDefinedStructsStoreDefaultInstance | 32 | User defined structs store their default instance, which is used for initializing instances |
| FunctionTerminatorNodesUseMemberReference | 33 | Function terminator nodes serialize an FMemberReference rather than a name/class pair |
| EditableEventsUseConstRefParameters | 34 | Custom event and non-native interface event implementations add 'const' to reference parameters |
| BlueprintGeneratedClassIsAlwaysAuthoritative | 35 | No longer serialize the legacy flag that indicates this state, as it is now implied since we don't serialize the skeleton CDO |
| EnforceBlueprintFunctionVisibility | 36 | Enforce visibility of blueprint functions - e.g. raise an error if calling a private function from another blueprint: |
| StoringUCSSerializationIndex | 37 | ActorComponents now store their serialization index |
