using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// Floating point quaternion that can represent a rotation about an axis in 3-D space.
/// The X, Y, Z, W components also double as the Axis/Angle format.
/// </summary>
public class QuatPropertyData : BasePropertyData<FQuat>
{
    public QuatPropertyData(FName name) : base(name) { }

    public QuatPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Quat");
    public override FString PropertyType => CurrentPropertyType;
}