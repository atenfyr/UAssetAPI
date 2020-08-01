using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class StrPropertyData : PropertyData<string>
    {
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

            Value = reader.ReadUString();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            int here = (int)writer.BaseStream.Position;
            writer.WriteUString(Value);
            return (int)writer.BaseStream.Position - here;
        }

        public override string ToString()
        {
            return Value;
        }

        public override void FromString(string[] d)
        {
            Value = d[0];
        }
    }
}