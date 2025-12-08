using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// Implements a container for rotation information.
/// All rotation values are stored in degrees.
/// </summary>
public class RotatorPropertyData : BasePropertyData<FRotator>
{        
    public RotatorPropertyData(FName name) : base(name) { }

    public RotatorPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Rotator");
    public override FString PropertyType => CurrentPropertyType;
}