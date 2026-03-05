using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class WeightedRandomSamplerPropertyData : BasePropertyData<FWeightedRandomSampler>
{
    public WeightedRandomSamplerPropertyData(FName name) : base(name) { }

    public WeightedRandomSamplerPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("WeightedRandomSampler");
    public override FString PropertyType => CurrentPropertyType;
}

public class SkeletalMeshAreaWeightedTriangleSamplerPropertyData : WeightedRandomSamplerPropertyData
{
    public SkeletalMeshAreaWeightedTriangleSamplerPropertyData(FName name) : base(name) { }

    public SkeletalMeshAreaWeightedTriangleSamplerPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("SkeletalMeshAreaWeightedTriangleSampler");
    public override FString PropertyType => CurrentPropertyType;
}
