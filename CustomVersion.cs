using System;
using System.Collections.Generic;

namespace UAssetAPI
{
    public class CustomVersion
    {
        /**
         * Static map of custom version GUIDs to the class or object that they represent in the Unreal Engine. This list is not necessarily exhaustive, so feel free to add to it if need be
         */
        private static readonly Dictionary<Guid, string> GuidToCustomVersionStringMap = new Dictionary<Guid, string>()
        {
            { UAPUtils.GUID(0, 0, 0, 0xF99D40C1), "UnusedCustomVersionKey" },
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

        public Guid Key;
        public string FriendlyName = null;
        public int Version;

        public CustomVersion(Guid key, int version)
        {
            Key = key;
            if (GuidToCustomVersionStringMap.ContainsKey(key)) FriendlyName = GuidToCustomVersionStringMap[key];
            Version = version;
        }

        public CustomVersion()
        {

        }
    }

    /* Below are some enums of custom versions we need for serialization */

    /* This attribute represents the engine version at the time that the custom version was implemented */
    [AttributeUsage(AttributeTargets.Field)]
    public class IntroducedAttribute : Attribute
    {
        public UE4Version IntroducedVersion;

        public IntroducedAttribute(UE4Version introducedVersion)
        {
            IntroducedVersion = introducedVersion;
        }
    }

    public enum FFortniteMainBranchObjectVersion
    {
        // Before any version changes were made
        [Introduced(UE4Version.VER_UE4_OLDEST_LOADABLE_PACKAGE)]
        BeforeCustomVersionWasAdded = 0,

        // World composition tile offset changed from 2d to 3d
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        WorldCompositionTile3DOffset,

        // Minor material serialization optimization
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        MaterialInstanceSerializeOptimization_ShaderFName,

        // Refactored cull distances to account for HLOD, explicit override and globals in priority
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        CullDistanceRefactor_RemovedDefaultDistance,
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        CullDistanceRefactor_NeverCullHLODsByDefault,
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        CullDistanceRefactor_NeverCullALODActorsByDefault,

        // Support to remove morphtarget generated by bRemapMorphtarget
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        SaveGeneratedMorphTargetByEngine,

        // Convert reduction setting options
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        ConvertReductionSettingOptions,

        // Serialize the type of blending used for landscape layer weight static params
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        StaticParameterTerrainLayerWeightBlendType,

        // Fix up None Named animation curve names,
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        FixUpNoneNameAnimationCurves,

        // Ensure ActiveBoneIndices to have parents even not skinned for old assets
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        EnsureActiveBoneIndicesToContainParents,

        // Serialize the instanced static mesh render data, to avoid building it at runtime
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        SerializeInstancedStaticMeshRenderData,

        // Cache material quality node usage
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        CachedMaterialQualityNodeUsage,

        // Font outlines no longer apply to drop shadows for new objects but we maintain the opposite way for backwards compat
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        FontOutlineDropShadowFixup,

        // New skeletal mesh import workflow (Geometry only or animation only re-import )
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        NewSkeletalMeshImporterWorkflow,

        // Migrate data from previous data structure to new one to support materials per LOD on the Landscape
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        NewLandscapeMaterialPerLOD,

        // New Pose Asset data type
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        RemoveUnnecessaryTracksFromPose,

        // Migrate Foliage TLazyObjectPtr to TSoftObjectPtr
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        FoliageLazyObjPtrToSoftObjPtr,

        // TimelineTemplates store their derived names instead of dynamically generating
        // This code tied to this version was reverted and redone at a later date
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        REVERTED_StoreTimelineNamesInTemplate,

        // Added BakePoseOverride for LOD setting
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        AddBakePoseOverrideForSkeletalMeshReductionSetting,

        // TimelineTemplates store their derived names instead of dynamically generating
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        StoreTimelineNamesInTemplate,

        // New Pose Asset data type
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        WidgetStopDuplicatingAnimations,

        // Allow reducing of the base LOD, we need to store some imported model data so we can reduce again from the same data.
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        AllowSkeletalMeshToReduceTheBaseLOD,

        // Curve Table size reduction
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        ShrinkCurveTableSize,

        // Widgets upgraded with WidgetStopDuplicatingAnimations, may not correctly default-to-self for the widget parameter.
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        WidgetAnimationDefaultToSelfFail,

        // HUDWidgets now require an element tag
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        FortHUDElementNowRequiresTag,

        // Animation saved as bulk data when cooked
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        FortMappedCookedAnimation,

        // Support Virtual Bone in Retarget Manager
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        SupportVirtualBoneInRetargeting,

        // Fixup bad defaults in water metadata
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        FixUpWaterMetadata,

        // Move the location of water metadata
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        MoveWaterMetadataToActor,

        // Replaced lake collision component
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        ReplaceLakeCollision,

        // Anim layer node names are now conformed by Guid
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        AnimLayerGuidConformation,

        // Ocean collision component has become dynamic
        [Introduced(UE4Version.VER_UE4_SKINWEIGHT_PROFILE_DATA_LAYOUT_CHANGES)]
        MakeOceanCollisionTransient,

