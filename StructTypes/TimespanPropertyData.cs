using System;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class TimespanPropertyData : PropertyData<TimeSpan>
    {
        public TimespanPropertyData(string name, AssetReader asset, bool forceReadNull = false) : base(name, asset, forceReadNull)
        {
            Type = "Timespan";
        }

        public TimespanPropertyData()
        {
            Type = "Timespan";
        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = new TimeSpan(reader.ReadInt64()); // number of ticks
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write(Value.Ticks);
            return sizeof(long);
        }

        public override void FromString(string[] d)
        {
            Value = TimeSpan.Parse(d[0]);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}