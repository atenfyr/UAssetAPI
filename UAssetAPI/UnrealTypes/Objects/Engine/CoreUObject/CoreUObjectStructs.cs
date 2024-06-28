using System;

namespace UAssetAPI.UnrealTypes;

public struct TRange<T>(AssetBinaryReader reader, Func<T> valueReader)
{
    public TRangeBound<T> LowerBound = new TRangeBound<T>(reader, valueReader);
    public TRangeBound<T> UpperBound = new TRangeBound<T>(reader, valueReader);

    public void Write(AssetBinaryWriter writer, Action<T> valueWriter)
    {
        LowerBound.Write(writer, valueWriter);
        UpperBound.Write(writer, valueWriter);
    }
}

/// <summary>
/// Template for range bounds.
/// </summary>
public struct TRangeBound<T>(AssetBinaryReader reader, Func<T> valueReader)
{
    public ERangeBoundTypes Type = (ERangeBoundTypes)reader.ReadByte();
    public T Value = valueReader();

    public void Write(AssetBinaryWriter writer, Action<T> valueWriter)
    {
        writer.Write((byte)Type);
        valueWriter(Value);
    }
}

public struct FFrameNumber
{
    public int Value;

    public FFrameNumber(int value)
    {
        Value = value;
    }

    public FFrameNumber(AssetBinaryReader reader)
    {
        Value = reader.ReadInt32();
    }
    
    public void Write(AssetBinaryWriter writer)
    {
        writer.Write(Value);
    }
}

public struct FFrameRate
{
    public int Numerator;
    public int Denominator;

    public FFrameRate() { }

    public FFrameRate(int numerator, int denominator)
    {
        Numerator = numerator;
        Denominator = denominator;
    }

    public FFrameRate(AssetBinaryReader reader)
    {
        Numerator = reader.ReadInt32();
        Denominator = reader.ReadInt32();
    }

    public void Write(AssetBinaryWriter writer)
    {
        writer.Write(Numerator);
        writer.Write(Denominator);
    }

    public override string ToString()
    {
        return Numerator.ToString() + "/" + Denominator.ToString();
    }

    public static bool TryParse(string s, out FFrameRate result)
    {
        result = new FFrameRate();
        string[] parts = s.Trim().Split('/');

        if (parts.Length != 2) return false;
        if (!int.TryParse(parts[0], out int numer)) return false;
        if (!int.TryParse(parts[1], out int denom)) return false;

        result = new FFrameRate(numer, denom);
        return true;
    }
}

public struct FFrameTime
{
    public FFrameNumber FrameNumber;
    public float SubFrame;

    public FFrameTime() { }

    public FFrameTime(FFrameNumber frameNumber, float subFrame)
    {
        FrameNumber = frameNumber;
        SubFrame = subFrame;
    }

    public FFrameTime(AssetBinaryReader reader)
    {
        FrameNumber = new FFrameNumber(reader);
        SubFrame = reader.ReadSingle();
    }

    public void Write(AssetBinaryWriter writer)
    {
        FrameNumber.Write(writer);
        writer.Write(SubFrame);
    }
}

public struct FQualifiedFrameTime
{
    public FFrameTime Time;
    public FFrameRate Rate;

    public FQualifiedFrameTime() { }

    public FQualifiedFrameTime(FFrameTime time, FFrameRate rate)
    {
        Time = time;
        Rate = rate;
    }

    public FQualifiedFrameTime(AssetBinaryReader reader)
    {
        Time = new FFrameTime(reader);
        Rate = new FFrameRate(reader);
    }

    public void Write(AssetBinaryWriter writer)
    {
        Time.Write(writer);
        Rate.Write(writer);
    }
}

public struct FTimecode
{
    public int Hours;
    public int Minutes;
    public int Seconds;
    public int Frames;
    public bool bDropFrameFormat;

    public FTimecode() { }

    public FTimecode(int hours, int minutes, int seconds, int frames, bool bDropFrameFormat)
    {
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
        Frames = frames;
        this.bDropFrameFormat = bDropFrameFormat;
    }

    public FTimecode(AssetBinaryReader reader)
    {
        Hours = reader.ReadInt32();
        Minutes = reader.ReadInt32();
        Seconds = reader.ReadInt32();
        Frames = reader.ReadInt32();
        bDropFrameFormat = reader.ReadBoolean();
    }

    public void Write(AssetBinaryWriter writer)
    {
        writer.Write(Hours);
        writer.Write(Minutes);
        writer.Write(Seconds);
        writer.Write(Frames);
        writer.Write(bDropFrameFormat);
    }
}