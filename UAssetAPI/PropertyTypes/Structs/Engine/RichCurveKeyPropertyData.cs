using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class RichCurveKeyPropertyData : BasePropertyData<FRichCurveKey>
{
    public RichCurveKeyPropertyData(FName name) : base(name) { }

    public RichCurveKeyPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("RichCurveKey");
    public override FString PropertyType => CurrentPropertyType;
}
