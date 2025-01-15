using UAssetAPI.CustomVersions;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// Information about a single segment of an evaluation track
/// </summary>
public class FMovieSceneSegment
{
    /// <summary> The segment's range </summary>
    public TRange<float> RangeOld;
    public TRange<FFrameNumber> Range;
    public int ID;
    /// <summary> Whether this segment has been generated yet or not </summary>
    public bool bAllowEmpty;
    /// <summary> Array of implementations that reside at the segment's range </summary>
    public StructPropertyData[] Impls;

    public FMovieSceneSegment()
    {

    }

    public FMovieSceneSegment(AssetBinaryReader reader)
    {
        if (reader != null)
        {
            if (reader.Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.FloatToIntConversion)
            {
                RangeOld = new TRange<float>(reader, reader.ReadSingle);
            }
            else
            {
                Range = new(reader, () => new FFrameNumber(reader));
            }

            if (reader.Asset.GetCustomVersion<FSequencerObjectVersion>() > FSequencerObjectVersion.EvaluationTree)
            {
                ID = reader.ReadInt32();
                bAllowEmpty = reader.ReadBooleanInt();
            }

            int length = reader.ReadInt32();
            Impls = new StructPropertyData[length];
            for (int i = 0; i < length; i++)
            {
                var data = new StructPropertyData(FName.DefineDummy(reader.Asset, "Impls"), FName.DefineDummy(reader.Asset, "SectionEvaluationData"));
                data.Read(reader, false, 1, 0, PropertySerializationContext.StructFallback);
                Impls[i] = data;
            }
        }
    }

    public int Write(AssetBinaryWriter writer)
    {
        var offset = writer.BaseStream.Position;
        if (writer.Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.FloatToIntConversion)
        {
            RangeOld.Write(writer, writer.Write);
        }
        else
        {
            Range.Write(writer, frame => frame.Write(writer));
        }

        if (writer.Asset.GetCustomVersion<FSequencerObjectVersion>() > FSequencerObjectVersion.EvaluationTree)
        {
            writer.Write(ID);
            writer.Write(bAllowEmpty ? 1 : 0);
        }

        writer.Write(Impls.Length);
        for (int i = 0; i < Impls.Length; i++)
        {
            Impls[i]?.Write(writer, false, PropertySerializationContext.StructFallback);
        }

        return (int)(writer.BaseStream.Position - offset);
    }
}