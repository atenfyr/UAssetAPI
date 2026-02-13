using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes.EngineEnums;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// One key in a rich, editable float curve
/// </summary>
public struct FRichCurveKey : IStruct<FRichCurveKey>
{
    public ERichCurveInterpMode InterpMode;
    public ERichCurveTangentMode TangentMode;
    public ERichCurveTangentWeightMode TangentWeightMode;
    public float Time;
    public float Value;
    public float ArriveTangent;
    public float ArriveTangentWeight;
    public float LeaveTangent;
    public float LeaveTangentWeight;

    public FRichCurveKey() { }

    public FRichCurveKey(ERichCurveInterpMode interpMode, ERichCurveTangentMode tangentMode, ERichCurveTangentWeightMode tangentWeightMode, float time, float value, float arriveTangent, float arriveTangentWeight, float leaveTangent, float leaveTangentWeight)
    {
        InterpMode = interpMode;
        TangentMode = tangentMode;
        TangentWeightMode = tangentWeightMode;
        Time = time;
        Value = value;
        ArriveTangent = arriveTangent;
        ArriveTangentWeight = arriveTangentWeight;
        LeaveTangent = leaveTangent;
        LeaveTangentWeight = leaveTangentWeight;
    }

    public FRichCurveKey(AssetBinaryReader reader)
    {
        InterpMode = (ERichCurveInterpMode)reader.ReadByte();
        TangentMode = (ERichCurveTangentMode)reader.ReadByte();
        TangentWeightMode = (ERichCurveTangentWeightMode)reader.ReadByte();
        Time = reader.ReadSingle();
        Value = reader.ReadSingle();
        ArriveTangent = reader.ReadSingle();
        ArriveTangentWeight = reader.ReadSingle();
        LeaveTangent = reader.ReadSingle();
        LeaveTangentWeight = reader.ReadSingle();
    }

    public int Write(AssetBinaryWriter writer)
    {
        writer.Write((byte)InterpMode);
        writer.Write((byte)TangentMode);
        writer.Write((byte)TangentWeightMode);
        writer.Write(Time);
        writer.Write(Value);
        writer.Write(ArriveTangent);
        writer.Write(ArriveTangentWeight);
        writer.Write(LeaveTangent);
        writer.Write(LeaveTangentWeight);
        return sizeof(float) * 6 + sizeof(byte) * 3;
    }

    public static FRichCurveKey Read(AssetBinaryReader reader) => new FRichCurveKey(reader); 

    public override string ToString()
    {
        return $"({InterpMode}, {TangentMode}, {TangentWeightMode}, {Time}, {Value}, {ArriveTangent}, {ArriveTangentWeight}, {LeaveTangent}, {LeaveTangentWeight})";
    }

    public static FRichCurveKey FromString(string[] d, UAsset asset)
    {
        Enum.TryParse<ERichCurveInterpMode>(d[0], out var InterpMode);
        Enum.TryParse<ERichCurveTangentMode>(d[1], out var TangentMode);
        Enum.TryParse<ERichCurveTangentWeightMode>(d[2], out var TangentWeightMode);
        float.TryParse(d[3], out float res1);
        float.TryParse(d[4], out float res2);
        float.TryParse(d[5], out float res3);
        float.TryParse(d[6], out float res4);
        float.TryParse(d[7], out float res5);
        float.TryParse(d[8], out float res6);
        return new FRichCurveKey(InterpMode, TangentMode, TangentWeightMode, res1, res2, res3, res4, res5, res6);
    }
}
