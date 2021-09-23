using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class BoolPropertyData : PropertyData<bool>
    {
        public BoolPropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public BoolPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("BoolProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            Value = reader.ReadBoolean();
            if (includeHeader)
            {
                reader.ReadByte();
            }
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            writer.Write(Value);
            if (includeHeader)
            {
                writer.Write((byte)0);
            }
            return 0;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = d[0].Equals("1") || d[0].ToLowerInvariant().Equals("true");
        }
    }
}