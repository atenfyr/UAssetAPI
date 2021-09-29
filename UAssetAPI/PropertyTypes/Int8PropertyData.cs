using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    /// <summary>
    /// Describes an 8-bit signed integer variable (<see cref="sbyte"/>).
    /// </summary>
    public class Int8PropertyData : PropertyData<sbyte>
    {
        public Int8PropertyData(FName name) : base(name)
        {

        }

        public Int8PropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("Int8Property");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadSByte();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Value);
            return sizeof(sbyte);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = 0;
            if (sbyte.TryParse(d[0], out sbyte res)) Value = res;
        }
    }
}