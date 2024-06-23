using UAssetAPI.CustomVersions;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.UnrealTypes.EngineEnums;

namespace UAssetAPI.PropertyTypes.Structs;

public class MovieSceneDoubleChannelPropertyData : PropertyData<FMovieSceneDoubleChannel>
{
    public int TimesStructLength;
    public int TimesLength;

    public int ValuesStructLength;
    public int ValuesLength;
    public int HasDefaultValue;


    public MovieSceneDoubleChannelPropertyData(FName name) : base(name) { }

    public MovieSceneDoubleChannelPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MovieSceneDoubleChannel");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            PropertyGuid = reader.ReadPropertyGuid();
        }

        Value = new FMovieSceneDoubleChannel();

        Value.PreInfinityExtrap = (ERichCurveExtrapolation)reader.ReadByte();
        Value.PostInfinityExtrap = (ERichCurveExtrapolation)reader.ReadByte();

        TimesStructLength = reader.ReadInt32();
        TimesLength = reader.ReadInt32();

        Value.Times = new FFrameNumber[TimesLength];
        for (int j = 0; j < TimesLength; j++)
        {
            Value.Times[j] = new FFrameNumber(reader.ReadInt32());
        }

        ValuesStructLength = reader.ReadInt32();
        ValuesLength = reader.ReadInt32();
        Value.Values = new FMovieSceneDoubleValue[ValuesLength];

        for (int j = 0; j < ValuesLength; j++)
        {
            Value.Values[j] = new FMovieSceneDoubleValue(reader);
        }

        Value.DefaultValue = reader.ReadDouble();
        HasDefaultValue = reader.ReadInt32();
        Value.bHasDefaultValue = HasDefaultValue == 0 ? false : true;

        Value.TickResolution = new FFrameRate(reader.ReadInt32(), reader.ReadInt32());

        Value.bShowCurve = reader.Asset.GetCustomVersion<FFortniteMainBranchObjectVersion>() > FFortniteMainBranchObjectVersion.SerializeFloatChannelShowCurve && reader.ReadInt32() != 0;
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            writer.WritePropertyGuid(PropertyGuid);
        }

        int here = (int)writer.BaseStream.Position;

        writer.Write((byte)Value.PreInfinityExtrap);
        writer.Write((byte)Value.PostInfinityExtrap);

        writer.Write(TimesStructLength);
        writer.Write(TimesLength);
        for (int j = 0; j < TimesLength; j++) {
            writer.Write(Value.Times[j].Value); 
        }

        writer.Write(ValuesStructLength);
        writer.Write(ValuesLength);
        for (int j = 0; j < ValuesLength; j++) {
            Value.Values[j].Write(writer);
        }

        writer.Write(Value.DefaultValue);
        writer.Write(Value.bHasDefaultValue == false ? 0 : HasDefaultValue);
        writer.Write(Value.TickResolution.Numerator);
        writer.Write(Value.TickResolution.Denominator);

        if (writer.Asset.GetCustomVersion<FFortniteMainBranchObjectVersion>() > FFortniteMainBranchObjectVersion.SerializeFloatChannelShowCurve)
        {
            writer.Write(Value.bShowCurve ? 1 : 0);
        }

        return (int)writer.BaseStream.Position - here;
    }

    public override void FromString(string[] d, UAsset asset)
    {

    }

    public override string ToString()
    {
        return "";
    }
}
