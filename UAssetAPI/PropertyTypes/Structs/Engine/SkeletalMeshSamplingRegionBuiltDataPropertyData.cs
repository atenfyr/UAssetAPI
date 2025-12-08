using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.StructTypes;

public class SkeletalMeshSamplingRegionBuiltDataPropertyData : BasePropertyData<FSkeletalMeshSamplingRegionBuiltData>
{
    public SkeletalMeshSamplingRegionBuiltDataPropertyData(FName name) : base(name) { }

    public SkeletalMeshSamplingRegionBuiltDataPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("SkeletalMeshSamplingRegionBuiltData");
    public override FString PropertyType => CurrentPropertyType;
}