using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public enum RichCurveInterpMode
    {
        Linear,
        Constant,
        Cubic,
        None
    };

    public enum RichCurveTangentMode
    {
        Auto,
        User,
        Break,
        None
    };

    public enum RichCurveTangentWeightMode
    {
        WeightedNone,
        WeightedArrive,
        WeightedLeave,
        WeightedBoth
    };

    public class RichCurveKeyProperty : PropertyData
    {
        public RichCurveInterpMode InterpMode;
        public RichCurveTangentMode TangentMode;
        public RichCurveTangentWeightMode TangentWeightMode;
        public float Time;
        public float Value;
        public float ArriveTangent;
        public float ArriveTangentWeight;
        public float LeaveTangent;
        public float LeaveTangentWeight;

        public RichCurveKeyProperty(string name, AssetReader asset) : base(name, asset)
        {
            Type = "RichCurveKey";
        }

        public RichCurveKeyProperty()
        {
            Type = "RichCurveKey";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng)
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

        public override int Write(BinaryWriter writer, bool includeHeader)
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
            return 0;
        }

        public override void FromString(string[] d)
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
