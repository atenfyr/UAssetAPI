# EBlueprintTextLiteralType

Namespace: UAssetAPI.Kismet.Bytecode

Kinds of text literals

```csharp
public enum EBlueprintTextLiteralType
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [EBlueprintTextLiteralType](./uassetapi.kismet.bytecode.eblueprinttextliteraltype.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Empty | 0 | Text is an empty string. The bytecode contains no strings, and you should use FText::GetEmpty() to initialize the FText instance. |
| LocalizedText | 1 | Text is localized. The bytecode will contain three strings - source, key, and namespace - and should be loaded via FInternationalization |
| InvariantText | 2 | Text is culture invariant. The bytecode will contain one string, and you should use FText::AsCultureInvariant to initialize the FText instance. |
| LiteralString | 3 | Text is a literal FString. The bytecode will contain one string, and you should use FText::FromString to initialize the FText instance. |
| StringTableEntry | 4 | Text is from a string table. The bytecode will contain an object pointer (not used) and two strings - the table ID, and key - and should be found via FText::FromStringTable |
