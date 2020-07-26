using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class StrPropertyData : PropertyData<string>
    {
        public StrPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "StrProperty";
        }

        public StrPropertyData()
        {
            Type = "StrProperty";
        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadUString();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
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