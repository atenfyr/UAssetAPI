using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    /// <summary>
    /// Describes something very cool!
    /// </summary>
    public class CoolPropertyData : PropertyData<int>
    {
        public CoolPropertyData(FName name) : base(name)
        {

        }

        public CoolPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("CoolProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }
            Value = (int)reader.ReadByte();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write((byte)Value);
            return sizeof(byte);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            if (int.TryParse(d[0], out int x)) Value = x;
        }
    }
}