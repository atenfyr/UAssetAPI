using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A vector in 2-D space composed of components (X, Y) with floating/double point precision.
/// </summary>
public class Vector2DPropertyData : BasePropertyData<FVector2D>
{
    public Vector2DPropertyData(FName name) : base(name) { }

    public Vector2DPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector2D");
    public override FString PropertyType => CurrentPropertyType;
}