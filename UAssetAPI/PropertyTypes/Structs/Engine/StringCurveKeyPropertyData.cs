using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public struct FStringCurveKey : IStruct<FStringCurveKey>
{
    public float Time;
    public FString Value;

    public FStringCurveKey(float time, FString value)
    {
        Time = time;
        Value = value;
    }

    public FStringCurveKey(AssetBinaryReader reader)
    {
        Time = reader.ReadSingle();
        Value = reader.ReadFString();
    }

    public static FStringCurveKey Read(AssetBinaryReader reader) => new FStringCurveKey(reader);

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;

        writer.Write(Time);
        writer.Write(Value);

        return (int)(writer.BaseStream.Position - offset);
    }

    public override string ToString() => $"({Time}, {Value})";

    public static FStringCurveKey FromString(string[] d, UAsset asset)
    {
        float.TryParse(d[0], out float Time);
        FString Value = FString.FromString(d[1]);
        return new FStringCurveKey(Time, Value);
    }
}

public class StringCurveKeyPropertyData : BasePropertyData<FStringCurveKey>
{
    public StringCurveKeyPropertyData(FName name) : base(name) { }
    public StringCurveKeyPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("StringCurveKey");
    public override FString PropertyType => CurrentPropertyType;
}