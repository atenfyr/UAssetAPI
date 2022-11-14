using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    public class MovieSceneFloatChannelPropertyData : PropertyData<FMovieSceneFloatChannel>
    {
        public int TimesStructLength;
        public int TimesLength;

        public int ValuesStructLength;
        public int ValuesLength;
        public int HasDefaultValue;


        public MovieSceneFloatChannelPropertyData(FName name) : base(name)
        {

        }

        public MovieSceneFloatChannelPropertyData()
        {
            
        }

        private static readonly FString CurrentPropertyType = new FString("MovieSceneFloatChannel");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FMovieSceneFloatChannel();

            Value.PreInfinityExtrap = (ERichCurveExtrapolation)reader.ReadSByte();
            Value.PostInfinityExtrap = (ERichCurveExtrapolation)reader.ReadSByte();

            TimesStructLength = reader.ReadInt32();
            TimesLength = reader.ReadInt32();

            Value.Times = new FFrameNumber[TimesLength];
            for (int j = 0; j < TimesLength; j++)
            {
                Value.Times[j] = new FFrameNumber(reader.ReadInt32());
            }

            ValuesStructLength = reader.ReadInt32();
            ValuesLength = reader.ReadInt32();
            Value.Values = new FMovieSceneFloatValue[ValuesLength];

            for (int j = 0; j < ValuesLength; j++)
            {
                Value.Values[j].Read(reader);
            }

            Value.DefaultValue = reader.ReadSingle();
            HasDefaultValue = reader.ReadInt32();
            Value.bHasDefaultValue = HasDefaultValue == 0 ? false : true;

            Value.TickResolution = new FFrameRate(reader.ReadInt32(), reader.ReadInt32());
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            int here = (int)writer.BaseStream.Position;

            writer.Write((sbyte)Value.PreInfinityExtrap);
            writer.Write((sbyte)Value.PostInfinityExtrap);

            writer.Write(TimesStructLength);
            writer.Write(TimesLength);
            for (int j = 0; j < TimesLength; j++) {
                writer.Write(Value.Times[j].Value); 
            }

            writer.Write(ValuesStructLength);
            writer.Write(ValuesLength);
            for (int j = 0; j < ValuesLength; j++) {
                Value.Values[j].Write(writer);
            }

            writer.Write(Value.DefaultValue);
            writer.Write(Value.bHasDefaultValue == false ? 0 : HasDefaultValue);
            writer.Write(Value.TickResolution.Numerator);
            writer.Write(Value.TickResolution.Denominator);

            return (int)writer.BaseStream.Position - here;
        }

        public override void FromString(string[] d, UAsset asset)
        {

        }

        public override string ToString()
        {
            return "";
        }
    }
}
