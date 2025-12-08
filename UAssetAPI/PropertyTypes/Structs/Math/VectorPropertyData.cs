using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A vector in 3-D space composed of components (X, Y, Z) with floating/double point precision.
/// </summary>
public class VectorPropertyData : BasePropertyData<FVector>
{
    public VectorPropertyData(FName name) : base(name) { }

    public VectorPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector");
    public override FString PropertyType => CurrentPropertyType;
}