        // FFieldPath will serialize the owner struct reference and only a short path to its property
        [Introduced(UE4Version.VER_UE4_SKINWEIGHT_PROFILE_DATA_LAYOUT_CHANGES)]
        FFieldPathOwnerSerialization,

        // Simplified WaterBody post process material handling
        [Introduced(UE4Version.VER_UE4_SKINWEIGHT_PROFILE_DATA_LAYOUT_CHANGES)]
        FixUpUnderwaterPostProcessMaterial,

        // A single water exclusion volume can now exclude N water bodies
        [Introduced(UE4Version.VER_UE4_SKINWEIGHT_PROFILE_DATA_LAYOUT_CHANGES)]
        SupportMultipleWaterBodiesPerExclusionVolume,

        // Serialize rigvm operators one by one instead of the full byte code array to ensure determinism
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        RigVMByteCodeDeterminism,

        // Serialize the physical materials generated by the render material
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        LandscapePhysicalMaterialRenderData,

        // RuntimeVirtualTextureVolume fix transforms
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        FixupRuntimeVirtualTextureVolume,

        // Retrieve water body collision components that were lost in cooked builds
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        FixUpRiverCollisionComponents,

        // Fix duplicate spline mesh components on rivers
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        FixDuplicateRiverSplineMeshCollisionComponents,

        // Indicates level has stable actor guids
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        ContainsStableActorGUIDs,

        // Levelset Serialization support for BodySetup.
        [Introduced(UE4Version.VER_UE4_NON_OUTER_PACKAGE_IMPORT)]
        LevelsetSerializationSupportForBodySetup,

        // Moving Chaos solver properties to allow them to exist in the project physics settings
        [Introduced(UE4Version.VER_UE4_ASSETREGISTRY_DEPENDENCYFLAGS)]
        ChaosSolverPropertiesMoved,

        // Moving some UFortGameFeatureData properties and behaviors into the UGameFeatureAction pattern
        [Introduced(UE4Version.VER_UE4_CORRECT_LICENSEE_FLAG)]
        GameFeatureData_MovedComponentListAndCheats,

        // Add centrifugal forces for cloth
        [Introduced(UE4Version.VER_UE4_CORRECT_LICENSEE_FLAG)]
        ChaosClothAddfictitiousforces,

        // Chaos Convex StructureData supports different index sizes based on num verts/planes
        // Chaos FConvex uses array of FVec3s for vertices instead of particles
        // (Merged from //UE4/Main)
        [Introduced(UE4Version.VER_UE4_CORRECT_LICENSEE_FLAG)]
        ChaosConvexVariableStructureDataAndVerticesArray,

        // Remove the WaterVelocityHeightTexture dependency on MPC_Landscape and LandscapeWaterIndo
        [Introduced(UE4Version.VER_UE4_CORRECT_LICENSEE_FLAG)]
        RemoveLandscapeWaterInfo,

        // -----<new versions can be added above this line>-------------------------------------------------
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        VersionPlusOne,
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        LatestVersion = VersionPlusOne - 1
    };

    public enum FFrameworkObjectVersion
    {
        // Before any version changes were made
        [Introduced(UE4Version.VER_UE4_OLDEST_LOADABLE_PACKAGE)]
        BeforeCustomVersionWasAdded = 0,

        // BodySetup's default instance collision profile is used by default when creating a new instance.
        [Introduced(UE4Version.VER_UE4_STREAMABLE_TEXTURE_AABB)]
        UseBodySetupCollisionProfile,

        // Regenerate subgraph arrays correctly in animation blueprints to remove duplicates and add
        // missing graphs that appear read only when edited
        [Introduced(UE4Version.VER_UE4_PROPERTY_GUID_IN_PROPERTY_TAG)]
        AnimBlueprintSubgraphFix,

        // Static and skeletal mesh sockets now use the specified scale
        [Introduced(UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)]
        MeshSocketScaleUtilization,

        // Attachment rules are now explicit in how they affect location, rotation and scale
        [Introduced(UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)]
        ExplicitAttachmentRules,

        // Moved compressed anim data from uasset to the DDC
        [Introduced(UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)]
        MoveCompressedAnimDataToTheDDC,

        // Some graph pins created using legacy code seem to have lost the RF_Transactional flag,
        // which causes issues with undo. Restore the flag at this version
        [Introduced(UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)]
        FixNonTransactionalPins,

        // Create new struct for SmartName, and use that for CurveName
        [Introduced(UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)]
        SmartNameRefactor,

        // Add Reference Skeleton to Rig
        [Introduced(UE4Version.VER_UE4_NAME_HASHES_SERIALIZED)]
        AddSourceReferenceSkeletonToRig,

        // Refactor ConstraintInstance so that we have an easy way to swap behavior paramters
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        ConstraintInstanceBehaviorParameters,

        // Pose Asset support mask per bone
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        PoseAssetSupportPerBoneMask,

        // Physics Assets now use SkeletalBodySetup instead of BodySetup
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        PhysAssetUseSkeletalBodySetup,

        // Remove SoundWave CompressionName
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        RemoveSoundWaveCompressionName,

