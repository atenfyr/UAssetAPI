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

    public enum FFortniteMainBranchObjectVersion
    {
        // Before any version changes were made
        BeforeCustomVersionWasAdded = 0,

        // World composition tile offset changed from 2d to 3d
        WorldCompositionTile3DOffset,

        // Minor material serialization optimization
        MaterialInstanceSerializeOptimization_ShaderFName,

        // Refactored cull distances to account for HLOD, explicit override and globals in priority
        CullDistanceRefactor_RemovedDefaultDistance,
        CullDistanceRefactor_NeverCullHLODsByDefault,
        CullDistanceRefactor_NeverCullALODActorsByDefault,

        // Support to remove morphtarget generated by bRemapMorphtarget
        SaveGeneratedMorphTargetByEngine,

        // Convert reduction setting options
        ConvertReductionSettingOptions,

        // Serialize the type of blending used for landscape layer weight static params
        StaticParameterTerrainLayerWeightBlendType,

        // Fix up None Named animation curve names, 
        FixUpNoneNameAnimationCurves,

        // Ensure ActiveBoneIndices to have parents even not skinned for old assets
        EnsureActiveBoneIndicesToContainParents,

        // Serialize the instanced static mesh render data, to avoid building it at runtime
        SerializeInstancedStaticMeshRenderData,

        // Cache material quality node usage
        CachedMaterialQualityNodeUsage,

        // Font outlines no longer apply to drop shadows for new objects but we maintain the opposite way for backwards compat
        FontOutlineDropShadowFixup,

        // New skeletal mesh import workflow (Geometry only or animation only re-import )
        NewSkeletalMeshImporterWorkflow,

        // Migrate data from previous data structure to new one to support materials per LOD on the Landscape
        NewLandscapeMaterialPerLOD,

        // New Pose Asset data type
        RemoveUnnecessaryTracksFromPose,

        // Migrate Foliage TLazyObjectPtr to TSoftObjectPtr
        FoliageLazyObjPtrToSoftObjPtr,

        // TimelineTemplates store their derived names instead of dynamically generating
        // This code tied to this version was reverted and redone at a later date
        REVERTED_StoreTimelineNamesInTemplate,

        // Added BakePoseOverride for LOD setting
        AddBakePoseOverrideForSkeletalMeshReductionSetting,

        // TimelineTemplates store their derived names instead of dynamically generating
        StoreTimelineNamesInTemplate,

        // New Pose Asset data type
        WidgetStopDuplicatingAnimations,

        // Allow reducing of the base LOD, we need to store some imported model data so we can reduce again from the same data.
        AllowSkeletalMeshToReduceTheBaseLOD,

        // Curve Table size reduction
        ShrinkCurveTableSize,

        // Widgets upgraded with WidgetStopDuplicatingAnimations, may not correctly default-to-self for the widget parameter.
        WidgetAnimationDefaultToSelfFail,

        // HUDWidgets now require an element tag
        FortHUDElementNowRequiresTag,

        // Animation saved as bulk data when cooked
        FortMappedCookedAnimation,

        // Support Virtual Bone in Retarget Manager
        SupportVirtualBoneInRetargeting,

        // Fixup bad defaults in water metadata
        FixUpWaterMetadata,

        // Move the location of water metadata
        MoveWaterMetadataToActor,

        // Replaced lake collision component
        ReplaceLakeCollision,

        // Anim layer node names are now conformed by Guid
        AnimLayerGuidConformation,

        // Ocean collision component has become dynamic
        MakeOceanCollisionTransient,

        // FFieldPath will serialize the owner struct reference and only a short path to its property
        FFieldPathOwnerSerialization,

        // Simplified WaterBody post process material handling 
        FixUpUnderwaterPostProcessMaterial,

        // A single water exclusion volume can now exclude N water bodies
        SupportMultipleWaterBodiesPerExclusionVolume,

        // Serialize rigvm operators one by one instead of the full byte code array to ensure determinism
        RigVMByteCodeDeterminism,

        // Serialize the physical materials generated by the render material
        LandscapePhysicalMaterialRenderData,

        // RuntimeVirtualTextureVolume fix transforms
        FixupRuntimeVirtualTextureVolume,

        // Retrieve water body collision components that were lost in cooked builds
        FixUpRiverCollisionComponents,

        // Fix duplicate spline mesh components on rivers
        FixDuplicateRiverSplineMeshCollisionComponents,

        // Indicates level has stable actor guids
        ContainsStableActorGUIDs,

        // Levelset Serialization support for BodySetup.
        LevelsetSerializationSupportForBodySetup,

        // Moving Chaos solver properties to allow them to exist in the project physics settings
        ChaosSolverPropertiesMoved,

        // Moving some UFortGameFeatureData properties and behaviors into the UGameFeatureAction pattern
        GameFeatureData_MovedComponentListAndCheats,

        // Add centrifugal forces for cloth
        ChaosClothAddfictitiousforces,

        // Chaos Convex StructureData supports different index sizes based on num verts/planes
        // Chaos FConvex uses array of FVec3s for vertices instead of particles
        // (Merged from //UE4/Main)
        ChaosConvexVariableStructureDataAndVerticesArray,

        // Remove the WaterVelocityHeightTexture dependency on MPC_Landscape and LandscapeWaterIndo 
        RemoveLandscapeWaterInfo,

        // -----<new versions can be added above this line>-------------------------------------------------
        VersionPlusOne,
        LatestVersion = VersionPlusOne - 1
    };

    public enum FFrameworkObjectVersion
    {
        // Before any version changes were made
        BeforeCustomVersionWasAdded = 0,

        // BodySetup's default instance collision profile is used by default when creating a new instance.
        UseBodySetupCollisionProfile,

        // Regenerate subgraph arrays correctly in animation blueprints to remove duplicates and add
        // missing graphs that appear read only when edited
        AnimBlueprintSubgraphFix,

        // Static and skeletal mesh sockets now use the specified scale
        MeshSocketScaleUtilization,

        // Attachment rules are now explicit in how they affect location, rotation and scale
        ExplicitAttachmentRules,

        // Moved compressed anim data from uasset to the DDC
        MoveCompressedAnimDataToTheDDC,

        // Some graph pins created using legacy code seem to have lost the RF_Transactional flag,
        // which causes issues with undo. Restore the flag at this version
        FixNonTransactionalPins,

        // Create new struct for SmartName, and use that for CurveName
        SmartNameRefactor,

        // Add Reference Skeleton to Rig
        AddSourceReferenceSkeletonToRig,

        // Refactor ConstraintInstance so that we have an easy way to swap behavior paramters
        ConstraintInstanceBehaviorParameters,

        // Pose Asset support mask per bone
        PoseAssetSupportPerBoneMask,

        // Physics Assets now use SkeletalBodySetup instead of BodySetup
        PhysAssetUseSkeletalBodySetup,

        // Remove SoundWave CompressionName
        RemoveSoundWaveCompressionName,

        // Switched render data for clothing over to unreal data, reskinned to the simulation mesh
        AddInternalClothingGraphicalSkinning,

        // Wheel force offset is now applied at the wheel instead of vehicle COM
        WheelOffsetIsFromWheel,

        // Move curve metadata to be saved in skeleton
        // Individual asset still saves some flag - i.e. disabled curve and editable or not, but 
        // major flag - i.e. material types - moves to skeleton and handle in one place
        MoveCurveTypesToSkeleton,

        // Cache destructible overlaps on save
        CacheDestructibleOverlaps,

        // Added serialization of materials applied to geometry cache objects
        GeometryCacheMissingMaterials,

        // Switch static & skeletal meshes to calculate LODs based on resolution-independent screen size
        LODsUseResolutionIndependentScreenSize,

        // Blend space post load verification
        BlendSpacePostLoadSnapToGrid,

        // Addition of rate scales to blend space samples
        SupportBlendSpaceRateScale,

        // LOD hysteresis also needs conversion from the LODsUseResolutionIndependentScreenSize version
        LODHysteresisUseResolutionIndependentScreenSize,

        // AudioComponent override subtitle priority default change
        ChangeAudioComponentOverrideSubtitlePriorityDefault,

        // Serialize hard references to sound files when possible
        HardSoundReferences,

        // Enforce const correctness in Animation Blueprint function graphs
        EnforceConstInAnimBlueprintFunctionGraphs,

        // Upgrade the InputKeySelector to use a text style
        InputKeySelectorTextStyle,

        // Represent a pins container type as an enum not 3 independent booleans
        EdGraphPinContainerType,

        // Switch asset pins to store as string instead of hard object reference
        ChangeAssetPinsToString,

        // Fix Local Variables so that the properties are correctly flagged as blueprint visible
        LocalVariablesBlueprintVisible,

        // Stopped serializing UField_Next so that UFunctions could be serialized in dependently of a UClass
        // in order to allow us to do all UFunction loading in a single pass (after classes and CDOs are created):
        RemoveUField_Next,

        // Fix User Defined structs so that all members are correct flagged blueprint visible 
        UserDefinedStructsBlueprintVisible,

        // FMaterialInput and FEdGraphPin store their name as FName instead of FString
        PinsStoreFName,

        // User defined structs store their default instance, which is used for initializing instances
        UserDefinedStructsStoreDefaultInstance,

        // Function terminator nodes serialize an FMemberReference rather than a name/class pair
        FunctionTerminatorNodesUseMemberReference,

        // Custom event and non-native interface event implementations add 'const' to reference parameters
        EditableEventsUseConstRefParameters,

        // No longer serialize the legacy flag that indicates this state, as it is now implied since we don't serialize the skeleton CDO
        BlueprintGeneratedClassIsAlwaysAuthoritative,

        // Enforce visibility of blueprint functions - e.g. raise an error if calling a private function from another blueprint:
        EnforceBlueprintFunctionVisibility,

        // ActorComponents now store their serialization index
        StoringUCSSerializationIndex,

        // -----<new versions can be added above this line>-------------------------------------------------
        VersionPlusOne,
        LatestVersion = VersionPlusOne - 1
    };

    public enum FCoreObjectVersion
    {
        // Before any version changes were made
        BeforeCustomVersionWasAdded = 0,
        MaterialInputNativeSerialize,
        EnumProperties,
        SkeletalMaterialEditorDataStripping,
        FProperties,

        // -----<new versions can be added above this line>-------------------------------------------------
        VersionPlusOne,
        LatestVersion = VersionPlusOne - 1
    };
}
