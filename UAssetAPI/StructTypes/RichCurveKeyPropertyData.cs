using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public enum RichCurveInterpMode
    {
        Linear,
        Constant,
        Cubic,
        None
    }

    public enum RichCurveTangentMode
    {
        Auto,
        User,
        Break,
        None
    }

    public enum RichCurveTangentWeightMode
    {
        WeightedNone,
        WeightedArrive,
        WeightedLeave,
        WeightedBoth
    }

    public class RichCurveKeyPropertyData : PropertyData
    {
        [JsonProperty]
        public RichCurveInterpMode InterpMode;
        [JsonProperty]
        public RichCurveTangentMode TangentMode;
        [JsonProperty]
        public RichCurveTangentWeightMode TangentWeightMode;
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

        private static readonly FName CurrentPropertyType = new FName("RichCurveKey");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            InterpMode = (RichCurveInterpMode)reader.ReadSByte();
            TangentMode = (RichCurveTangentMode)reader.ReadSByte();
            TangentWeightMode = (RichCurveTangentWeightMode)reader.ReadSByte();
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
                writer.Write((byte)0);
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
