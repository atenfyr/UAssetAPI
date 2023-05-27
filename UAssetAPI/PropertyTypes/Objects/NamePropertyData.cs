using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes an <see cref="FName"/>.
    /// </summary>
    public class NamePropertyData : PropertyData<FName>
    {
        public NamePropertyData(FName name) : base(name)
        {

        }

        public NamePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("NameProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = reader.ReadFName();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value);
            return sizeof(int) * 2;
        }

        public override bool IsZero(UnrealPackage asset)
        {
            return false; // even if the index is 0, we always serialize it anyways
        }

        public override string ToString()
        {
            return Value == null ? "null" : Value.ToString();
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = FName.FromString(asset, d[0]);
        }
    }
}