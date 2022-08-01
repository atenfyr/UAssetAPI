using System;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.UnrealTypes
{
    /// <summary>
    /// Flags describing an object instance
    /// </summary>
    [Flags]
    public enum EObjectFlags
    {
        RF_NoFlags = 0x00000000,
        RF_Public = 0x00000001,
        RF_Standalone = 0x00000002,
        RF_MarkAsNative = 0x00000004,
        RF_Transactional = 0x00000008,
        RF_ClassDefaultObject = 0x00000010,
        RF_ArchetypeObject = 0x00000020,
        RF_Transient = 0x00000040,
        RF_MarkAsRootSet = 0x00000080,
        RF_TagGarbageTemp = 0x00000100,
        RF_NeedInitialization = 0x00000200,
        RF_NeedLoad = 0x00000400,
        RF_KeepForCooker = 0x00000800,
        RF_NeedPostLoad = 0x00001000,
        RF_NeedPostLoadSubobjects = 0x00002000,
        RF_NewerVersionExists = 0x00004000,
        RF_BeginDestroyed = 0x00008000,
        RF_FinishDestroyed = 0x00010000,
        RF_BeingRegenerated = 0x00020000,
        RF_DefaultSubObject = 0x00040000,
        RF_WasLoaded = 0x00080000,
        RF_TextExportTransient = 0x00100000,
        RF_LoadCompleted = 0x00200000,
        RF_InheritableComponentTemplate = 0x00400000,
        RF_DuplicateTransient = 0x00800000,
        RF_StrongRefOnFrame = 0x01000000,
        RF_NonPIEDuplicateTransient = 0x02000000,
        RF_Dynamic = 0x04000000,
        RF_WillBeLoaded = 0x08000000,
        RF_HasExternalPackage = 0x10000000
    }

    /// <summary>
    /// Package flags, passed into UPackage::SetPackageFlags and related functions in the Unreal Engine
    /// </summary>
    [Flags]
    public enum EPackageFlags : uint
    {
	    ///<summary>No flags</summary>
	    PKG_None = 0x00000000,
	    ///<summary>Newly created package, not saved yet. In editor only.</summary>
	    PKG_NewlyCreated = 0x00000001,
	    ///<summary>Purely optional for clients.</summary>
	    PKG_ClientOptional = 0x00000002,
	    ///<summary>Only needed on the server side.</summary>
	    PKG_ServerSideOnly = 0x00000004,
	    ///<summary>This package is from "compiled in" classes.</summary>
	    PKG_CompiledIn = 0x00000010,
	    ///<summary>This package was loaded just for the purposes of diffing</summary>
	    PKG_ForDiffing = 0x00000020,
	    ///<summary>This is editor-only package (for example: editor module script package)</summary>
	    PKG_EditorOnly = 0x00000040,
	    ///<summary>Developer module</summary>
	    PKG_Developer = 0x00000080,
	    ///<summary>Loaded only in uncooked builds (i.e. runtime in editor)</summary>
	    PKG_UncookedOnly = 0x00000100,
	    ///<summary>Package is cooked</summary>
	    PKG_Cooked = 0x00000200,
	    ///<summary>Package doesn't contain any asset object (although asset tags can be present)</summary>
	    PKG_ContainsNoAsset = 0x00000400,
	    ///<summary>Uses unversioned property serialization instead of versioned tagged property serialization</summary>
	    PKG_UnversionedProperties = 0x00002000,
	    ///<summary>Contains map data (UObjects only referenced by a single ULevel) but is stored in a different package</summary>
	    PKG_ContainsMapData = 0x00004000,
	    ///<summary>package is currently being compiled</summary>
	    PKG_Compiling = 0x00010000,
	    ///<summary>Set if the package contains a ULevel/ UWorld object</summary>
	    PKG_ContainsMap = 0x00020000,
	    ///<summary>???</summary>
	    PKG_RequiresLocalizationGather = 0x00040000,
	    ///<summary>Set if the package was created for the purpose of PIE</summary>
	    PKG_PlayInEditor = 0x00100000,
	    ///<summary>Package is allowed to contain UClass objects</summary>
	    PKG_ContainsScript = 0x00200000,
	    ///<summary>Editor should not export asset in this package</summary>
	    PKG_DisallowExport = 0x00400000,
	    ///<summary>This package should resolve dynamic imports from its export at runtime.</summary>
	    PKG_DynamicImports = 0x10000000,
	    ///<summary>This package contains elements that are runtime generated, and may not follow standard loading order rules</summary>
	    PKG_RuntimeGenerated = 0x20000000,
	    ///<summary>This package is reloading in the cooker, try to avoid getting data we will never need. We won't save this package.</summary>
	    PKG_ReloadingForCooker = 0x40000000,
	    ///<summary>Package has editor-only data filtered out</summary>
	    PKG_FilterEditorOnly = 0x80000000,
    }

    /// <summary>
    /// Flags associated with each property in a class, overriding the property's default behavior.
    /// </summary>
    [Flags]
    public enum EPropertyFlags : ulong
    {
        CPF_None = 0,

        ///<summary>Property is user-settable in the editor.</summary>
        CPF_Edit = 0x0000000000000001,
        ///<summary>This is a constant function parameter</summary>
        CPF_ConstParm = 0x0000000000000002,
        ///<summary>This property can be read by blueprint code</summary>
        CPF_BlueprintVisible = 0x0000000000000004,
        ///<summary>Object can be exported with actor.</summary>
        CPF_ExportObject = 0x0000000000000008,
        ///<summary>This property cannot be modified by blueprint code</summary>
        CPF_BlueprintReadOnly = 0x0000000000000010,
        ///<summary>Property is relevant to network replication.</summary>
        CPF_Net = 0x0000000000000020,
        ///<summary>Indicates that elements of an array can be modified, but its size cannot be changed.</summary>
        CPF_EditFixedSize = 0x0000000000000040,
        ///<summary>Function/When call parameter.</summary>
        CPF_Parm = 0x0000000000000080,
        ///<summary>Value is copied out after function call.</summary>
        CPF_OutParm = 0x0000000000000100,
        ///<summary>memset is fine for construction</summary>
        CPF_ZeroConstructor = 0x0000000000000200,
        ///<summary>Return value.</summary>
        CPF_ReturnParm = 0x0000000000000400,
        ///<summary>Disable editing of this property on an archetype/sub-blueprint</summary>
        CPF_DisableEditOnTemplate = 0x0000000000000800,
        ///<summary>Property is transient: shouldn't be saved or loaded, except for Blueprint CDOs.</summary>
        CPF_Transient = 0x0000000000002000,
        ///<summary>Property should be loaded/saved as permanent profile.</summary>
        CPF_Config = 0x0000000000004000,
        ///<summary>Disable editing on an instance of this class</summary>
        CPF_DisableEditOnInstance = 0x0000000000010000,
        ///<summary>Property is uneditable in the editor.</summary>
        CPF_EditConst = 0x0000000000020000,
        ///<summary>Load config from base class, not subclass.</summary>
        CPF_GlobalConfig = 0x0000000000040000,
        ///<summary>Property is a component references.</summary>
        CPF_InstancedReference = 0x0000000000080000,
        ///<summary>Property should always be reset to the default value during any type of duplication (copy/paste, binary duplication, etc.)</summary>
        CPF_DuplicateTransient = 0x0000000000200000,
        ///<summary>Property should be serialized for save games, this is only checked for game-specific archives with ArIsSaveGame</summary>
        CPF_SaveGame = 0x0000000001000000,
        ///<summary>Hide clear (and browse) button.</summary>
        CPF_NoClear = 0x0000000002000000,
        ///<summary>Value is passed by reference; CPF_OutParam and CPF_Param should also be set.</summary>
        CPF_ReferenceParm = 0x0000000008000000,
        ///<summary>MC Delegates only.  Property should be exposed for assigning in blueprint code</summary>
        CPF_BlueprintAssignable = 0x0000000010000000,
        ///<summary>Property is deprecated.  Read it from an archive, but don't save it.</summary>
        CPF_Deprecated = 0x0000000020000000,
        ///<summary>If this is set, then the property can be memcopied instead of CopyCompleteValue / CopySingleValue</summary>
        CPF_IsPlainOldData = 0x0000000040000000,
        ///<summary>Not replicated. For non replicated properties in replicated structs </summary>
        CPF_RepSkip = 0x0000000080000000,
        ///<summary>Notify actors when a property is replicated</summary>
        CPF_RepNotify = 0x0000000100000000,
        ///<summary>interpolatable property for use with matinee</summary>
        CPF_Interp = 0x0000000200000000,
        ///<summary>Property isn't transacted</summary>
        CPF_NonTransactional = 0x0000000400000000,
        ///<summary>Property should only be loaded in the editor</summary>
        CPF_EditorOnly = 0x0000000800000000,
        ///<summary>No destructor</summary>
        CPF_NoDestructor = 0x0000001000000000,
        ///<summary>Only used for weak pointers, means the export type is autoweak</summary>
        CPF_AutoWeak = 0x0000004000000000,
        ///<summary>Property contains component references.</summary>
        CPF_ContainsInstancedReference = 0x0000008000000000,
        ///<summary>asset instances will add properties with this flag to the asset registry automatically</summary>
        CPF_AssetRegistrySearchable = 0x0000010000000000,
        ///<summary>The property is visible by default in the editor details view</summary>
        CPF_SimpleDisplay = 0x0000020000000000,
        ///<summary>The property is advanced and not visible by default in the editor details view</summary>
        CPF_AdvancedDisplay = 0x0000040000000000,
        ///<summary>property is protected from the perspective of script</summary>
        CPF_Protected = 0x0000080000000000,
        ///<summary>MC Delegates only.  Property should be exposed for calling in blueprint code</summary>
        CPF_BlueprintCallable = 0x0000100000000000,
        ///<summary>MC Delegates only.  This delegate accepts (only in blueprint) only events with BlueprintAuthorityOnly.</summary>
        CPF_BlueprintAuthorityOnly = 0x0000200000000000,
        ///<summary>Property shouldn't be exported to text format (e.g. copy/paste)</summary>
        CPF_TextExportTransient = 0x0000400000000000,
        ///<summary>Property should only be copied in PIE</summary>
        CPF_NonPIEDuplicateTransient = 0x0000800000000000,
        ///<summary>Property is exposed on spawn</summary>
        CPF_ExposeOnSpawn = 0x0001000000000000,
        ///<summary>A object referenced by the property is duplicated like a component. (Each actor should have an own instance.)</summary>
        CPF_PersistentInstance = 0x0002000000000000,
        ///<summary>Property was parsed as a wrapper class like TSubclassOf T, FScriptInterface etc., rather than a USomething*</summary>
        CPF_UObjectWrapper = 0x0004000000000000,
        ///<summary>This property can generate a meaningful hash value.</summary>
        CPF_HasGetValueTypeHash = 0x0008000000000000,
        ///<summary>Public native access specifier</summary>
        CPF_NativeAccessSpecifierPublic = 0x0010000000000000,
        ///<summary>Protected native access specifier</summary>
        CPF_NativeAccessSpecifierProtected = 0x0020000000000000,
        ///<summary>Private native access specifier</summary>
        CPF_NativeAccessSpecifierPrivate = 0x0040000000000000,
        ///<summary>Property shouldn't be serialized, can still be exported to text</summary>
        CPF_SkipSerialization = 0x0080000000000000,
    }

    /// <summary>
    /// Flags describing a class.
    /// </summary>
    [Flags]
    public enum EClassFlags : uint
    {
        /// <summary>No Flags</summary>
        CLASS_None = 0x00000000,
        /// <summary>Class is abstract and can't be instantiated directly.</summary>
        CLASS_Abstract = 0x00000001,
        /// <summary>Save object configuration only to Default INIs, never to local INIs. Must be combined with CLASS_Config</summary>
        CLASS_DefaultConfig = 0x00000002,
        /// <summary>Load object configuration at construction time.</summary>
        CLASS_Config = 0x00000004,
        /// <summary>This object type can't be saved; null it out at save time.</summary>
        CLASS_Transient = 0x00000008,
        /// <summary>Successfully parsed.</summary>
        CLASS_Parsed = 0x00000010,
        /// <summary>???</summary>
        CLASS_MatchedSerializers = 0x00000020,
        /// <summary>Indicates that the config settings for this class will be saved to Project/User*.ini (similar to CLASS_GlobalUserConfig)</summary>
        CLASS_ProjectUserConfig = 0x00000040,
        /// <summary>Class is a native class - native interfaces will have CLASS_Native set, but not RF_MarkAsNative</summary>
        CLASS_Native = 0x00000080,
        /// <summary>Don't export to C++ header.</summary>
        CLASS_NoExport = 0x00000100,
        /// <summary>Do not allow users to create in the editor.</summary>
        CLASS_NotPlaceable = 0x00000200,
        /// <summary>Handle object configuration on a per-object basis, rather than per-class.</summary>
        CLASS_PerObjectConfig = 0x00000400,
        /// <summary>Whether SetUpRuntimeReplicationData still needs to be called for this class</summary>
        CLASS_ReplicationDataIsSetUp = 0x00000800u,
        /// <summary>Class can be constructed from editinline New button.</summary>
        CLASS_EditInlineNew = 0x00001000,
        /// <summary>Display properties in the editor without using categories.</summary>
        CLASS_CollapseCategories = 0x00002000,
        /// <summary>Class is an interface</summary>
        CLASS_Interface = 0x00004000,
        /// <summary>Do not export a constructor for this class, assuming it is in the cpptext</summary>
        CLASS_CustomConstructor = 0x00008000,
        /// <summary>All properties and functions in this class are const and should be exported as const</summary>
        CLASS_Const = 0x00010000,
        /// <summary>Class flag indicating the class is having its layout changed, and therefore is not ready for a CDO to be created</summary>
        CLASS_LayoutChanging = 0x00020000,
        /// <summary>Indicates that the class was created from blueprint source material</summary>
        CLASS_CompiledFromBlueprint = 0x00040000,
        /// <summary>Indicates that only the bare minimum bits of this class should be DLL exported/imported</summary>
        CLASS_MinimalAPI = 0x00080000,
        /// <summary>Indicates this class must be DLL exported/imported (along with all of it's members)</summary>
        CLASS_RequiredAPI = 0x00100000,
        /// <summary>Indicates that references to this class default to instanced. Used to be subclasses of UComponent, but now can be any UObject</summary>
        CLASS_DefaultToInstanced = 0x00200000,
        /// <summary>Indicates that the parent token stream has been merged with ours.</summary>
        CLASS_TokenStreamAssembled = 0x00400000,
        /// <summary>Class has component properties.</summary>
        CLASS_HasInstancedReference = 0x00800000,
        /// <summary>Don't show this class in the editor class browser or edit inline new menus.</summary>
        CLASS_Hidden = 0x01000000,
        /// <summary>Don't save objects of this class when serializing</summary>
        CLASS_Deprecated = 0x02000000,
        /// <summary>Class not shown in editor drop down for class selection</summary>
        CLASS_HideDropDown = 0x04000000,
        /// <summary>Class settings are saved to AppData/..../Blah.ini (as opposed to CLASS_DefaultConfig)</summary>
        CLASS_GlobalUserConfig = 0x08000000,
        /// <summary>Class was declared directly in C++ and has no boilerplate generated by UnrealHeaderTool</summary>
        CLASS_Intrinsic = 0x10000000,
        /// <summary>Class has already been constructed (maybe in a previous DLL version before hot-reload).</summary>
        CLASS_Constructed = 0x20000000,
        /// <summary>Indicates that object configuration will not check against ini base/defaults when serialized</summary>
        CLASS_ConfigDoNotCheckDefaults = 0x40000000,
        /// <summary>Class has been consigned to oblivion as part of a blueprint recompile, and a newer version currently exists.</summary>
        CLASS_NewerVersionExists = 0x80000000,
    };

    /// <summary>
    /// Flags describing a function.
    /// </summary>
    [Flags]
    public enum EFunctionFlags : uint {
        FUNC_None = 0x00000000,
        FUNC_Final = 0x00000001,
        FUNC_RequiredAPI = 0x00000002,
        FUNC_BlueprintAuthorityOnly = 0x00000004,
        FUNC_BlueprintCosmetic = 0x00000008,
        FUNC_Net = 0x00000040,
        FUNC_NetReliable = 0x00000080,
        FUNC_NetRequest = 0x00000100,
        FUNC_Exec = 0x00000200,
        FUNC_Native = 0x00000400,
        FUNC_Event = 0x00000800,
        FUNC_NetResponse = 0x00001000,
        FUNC_Static = 0x00002000,
        FUNC_NetMulticast = 0x00004000,
        FUNC_UbergraphFunction = 0x00008000,
        FUNC_MulticastDelegate = 0x00010000,
        FUNC_Public = 0x00020000,
        FUNC_Private = 0x00040000,
        FUNC_Protected = 0x00080000,
        FUNC_Delegate = 0x00100000,
        FUNC_NetServer = 0x00200000,
        FUNC_HasOutParms = 0x00400000,
        FUNC_HasDefaults = 0x00800000,
        FUNC_NetClient = 0x01000000,
        FUNC_DLLImport = 0x02000000,
        FUNC_BlueprintCallable = 0x04000000,
        FUNC_BlueprintEvent = 0x08000000,
        FUNC_BlueprintPure = 0x10000000,
        FUNC_EditorOnly = 0x20000000,
        FUNC_Const = 0x40000000,
        FUNC_NetValidate = 0x80000000,
        FUNC_AllFlags = 0xFFFFFFFF,
    };
}
