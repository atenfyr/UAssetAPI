# FAnimPhysObjectVersion

Namespace: UAssetAPI.CustomVersions

Custom serialization version for changes made in Dev-AnimPhys stream

```csharp
public enum FAnimPhysObjectVersion
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [FAnimPhysObjectVersion](./uassetapi.customversions.fanimphysobjectversion.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| BeforeCustomVersionWasAdded | 0 | Before any version changes were made |
| ConvertAnimNodeLookAtAxis | 1 | convert animnode look at to use just default axis instead of enum, which doesn't do much |
| BoxSphylElemsUseRotators | 2 | Change FKSphylElem and FKBoxElem to use Rotators not Quats for easier editing |
| ThumbnailSceneInfoAndAssetImportDataAreTransactional | 3 | Change thumbnail scene info and asset import data to be transactional |
| AddedClothingMaskWorkflow | 4 | Enabled clothing masks rather than painting parameters directly |
| RemoveUIDFromSmartNameSerialize | 5 | Remove UID from smart name serialize, it just breaks determinism |
| CreateTargetReference | 6 | Convert FName Socket to FSocketReference and added TargetReference that support bone and socket |
| TuneSoftLimitStiffnessAndDamping | 7 | Tune soft limit stiffness and damping coefficients |
| FixInvalidClothParticleMasses | 8 | Fix possible inf/nans in clothing particle masses |
| CacheClothMeshInfluences | 9 | Moved influence count to cached data |
| SmartNameRefactorForDeterministicCooking | 10 | Remove GUID from Smart Names entirely + remove automatic name fixup |
| RenameDisableAnimCurvesToAllowAnimCurveEvaluation | 11 | rename the variable and allow individual curves to be set |
| AddLODToCurveMetaData | 12 | link curve to LOD, so curve metadata has to include LODIndex |
| FixupBadBlendProfileReferences | 13 | Fixed blend profile references persisting after paste when they aren't compatible |
| AllowMultipleAudioPluginSettings | 14 | Allowing multiple audio plugin settings |
| ChangeRetargetSourceReferenceToSoftObjectPtr | 15 | Change RetargetSource reference to SoftObjectPtr |
| SaveEditorOnlyFullPoseForPoseAsset | 16 | Save editor only full pose for pose asset |
| GeometryCacheAssetDeprecation | 17 | Asset change and cleanup to facilitate new streaming system |
