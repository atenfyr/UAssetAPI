using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    /// <summary>
    /// Describes a 16-bit unsigned integer variable (<see cref="ushort"/>).
    /// </summary>
    public class UInt16PropertyData : PropertyData<ushort>
    {
        public UInt16PropertyData(FName name, UAsset asset) : base(name, asset)
        {

        }

        public UInt16PropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("UInt16Property");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadUInt16();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Value);
            return sizeof(ushort);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = 0;
            if (ushort.TryParse(d[0], out ushort res)) Value = res;
        }
    }
}