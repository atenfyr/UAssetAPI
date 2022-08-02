using Newtonsoft.Json.Linq;
using System;

namespace UAssetAPI.UnrealTypes
{
	public class FFrameNumber
    {
		public int Value;

		public FFrameNumber(int value)
        {
			Value = value;
		}

		public FFrameNumber()
        {

		}
    }

	public class FFrameRate
    {
		public int Numerator; // 0x00(0x04)
		public int Denominator; // 0x04(0x04)

        public FFrameRate()
        {

        }

        public FFrameRate(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
    }

    public class FFrameTime
    {
		public FFrameNumber FrameNumber; // 0x00(0x04)
		public float SubFrame; // 0x04(0x04)

        public FFrameTime()
        {

        }

        public FFrameTime(FFrameNumber frameNumber, float subFrame)
        {
            FrameNumber = frameNumber;
            SubFrame = subFrame;
        }
    }

    // Scriptclass CoreUObject.QualifiedFrameTime
    // Size: 0x10 (Inherited: 0x00)
    public class FQualifiedFrameTime
    {
		public FFrameTime Time; // 0x00(0x08)
		public FFrameRate Rate; // 0x08(0x08)

        public FQualifiedFrameTime()
        {

        }

        public FQualifiedFrameTime(FFrameTime time, FFrameRate rate)
        {
            Time = time;
            Rate = rate;
        }

    }

    // Scriptclass CoreUObject.Timecode
    // Size: 0x14 (Inherited: 0x00)
    public class FTimecode
    {
		public int Hours; // 0x00(0x04)
		public int Minutes; // 0x04(0x04)
		public int Seconds; // 0x08(0x04)
		public int Frames; // 0x0c(0x04)
		public bool bDropFrameFormat; // 0x10(0x01)
        //char pad_11[0x3]; // 0x11(0x03)

        public FTimecode()
        {
        }

        public FTimecode(int hours, int minutes, int seconds, int frames, bool bDropFrameFormat)
        {
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Frames = frames;
            this.bDropFrameFormat = bDropFrameFormat;
        }
    }

    public struct FFrameNumberRangeBound
    {
        public ERangeBoundTypes Type; // 0x00(0x01)
        public FFrameNumber Value; // 0x04(0x04)

        public FFrameNumberRangeBound(sbyte _Type, int _Value)
        {
            Type = (ERangeBoundTypes)_Type;
            Value = new FFrameNumber(_Value);
        }
    }

    // ScriptStruct CoreUObject.FrameNumberRange
    // Size: 0x10 (Inherited: 0x00)
    public struct FFrameNumberRange
    {
        public FFrameNumberRangeBound LowerBound; // 0x00(0x08)
	    public FFrameNumberRangeBound UpperBound; // 0x08(0x08)

        public FFrameNumberRange(AssetBinaryReader reader)
        {
            LowerBound = new FFrameNumberRangeBound(reader.ReadSByte(), reader.ReadInt32());
            UpperBound = new FFrameNumberRangeBound(reader.ReadSByte(), reader.ReadInt32());
        }

        public void Read(AssetBinaryReader reader)
        {
            LowerBound = new FFrameNumberRangeBound(reader.ReadSByte(), reader.ReadInt32());
            UpperBound = new FFrameNumberRangeBound(reader.ReadSByte(), reader.ReadInt32());
        }

        public void Write(AssetBinaryWriter writer)
        {
            writer.Write((sbyte)LowerBound.Type);
            writer.Write(LowerBound.Value.Value);
            writer.Write((sbyte)UpperBound.Type);
            writer.Write(UpperBound.Value.Value);
        }
    }
}
