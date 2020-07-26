using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class BoolPropertyData : PropertyData<bool>
    {
        public BoolPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "BoolProperty";
        }

        public BoolPropertyData()
        {
            Type = "BoolProperty";
        }

        public override void Read(BinaryReader reader, long leng)
        {
            Value = reader.ReadInt16() > 0;
        }

        public override int Write(BinaryWriter writer)
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