using System;
using System.IO;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    /// <summary>
    /// Implements a date and time.
    ///
    /// Values of this type represent dates and times between Midnight 00:00:00, January 1, 0001 and
    /// Midnight 23:59:59.9999999, December 31, 9999 in the Gregorian calendar. Internally, the time
    /// values are stored in ticks of 0.1 microseconds (= 100 nanoseconds) since January 1, 0001.
    ///
    /// The companion class <see cref="TimespanPropertyData"/> (<see cref="TimeSpan"/>) is provided for
    /// enabling date and time based arithmetic, such as calculating the difference between two dates
    /// or adding a certain amount of time to a given date.
    /// </summary>
    public class DateTimePropertyData : PropertyData<DateTime>
    {
        public DateTimePropertyData(FName name) : base(name)
        {

        }

        public DateTimePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("DateTime");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new DateTime(reader.ReadInt64()); // number of ticks since January 1, 0001
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
            Value = DateTime.Parse(d[0]);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        protected override void HandleCloned(PropertyData res)
        {
            DateTimePropertyData cloningProperty = (DateTimePropertyData)res;
            cloningProperty.Value = new DateTime(cloningProperty.Value.Ticks);
        }
    }
}