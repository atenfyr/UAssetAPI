using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class FloatPropertyData : PropertyData<float>
    {
        public FloatPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "FloatProperty";
        }

        public FloatPropertyData()
        {
            Type = "FloatProperty";
        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadSingle();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write(Value);
            return 4;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = 0;
            if (float.TryParse(d[0], out float res)) Value = res;
        }
    }
}