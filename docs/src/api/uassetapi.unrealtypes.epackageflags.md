# EPackageFlags

Namespace: UAssetAPI.UnrealTypes

Package flags, passed into UPackage::SetPackageFlags and related functions in the Unreal Engine

```csharp
public enum EPackageFlags
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [EPackageFlags](./uassetapi.unrealtypes.epackageflags.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| PKG_None | 0 | No flags |
| PKG_NewlyCreated | 1 | Newly created package, not saved yet. In editor only. |
| PKG_ClientOptional | 2 | Purely optional for clients. |
| PKG_ServerSideOnly | 4 | Only needed on the server side. |
| PKG_CompiledIn | 16 | This package is from "compiled in" classes. |
| PKG_ForDiffing | 32 | This package was loaded just for the purposes of diffing |
| PKG_EditorOnly | 64 | This is editor-only package (for example: editor module script package) |
| PKG_Developer | 128 | Developer module |
| PKG_UncookedOnly | 256 | Loaded only in uncooked builds (i.e. runtime in editor) |
| PKG_Cooked | 512 | Package is cooked |
| PKG_ContainsNoAsset | 1024 | Package doesn't contain any asset object (although asset tags can be present) |
| PKG_UnversionedProperties | 8192 | Uses unversioned property serialization instead of versioned tagged property serialization |
| PKG_ContainsMapData | 16384 | Contains map data (UObjects only referenced by a single ULevel) but is stored in a different package |
| PKG_Compiling | 65536 | package is currently being compiled |
| PKG_ContainsMap | 131072 | Set if the package contains a ULevel/ UWorld object |
| PKG_RequiresLocalizationGather | 262144 | ??? |
| PKG_PlayInEditor | 1048576 | Set if the package was created for the purpose of PIE |
| PKG_ContainsScript | 2097152 | Package is allowed to contain UClass objects |
| PKG_DisallowExport | 4194304 | Editor should not export asset in this package |
| PKG_DynamicImports | 268435456 | This package should resolve dynamic imports from its export at runtime. |
| PKG_RuntimeGenerated | 536870912 | This package contains elements that are runtime generated, and may not follow standard loading order rules |
| PKG_ReloadingForCooker | 1073741824 | This package is reloading in the cooker, try to avoid getting data we will never need. We won't save this package. |
| PKG_FilterEditorOnly | 2147483648 | Package has editor-only data filtered out |
