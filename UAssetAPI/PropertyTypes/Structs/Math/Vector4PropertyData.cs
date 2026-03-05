using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A vector in 4-D space composed of components (X, Y, Z, W) with floating/double point precision.
/// </summary>
public class Vector4PropertyData : BasePropertyData<FVector4>
{
    public Vector4PropertyData(FName name) : base(name) { }

    public Vector4PropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector4");
    public override FString PropertyType => CurrentPropertyType;
}