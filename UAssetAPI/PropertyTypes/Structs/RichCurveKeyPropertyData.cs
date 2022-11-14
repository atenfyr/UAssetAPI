using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    public class RichCurveKeyPropertyData : PropertyData
    {
        [JsonProperty]
        public ERichCurveInterpMode InterpMode;
        [JsonProperty]
        public ERichCurveTangentMode TangentMode;
        [JsonProperty]
        public ERichCurveTangentWeightMode TangentWeightMode;
        [JsonProperty]
        public float Time;
        [JsonProperty]
        public float Value;
        [JsonProperty]
        public float ArriveTangent;
        [JsonProperty]
        public float ArriveTangentWeight;
        [JsonProperty]
        public float LeaveTangent;
        [JsonProperty]
        public float LeaveTangentWeight;

        public RichCurveKeyPropertyData(FName name) : base(name)
        {

        }

        public RichCurveKeyPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("RichCurveKey");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            InterpMode = (ERichCurveInterpMode)reader.ReadSByte();
            TangentMode = (ERichCurveTangentMode)reader.ReadSByte();
            TangentWeightMode = (ERichCurveTangentWeightMode)reader.ReadSByte();
            Time = reader.ReadSingle();
            Value = reader.ReadSingle();
            ArriveTangent = reader.ReadSingle();
            ArriveTangentWeight = reader.ReadSingle();
            LeaveTangent = reader.ReadSingle();
            LeaveTangentWeight = reader.ReadSingle();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write((sbyte)InterpMode);
            writer.Write((sbyte)TangentMode);
            writer.Write((sbyte)TangentWeightMode);
            writer.Write(Time);
            writer.Write(Value);
            writer.Write(ArriveTangent);
            writer.Write(ArriveTangentWeight);
            writer.Write(LeaveTangent);
            writer.Write(LeaveTangentWeight);
            return sizeof(float) * 6 + sizeof(sbyte) * 3;
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Enum.TryParse(d[0], out InterpMode);
            Enum.TryParse(d[1], out TangentMode);
            Enum.TryParse(d[2], out TangentWeightMode);
            if (float.TryParse(d[3], out float res1)) Time = res1;
            if (float.TryParse(d[4], out float res2)) Value = res2;
            if (float.TryParse(d[5], out float res3)) ArriveTangent = res3;
            if (float.TryParse(d[6], out float res4)) ArriveTangentWeight = res4;
            if (float.TryParse(d[7], out float res5)) LeaveTangent = res5;
            if (float.TryParse(d[8], out float res6)) LeaveTangentWeight = res6;
        }

        public override string ToString()
        {
            string oup = "(";
            oup += InterpMode + ", ";
            oup += TangentMode + ", ";
            oup += TangentWeightMode + ", ";
            oup += Time + ", ";
            oup += Value + ", ";
            oup += ArriveTangent + ", ";
            oup += ArriveTangentWeight + ", ";
            oup += LeaveTangent + ", ";
            oup += LeaveTangentWeight + ")";
            return oup;
        }
    }
}
