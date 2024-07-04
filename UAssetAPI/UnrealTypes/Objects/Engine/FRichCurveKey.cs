using UAssetAPI.UnrealTypes.EngineEnums;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// One key in a rich, editable float curve
/// </summary>
public struct FRichCurveKey
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
}
