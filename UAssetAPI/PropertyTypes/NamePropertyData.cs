using System.IO;

namespace UAssetAPI.PropertyTypes
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

        private static readonly FName CurrentPropertyType = new FName("NameProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadFName();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Value);
            return sizeof(int) * 2;
        }

        public override string ToString()
        {
            return Value == null ? "null" : Value.ToString();
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = FName.FromString(d[0]);
        }
    }
}