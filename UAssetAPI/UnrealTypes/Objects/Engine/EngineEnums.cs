using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.UnrealTypes
{

	// Enum Engine.ETextGender
	public enum ETextGender : byte {
		Masculine = 0,
		Feminine = 1,
		Neuter = 2,
		ETextGender_MAX = 3
	};

	// Enum Engine.EFormatArgumentType
	public enum EFormatArgumentType : byte {
		Int = 0,
		UInt = 1,
		Float = 2,
		Double = 3,
		Text = 4,
		Gender = 5,
		EFormatArgumentType_MAX = 6
	};

	// Enum Engine.EEndPlayReason
	public enum EEndPlayReason : byte {
		Destroyed = 0,
		LevelTransition = 1,
		EndPlayInEditor = 2,
		RemovedFromWorld = 3,
		Quit = 4,
		EEndPlayReason_MAX = 5
	};

	// Enum Engine.ETickingGroup
	public enum ETickingGroup : byte {
		TG_PrePhysics = 0,
		TG_StartPhysics = 1,
		TG_DuringPhysics = 2,
		TG_EndPhysics = 3,
		TG_PostPhysics = 4,
		TG_PostUpdateWork = 5,
		TG_LastDemotable = 6,
		TG_NewlySpawned = 7,
		TG_MAX = 8
	};

	// Enum Engine.EComponentCreationMethod
	public enum EComponentCreationMethod : byte {
		Native = 0,
		SimpleConstructionScript = 1,
		UserConstructionScript = 2,
		Instance = 3,
		EComponentCreationMethod_MAX = 4
	};

	// Enum Engine.ETemperatureSeverityType
	public enum ETemperatureSeverityType : byte {
		Unknown = 0,
		Good = 1,
		Bad = 2,
		Serious = 3,
		Critical = 4,
		NumSeverities = 5,
		ETemperatureSeverityType_MAX = 6
	};

	// Enum Engine.EPlaneConstraintAxisSetting
	public enum EPlaneConstraintAxisSetting : byte {
		Custom = 0,
		X = 1,
		Y = 2,
		Z = 3,
		UseGlobalPhysicsSetting = 4,
		EPlaneConstraintAxisSetting_MAX = 5
	};

	// Enum Engine.EInterpToBehaviourType
	public enum EInterpToBehaviourType : byte {
		OneShot = 0,
		OneShot_Reverse = 1,
		Loop_Reset = 2,
		PingPong = 3,
		EInterpToBehaviourType_MAX = 4
	};

	// Enum Engine.ETeleportType
	public enum ETeleportType : byte {
		None = 0,
		TeleportPhysics = 1,
		ResetPhysics = 2,
		ETeleportType_MAX = 3
	};

	// Enum Engine.EPlatformInterfaceDataType
	public enum EPlatformInterfaceDataType : byte {
		PIDT_None = 0,
		PIDT_Int = 1,
		PIDT_Float = 2,
		PIDT_String = 3,
		PIDT_Object = 4,
		PIDT_Custom = 5,
		PIDT_MAX = 6
	};

	// Enum Engine.EMovementMode
	public enum EMovementMode : byte {
		MOVE_None = 0,
		MOVE_Walking = 1,
		MOVE_NavWalking = 2,
		MOVE_Falling = 3,
		MOVE_Swimming = 4,
		MOVE_Flying = 5,
		MOVE_Custom = 6,
		MOVE_MAX = 7
	};

	// Enum Engine.ENetworkFailure
	public enum ENetworkFailure : byte {
		NetDriverAlreadyExists = 0,
		NetDriverCreateFailure = 1,
		NetDriverListenFailure = 2,
		ConnectionLost = 3,
		ConnectionTimeout = 4,
		FailureReceived = 5,
		OutdatedClient = 6,
		OutdatedServer = 7,
		PendingConnectionFailure = 8,
		NetGuidMismatch = 9,
		NetChecksumMismatch = 10,
		ENetworkFailure_MAX = 11
	};

	// Enum Engine.ETravelFailure
	public enum ETravelFailure : byte {
		NoLevel = 0,
		LoadMapFailure = 1,
		InvalidURL = 2,
		PackageMissing = 3,
		PackageVersion = 4,
		NoDownload = 5,
		TravelFailure = 6,
		CheatCommands = 7,
		PendingNetGameCreateFailure = 8,
		CloudSaveFailure = 9,
		ServerTravelFailure = 10,
		ClientTravelFailure = 11,
		ETravelFailure_MAX = 12
	};

	// Enum Engine.EScreenOrientation
	public enum EScreenOrientation : byte {
		Unknown = 0,
		Portrait = 1,
		PortraitUpsideDown = 2,
		LandscapeLeft = 3,
		LandscapeRight = 4,
		FaceUp = 5,
		FaceDown = 6,
		EScreenOrientation_MAX = 7
	};

	// Enum Engine.EApplicationState
	public enum EApplicationState : byte {
		Unknown = 0,
		Inactive = 1,
		Background = 2,
		Active = 3,
		EApplicationState_MAX = 4
	};

	// Enum Engine.EObjectTypeQuery
	public enum EObjectTypeQuery : byte {
		ObjectTypeQuery1 = 0,
		ObjectTypeQuery2 = 1,
		ObjectTypeQuery3 = 2,
		ObjectTypeQuery4 = 3,
		ObjectTypeQuery5 = 4,
		ObjectTypeQuery6 = 5,
		ObjectTypeQuery7 = 6,
		ObjectTypeQuery8 = 7,
		ObjectTypeQuery9 = 8,
		ObjectTypeQuery10 = 9,
		ObjectTypeQuery11 = 10,
		ObjectTypeQuery12 = 11,
		ObjectTypeQuery13 = 12,
		ObjectTypeQuery14 = 13,
		ObjectTypeQuery15 = 14,
		ObjectTypeQuery16 = 15,
		ObjectTypeQuery17 = 16,
		ObjectTypeQuery18 = 17,
		ObjectTypeQuery19 = 18,
		ObjectTypeQuery20 = 19,
		ObjectTypeQuery21 = 20,
		ObjectTypeQuery22 = 21,
		ObjectTypeQuery23 = 22,
		ObjectTypeQuery24 = 23,
		ObjectTypeQuery25 = 24,
		ObjectTypeQuery26 = 25,
		ObjectTypeQuery27 = 26,
		ObjectTypeQuery28 = 27,
		ObjectTypeQuery29 = 28,
		ObjectTypeQuery30 = 29,
		ObjectTypeQuery31 = 30,
		ObjectTypeQuery32 = 31,
		ObjectTypeQuery_MAX = 32,
		EObjectTypeQuery_MAX = 33
	};

	// Enum Engine.EDrawDebugTrace
	public enum EDrawDebugTrace : byte {
		None = 0,
		ForOneFrame = 1,
		ForDuration = 2,
		Persistent = 3,
		EDrawDebugTrace_MAX = 4
	};

	// Enum Engine.ETraceTypeQuery
	public enum ETraceTypeQuery : byte {
		TraceTypeQuery1 = 0,
		TraceTypeQuery2 = 1,
		TraceTypeQuery3 = 2,
		TraceTypeQuery4 = 3,
		TraceTypeQuery5 = 4,
		TraceTypeQuery6 = 5,
		TraceTypeQuery7 = 6,
		TraceTypeQuery8 = 7,
		TraceTypeQuery9 = 8,
		TraceTypeQuery10 = 9,
		TraceTypeQuery11 = 10,
		TraceTypeQuery12 = 11,
		TraceTypeQuery13 = 12,
		TraceTypeQuery14 = 13,
		TraceTypeQuery15 = 14,
		TraceTypeQuery16 = 15,
		TraceTypeQuery17 = 16,
		TraceTypeQuery18 = 17,
		TraceTypeQuery19 = 18,
		TraceTypeQuery20 = 19,
		TraceTypeQuery21 = 20,
		TraceTypeQuery22 = 21,
		TraceTypeQuery23 = 22,
		TraceTypeQuery24 = 23,
		TraceTypeQuery25 = 24,
		TraceTypeQuery26 = 25,
		TraceTypeQuery27 = 26,
		TraceTypeQuery28 = 27,
		TraceTypeQuery29 = 28,
		TraceTypeQuery30 = 29,
		TraceTypeQuery31 = 30,
		TraceTypeQuery32 = 31,
		TraceTypeQuery_MAX = 32,
		ETraceTypeQuery_MAX = 33
	};

	// Enum Engine.EMoveComponentAction
	public enum EMoveComponentAction : byte {
		Move = 0,
		Stop = 1,
		Return = 2,
		EMoveComponentAction_MAX = 3
	};

	// Enum Engine.EQuitPreference
	public enum EQuitPreference : byte {
		Quit = 0,
		Background = 1,
		EQuitPreference_MAX = 2
	};

	// Enum Engine.ERelativeTransformSpace
	public enum ERelativeTransformSpace : byte {
		RTS_World = 0,
		RTS_Actor = 1,
		RTS_Component = 2,
		RTS_ParentBoneSpace = 3,
		RTS_MAX = 4
	};

	// Enum Engine.EAttachLocation
	public enum EAttachLocation : byte {
		KeepRelativeOffset = 0,
		KeepWorldPosition = 1,
		SnapToTarget = 2,
		SnapToTargetIncludingScale = 3,
		EAttachLocation_MAX = 4
	};

	// Enum Engine.EAttachmentRule
	public enum EAttachmentRule : byte {
		KeepRelative = 0,
		KeepWorld = 1,
		SnapToTarget = 2,
		EAttachmentRule_MAX = 3
	};

	// Enum Engine.EDetachmentRule
	public enum EDetachmentRule : byte {
		KeepRelative = 0,
		KeepWorld = 1,
		EDetachmentRule_MAX = 2
	};

	// Enum Engine.EComponentMobility
	public enum EComponentMobility : byte {
		Static = 0,
		Stationary = 1,
		Movable = 2,
		EComponentMobility_MAX = 3
	};

	// Enum Engine.EDetailMode
	public enum EDetailMode : byte {
		DM_Low = 0,
		DM_Medium = 1,
		DM_High = 2,
		DM_MAX = 3
	};

	// Enum Engine.ENetRole
	public enum ENetRole : byte {
		ROLE_None = 0,
		ROLE_SimulatedProxy = 1,
		ROLE_AutonomousProxy = 2,
		ROLE_Authority = 3,
		ROLE_MAX = 4
	};

	// Enum Engine.ENetDormancy
	public enum ENetDormancy : byte {
		DORM_Never = 0,
		DORM_Awake = 1,
		DORM_DormantAll = 2,
		DORM_DormantPartial = 3,
		DORM_Initial = 4,
		DORM_MAX = 5
	};

	// Enum Engine.EAutoReceiveInput
	public enum EAutoReceiveInput : byte {
		Disabled = 0,
		Player0 = 1,
		Player1 = 2,
		Player2 = 3,
		Player3 = 4,
		Player4 = 5,
		Player5 = 6,
		Player6 = 7,
		Player7 = 8,
		EAutoReceiveInput_MAX = 9
	};

	// Enum Engine.ESpawnActorCollisionHandlingMethod
	public enum ESpawnActorCollisionHandlingMethod : byte {
		Undefined = 0,
		AlwaysSpawn = 1,
		AdjustIfPossibleButAlwaysSpawn = 2,
		AdjustIfPossibleButDontSpawnIfColliding = 3,
		DontSpawnIfColliding = 4,
		ESpawnActorCollisionHandlingMethod_MAX = 5
	};

	// Enum Engine.ERotatorQuantization
	public enum ERotatorQuantization : byte {
		ByteComponents = 0,
		ShortComponents = 1,
		ERotatorQuantization_MAX = 2
	};

	// Enum Engine.EVectorQuantization
	public enum EVectorQuantization : byte {
		RoundWholeNumber = 0,
		RoundOneDecimal = 1,
		RoundTwoDecimals = 2,
		EVectorQuantization_MAX = 3
	};

	// Enum Engine.EActorUpdateOverlapsMethod
	public enum EActorUpdateOverlapsMethod : byte {
		UseConfigDefault = 0,
		AlwaysUpdate = 1,
		OnlyUpdateMovable = 2,
		NeverUpdate = 3,
		EActorUpdateOverlapsMethod_MAX = 4
	};

	// Enum Engine.EAutoPossessAI
	public enum EAutoPossessAI : byte {
		Disabled = 0,
		PlacedInWorld = 1,
		Spawned = 2,
		PlacedInWorldOrSpawned = 3,
		EAutoPossessAI_MAX = 4
	};

	// Enum Engine.EPhysicalSurface
	public enum EPhysicalSurface : byte {
		SurfaceType_Default = 0,
		SurfaceType1 = 1,
		SurfaceType2 = 2,
		SurfaceType3 = 3,
		SurfaceType4 = 4,
		SurfaceType5 = 5,
		SurfaceType6 = 6,
		SurfaceType7 = 7,
		SurfaceType8 = 8,
		SurfaceType9 = 9,
		SurfaceType10 = 10,
		SurfaceType11 = 11,
		SurfaceType12 = 12,
		SurfaceType13 = 13,
		SurfaceType14 = 14,
		SurfaceType15 = 15,
		SurfaceType16 = 16,
		SurfaceType17 = 17,
		SurfaceType18 = 18,
		SurfaceType19 = 19,
		SurfaceType20 = 20,
		SurfaceType21 = 21,
		SurfaceType22 = 22,
		SurfaceType23 = 23,
		SurfaceType24 = 24,
		SurfaceType25 = 25,
		SurfaceType26 = 26,
		SurfaceType27 = 27,
		SurfaceType28 = 28,
		SurfaceType29 = 29,
		SurfaceType30 = 30,
		SurfaceType31 = 31,
		SurfaceType32 = 32,
		SurfaceType33 = 33,
		SurfaceType34 = 34,
		SurfaceType35 = 35,
		SurfaceType36 = 36,
		SurfaceType37 = 37,
		SurfaceType38 = 38,
		SurfaceType39 = 39,
		SurfaceType40 = 40,
		SurfaceType41 = 41,
		SurfaceType42 = 42,
		SurfaceType43 = 43,
		SurfaceType44 = 44,
		SurfaceType45 = 45,
		SurfaceType46 = 46,
		SurfaceType47 = 47,
		SurfaceType48 = 48,
		SurfaceType49 = 49,
		SurfaceType50 = 50,
		SurfaceType51 = 51,
		SurfaceType52 = 52,
		SurfaceType53 = 53,
		SurfaceType54 = 54,
		SurfaceType55 = 55,
		SurfaceType56 = 56,
		SurfaceType57 = 57,
		SurfaceType58 = 58,
		SurfaceType59 = 59,
		SurfaceType60 = 60,
		SurfaceType61 = 61,
		SurfaceType62 = 62,
		SurfaceType_Max = 63,
		EPhysicalSurface_MAX = 64
	};

	// Enum Engine.EMouseLockMode
	public enum EMouseLockMode : byte {
		DoNotLock = 0,
		LockOnCapture = 1,
		LockAlways = 2,
		LockInFullscreen = 3,
		EMouseLockMode_MAX = 4
	};

	// Enum Engine.EWindowTitleBarMode
	public enum EWindowTitleBarMode : byte {
		Overlay = 0,
		VerticalBox = 1,
		EWindowTitleBarMode_MAX = 2
	};

	// Enum Engine.EAlphaBlendOption
	public enum EAlphaBlendOption : byte {
		Linear = 0,
		Cubic = 1,
		HermiteCubic = 2,
		Sinusoidal = 3,
		QuadraticInOut = 4,
		CubicInOut = 5,
		QuarticInOut = 6,
		QuinticInOut = 7,
		CircularIn = 8,
		CircularOut = 9,
		CircularInOut = 10,
		ExpIn = 11,
		ExpOut = 12,
		ExpInOut = 13,
		Custom = 14,
		EAlphaBlendOption_MAX = 15
	};

	// Enum Engine.EAnimGroupRole
	public enum EAnimGroupRole : byte {
		CanBeLeader = 0,
		AlwaysFollower = 1,
		AlwaysLeader = 2,
		TransitionLeader = 3,
		TransitionFollower = 4,
		EAnimGroupRole_MAX = 5
	};

	// Enum Engine.EPreviewAnimationBlueprintApplicationMethod
	public enum EPreviewAnimationBlueprintApplicationMethod : byte {
		LinkedLayers = 0,
		LinkedAnimGraph = 1,
		EPreviewAnimationBlueprintApplicationMethod_MAX = 2
	};

	// Enum Engine.AnimationKeyFormat
	public enum AnimationKeyFormat : byte {
		AKF_ConstantKeyLerp = 0,
		AKF_VariableKeyLerp = 1,
		AKF_PerTrackCompression = 2,
		AKF_MAX = 3
	};

	// Enum Engine.ERawCurveTrackTypes
	public enum ERawCurveTrackTypes : byte {
		RCT_Float = 0,
		RCT_Vector = 1,
		RCT_Transform = 2,
		RCT_MAX = 3
	};

	// Enum Engine.EAnimAssetCurveFlags
	public enum EAnimAssetCurveFlags : byte {
		AACF_NONE = 0,
		AACF_DriveMorphTarget_DEPRECATED = 1,
		AACF_DriveAttribute_DEPRECATED = 2,
		AACF_Editable = 4,
		AACF_DriveMaterial_DEPRECATED = 8,
		AACF_Metadata = 16,
		AACF_DriveTrack = 32,
		AACF_Disabled = 64,
		AACF_MAX = 65
	};

	// Enum Engine.AnimationCompressionFormat
	public enum AnimationCompressionFormat : byte {
		ACF_None = 0,
		ACF_Float96NoW = 1,
		ACF_Fixed48NoW = 2,
		ACF_IntervalFixed32NoW = 3,
		ACF_Fixed32NoW = 4,
		ACF_Float32NoW = 5,
		ACF_Identity = 6,
		ACF_MAX = 7
	};

	// Enum Engine.EAdditiveBasePoseType
	public enum EAdditiveBasePoseType : byte {
		ABPT_None = 0,
		ABPT_RefPose = 1,
		ABPT_AnimScaled = 2,
		ABPT_AnimFrame = 3,
		ABPT_MAX = 4
	};

	// Enum Engine.ERootMotionMode
	public enum ERootMotionMode : byte {
		NoRootMotionExtraction = 0,
		IgnoreRootMotion = 1,
		RootMotionFromEverything = 2,
		RootMotionFromMontagesOnly = 3,
		ERootMotionMode_MAX = 4
	};

	// Enum Engine.ERootMotionRootLock
	public enum ERootMotionRootLock : byte {
		RefPose = 0,
		AnimFirstFrame = 1,
		Zero = 2,
		ERootMotionRootLock_MAX = 3
	};

	// Enum Engine.EMontagePlayReturnType
	public enum EMontagePlayReturnType : byte {
		MontageLength = 0,
		Duration = 1,
		EMontagePlayReturnType_MAX = 2
	};

	// Enum Engine.EDrawDebugItemType
	public enum EDrawDebugItemType : byte {
		DirectionalArrow = 0,
		Sphere = 1,
		Line = 2,
		OnScreenMessage = 3,
		CoordinateSystem = 4,
		EDrawDebugItemType_MAX = 5
	};

	// Enum Engine.EAnimLinkMethod
	public enum EAnimLinkMethod : byte {
		Absolute = 0,
		Relative = 1,
		Proportional = 2,
		EAnimLinkMethod_MAX = 3
	};

	// Enum Engine.EMontageSubStepResult
	public enum EMontageSubStepResult : byte {
		Moved = 0,
		NotMoved = 1,
		InvalidSection = 2,
		InvalidMontage = 3,
		EMontageSubStepResult_MAX = 4
	};

	// Enum Engine.EAnimNotifyEventType
	public enum EAnimNotifyEventType : byte {
		Begin = 0,
		End = 1,
		EAnimNotifyEventType_MAX = 2
	};

	// Enum Engine.EInertializationSpace
	public enum EInertializationSpace : byte {
		Default = 0,
		WorldSpace = 1,
		WorldRotation = 2,
		EInertializationSpace_MAX = 3
	};

	// Enum Engine.EInertializationBoneState
	public enum EInertializationBoneState : byte {
		Invalid = 0,
		Valid = 1,
		Excluded = 2,
		EInertializationBoneState_MAX = 3
	};

	// Enum Engine.EInertializationState
	public enum EInertializationState : byte {
		Inactive = 0,
		Pending = 1,
		Active = 2,
		EInertializationState_MAX = 3
	};

	// Enum Engine.EEvaluatorMode
	public enum EEvaluatorMode : byte {
		EM_Standard = 0,
		EM_Freeze = 1,
		EM_DelayedFreeze = 2,
		EM_MAX = 3
	};

	// Enum Engine.EEvaluatorDataSource
	public enum EEvaluatorDataSource : byte {
		EDS_SourcePose = 0,
		EDS_DestinationPose = 1,
		EDS_MAX = 2
	};

	// Enum Engine.ECopyType
	public enum ECopyType : byte {
		PlainProperty = 0,
		BoolProperty = 1,
		StructProperty = 2,
		ObjectProperty = 3,
		NameProperty = 4,
		ECopyType_MAX = 5
	};

	// Enum Engine.EPostCopyOperation
	public enum EPostCopyOperation : byte {
		None = 0,
		LogicalNegateBool = 1,
		EPostCopyOperation_MAX = 2
	};

	// Enum Engine.EPinHidingMode
	public enum EPinHidingMode : byte {
		NeverAsPin = 0,
		PinHiddenByDefault = 1,
		PinShownByDefault = 2,
		AlwaysAsPin = 3,
		EPinHidingMode_MAX = 4
	};

	// Enum Engine.AnimPhysCollisionType
	public enum AnimPhysCollisionType : byte {
		CoM = 0,
		CustomSphere = 1,
		InnerSphere = 2,
		OuterSphere = 3,
		AnimPhysCollisionType_MAX = 4
	};

	// Enum Engine.AnimPhysTwistAxis
	public enum AnimPhysTwistAxis : byte {
		AxisX = 0,
		AxisY = 1,
		AxisZ = 2,
		AnimPhysTwistAxis_MAX = 3
	};

	// Enum Engine.ETypeAdvanceAnim
	public enum ETypeAdvanceAnim : byte {
		ETAA_Default = 0,
		ETAA_Finished = 1,
		ETAA_Looped = 2,
		ETAA_MAX = 3
	};

	// Enum Engine.ETransitionLogicType
	public enum ETransitionLogicType : byte {
		TLT_StandardBlend = 0,
		TLT_Inertialization = 1,
		TLT_Custom = 2,
		TLT_MAX = 3
	};

	// Enum Engine.ETransitionBlendMode
	public enum ETransitionBlendMode : byte {
		TBM_Linear = 0,
		TBM_Cubic = 1,
		TBM_MAX = 2
	};

	// Enum Engine.EComponentType
	public enum EComponentType : byte {
		None = 0,
		TranslationX = 1,
		TranslationY = 2,
		TranslationZ = 3,
		RotationX = 4,
		RotationY = 5,
		RotationZ = 6,
		Scale = 7,
		ScaleX = 8,
		ScaleY = 9,
		ScaleZ = 10,
		EComponentType_MAX = 11
	};

	// Enum Engine.EAxisOption
	public enum EAxisOption : byte {
		X = 0,
		Y = 1,
		Z = 2,
		X_Neg = 3,
		Y_Neg = 4,
		Z_Neg = 5,
		Custom = 6,
		EAxisOption_MAX = 7
	};

	// Enum Engine.EAnimInterpolationType
	public enum EAnimInterpolationType : byte {
		Linear = 0,
		Step = 1,
		EAnimInterpolationType_MAX = 2
	};

	// Enum Engine.ECurveBlendOption
	public enum ECurveBlendOption : byte {
		Override = 0,
		DoNotOverride = 1,
		NormalizeByWeight = 2,
		BlendByWeight = 3,
		UseBasePose = 4,
		UseMaxValue = 5,
		UseMinValue = 6,
		ECurveBlendOption_MAX = 7
	};

	// Enum Engine.EAdditiveAnimationType
	public enum EAdditiveAnimationType : byte {
		AAT_None = 0,
		AAT_LocalSpaceBase = 1,
		AAT_RotationOffsetMeshSpace = 2,
		AAT_MAX = 3
	};

	// Enum Engine.ENotifyFilterType
	public enum ENotifyFilterType : byte {
		NoFiltering = 0,
		LOD = 1,
		ENotifyFilterType_MAX = 2
	};

	// Enum Engine.EMontageNotifyTickType
	public enum EMontageNotifyTickType : byte {
		Queued = 0,
		BranchingPoint = 1,
		EMontageNotifyTickType_MAX = 2
	};

	// Enum Engine.EBoneRotationSource
	public enum EBoneRotationSource : byte {
		BRS_KeepComponentSpaceRotation = 0,
		BRS_KeepLocalSpaceRotation = 1,
		BRS_CopyFromTarget = 2,
		BRS_MAX = 3
	};

	// Enum Engine.EBoneControlSpace
	public enum EBoneControlSpace : byte {
		BCS_WorldSpace = 0,
		BCS_ComponentSpace = 1,
		BCS_ParentBoneSpace = 2,
		BCS_BoneSpace = 3,
		BCS_MAX = 4
	};

	// Enum Engine.EBoneAxis
	public enum EBoneAxis : byte {
		BA_X = 0,
		BA_Y = 1,
		BA_Z = 2,
		BA_MAX = 3
	};

	// Enum Engine.EPrimaryAssetCookRule
	public enum EPrimaryAssetCookRule : byte {
		Unknown = 0,
		NeverCook = 1,
		DevelopmentCook = 2,
		DevelopmentAlwaysCook = 3,
		AlwaysCook = 4,
		EPrimaryAssetCookRule_MAX = 5
	};

	// Enum Engine.ENaturalSoundFalloffMode
	public enum ENaturalSoundFalloffMode : byte {
		Continues = 0,
		Silent = 1,
		Hold = 2,
		ENaturalSoundFalloffMode_MAX = 3
	};

	// Enum Engine.EAttenuationShape
	public enum EAttenuationShape : byte {
		Sphere = 0,
		Capsule = 1,
		Box = 2,
		Cone = 3,
		EAttenuationShape_MAX = 4
	};

	// Enum Engine.EAttenuationDistanceModel
	public enum EAttenuationDistanceModel : byte {
		Linear = 0,
		Logarithmic = 1,
		Inverse = 2,
		LogReverse = 3,
		NaturalSound = 4,
		Custom = 5,
		EAttenuationDistanceModel_MAX = 6
	};

	// Enum Engine.EAudioFaderCurve
	public enum EAudioFaderCurve : byte {
		Linear = 0,
		Logarithmic = 1,
		SCurve = 2,
		Sin = 3,
		Count = 4,
		EAudioFaderCurve_MAX = 5
	};

	// Enum Engine.EAudioComponentPlayState
	public enum EAudioComponentPlayState : byte {
		Playing = 0,
		Stopped = 1,
		Paused = 2,
		FadingIn = 3,
		FadingOut = 4,
		Count = 5,
		EAudioComponentPlayState_MAX = 6
	};

	// Enum Engine.EAudioOutputTarget
	public enum EAudioOutputTarget : byte {
		Speaker = 0,
		Controller = 1,
		ControllerFallbackToSpeaker = 2,
		EAudioOutputTarget_MAX = 3
	};

	// Enum Engine.EMonoChannelUpmixMethod
	public enum EMonoChannelUpmixMethod : byte {
		Linear = 0,
		EqualPower = 1,
		FullVolume = 2,
		EMonoChannelUpmixMethod_MAX = 3
	};

	// Enum Engine.EPanningMethod
	public enum EPanningMethod : byte {
		Linear = 0,
		EqualPower = 1,
		EPanningMethod_MAX = 2
	};

	// Enum Engine.EVoiceSampleRate
	public enum EVoiceSampleRate : int {
		Low16000Hz = 16000,
		Normal24000Hz = 24000,
		EVoiceSampleRate_MAX = 24001
	};

	// Enum Engine.EBlendableLocation
	public enum EBlendableLocation : byte {
		BL_AfterTonemapping = 0,
		BL_BeforeTonemapping = 1,
		BL_BeforeTranslucency = 2,
		BL_ReplacingTonemapper = 3,
		BL_SSRInput = 4,
		BL_MAX = 5
	};

	// Enum Engine.ENotifyTriggerMode
	public enum ENotifyTriggerMode : byte {
		AllAnimations = 0,
		HighestWeightedAnimation = 1,
		None = 2,
		ENotifyTriggerMode_MAX = 3
	};

	// Enum Engine.EBlendSpaceAxis
	public enum EBlendSpaceAxis : byte {
		BSA_None = 0,
		BSA_X = 1,
		BSA_Y = 2,
		BSA_Max = 3
	};

	// Enum Engine.EBlueprintNativizationFlag
	public enum EBlueprintNativizationFlag : byte {
		Disabled = 0,
		Dependency = 1,
		ExplicitlyEnabled = 2,
		EBlueprintNativizationFlag_MAX = 3
	};

	// Enum Engine.EBlueprintCompileMode
	public enum EBlueprintCompileMode : byte {
		Default = 0,
		Development = 1,
		FinalRelease = 2,
		EBlueprintCompileMode_MAX = 3
	};

	// Enum Engine.EBlueprintType
	public enum EBlueprintType : byte {
		BPTYPE_Normal = 0,
		BPTYPE_Const = 1,
		BPTYPE_MacroLibrary = 2,
		BPTYPE_Interface = 3,
		BPTYPE_LevelScript = 4,
		BPTYPE_FunctionLibrary = 5,
		BPTYPE_MAX = 6
	};

	// Enum Engine.EBlueprintStatus
	public enum EBlueprintStatus : byte {
		BS_Unknown = 0,
		BS_Dirty = 1,
		BS_Error = 2,
		BS_UpToDate = 3,
		BS_BeingCreated = 4,
		BS_UpToDateWithWarnings = 5,
		BS_MAX = 6
	};

	// Enum Engine.EDOFMode
	public enum EDOFMode : byte {
		Default = 0,
		SixDOF = 1,
		YZPlane = 2,
		XZPlane = 3,
		XYPlane = 4,
		CustomPlane = 5,
		None = 6,
		EDOFMode_MAX = 7
	};

	// Enum Engine.EBodyCollisionResponse
	public enum EBodyCollisionResponse : byte {
		BodyCollision_Enabled = 0,
		BodyCollision_Disabled = 1,
		BodyCollision_MAX = 2
	};

	// Enum Engine.EPhysicsType
	public enum EPhysicsType : byte {
		PhysType_Default = 0,
		PhysType_Kinematic = 1,
		PhysType_Simulated = 2,
		PhysType_MAX = 3
	};

	// Enum Engine.ECollisionTraceFlag
	public enum ECollisionTraceFlag : byte {
		CTF_UseDefault = 0,
		CTF_UseSimpleAndComplex = 1,
		CTF_UseSimpleAsComplex = 2,
		CTF_UseComplexAsSimple = 3,
		CTF_MAX = 4
	};

	// Enum Engine.EBrushType
	public enum EBrushType : byte {
		Brush_Default = 0,
		Brush_Add = 1,
		Brush_Subtract = 2,
		Brush_MAX = 3
	};

	// Enum Engine.ECsgOper
	public enum ECsgOper : byte {
		CSG_Active = 0,
		CSG_Add = 1,
		CSG_Subtract = 2,
		CSG_Intersect = 3,
		CSG_Deintersect = 4,
		CSG_None = 5,
		CSG_MAX = 6
	};

	// Enum Engine.EInitialOscillatorOffset
	public enum EInitialOscillatorOffset : byte {
		EOO_OffsetRandom = 0,
		EOO_OffsetZero = 1,
		EOO_MAX = 2
	};

	// Enum Engine.EOscillatorWaveform
	public enum EOscillatorWaveform : byte {
		SineWave = 0,
		PerlinNoise = 1,
		EOscillatorWaveform_MAX = 2
	};

	// Enum Engine.ECameraShakeAttenuation
	public enum ECameraShakeAttenuation : byte {
		Linear = 0,
		Quadratic = 1,
		ECameraShakeAttenuation_MAX = 2
	};

	// Enum Engine.ECameraAlphaBlendMode
	public enum ECameraAlphaBlendMode : byte {
		CABM_Linear = 0,
		CABM_Cubic = 1,
		CABM_MAX = 2
	};

	// Enum Engine.ECameraAnimPlaySpace
	public enum ECameraAnimPlaySpace : byte {
		CameraLocal = 0,
		World = 1,
		UserDefined = 2,
		ECameraAnimPlaySpace_MAX = 3
	};

	// Enum Engine.ECameraProjectionMode
	public enum ECameraProjectionMode : byte {
		Perspective = 0,
		Orthographic = 1,
		ECameraProjectionMode_MAX = 2
	};

	// Enum Engine.ECloudStorageDelegate
	public enum ECloudStorageDelegate : byte {
		CSD_KeyValueReadComplete = 0,
		CSD_KeyValueWriteComplete = 1,
		CSD_ValueChanged = 2,
		CSD_DocumentQueryComplete = 3,
		CSD_DocumentReadComplete = 4,
		CSD_DocumentWriteComplete = 5,
		CSD_DocumentConflictDetected = 6,
		CSD_MAX = 7
	};

	// Enum Engine.EAngularDriveMode
	public enum EAngularDriveMode : byte {
		SLERP = 0,
		TwistAndSwing = 1,
		EAngularDriveMode_MAX = 2
	};

	// Enum Engine.ELinearConstraintMotion
	public enum ELinearConstraintMotion : byte {
		LCM_Free = 0,
		LCM_Limited = 1,
		LCM_Locked = 2,
		LCM_MAX = 3
	};

	// Enum Engine.ECurveTableMode
	public enum ECurveTableMode : byte {
		Empty = 0,
		SimpleCurves = 1,
		RichCurves = 2,
		ECurveTableMode_MAX = 3
	};

	// Enum Engine.EEvaluateCurveTableResult
	public enum EEvaluateCurveTableResult : byte {
		RowFound = 0,
		RowNotFound = 1,
		EEvaluateCurveTableResult_MAX = 2
	};

	// Enum Engine.EGrammaticalNumber
	public enum EGrammaticalNumber : byte {
		Singular = 0,
		Plural = 1,
		EGrammaticalNumber_MAX = 2
	};

	// Enum Engine.EGrammaticalGender
	public enum EGrammaticalGender : byte {
		Neuter = 0,
		Masculine = 1,
		Feminine = 2,
		Mixed = 3,
		EGrammaticalGender_MAX = 4
	};

	// Enum Engine.DistributionParamMode
	public enum DistributionParamMode : byte {
		DPM_Normal = 0,
		DPM_Abs = 1,
		DPM_Direct = 2,
		DPM_MAX = 3
	};

	// Enum Engine.EDistributionVectorMirrorFlags
	public enum EDistributionVectorMirrorFlags : byte {
		EDVMF_Same = 0,
		EDVMF_Different = 1,
		EDVMF_Mirror = 2,
		EDVMF_MAX = 3
	};

	// Enum Engine.EDistributionVectorLockFlags
	public enum EDistributionVectorLockFlags : byte {
		EDVLF_None = 0,
		EDVLF_XY = 1,
		EDVLF_XZ = 2,
		EDVLF_YZ = 3,
		EDVLF_XYZ = 4,
		EDVLF_MAX = 5
	};

	// Enum Engine.ENodeEnabledState
	public enum ENodeEnabledState : byte {
		Enabled = 0,
		Disabled = 1,
		DevelopmentOnly = 2,
		ENodeEnabledState_MAX = 3
	};

	// Enum Engine.ENodeAdvancedPins
	public enum ENodeAdvancedPins : byte {
		NoPins = 0,
		Shown = 1,
		Hidden = 2,
		ENodeAdvancedPins_MAX = 3
	};

	// Enum Engine.ENodeTitleType
	public enum ENodeTitleType : byte {
		FullTitle = 0,
		ListView = 1,
		EditableTitle = 2,
		MenuTitle = 3,
		MAX_TitleTypes = 4,
		ENodeTitleType_MAX = 5
	};

	// Enum Engine.EPinContainerType
	public enum EPinContainerType : byte {
		None = 0,
		Array = 1,
		Set = 2,
		Map = 3,
		EPinContainerType_MAX = 4
	};

	// Enum Engine.EEdGraphPinDirection
	public enum EEdGraphPinDirection : byte {
		EGPD_Input = 0,
		EGPD_Output = 1,
		EGPD_MAX = 2
	};

	// Enum Engine.EBlueprintPinStyleType
	public enum EBlueprintPinStyleType : byte {
		BPST_Original = 0,
		BPST_VariantA = 1,
		BPST_MAX = 2
	};

	// Enum Engine.ECanCreateConnectionResponse
	public enum ECanCreateConnectionResponse : byte {
		CONNECT_RESPONSE_MAKE = 0,
		CONNECT_RESPONSE_DISALLOW = 1,
		CONNECT_RESPONSE_BREAK_OTHERS_A = 2,
		CONNECT_RESPONSE_BREAK_OTHERS_B = 3,
		CONNECT_RESPONSE_BREAK_OTHERS_AB = 4,
		CONNECT_RESPONSE_MAKE_WITH_CONVERSION_NODE = 5,
		CONNECT_RESPONSE_MAX = 6
	};

	// Enum Engine.EGraphType
	public enum EGraphType : byte {
		GT_Function = 0,
		GT_Ubergraph = 1,
		GT_Macro = 2,
		GT_Animation = 3,
		GT_StateMachine = 4,
		GT_MAX = 5
	};

	// Enum Engine.ETransitionType
	public enum ETransitionType : byte {
		None = 0,
		Paused = 1,
		Loading = 2,
		Saving = 3,
		Connecting = 4,
		Precaching = 5,
		WaitingToConnect = 6,
		MAX = 7
	};

	// Enum Engine.EFullyLoadPackageType
	public enum EFullyLoadPackageType : byte {
		FULLYLOAD_Map = 0,
		FULLYLOAD_Game_PreLoadClass = 1,
		FULLYLOAD_Game_PostLoadClass = 2,
		FULLYLOAD_Always = 3,
		FULLYLOAD_Mutator = 4,
		FULLYLOAD_MAX = 5
	};

	// Enum Engine.EViewModeIndex
	public enum EViewModeIndex : byte {
		VMI_BrushWireframe = 0,
		VMI_Wireframe = 1,
		VMI_Unlit = 2,
		VMI_Lit = 3,
		VMI_Lit_DetailLighting = 4,
		VMI_LightingOnly = 5,
		VMI_LightComplexity = 6,
		VMI_ShaderComplexity = 8,
		VMI_LightmapDensity = 9,
		VMI_LitLightmapDensity = 10,
		VMI_ReflectionOverride = 11,
		VMI_VisualizeBuffer = 12,
		VMI_StationaryLightOverlap = 14,
		VMI_CollisionPawn = 15,
		VMI_CollisionVisibility = 16,
		VMI_LODColoration = 18,
		VMI_QuadOverdraw = 19,
		VMI_PrimitiveDistanceAccuracy = 20,
		VMI_MeshUVDensityAccuracy = 21,
		VMI_ShaderComplexityWithQuadOverdraw = 22,
		VMI_HLODColoration = 23,
		VMI_GroupLODColoration = 24,
		VMI_MaterialTextureScaleAccuracy = 25,
		VMI_RequiredTextureResolution = 26,
		VMI_PathTracing = 27,
		VMI_RayTracingDebug = 28,
		VMI_Max = 29,
		VMI_Unknown = 255
	};

	// Enum Engine.EDemoPlayFailure
	public enum EDemoPlayFailure : byte {
		Generic = 0,
		DemoNotFound = 1,
		Corrupt = 2,
		InvalidVersion = 3,
		InitBase = 4,
		GameSpecificHeader = 5,
		ReplayStreamerInternal = 6,
		LoadMap = 7,
		Serialization = 8,
		EDemoPlayFailure_MAX = 9
	};

	// Enum Engine.ETravelType
	public enum ETravelType : byte {
		TRAVEL_Absolute = 0,
		TRAVEL_Partial = 1,
		TRAVEL_Relative = 2,
		TRAVEL_MAX = 3
	};

	// Enum Engine.ENetworkLagState
	public enum ENetworkLagState : byte {
		NotLagging = 0,
		Lagging = 1,
		ENetworkLagState_MAX = 2
	};

	// Enum Engine.EMouseCaptureMode
	public enum EMouseCaptureMode : byte {
		NoCapture = 0,
		CapturePermanently = 1,
		CapturePermanently_IncludingInitialMouseDown = 2,
		CaptureDuringMouseDown = 3,
		CaptureDuringRightMouseDown = 4,
		EMouseCaptureMode_MAX = 5
	};

	// Enum Engine.EInputEvent
	public enum EInputEvent : byte {
		IE_Pressed = 0,
		IE_Released = 1,
		IE_Repeat = 2,
		IE_DoubleClick = 3,
		IE_Axis = 4,
		IE_MAX = 5
	};

	// Enum Engine.ECustomTimeStepSynchronizationState
	public enum ECustomTimeStepSynchronizationState : byte {
		Closed = 0,
		Error = 1,
		Synchronized = 2,
		Synchronizing = 3,
		ECustomTimeStepSynchronizationState_MAX = 4
	};

	// Enum Engine.EMeshBufferAccess
	public enum EMeshBufferAccess : byte {
		Default = 0,
		ForceCPUAndGPU = 1,
		EMeshBufferAccess_MAX = 2
	};

	// Enum Engine.EConstraintFrame
	public enum EConstraintFrame : byte {
		Frame1 = 0,
		Frame2 = 1,
		EConstraintFrame_MAX = 2
	};

	// Enum Engine.EAngularConstraintMotion
	public enum EAngularConstraintMotion : byte {
		ACM_Free = 0,
		ACM_Limited = 1,
		ACM_Locked = 2,
		ACM_MAX = 3
	};

	// Enum Engine.EComponentSocketType
	public enum EComponentSocketType : byte {
		Invalid = 0,
		Bone = 1,
		Socket = 2,
		EComponentSocketType_MAX = 3
	};

	// Enum Engine.EPhysicalMaterialMaskColor
	public enum EPhysicalMaterialMaskColor : byte {
		Red = 0,
		Green = 1,
		Blue = 2,
		Cyan = 3,
		Magenta = 4,
		Yellow = 5,
		White = 6,
		Black = 7,
		MAX = 8
	};

	// Enum Engine.EWalkableSlopeBehavior
	public enum EWalkableSlopeBehavior : byte {
		WalkableSlope_Default = 0,
		WalkableSlope_Increase = 1,
		WalkableSlope_Decrease = 2,
		WalkableSlope_Unwalkable = 3,
		WalkableSlope_Max = 4
	};

	// Enum Engine.EUpdateRateShiftBucket
	public enum EUpdateRateShiftBucket : byte {
		ShiftBucket0 = 0,
		ShiftBucket1 = 1,
		ShiftBucket2 = 2,
		ShiftBucket3 = 3,
		ShiftBucket4 = 4,
		ShiftBucket5 = 5,
		ShiftBucketMax = 6,
		EUpdateRateShiftBucket_MAX = 7
	};

	// Enum Engine.EShadowMapFlags
	public enum EShadowMapFlags : byte {
		SMF_None = 0,
		SMF_Streamed = 1,
		SMF_MAX = 2
	};

	// Enum Engine.ELightMapPaddingType
	public enum ELightMapPaddingType : byte {
		LMPT_NormalPadding = 0,
		LMPT_PrePadding = 1,
		LMPT_NoPadding = 2,
		LMPT_MAX = 3
	};

	// Enum Engine.ECollisionEnabled
	public enum ECollisionEnabled : byte {
		NoCollision = 0,
		QueryOnly = 1,
		PhysicsOnly = 2,
		QueryAndPhysics = 3,
		ECollisionEnabled_MAX = 4
	};

	// Enum Engine.ETimelineSigType
	public enum ETimelineSigType : byte {
		ETS_EventSignature = 0,
		ETS_FloatSignature = 1,
		ETS_VectorSignature = 2,
		ETS_LinearColorSignature = 3,
		ETS_InvalidSignature = 4,
		ETS_MAX = 5
	};

	// Enum Engine.ESleepFamily
	public enum ESleepFamily : byte {
		Normal = 0,
		Sensitive = 1,
		Custom = 2,
		ESleepFamily_MAX = 3
	};

	// Enum Engine.ERadialImpulseFalloff
	public enum ERadialImpulseFalloff : byte {
		RIF_Constant = 0,
		RIF_Linear = 1,
		RIF_MAX = 2
	};

	// Enum Engine.EFilterInterpolationType
	public enum EFilterInterpolationType : byte {
		BSIT_Average = 0,
		BSIT_Linear = 1,
		BSIT_Cubic = 2,
		BSIT_MAX = 3
	};

	// Enum Engine.ECollisionResponse
	public enum ECollisionResponse : byte {
		ECR_Ignore = 0,
		ECR_Overlap = 1,
		ECR_Block = 2,
		ECR_MAX = 3
	};

	// Enum Engine.EOverlapFilterOption
	public enum EOverlapFilterOption : byte {
		OverlapFilter_All = 0,
		OverlapFilter_DynamicOnly = 1,
		OverlapFilter_StaticOnly = 2,
		OverlapFilter_MAX = 3
	};

	// Enum Engine.ECollisionChannel
	public enum ECollisionChannel : byte {
		ECC_WorldStatic = 0,
		ECC_WorldDynamic = 1,
		ECC_Pawn = 2,
		ECC_Visibility = 3,
		ECC_Camera = 4,
		ECC_PhysicsBody = 5,
		ECC_Vehicle = 6,
		ECC_Destructible = 7,
		ECC_EngineTraceChannel1 = 8,
		ECC_EngineTraceChannel2 = 9,
		ECC_EngineTraceChannel3 = 10,
		ECC_EngineTraceChannel4 = 11,
		ECC_EngineTraceChannel5 = 12,
		ECC_EngineTraceChannel6 = 13,
		ECC_GameTraceChannel1 = 14,
		ECC_GameTraceChannel2 = 15,
		ECC_GameTraceChannel3 = 16,
		ECC_GameTraceChannel4 = 17,
		ECC_GameTraceChannel5 = 18,
		ECC_GameTraceChannel6 = 19,
		ECC_GameTraceChannel7 = 20,
		ECC_GameTraceChannel8 = 21,
		ECC_GameTraceChannel9 = 22,
		ECC_GameTraceChannel10 = 23,
		ECC_GameTraceChannel11 = 24,
		ECC_GameTraceChannel12 = 25,
		ECC_GameTraceChannel13 = 26,
		ECC_GameTraceChannel14 = 27,
		ECC_GameTraceChannel15 = 28,
		ECC_GameTraceChannel16 = 29,
		ECC_GameTraceChannel17 = 30,
		ECC_GameTraceChannel18 = 31,
		ECC_OverlapAll_Deprecated = 32,
		ECC_MAX = 33
	};

	// Enum Engine.ENetworkSmoothingMode
	public enum ENetworkSmoothingMode : byte {
		Disabled = 0,
		Linear = 1,
		Exponential = 2,
		Replay = 3,
		ENetworkSmoothingMode_MAX = 4
	};

	// Enum Engine.ELightingBuildQuality
	public enum ELightingBuildQuality : byte {
		Quality_Preview = 0,
		Quality_Medium = 1,
		Quality_High = 2,
		Quality_Production = 3,
		Quality_MAX = 4
	};

	// Enum Engine.EMaterialStencilCompare
	public enum EMaterialStencilCompare : byte {
		MSC_Less = 0,
		MSC_LessEqual = 1,
		MSC_Greater = 2,
		MSC_GreaterEqual = 3,
		MSC_Equal = 4,
		MSC_NotEqual = 5,
		MSC_Never = 6,
		MSC_Always = 7,
		MSC_Count = 8,
		MSC_MAX = 9
	};

	// Enum Engine.EMaterialSamplerType
	public enum EMaterialSamplerType : byte {
		SAMPLERTYPE_Color = 0,
		SAMPLERTYPE_Grayscale = 1,
		SAMPLERTYPE_Alpha = 2,
		SAMPLERTYPE_Normal = 3,
		SAMPLERTYPE_Masks = 4,
		SAMPLERTYPE_DistanceFieldFont = 5,
		SAMPLERTYPE_LinearColor = 6,
		SAMPLERTYPE_LinearGrayscale = 7,
		SAMPLERTYPE_Data = 8,
		SAMPLERTYPE_External = 9,
		SAMPLERTYPE_VirtualColor = 10,
		SAMPLERTYPE_VirtualGrayscale = 11,
		SAMPLERTYPE_VirtualAlpha = 12,
		SAMPLERTYPE_VirtualNormal = 13,
		SAMPLERTYPE_VirtualMasks = 14,
		SAMPLERTYPE_VirtualLinearColor = 15,
		SAMPLERTYPE_VirtualLinearGrayscale = 16,
		SAMPLERTYPE_MAX = 17
	};

	// Enum Engine.EMaterialTessellationMode
	public enum EMaterialTessellationMode : byte {
		MTM_NoTessellation = 0,
		MTM_FlatTessellation = 1,
		MTM_PNTriangles = 2,
		MTM_MAX = 3
	};

	// Enum Engine.EMaterialShadingModel
	public enum EMaterialShadingModel : byte {
		MSM_Unlit = 0,
		MSM_DefaultLit = 1,
		MSM_Subsurface = 2,
		MSM_PreintegratedSkin = 3,
		MSM_ClearCoat = 4,
		MSM_SubsurfaceProfile = 5,
		MSM_TwoSidedFoliage = 6,
		MSM_Hair = 7,
		MSM_Cloth = 8,
		MSM_Eye = 9,
		MSM_SingleLayerWater = 10,
		MSM_ThinTranslucent = 11,
		MSM_NUM = 12,
		MSM_FromMaterialExpression = 13,
		MSM_MAX = 14
	};

	// Enum Engine.EParticleCollisionMode
	public enum EParticleCollisionMode : byte {
		SceneDepth = 0,
		DistanceField = 1,
		EParticleCollisionMode_MAX = 2
	};

	// Enum Engine.ETrailWidthMode
	public enum ETrailWidthMode : byte {
		ETrailWidthMode_FromCentre = 0,
		ETrailWidthMode_FromFirst = 1,
		ETrailWidthMode_FromSecond = 2,
		ETrailWidthMode_MAX = 3
	};

	// Enum Engine.EGBufferFormat
	public enum EGBufferFormat : byte {
		Force8BitsPerChannel = 0,
		Default = 1,
		HighPrecisionNormals = 3,
		Force16BitsPerChannel = 5,
		EGBufferFormat_MAX = 6
	};

	// Enum Engine.ESceneCaptureCompositeMode
	public enum ESceneCaptureCompositeMode : byte {
		SCCM_Overwrite = 0,
		SCCM_Additive = 1,
		SCCM_Composite = 2,
		SCCM_MAX = 3
	};

	// Enum Engine.ESceneCaptureSource
	public enum ESceneCaptureSource : byte {
		SCS_SceneColorHDR = 0,
		SCS_SceneColorHDRNoAlpha = 1,
		SCS_FinalColorLDR = 2,
		SCS_SceneColorSceneDepth = 3,
		SCS_SceneDepth = 4,
		SCS_DeviceDepth = 5,
		SCS_Normal = 6,
		SCS_BaseColor = 7,
		SCS_FinalColorHDR = 8,
		SCS_FinalToneCurveHDR = 9,
		SCS_MAX = 10
	};

	// Enum Engine.ETranslucentSortPolicy
	public enum ETranslucentSortPolicy : byte {
		SortByDistance = 0,
		SortByProjectedZ = 1,
		SortAlongAxis = 2,
		ETranslucentSortPolicy_MAX = 3
	};

	// Enum Engine.ERefractionMode
	public enum ERefractionMode : byte {
		RM_IndexOfRefraction = 0,
		RM_PixelNormalOffset = 1,
		RM_MAX = 2
	};

	// Enum Engine.ETranslucencyLightingMode
	public enum ETranslucencyLightingMode : byte {
		TLM_VolumetricNonDirectional = 0,
		TLM_VolumetricDirectional = 1,
		TLM_VolumetricPerVertexNonDirectional = 2,
		TLM_VolumetricPerVertexDirectional = 3,
		TLM_Surface = 4,
		TLM_SurfacePerPixelLighting = 5,
		TLM_MAX = 6
	};

	// Enum Engine.ESamplerSourceMode
	public enum ESamplerSourceMode : byte {
		SSM_FromTextureAsset = 0,
		SSM_Wrap_WorldGroupSettings = 1,
		SSM_Clamp_WorldGroupSettings = 2,
		SSM_MAX = 3
	};

	// Enum Engine.EBlendMode
	public enum EBlendMode : byte {
		BLEND_Opaque = 0,
		BLEND_Masked = 1,
		BLEND_Translucent = 2,
		BLEND_Additive = 3,
		BLEND_Modulate = 4,
		BLEND_AlphaComposite = 5,
		BLEND_AlphaHoldout = 6,
		BLEND_MAX = 7
	};

	// Enum Engine.EOcclusionCombineMode
	public enum EOcclusionCombineMode : byte {
		OCM_Minimum = 0,
		OCM_Multiply = 1,
		OCM_MAX = 2
	};

	// Enum Engine.ELightmapType
	public enum ELightmapType : byte {
		Default = 0,
		ForceSurface = 1,
		ForceVolumetric = 2,
		ELightmapType_MAX = 3
	};

	// Enum Engine.EIndirectLightingCacheQuality
	public enum EIndirectLightingCacheQuality : byte {
		ILCQ_Off = 0,
		ILCQ_Point = 1,
		ILCQ_Volume = 2,
		ILCQ_MAX = 3
	};

	// Enum Engine.ESceneDepthPriorityGroup
	public enum ESceneDepthPriorityGroup : byte {
		SDPG_World = 0,
		SDPG_Foreground = 1,
		SDPG_MAX = 2
	};

	// Enum Engine.EAspectRatioAxisConstraint
	public enum EAspectRatioAxisConstraint : byte {
		AspectRatio_MaintainYFOV = 0,
		AspectRatio_MaintainXFOV = 1,
		AspectRatio_MajorAxisFOV = 2,
		AspectRatio_MAX = 3
	};

	// Enum Engine.EFontCacheType
	public enum EFontCacheType : byte {
		Offline = 0,
		Runtime = 1,
		EFontCacheType_MAX = 2
	};

	// Enum Engine.EFontImportCharacterSet
	public enum EFontImportCharacterSet : byte {
		FontICS_Default = 0,
		FontICS_Ansi = 1,
		FontICS_Symbol = 2,
		FontICS_MAX = 3
	};

	// Enum Engine.EStandbyType
	public enum EStandbyType : byte {
		STDBY_Rx = 0,
		STDBY_Tx = 1,
		STDBY_BadPing = 2,
		STDBY_MAX = 3
	};

	// Enum Engine.ESuggestProjVelocityTraceOption
	public enum ESuggestProjVelocityTraceOption : byte {
		DoNotTrace = 0,
		TraceFullPath = 1,
		OnlyTraceWhileAscending = 2,
		ESuggestProjVelocityTraceOption_MAX = 3
	};

	// Enum Engine.EWindowMode
	public enum EWindowMode : byte {
		Fullscreen = 0,
		WindowedFullscreen = 1,
		Windowed = 2,
		EWindowMode_MAX = 3
	};

	// Enum Engine.EHitProxyPriority
	public enum EHitProxyPriority : byte {
		HPP_World = 0,
		HPP_Wireframe = 1,
		HPP_Foreground = 2,
		HPP_UI = 3,
		HPP_MAX = 4
	};

	// Enum Engine.EImportanceWeight
	public enum EImportanceWeight : byte {
		Luminance = 0,
		Red = 1,
		Green = 2,
		Blue = 3,
		Alpha = 4,
		EImportanceWeight_MAX = 5
	};

	// Enum Engine.EAdManagerDelegate
	public enum EAdManagerDelegate : byte {
		AMD_ClickedBanner = 0,
		AMD_UserClosedAd = 1,
		AMD_MAX = 2
	};

	// Enum Engine.EControllerAnalogStick
	public enum EControllerAnalogStick : byte {
		CAS_LeftStick = 0,
		CAS_RightStick = 1,
		CAS_MAX = 2
	};

	// Enum Engine.EAnimAlphaInputType
	public enum EAnimAlphaInputType : byte {
		Float = 0,
		Bool = 1,
		Curve = 2,
		EAnimAlphaInputType_MAX = 3
	};

	// Enum Engine.ETrackActiveCondition
	public enum ETrackActiveCondition : byte {
		ETAC_Always = 0,
		ETAC_GoreEnabled = 1,
		ETAC_GoreDisabled = 2,
		ETAC_MAX = 3
	};

	// Enum Engine.EInterpTrackMoveRotMode
	public enum EInterpTrackMoveRotMode : byte {
		IMR_Keyframed = 0,
		IMR_LookAtGroup = 1,
		IMR_Ignore = 2,
		IMR_MAX = 3
	};

	// Enum Engine.EInterpMoveAxis
	public enum EInterpMoveAxis : byte {
		AXIS_TranslationX = 0,
		AXIS_TranslationY = 1,
		AXIS_TranslationZ = 2,
		AXIS_RotationX = 3,
		AXIS_RotationY = 4,
		AXIS_RotationZ = 5,
		AXIS_MAX = 6
	};

	// Enum Engine.ETrackToggleAction
	public enum ETrackToggleAction : byte {
		ETTA_Off = 0,
		ETTA_On = 1,
		ETTA_Toggle = 2,
		ETTA_Trigger = 3,
		ETTA_MAX = 4
	};

	// Enum Engine.EVisibilityTrackCondition
	public enum EVisibilityTrackCondition : byte {
		EVTC_Always = 0,
		EVTC_GoreEnabled = 1,
		EVTC_GoreDisabled = 2,
		EVTC_MAX = 3
	};

	// Enum Engine.EVisibilityTrackAction
	public enum EVisibilityTrackAction : byte {
		EVTA_Hide = 0,
		EVTA_Show = 1,
		EVTA_Toggle = 2,
		EVTA_MAX = 3
	};

	// Enum Engine.ESlateGesture
	public enum ESlateGesture : byte {
		None = 0,
		Scroll = 1,
		Magnify = 2,
		Swipe = 3,
		Rotate = 4,
		LongPress = 5,
		ESlateGesture_MAX = 6
	};

	// Enum Engine.EMatrixColumns
	public enum EMatrixColumns : byte {
		First = 0,
		Second = 1,
		Third = 2,
		Fourth = 3,
		EMatrixColumns_MAX = 4
	};

	// Enum Engine.ELerpInterpolationMode
	public enum ELerpInterpolationMode : byte {
		QuatInterp = 0,
		EulerInterp = 1,
		DualQuatInterp = 2,
		ELerpInterpolationMode_MAX = 3
	};

	// Enum Engine.EEasingFunc
	public enum EEasingFunc : byte {
		Linear = 0,
		Step = 1,
		SinusoidalIn = 2,
		SinusoidalOut = 3,
		SinusoidalInOut = 4,
		EaseIn = 5,
		EaseOut = 6,
		EaseInOut = 7,
		ExpoIn = 8,
		ExpoOut = 9,
		ExpoInOut = 10,
		CircularIn = 11,
		CircularOut = 12,
		CircularInOut = 13,
		EEasingFunc_MAX = 14
	};

	// Enum Engine.ERoundingMode
	public enum ERoundingMode : byte {
		HalfToEven = 0,
		HalfFromZero = 1,
		HalfToZero = 2,
		FromZero = 3,
		ToZero = 4,
		ToNegativeInfinity = 5,
		ToPositiveInfinity = 6,
		ERoundingMode_MAX = 7
	};

	// Enum Engine.EStreamingVolumeUsage
	public enum EStreamingVolumeUsage : byte {
		SVB_Loading = 0,
		SVB_LoadingAndVisibility = 1,
		SVB_VisibilityBlockingOnLoad = 2,
		SVB_BlockingOnLoad = 3,
		SVB_LoadingNotVisible = 4,
		SVB_MAX = 5
	};

	// Enum Engine.EMaterialDecalResponse
	public enum EMaterialDecalResponse : byte {
		MDR_None = 0,
		MDR_ColorNormalRoughness = 1,
		MDR_Color = 2,
		MDR_ColorNormal = 3,
		MDR_ColorRoughness = 4,
		MDR_Normal = 5,
		MDR_NormalRoughness = 6,
		MDR_Roughness = 7,
		MDR_MAX = 8
	};

	// Enum Engine.EDecalBlendMode
	public enum EDecalBlendMode : byte {
		DBM_Translucent = 0,
		DBM_Stain = 1,
		DBM_Normal = 2,
		DBM_Emissive = 3,
		DBM_DBuffer_ColorNormalRoughness = 4,
		DBM_DBuffer_Color = 5,
		DBM_DBuffer_ColorNormal = 6,
		DBM_DBuffer_ColorRoughness = 7,
		DBM_DBuffer_Normal = 8,
		DBM_DBuffer_NormalRoughness = 9,
		DBM_DBuffer_Roughness = 10,
		DBM_DBuffer_Emissive = 11,
		DBM_DBuffer_AlphaComposite = 12,
		DBM_DBuffer_EmissiveAlphaComposite = 13,
		DBM_Volumetric_DistanceFunction = 14,
		DBM_AlphaComposite = 15,
		DBM_AmbientOcclusion = 16,
		DBM_MAX = 17
	};

	// Enum Engine.ETextureColorChannel
	public enum ETextureColorChannel : byte {
		TCC_Red = 0,
		TCC_Green = 1,
		TCC_Blue = 2,
		TCC_Alpha = 3,
		TCC_MAX = 4
	};

	// Enum Engine.EMaterialAttributeBlend
	public enum EMaterialAttributeBlend : byte {
		Blend = 0,
		UseA = 1,
		UseB = 2,
		EMaterialAttributeBlend_MAX = 3
	};

	// Enum Engine.EChannelMaskParameterColor
	public enum EChannelMaskParameterColor : byte {
		Red = 0,
		Green = 1,
		Blue = 2,
		Alpha = 3,
		EChannelMaskParameterColor_MAX = 4
	};

	// Enum Engine.EClampMode
	public enum EClampMode : byte {
		CMODE_Clamp = 0,
		CMODE_ClampMin = 1,
		CMODE_ClampMax = 2,
		CMODE_MAX = 3
	};

	// Enum Engine.ECustomMaterialOutputType
	public enum ECustomMaterialOutputType : byte {
		CMOT_Float1 = 0,
		CMOT_Float2 = 1,
		CMOT_Float3 = 2,
		CMOT_Float4 = 3,
		CMOT_MAX = 4
	};

	// Enum Engine.EDepthOfFieldFunctionValue
	public enum EDepthOfFieldFunctionValue : byte {
		TDOF_NearAndFarMask = 0,
		TDOF_NearMask = 1,
		TDOF_FarMask = 2,
		TDOF_CircleOfConfusionRadius = 3,
		TDOF_MAX = 4
	};

	// Enum Engine.EFunctionInputType
	public enum EFunctionInputType : byte {
		FunctionInput_Scalar = 0,
		FunctionInput_Vector2 = 1,
		FunctionInput_Vector3 = 2,
		FunctionInput_Vector4 = 3,
		FunctionInput_Texture2D = 4,
		FunctionInput_TextureCube = 5,
		FunctionInput_Texture2DArray = 6,
		FunctionInput_VolumeTexture = 7,
		FunctionInput_StaticBool = 8,
		FunctionInput_MaterialAttributes = 9,
		FunctionInput_TextureExternal = 10,
		FunctionInput_MAX = 11
	};

	// Enum Engine.ENoiseFunction
	public enum ENoiseFunction : byte {
		NOISEFUNCTION_SimplexTex = 0,
		NOISEFUNCTION_GradientTex = 1,
		NOISEFUNCTION_GradientTex3D = 2,
		NOISEFUNCTION_GradientALU = 3,
		NOISEFUNCTION_ValueALU = 4,
		NOISEFUNCTION_VoronoiALU = 5,
		NOISEFUNCTION_MAX = 6
	};

	// Enum Engine.ERuntimeVirtualTextureMipValueMode
	public enum ERuntimeVirtualTextureMipValueMode : byte {
		RVTMVM_None = 0,
		RVTMVM_MipLevel = 1,
		RVTMVM_MipBias = 2,
		RVTMVM_MAX = 3
	};

	// Enum Engine.EMaterialSceneAttributeInputMode
	public enum EMaterialSceneAttributeInputMode : byte {
		Coordinates = 0,
		OffsetFraction = 1,
		EMaterialSceneAttributeInputMode_MAX = 2
	};

	// Enum Engine.ESpeedTreeLODType
	public enum ESpeedTreeLODType : byte {
		STLOD_Pop = 0,
		STLOD_Smooth = 1,
		STLOD_MAX = 2
	};

	// Enum Engine.ESpeedTreeWindType
	public enum ESpeedTreeWindType : byte {
		STW_None = 0,
		STW_Fastest = 1,
		STW_Fast = 2,
		STW_Better = 3,
		STW_Best = 4,
		STW_Palm = 5,
		STW_BestPlus = 6,
		STW_MAX = 7
	};

	// Enum Engine.ESpeedTreeGeometryType
	public enum ESpeedTreeGeometryType : byte {
		STG_Branch = 0,
		STG_Frond = 1,
		STG_Leaf = 2,
		STG_FacingLeaf = 3,
		STG_Billboard = 4,
		STG_MAX = 5
	};

	// Enum Engine.EMaterialExposedTextureProperty
	public enum EMaterialExposedTextureProperty : byte {
		TMTM_TextureSize = 0,
		TMTM_TexelSize = 1,
		TMTM_MAX = 2
	};

	// Enum Engine.ETextureMipValueMode
	public enum ETextureMipValueMode : byte {
		TMVM_None = 0,
		TMVM_MipLevel = 1,
		TMVM_MipBias = 2,
		TMVM_Derivative = 3,
		TMVM_MAX = 4
	};

	// Enum Engine.EMaterialVectorCoordTransform
	public enum EMaterialVectorCoordTransform : byte {
		TRANSFORM_Tangent = 0,
		TRANSFORM_Local = 1,
		TRANSFORM_World = 2,
		TRANSFORM_View = 3,
		TRANSFORM_Camera = 4,
		TRANSFORM_ParticleWorld = 5,
		TRANSFORM_MAX = 6
	};

	// Enum Engine.EMaterialVectorCoordTransformSource
	public enum EMaterialVectorCoordTransformSource : byte {
		TRANSFORMSOURCE_Tangent = 0,
		TRANSFORMSOURCE_Local = 1,
		TRANSFORMSOURCE_World = 2,
		TRANSFORMSOURCE_View = 3,
		TRANSFORMSOURCE_Camera = 4,
		TRANSFORMSOURCE_ParticleWorld = 5,
		TRANSFORMSOURCE_MAX = 6
	};

	// Enum Engine.EMaterialPositionTransformSource
	public enum EMaterialPositionTransformSource : byte {
		TRANSFORMPOSSOURCE_Local = 0,
		TRANSFORMPOSSOURCE_World = 1,
		TRANSFORMPOSSOURCE_TranslatedWorld = 2,
		TRANSFORMPOSSOURCE_View = 3,
		TRANSFORMPOSSOURCE_Camera = 4,
		TRANSFORMPOSSOURCE_Particle = 5,
		TRANSFORMPOSSOURCE_MAX = 6
	};

	// Enum Engine.EVectorNoiseFunction
	public enum EVectorNoiseFunction : byte {
		VNF_CellnoiseALU = 0,
		VNF_VectorALU = 1,
		VNF_GradientALU = 2,
		VNF_CurlALU = 3,
		VNF_VoronoiALU = 4,
		VNF_MAX = 5
	};

	// Enum Engine.EMaterialExposedViewProperty
	public enum EMaterialExposedViewProperty : byte {
		MEVP_BufferSize = 0,
		MEVP_FieldOfView = 1,
		MEVP_TanHalfFieldOfView = 2,
		MEVP_ViewSize = 3,
		MEVP_WorldSpaceViewPosition = 4,
		MEVP_WorldSpaceCameraPosition = 5,
		MEVP_ViewportOffset = 6,
		MEVP_TemporalSampleCount = 7,
		MEVP_TemporalSampleIndex = 8,
		MEVP_TemporalSampleOffset = 9,
		MEVP_RuntimeVirtualTextureOutputLevel = 10,
		MEVP_RuntimeVirtualTextureOutputDerivative = 11,
		MEVP_PreExposure = 12,
		MEVP_MAX = 13
	};

	// Enum Engine.EWorldPositionIncludedOffsets
	public enum EWorldPositionIncludedOffsets : byte {
		WPT_Default = 0,
		WPT_ExcludeAllShaderOffsets = 1,
		WPT_CameraRelative = 2,
		WPT_CameraRelativeNoOffsets = 3,
		WPT_MAX = 4
	};

	// Enum Engine.EMaterialFunctionUsage
	public enum EMaterialFunctionUsage : byte {
		Default = 0,
		MaterialLayer = 1,
		MaterialLayerBlend = 2,
		EMaterialFunctionUsage_MAX = 3
	};

	// Enum Engine.EMaterialUsage
	public enum EMaterialUsage : byte {
		MATUSAGE_SkeletalMesh = 0,
		MATUSAGE_ParticleSprites = 1,
		MATUSAGE_BeamTrails = 2,
		MATUSAGE_MeshParticles = 3,
		MATUSAGE_StaticLighting = 4,
		MATUSAGE_MorphTargets = 5,
		MATUSAGE_SplineMesh = 6,
		MATUSAGE_InstancedStaticMeshes = 7,
		MATUSAGE_GeometryCollections = 8,
		MATUSAGE_Clothing = 9,
		MATUSAGE_NiagaraSprites = 10,
		MATUSAGE_NiagaraRibbons = 11,
		MATUSAGE_NiagaraMeshParticles = 12,
		MATUSAGE_GeometryCache = 13,
		MATUSAGE_Water = 14,
		MATUSAGE_HairStrands = 15,
		MATUSAGE_LidarPointCloud = 16,
		MATUSAGE_MAX = 17
	};

	// Enum Engine.EMaterialParameterAssociation
	public enum EMaterialParameterAssociation : byte {
		LayerParameter = 0,
		BlendParameter = 1,
		GlobalParameter = 2,
		EMaterialParameterAssociation_MAX = 3
	};

	// Enum Engine.EMaterialMergeType
	public enum EMaterialMergeType : byte {
		MaterialMergeType_Default = 0,
		MaterialMergeType_Simplygon = 1,
		MaterialMergeType_MAX = 2
	};

	// Enum Engine.ETextureSizingType
	public enum ETextureSizingType : byte {
		TextureSizingType_UseSingleTextureSize = 0,
		TextureSizingType_UseAutomaticBiasedSizes = 1,
		TextureSizingType_UseManualOverrideTextureSize = 2,
		TextureSizingType_UseSimplygonAutomaticSizing = 3,
		TextureSizingType_MAX = 4
	};

	// Enum Engine.ESceneTextureId
	public enum ESceneTextureId : byte {
		PPI_SceneColor = 0,
		PPI_SceneDepth = 1,
		PPI_DiffuseColor = 2,
		PPI_SpecularColor = 3,
		PPI_SubsurfaceColor = 4,
		PPI_BaseColor = 5,
		PPI_Specular = 6,
		PPI_Metallic = 7,
		PPI_WorldNormal = 8,
		PPI_SeparateTranslucency = 9,
		PPI_Opacity = 10,
		PPI_Roughness = 11,
		PPI_MaterialAO = 12,
		PPI_CustomDepth = 13,
		PPI_PostProcessInput0 = 14,
		PPI_PostProcessInput1 = 15,
		PPI_PostProcessInput2 = 16,
		PPI_PostProcessInput3 = 17,
		PPI_PostProcessInput4 = 18,
		PPI_PostProcessInput5 = 19,
		PPI_PostProcessInput6 = 20,
		PPI_DecalMask = 21,
		PPI_ShadingModelColor = 22,
		PPI_ShadingModelID = 23,
		PPI_AmbientOcclusion = 24,
		PPI_CustomStencil = 25,
		PPI_StoredBaseColor = 26,
		PPI_StoredSpecular = 27,
		PPI_Velocity = 28,
		PPI_WorldTangent = 29,
		PPI_Anisotropy = 30,
		PPI_MAX = 31
	};

	// Enum Engine.EMaterialDomain
	public enum EMaterialDomain : byte {
		MD_Surface = 0,
		MD_DeferredDecal = 1,
		MD_LightFunction = 2,
		MD_Volume = 3,
		MD_PostProcess = 4,
		MD_UI = 5,
		MD_RuntimeVirtualTexture = 6,
		MD_MAX = 7
	};

	// Enum Engine.EMeshInstancingReplacementMethod
	public enum EMeshInstancingReplacementMethod : byte {
		RemoveOriginalActors = 0,
		KeepOriginalActorsAsEditorOnly = 1,
		EMeshInstancingReplacementMethod_MAX = 2
	};

	// Enum Engine.EUVOutput
	public enum EUVOutput : byte {
		DoNotOutputChannel = 0,
		OutputChannel = 1,
		EUVOutput_MAX = 2
	};

	// Enum Engine.EMeshMergeType
	public enum EMeshMergeType : byte {
		MeshMergeType_Default = 0,
		MeshMergeType_MergeActor = 1,
		MeshMergeType_MAX = 2
	};

	// Enum Engine.EMeshLODSelectionType
	public enum EMeshLODSelectionType : byte {
		AllLODs = 0,
		SpecificLOD = 1,
		CalculateLOD = 2,
		LowestDetailLOD = 3,
		EMeshLODSelectionType_MAX = 4
	};

	// Enum Engine.EProxyNormalComputationMethod
	public enum EProxyNormalComputationMethod : byte {
		AngleWeighted = 0,
		AreaWeighted = 1,
		EqualWeighted = 2,
		EProxyNormalComputationMethod_MAX = 3
	};

	// Enum Engine.ELandscapeCullingPrecision
	public enum ELandscapeCullingPrecision : byte {
		High = 0,
		Medium = 1,
		Low = 2,
		ELandscapeCullingPrecision_MAX = 3
	};

	// Enum Engine.EStaticMeshReductionTerimationCriterion
	public enum EStaticMeshReductionTerimationCriterion : byte {
		Triangles = 0,
		Vertices = 1,
		Any = 2,
		EStaticMeshReductionTerimationCriterion_MAX = 3
	};

	// Enum Engine.EMeshFeatureImportance
	public enum EMeshFeatureImportance : byte {
		Off = 0,
		Lowest = 1,
		Low = 2,
		Normal = 3,
		High = 4,
		Highest = 5,
		EMeshFeatureImportance_MAX = 6
	};

	// Enum Engine.EVertexPaintAxis
	public enum EVertexPaintAxis : byte {
		X = 0,
		Y = 1,
		Z = 2,
		EVertexPaintAxis_MAX = 3
	};

	// Enum Engine.EMicroTransactionResult
	public enum EMicroTransactionResult : byte {
		MTR_Succeeded = 0,
		MTR_Failed = 1,
		MTR_Canceled = 2,
		MTR_RestoredFromServer = 3,
		MTR_MAX = 4
	};

	// Enum Engine.EMicroTransactionDelegate
	public enum EMicroTransactionDelegate : byte {
		MTD_PurchaseQueryComplete = 0,
		MTD_PurchaseComplete = 1,
		MTD_MAX = 2
	};

	// Enum Engine.FNavigationSystemRunMode
	public enum FNavigationSystemRunMode : byte {
		InvalidMode = 0,
		GameMode = 1,
		EditorMode = 2,
		SimulationMode = 3,
		PIEMode = 4,
		FNavigationSystemRunMode_MAX = 5
	};

	// Enum Engine.ENavigationQueryResult
	public enum ENavigationQueryResult : byte {
		Invalid = 0,
		Error = 1,
		Fail = 2,
		Success = 3,
		ENavigationQueryResult_MAX = 4
	};

	// Enum Engine.ENavPathEvent
	public enum ENavPathEvent : byte {
		Cleared = 0,
		NewPath = 1,
		UpdatedDueToGoalMoved = 2,
		UpdatedDueToNavigationChanged = 3,
		Invalidated = 4,
		RePathFailed = 5,
		MetaPathUpdate = 6,
		Custom = 7,
		ENavPathEvent_MAX = 8
	};

	// Enum Engine.ENavDataGatheringModeConfig
	public enum ENavDataGatheringModeConfig : byte {
		Invalid = 0,
		Instant = 1,
		Lazy = 2,
		ENavDataGatheringModeConfig_MAX = 3
	};

	// Enum Engine.ENavDataGatheringMode
	public enum ENavDataGatheringMode : byte {
		Default = 0,
		Instant = 1,
		Lazy = 2,
		ENavDataGatheringMode_MAX = 3
	};

	// Enum Engine.ENavigationOptionFlag
	public enum ENavigationOptionFlag : byte {
		Default = 0,
		Enable = 1,
		Disable = 2,
		MAX = 3
	};

	// Enum Engine.ENavLinkDirection
	public enum ENavLinkDirection : byte {
		BothWays = 0,
		LeftToRight = 1,
		RightToLeft = 2,
		ENavLinkDirection_MAX = 3
	};

	// Enum Engine.EFastArraySerializerDeltaFlags
	public enum EFastArraySerializerDeltaFlags : byte {
		None = 0,
		HasBeenSerialized = 1,
		HasDeltaBeenRequested = 2,
		IsUsingDeltaSerialization = 4,
		EFastArraySerializerDeltaFlags_MAX = 5
	};

	// Enum Engine.EEmitterRenderMode
	public enum EEmitterRenderMode : byte {
		ERM_Normal = 0,
		ERM_Point = 1,
		ERM_Cross = 2,
		ERM_LightsOnly = 3,
		ERM_None = 4,
		ERM_MAX = 5
	};

	// Enum Engine.EParticleSubUVInterpMethod
	public enum EParticleSubUVInterpMethod : byte {
		PSUVIM_None = 0,
		PSUVIM_Linear = 1,
		PSUVIM_Linear_Blend = 2,
		PSUVIM_Random = 3,
		PSUVIM_Random_Blend = 4,
		PSUVIM_MAX = 5
	};

	// Enum Engine.EParticleBurstMethod
	public enum EParticleBurstMethod : byte {
		EPBM_Instant = 0,
		EPBM_Interpolated = 1,
		EPBM_MAX = 2
	};

	// Enum Engine.EParticleSystemInsignificanceReaction
	public enum EParticleSystemInsignificanceReaction : byte {
		Auto = 0,
		Complete = 1,
		DisableTick = 2,
		DisableTickAndKill = 3,
		Num = 4,
		EParticleSystemInsignificanceReaction_MAX = 5
	};

	// Enum Engine.EParticleSignificanceLevel
	public enum EParticleSignificanceLevel : byte {
		Low = 0,
		Medium = 1,
		High = 2,
		Critical = 3,
		Num = 4,
		EParticleSignificanceLevel_MAX = 5
	};

	// Enum Engine.EParticleDetailMode
	public enum EParticleDetailMode : byte {
		PDM_Low = 0,
		PDM_Medium = 1,
		PDM_High = 2,
		PDM_MAX = 3
	};

	// Enum Engine.EParticleSourceSelectionMethod
	public enum EParticleSourceSelectionMethod : byte {
		EPSSM_Random = 0,
		EPSSM_Sequential = 1,
		EPSSM_MAX = 2
	};

	// Enum Engine.EModuleType
	public enum EModuleType : byte {
		EPMT_General = 0,
		EPMT_TypeData = 1,
		EPMT_Beam = 2,
		EPMT_Trail = 3,
		EPMT_Spawn = 4,
		EPMT_Required = 5,
		EPMT_Event = 6,
		EPMT_Light = 7,
		EPMT_SubUV = 8,
		EPMT_MAX = 9
	};

	// Enum Engine.EAttractorParticleSelectionMethod
	public enum EAttractorParticleSelectionMethod : byte {
		EAPSM_Random = 0,
		EAPSM_Sequential = 1,
		EAPSM_MAX = 2
	};

	// Enum Engine.Beam2SourceTargetTangentMethod
	public enum Beam2SourceTargetTangentMethod : byte {
		PEB2STTM_Direct = 0,
		PEB2STTM_UserSet = 1,
		PEB2STTM_Distribution = 2,
		PEB2STTM_Emitter = 3,
		PEB2STTM_MAX = 4
	};

	// Enum Engine.Beam2SourceTargetMethod
	public enum Beam2SourceTargetMethod : byte {
		PEB2STM_Default = 0,
		PEB2STM_UserSet = 1,
		PEB2STM_Emitter = 2,
		PEB2STM_Particle = 3,
		PEB2STM_Actor = 4,
		PEB2STM_MAX = 5
	};

	// Enum Engine.BeamModifierType
	public enum BeamModifierType : byte {
		PEB2MT_Source = 0,
		PEB2MT_Target = 1,
		PEB2MT_MAX = 2
	};

	// Enum Engine.EParticleCameraOffsetUpdateMethod
	public enum EParticleCameraOffsetUpdateMethod : byte {
		EPCOUM_DirectSet = 0,
		EPCOUM_Additive = 1,
		EPCOUM_Scalar = 2,
		EPCOUM_MAX = 3
	};

	// Enum Engine.EParticleCollisionComplete
	public enum EParticleCollisionComplete : byte {
		EPCC_Kill = 0,
		EPCC_Freeze = 1,
		EPCC_HaltCollisions = 2,
		EPCC_FreezeTranslation = 3,
		EPCC_FreezeRotation = 4,
		EPCC_FreezeMovement = 5,
		EPCC_MAX = 6
	};

	// Enum Engine.EParticleCollisionResponse
	public enum EParticleCollisionResponse : byte {
		Bounce = 0,
		Stop = 1,
		Kill = 2,
		EParticleCollisionResponse_MAX = 3
	};

	// Enum Engine.ELocationBoneSocketSelectionMethod
	public enum ELocationBoneSocketSelectionMethod : byte {
		BONESOCKETSEL_Sequential = 0,
		BONESOCKETSEL_Random = 1,
		BONESOCKETSEL_MAX = 2
	};

	// Enum Engine.ELocationBoneSocketSource
	public enum ELocationBoneSocketSource : byte {
		BONESOCKETSOURCE_Bones = 0,
		BONESOCKETSOURCE_Sockets = 1,
		BONESOCKETSOURCE_MAX = 2
	};

	// Enum Engine.ELocationEmitterSelectionMethod
	public enum ELocationEmitterSelectionMethod : byte {
		ELESM_Random = 0,
		ELESM_Sequential = 1,
		ELESM_MAX = 2
	};

	// Enum Engine.CylinderHeightAxis
	public enum CylinderHeightAxis : byte {
		PMLPC_HEIGHTAXIS_X = 0,
		PMLPC_HEIGHTAXIS_Y = 1,
		PMLPC_HEIGHTAXIS_Z = 2,
		PMLPC_HEIGHTAXIS_MAX = 3
	};

	// Enum Engine.ELocationSkelVertSurfaceSource
	public enum ELocationSkelVertSurfaceSource : byte {
		VERTSURFACESOURCE_Vert = 0,
		VERTSURFACESOURCE_Surface = 1,
		VERTSURFACESOURCE_MAX = 2
	};

	// Enum Engine.EOrbitChainMode
	public enum EOrbitChainMode : byte {
		EOChainMode_Add = 0,
		EOChainMode_Scale = 1,
		EOChainMode_Link = 2,
		EOChainMode_MAX = 3
	};

	// Enum Engine.EParticleAxisLock
	public enum EParticleAxisLock : byte {
		EPAL_NONE = 0,
		EPAL_X = 1,
		EPAL_Y = 2,
		EPAL_Z = 3,
		EPAL_NEGATIVE_X = 4,
		EPAL_NEGATIVE_Y = 5,
		EPAL_NEGATIVE_Z = 6,
		EPAL_ROTATE_X = 7,
		EPAL_ROTATE_Y = 8,
		EPAL_ROTATE_Z = 9,
		EPAL_MAX = 10
	};

	// Enum Engine.EEmitterDynamicParameterValue
	public enum EEmitterDynamicParameterValue : byte {
		EDPV_UserSet = 0,
		EDPV_AutoSet = 1,
		EDPV_VelocityX = 2,
		EDPV_VelocityY = 3,
		EDPV_VelocityZ = 4,
		EDPV_VelocityMag = 5,
		EDPV_MAX = 6
	};

	// Enum Engine.EEmitterNormalsMode
	public enum EEmitterNormalsMode : byte {
		ENM_CameraFacing = 0,
		ENM_Spherical = 1,
		ENM_Cylindrical = 2,
		ENM_MAX = 3
	};

	// Enum Engine.EParticleSortMode
	public enum EParticleSortMode : byte {
		PSORTMODE_None = 0,
		PSORTMODE_ViewProjDepth = 1,
		PSORTMODE_DistanceToView = 2,
		PSORTMODE_Age_OldestFirst = 3,
		PSORTMODE_Age_NewestFirst = 4,
		PSORTMODE_MAX = 5
	};

	// Enum Engine.EParticleUVFlipMode
	public enum EParticleUVFlipMode : byte {
		None = 0,
		FlipUV = 1,
		FlipUOnly = 2,
		FlipVOnly = 3,
		RandomFlipUV = 4,
		RandomFlipUOnly = 5,
		RandomFlipVOnly = 6,
		RandomFlipUVIndependent = 7,
		EParticleUVFlipMode_MAX = 8
	};

	// Enum Engine.ETrail2SourceMethod
	public enum ETrail2SourceMethod : byte {
		PET2SRCM_Default = 0,
		PET2SRCM_Particle = 1,
		PET2SRCM_Actor = 2,
		PET2SRCM_MAX = 3
	};

	// Enum Engine.EBeamTaperMethod
	public enum EBeamTaperMethod : byte {
		PEBTM_None = 0,
		PEBTM_Full = 1,
		PEBTM_Partial = 2,
		PEBTM_MAX = 3
	};

	// Enum Engine.EBeam2Method
	public enum EBeam2Method : byte {
		PEB2M_Distance = 0,
		PEB2M_Target = 1,
		PEB2M_Branch = 2,
		PEB2M_MAX = 3
	};

	// Enum Engine.EMeshCameraFacingOptions
	public enum EMeshCameraFacingOptions : byte {
		XAxisFacing_NoUp = 0,
		XAxisFacing_ZUp = 1,
		XAxisFacing_NegativeZUp = 2,
		XAxisFacing_YUp = 3,
		XAxisFacing_NegativeYUp = 4,
		LockedAxis_ZAxisFacing = 5,
		LockedAxis_NegativeZAxisFacing = 6,
		LockedAxis_YAxisFacing = 7,
		LockedAxis_NegativeYAxisFacing = 8,
		VelocityAligned_ZAxisFacing = 9,
		VelocityAligned_NegativeZAxisFacing = 10,
		VelocityAligned_YAxisFacing = 11,
		VelocityAligned_NegativeYAxisFacing = 12,
		EMeshCameraFacingOptions_MAX = 13
	};

	// Enum Engine.EMeshCameraFacingUpAxis
	public enum EMeshCameraFacingUpAxis : byte {
		CameraFacing_NoneUP = 0,
		CameraFacing_ZUp = 1,
		CameraFacing_NegativeZUp = 2,
		CameraFacing_YUp = 3,
		CameraFacing_NegativeYUp = 4,
		CameraFacing_MAX = 5
	};

	// Enum Engine.EMeshScreenAlignment
	public enum EMeshScreenAlignment : byte {
		PSMA_MeshFaceCameraWithRoll = 0,
		PSMA_MeshFaceCameraWithSpin = 1,
		PSMA_MeshFaceCameraWithLockedAxis = 2,
		PSMA_MAX = 3
	};

	// Enum Engine.ETrailsRenderAxisOption
	public enum ETrailsRenderAxisOption : byte {
		Trails_CameraUp = 0,
		Trails_SourceUp = 1,
		Trails_WorldUp = 2,
		Trails_MAX = 3
	};

	// Enum Engine.EParticleScreenAlignment
	public enum EParticleScreenAlignment : byte {
		PSA_FacingCameraPosition = 0,
		PSA_Square = 1,
		PSA_Rectangle = 2,
		PSA_Velocity = 3,
		PSA_AwayFromCenter = 4,
		PSA_TypeSpecific = 5,
		PSA_FacingCameraDistanceBlend = 6,
		PSA_MAX = 7
	};

	// Enum Engine.EParticleSystemOcclusionBoundsMethod
	public enum EParticleSystemOcclusionBoundsMethod : byte {
		EPSOBM_None = 0,
		EPSOBM_ParticleBounds = 1,
		EPSOBM_CustomBounds = 2,
		EPSOBM_MAX = 3
	};

	// Enum Engine.ParticleSystemLODMethod
	public enum ParticleSystemLODMethod : byte {
		PARTICLESYSTEMLODMETHOD_Automatic = 0,
		PARTICLESYSTEMLODMETHOD_DirectSet = 1,
		PARTICLESYSTEMLODMETHOD_ActivateAutomatic = 2,
		PARTICLESYSTEMLODMETHOD_MAX = 3
	};

	// Enum Engine.EParticleSystemUpdateMode
	public enum EParticleSystemUpdateMode : byte {
		EPSUM_RealTime = 0,
		EPSUM_FixedTime = 1,
		EPSUM_MAX = 2
	};

	// Enum Engine.EParticleEventType
	public enum EParticleEventType : byte {
		EPET_Any = 0,
		EPET_Spawn = 1,
		EPET_Death = 2,
		EPET_Collision = 3,
		EPET_Burst = 4,
		EPET_Blueprint = 5,
		EPET_MAX = 6
	};

	// Enum Engine.ParticleReplayState
	public enum ParticleReplayState : byte {
		PRS_Disabled = 0,
		PRS_Capturing = 1,
		PRS_Replaying = 2,
		PRS_MAX = 3
	};

	// Enum Engine.EParticleSysParamType
	public enum EParticleSysParamType : byte {
		PSPT_None = 0,
		PSPT_Scalar = 1,
		PSPT_ScalarRand = 2,
		PSPT_Vector = 3,
		PSPT_VectorRand = 4,
		PSPT_Color = 5,
		PSPT_Actor = 6,
		PSPT_Material = 7,
		PSPT_VectorUnitRand = 8,
		PSPT_MAX = 9
	};

	// Enum Engine.ESettingsLockedAxis
	public enum ESettingsLockedAxis : byte {
		None = 0,
		X = 1,
		Y = 2,
		Z = 3,
		Invalid = 4,
		ESettingsLockedAxis_MAX = 5
	};

	// Enum Engine.ESettingsDOF
	public enum ESettingsDOF : byte {
		Full3D = 0,
		YZPlane = 1,
		XZPlane = 2,
		XYPlane = 3,
		ESettingsDOF_MAX = 4
	};

	// Enum Engine.EFrictionCombineMode
	public enum EFrictionCombineMode : byte {
		Average = 0,
		Min = 1,
		Multiply = 2,
		Max = 3
	};

	// Enum Engine.EViewTargetBlendFunction
	public enum EViewTargetBlendFunction : byte {
		VTBlend_Linear = 0,
		VTBlend_Cubic = 1,
		VTBlend_EaseIn = 2,
		VTBlend_EaseOut = 3,
		VTBlend_EaseInOut = 4,
		VTBlend_MAX = 5
	};

	// Enum Engine.EDynamicForceFeedbackAction
	public enum EDynamicForceFeedbackAction : byte {
		Start = 0,
		Update = 1,
		Stop = 2,
		EDynamicForceFeedbackAction_MAX = 3
	};

	// Enum Engine.ERendererStencilMask
	public enum ERendererStencilMask : byte {
		ERSM_Default = 0,
		ERSM_256 = 1,
		ERSM_2 = 2,
		ERSM_3 = 3,
		ERSM_5 = 4,
		ERSM_9 = 5,
		ERSM_17 = 6,
		ERSM_33 = 7,
		ERSM_65 = 8,
		ERSM_129 = 9,
		ERSM_MAX = 10
	};

	// Enum Engine.EHasCustomNavigableGeometry
	public enum EHasCustomNavigableGeometry : byte {
		No = 0,
		Yes = 1,
		EvenIfNotCollidable = 2,
		DontExport = 3,
		EHasCustomNavigableGeometry_MAX = 4
	};

	// Enum Engine.ECanBeCharacterBase
	public enum ECanBeCharacterBase : byte {
		ECB_No = 0,
		ECB_Yes = 1,
		ECB_Owner = 2,
		ECB_MAX = 3
	};

	// Enum Engine.ERichCurveExtrapolation
	public enum ERichCurveExtrapolation : byte {
		RCCE_Cycle = 0,
		RCCE_CycleWithOffset = 1,
		RCCE_Oscillate = 2,
		RCCE_Linear = 3,
		RCCE_Constant = 4,
		RCCE_None = 5,
		RCCE_MAX = 6
	};

	// Enum Engine.ERichCurveInterpMode
	public enum ERichCurveInterpMode : byte {
		RCIM_Linear = 0,
		RCIM_Constant = 1,
		RCIM_Cubic = 2,
		RCIM_None = 3,
		RCIM_MAX = 4
	};

	// Enum Engine.EReflectionSourceType
	public enum EReflectionSourceType : byte {
		CapturedScene = 0,
		SpecifiedCubemap = 1,
		EReflectionSourceType_MAX = 2
	};

	// Enum Engine.EDefaultBackBufferPixelFormat
	public enum EDefaultBackBufferPixelFormat : byte {
		DBBPF_B8G8R8A8 = 0,
		DBBPF_A16B16G16R16_DEPRECATED = 1,
		DBBPF_FloatRGB_DEPRECATED = 2,
		DBBPF_FloatRGBA = 3,
		DBBPF_A2B10G10R10 = 4,
		DBBPF_MAX = 5
	};

	// Enum Engine.EAutoExposureMethodUI
	public enum EAutoExposureMethodUI : byte {
		AEM_Histogram = 0,
		AEM_Basic = 1,
		AEM_Manual = 2,
		AEM_MAX = 3
	};

	// Enum Engine.EAlphaChannelMode
	public enum EAlphaChannelMode : byte {
		Disabled = 0,
		LinearColorSpaceOnly = 1,
		AllowThroughTonemapper = 2,
		EAlphaChannelMode_MAX = 3
	};

	// Enum Engine.EEarlyZPass
	public enum EEarlyZPass : byte {
		None = 0,
		OpaqueOnly = 1,
		OpaqueAndMasked = 2,
		Auto = 3,
		EEarlyZPass_MAX = 4
	};

	// Enum Engine.ECustomDepthStencil
	public enum ECustomDepthStencil : byte {
		Disabled = 0,
		Enabled = 1,
		EnabledOnDemand = 2,
		EnabledWithStencil = 3,
		ECustomDepthStencil_MAX = 4
	};

	// Enum Engine.EMobileMSAASampleCount
	public enum EMobileMSAASampleCount : byte {
		One = 1,
		Two = 2,
		Four = 4,
		Eight = 8,
		EMobileMSAASampleCount_MAX = 9
	};

	// Enum Engine.ECompositingSampleCount
	public enum ECompositingSampleCount : byte {
		One = 1,
		Two = 2,
		Four = 4,
		Eight = 8,
		ECompositingSampleCount_MAX = 9
	};

	// Enum Engine.EClearSceneOptions
	public enum EClearSceneOptions : byte {
		NoClear = 0,
		HardwareClear = 1,
		QuadAtMaxZ = 2,
		EClearSceneOptions_MAX = 3
	};

	// Enum Engine.EReporterLineStyle
	public enum EReporterLineStyle : byte {
		Line = 0,
		Dash = 1,
		EReporterLineStyle_MAX = 2
	};

	// Enum Engine.ELegendPosition
	public enum ELegendPosition : byte {
		Outside = 0,
		Inside = 1,
		ELegendPosition_MAX = 2
	};

	// Enum Engine.EGraphDataStyle
	public enum EGraphDataStyle : byte {
		Lines = 0,
		Filled = 1,
		EGraphDataStyle_MAX = 2
	};

	// Enum Engine.EGraphAxisStyle
	public enum EGraphAxisStyle : byte {
		Lines = 0,
		Notches = 1,
		Grid = 2,
		EGraphAxisStyle_MAX = 3
	};

	// Enum Engine.ReverbPreset
	public enum ReverbPreset : byte {
		REVERB_Default = 0,
		REVERB_Bathroom = 1,
		REVERB_StoneRoom = 2,
		REVERB_Auditorium = 3,
		REVERB_ConcertHall = 4,
		REVERB_Cave = 5,
		REVERB_Hallway = 6,
		REVERB_StoneCorridor = 7,
		REVERB_Alley = 8,
		REVERB_Forest = 9,
		REVERB_City = 10,
		REVERB_Mountains = 11,
		REVERB_Quarry = 12,
		REVERB_Plain = 13,
		REVERB_ParkingLot = 14,
		REVERB_SewerPipe = 15,
		REVERB_Underwater = 16,
		REVERB_SmallRoom = 17,
		REVERB_MediumRoom = 18,
		REVERB_LargeRoom = 19,
		REVERB_MediumHall = 20,
		REVERB_LargeHall = 21,
		REVERB_Plate = 22,
		REVERB_MAX = 23
	};

	// Enum Engine.ERichCurveKeyTimeCompressionFormat
	public enum ERichCurveKeyTimeCompressionFormat : byte {
		RCKTCF_uint16 = 0,
		RCKTCF_float32 = 1,
		RCKTCF_MAX = 2
	};

	// Enum Engine.ERichCurveCompressionFormat
	public enum ERichCurveCompressionFormat : byte {
		RCCF_Empty = 0,
		RCCF_Constant = 1,
		RCCF_Linear = 2,
		RCCF_Cubic = 3,
		RCCF_Mixed = 4,
		RCCF_MAX = 5
	};

	// Enum Engine.ERichCurveTangentWeightMode
	public enum ERichCurveTangentWeightMode : byte {
		RCTWM_WeightedNone = 0,
		RCTWM_WeightedArrive = 1,
		RCTWM_WeightedLeave = 2,
		RCTWM_WeightedBoth = 3,
		RCTWM_MAX = 4
	};

	// Enum Engine.ERichCurveTangentMode
	public enum ERichCurveTangentMode : byte {
		RCTM_Auto = 0,
		RCTM_User = 1,
		RCTM_Break = 2,
		RCTM_None = 3,
		RCTM_MAX = 4
	};

	// Enum Engine.EConstraintTransform
	public enum EConstraintTransform : byte {
		Absolute = 0,
		Relative = 1,
		EConstraintTransform_MAX = 2
	};

	// Enum Engine.EControlConstraint
	public enum EControlConstraint : byte {
		Orientation = 0,
		Translation = 1,
		MAX = 2
	};

	// Enum Engine.ERootMotionFinishVelocityMode
	public enum ERootMotionFinishVelocityMode : byte {
		MaintainLastRootMotionVelocity = 0,
		SetVelocity = 1,
		ClampVelocity = 2,
		ERootMotionFinishVelocityMode_MAX = 3
	};

	// Enum Engine.ERootMotionSourceSettingsFlags
	public enum ERootMotionSourceSettingsFlags : byte {
		UseSensitiveLiftoffCheck = 1,
		DisablePartialEndTick = 2,
		IgnoreZAccumulate = 4,
		ERootMotionSourceSettingsFlags_MAX = 5
	};

	// Enum Engine.ERootMotionSourceStatusFlags
	public enum ERootMotionSourceStatusFlags : byte {
		Prepared = 1,
		Finished = 2,
		MarkedForRemoval = 4,
		ERootMotionSourceStatusFlags_MAX = 5
	};

	// Enum Engine.ERootMotionAccumulateMode
	public enum ERootMotionAccumulateMode : byte {
		Override = 0,
		Additive = 1,
		ERootMotionAccumulateMode_MAX = 2
	};

	// Enum Engine.ERuntimeVirtualTextureMainPassType
	public enum ERuntimeVirtualTextureMainPassType : byte {
		Never = 0,
		Exclusive = 1,
		Always = 2,
		ERuntimeVirtualTextureMainPassType_MAX = 3
	};

	// Enum Engine.ERuntimeVirtualTextureMaterialType
	public enum ERuntimeVirtualTextureMaterialType : byte {
		BaseColor = 0,
		BaseColor_Normal_DEPRECATED = 1,
		BaseColor_Normal_Specular = 2,
		BaseColor_Normal_Specular_YCoCg = 3,
		BaseColor_Normal_Specular_Mask_YCoCg = 4,
		WorldHeight = 5,
		Count = 6,
		ERuntimeVirtualTextureMaterialType_MAX = 7
	};

	// Enum Engine.EReflectedAndRefractedRayTracedShadows
	public enum EReflectedAndRefractedRayTracedShadows : byte {
		Disabled = 0,
		Hard_shadows = 1,
		Area_shadows = 2,
		EReflectedAndRefractedRayTracedShadows_MAX = 3
	};

	// Enum Engine.ERayTracingGlobalIlluminationType
	public enum ERayTracingGlobalIlluminationType : byte {
		Disabled = 0,
		BruteForce = 1,
		FinalGather = 2,
		ERayTracingGlobalIlluminationType_MAX = 3
	};

	// Enum Engine.ETranslucencyType
	public enum ETranslucencyType : byte {
		Raster = 0,
		RayTracing = 1,
		ETranslucencyType_MAX = 2
	};

	// Enum Engine.EReflectionsType
	public enum EReflectionsType : byte {
		ScreenSpace = 0,
		RayTracing = 1,
		EReflectionsType_MAX = 2
	};

	// Enum Engine.ELightUnits
	public enum ELightUnits : byte {
		Unitless = 0,
		Candelas = 1,
		Lumens = 2,
		ELightUnits_MAX = 3
	};

	// Enum Engine.EBloomMethod
	public enum EBloomMethod : byte {
		BM_SOG = 0,
		BM_FFT = 1,
		BM_MAX = 2
	};

	// Enum Engine.EAutoExposureMethod
	public enum EAutoExposureMethod : byte {
		AEM_Histogram = 0,
		AEM_Basic = 1,
		AEM_Manual = 2,
		AEM_MAX = 3
	};

	// Enum Engine.EAntiAliasingMethod
	public enum EAntiAliasingMethod : byte {
		AAM_None = 0,
		AAM_FXAA = 1,
		AAM_TemporalAA = 2,
		AAM_MSAA = 3,
		AAM_DLSS = 4,
		AAM_MAX = 5
	};

	// Enum Engine.EDepthOfFieldMethod
	public enum EDepthOfFieldMethod : byte {
		DOFM_BokehDOF = 0,
		DOFM_Gaussian = 1,
		DOFM_CircleDOF = 2,
		DOFM_MAX = 3
	};

	// Enum Engine.ESceneCapturePrimitiveRenderMode
	public enum ESceneCapturePrimitiveRenderMode : byte {
		PRM_LegacySceneCapture = 0,
		PRM_RenderScenePrimitives = 1,
		PRM_UseShowOnlyList = 2,
		PRM_MAX = 3
	};

	// Enum Engine.EMaterialProperty
	public enum EMaterialProperty : byte {
		MP_EmissiveColor = 0,
		MP_Opacity = 1,
		MP_OpacityMask = 2,
		MP_DiffuseColor = 3,
		MP_SpecularColor = 4,
		MP_BaseColor = 5,
		MP_Metallic = 6,
		MP_Specular = 7,
		MP_Roughness = 8,
		MP_Anisotropy = 9,
		MP_Normal = 10,
		MP_Tangent = 11,
		MP_WorldPositionOffset = 12,
		MP_WorldDisplacement = 13,
		MP_TessellationMultiplier = 14,
		MP_SubsurfaceColor = 15,
		MP_CustomData0 = 16,
		MP_CustomData1 = 17,
		MP_AmbientOcclusion = 18,
		MP_Refraction = 19,
		MP_CustomizedUVs0 = 20,
		MP_CustomizedUVs1 = 21,
		MP_CustomizedUVs2 = 22,
		MP_CustomizedUVs3 = 23,
		MP_CustomizedUVs4 = 24,
		MP_CustomizedUVs5 = 25,
		MP_CustomizedUVs6 = 26,
		MP_CustomizedUVs7 = 27,
		MP_PixelDepthOffset = 28,
		MP_ShadingModel = 29,
		MP_MaterialAttributes = 30,
		MP_CustomOutput = 31,
		MP_MAX = 32
	};

	// Enum Engine.ESkinCacheDefaultBehavior
	public enum ESkinCacheDefaultBehavior : byte {
		Exclusive = 0,
		Inclusive = 1,
		ESkinCacheDefaultBehavior_MAX = 2
	};

	// Enum Engine.ESkinCacheUsage
	public enum ESkinCacheUsage : byte {
		Auto = 0,
		Disabled = 255,
		Enabled = 1,
		//ESkinCacheUsage_MAX = 256
	};

	// Enum Engine.EPhysicsTransformUpdateMode
	public enum EPhysicsTransformUpdateMode : byte {
		SimulationUpatesComponentTransform = 0,
		ComponentTransformIsKinematic = 1,
		EPhysicsTransformUpdateMode_MAX = 2
	};

	// Enum Engine.EAnimationMode
	public enum EAnimationMode : byte {
		AnimationBlueprint = 0,
		AnimationSingleNode = 1,
		AnimationCustomMode = 2,
		EAnimationMode_MAX = 3
	};

	// Enum Engine.EKinematicBonesUpdateToPhysics
	public enum EKinematicBonesUpdateToPhysics : byte {
		SkipSimulatingBones = 0,
		SkipAllBones = 1,
		EKinematicBonesUpdateToPhysics_MAX = 2
	};

	// Enum Engine.EClothMassMode
	public enum EClothMassMode : byte {
		UniformMass = 0,
		TotalMass = 1,
		Density = 2,
		MaxClothMassMode = 3,
		EClothMassMode_MAX = 4
	};

	// Enum Engine.EAnimCurveType
	public enum EAnimCurveType : byte {
		AttributeCurve = 0,
		MaterialCurve = 1,
		MorphTargetCurve = 2,
		MaxAnimCurveType = 3,
		EAnimCurveType_MAX = 4
	};

	// Enum Engine.ESkeletalMeshSkinningImportVersions
	public enum ESkeletalMeshSkinningImportVersions : byte {
		Before_Versionning = 0,
		SkeletalMeshBuildRefactor = 1,
		VersionPlusOne = 2,
		LatestVersion = 1,
		ESkeletalMeshSkinningImportVersions_MAX = 3
	};

	// Enum Engine.ESkeletalMeshGeoImportVersions
	public enum ESkeletalMeshGeoImportVersions : byte {
		Before_Versionning = 0,
		SkeletalMeshBuildRefactor = 1,
		VersionPlusOne = 2,
		LatestVersion = 1,
		ESkeletalMeshGeoImportVersions_MAX = 3
	};

	// Enum Engine.EBoneFilterActionOption
	public enum EBoneFilterActionOption : byte {
		Remove = 0,
		Keep = 1,
		Invalid = 2,
		EBoneFilterActionOption_MAX = 3
	};

	// Enum Engine.SkeletalMeshOptimizationImportance
	public enum SkeletalMeshOptimizationImportance : byte {
		SMOI_Off = 0,
		SMOI_Lowest = 1,
		SMOI_Low = 2,
		SMOI_Normal = 3,
		SMOI_High = 4,
		SMOI_Highest = 5,
		SMOI_MAX = 6
	};

	// Enum Engine.SkeletalMeshOptimizationType
	public enum SkeletalMeshOptimizationType : byte {
		SMOT_NumOfTriangles = 0,
		SMOT_MaxDeviation = 1,
		SMOT_TriangleOrDeviation = 2,
		SMOT_MAX = 3
	};

	// Enum Engine.SkeletalMeshTerminationCriterion
	public enum SkeletalMeshTerminationCriterion : byte {
		SMTC_NumOfTriangles = 0,
		SMTC_NumOfVerts = 1,
		SMTC_TriangleOrVert = 2,
		SMTC_AbsNumOfTriangles = 3,
		SMTC_AbsNumOfVerts = 4,
		SMTC_AbsTriangleOrVert = 5,
		SMTC_MAX = 6
	};

	// Enum Engine.EBoneTranslationRetargetingMode
	public enum EBoneTranslationRetargetingMode : byte {
		Animation = 0,
		Skeleton = 1,
		AnimationScaled = 2,
		AnimationRelative = 3,
		OrientAndScale = 4,
		EBoneTranslationRetargetingMode_MAX = 5
	};

	// Enum Engine.EBoneSpaces
	public enum EBoneSpaces : byte {
		WorldSpace = 0,
		ComponentSpace = 1,
		EBoneSpaces_MAX = 2
	};

	// Enum Engine.EVisibilityBasedAnimTickOption
	public enum EVisibilityBasedAnimTickOption : byte {
		AlwaysTickPoseAndRefreshBones = 0,
		AlwaysTickPose = 1,
		OnlyTickMontagesWhenNotRendered = 2,
		OnlyTickPoseWhenRendered = 3,
		EVisibilityBasedAnimTickOption_MAX = 4
	};

	// Enum Engine.EPhysBodyOp
	public enum EPhysBodyOp : byte {
		PBO_None = 0,
		PBO_Term = 1,
		PBO_MAX = 2
	};

	// Enum Engine.EBoneVisibilityStatus
	public enum EBoneVisibilityStatus : byte {
		BVS_HiddenByParent = 0,
		BVS_Visible = 1,
		BVS_ExplicitlyHidden = 2,
		BVS_MAX = 3
	};

	// Enum Engine.ESkyAtmosphereTransformMode
	public enum ESkyAtmosphereTransformMode : byte {
		PlanetTopAtAbsoluteWorldOrigin = 0,
		PlanetTopAtComponentTransform = 1,
		PlanetCenterAtComponentTransform = 2,
		ESkyAtmosphereTransformMode_MAX = 3
	};

	// Enum Engine.ESkyLightSourceType
	public enum ESkyLightSourceType : byte {
		SLS_CapturedScene = 0,
		SLS_SpecifiedCubemap = 1,
		SLS_MAX = 2
	};

	// Enum Engine.EPriorityAttenuationMethod
	public enum EPriorityAttenuationMethod : byte {
		Linear = 0,
		CustomCurve = 1,
		Manual = 2,
		EPriorityAttenuationMethod_MAX = 3
	};

	// Enum Engine.ESubmixSendMethod
	public enum ESubmixSendMethod : byte {
		Linear = 0,
		CustomCurve = 1,
		Manual = 2,
		ESubmixSendMethod_MAX = 3
	};

	// Enum Engine.EReverbSendMethod
	public enum EReverbSendMethod : byte {
		Linear = 0,
		CustomCurve = 1,
		Manual = 2,
		EReverbSendMethod_MAX = 3
	};

	// Enum Engine.EAirAbsorptionMethod
	public enum EAirAbsorptionMethod : byte {
		Linear = 0,
		CustomCurve = 1,
		EAirAbsorptionMethod_MAX = 2
	};

	// Enum Engine.ESoundSpatializationAlgorithm
	public enum ESoundSpatializationAlgorithm : byte {
		SPATIALIZATION_Default = 0,
		SPATIALIZATION_HRTF = 1,
		SPATIALIZATION_MAX = 2
	};

	// Enum Engine.ESoundDistanceCalc
	public enum ESoundDistanceCalc : byte {
		SOUNDDISTANCE_Normal = 0,
		SOUNDDISTANCE_InfiniteXYPlane = 1,
		SOUNDDISTANCE_InfiniteXZPlane = 2,
		SOUNDDISTANCE_InfiniteYZPlane = 3,
		SOUNDDISTANCE_MAX = 4
	};

	// Enum Engine.EVirtualizationMode
	public enum EVirtualizationMode : byte {
		Disabled = 0,
		PlayWhenSilent = 1,
		Restart = 2,
		EVirtualizationMode_MAX = 3
	};

	// Enum Engine.EMaxConcurrentResolutionRule
	public enum EMaxConcurrentResolutionRule : byte {
		PreventNew = 0,
		StopOldest = 1,
		StopFarthestThenPreventNew = 2,
		StopFarthestThenOldest = 3,
		StopLowestPriority = 4,
		StopQuietest = 5,
		StopLowestPriorityThenPreventNew = 6,
		Count = 7,
		EMaxConcurrentResolutionRule_MAX = 8
	};

	// Enum Engine.ESoundGroup
	public enum ESoundGroup : byte {
		SOUNDGROUP_Default = 0,
		SOUNDGROUP_Effects = 1,
		SOUNDGROUP_UI = 2,
		SOUNDGROUP_Music = 3,
		SOUNDGROUP_Voice = 4,
		SOUNDGROUP_GameSoundGroup1 = 5,
		SOUNDGROUP_GameSoundGroup2 = 6,
		SOUNDGROUP_GameSoundGroup3 = 7,
		SOUNDGROUP_GameSoundGroup4 = 8,
		SOUNDGROUP_GameSoundGroup5 = 9,
		SOUNDGROUP_GameSoundGroup6 = 10,
		SOUNDGROUP_GameSoundGroup7 = 11,
		SOUNDGROUP_GameSoundGroup8 = 12,
		SOUNDGROUP_GameSoundGroup9 = 13,
		SOUNDGROUP_GameSoundGroup10 = 14,
		SOUNDGROUP_GameSoundGroup11 = 15,
		SOUNDGROUP_GameSoundGroup12 = 16,
		SOUNDGROUP_GameSoundGroup13 = 17,
		SOUNDGROUP_GameSoundGroup14 = 18,
		SOUNDGROUP_GameSoundGroup15 = 19,
		SOUNDGROUP_GameSoundGroup16 = 20,
		SOUNDGROUP_GameSoundGroup17 = 21,
		SOUNDGROUP_GameSoundGroup18 = 22,
		SOUNDGROUP_GameSoundGroup19 = 23,
		SOUNDGROUP_GameSoundGroup20 = 24,
		SOUNDGROUP_MAX = 25
	};

	// Enum Engine.ModulationParamMode
	public enum ModulationParamMode : byte {
		MPM_Normal = 0,
		MPM_Abs = 1,
		MPM_Direct = 2,
		MPM_MAX = 3
	};

	// Enum Engine.ESourceBusChannels
	public enum ESourceBusChannels : byte {
		Mono = 0,
		Stereo = 1,
		ESourceBusChannels_MAX = 2
	};

	// Enum Engine.ESourceBusSendLevelControlMethod
	public enum ESourceBusSendLevelControlMethod : byte {
		Linear = 0,
		CustomCurve = 1,
		Manual = 2,
		ESourceBusSendLevelControlMethod_MAX = 3
	};

	// Enum Engine.ESendLevelControlMethod
	public enum ESendLevelControlMethod : byte {
		Linear = 0,
		CustomCurve = 1,
		Manual = 2,
		ESendLevelControlMethod_MAX = 3
	};

	// Enum Engine.EAudioRecordingExportType
	public enum EAudioRecordingExportType : byte {
		SoundWave = 0,
		WavFile = 1,
		EAudioRecordingExportType_MAX = 2
	};

	// Enum Engine.ESoundWaveFFTSize
	public enum ESoundWaveFFTSize : byte {
		VerySmall_65 = 0,
		Small_257 = 1,
		Medium_513 = 2,
		Large_1025 = 3,
		VeryLarge_2049 = 4,
		ESoundWaveFFTSize_MAX = 5
	};

	// Enum Engine.EDecompressionType
	public enum EDecompressionType : byte {
		DTYPE_Setup = 0,
		DTYPE_Invalid = 1,
		DTYPE_Preview = 2,
		DTYPE_Native = 3,
		DTYPE_RealTime = 4,
		DTYPE_Procedural = 5,
		DTYPE_Xenon = 6,
		DTYPE_Streaming = 7,
		DTYPE_MAX = 8
	};

	// Enum Engine.ESoundWaveLoadingBehavior
	public enum ESoundWaveLoadingBehavior : byte {
		Inherited = 0,
		RetainOnLoad = 1,
		PrimeOnLoad = 2,
		LoadOnDemand = 3,
		ForceInline = 4,
		Uninitialized = 255,
		//ESoundWaveLoadingBehavior_MAX = 256
	};

	// Enum Engine.ESplineCoordinateSpace
	public enum ESplineCoordinateSpace : byte {
		Local = 0,
		World = 1,
		ESplineCoordinateSpace_MAX = 2
	};

	// Enum Engine.ESplinePointType
	public enum ESplinePointType : byte {
		Linear = 0,
		Curve = 1,
		Constant = 2,
		CurveClamped = 3,
		CurveCustomTangent = 4,
		ESplinePointType_MAX = 5
	};

	// Enum Engine.ESplineMeshAxis
	public enum ESplineMeshAxis : byte {
		X = 0,
		Y = 1,
		Z = 2,
		ESplineMeshAxis_MAX = 3
	};

	// Enum Engine.EOptimizationType
	public enum EOptimizationType : byte {
		OT_NumOfTriangles = 0,
		OT_MaxDeviation = 1,
		OT_MAX = 2
	};

	// Enum Engine.EImportanceLevel
	public enum EImportanceLevel : byte {
		IL_Off = 0,
		IL_Lowest = 1,
		IL_Low = 2,
		IL_Normal = 3,
		IL_High = 4,
		IL_Highest = 5,
		TEMP_BROKEN2 = 6,
		EImportanceLevel_MAX = 7
	};

	// Enum Engine.ENormalMode
	public enum ENormalMode : byte {
		NM_PreserveSmoothingGroups = 0,
		NM_RecalculateNormals = 1,
		NM_RecalculateNormalsSmooth = 2,
		NM_RecalculateNormalsHard = 3,
		TEMP_BROKEN = 4,
		ENormalMode_MAX = 5
	};

	// Enum Engine.EStereoLayerShape
	public enum EStereoLayerShape : byte {
		SLSH_QuadLayer = 0,
		SLSH_CylinderLayer = 1,
		SLSH_CubemapLayer = 2,
		SLSH_EquirectLayer = 3,
		SLSH_MAX = 4
	};

	// Enum Engine.EStereoLayerType
	public enum EStereoLayerType : byte {
		SLT_WorldLocked = 0,
		SLT_TrackerLocked = 1,
		SLT_FaceLocked = 2,
		SLT_MAX = 3
	};

	// Enum Engine.EOpacitySourceMode
	public enum EOpacitySourceMode : byte {
		OSM_Alpha = 0,
		OSM_ColorBrightness = 1,
		OSM_RedChannel = 2,
		OSM_GreenChannel = 3,
		OSM_BlueChannel = 4,
		OSM_MAX = 5
	};

	// Enum Engine.ESubUVBoundingVertexCount
	public enum ESubUVBoundingVertexCount : byte {
		BVC_FourVertices = 0,
		BVC_EightVertices = 1,
		BVC_MAX = 2
	};

	// Enum Engine.EVerticalTextAligment
	public enum EVerticalTextAligment : byte {
		EVRTA_TextTop = 0,
		EVRTA_TextCenter = 1,
		EVRTA_TextBottom = 2,
		EVRTA_QuadTop = 3,
		EVRTA_MAX = 4
	};

	// Enum Engine.EHorizTextAligment
	public enum EHorizTextAligment : byte {
		EHTA_Left = 0,
		EHTA_Center = 1,
		EHTA_Right = 2,
		EHTA_MAX = 3
	};

	// Enum Engine.ETextureLossyCompressionAmount
	public enum ETextureLossyCompressionAmount : byte {
		TLCA_Default = 0,
		TLCA_None = 1,
		TLCA_Lowest = 2,
		TLCA_Low = 3,
		TLCA_Medium = 4,
		TLCA_High = 5,
		TLCA_Highest = 6,
		TLCA_MAX = 7
	};

	// Enum Engine.ETextureCompressionQuality
	public enum ETextureCompressionQuality : byte {
		TCQ_Default = 0,
		TCQ_Lowest = 1,
		TCQ_Low = 2,
		TCQ_Medium = 3,
		TCQ_High = 4,
		TCQ_Highest = 5,
		TCQ_MAX = 6
	};

	// Enum Engine.ETextureSourceFormat
	public enum ETextureSourceFormat : byte {
		TSF_Invalid = 0,
		TSF_G8 = 1,
		TSF_BGRA8 = 2,
		TSF_BGRE8 = 3,
		TSF_RGBA16 = 4,
		TSF_RGBA16F = 5,
		TSF_RGBA8 = 6,
		TSF_RGBE8 = 7,
		TSF_G16 = 8,
		TSF_MAX = 9
	};

	// Enum Engine.ETextureSourceArtType
	public enum ETextureSourceArtType : byte {
		TSAT_Uncompressed = 0,
		TSAT_PNGCompressed = 1,
		TSAT_DDSFile = 2,
		TSAT_MAX = 3
	};

	// Enum Engine.ETextureMipCount
	public enum ETextureMipCount : byte {
		TMC_ResidentMips = 0,
		TMC_AllMips = 1,
		TMC_AllMipsBiased = 2,
		TMC_MAX = 3
	};

	// Enum Engine.ECompositeTextureMode
	public enum ECompositeTextureMode : byte {
		CTM_Disabled = 0,
		CTM_NormalRoughnessToRed = 1,
		CTM_NormalRoughnessToGreen = 2,
		CTM_NormalRoughnessToBlue = 3,
		CTM_NormalRoughnessToAlpha = 4,
		CTM_MAX = 5
	};

	// Enum Engine.TextureAddress
	public enum TextureAddress : byte {
		TA_Wrap = 0,
		TA_Clamp = 1,
		TA_Mirror = 2,
		TA_MAX = 3
	};

	// Enum Engine.TextureFilter
	public enum TextureFilter : byte {
		TF_Nearest = 0,
		TF_Bilinear = 1,
		TF_Trilinear = 2,
		TF_Default = 3,
		TF_MAX = 4
	};

	// Enum Engine.TextureCompressionSettings
	public enum TextureCompressionSettings : byte {
		TC_Default = 0,
		TC_Normalmap = 1,
		TC_Masks = 2,
		TC_Grayscale = 3,
		TC_Displacementmap = 4,
		TC_VectorDisplacementmap = 5,
		TC_HDR = 6,
		TC_EditorIcon = 7,
		TC_Alpha = 8,
		TC_DistanceFieldFont = 9,
		TC_HDR_Compressed = 10,
		TC_BC7 = 11,
		TC_MAX = 12
	};

	// Enum Engine.ETextureMipLoadOptions
	public enum ETextureMipLoadOptions : byte {
		Default = 0,
		AllMips = 1,
		OnlyFirstMip = 2,
		ETextureMipLoadOptions_MAX = 3
	};

	// Enum Engine.ETextureSamplerFilter
	public enum ETextureSamplerFilter : byte {
		Point = 0,
		Bilinear = 1,
		Trilinear = 2,
		AnisotropicPoint = 3,
		AnisotropicLinear = 4,
		ETextureSamplerFilter_MAX = 5
	};

	// Enum Engine.ETexturePowerOfTwoSetting
	public enum ETexturePowerOfTwoSetting : byte {
		None = 0,
		PadToPowerOfTwo = 1,
		PadToSquarePowerOfTwo = 2,
		ETexturePowerOfTwoSetting_MAX = 3
	};

	// Enum Engine.TextureMipGenSettings
	public enum TextureMipGenSettings : byte {
		TMGS_FromTextureGroup = 0,
		TMGS_SimpleAverage = 1,
		TMGS_Sharpen0 = 2,
		TMGS_Sharpen1 = 3,
		TMGS_Sharpen2 = 4,
		TMGS_Sharpen3 = 5,
		TMGS_Sharpen4 = 6,
		TMGS_Sharpen5 = 7,
		TMGS_Sharpen6 = 8,
		TMGS_Sharpen7 = 9,
		TMGS_Sharpen8 = 10,
		TMGS_Sharpen9 = 11,
		TMGS_Sharpen10 = 12,
		TMGS_NoMipmaps = 13,
		TMGS_LeaveExistingMips = 14,
		TMGS_Blur1 = 15,
		TMGS_Blur2 = 16,
		TMGS_Blur3 = 17,
		TMGS_Blur4 = 18,
		TMGS_Blur5 = 19,
		TMGS_Unfiltered = 20,
		TMGS_MAX = 21
	};

	// Enum Engine.TextureGroup
	public enum TextureGroup : byte {
		TEXTUREGROUP_World = 0,
		TEXTUREGROUP_WorldNormalMap = 1,
		TEXTUREGROUP_WorldSpecular = 2,
		TEXTUREGROUP_Character = 3,
		TEXTUREGROUP_CharacterNormalMap = 4,
		TEXTUREGROUP_CharacterSpecular = 5,
		TEXTUREGROUP_Weapon = 6,
		TEXTUREGROUP_WeaponNormalMap = 7,
		TEXTUREGROUP_WeaponSpecular = 8,
		TEXTUREGROUP_Vehicle = 9,
		TEXTUREGROUP_VehicleNormalMap = 10,
		TEXTUREGROUP_VehicleSpecular = 11,
		TEXTUREGROUP_Cinematic = 12,
		TEXTUREGROUP_Effects = 13,
		TEXTUREGROUP_EffectsNotFiltered = 14,
		TEXTUREGROUP_Skybox = 15,
		TEXTUREGROUP_UI = 16,
		TEXTUREGROUP_Lightmap = 17,
		TEXTUREGROUP_RenderTarget = 18,
		TEXTUREGROUP_MobileFlattened = 19,
		TEXTUREGROUP_ProcBuilding_Face = 20,
		TEXTUREGROUP_ProcBuilding_LightMap = 21,
		TEXTUREGROUP_Shadowmap = 22,
		TEXTUREGROUP_ColorLookupTable = 23,
		TEXTUREGROUP_Terrain_Heightmap = 24,
		TEXTUREGROUP_Terrain_Weightmap = 25,
		TEXTUREGROUP_Bokeh = 26,
		TEXTUREGROUP_IESLightProfile = 27,
		TEXTUREGROUP_Pixels2D = 28,
		TEXTUREGROUP_HierarchicalLOD = 29,
		TEXTUREGROUP_Impostor = 30,
		TEXTUREGROUP_ImpostorNormalDepth = 31,
		TEXTUREGROUP_8BitData = 32,
		TEXTUREGROUP_16BitData = 33,
		TEXTUREGROUP_Project01 = 34,
		TEXTUREGROUP_Project02 = 35,
		TEXTUREGROUP_Project03 = 36,
		TEXTUREGROUP_Project04 = 37,
		TEXTUREGROUP_Project05 = 38,
		TEXTUREGROUP_Project06 = 39,
		TEXTUREGROUP_Project07 = 40,
		TEXTUREGROUP_Project08 = 41,
		TEXTUREGROUP_Project09 = 42,
		TEXTUREGROUP_Project10 = 43,
		TEXTUREGROUP_Project11 = 44,
		TEXTUREGROUP_Project12 = 45,
		TEXTUREGROUP_Project13 = 46,
		TEXTUREGROUP_Project14 = 47,
		TEXTUREGROUP_Project15 = 48,
		TEXTUREGROUP_MAX = 49
	};

	// Enum Engine.ETextureRenderTargetFormat
	public enum ETextureRenderTargetFormat : byte {
		RTF_R8 = 0,
		RTF_RG8 = 1,
		RTF_RGBA8 = 2,
		RTF_RGBA8_SRGB = 3,
		RTF_R16f = 4,
		RTF_RG16f = 5,
		RTF_RGBA16f = 6,
		RTF_R32f = 7,
		RTF_RG32f = 8,
		RTF_RGBA32f = 9,
		RTF_RGB10A2 = 10,
		RTF_MAX = 11
	};

	// Enum Engine.ETimecodeProviderSynchronizationState
	public enum ETimecodeProviderSynchronizationState : byte {
		Closed = 0,
		Error = 1,
		Synchronized = 2,
		Synchronizing = 3,
		ETimecodeProviderSynchronizationState_MAX = 4
	};

	// Enum Engine.ETimelineDirection
	public enum ETimelineDirection : byte {
		Forward = 0,
		Backward = 1,
		ETimelineDirection_MAX = 2
	};

	// Enum Engine.ETimelineLengthMode
	public enum ETimelineLengthMode : byte {
		TL_TimelineLength = 0,
		TL_LastKeyFrame = 1,
		TL_MAX = 2
	};

	// Enum Engine.ETimeStretchCurveMapping
	public enum ETimeStretchCurveMapping : byte {
		T_Original = 0,
		T_TargetMin = 1,
		T_TargetMax = 2,
		MAX = 3
	};

	// Enum Engine.ETwitterIntegrationDelegate
	public enum ETwitterIntegrationDelegate : byte {
		TID_AuthorizeComplete = 0,
		TID_TweetUIComplete = 1,
		TID_RequestComplete = 2,
		TID_MAX = 3
	};

	// Enum Engine.ETwitterRequestMethod
	public enum ETwitterRequestMethod : byte {
		TRM_Get = 0,
		TRM_Post = 1,
		TRM_Delete = 2,
		TRM_MAX = 3
	};

	// Enum Engine.EUserDefinedStructureStatus
	public enum EUserDefinedStructureStatus : byte {
		UDSS_UpToDate = 0,
		UDSS_Dirty = 1,
		UDSS_Error = 2,
		UDSS_Duplicate = 3,
		UDSS_MAX = 4
	};

	// Enum Engine.EUIScalingRule
	public enum EUIScalingRule : byte {
		ShortestSide = 0,
		LongestSide = 1,
		Horizontal = 2,
		Vertical = 3,
		Custom = 4,
		EUIScalingRule_MAX = 5
	};

	// Enum Engine.ERenderFocusRule
	public enum ERenderFocusRule : byte {
		Always = 0,
		NonPointer = 1,
		NavigationOnly = 2,
		Never = 3,
		ERenderFocusRule_MAX = 4
	};

	// Enum Engine.EVectorFieldConstructionOp
	public enum EVectorFieldConstructionOp : byte {
		VFCO_Extrude = 0,
		VFCO_Revolve = 1,
		VFCO_MAX = 2
	};

	// Enum Engine.EWindSourceType
	public enum EWindSourceType : byte {
		Directional = 0,
		Point = 1,
		EWindSourceType_MAX = 2
	};

	// Enum Engine.EPSCPoolMethod
	public enum EPSCPoolMethod : byte {
		None = 0,
		AutoRelease = 1,
		ManualRelease = 2,
		ManualRelease_OnComplete = 3,
		FreeInPool = 4,
		EPSCPoolMethod_MAX = 5
	};

	// Enum Engine.EVolumeLightingMethod
	public enum EVolumeLightingMethod : byte {
		VLM_VolumetricLightmap = 0,
		VLM_SparseVolumeLightingSamples = 1,
		VLM_MAX = 2
	};

	// Enum Engine.EVisibilityAggressiveness
	public enum EVisibilityAggressiveness : byte {
		VIS_LeastAggressive = 0,
		VIS_ModeratelyAggressive = 1,
		VIS_MostAggressive = 2,
		VIS_Max = 3
	};



}

