# ObjectVersion

Namespace: UAssetAPI.UnrealTypes

An enum used to represent the global object version of UE4.

```csharp
public enum ObjectVersion
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [ObjectVersion](./uassetapi.unrealtypes.objectversion.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| VER_UE4_BLUEPRINT_VARS_NOT_READ_ONLY | 215 | Removed restriction on blueprint-exposed variables from being read-only |
| VER_UE4_STATIC_MESH_STORE_NAV_COLLISION | 216 | Added manually serialized element to UStaticMesh (precalculated nav collision) |
| VER_UE4_ATMOSPHERIC_FOG_DECAY_NAME_CHANGE | 217 | Changed property name for atmospheric fog |
| VER_UE4_SCENECOMP_TRANSLATION_TO_LOCATION | 218 | Change many properties/functions from Translation to Location |
| VER_UE4_MATERIAL_ATTRIBUTES_REORDERING | 219 | Material attributes reordering |
| VER_UE4_COLLISION_PROFILE_SETTING | 220 | Collision Profile setting has been added, and all components that exists has to be properly upgraded |
| VER_UE4_BLUEPRINT_SKEL_TEMPORARY_TRANSIENT | 221 | Making the blueprint's skeleton class transient |
| VER_UE4_BLUEPRINT_SKEL_SERIALIZED_AGAIN | 222 | Making the blueprint's skeleton class serialized again |
| VER_UE4_BLUEPRINT_SETS_REPLICATION | 223 | Blueprint now controls replication settings again |
| VER_UE4_WORLD_LEVEL_INFO | 224 | Added level info used by World browser |
| VER_UE4_AFTER_CAPSULE_HALF_HEIGHT_CHANGE | 225 | Changed capsule height to capsule half-height (afterwards) |
| VER_UE4_ADDED_NAMESPACE_AND_KEY_DATA_TO_FTEXT | 226 | Added Namepace, GUID (Key) and Flags to FText |
| VER_UE4_ATTENUATION_SHAPES | 227 | Attenuation shapes |
| VER_UE4_LIGHTCOMPONENT_USE_IES_TEXTURE_MULTIPLIER_ON_NON_IES_BRIGHTNESS | 228 | Use IES texture multiplier even when IES brightness is not being used |
| VER_UE4_REMOVE_INPUT_COMPONENTS_FROM_BLUEPRINTS | 229 | Removed InputComponent as a blueprint addable component |
| VER_UE4_VARK2NODE_USE_MEMBERREFSTRUCT | 230 | Use an FMemberReference struct in UK2Node_Variable |
| VER_UE4_REFACTOR_MATERIAL_EXPRESSION_SCENECOLOR_AND_SCENEDEPTH_INPUTS | 231 | Refactored material expression inputs for UMaterialExpressionSceneColor and UMaterialExpressionSceneDepth |
| VER_UE4_SPLINE_MESH_ORIENTATION | 232 | Spline meshes changed from Z forwards to configurable |
| VER_UE4_REVERB_EFFECT_ASSET_TYPE | 233 | Added ReverbEffect asset type |
| VER_UE4_MAX_TEXCOORD_INCREASED | 234 | changed max texcoords from 4 to 8 |
| VER_UE4_SPEEDTREE_STATICMESH | 235 | static meshes changed to support SpeedTrees |
| VER_UE4_LANDSCAPE_COMPONENT_LAZY_REFERENCES | 236 | Landscape component reference between landscape component and collision component |
| VER_UE4_SWITCH_CALL_NODE_TO_USE_MEMBER_REFERENCE | 237 | Refactored UK2Node_CallFunction to use FMemberReference |
| VER_UE4_ADDED_SKELETON_ARCHIVER_REMOVAL | 238 | Added fixup step to remove skeleton class references from blueprint objects |
| VER_UE4_ADDED_SKELETON_ARCHIVER_REMOVAL_SECOND_TIME | 239 | See above, take 2. |
| VER_UE4_BLUEPRINT_SKEL_CLASS_TRANSIENT_AGAIN | 240 | Making the skeleton class on blueprints transient |
| VER_UE4_ADD_COOKED_TO_UCLASS | 241 | UClass knows if it's been cooked |
| VER_UE4_DEPRECATED_STATIC_MESH_THUMBNAIL_PROPERTIES_REMOVED | 242 | Deprecated static mesh thumbnail properties were removed |
| VER_UE4_COLLECTIONS_IN_SHADERMAPID | 243 | Added collections in material shader map ids |
| VER_UE4_REFACTOR_MOVEMENT_COMPONENT_HIERARCHY | 244 | Renamed some Movement Component properties, added PawnMovementComponent |
| VER_UE4_FIX_TERRAIN_LAYER_SWITCH_ORDER | 245 | Swap UMaterialExpressionTerrainLayerSwitch::LayerUsed/LayerNotUsed the correct way round |
| VER_UE4_ALL_PROPS_TO_CONSTRAINTINSTANCE | 246 | Remove URB_ConstraintSetup |
| VER_UE4_LOW_QUALITY_DIRECTIONAL_LIGHTMAPS | 247 | Low quality directional lightmaps |
| VER_UE4_ADDED_NOISE_EMITTER_COMPONENT | 248 | Added NoiseEmitterComponent and removed related Pawn properties. |
| VER_UE4_ADD_TEXT_COMPONENT_VERTICAL_ALIGNMENT | 249 | Add text component vertical alignment |
| VER_UE4_ADDED_FBX_ASSET_IMPORT_DATA | 250 | Added AssetImportData for FBX asset types, deprecating SourceFilePath and SourceFileTimestamp |
| VER_UE4_REMOVE_LEVELBODYSETUP | 251 | Remove LevelBodySetup from ULevel |
| VER_UE4_REFACTOR_CHARACTER_CROUCH | 252 | Refactor character crouching |
| VER_UE4_SMALLER_DEBUG_MATERIALSHADER_UNIFORM_EXPRESSIONS | 253 | Trimmed down material shader debug information. |
| VER_UE4_APEX_CLOTH | 254 | APEX Clothing |
| VER_UE4_SAVE_COLLISIONRESPONSE_PER_CHANNEL | 255 | Change Collision Channel to save only modified ones than all of them. Note!!! Once we pass this CL, we can rename FCollisionResponseContainer enum values. We should rename to match ECollisionChannel |
| VER_UE4_ADDED_LANDSCAPE_SPLINE_EDITOR_MESH | 256 | Added Landscape Spline editor meshes |
| VER_UE4_CHANGED_MATERIAL_REFACTION_TYPE | 257 | Fixup input expressions for reading from refraction material attributes. |
| VER_UE4_REFACTOR_PROJECTILE_MOVEMENT | 258 | Refactor projectile movement, along with some other movement component work. |
| VER_UE4_REMOVE_PHYSICALMATERIALPROPERTY | 259 | Remove PhysicalMaterialProperty and replace with user defined enum |
| VER_UE4_PURGED_FMATERIAL_COMPILE_OUTPUTS | 260 | Removed all compile outputs from FMaterial |
| VER_UE4_ADD_COOKED_TO_LANDSCAPE | 261 | Ability to save cooked PhysX meshes to Landscape |
| VER_UE4_CONSUME_INPUT_PER_BIND | 262 | Change how input component consumption works |
| VER_UE4_SOUND_CLASS_GRAPH_EDITOR | 263 | Added new Graph based SoundClass Editor |
| VER_UE4_FIXUP_TERRAIN_LAYER_NODES | 264 | Fixed terrain layer node guids which was causing artifacts |
| VER_UE4_RETROFIT_CLAMP_EXPRESSIONS_SWAP | 265 | Added clamp min/max swap check to catch older materials |
| VER_UE4_REMOVE_LIGHT_MOBILITY_CLASSES | 266 | Remove static/movable/stationary light classes |
| VER_UE4_REFACTOR_PHYSICS_BLENDING | 267 | Refactor the way physics blending works to allow partial blending |
| VER_UE4_WORLD_LEVEL_INFO_UPDATED | 268 | WorldLevelInfo: Added reference to parent level and streaming distance |
| VER_UE4_STATIC_SKELETAL_MESH_SERIALIZATION_FIX | 269 | Fixed cooking of skeletal/static meshes due to bad serialization logic |
| VER_UE4_REMOVE_STATICMESH_MOBILITY_CLASSES | 270 | Removal of InterpActor and PhysicsActor |
| VER_UE4_REFACTOR_PHYSICS_TRANSFORMS | 271 | Refactor physics transforms |
| VER_UE4_REMOVE_ZERO_TRIANGLE_SECTIONS | 272 | Remove zero triangle sections from static meshes and compact material indices. |
| VER_UE4_CHARACTER_MOVEMENT_DECELERATION | 273 | Add param for deceleration in character movement instead of using acceleration. |
| VER_UE4_CAMERA_ACTOR_USING_CAMERA_COMPONENT | 274 | Made ACameraActor use a UCameraComponent for parameter storage, etc... |
| VER_UE4_CHARACTER_MOVEMENT_DEPRECATE_PITCH_ROLL | 275 | Deprecated some pitch/roll properties in CharacterMovementComponent |
| VER_UE4_REBUILD_TEXTURE_STREAMING_DATA_ON_LOAD | 276 | Rebuild texture streaming data on load for uncooked builds |
| VER_UE4_SUPPORT_32BIT_STATIC_MESH_INDICES | 277 | Add support for 32 bit index buffers for static meshes. |
| VER_UE4_ADDED_CHUNKID_TO_ASSETDATA_AND_UPACKAGE | 278 | Added streaming install ChunkID to AssetData and UPackage |
| VER_UE4_CHARACTER_DEFAULT_MOVEMENT_BINDINGS | 279 | Add flag to control whether Character blueprints receive default movement bindings. |
| VER_UE4_APEX_CLOTH_LOD | 280 | APEX Clothing LOD Info |
| VER_UE4_ATMOSPHERIC_FOG_CACHE_DATA | 281 | Added atmospheric fog texture data to be general |
| VAR_UE4_ARRAY_PROPERTY_INNER_TAGS | 282 | Arrays serialize their inner's tags |
| VER_UE4_KEEP_SKEL_MESH_INDEX_DATA | 283 | Skeletal mesh index data is kept in memory in game to support mesh merging. |
| VER_UE4_BODYSETUP_COLLISION_CONVERSION | 284 | Added compatibility for the body instance collision change |
| VER_UE4_REFLECTION_CAPTURE_COOKING | 285 | Reflection capture cooking |
| VER_UE4_REMOVE_DYNAMIC_VOLUME_CLASSES | 286 | Removal of DynamicTriggerVolume, DynamicBlockingVolume, DynamicPhysicsVolume |
| VER_UE4_STORE_HASCOOKEDDATA_FOR_BODYSETUP | 287 | Store an additional flag in the BodySetup to indicate whether there is any cooked data to load |
| VER_UE4_REFRACTION_BIAS_TO_REFRACTION_DEPTH_BIAS | 288 | Changed name of RefractionBias to RefractionDepthBias. |
| VER_UE4_REMOVE_SKELETALPHYSICSACTOR | 289 | Removal of SkeletalPhysicsActor |
| VER_UE4_PC_ROTATION_INPUT_REFACTOR | 290 | PlayerController rotation input refactor |
| VER_UE4_LANDSCAPE_PLATFORMDATA_COOKING | 291 | Landscape Platform Data cooking |
| VER_UE4_CREATEEXPORTS_CLASS_LINKING_FOR_BLUEPRINTS | 292 | Added call for linking classes in CreateExport to ensure memory is initialized properly |
| VER_UE4_REMOVE_NATIVE_COMPONENTS_FROM_BLUEPRINT_SCS | 293 | Remove native component nodes from the blueprint SimpleConstructionScript |
| VER_UE4_REMOVE_SINGLENODEINSTANCE | 294 | Removal of Single Node Instance |
| VER_UE4_CHARACTER_BRAKING_REFACTOR | 295 | Character movement braking changes |
| VER_UE4_VOLUME_SAMPLE_LOW_QUALITY_SUPPORT | 296 | Supported low quality lightmaps in volume samples |
| VER_UE4_SPLIT_TOUCH_AND_CLICK_ENABLES | 297 | Split bEnableTouchEvents out from bEnableClickEvents |
| VER_UE4_HEALTH_DEATH_REFACTOR | 298 | Health/Death refactor |
| VER_UE4_SOUND_NODE_ENVELOPER_CURVE_CHANGE | 299 | Moving USoundNodeEnveloper from UDistributionFloatConstantCurve to FRichCurve |
| VER_UE4_POINT_LIGHT_SOURCE_RADIUS | 300 | Moved SourceRadius to UPointLightComponent |
| VER_UE4_SCENE_CAPTURE_CAMERA_CHANGE | 301 | Scene capture actors based on camera actors. |
| VER_UE4_MOVE_SKELETALMESH_SHADOWCASTING | 302 | Moving SkeletalMesh shadow casting flag from LoD details to material |
| VER_UE4_CHANGE_SETARRAY_BYTECODE | 303 | Changing bytecode operators for creating arrays |
| VER_UE4_MATERIAL_INSTANCE_BASE_PROPERTY_OVERRIDES | 304 | Material Instances overriding base material properties. |
| VER_UE4_COMBINED_LIGHTMAP_TEXTURES | 305 | Combined top/bottom lightmap textures |
| VER_UE4_BUMPED_MATERIAL_EXPORT_GUIDS | 306 | Forced material lightmass guids to be regenerated |
| VER_UE4_BLUEPRINT_INPUT_BINDING_OVERRIDES | 307 | Allow overriding of parent class input bindings |
| VER_UE4_FIXUP_BODYSETUP_INVALID_CONVEX_TRANSFORM | 308 | Fix up convex invalid transform |
| VER_UE4_FIXUP_STIFFNESS_AND_DAMPING_SCALE | 309 | Fix up scale of physics stiffness and damping value |
| VER_UE4_REFERENCE_SKELETON_REFACTOR | 310 | Convert USkeleton and FBoneContrainer to using FReferenceSkeleton. |
| VER_UE4_K2NODE_REFERENCEGUIDS | 311 | Adding references to variable, function, and macro nodes to be able to update to renamed values |
| VER_UE4_FIXUP_ROOTBONE_PARENT | 312 | Fix up the 0th bone's parent bone index. |
| VER_UE4_MATERIAL_INSTANCE_BASE_PROPERTY_OVERRIDES_PHASE_2 | 314 | Material Instances overriding base material properties #2. |
| VER_UE4_CLASS_NOTPLACEABLE_ADDED | 315 | CLASS_Placeable becomes CLASS_NotPlaceable |
| VER_UE4_WORLD_LEVEL_INFO_LOD_LIST | 316 | Added LOD info list to a world tile description |
| VER_UE4_CHARACTER_MOVEMENT_VARIABLE_RENAMING_1 | 317 | CharacterMovement variable naming refactor |
| VER_UE4_FSLATESOUND_CONVERSION | 318 | FName properties containing sound names converted to FSlateSound properties |
| VER_UE4_WORLD_LEVEL_INFO_ZORDER | 319 | Added ZOrder to a world tile description |
| VER_UE4_PACKAGE_REQUIRES_LOCALIZATION_GATHER_FLAGGING | 320 | Added flagging of localization gather requirement to packages |
| VER_UE4_BP_ACTOR_VARIABLE_DEFAULT_PREVENTING | 321 | Preventing Blueprint Actor variables from having default values |
| VER_UE4_TEST_ANIMCOMP_CHANGE | 322 | Preventing Blueprint Actor variables from having default values |
| VER_UE4_EDITORONLY_BLUEPRINTS | 323 | Class as primary asset, name convention changed |
| VER_UE4_EDGRAPHPINTYPE_SERIALIZATION | 324 | Custom serialization for FEdGraphPinType |
| VER_UE4_NO_MIRROR_BRUSH_MODEL_COLLISION | 325 | Stop generating 'mirrored' cooked mesh for Brush and Model components |
| VER_UE4_CHANGED_CHUNKID_TO_BE_AN_ARRAY_OF_CHUNKIDS | 326 | Changed ChunkID to be an array of IDs. |
| VER_UE4_WORLD_NAMED_AFTER_PACKAGE | 327 | Worlds have been renamed from "TheWorld" to be named after the package containing them |
| VER_UE4_SKY_LIGHT_COMPONENT | 328 | Added sky light component |
| VER_UE4_WORLD_LAYER_ENABLE_DISTANCE_STREAMING | 329 | Added Enable distance streaming flag to FWorldTileLayer |
| VER_UE4_REMOVE_ZONES_FROM_MODEL | 330 | Remove visibility/zone information from UModel |
| VER_UE4_FIX_ANIMATIONBASEPOSE_SERIALIZATION | 331 | Fix base pose serialization |
| VER_UE4_SUPPORT_8_BONE_INFLUENCES_SKELETAL_MESHES | 332 | Support for up to 8 skinning influences per vertex on skeletal meshes (on non-gpu vertices) |
| VER_UE4_ADD_OVERRIDE_GRAVITY_FLAG | 333 | Add explicit bOverrideGravity to world settings |
| VER_UE4_SUPPORT_GPUSKINNING_8_BONE_INFLUENCES | 334 | Support for up to 8 skinning influences per vertex on skeletal meshes (on gpu vertices) |
| VER_UE4_ANIM_SUPPORT_NONUNIFORM_SCALE_ANIMATION | 335 | Supporting nonuniform scale animation |
| VER_UE4_ENGINE_VERSION_OBJECT | 336 | Engine version is stored as a FEngineVersion object rather than changelist number |
| VER_UE4_PUBLIC_WORLDS | 337 | World assets now have RF_Public |
| VER_UE4_SKELETON_GUID_SERIALIZATION | 338 | Skeleton Guid |
| VER_UE4_CHARACTER_MOVEMENT_WALKABLE_FLOOR_REFACTOR | 339 | Character movement WalkableFloor refactor |
| VER_UE4_INVERSE_SQUARED_LIGHTS_DEFAULT | 340 | Lights default to inverse squared |
| VER_UE4_DISABLED_SCRIPT_LIMIT_BYTECODE | 341 | Disabled SCRIPT_LIMIT_BYTECODE_TO_64KB |
| VER_UE4_PRIVATE_REMOTE_ROLE | 342 | Made remote role private, exposed bReplicates |
| VER_UE4_FOLIAGE_STATIC_MOBILITY | 343 | Fix up old foliage components to have static mobility (superseded by VER_UE4_FOLIAGE_MOVABLE_MOBILITY) |
| VER_UE4_BUILD_SCALE_VECTOR | 344 | Change BuildScale from a float to a vector |
| VER_UE4_FOLIAGE_COLLISION | 345 | After implementing foliage collision, need to disable collision on old foliage instances |
| VER_UE4_SKY_BENT_NORMAL | 346 | Added sky bent normal to indirect lighting cache |
| VER_UE4_LANDSCAPE_COLLISION_DATA_COOKING | 347 | Added cooking for landscape collision data |
| VER_UE4_MORPHTARGET_CPU_TANGENTZDELTA_FORMATCHANGE | 348 | Convert CPU tangent Z delta to vector from PackedNormal since we don't get any benefit other than memory we still convert all to FVector in CPU time whenever any calculation |
| VER_UE4_SOFT_CONSTRAINTS_USE_MASS | 349 | Soft constraint limits will implicitly use the mass of the bodies |
| VER_UE4_REFLECTION_DATA_IN_PACKAGES | 350 | Reflection capture data saved in packages |
| VER_UE4_FOLIAGE_MOVABLE_MOBILITY | 351 | Fix up old foliage components to have movable mobility (superseded by VER_UE4_FOLIAGE_STATIC_LIGHTING_SUPPORT) |
| VER_UE4_UNDO_BREAK_MATERIALATTRIBUTES_CHANGE | 352 | Undo BreakMaterialAttributes changes as it broke old content |
| VER_UE4_ADD_CUSTOMPROFILENAME_CHANGE | 353 | Now Default custom profile name isn't NONE anymore due to copy/paste not working properly with it |
| VER_UE4_FLIP_MATERIAL_COORDS | 354 | Permanently flip and scale material expression coordinates |
| VER_UE4_MEMBERREFERENCE_IN_PINTYPE | 355 | PinSubCategoryMemberReference added to FEdGraphPinType |
| VER_UE4_VEHICLES_UNIT_CHANGE | 356 | Vehicles use Nm for Torque instead of cm and RPM instead of rad/s |
| VER_UE4_ANIMATION_REMOVE_NANS | 357 | removes NANs from all animations when loaded now importing should detect NaNs, so we should not have NaNs in source data |
| VER_UE4_SKELETON_ASSET_PROPERTY_TYPE_CHANGE | 358 | Change skeleton preview attached assets property type |
| VER_UE4_FIX_BLUEPRINT_VARIABLE_FLAGS | 359 | Fix some blueprint variables that have the CPF_DisableEditOnTemplate flag set when they shouldn't |
| VER_UE4_VEHICLES_UNIT_CHANGE2 | 360 | Vehicles use Nm for Torque instead of cm and RPM instead of rad/s part two (missed conversion for some variables |
| VER_UE4_UCLASS_SERIALIZE_INTERFACES_AFTER_LINKING | 361 | Changed order of interface class serialization |
| VER_UE4_STATIC_MESH_SCREEN_SIZE_LODS | 362 | Change from LOD distances to display factors |
| VER_UE4_FIX_MATERIAL_COORDS | 363 | Requires test of material coords to ensure they're saved correctly |
| VER_UE4_SPEEDTREE_WIND_V7 | 364 | Changed SpeedTree wind presets to v7 |
| VER_UE4_LOAD_FOR_EDITOR_GAME | 365 | NeedsLoadForEditorGame added |
| VER_UE4_SERIALIZE_RICH_CURVE_KEY | 366 | Manual serialization of FRichCurveKey to save space |
| VER_UE4_MOVE_LANDSCAPE_MICS_AND_TEXTURES_WITHIN_LEVEL | 367 | Change the outer of ULandscapeMaterialInstanceConstants and Landscape-related textures to the level in which they reside |
| VER_UE4_FTEXT_HISTORY | 368 | FTexts have creation history data, removed Key, Namespaces, and SourceString |
| VER_UE4_FIX_MATERIAL_COMMENTS | 369 | Shift comments to the left to contain expressions properly |
| VER_UE4_STORE_BONE_EXPORT_NAMES | 370 | Bone names stored as FName means that we can't guarantee the correct case on export, now we store a separate string for export purposes only |
| VER_UE4_MESH_EMITTER_INITIAL_ORIENTATION_DISTRIBUTION | 371 | changed mesh emitter initial orientation to distribution |
| VER_UE4_DISALLOW_FOLIAGE_ON_BLUEPRINTS | 372 | Foliage on blueprints causes crashes |
| VER_UE4_FIXUP_MOTOR_UNITS | 373 | change motors to use revolutions per second instead of rads/second |
| VER_UE4_DEPRECATED_MOVEMENTCOMPONENT_MODIFIED_SPEEDS | 374 | deprecated MovementComponent functions including "ModifiedMaxSpeed" et al |
| VER_UE4_RENAME_CANBECHARACTERBASE | 375 | rename CanBeCharacterBase |
| VER_UE4_GAMEPLAY_TAG_CONTAINER_TAG_TYPE_CHANGE | 376 | Change GameplayTagContainers to have FGameplayTags instead of FNames; Required to fix-up native serialization |
| VER_UE4_FOLIAGE_SETTINGS_TYPE | 377 | Change from UInstancedFoliageSettings to UFoliageType, and change the api from being keyed on UStaticMesh* to UFoliageType* |
| VER_UE4_STATIC_SHADOW_DEPTH_MAPS | 378 | Lights serialize static shadow depth maps |
| VER_UE4_ADD_TRANSACTIONAL_TO_DATA_ASSETS | 379 | Add RF_Transactional to data assets, fixing undo problems when editing them |
| VER_UE4_ADD_LB_WEIGHTBLEND | 380 | Change LB_AlphaBlend to LB_WeightBlend in ELandscapeLayerBlendType |
| VER_UE4_ADD_ROOTCOMPONENT_TO_FOLIAGEACTOR | 381 | Add root component to an foliage actor, all foliage cluster components will be attached to a root |
| VER_UE4_FIX_MATERIAL_PROPERTY_OVERRIDE_SERIALIZE | 382 | FMaterialInstanceBasePropertyOverrides didn't use proper UObject serialize |
| VER_UE4_ADD_LINEAR_COLOR_SAMPLER | 383 | Addition of linear color sampler. color sample type is changed to linear sampler if source texture !sRGB |
| VER_UE4_ADD_STRING_ASSET_REFERENCES_MAP | 384 | Added StringAssetReferencesMap to support renames of FStringAssetReference properties. |
| VER_UE4_BLUEPRINT_USE_SCS_ROOTCOMPONENT_SCALE | 385 | Apply scale from SCS RootComponent details in the Blueprint Editor to new actor instances at construction time |
| VER_UE4_LEVEL_STREAMING_DRAW_COLOR_TYPE_CHANGE | 386 | Changed level streaming to have a linear color since the visualization doesn't gamma correct. |
| VER_UE4_CLEAR_NOTIFY_TRIGGERS | 387 | Cleared end triggers from non-state anim notifies |
| VER_UE4_SKELETON_ADD_SMARTNAMES | 388 | Convert old curve names stored in anim assets into skeleton smartnames |
| VER_UE4_ADDED_CURRENCY_CODE_TO_FTEXT | 389 | Added the currency code field to FTextHistory_AsCurrency |
| VER_UE4_ENUM_CLASS_SUPPORT | 390 | Added support for C++11 enum classes |
| VER_UE4_FIXUP_WIDGET_ANIMATION_CLASS | 391 | Fixup widget animation class |
| VER_UE4_SOUND_COMPRESSION_TYPE_ADDED | 392 | USoundWave objects now contain details about compression scheme used. |
| VER_UE4_AUTO_WELDING | 393 | Bodies will automatically weld when attached |
| VER_UE4_RENAME_CROUCHMOVESCHARACTERDOWN | 394 | Rename UCharacterMovementComponent::bCrouchMovesCharacterDown |
| VER_UE4_LIGHTMAP_MESH_BUILD_SETTINGS | 395 | Lightmap parameters in FMeshBuildSettings |
| VER_UE4_RENAME_SM3_TO_ES3_1 | 396 | Rename SM3 to ES3_1 and updates featurelevel material node selector |
| VER_UE4_DEPRECATE_UMG_STYLE_ASSETS | 397 | Deprecated separate style assets for use in UMG |
| VER_UE4_POST_DUPLICATE_NODE_GUID | 398 | Duplicating Blueprints will regenerate NodeGuids after this version |
| VER_UE4_RENAME_CAMERA_COMPONENT_VIEW_ROTATION | 399 | Rename USpringArmComponent::bUseControllerViewRotation to bUsePawnViewRotation, Rename UCameraComponent::bUseControllerViewRotation to bUsePawnViewRotation (and change the default value) |
| VER_UE4_CASE_PRESERVING_FNAME | 400 | Changed FName to be case preserving |
| VER_UE4_RENAME_CAMERA_COMPONENT_CONTROL_ROTATION | 401 | Rename USpringArmComponent::bUsePawnViewRotation to bUsePawnControlRotation, Rename UCameraComponent::bUsePawnViewRotation to bUsePawnControlRotation |
| VER_UE4_FIX_REFRACTION_INPUT_MASKING | 402 | Fix bad refraction material attribute masks |
| VER_UE4_GLOBAL_EMITTER_SPAWN_RATE_SCALE | 403 | A global spawn rate for emitters. |
| VER_UE4_CLEAN_DESTRUCTIBLE_SETTINGS | 404 | Cleanup destructible mesh settings |
| VER_UE4_CHARACTER_MOVEMENT_UPPER_IMPACT_BEHAVIOR | 405 | CharacterMovementComponent refactor of AdjustUpperHemisphereImpact and deprecation of some associated vars. |
| VER_UE4_BP_MATH_VECTOR_EQUALITY_USES_EPSILON | 406 | Changed Blueprint math equality functions for vectors and rotators to operate as a "nearly" equals rather than "exact" |
| VER_UE4_FOLIAGE_STATIC_LIGHTING_SUPPORT | 407 | Static lighting support was re-added to foliage, and mobility was returned to static |
| VER_UE4_SLATE_COMPOSITE_FONTS | 408 | Added composite fonts to Slate font info |
| VER_UE4_REMOVE_SAVEGAMESUMMARY | 409 | Remove UDEPRECATED_SaveGameSummary, required for UWorld::Serialize |
| VER_UE4_REMOVE_SKELETALMESH_COMPONENT_BODYSETUP_SERIALIZATION | 410 | Remove bodyseutp serialization from skeletal mesh component |
| VER_UE4_SLATE_BULK_FONT_DATA | 411 | Made Slate font data use bulk data to store the embedded font data |
| VER_UE4_ADD_PROJECTILE_FRICTION_BEHAVIOR | 412 | Add new friction behavior in ProjectileMovementComponent. |
| VER_UE4_MOVEMENTCOMPONENT_AXIS_SETTINGS | 413 | Add axis settings enum to MovementComponent. |
| VER_UE4_GRAPH_INTERACTIVE_COMMENTBUBBLES | 414 | Switch to new interactive comments, requires boundry conversion to preserve previous states |
| VER_UE4_LANDSCAPE_SERIALIZE_PHYSICS_MATERIALS | 415 | Landscape serializes physical materials for collision objects |
| VER_UE4_RENAME_WIDGET_VISIBILITY | 416 | Rename Visiblity on widgets to Visibility |
| VER_UE4_ANIMATION_ADD_TRACKCURVES | 417 | add track curves for animation |
| VER_UE4_MONTAGE_BRANCHING_POINT_REMOVAL | 418 | Removed BranchingPoints from AnimMontages and converted them to regular AnimNotifies. |
| VER_UE4_BLUEPRINT_ENFORCE_CONST_IN_FUNCTION_OVERRIDES | 419 | Enforce const-correctness in Blueprint implementations of native C++ const class methods |
| VER_UE4_ADD_PIVOT_TO_WIDGET_COMPONENT | 420 | Added pivot to widget components, need to load old versions as a 0,0 pivot, new default is 0.5,0.5 |
| VER_UE4_PAWN_AUTO_POSSESS_AI | 421 | Added finer control over when AI Pawns are automatically possessed. Also renamed Pawn.AutoPossess to Pawn.AutoPossessPlayer indicate this was a setting for players and not AI. |
| VER_UE4_FTEXT_HISTORY_DATE_TIMEZONE | 422 | Added serialization of timezone to FTextHistory for AsDate operations. |
| VER_UE4_SORT_ACTIVE_BONE_INDICES | 423 | Sort ActiveBoneIndices on lods so that we can avoid doing it at run time |
| VER_UE4_PERFRAME_MATERIAL_UNIFORM_EXPRESSIONS | 424 | Added per-frame material uniform expressions |
| VER_UE4_MIKKTSPACE_IS_DEFAULT | 425 | Make MikkTSpace the default tangent space calculation method for static meshes. |
| VER_UE4_LANDSCAPE_GRASS_COOKING | 426 | Only applies to cooked files, grass cooking support. |
| VER_UE4_FIX_SKEL_VERT_ORIENT_MESH_PARTICLES | 427 | Fixed code for using the bOrientMeshEmitters property. |
| VER_UE4_LANDSCAPE_STATIC_SECTION_OFFSET | 428 | Do not change landscape section offset on load under world composition |
| VER_UE4_ADD_MODIFIERS_RUNTIME_GENERATION | 429 | New options for navigation data runtime generation (static, modifiers only, dynamic) |
| VER_UE4_MATERIAL_MASKED_BLENDMODE_TIDY | 430 | Tidied up material's handling of masked blend mode. |
| VER_UE4_MERGED_ADD_MODIFIERS_RUNTIME_GENERATION_TO_4_7_DEPRECATED | 431 | Original version of VER_UE4_MERGED_ADD_MODIFIERS_RUNTIME_GENERATION_TO_4_7; renumbered to prevent blocking promotion in main. |
| VER_UE4_AFTER_MERGED_ADD_MODIFIERS_RUNTIME_GENERATION_TO_4_7_DEPRECATED | 432 | Original version of VER_UE4_AFTER_MERGED_ADD_MODIFIERS_RUNTIME_GENERATION_TO_4_7; renumbered to prevent blocking promotion in main. |
| VER_UE4_MERGED_ADD_MODIFIERS_RUNTIME_GENERATION_TO_4_7 | 433 | After merging VER_UE4_ADD_MODIFIERS_RUNTIME_GENERATION into 4.7 branch |
| VER_UE4_AFTER_MERGING_ADD_MODIFIERS_RUNTIME_GENERATION_TO_4_7 | 434 | After merging VER_UE4_ADD_MODIFIERS_RUNTIME_GENERATION into 4.7 branch |
| VER_UE4_SERIALIZE_LANDSCAPE_GRASS_DATA | 435 | Landscape grass weightmap data is now generated in the editor and serialized. |
| VER_UE4_OPTIONALLY_CLEAR_GPU_EMITTERS_ON_INIT | 436 | New property to optionally prevent gpu emitters clearing existing particles on Init(). |
| VER_UE4_SERIALIZE_LANDSCAPE_GRASS_DATA_MATERIAL_GUID | 437 | Also store the Material guid with the landscape grass data |
| VER_UE4_BLUEPRINT_GENERATED_CLASS_COMPONENT_TEMPLATES_PUBLIC | 438 | Make sure that all template components from blueprint generated classes are flagged as public |
| VER_UE4_ACTOR_COMPONENT_CREATION_METHOD | 439 | Split out creation method on ActorComponents to distinguish between native, instance, and simple or user construction script |
| VER_UE4_K2NODE_EVENT_MEMBER_REFERENCE | 440 | K2Node_Event now uses FMemberReference for handling references |
| VER_UE4_STRUCT_GUID_IN_PROPERTY_TAG | 441 | FPropertyTag stores GUID of struct |
| VER_UE4_REMOVE_UNUSED_UPOLYS_FROM_UMODEL | 442 | Remove unused UPolys from UModel cooked content |
| VER_UE4_REBUILD_HIERARCHICAL_INSTANCE_TREES | 443 | This doesn't do anything except trigger a rebuild on HISMC cluster trees, in this case to get a good "occlusion query" level |
| VER_UE4_PACKAGE_SUMMARY_HAS_COMPATIBLE_ENGINE_VERSION | 444 | Package summary includes an CompatibleWithEngineVersion field, separately to the version it's saved with |
| VER_UE4_TRACK_UCS_MODIFIED_PROPERTIES | 445 | Track UCS modified properties on Actor Components |
| VER_UE4_LANDSCAPE_SPLINE_CROSS_LEVEL_MESHES | 446 | Allowed landscape spline meshes to be stored into landscape streaming levels rather than the spline's level |
| VER_UE4_DEPRECATE_USER_WIDGET_DESIGN_SIZE | 447 | Deprecate the variables used for sizing in the designer on UUserWidget |
| VER_UE4_ADD_EDITOR_VIEWS | 448 | Make the editor views array dynamically sized |
| VER_UE4_FOLIAGE_WITH_ASSET_OR_CLASS | 449 | Updated foliage to work with either FoliageType assets or blueprint classes |
| VER_UE4_BODYINSTANCE_BINARY_SERIALIZATION | 450 | Allows PhysicsSerializer to serialize shapes and actors for faster load times |
| VER_UE4_SERIALIZE_BLUEPRINT_EVENTGRAPH_FASTCALLS_IN_UFUNCTION | 451 | Added fastcall data serialization directly in UFunction |
| VER_UE4_INTERPCURVE_SUPPORTS_LOOPING | 452 | Changes to USplineComponent and FInterpCurve |
| VER_UE4_MATERIAL_INSTANCE_BASE_PROPERTY_OVERRIDES_DITHERED_LOD_TRANSITION | 453 | Material Instances overriding base material LOD transitions |
| VER_UE4_SERIALIZE_LANDSCAPE_ES2_TEXTURES | 454 | Serialize ES2 textures separately rather than overwriting the properties used on other platforms |
| VER_UE4_CONSTRAINT_INSTANCE_MOTOR_FLAGS | 455 | Constraint motor velocity is broken into per-component |
| VER_UE4_SERIALIZE_PINTYPE_CONST | 456 | Serialize bIsConst in FEdGraphPinType |
| VER_UE4_LIBRARY_CATEGORIES_AS_FTEXT | 457 | Change UMaterialFunction::LibraryCategories to LibraryCategoriesText (old assets were saved before auto-conversion of FArrayProperty was possible) |
| VER_UE4_SKIP_DUPLICATE_EXPORTS_ON_SAVE_PACKAGE | 458 | Check for duplicate exports while saving packages. |
| VER_UE4_SERIALIZE_TEXT_IN_PACKAGES | 459 | Pre-gathering of gatherable, localizable text in packages to optimize text gathering operation times |
| VER_UE4_ADD_BLEND_MODE_TO_WIDGET_COMPONENT | 460 | Added pivot to widget components, need to load old versions as a 0,0 pivot, new default is 0.5,0.5 |
| VER_UE4_NEW_LIGHTMASS_PRIMITIVE_SETTING | 461 | Added lightmass primitive setting |
| VER_UE4_REPLACE_SPRING_NOZ_PROPERTY | 462 | Deprecate NoZSpring property on spring nodes to be replaced with TranslateZ property |
| VER_UE4_TIGHTLY_PACKED_ENUMS | 463 | Keep enums tight and serialize their values as pairs of FName and value. Don't insert dummy values. |
| VER_UE4_ASSET_IMPORT_DATA_AS_JSON | 464 | Changed Asset import data to serialize file meta data as JSON |
| VER_UE4_TEXTURE_LEGACY_GAMMA | 465 | Legacy gamma support for textures. |
| VER_UE4_ADDED_NATIVE_SERIALIZATION_FOR_IMMUTABLE_STRUCTURES | 466 | Added WithSerializer for basic native structures like FVector, FColor etc to improve serialization performance |
| VER_UE4_DEPRECATE_UMG_STYLE_OVERRIDES | 467 | Deprecated attributes that override the style on UMG widgets |
| VER_UE4_STATIC_SHADOWMAP_PENUMBRA_SIZE | 468 | Shadowmap penumbra size stored |
| VER_UE4_NIAGARA_DATA_OBJECT_DEV_UI_FIX | 469 | Fix BC on Niagara effects from the data object and dev UI changes. |
| VER_UE4_FIXED_DEFAULT_ORIENTATION_OF_WIDGET_COMPONENT | 470 | Fixed the default orientation of widget component so it faces down +x |
| VER_UE4_REMOVED_MATERIAL_USED_WITH_UI_FLAG | 471 | Removed bUsedWithUI flag from UMaterial and replaced it with a new material domain for UI |
| VER_UE4_CHARACTER_MOVEMENT_ADD_BRAKING_FRICTION | 472 | Added braking friction separate from turning friction. |
| VER_UE4_BSP_UNDO_FIX | 473 | Removed TTransArrays from UModel |
| VER_UE4_DYNAMIC_PARAMETER_DEFAULT_VALUE | 474 | Added default value to dynamic parameter. |
| VER_UE4_STATIC_MESH_EXTENDED_BOUNDS | 475 | Added ExtendedBounds to StaticMesh |
| VER_UE4_ADDED_NON_LINEAR_TRANSITION_BLENDS | 476 | Added non-linear blending to anim transitions, deprecating old types |
| VER_UE4_AO_MATERIAL_MASK | 477 | AO Material Mask texture |
| VER_UE4_NAVIGATION_AGENT_SELECTOR | 478 | Replaced navigation agents selection with single structure |
| VER_UE4_MESH_PARTICLE_COLLISIONS_CONSIDER_PARTICLE_SIZE | 479 | Mesh particle collisions consider particle size. |
| VER_UE4_BUILD_MESH_ADJ_BUFFER_FLAG_EXPOSED | 480 | Adjacency buffer building no longer automatically handled based on triangle count, user-controlled |
| VER_UE4_MAX_ANGULAR_VELOCITY_DEFAULT | 481 | Change the default max angular velocity |
| VER_UE4_APEX_CLOTH_TESSELLATION | 482 | Build Adjacency index buffer for clothing tessellation |
| VER_UE4_DECAL_SIZE | 483 | Added DecalSize member, solved backward compatibility |
| VER_UE4_KEEP_ONLY_PACKAGE_NAMES_IN_STRING_ASSET_REFERENCES_MAP | 484 | Keep only package names in StringAssetReferencesMap |
| VER_UE4_COOKED_ASSETS_IN_EDITOR_SUPPORT | 485 | Support sound cue not saving out editor only data |
| VER_UE4_DIALOGUE_WAVE_NAMESPACE_AND_CONTEXT_CHANGES | 486 | Updated dialogue wave localization gathering logic. |
| VER_UE4_MAKE_ROT_RENAME_AND_REORDER | 487 | Renamed MakeRot MakeRotator and rearranged parameters. |
| VER_UE4_K2NODE_VAR_REFERENCEGUIDS | 488 | K2Node_Variable will properly have the VariableReference Guid set if available |
| VER_UE4_SOUND_CONCURRENCY_PACKAGE | 489 | Added support for sound concurrency settings structure and overrides |
| VER_UE4_USERWIDGET_DEFAULT_FOCUSABLE_FALSE | 490 | Changing the default value for focusable user widgets to false |
| VER_UE4_BLUEPRINT_CUSTOM_EVENT_CONST_INPUT | 491 | Custom event nodes implicitly set 'const' on array and non-array pass-by-reference input params |
| VER_UE4_USE_LOW_PASS_FILTER_FREQ | 492 | Renamed HighFrequencyGain to LowPassFilterFrequency |
| VER_UE4_NO_ANIM_BP_CLASS_IN_GAMEPLAY_CODE | 493 | UAnimBlueprintGeneratedClass can be replaced by a dynamic class. Use TSubclassOf UAnimInstance instead. |
| VER_UE4_SCS_STORES_ALLNODES_ARRAY | 494 | The SCS keeps a list of all nodes in its hierarchy rather than recursively building it each time it is requested |
| VER_UE4_FBX_IMPORT_DATA_RANGE_ENCAPSULATION | 495 | Moved StartRange and EndRange in UFbxAnimSequenceImportData to use FInt32Interval |
| VER_UE4_CAMERA_COMPONENT_ATTACH_TO_ROOT | 496 | Adding a new root scene component to camera component |
| VER_UE4_INSTANCED_STEREO_UNIFORM_UPDATE | 497 | Updating custom material expression nodes for instanced stereo implementation |
| VER_UE4_STREAMABLE_TEXTURE_MIN_MAX_DISTANCE | 498 | Texture streaming min and max distance to handle HLOD |
| VER_UE4_INJECT_BLUEPRINT_STRUCT_PIN_CONVERSION_NODES | 499 | Fixing up invalid struct-to-struct pin connections by injecting available conversion nodes |
| VER_UE4_INNER_ARRAY_TAG_INFO | 500 | Saving tag data for Array Property's inner property |
| VER_UE4_FIX_SLOT_NAME_DUPLICATION | 501 | Fixed duplicating slot node names in skeleton due to skeleton preload on compile |
| VER_UE4_STREAMABLE_TEXTURE_AABB | 502 | Texture streaming using AABBs instead of Spheres |
| VER_UE4_PROPERTY_GUID_IN_PROPERTY_TAG | 503 | FPropertyTag stores GUID of property |
| VER_UE4_NAME_HASHES_SERIALIZED | 504 | Name table hashes are calculated and saved out rather than at load time |
| VER_UE4_INSTANCED_STEREO_UNIFORM_REFACTOR | 505 | Updating custom material expression nodes for instanced stereo implementation refactor |
| VER_UE4_COMPRESSED_SHADER_RESOURCES | 506 | Added compression to the shader resource for memory savings |
| VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS | 507 | Cooked files contain the dependency graph for the event driven loader (the serialization is largely independent of the use of the new loader) |
| VER_UE4_TemplateIndex_IN_COOKED_EXPORTS | 508 | Cooked files contain the TemplateIndex used by the event driven loader (the serialization is largely independent of the use of the new loader, i.e. this will be null if cooking for the old loader) |
| VER_UE4_PROPERTY_TAG_SET_MAP_SUPPORT | 509 | FPropertyTag includes contained type(s) for Set and Map properties |
| VER_UE4_ADDED_SEARCHABLE_NAMES | 510 | Added SearchableNames to the package summary and asset registry |
| VER_UE4_64BIT_EXPORTMAP_SERIALSIZES | 511 | Increased size of SerialSize and SerialOffset in export map entries to 64 bit, allow support for bigger files |
| VER_UE4_SKYLIGHT_MOBILE_IRRADIANCE_MAP | 512 | Sky light stores IrradianceMap for mobile renderer. |
| VER_UE4_ADDED_SWEEP_WHILE_WALKING_FLAG | 513 | Added flag to control sweep behavior while walking in UCharacterMovementComponent. |
| VER_UE4_ADDED_SOFT_OBJECT_PATH | 514 | StringAssetReference changed to SoftObjectPath and swapped to serialize as a name+string instead of a string |
| VER_UE4_POINTLIGHT_SOURCE_ORIENTATION | 515 | Changed the source orientation of point lights to match spot lights (z axis) |
| VER_UE4_ADDED_PACKAGE_SUMMARY_LOCALIZATION_ID | 516 | LocalizationId has been added to the package summary (editor-only) |
| VER_UE4_FIX_WIDE_STRING_CRC | 517 | Fixed case insensitive hashes of wide strings containing character values from 128-255 |
| VER_UE4_ADDED_PACKAGE_OWNER | 518 | Added package owner to allow private references |
| VER_UE4_SKINWEIGHT_PROFILE_DATA_LAYOUT_CHANGES | 519 | Changed the data layout for skin weight profile data |
| VER_UE4_NON_OUTER_PACKAGE_IMPORT | 520 | Added import that can have package different than their outer |
| VER_UE4_ASSETREGISTRY_DEPENDENCYFLAGS | 521 | Added DependencyFlags to AssetRegistry |
| VER_UE4_CORRECT_LICENSEE_FLAG | 522 | Fixed corrupt licensee flag in 4.26 assets |
| VER_UE4_AUTOMATIC_VERSION | 522 | The newest specified version of the Unreal Engine. |
