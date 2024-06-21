# EPropertyFlags

Namespace: UAssetAPI.UnrealTypes

Flags associated with each property in a class, overriding the property's default behavior.

```csharp
public enum EPropertyFlags
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [EPropertyFlags](./uassetapi.unrealtypes.epropertyflags.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| CPF_Edit | 1 | Property is user-settable in the editor. |
| CPF_ConstParm | 2 | This is a constant function parameter |
| CPF_BlueprintVisible | 4 | This property can be read by blueprint code |
| CPF_ExportObject | 8 | Object can be exported with actor. |
| CPF_BlueprintReadOnly | 16 | This property cannot be modified by blueprint code |
| CPF_Net | 32 | Property is relevant to network replication. |
| CPF_EditFixedSize | 64 | Indicates that elements of an array can be modified, but its size cannot be changed. |
| CPF_Parm | 128 | Function/When call parameter. |
| CPF_OutParm | 256 | Value is copied out after function call. |
| CPF_ZeroConstructor | 512 | memset is fine for construction |
| CPF_ReturnParm | 1024 | Return value. |
| CPF_DisableEditOnTemplate | 2048 | Disable editing of this property on an archetype/sub-blueprint |
| CPF_Transient | 8192 | Property is transient: shouldn't be saved or loaded, except for Blueprint CDOs. |
| CPF_Config | 16384 | Property should be loaded/saved as permanent profile. |
| CPF_DisableEditOnInstance | 65536 | Disable editing on an instance of this class |
| CPF_EditConst | 131072 | Property is uneditable in the editor. |
| CPF_GlobalConfig | 262144 | Load config from base class, not subclass. |
| CPF_InstancedReference | 524288 | Property is a component references. |
| CPF_DuplicateTransient | 2097152 | Property should always be reset to the default value during any type of duplication (copy/paste, binary duplication, etc.) |
| CPF_SaveGame | 16777216 | Property should be serialized for save games, this is only checked for game-specific archives with ArIsSaveGame |
| CPF_NoClear | 33554432 | Hide clear (and browse) button. |
| CPF_ReferenceParm | 134217728 | Value is passed by reference; CPF_OutParam and CPF_Param should also be set. |
| CPF_BlueprintAssignable | 268435456 | MC Delegates only. Property should be exposed for assigning in blueprint code |
| CPF_Deprecated | 536870912 | Property is deprecated. Read it from an archive, but don't save it. |
| CPF_IsPlainOldData | 1073741824 | If this is set, then the property can be memcopied instead of CopyCompleteValue / CopySingleValue |
| CPF_RepSkip | 2147483648 | Not replicated. For non replicated properties in replicated structs |
| CPF_RepNotify | 4294967296 | Notify actors when a property is replicated |
| CPF_Interp | 8589934592 | interpolatable property for use with matinee |
| CPF_NonTransactional | 17179869184 | Property isn't transacted |
| CPF_EditorOnly | 34359738368 | Property should only be loaded in the editor |
| CPF_NoDestructor | 68719476736 | No destructor |
| CPF_AutoWeak | 274877906944 | Only used for weak pointers, means the export type is autoweak |
| CPF_ContainsInstancedReference | 549755813888 | Property contains component references. |
| CPF_AssetRegistrySearchable | 1099511627776 | asset instances will add properties with this flag to the asset registry automatically |
| CPF_SimpleDisplay | 2199023255552 | The property is visible by default in the editor details view |
| CPF_AdvancedDisplay | 4398046511104 | The property is advanced and not visible by default in the editor details view |
| CPF_Protected | 8796093022208 | property is protected from the perspective of script |
| CPF_BlueprintCallable | 17592186044416 | MC Delegates only. Property should be exposed for calling in blueprint code |
| CPF_BlueprintAuthorityOnly | 35184372088832 | MC Delegates only. This delegate accepts (only in blueprint) only events with BlueprintAuthorityOnly. |
| CPF_TextExportTransient | 70368744177664 | Property shouldn't be exported to text format (e.g. copy/paste) |
| CPF_NonPIEDuplicateTransient | 140737488355328 | Property should only be copied in PIE |
| CPF_ExposeOnSpawn | 281474976710656 | Property is exposed on spawn |
| CPF_PersistentInstance | 562949953421312 | A object referenced by the property is duplicated like a component. (Each actor should have an own instance.) |
| CPF_UObjectWrapper | 1125899906842624 | Property was parsed as a wrapper class like TSubclassOf T, FScriptInterface etc., rather than a USomething* |
| CPF_HasGetValueTypeHash | 2251799813685248 | This property can generate a meaningful hash value. |
| CPF_NativeAccessSpecifierPublic | 4503599627370496 | Public native access specifier |
| CPF_NativeAccessSpecifierProtected | 9007199254740992 | Protected native access specifier |
| CPF_NativeAccessSpecifierPrivate | 18014398509481984 | Private native access specifier |
| CPF_SkipSerialization | 36028797018963968 | Property shouldn't be serialized, can still be exported to text |
