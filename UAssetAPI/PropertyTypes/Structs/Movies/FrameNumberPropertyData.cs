using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class FrameNumberPropertyData : BasePropertyData<FFrameNumber>
{
    public FrameNumberPropertyData(FName name) : base(name) { }

    public FrameNumberPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("FrameNumber");
    public override FString PropertyType => CurrentPropertyType;
}