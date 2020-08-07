using System.Drawing;
using System.IO;
using System;
using System.Diagnostics;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class LinearColor
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public Color GetARGB()
        {
            return Color.FromArgb(
                (int)(Math.Pow(Utils.Clamp(A, 0, 1), 1.0 / 2.2) * 255),
                (int)(Math.Pow(Utils.Clamp(R, 0, 1), 1.0 / 2.2) * 255),
                (int)(Math.Pow(Utils.Clamp(G, 0, 1), 1.0 / 2.2) * 255),
                (int)(Math.Pow(Utils.Clamp(B, 0, 1), 1.0 / 2.2) * 255)
            );
        }

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
        public LinearColorPropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "LinearColor";
        }

        public LinearColorPropertyData()
        {
            Type = "LinearColor";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng)
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
            Value = new LinearColor(colorA, colorR, colorG, colorB);
        }
    }
}