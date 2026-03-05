using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class LinearColorPropertyData : BasePropertyData<FLinearColor> // R, G, B, A
{
    public LinearColorPropertyData(FName name) : base(name) { }

    public LinearColorPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("LinearColor");
    public override FString PropertyType => CurrentPropertyType;
}