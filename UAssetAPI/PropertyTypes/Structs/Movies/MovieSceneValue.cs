using System;
using UAssetAPI.UnrealTypes.EngineEnums;
using UAssetAPI.CustomVersions;

namespace UAssetAPI.PropertyTypes.Structs;

public class FMovieSceneTangentData
{
    public float ArriveTangent;
    public float LeaveTangent;
    public float ArriveTangentWeight;
    public float LeaveTangentWeight;
    public ERichCurveTangentWeightMode TangentWeightMode;
    public byte[] padding;

    public FMovieSceneTangentData()
    {

    }

    public FMovieSceneTangentData(AssetBinaryReader reader)
    {
        if (reader != null)
        {
            ArriveTangent = reader.ReadSingle();
            LeaveTangent = reader.ReadSingle();
            if (reader.Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.SerializeFloatChannelCompletely)
            {
                TangentWeightMode = (ERichCurveTangentWeightMode)reader.ReadByte();
                ArriveTangentWeight = reader.ReadSingle();
                LeaveTangentWeight = reader.ReadSingle();
                padding = [];
            }
            else
            {
                ArriveTangentWeight = reader.ReadSingle();
                LeaveTangentWeight = reader.ReadSingle();
                TangentWeightMode = (ERichCurveTangentWeightMode)reader.ReadByte();
                padding = reader.ReadBytes(3);
            }
        }
    }

    public void Write(AssetBinaryWriter writer)
    {
        writer.Write(ArriveTangent);
        writer.Write(LeaveTangent);
        if (writer.Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.SerializeFloatChannelCompletely)
        {
            writer.Write((byte)TangentWeightMode);
            writer.Write(ArriveTangentWeight);
            writer.Write(LeaveTangentWeight);
        }
        else
        {
            writer.Write(ArriveTangentWeight);
            writer.Write(LeaveTangentWeight);
            writer.Write((byte)TangentWeightMode);
        }
        writer.Write(padding);
    }
}

public class FMovieSceneValue<T>
{
    public T Value;
    public FMovieSceneTangentData Tangent;
    public ERichCurveInterpMode InterpMode;
    public ERichCurveTangentMode TangentMode;
    public byte[] padding;

    public FMovieSceneValue(AssetBinaryReader reader, T value)
    {
        Value = value;
        if (reader != null)
        {
            if (reader.Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.SerializeFloatChannelCompletely)
            {
                InterpMode = (ERichCurveInterpMode)reader.ReadByte();
                TangentMode = (ERichCurveTangentMode)reader.ReadByte();
                Tangent = new FMovieSceneTangentData(reader);
                padding = [];
            }
            else
            {
                Tangent = new FMovieSceneTangentData(reader);
                InterpMode = (ERichCurveInterpMode)reader.ReadByte();
                TangentMode = (ERichCurveTangentMode)reader.ReadByte();
                padding = reader.ReadBytes(2);
            }
        }
    }

    public void Write(AssetBinaryWriter writer, Action<T> valueWriter)
    {
        valueWriter(Value);
        if (writer.Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.SerializeFloatChannelCompletely)
        {
            writer.Write((byte)InterpMode);
            writer.Write((byte)TangentMode);
            Tangent.Write(writer);
        }
        else
        {
            Tangent.Write(writer);
            writer.Write((byte)InterpMode);
            writer.Write((byte)TangentMode);
        }
        writer.Write(padding);
    }
}

public class FMovieSceneFloatValue(AssetBinaryReader reader) : FMovieSceneValue<float>(reader, reader?.ReadSingle() ?? 0) { }

public class FMovieSceneDoubleValue(AssetBinaryReader reader) : FMovieSceneValue<double>(reader, reader?.ReadDouble() ?? 0) { }