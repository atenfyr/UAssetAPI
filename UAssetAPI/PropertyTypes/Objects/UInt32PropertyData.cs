using System;
using System.IO;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes a 32-bit unsigned integer variable (<see cref="uint"/>).
    /// </summary>
    public class UInt32PropertyData : PropertyData<uint>
    {
        public UInt32PropertyData(FName name) : base(name)
        {

        }

        public UInt32PropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("UInt32Property");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = reader.ReadUInt32();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value);
            return sizeof(uint);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = 0;
            if (uint.TryParse(d[0], out uint res)) Value = res;
        }
    }
}