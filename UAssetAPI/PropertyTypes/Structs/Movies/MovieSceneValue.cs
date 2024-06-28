using System;
using UAssetAPI.UnrealTypes.EngineEnums;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class FMovieSceneTangentData
{
    public float ArriveTangent;
    public float LeaveTangent;
    public float ArriveTangentWeight;
    public float LeaveTangentWeight;
    public ERichCurveTangentWeightMode TangentWeightMode;
    public byte[] padding;

    public FMovieSceneTangentData(AssetBinaryReader reader)
    {
        ArriveTangent = reader.ReadSingle();
        LeaveTangent = reader.ReadSingle();
        ArriveTangentWeight = reader.ReadSingle();
        LeaveTangentWeight = reader.ReadSingle();
        TangentWeightMode = (ERichCurveTangentWeightMode)reader.ReadByte();

        // guessing this happens when `PaddingByte` gets added to FMovieSceneFloatValue but requires a lot more testing
        padding = reader.Asset.GetEngineVersion() >= EngineVersion.VER_UE4_25 ? reader.ReadBytes(3) : [];
    }

    public void Write(AssetBinaryWriter writer)
    {
        writer.Write(ArriveTangent);
        writer.Write(LeaveTangent);
        writer.Write(ArriveTangentWeight);
        writer.Write(LeaveTangentWeight);
        writer.Write((byte)TangentWeightMode);
        writer.Write(padding);
    }
}

public class FMovieSceneValue<T>(AssetBinaryReader reader, T value)
{
    public T Value = value;
    public FMovieSceneTangentData Tangent = new FMovieSceneTangentData(reader);
    public ERichCurveInterpMode InterpMode = (ERichCurveInterpMode)reader.ReadByte();
    public ERichCurveTangentMode TangentMode = (ERichCurveTangentMode)reader.ReadByte();
    public byte[] padding = reader.Asset.GetEngineVersion() >= EngineVersion.VER_UE4_25 ? reader.ReadBytes(2) : [];

    public void Write(AssetBinaryWriter writer, Action<T> valueWriter)
    {
        valueWriter(Value);
        Tangent.Write(writer);
        writer.Write((byte)InterpMode);
        writer.Write((byte)TangentMode);
        writer.Write(padding);
    }
}

public class FMovieSceneFloatValue(AssetBinaryReader reader) : FMovieSceneValue<float>(reader, reader.ReadSingle()) { }

public class FMovieSceneDoubleValue(AssetBinaryReader reader) : FMovieSceneValue<double>(reader, reader.ReadDouble()) { }