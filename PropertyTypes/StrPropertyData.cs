using System.IO;
using System.Text;

namespace UAssetAPI.PropertyTypes
{
    public class StrPropertyData : PropertyData<string>
    {
        public Encoding Encoding = Encoding.UTF8;

        public StrPropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "StrProperty";
        }

        public StrPropertyData()
        {
            Type = "StrProperty";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            UString ustr = reader.ReadUStringWithEncoding();
            if (ustr != null) Encoding = ustr.Encoding;
            Value = ustr != null ? ustr.Value : null;
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            int here = (int)writer.BaseStream.Position;
            writer.WriteUString(new UString(Value, Encoding));
            return (int)writer.BaseStream.Position - here;
        }

        public override string ToString()
        {
            return Value;
        }

        public override void FromString(string[] d)
        {
            Value = d[0];
            if (d.Length >= 5) Encoding = (d[5].Equals("utf-16") ? Encoding.ASCII : Encoding.UTF8);
        }
    }
}