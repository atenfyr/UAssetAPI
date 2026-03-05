using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class DeprecateSlateVector2DPropertyData : BasePropertyData<FVector2f>
{
    public DeprecateSlateVector2DPropertyData(FName name) : base(name) { }

    public DeprecateSlateVector2DPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("DeprecateSlateVector2D");
    public override FString PropertyType => CurrentPropertyType;
}