using System.Drawing;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class ColorPropertyData : PropertyData<Color> // R, G, B, A
    {
        public ColorPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public ColorPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("Color");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = Color.FromArgb(reader.ReadInt32());
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Value.ToArgb());
            return sizeof(int);
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

        protected override void HandleCloned(PropertyData res)
        {
            ColorPropertyData cloningProperty = (ColorPropertyData)res;
            cloningProperty.Value = Color.FromArgb(this.Value.ToArgb());
        }
    }
}