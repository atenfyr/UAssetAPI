using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public struct FStringCurveKey(AssetBinaryReader reader) : IStruct<FStringCurveKey>
{
    public float Time = reader.ReadSingle();
    public FString Value = reader.ReadFString();

    public static FStringCurveKey Read(AssetBinaryReader reader) => new FStringCurveKey(reader);

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;

        writer.Write(Time);
        writer.Write(Value);

        return (int)(writer.BaseStream.Position - offset);
    }
}

public class StringCurveKeyPropertyData : BasePropertyData<FStringCurveKey>
{
    public StringCurveKeyPropertyData(FName name) : base(name) { }
    public StringCurveKeyPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("StringCurveKey");
    public override FString PropertyType => CurrentPropertyType;
}