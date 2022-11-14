using System;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /// <summary>
    /// Implements a time span.
    /// A time span is the difference between two dates and times. For example, the time span between
    /// 12:00:00 January 1, 2000 and 18:00:00 January 2, 2000 is 30.0 hours. Time spans are measured in
    /// positive or negative ticks depending on whether the difference is measured forward or backward.
    /// Each tick has a resolution of 0.1 microseconds (= 100 nanoseconds).
    /// 
    /// In conjunction with the companion class <see cref="DateTimePropertyData"/> (<see cref="DateTime"/>),
    /// time spans can be used to perform date and time based arithmetic, such as calculating the
    /// difference between two dates or adding a certain amount of time to a given date.
    /// </summary>
    public class TimespanPropertyData : PropertyData<TimeSpan>
    {
        public TimespanPropertyData(FName name) : base(name)
        {

        }

        public TimespanPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("Timespan");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new TimeSpan(reader.ReadInt64()); // number of ticks
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value.Ticks);
            return sizeof(long);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = TimeSpan.Parse(d[0]);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected override void HandleCloned(PropertyData res)
        {
            TimespanPropertyData cloningProperty = (TimespanPropertyData)res;
            cloningProperty.Value = new TimeSpan(cloningProperty.Value.Ticks);
        }
    }
}