        // Switched render data for clothing over to unreal data, reskinned to the simulation mesh
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        AddInternalClothingGraphicalSkinning,

        // Wheel force offset is now applied at the wheel instead of vehicle COM
        [Introduced(UE4Version.VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR)]
        WheelOffsetIsFromWheel,

        // Move curve metadata to be saved in skeleton
        // Individual asset still saves some flag - i.e. disabled curve and editable or not, but 
        // major flag - i.e. material types - moves to skeleton and handle in one place
        [Introduced(UE4Version.VER_UE4_COMPRESSED_SHADER_RESOURCES)]
        MoveCurveTypesToSkeleton,

        // Cache destructible overlaps on save
        [Introduced(UE4Version.VER_UE4_TemplateIndex_IN_COOKED_EXPORTS)]
        CacheDestructibleOverlaps,

        // Added serialization of materials applied to geometry cache objects
        [Introduced(UE4Version.VER_UE4_TemplateIndex_IN_COOKED_EXPORTS)]
        GeometryCacheMissingMaterials,

        // Switch static & skeletal meshes to calculate LODs based on resolution-independent screen size
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        LODsUseResolutionIndependentScreenSize,

        // Blend space post load verification
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        BlendSpacePostLoadSnapToGrid,

        // Addition of rate scales to blend space samples
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        SupportBlendSpaceRateScale,

        // LOD hysteresis also needs conversion from the LODsUseResolutionIndependentScreenSize version
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        LODHysteresisUseResolutionIndependentScreenSize,

        // AudioComponent override subtitle priority default change
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        ChangeAudioComponentOverrideSubtitlePriorityDefault,

        // Serialize hard references to sound files when possible
        [Introduced(UE4Version.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)]
        HardSoundReferences,

        // Enforce const correctness in Animation Blueprint function graphs
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        EnforceConstInAnimBlueprintFunctionGraphs,

        // Upgrade the InputKeySelector to use a text style
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        InputKeySelectorTextStyle,

        // Represent a pins container type as an enum not 3 independent booleans
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        EdGraphPinContainerType,

        // Switch asset pins to store as string instead of hard object reference
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        ChangeAssetPinsToString,

        // Fix Local Variables so that the properties are correctly flagged as blueprint visible
        [Introduced(UE4Version.VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG)]
        LocalVariablesBlueprintVisible,

        // Stopped serializing UField_Next so that UFunctions could be serialized in dependently of a UClass
        // in order to allow us to do all UFunction loading in a single pass (after classes and CDOs are created):
        [Introduced(UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)]
        RemoveUField_Next,

        // Fix User Defined structs so that all members are correct flagged blueprint visible 
        [Introduced(UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)]
        UserDefinedStructsBlueprintVisible,

        // FMaterialInput and FEdGraphPin store their name as FName instead of FString
        [Introduced(UE4Version.VER_UE4_ADDED_SOFT_OBJECT_PATH)]
        PinsStoreFName,

        // User defined structs store their default instance, which is used for initializing instances
        [Introduced(UE4Version.VER_UE4_POINTLIGHT_SOURCE_ORIENTATION)]
        UserDefinedStructsStoreDefaultInstance,

        // Function terminator nodes serialize an FMemberReference rather than a name/class pair
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        FunctionTerminatorNodesUseMemberReference,

        // Custom event and non-native interface event implementations add 'const' to reference parameters
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID)]
        EditableEventsUseConstRefParameters,

        // No longer serialize the legacy flag that indicates this state, as it is now implied since we don't serialize the skeleton CDO
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        BlueprintGeneratedClassIsAlwaysAuthoritative,

        // Enforce visibility of blueprint functions - e.g. raise an error if calling a private function from another blueprint:
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        EnforceBlueprintFunctionVisibility,

        // ActorComponents now store their serialization index
        [Introduced(UE4Version.VER_UE4_ADDED_PACKAGE_OWNER)]
        StoringUCSSerializationIndex,

        // -----<new versions can be added above this line>-------------------------------------------------
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        VersionPlusOne,
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        LatestVersion = VersionPlusOne - 1
    };

    public enum FCoreObjectVersion
    {
        // Before any version changes were made
        [Introduced(UE4Version.VER_UE4_OLDEST_LOADABLE_PACKAGE)]
        BeforeCustomVersionWasAdded = 0,
        [Introduced(UE4Version.VER_UE4_PROPERTY_GUID_IN_PROPERTY_TAG)]
        MaterialInputNativeSerialize,
        [Introduced(UE4Version.VER_UE4_ADDED_SEARCHABLE_NAMES)]
        EnumProperties,
        [Introduced(UE4Version.VER_UE4_FIX_WIDE_STRING_CRC)]
        SkeletalMaterialEditorDataStripping,
        [Introduced(UE4Version.VER_UE4_SKINWEIGHT_PROFILE_DATA_LAYOUT_CHANGES)]
        FProperties,

        // -----<new versions can be added above this line>-------------------------------------------------
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION_PLUS_ONE)]
        VersionPlusOne,
        [Introduced(UE4Version.VER_UE4_AUTOMATIC_VERSION)]
        LatestVersion = VersionPlusOne - 1
    };
}
