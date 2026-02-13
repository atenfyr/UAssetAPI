using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class IntVector2PropertyData : BasePropertyData<FIntVector2>
{
    public IntVector2PropertyData(FName name) : base(name) { }

    public IntVector2PropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("IntVector2");
    public override FString PropertyType => CurrentPropertyType;
}
