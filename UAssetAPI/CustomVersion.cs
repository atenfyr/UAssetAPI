using System;
using System.Collections.Generic;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI
{
    /// <summary>
    /// A custom version. Controls more specific serialization than the main engine object version does.
    /// </summary>
    public class CustomVersion
    {
        /// <summary>
        /// Static map of custom version GUIDs to the object or enum that they represent in the Unreal Engine. This list is not necessarily exhaustive, so feel free to add to it if need be.
        /// </summary>
        public static readonly Dictionary<Guid, string> GuidToCustomVersionStringMap = new Dictionary<Guid, string>()
        {
            { UnusedCustomVersionKey, "UnusedCustomVersionKey" },
            { UAPUtils.GUID(0xB0D832E4, 0x1F894F0D, 0xACCF7EB7, 0x36FD4AA2), "FBlueprintsObjectVersion" },
            { UAPUtils.GUID(0xE1C64328, 0xA22C4D53, 0xA36C8E86, 0x6417BD8C), "FBuildObjectVersion" },
            { UAPUtils.GUID(0x375EC13C, 0x06E448FB, 0xB50084F0, 0x262A717E), "FCoreObjectVersion" },
            { UAPUtils.GUID(0xE4B068ED, 0xF49442E9, 0xA231DA0B, 0x2E46BB41), "FEditorObjectVersion" },
            { UAPUtils.GUID(0xCFFC743F, 0x43B04480, 0x939114DF, 0x171D2073), "FFrameworkObjectVersion" },
            { UAPUtils.GUID(0xB02B49B5, 0xBB2044E9, 0xA30432B7, 0x52E40360), "FMobileObjectVersion" },
            { UAPUtils.GUID(0xA4E4105C, 0x59A149B5, 0xA7C540C4, 0x547EDFEE), "FNetworkingObjectVersion" },
            { UAPUtils.GUID(0x39C831C9, 0x5AE647DC, 0x9A449C17, 0x3E1C8E7C), "FOnlineObjectVersion" },
            { UAPUtils.GUID(0x78F01B33, 0xEBEA4F98, 0xB9B484EA, 0xCCB95AA2), "FPhysicsObjectVersion" },
            { UAPUtils.GUID(0x6631380F, 0x2D4D43E0, 0x8009CF27, 0x6956A95A), "FPlatformObjectVersion" },
            { UAPUtils.GUID(0x12F88B9F, 0x88754AFC, 0xA67CD90C, 0x383ABD29), "FRenderingObjectVersion" },
            { UAPUtils.GUID(0x7B5AE74C, 0xD2704C10, 0xA9585798, 0x0B212A5A), "FSequencerObjectVersion" },
            { UAPUtils.GUID(0xD7296918, 0x1DD64BDD, 0x9DE264A8, 0x3CC13884), "FVRObjectVersion" },
            { UAPUtils.GUID(0xC2A15278, 0xBFE74AFE, 0x6C1790FF, 0x531DF755), "FLoadTimesObjectVersion" },
            { UAPUtils.GUID(0x6EACA3D4, 0x40EC4CC1, 0xb7868BED, 0x9428FC5), "FGeometryObjectVersion" },
            { UAPUtils.GUID(0x29E575DD, 0xE0A34627, 0x9D10D276, 0x232CDCEA), "FAnimPhysObjectVersion" },
            { UAPUtils.GUID(0xAF43A65D, 0x7FD34947, 0x98733E8E, 0xD9C1BB05), "FAnimObjectVersion" },
            { UAPUtils.GUID(0x6B266CEC, 0x1EC74B8F, 0xA30BE4D9, 0x0942FC07), "FReflectionCaptureObjectVersion" },
            { UAPUtils.GUID(0x0DF73D61, 0xA23F47EA, 0xB72789E9, 0x0C41499A), "FAutomationObjectVersion" },
            { UAPUtils.GUID(0x601D1886, 0xAC644F84, 0xAA16D3DE, 0x0DEAC7D6), "FFortniteMainBranchObjectVersion" },
            { UAPUtils.GUID(0x9DFFBCD6, 0x494F0158, 0xE2211282, 0x3C92A888), "FEnterpriseObjectVersion" },
            { UAPUtils.GUID(0xF2AED0AC, 0x9AFE416F, 0x8664AA7F, 0xFA26D6FC), "FNiagaraObjectVersion" },
            { UAPUtils.GUID(0x174F1F0B, 0xB4C645A5, 0xB13F2EE8, 0xD0FB917D), "FDestructionObjectVersion" },
            { UAPUtils.GUID(0x35F94A83, 0xE258406C, 0xA31809F5, 0x9610247C), "FExternalPhysicsCustomObjectVersion" },
            { UAPUtils.GUID(0xB68FC16E, 0x8B1B42E2, 0xB453215C, 0x058844FE), "FExternalPhysicsMaterialCustomObjectVersion" },
            { UAPUtils.GUID(0xB2E18506, 0x4273CFC2, 0xA54EF4BB, 0x758BBA07), "FCineCameraObjectVersion" },
            { UAPUtils.GUID(0x64F58936, 0xFD1B42BA, 0xBA967289, 0xD5D0FA4E), "FVirtualProductionObjectVersion" },
            { UAPUtils.GUID(0x6f0ed827, 0xa6094895, 0x9c91998d, 0x90180ea4), "FMediaFrameworkObjectVersion" },
            { UAPUtils.GUID(0xAFE08691, 0x3A0D4952, 0xB673673B, 0x7CF22D1E), "FPoseDriverCustomVersion" },
            { UAPUtils.GUID(0xCB8AB0CD, 0xE78C4BDE, 0xA8621393, 0x14E9EF62), "FTempCustomVersion" },
            { UAPUtils.GUID(0x2EB5FDBD, 0x01AC4D10, 0x8136F38F, 0x3393A5DA), "FAnimationCustomVersion" },
            { UAPUtils.GUID(0x717F9EE7, 0xE9B0493A, 0x88B39132, 0x1B388107), "FAssetRegistryVersion" },
            { UAPUtils.GUID(0xFB680AF2, 0x59EF4BA3, 0xBAA819B5, 0x73C8443D), "FClothingAssetCustomVersion" },
            { UAPUtils.GUID(0x9C54D522, 0xA8264FBE, 0x94210746, 0x61B482D0), "FReleaseObjectVersion" },
            { UAPUtils.GUID(0x4A56EB40, 0x10F511DC, 0x92D3347E, 0xB2C96AE7), "FParticleSystemCustomVersion" },
            { UAPUtils.GUID(0xD78A4A00, 0xE8584697, 0xBAA819B5, 0x487D46B4), "FSkeletalMeshCustomVersion" },
            { UAPUtils.GUID(0x5579F886, 0x933A4C1F, 0x83BA087B, 0x6361B92F), "FRecomputeTangentCustomVersion" },
            { UAPUtils.GUID(0x612FBE52, 0xDA53400B, 0x910D4F91, 0x9FB1857C), "FOverlappingVerticesCustomVersion" },
            { UAPUtils.GUID(0x430C4D19, 0x71544970, 0x87699B69, 0xDF90B0E5), "FFoliageCustomVersion" },
            { UAPUtils.GUID(0xaafe32bd, 0x53954c14, 0xb66a5e25, 0x1032d1dd), "FProceduralFoliageCustomVersion" },
            { UAPUtils.GUID(0xab965196, 0x45d808fc, 0xb7d7228d, 0x78ad569e), "FLiveLinkCustomVersion" },
            // etc.
        };

        /// <summary>
        /// A GUID that represents an unused custom version.
        /// </summary>
        public static readonly Guid UnusedCustomVersionKey = UAPUtils.GUID(0, 0, 0, 0xF99D40C1);

        /// <summary>
        /// Returns the name of the object or enum that a custom version GUID represents, as specified in <see cref="GuidToCustomVersionStringMap"/>.
        /// </summary>
        /// <param name="guid">A GUID that represents a custom version.</param>
        /// <returns>A string that represents the friendly name of the corresponding custom version.</returns>
        public static string GetCustomVersionFriendlyNameFromGuid(Guid guid)
        {
            return GuidToCustomVersionStringMap.ContainsKey(guid) ? GuidToCustomVersionStringMap[guid] : null;
        }

        /// <summary>
        /// Returns the GUID of the custom version that the object or enum name provided represents.
        /// </summary>
        /// <param name="friendlyName">The name of a custom version object or enum.</param>
        /// <returns>A GUID that represents the custom version</returns>
        public static Guid GetCustomVersionGuidFromFriendlyName(string friendlyName)
        {
            foreach (KeyValuePair<Guid, string> entry in GuidToCustomVersionStringMap)
            {
                if (entry.Value == friendlyName) return entry.Key;
            }
            return UnusedCustomVersionKey;
        }

        public Guid Key;
        public string FriendlyName = null;
        public int Version;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomVersion"/> class given an object or enum name and a version number.
        /// </summary>
        /// <param name="friendlyName">The friendly name to use when initializing this custom version.</param>
        /// <param name="version">The version number to use when initializing this custom version.</param>
        public CustomVersion(string friendlyName, int version)
        {
            Key = GetCustomVersionGuidFromFriendlyName(friendlyName);
            FriendlyName = friendlyName;
            Version = version;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomVersion"/> class given a custom version GUID and a version number.
        /// </summary>
        /// <param name="key">The GUID to use when initializing this custom version.</param>
        /// <param name="version">The version number to use when initializing this custom version.</param>
        public CustomVersion(Guid key, int version)
        {
            Key = key;
            if (GuidToCustomVersionStringMap.ContainsKey(key)) FriendlyName = GuidToCustomVersionStringMap[key];
            Version = version;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomVersion"/> class.
        /// </summary>
        public CustomVersion()
        {
            Key = UnusedCustomVersionKey;
            Version = 0;
        }
    }

    /* Below are some enums of custom versions we need for serialization */

    /// <summary>
    /// Represents the engine version at the time that a custom version was implemented.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class IntroducedAttribute : Attribute
    {
        public UE4Version IntroducedVersion;

        public IntroducedAttribute(UE4Version introducedVersion)
        {
            IntroducedVersion = introducedVersion;
        }
    }

    /// <summary>
    /// Custom serialization version for changes made in the //Fortnite/Main stream.
    /// </summary>
    public enum FFortniteMainBranchObjectVersion
    {
        /// <summary>Before any version changes were made</summary>
        [Introduced(UE4Version.VER_UE4_OLDEST_LOADABLE_PACKAGE)]
        BeforeCustomVersionWasAdded = 0,

        /// <summary>World composition tile offset changed from 2d to 3d</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        WorldCompositionTile3DOffset,

        /// <summary>Minor material serialization optimization</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        MaterialInstanceSerializeOptimization_ShaderFName,

        /// <summary>Refactored cull distances to account for HLOD, explicit override and globals in priority</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        CullDistanceRefactor_RemovedDefaultDistance,
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        CullDistanceRefactor_NeverCullHLODsByDefault,
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        CullDistanceRefactor_NeverCullALODActorsByDefault,

        /// <summary>Support to remove morphtarget generated by bRemapMorphtarget</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        SaveGeneratedMorphTargetByEngine,

        /// <summary>Convert reduction setting options</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        ConvertReductionSettingOptions,

        /// <summary>Serialize the type of blending used for landscape layer weight static params</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        StaticParameterTerrainLayerWeightBlendType,

        /// <summary>Fix up None Named animation curve names,</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        FixUpNoneNameAnimationCurves,

        /// <summary>Ensure ActiveBoneIndices to have parents even not skinned for old assets</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        EnsureActiveBoneIndicesToContainParents,

        /// <summary>Serialize the instanced static mesh render data, to avoid building it at runtime</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        SerializeInstancedStaticMeshRenderData,

        /// <summary>Cache material quality node usage</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        CachedMaterialQualityNodeUsage,

        /// <summary>Font outlines no longer apply to drop shadows for new objects but we maintain the opposite way for backwards compat</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        FontOutlineDropShadowFixup,

        /// <summary>New skeletal mesh import workflow (Geometry only or animation only re-import )</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        NewSkeletalMeshImporterWorkflow,

        /// <summary>Migrate data from previous data structure to new one to support materials per LOD on the Landscape</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        NewLandscapeMaterialPerLOD,

        /// <summary>New Pose Asset data type</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        RemoveUnnecessaryTracksFromPose,

        /// <summary>Migrate Foliage TLazyObjectPtr to TSoftObjectPtr</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        FoliageLazyObjPtrToSoftObjPtr,

        /// <summary>TimelineTemplates store their derived names instead of dynamically generating. This code tied to this version was reverted and redone at a later date</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        REVERTED_StoreTimelineNamesInTemplate,

        /// <summary>Added BakePoseOverride for LOD setting</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        AddBakePoseOverrideForSkeletalMeshReductionSetting,

        /// <summary>TimelineTemplates store their derived names instead of dynamically generating</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        StoreTimelineNamesInTemplate,

        /// <summary>New Pose Asset data type</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        WidgetStopDuplicatingAnimations,

        /// <summary>Allow reducing of the base LOD, we need to store some imported model data so we can reduce again from the same data.</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        AllowSkeletalMeshToReduceTheBaseLOD,

        /// <summary>Curve Table size reduction</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        ShrinkCurveTableSize,

        /// <summary>Widgets upgraded with WidgetStopDuplicatingAnimations, may not correctly default-to-self for the widget parameter.</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        WidgetAnimationDefaultToSelfFail,

        /// <summary>HUDWidgets now require an element tag</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        FortHUDElementNowRequiresTag,

        /// <summary>Animation saved as bulk data when cooked</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        FortMappedCookedAnimation,

        /// <summary>Support Virtual Bone in Retarget Manager</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        SupportVirtualBoneInRetargeting,

        /// <summary>Fixup bad defaults in water metadata</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        FixUpWaterMetadata,

        /// <summary>Move the location of water metadata</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        MoveWaterMetadataToActor,

        /// <summary>Replaced lake collision component</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        ReplaceLakeCollision,

        /// <summary>Anim layer node names are now conformed by Guid</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        AnimLayerGuidConformation,

        /// <summary>Ocean collision component has become dynamic</summary>
        [Introduced(UE4Version.VER_UE4_SKINWEIGHT_PROFILE_DATA_LAYOUT_CHANGES)]
        MakeOceanCollisionTransient,

        /// <summary>FFieldPath will serialize the owner struct reference and only a short path to its property</summary>
        [Introduced(UE4Version.VER_UE4_SKINWEIGHT_PROFILE_DATA_LAYOUT_CHANGES)]
        FFieldPathOwnerSerialization,

        /// <summary>Simplified WaterBody post process material handling</summary>
        [Introduced(UE4Version.VER_UE4_SKINWEIGHT_PROFILE_DATA_LAYOUT_CHANGES)]
        FixUpUnderwaterPostProcessMaterial,

        /// <summary>A single water exclusion volume can now exclude N water bodies</summary>
        [Introduced(UE4Version.VER_UE4_SKINWEIGHT_PROFILE_DATA_LAYOUT_CHANGES)]
        SupportMultipleWaterBodiesPerExclusionVolume,

        /// <summary>Serialize rigvm operators one by one instead of the full byte code array to ensure determinism</summary>
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        RigVMByteCodeDeterminism,

        /// <summary>Serialize the physical materials generated by the render material</summary>
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        LandscapePhysicalMaterialRenderData,

        /// <summary>RuntimeVirtualTextureVolume fix transforms</summary>
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        FixupRuntimeVirtualTextureVolume,

        /// <summary>Retrieve water body collision components that were lost in cooked builds</summary>
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        FixUpRiverCollisionComponents,

        /// <summary>Fix duplicate spline mesh components on rivers</summary>
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        FixDuplicateRiverSplineMeshCollisionComponents,

        /// <summary>Indicates level has stable actor guids</summary>
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        ContainsStableActorGUIDs,

        /// <summary>Levelset Serialization support for BodySetup.</summary>
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        LevelsetSerializationSupportForBodySetup,

        /// <summary>Moving Chaos solver properties to allow them to exist in the project physics settings</summary>
        [Introduced(UE4Version.VER_UE4_ASSETREGISTRY_DEPENDENCYFLAGS)]
        ChaosSolverPropertiesMoved,

        /// <summary>Moving some UFortGameFeatureData properties and behaviors into the UGameFeatureAction pattern</summary>
        [Introduced(UE4Version.VER_UE4_CORRECT_LICENSEE_FLAG)]
        GameFeatureData_MovedComponentListAndCheats,

        /// <summary>Add centrifugal forces for cloth</summary>
        [Introduced(UE4Version.VER_UE4_CORRECT_LICENSEE_FLAG)]
        ChaosClothAddfictitiousforces,

        /// <summary>Chaos Convex StructureData supports different index sizes based on num verts/planes. Chaos FConvex uses array of FVec3s for vertices instead of particles (Merged from //UE4/Main)</summary>
        [Introduced(UE4Version.VER_UE4_CORRECT_LICENSEE_FLAG)]
        ChaosConvexVariableStructureDataAndVerticesArray,

        /// <summary>Remove the WaterVelocityHeightTexture dependency on MPC_Landscape and LandscapeWaterIndo</summary>
        [Introduced(UE4Version.VER_UE4_CORRECT_LICENSEE_FLAG)]
        RemoveLandscapeWaterInfo,

        // CHANGES BEYOND HERE ARE UE5 ONLY //

        /// <summary>Added the weighted value property type to store the cloths weight maps' low/high ranges</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        ChaosClothAddWeightedValue,

        /// <summary>Added the Long Range Attachment stiffness weight map</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        ChaosClothAddTetherStiffnessWeightMap,

        /// <summary>Fix corrupted LOD transition maps</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        ChaosClothFixLODTransitionMaps,

        /// <summary>Enable a few more weight maps to better art direct the cloth simulation</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        ChaosClothAddTetherScaleAndDragLiftWeightMaps,

        /// <summary>Enable material (edge, bending, and area stiffness) weight maps</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        ChaosClothAddMaterialWeightMaps,

        /// <summary>Added bShowCurve for movie scene float channel serialization</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        SerializeFloatChannelShowCurve,

        /// <summary>Minimize slack waste by using a single array for grass data</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        LandscapeGrassSingleArray,

        /// <summary>Add loop counters to sequencer's compiled sub-sequence data</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        AddedSubSequenceEntryWarpCounter,

        /// <summary>Water plugin is now component-based rather than actor based</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        WaterBodyComponentRefactor,

        /// <summary>Cooked BPGC storing editor-only asset tags</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        BPGCCookedEditorTags,

        /// <summary>Terrain layer weights are no longer considered material parameters</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        TerrainLayerWeightsAreNotParameters,

        /// <summary>
        /// Anim Dynamics Node Gravity Override vector is now defined in world space, not simulation space. 
        /// Legacy behavior can be maintained with a flag, which is set false by default for new nodes, 
        /// true for nodes predating this change.
        /// </summary>
        GravityOverrideDefinedInWorldSpace,

        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        VersionPlusOne,
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        LatestVersion = VersionPlusOne - 1
    };

    /// <summary>
    /// Custom serialization version for changes made in Dev-Framework stream.
    /// </summary>
    public enum FFrameworkObjectVersion
    {
        /// <summary>Before any version changes were made</summary>
        [Introduced(UE4Version.VER_UE4_OLDEST_LOADABLE_PACKAGE)]
        BeforeCustomVersionWasAdded = 0,

        /// <summary>BodySetup's default instance collision profile is used by default when creating a new instance.</summary>
        [Introduced(UE4Version.VER_UE4_STREAMABLE_TEXTURE_AABB)]
        UseBodySetupCollisionProfile,

        /// <summary>Regenerate subgraph arrays correctly in animation blueprints to remove duplicates and add missing graphs that appear read only when edited</summary>
        [Introduced(UE4Version.VER_UE4_PROPERTY_GUID_IN_PROPERTY_TAG)]
        AnimBlueprintSubgraphFix,

        /// <summary>Static and skeletal mesh sockets now use the specified scale</summary>
        [Introduced(UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)]
        MeshSocketScaleUtilization,

        /// <summary>Attachment rules are now explicit in how they affect location, rotation and scale</summary>
        [Introduced(UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)]
        ExplicitAttachmentRules,

        /// <summary>Moved compressed anim data from uasset to the DDC</summary>
        [Introduced(UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)]
        MoveCompressedAnimDataToTheDDC,

        /// <summary>Some graph pins created using legacy code seem to have lost the RF_Transactional flag, which causes issues with undo. Restore the flag at this version</summary>
        [Introduced(UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)]
        FixNonTransactionalPins,

        /// <summary>Create new struct for SmartName, and use that for CurveName</summary>
        [Introduced(UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)]
        SmartNameRefactor,

        /// <summary>Add Reference Skeleton to Rig</summary>
        [Introduced(UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)]
        AddSourceReferenceSkeletonToRig,

        /// <summary>Refactor ConstraintInstance so that we have an easy way to swap behavior paramters</summary>
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        ConstraintInstanceBehaviorParameters,

        /// <summary>Pose Asset support mask per bone</summary>
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        PoseAssetSupportPerBoneMask,

        /// <summary>Physics Assets now use SkeletalBodySetup instead of BodySetup</summary>
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        PhysAssetUseSkeletalBodySetup,

        /// <summary>Remove SoundWave CompressionName</summary>
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        RemoveSoundWaveCompressionName,

        /// <summary>Switched render data for clothing over to unreal data, reskinned to the simulation mesh</summary>
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        AddInternalClothingGraphicalSkinning,

        /// <summary>Wheel force offset is now applied at the wheel instead of vehicle COM</summary>
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        WheelOffsetIsFromWheel,

        /// <summary>Move curve metadata to be saved in skeleton. Individual asset still saves some flag - i.e. disabled curve and editable or not, but major flag - i.e. material types - moves to skeleton and handle in one place</summary>
        [Introduced(UE4Version.VER_UE4_COMPRESSED_SHADER_RESOURCES)]
        MoveCurveTypesToSkeleton,

        /// <summary>Cache destructible overlaps on save</summary>
        [Introduced(UE4Version.VER_UE4_TemplateIndex_IN_COOKED_EXPORTS)]
        CacheDestructibleOverlaps,

        /// <summary>Added serialization of materials applied to geometry cache objects</summary>
        [Introduced(UE4Version.VER_UE4_TemplateIndex_IN_COOKED_EXPORTS)]
        GeometryCacheMissingMaterials,

        /// <summary>Switch static and skeletal meshes to calculate LODs based on resolution-independent screen size</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        LODsUseResolutionIndependentScreenSize,

        /// <summary>Blend space post load verification</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        BlendSpacePostLoadSnapToGrid,

        /// <summary>Addition of rate scales to blend space samples</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        SupportBlendSpaceRateScale,

        /// <summary>LOD hysteresis also needs conversion from the LODsUseResolutionIndependentScreenSize version</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        LODHysteresisUseResolutionIndependentScreenSize,

        /// <summary>AudioComponent override subtitle priority default change</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        ChangeAudioComponentOverrideSubtitlePriorityDefault,

        /// <summary>Serialize hard references to sound files when possible</summary>
        [Introduced(UE4Version.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)]
        HardSoundReferences,

        /// <summary>Enforce const correctness in Animation Blueprint function graphs</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        EnforceConstInAnimBlueprintFunctionGraphs,

        /// <summary>Upgrade the InputKeySelector to use a text style</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        InputKeySelectorTextStyle,

        /// <summary>Represent a pins container type as an enum not 3 independent booleans</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        EdGraphPinContainerType,

        /// <summary>Switch asset pins to store as string instead of hard object reference</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        ChangeAssetPinsToString,

        /// <summary>Fix Local Variables so that the properties are correctly flagged as blueprint visible</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        LocalVariablesBlueprintVisible,

        /// <summary>Stopped serializing UField_Next so that UFunctions could be serialized in dependently of a UClass in order to allow us to do all UFunction loading in a single pass (after classes and CDOs are created)</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)]
        RemoveUField_Next,

        /// <summary>Fix User Defined structs so that all members are correct flagged blueprint visible</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)]
        UserDefinedStructsBlueprintVisible,

        /// <summary>FMaterialInput and FEdGraphPin store their name as FName instead of FString</summary>
        [Introduced(UE4Version.VER_UE4_POINTLIGHT_SOURCE_ORIENTATION)]
        PinsStoreFName,

        /// <summary>User defined structs store their default instance, which is used for initializing instances</summary>
        [Introduced(UE4Version.VER_UE4_POINTLIGHT_SOURCE_ORIENTATION)]
        UserDefinedStructsStoreDefaultInstance,

        /// <summary>Function terminator nodes serialize an FMemberReference rather than a name/class pair</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        FunctionTerminatorNodesUseMemberReference,

        /// <summary>Custom event and non-native interface event implementations add 'const' to reference parameters</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        EditableEventsUseConstRefParameters,

        /// <summary>No longer serialize the legacy flag that indicates this state, as it is now implied since we don't serialize the skeleton CDO</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        BlueprintGeneratedClassIsAlwaysAuthoritative,

        /// <summary>Enforce visibility of blueprint functions - e.g. raise an error if calling a private function from another blueprint:</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        EnforceBlueprintFunctionVisibility,

        /// <summary>ActorComponents now store their serialization index</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        StoringUCSSerializationIndex,

        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        VersionPlusOne,
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        LatestVersion = VersionPlusOne - 1
    };

    /// <summary>
    /// Custom serialization version for changes made in Dev-Core stream.
    /// </summary>
    public enum FCoreObjectVersion
    {
        /// <summary>Before any version changes were made</summary>
        [Introduced(UE4Version.VER_UE4_OLDEST_LOADABLE_PACKAGE)]
        BeforeCustomVersionWasAdded = 0,

        [Introduced(UE4Version.VER_UE4_PROPERTY_GUID_IN_PROPERTY_TAG)]
        MaterialInputNativeSerialize,

        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        EnumProperties,

        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        SkeletalMaterialEditorDataStripping,

        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        FProperties,

        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        VersionPlusOne,
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        LatestVersion = VersionPlusOne - 1
    };

    /// <summary>
    /// Custom serialization version for changes made in Dev-Editor stream.
    /// </summary>
    public enum FEditorObjectVersion
    {
        /// <summary>Before any version changes were made</summary>
        [Introduced(UE4Version.VER_UE4_OLDEST_LOADABLE_PACKAGE)]
        BeforeCustomVersionWasAdded = 0,

        /// <summary>Localizable text gathered and stored in packages is now flagged with a localizable text gathering process version</summary>
        [Introduced(UE4Version.VER_UE4_STREAMABLE_TEXTURE_AABB)]
        GatheredTextProcessVersionFlagging,

        /// <summary>Fixed several issues with the gathered text cache stored in package headers</summary>
        [Introduced(UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)]
        GatheredTextPackageCacheFixesV1,

        /// <summary>Added support for "root" meta-data (meta-data not associated with a particular object in a package)</summary>
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        RootMetaDataSupport,

        /// <summary>Fixed issues with how Blueprint bytecode was cached</summary>
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        GatheredTextPackageCacheFixesV2,

        /// <summary>Updated FFormatArgumentData to allow variant data to be marshaled from a BP into C++</summary>
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        TextFormatArgumentDataIsVariant,

        /// <summary>Changes to SplineComponent</summary>
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        SplineComponentCurvesInStruct,

        /// <summary>Updated ComboBox to support toggling the menu open, better controller support</summary>
        [Introduced(UE4Version.VER_UE4_COMPRESSED_SHADER_RESOURCES)]
        ComboBoxControllerSupportUpdate,

        /// <summary>Refactor mesh editor materials</summary>
        [Introduced(UE4Version.VER_UE4_COMPRESSED_SHADER_RESOURCES)]
        RefactorMeshEditorMaterials,

        /// <summary>Added UFontFace assets</summary>
        [Introduced(UE4Version.VER_UE4_TemplateIndex_IN_COOKED_EXPORTS)]
        AddedFontFaceAssets,

        /// <summary>Add UPROPERTY for TMap of Mesh section, so the serialize will be done normally (and export to text will work correctly)</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        UPropertryForMeshSection,

        /// <summary>Update the schema of all widget blueprints to use the WidgetGraphSchema</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        WidgetGraphSchema,

        /// <summary>Added a specialized content slot to the background blur widget</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        AddedBackgroundBlurContentSlot,

        /// <summary>Updated UserDefinedEnums to have stable keyed display names</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        StableUserDefinedEnumDisplayNames,

        /// <summary>Added "Inline" option to UFontFace assets</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        AddedInlineFontFaceAssets,

        /// <summary>Fix a serialization issue with static mesh FMeshSectionInfoMap FProperty</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        UPropertryForMeshSectionSerialize,

        /// <summary>Adding a version bump for the new fast widget construction in case of problems.</summary>
        [Introduced(UE4Version.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)]
        FastWidgetTemplates,

        /// <summary>Update material thumbnails to be more intelligent on default primitive shape for certain material types</summary>
        [Introduced(UE4Version.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)]
        MaterialThumbnailRenderingChanges,

        /// <summary>Introducing a new clipping system for Slate/UMG</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        NewSlateClippingSystem,

        /// <summary>MovieScene Meta Data added as native Serialization</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        MovieSceneMetaDataSerialization,

        /// <summary>Text gathered from properties now adds two variants: a version without the package localization ID (for use at runtime), and a version with it (which is editor-only)</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        GatheredTextEditorOnlyPackageLocId,

        /// <summary>Added AlwaysSign to FNumberFormattingOptions</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)]
        AddedAlwaysSignNumberFormattingOption,

        /// <summary>Added additional objects that must be serialized as part of this new material feature</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        AddedMaterialSharedInputs,

        /// <summary>Added morph target section indices</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        AddedMorphTargetSectionIndices,

        /// <summary>Serialize the instanced static mesh render data, to avoid building it at runtime</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        SerializeInstancedStaticMeshRenderData,

        /// <summary>Change to MeshDescription serialization (moved to release)</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        MeshDescriptionNewSerialization_MovedToRelease,

        /// <summary>New format for mesh description attributes</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        MeshDescriptionNewAttributeFormat,

        /// <summary>Switch root component of SceneCapture actors from MeshComponent to SceneComponent</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        ChangeSceneCaptureRootComponent,

        /// <summary>StaticMesh serializes MeshDescription instead of RawMesh</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        StaticMeshDeprecatedRawMesh,

        /// <summary>MeshDescriptionBulkData contains a Guid used as a DDC key</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        MeshDescriptionBulkDataGuid,

        /// <summary>Change to MeshDescription serialization (removed FMeshPolygon::HoleContours)</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        MeshDescriptionRemovedHoles,

        /// <summary>Change to the WidgetCompoent WindowVisibilty default value</summary>
        [Introduced(UE4Version.VER_UE4_FILLER_22)]
        ChangedWidgetComponentWindowVisibilityDefault,

        /// <summary>Avoid keying culture invariant display strings during serialization to avoid non-deterministic cooking issues</summary>
        [Introduced(UE4Version.VER_UE4_FILLER_22)]
        CultureInvariantTextSerializationKeyStability,

        /// <summary>Change to UScrollBar and UScrollBox thickness property (removed implicit padding of 2, so thickness value must be incremented by 4).</summary>
        [Introduced(UE4Version.VER_UE4_FILLER_22)]
        ScrollBarThicknessChange,

        /// <summary>Deprecated LandscapeHoleMaterial</summary>
        [Introduced(UE4Version.VER_UE4_FILLER_22)]
        RemoveLandscapeHoleMaterial,

        /// <summary>MeshDescription defined by triangles instead of arbitrary polygons</summary>
        [Introduced(UE4Version.VER_UE4_FILLER_22)]
        MeshDescriptionTriangles,

        /// <summary>Add weighted area and angle when computing the normals</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        ComputeWeightedNormals,

        /// <summary>SkeletalMesh now can be rebuild in editor, no more need to re-import</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        SkeletalMeshBuildRefactor,

        /// <summary>Move all SkeletalMesh source data into a private uasset in the same package has the skeletalmesh</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        SkeletalMeshMoveEditorSourceDataToPrivateAsset,

        /// <summary>Parse text only if the number is inside the limits of its type</summary>
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        NumberParsingOptionsNumberLimitsAndClamping,

        /// <summary>Make sure we can have more then 255 material in the skeletal mesh source data</summary>
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        SkeletalMeshSourceDataSupport16bitOfMaterialNumber,

        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        VersionPlusOne,
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        LatestVersion = VersionPlusOne - 1
    };

    /// <summary>
    /// Custom serialization version for changes made in Dev-AnimPhys stream
    /// </summary>
    public enum FAnimPhysObjectVersion
    {
        /// <summary>Before any version changes were made</summary>
        [Introduced(UE4Version.VER_UE4_OLDEST_LOADABLE_PACKAGE)]
        BeforeCustomVersionWasAdded,

        /// <summary>convert animnode look at to use just default axis instead of enum, which doesn't do much</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        ConvertAnimNodeLookAtAxis,

        /// <summary>Change FKSphylElem and FKBoxElem to use Rotators not Quats for easier editing</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        BoxSphylElemsUseRotators,

        /// <summary>Change thumbnail scene info and asset import data to be transactional</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        ThumbnailSceneInfoAndAssetImportDataAreTransactional,

        /// <summary>Enabled clothing masks rather than painting parameters directly</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        AddedClothingMaskWorkflow,

        /// <summary>Remove UID from smart name serialize, it just breaks determinism</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        RemoveUIDFromSmartNameSerialize,

        /// <summary>Convert FName Socket to FSocketReference and added TargetReference that support bone and socket</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        CreateTargetReference,

        /// <summary>Tune soft limit stiffness and damping coefficients</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        TuneSoftLimitStiffnessAndDamping,

        /// <summary>Fix possible inf/nans in clothing particle masses</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        FixInvalidClothParticleMasses,

        /// <summary>Moved influence count to cached data</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        CacheClothMeshInfluences,

        /// <summary>Remove GUID from Smart Names entirely + remove automatic name fixup</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)]
        SmartNameRefactorForDeterministicCooking,

        /// <summary>rename the variable and allow individual curves to be set</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)]
        RenameDisableAnimCurvesToAllowAnimCurveEvaluation,

        /// <summary>link curve to LOD, so curve metadata has to include LODIndex</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)]
        AddLODToCurveMetaData,

        /// <summary>Fixed blend profile references persisting after paste when they aren't compatible</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)]
        FixupBadBlendProfileReferences,

        /// <summary>Allowing multiple audio plugin settings</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)]
        AllowMultipleAudioPluginSettings,

        /// <summary>Change RetargetSource reference to SoftObjectPtr</summary>
        [Introduced(UE4Version.VER_UE4_POINTLIGHT_SOURCE_ORIENTATION)]
        ChangeRetargetSourceReferenceToSoftObjectPtr,

        /// <summary>Save editor only full pose for pose asset</summary>
        [Introduced(UE4Version.VER_UE4_POINTLIGHT_SOURCE_ORIENTATION)]
        SaveEditorOnlyFullPoseForPoseAsset,

        /// <summary>Asset change and cleanup to facilitate new streaming system</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        GeometryCacheAssetDeprecation,

        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        VersionPlusOne,
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        LatestVersion = VersionPlusOne - 1
    }

    /// <summary>
    /// Custom serialization version for changes made in Release streams.
    /// </summary>
    public enum FReleaseObjectVersion
    {
        /// <summary>Before any version changes were made</summary>
        [Introduced(UE4Version.VER_UE4_OLDEST_LOADABLE_PACKAGE)]
        BeforeCustomVersionWasAdded = 0,

        /// <summary>Static Mesh extended bounds radius fix</summary>
        [Introduced(UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)]
        StaticMeshExtendedBoundsFix,

        /// <summary>Physics asset bodies are either in the sync scene or the async scene, but not both</summary>
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        NoSyncAsyncPhysAsset,

        /// <summary>ULevel was using TTransArray incorrectly (serializing the entire array in addition to individual mutations). converted to a TArray</summary>
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        LevelTransArrayConvertedToTArray,

        /// <summary>Add Component node templates now use their own unique naming scheme to ensure more reliable archetype lookups.</summary>
        [Introduced(UE4Version.VER_UE4_TemplateIndex_IN_COOKED_EXPORTS)]
        AddComponentNodeTemplateUniqueNames,

        /// <summary>Fix a serialization issue with static mesh FMeshSectionInfoMap FProperty</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        UPropertryForMeshSectionSerialize,

        /// <summary>Existing HLOD settings screen size to screen area conversion</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        ConvertHLODScreenSize,

        /// <summary>Adding mesh section info data for existing billboard LOD models</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        SpeedTreeBillboardSectionInfoFixup,

        /// <summary>Change FMovieSceneEventParameters::StructType to be a string asset reference from a TWeakObjectPtr UScriptStruct</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        EventSectionParameterStringAssetRef,

        /// <summary>Remove serialized irradiance map data from skylight.</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        SkyLightRemoveMobileIrradianceMap,

        /// <summary>rename bNoTwist to bAllowTwist</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        RenameNoTwistToAllowTwistInTwoBoneIK,

        /// <summary>Material layers serialization refactor</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        MaterialLayersParameterSerializationRefactor,

        /// <summary>Added disable flag to skeletal mesh data</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        AddSkeletalMeshSectionDisable,

        /// <summary>Removed objects that were serialized as part of this material feature</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        RemovedMaterialSharedInputCollection,

        /// <summary>HISMC Cluster Tree migration to add new data</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        HISMCClusterTreeMigration,

        /// <summary>Default values on pins in blueprints could be saved incoherently</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        PinDefaultValuesVerified,

        /// <summary>During copy and paste transition getters could end up with broken state machine references</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        FixBrokenStateMachineReferencesInTransitionGetters,

        /// <summary>Change to MeshDescription serialization</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        MeshDescriptionNewSerialization,

        /// <summary>Change to not clamp RGB values > 1 on linear color curves</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        UnclampRGBColorCurves,

        /// <summary>Bugfix for FAnimObjectVersion::LinkTimeAnimBlueprintRootDiscovery.</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        LinkTimeAnimBlueprintRootDiscoveryBugFix,

        /// <summary>Change trail anim node variable deprecation</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        TrailNodeBlendVariableNameChange,

        /// <summary>Make sure the Blueprint Replicated Property Conditions are actually serialized properly.</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        PropertiesSerializeRepCondition,

        /// <summary>DepthOfFieldFocalDistance at 0 now disables DOF instead of DepthOfFieldFstop at 0.</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        FocalDistanceDisablesDOF,

        /// <summary>Removed versioning, but version entry must still exist to keep assets saved with this version loadable</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        Unused_SoundClass2DReverbSend,

        /// <summary>Groom asset version</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        GroomAssetVersion1,

        /// <summary>Groom asset version</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        GroomAssetVersion2,

        /// <summary>Store applied version of Animation Modifier to use when reverting</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        SerializeAnimModifierState,

        /// <summary>Groom asset version</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        GroomAssetVersion3,

        /// <summary>Upgrade filmback</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        DeprecateFilmbackSettings,

        /// <summary>custom collision type</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        CustomImplicitCollisionType,

        /// <summary>FFieldPath will serialize the owner struct reference and only a short path to its property</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        FFieldPathOwnerSerialization,

        /// <summary>Dummy version to allow us to fix up the fact that ReleaseObjectVersion was changed elsewhere</summary>
        [Introduced(UE4Version.VER_UE4_SKINWEIGHT_PROFILE_DATA_LAYOUT_CHANGES)]
        ReleaseObjectVersionFixup,

        /// <summary>Pin types include a flag that propagates the 'CPF_UObjectWrapper' flag to generated properties</summary>
        [Introduced(UE4Version.VER_UE4_ASSETREGISTRY_DEPENDENCYFLAGS)]
        PinTypeIncludesUObjectWrapperFlag,

        /// <summary>Added Weight member to FMeshToMeshVertData</summary>
        [Introduced(UE4Version.VER_UE4_ASSETREGISTRY_DEPENDENCYFLAGS)]
        WeightFMeshToMeshVertData,

        /// <summary>Animation graph node bindings displayed as pins</summary>
        [Introduced(UE4Version.VER_UE4_ASSETREGISTRY_DEPENDENCYFLAGS)]
        AnimationGraphNodeBindingsDisplayedAsPins,

        /// <summary>Serialized rigvm offset segment paths</summary>
        [Introduced(UE4Version.VER_UE4_ASSETREGISTRY_DEPENDENCYFLAGS)]
        SerializeRigVMOffsetSegmentPaths,

        /// <summary>Upgrade AbcGeomCacheImportSettings for velocities</summary>
        [Introduced(UE4Version.VER_UE4_ASSETREGISTRY_DEPENDENCYFLAGS)]
        AbcVelocitiesSupport,

        /// <summary>Add margin support to Chaos Convex</summary>
        [Introduced(UE4Version.VER_UE4_ASSETREGISTRY_DEPENDENCYFLAGS)]
        MarginAddedToConvexAndBox,

        /// <summary>Add structure data to Chaos Convex</summary>
        [Introduced(UE4Version.VER_UE4_ASSETREGISTRY_DEPENDENCYFLAGS)]
        StructureDataAddedToConvex,

        /// <summary>Changed axis UI for LiveLink AxisSwitch Pre Processor</summary>
        [Introduced(UE4Version.VER_UE4_CORRECT_LICENSEE_FLAG)]
        AddedFrontRightUpAxesToLiveLinkPreProcessor,

        /// <summary>Some sequencer event sections that were copy-pasted left broken links to the director BP</summary>
        [Introduced(UE4Version.VER_UE4_CORRECT_LICENSEE_FLAG)]
        FixupCopiedEventSections,

        /// <summary>Serialize the number of bytes written when serializing function arguments</summary>
        [Introduced(UE4Version.VER_UE4_CORRECT_LICENSEE_FLAG)]
        RemoteControlSerializeFunctionArgumentsSize,

        /// <summary>Add loop counters to sequencer's compiled sub-sequence data</summary>
        [Introduced(UE4Version.VER_UE4_CORRECT_LICENSEE_FLAG)]
        AddedSubSequenceEntryWarpCounter,

        /// <summary>Remove default resolution limit of 512 pixels for cubemaps generated from long-lat sources</summary>
        [Introduced(UE4Version.VER_UE4_CORRECT_LICENSEE_FLAG)]
        LonglatTextureCubeDefaultMaxResolution,

        /// <summary>bake center of mass into chaos cache</summary>
        [Introduced(UE4Version.VER_UE4_CORRECT_LICENSEE_FLAG)]
        GeometryCollectionCacheRemovesMassToLocal,

        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        VersionPlusOne,
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        LatestVersion = VersionPlusOne - 1
    }

    /// <summary>
    /// Version used for serializing asset registry caches, both runtime and editor
    /// </summary>
    public enum FAssetRegistryVersion
    {
        /// <summary>From before file versioning was implemented</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        PreVersioning = 0,

        /// <summary>The first version of the runtime asset registry to include file versioning.</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        HardSoftDependencies,

        /// <summary>Added FAssetRegistryState and support for piecemeal serialization</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        AddAssetRegistryState,

        /// <summary>AssetData serialization format changed, versions before this are not readable</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        ChangedAssetData,

        /// <summary>Removed MD5 hash from package data</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        RemovedMD5Hash,

        /// <summary>Added hard/soft manage references</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        AddedHardManage,

        /// <summary>Added MD5 hash of cooked package to package data</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        AddedCookedMD5Hash,

        /// <summary>Added UE::AssetRegistry::EDependencyProperty to each dependency</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        AddedDependencyFlags,

        /// <summary>Major tag format change that replaces USE_COMPACT_ASSET_REGISTRY:</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        FixedTags,

        /// <summary>Added Version information to AssetPackageData</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        WorkspaceDomain,

        /// <summary>Added ImportedClasses to AssetPackageData</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        PackageImportedClasses,

        /// <summary>A new version number of UE5 was added to FPackageFileSummary</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        PackageFileSummaryVersionChange,

        /// <summary>Change to linker export/import resource serializationn</summary>
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        ObjectResourceOptionalVersionChange,

        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        VersionPlusOne,
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        LatestVersion = VersionPlusOne - 1
    }

    public enum FSequencerObjectVersion
    {
        /// <summary>Before any version changes were made</summary>
        [Introduced(UE4Version.VER_UE4_OLDEST_LOADABLE_PACKAGE)]
        BeforeCustomVersionWasAdded = 0,

        /// <summary>Per-platform overrides player overrides for media sources changed name and type.</summary>
        [Introduced(UE4Version.VER_UE4_OLDEST_LOADABLE_PACKAGE)]
        RenameMediaSourcePlatformPlayers,

        /// <summary>Enable root motion isn't the right flag to use, but force root lock</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        ConvertEnableRootMotionToForceRootLock,

        /// <summary>Convert multiple rows to tracks</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        ConvertMultipleRowsToTracks,

        /// <summary>When finished now defaults to restore state</summary>
        [Introduced(UE4Version.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)]
        WhenFinishedDefaultsToRestoreState,

        /// <summary>EvaluationTree added</summary>
        [Introduced(UE4Version.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)]
        EvaluationTree,

        /// <summary>When finished now defaults to project default</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)]
        WhenFinishedDefaultsToProjectDefault,

        /// <summary>Use int range rather than float range in FMovieSceneSegment</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        FloatToIntConversion,

        /// <summary>Purged old spawnable blueprint classes from level sequence assets</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        PurgeSpawnableBlueprints,

        /// <summary>Finish UMG evaluation on end</summary>
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        FinishUMGEvaluation,

        /// <summary>Manual serialization of float channel</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        SerializeFloatChannel,

        /// <summary>Change the linear keys so they act the old way and interpolate always.</summary>
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        ModifyLinearKeysForOldInterp,

        /// <summary>Full Manual serialization of float channel</summary>
        [Introduced(UE4Version.VER_UE4_SKINWEIGHT_PROFILE_DATA_LAYOUT_CHANGES)]
        SerializeFloatChannelCompletely,

        /// <summary>Set ContinuouslyRespawn to false by default, added FMovieSceneSpawnable::bNetAddressableName</summary>
        [Introduced(UE4Version.VER_UE4_ASSETREGISTRY_DEPENDENCYFLAGS)]
        SpawnableImprovements,

        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        VersionPlusOne,
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        LatestVersion = VersionPlusOne - 1
    }
}
