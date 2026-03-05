using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A vector in 4-D space composed of components (X, Y, Z, W) with floating point precision.
/// </summary>
public class Vector4fPropertyData : BasePropertyData<FVector4f>
{
    public Vector4fPropertyData(FName name) : base(name) { }

    public Vector4fPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector4f");
    public override FString PropertyType => CurrentPropertyType;
}