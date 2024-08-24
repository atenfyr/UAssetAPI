using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

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
    public TimespanPropertyData(FName name) : base(name) { }

    public TimespanPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Timespan");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new TimeSpan(reader.ReadInt64()); // number of ticks
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
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
        cloningProperty.Value = new TimeSpan(this.Value.Ticks);
    }
}