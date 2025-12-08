using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class MatrixPropertyData : BasePropertyData<FMatrix>
{
    public MatrixPropertyData(FName name) : base(name) { }

    public MatrixPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Matrix");
    public override FString PropertyType => CurrentPropertyType;
}