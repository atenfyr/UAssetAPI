# FEditorObjectVersion

Namespace: UAssetAPI.CustomVersions

Custom serialization version for changes made in Dev-Editor stream.

```csharp
public enum FEditorObjectVersion
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [FEditorObjectVersion](./uassetapi.customversions.feditorobjectversion.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| BeforeCustomVersionWasAdded | 0 | Before any version changes were made |
| GatheredTextProcessVersionFlagging | 1 | Localizable text gathered and stored in packages is now flagged with a localizable text gathering process version |
| GatheredTextPackageCacheFixesV1 | 2 | Fixed several issues with the gathered text cache stored in package headers |
| RootMetaDataSupport | 3 | Added support for "root" meta-data (meta-data not associated with a particular object in a package) |
| GatheredTextPackageCacheFixesV2 | 4 | Fixed issues with how Blueprint bytecode was cached |
| TextFormatArgumentDataIsVariant | 5 | Updated FFormatArgumentData to allow variant data to be marshaled from a BP into C++ |
| SplineComponentCurvesInStruct | 6 | Changes to SplineComponent |
| ComboBoxControllerSupportUpdate | 7 | Updated ComboBox to support toggling the menu open, better controller support |
| RefactorMeshEditorMaterials | 8 | Refactor mesh editor materials |
| AddedFontFaceAssets | 9 | Added UFontFace assets |
| UPropertryForMeshSection | 10 | Add UPROPERTY for TMap of Mesh section, so the serialize will be done normally (and export to text will work correctly) |
| WidgetGraphSchema | 11 | Update the schema of all widget blueprints to use the WidgetGraphSchema |
| AddedBackgroundBlurContentSlot | 12 | Added a specialized content slot to the background blur widget |
| StableUserDefinedEnumDisplayNames | 13 | Updated UserDefinedEnums to have stable keyed display names |
| AddedInlineFontFaceAssets | 14 | Added "Inline" option to UFontFace assets |
| UPropertryForMeshSectionSerialize | 15 | Fix a serialization issue with static mesh FMeshSectionInfoMap FProperty |
| FastWidgetTemplates | 16 | Adding a version bump for the new fast widget construction in case of problems. |
| MaterialThumbnailRenderingChanges | 17 | Update material thumbnails to be more intelligent on default primitive shape for certain material types |
| NewSlateClippingSystem | 18 | Introducing a new clipping system for Slate/UMG |
| MovieSceneMetaDataSerialization | 19 | MovieScene Meta Data added as native Serialization |
| GatheredTextEditorOnlyPackageLocId | 20 | Text gathered from properties now adds two variants: a version without the package localization ID (for use at runtime), and a version with it (which is editor-only) |
| AddedAlwaysSignNumberFormattingOption | 21 | Added AlwaysSign to FNumberFormattingOptions |
| AddedMaterialSharedInputs | 22 | Added additional objects that must be serialized as part of this new material feature |
| AddedMorphTargetSectionIndices | 23 | Added morph target section indices |
| SerializeInstancedStaticMeshRenderData | 24 | Serialize the instanced static mesh render data, to avoid building it at runtime |
| MeshDescriptionNewSerialization_MovedToRelease | 25 | Change to MeshDescription serialization (moved to release) |
| MeshDescriptionNewAttributeFormat | 26 | New format for mesh description attributes |
| ChangeSceneCaptureRootComponent | 27 | Switch root component of SceneCapture actors from MeshComponent to SceneComponent |
| StaticMeshDeprecatedRawMesh | 28 | StaticMesh serializes MeshDescription instead of RawMesh |
| MeshDescriptionBulkDataGuid | 29 | MeshDescriptionBulkData contains a Guid used as a DDC key |
| MeshDescriptionRemovedHoles | 30 | Change to MeshDescription serialization (removed FMeshPolygon::HoleContours) |
| ChangedWidgetComponentWindowVisibilityDefault | 31 | Change to the WidgetCompoent WindowVisibilty default value |
| CultureInvariantTextSerializationKeyStability | 32 | Avoid keying culture invariant display strings during serialization to avoid non-deterministic cooking issues |
| ScrollBarThicknessChange | 33 | Change to UScrollBar and UScrollBox thickness property (removed implicit padding of 2, so thickness value must be incremented by 4). |
| RemoveLandscapeHoleMaterial | 34 | Deprecated LandscapeHoleMaterial |
| MeshDescriptionTriangles | 35 | MeshDescription defined by triangles instead of arbitrary polygons |
| ComputeWeightedNormals | 36 | Add weighted area and angle when computing the normals |
| SkeletalMeshBuildRefactor | 37 | SkeletalMesh now can be rebuild in editor, no more need to re-import |
| SkeletalMeshMoveEditorSourceDataToPrivateAsset | 38 | Move all SkeletalMesh source data into a private uasset in the same package has the skeletalmesh |
| NumberParsingOptionsNumberLimitsAndClamping | 39 | Parse text only if the number is inside the limits of its type |
| SkeletalMeshSourceDataSupport16bitOfMaterialNumber | 40 | Make sure we can have more then 255 material in the skeletal mesh source data |
