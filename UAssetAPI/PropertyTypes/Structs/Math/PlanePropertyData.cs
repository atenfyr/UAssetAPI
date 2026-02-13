using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A plane in 3-D space stores the coeffecients as Xx+Yy+Zz=W.
/// </summary>
public class PlanePropertyData : BasePropertyData<FPlane>
{
    public PlanePropertyData(FName name) : base(name) { }

    public PlanePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Plane");
    public override FString PropertyType => CurrentPropertyType;
}