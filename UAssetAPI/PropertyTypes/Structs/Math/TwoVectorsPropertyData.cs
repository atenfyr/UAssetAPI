using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class TwoVectorsPropertyData : BasePropertyData<FTwoVectors>
{
    public TwoVectorsPropertyData(FName name) : base(name) { }

    public TwoVectorsPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("TwoVectors");
    public override FString PropertyType => CurrentPropertyType;
}