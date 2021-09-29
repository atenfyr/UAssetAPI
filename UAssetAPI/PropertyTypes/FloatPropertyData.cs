using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    /// <summary>
    /// Describes an IEEE 32-bit floating point variable (<see cref="float"/>).
    /// </summary>
    public class FloatPropertyData : PropertyData<float>
    {
        public FloatPropertyData(FName name) : base(name)
        {

        }

        public FloatPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("FloatProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadSingle();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Value);
            return sizeof(float);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = 0;
            if (float.TryParse(d[0], out float res)) Value = res;
        }
    }
}