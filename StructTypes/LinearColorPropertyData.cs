using System;
using System.Drawing;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public static class LinearHelpers
    {
        public static Color Convert(LinearColor color)
        {
            float FloatR = UAPUtils.Clamp(color.R, 0.0f, 1.0f);
            float FloatG = UAPUtils.Clamp(color.G, 0.0f, 1.0f);
            float FloatB = UAPUtils.Clamp(color.B, 0.0f, 1.0f);
            float FloatA = UAPUtils.Clamp(color.A, 0.0f, 1.0f);

            FloatR = (float)(FloatR <= 0.0031308f ? FloatR * 12.92f : Math.Pow(FloatR, 1.0f / 2.4f) * 1.055f - 0.055f);
            FloatG = (float)(FloatG <= 0.0031308f ? FloatG * 12.92f : Math.Pow(FloatG, 1.0f / 2.4f) * 1.055f - 0.055f);
            FloatB = (float)(FloatB <= 0.0031308f ? FloatB * 12.92f : Math.Pow(FloatB, 1.0f / 2.4f) * 1.055f - 0.055f);

            return Color.FromArgb((byte)Math.Floor(FloatA * 255.999f), (byte)Math.Floor(FloatR * 255.999f), (byte)Math.Floor(FloatG * 255.999f), (byte)Math.Floor(FloatB * 255.999f));
        }
    }

    public class LinearColor
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public LinearColor()
        {

        }

        public LinearColor(float R, float G, float B, float A)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }
    }

    public class LinearColorPropertyData : PropertyData<LinearColor> // R, G, B, A
    {
        public LinearColorPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public LinearColorPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("LinearColor");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = new LinearColor
            {
                R = reader.ReadSingle(),
                G = reader.ReadSingle(),
                B = reader.ReadSingle(),
                A = reader.ReadSingle()
            };
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Value.R);
            writer.Write(Value.G);
            writer.Write(Value.B);
            writer.Write(Value.A);
            return sizeof(float) * 4;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override void FromString(string[] d)
        {
            if (!float.TryParse(d[0], out float colorR)) return;
            if (!float.TryParse(d[1], out float colorG)) return;
            if (!float.TryParse(d[2], out float colorB)) return;
            if (!float.TryParse(d[3], out float colorA)) return;
            Value = new LinearColor(colorR, colorG, colorB, colorA);
        }
    }
}