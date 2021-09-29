using System.IO;
using System.Text;

namespace UAssetAPI.PropertyTypes
{
    /// <summary>
    /// Describes an <see cref="FString"/>.
    /// </summary>
    public class StrPropertyData : PropertyData<FString>
    {
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

            Value = reader.ReadFStringWithEncoding();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            int here = (int)writer.BaseStream.Position;
            writer.WriteFString(Value);
            return (int)writer.BaseStream.Position - here;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override void FromString(string[] d)
        {
            var encoding = Encoding.ASCII;
            if (d.Length >= 5) encoding = (d[4].Equals("utf-16") ? Encoding.Unicode : Encoding.ASCII);
            Value = new FString(d[0], encoding);
        }
    }
}