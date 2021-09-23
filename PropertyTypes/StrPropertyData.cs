using System.IO;
using System.Text;

namespace UAssetAPI.PropertyTypes
{
    public class StrPropertyData : PropertyData<string>
    {
        public Encoding Encoding = Encoding.ASCII;

        public StrPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public StrPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("StrProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            FString ustr = reader.ReadFStringWithEncoding();
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
            writer.WriteFString(new FString(Value, Encoding));
            return (int)writer.BaseStream.Position - here;
        }

        public override string ToString()
        {
            return Value;
        }

        public override void FromString(string[] d)
        {
            Value = d[0];
            if (d.Length >= 5) Encoding = (d[4].Equals("utf-16") ? Encoding.Unicode : Encoding.ASCII);
        }
    }
}