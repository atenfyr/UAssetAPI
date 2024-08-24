using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.UnrealTypes.EngineEnums;

namespace UAssetAPI.PropertyTypes.Structs;

public class RichCurveKeyPropertyData : PropertyData<FRichCurveKey>
{
    public RichCurveKeyPropertyData(FName name) : base(name) { }

    public RichCurveKeyPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("RichCurveKey");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FRichCurveKey(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        return Value.Write(writer);
    }

    public override void FromString(string[] d, UAsset asset)
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
        Value = new FRichCurveKey(InterpMode, TangentMode, TangentWeightMode, res1, res2, res3, res4, res5, res6);
    }

    public override string ToString()
    {
        string oup = "(";
        oup += Value.InterpMode + ", ";
        oup += Value.TangentMode + ", ";
        oup += Value.TangentWeightMode + ", ";
        oup += Value.Time + ", ";
        oup += Value.Value + ", ";
        oup += Value.ArriveTangent + ", ";
        oup += Value.ArriveTangentWeight + ", ";
        oup += Value.LeaveTangent + ", ";
        oup += Value.LeaveTangentWeight + ")";
        return oup;
    }
}
