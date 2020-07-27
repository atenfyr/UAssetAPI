using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class DateTimePropertyData : PropertyData<DateTime>
    {
        public DateTimePropertyData(string name, AssetReader asset, bool forceReadNull = false) : base(name, asset, forceReadNull)
        {
            Type = "DateTime";
        }

        public DateTimePropertyData()
        {
            Type = "DateTime";
        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = new DateTime(reader.ReadInt64()); // number of ticks since January 1, 0001
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write(Value.Ticks);
            return sizeof(long);
        }

        public override void FromString(string[] d)
        {
            Value = DateTime.Parse(d[0]);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}