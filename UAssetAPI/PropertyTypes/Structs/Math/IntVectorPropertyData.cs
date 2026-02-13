using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class IntVectorPropertyData : BasePropertyData<FIntVector>
{
    public IntVectorPropertyData(FName name) : base(name) { }

    public IntVectorPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("IntVector");
    public override FString PropertyType => CurrentPropertyType;
}

