using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A vector in 2-D space composed of components (X, Y) with floating/double point precision.
/// </summary>
public class Vector2fPropertyData : BasePropertyData<FVector2f>
{
    public Vector2fPropertyData(FName name) : base(name) { }

    public Vector2fPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector2f");
    public override FString PropertyType => CurrentPropertyType;
}