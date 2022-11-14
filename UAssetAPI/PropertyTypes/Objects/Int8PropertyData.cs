using System;
using System.IO;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Objects
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

        private static readonly FString CurrentPropertyType = new FString("Int8Property");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = reader.ReadSByte();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
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