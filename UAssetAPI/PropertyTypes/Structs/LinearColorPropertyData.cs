using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
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

    /// <summary>
    /// A linear, 32-bit/component floating point RGBA color.
    /// </summary>
    public class LinearColor : ICloneable
    {
        [JsonProperty]
        public float R;
        [JsonProperty]
        public float G;
        [JsonProperty]
        public float B;
        [JsonProperty]
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

        public object Clone()
        {
            return new LinearColor(this.R, this.G, this.B, this.A);
        }
    }

    public class LinearColorPropertyData : PropertyData<LinearColor> // R, G, B, A
    {
        public LinearColorPropertyData(FName name) : base(name)
        {

        }

        public LinearColorPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("LinearColor");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new LinearColor
            {
                R = reader.ReadSingle(),
                G = reader.ReadSingle(),
                B = reader.ReadSingle(),
                A = reader.ReadSingle()
            };
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
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

        public override void FromString(string[] d, UAsset asset)
        {
            if (!float.TryParse(d[0], out float colorR)) return;
            if (!float.TryParse(d[1], out float colorG)) return;
            if (!float.TryParse(d[2], out float colorB)) return;
            if (!float.TryParse(d[3], out float colorA)) return;
            Value = new LinearColor(colorR, colorG, colorB, colorA);
        }
    }
}