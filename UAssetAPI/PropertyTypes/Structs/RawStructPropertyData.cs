using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs
{
    public class RawStructPropertyData : PropertyData<byte[]>
    {
        public RawStructPropertyData(FName name) : base(name)
        {

        }

        public RawStructPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("RawStructProperty");
        public override bool HasCustomStructSerialization { get { return true; } }
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = reader.ReadBytes((int)leng1);
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value);
            return Value.Length;
        }
    }
}
