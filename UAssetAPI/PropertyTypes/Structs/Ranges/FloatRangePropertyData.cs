using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    public class FloatRangePropertyData : PropertyData
    {
        public float LowerBound;
        public float UpperBound;

        public FloatRangePropertyData(FName name) : base(name)
        {

        }

        public FloatRangePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("FloatRange");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            LowerBound = reader.ReadSingle();
            UpperBound = reader.ReadSingle();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(LowerBound);
            writer.Write(UpperBound);
            return sizeof(float) * 2;
        }

        public override void FromString(string[] d, UAsset asset)
        {
            if (float.TryParse(d[0], out float res1)) LowerBound = res1;
            if (float.TryParse(d[1], out float res2)) UpperBound = res2;
        }

        public override string ToString()
        {
            return "(" + LowerBound + ", " + UpperBound + ")";
        }
    }
}