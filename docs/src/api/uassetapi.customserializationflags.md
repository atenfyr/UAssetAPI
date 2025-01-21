# CustomSerializationFlags

Namespace: UAssetAPI

```csharp
public enum CustomSerializationFlags
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [CustomSerializationFlags](./uassetapi.customserializationflags.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| None | 0 | No flags. |
| NoDummies | 1 | Serialize all dummy FNames to the name map. |
| SkipParsingBytecode | 2 | Skip Kismet bytecode serialization. |
| SkipPreloadDependencyLoading | 4 | Skip loading other assets referenced in preload dependencies. You may wish to set this flag when possible in multi-threading applications, since preload dependency loading could lead to file handle race conditions. |
| SkipParsingExports | 8 | Skip parsing exports at read time. Entries in the export map will be read as raw exports. You can manually parse exports with the [UAsset.ParseExport(AssetBinaryReader, Int32, Boolean)](./uassetapi.uasset.md#parseexportassetbinaryreader-int32-boolean) method. |
