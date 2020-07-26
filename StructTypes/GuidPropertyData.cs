using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class GuidPropertyData : PropertyData<Guid>
    {
        public GuidPropertyData(string name, AssetReader asset, bool forceReadNull = false) : base(name, asset, forceReadNull)
        {
            Type = "Guid";
        }

        public GuidPropertyData()
        {
            Type = "Guid";
        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = new Guid(reader.ReadBytes(16));
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write(Value.ToByteArray());
            return 16;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            if (Guid.TryParse(d[0], out Guid res)) Value = res;
        }
    }
}