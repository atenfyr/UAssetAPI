using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    /// <summary>
    /// Describes a 64-bit signed integer variable (<see cref="long"/>).
    /// </summary>
    public class Int64PropertyData : PropertyData<long>
    {
        public Int64PropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public Int64PropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("Int64Property");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadInt64();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Value);
            return sizeof(long);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = 0;
            if (long.TryParse(d[0], out long res)) Value = res;
        }
    }
}