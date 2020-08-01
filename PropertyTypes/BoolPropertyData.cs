using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class BoolPropertyData : PropertyData<bool>
    {
        public BoolPropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "BoolProperty";
        }

        public BoolPropertyData()
        {
            Type = "BoolProperty";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng)
        {
            Value = reader.ReadInt16() > 0;
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            writer.Write((short)(Value ? 1 : 0));
            return 0;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = d[0].Equals("1") || d[0].ToLower().Equals("true");
        }
    }
}