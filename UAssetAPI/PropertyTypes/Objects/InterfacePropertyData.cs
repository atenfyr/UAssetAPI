using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Objects;

/// <summary>
/// Describes a reference variable to another object (import/export) which may be null (<see cref="FPackageIndex"/>).
/// </summary>
public class InterfacePropertyData : ObjectPropertyData
{
    public InterfacePropertyData(FName name) : base(name) { }

    public InterfacePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("InterfaceProperty");
    public override FString PropertyType => CurrentPropertyType;
}
