using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public struct FNameCurveKey : IStruct<FNameCurveKey>
{
    public float Time;
    public FName Value;

    public FNameCurveKey(float time, FName value)
    {
        Time = time;
        Value = value;
    }

    public FNameCurveKey(AssetBinaryReader reader)
    {
        Time = reader.ReadSingle();
        Value = reader.ReadFName();
    }

    public static FNameCurveKey Read(AssetBinaryReader reader) => new FNameCurveKey(reader);

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;

        writer.Write(Time);
        writer.Write(Value);

        return (int)(writer.BaseStream.Position - offset);
    }

    public override string ToString() => $"({Time}, {Value})";

    public static FNameCurveKey FromString(string[] d, UAsset asset)
    {
        float.TryParse(d[0], out float time);
        FName value = FName.FromString(asset, d[1]);
        return new FNameCurveKey(time, value);
    }
}

public class NameCurveKeyPropertyData : BasePropertyData<FNameCurveKey>
{
    public NameCurveKeyPropertyData(FName name) : base(name) { }
    public NameCurveKeyPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("NameCurveKey");
    public override FString PropertyType => CurrentPropertyType;
}