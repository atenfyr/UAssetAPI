using System;
using UAssetAPI.CustomVersions;
using UAssetAPI.UnrealTypes.EngineEnums;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class FMovieSceneChannel<T>
{
    public ERichCurveExtrapolation PreInfinityExtrap;
    public ERichCurveExtrapolation PostInfinityExtrap;
    public int TimesStructLength;
    public FFrameNumber[] Times;
    public int ValuesStructLength;
    public FMovieSceneValue<T>[] Values;
    public T DefaultValue;
    public bool bHasDefaultValue;
    public FFrameRate TickResolution;
    public bool bShowCurve;

    public FMovieSceneChannel()
    {
        PreInfinityExtrap = ERichCurveExtrapolation.RCCE_Constant;
        PostInfinityExtrap = ERichCurveExtrapolation.RCCE_Constant;
        Times = [];
        Values = [];
        DefaultValue = default;
        bHasDefaultValue = false;
        TickResolution = new FFrameRate(60000, 1);
        bShowCurve = false;
    }

    public FMovieSceneChannel(AssetBinaryReader reader, Func<T> valueReader)
    {
        if (reader != null && valueReader != null)
        {
            PreInfinityExtrap = (ERichCurveExtrapolation)reader.ReadByte();
            PostInfinityExtrap = (ERichCurveExtrapolation)reader.ReadByte();

            TimesStructLength = reader.ReadInt32();
            var TimesLength = reader.ReadInt32();
            Times = new FFrameNumber[TimesLength];
            for (int i = 0; i < TimesLength; i++)
            {
                Times[i] = new FFrameNumber(reader);
            }

            ValuesStructLength = reader.ReadInt32();
            var ValuesLength = reader.ReadInt32();
            Values = new FMovieSceneValue<T>[ValuesLength];
            for (int i = 0; i < ValuesLength; i++)
            {
                Values[i] = new FMovieSceneValue<T>(reader, valueReader());
            }

            DefaultValue = valueReader();
            bHasDefaultValue = reader.ReadBooleanInt();
            TickResolution = new FFrameRate(reader);
            bShowCurve = reader.Asset.GetCustomVersion<FFortniteMainBranchObjectVersion>() > FFortniteMainBranchObjectVersion.SerializeFloatChannelShowCurve && reader.ReadBooleanInt();
        }
    }

    public void Write(AssetBinaryWriter writer, Action<T> valueWriter)
    {
        writer.Write((byte)PreInfinityExtrap);
        writer.Write((byte)PostInfinityExtrap);

        writer.Write(TimesStructLength);
        writer.Write(Times.Length);
        for (int i = 0; i < Times.Length; i++)
        {
            Times[i].Write(writer);
        }

        writer.Write(ValuesStructLength);
        writer.Write(Values.Length);
        for (int i = 0; i < Values.Length; i++)
        {
            Values[i].Write(writer, valueWriter);
        }

        valueWriter(DefaultValue);
        writer.Write(bHasDefaultValue ? 1 : 0);
        TickResolution.Write(writer);
        if (writer.Asset.GetCustomVersion<FFortniteMainBranchObjectVersion>() > FFortniteMainBranchObjectVersion.SerializeFloatChannelShowCurve)
            writer.Write(bShowCurve ? 1 : 0);
    }
}

public class FMovieSceneFloatChannel(AssetBinaryReader reader) : FMovieSceneChannel<float>(reader, reader == null ? null : reader.ReadSingle) { }

public class FMovieSceneDoubleChannel(AssetBinaryReader reader) : FMovieSceneChannel<double>(reader, reader == null ? null : reader.ReadDouble) { }