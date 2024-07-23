using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public struct FNameCurveKey(AssetBinaryReader reader) : IStruct<FNameCurveKey>
{
    public float Time = reader.ReadSingle();
    public FName Value = reader.ReadFName();

    public static FNameCurveKey Read(AssetBinaryReader reader) => new FNameCurveKey(reader);

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;

        writer.Write(Time);
        writer.Write(Value);

        return (int)(writer.BaseStream.Position - offset);
    }
}

public class NameCurveKeyPropertyData : BasePropertyData<FNameCurveKey>
{
    public NameCurveKeyPropertyData(FName name) : base(name) { }
    public NameCurveKeyPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("NameCurveKey");
    public override FString PropertyType => CurrentPropertyType;
}