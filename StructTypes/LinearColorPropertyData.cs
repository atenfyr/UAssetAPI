using System.Drawing;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class LinearColorPropertyData : PropertyData<Color> // R, G, B, A
    {
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
            var data = new float[4];
            for (int i = 0; i < 4; i++)
            {
                data[i] = reader.ReadSingle();
            }
            Value = Color.FromArgb((int)(data[3] * 255), (int)(data[0] * 255), (int)(data[1] * 255), (int)(data[2] * 255));
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write((float)Value.R / 255);
            writer.Write((float)Value.G / 255);
            writer.Write((float)Value.B / 255);
            writer.Write((float)Value.A / 255);
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