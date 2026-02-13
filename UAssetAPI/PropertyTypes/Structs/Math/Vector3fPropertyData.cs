using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A vector in 3-D space composed of components (X, Y, Z) with floating point precision.
/// </summary>
public class Vector3fPropertyData : BasePropertyData<FVector3f>
{
    public Vector3fPropertyData(FName name) : base(name) { }

    public Vector3fPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector3f");
    public override FString PropertyType => CurrentPropertyType;
}