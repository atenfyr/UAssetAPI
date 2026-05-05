# EOverriddenPropertyOperation

Namespace: UAssetAPI.PropertyTypes.Objects

```csharp
public enum EOverriddenPropertyOperation
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [EOverriddenPropertyOperation](./uassetapi.propertytypes.objects.eoverriddenpropertyoperation.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| None | 0 | no overridden operation was recorded on this property |
| Modified | 1 | some sub property has recorded overridden operation |
| Replace | 2 | everything has been overridden from this property down to every sub property/sub object |
| Add | 3 | this element was added in the container |
| Remove | 4 | this element was removed from the container |
