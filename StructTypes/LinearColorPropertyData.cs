using System.Drawing;
using System.IO;
using System;
using System.Diagnostics;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class LinearColorPropertyData : PropertyData<Color> // R, G, B, A
    {
        // These fields are used to ensure that identical floats are written if the color is unchanged
        private float[] originalData;
        private Color initialColor;

        public LinearColorPropertyData(string name, AssetReader asset, bool forceReadNull = false) : base(name, asset, forceReadNull)
        {
            Type = "LinearColor";
        }

        public LinearColorPropertyData()
        {
            Type = "LinearColor";
        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            originalData = new float[4];
            for (int i = 0; i < 4; i++)
            {
                originalData[i] = reader.ReadSingle();
            }

            Value = Color.FromArgb((int)(Math.Min(originalData[3] * 255, 255)), (int)(Math.Min(originalData[0] * 255, 255)), (int)(Math.Min(originalData[1] * 255, 255)), (int)(Math.Min(originalData[2] * 255, 255)));
            initialColor = Color.FromArgb(Value.ToArgb());
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);

            if (initialColor.ToArgb() == Value.ToArgb())
            {
                for (int i = 0; i < 4; i++) writer.Write(originalData[i]);
            }
            else
            {
                writer.Write((float)Value.R / 255);
                writer.Write((float)Value.G / 255);
                writer.Write((float)Value.B / 255);
                writer.Write((float)Value.A / 255);
            }
            return 16;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override void FromString(string[] d)
        {
            if (!int.TryParse(d[0], out int colorR)) return;
            if (!int.TryParse(d[1], out int colorG)) return;
            if (!int.TryParse(d[2], out int colorB)) return;
            if (!int.TryParse(d[3], out int colorA)) return;
            Value = Color.FromArgb(colorA, colorR, colorG, colorB);
        }
    }
}