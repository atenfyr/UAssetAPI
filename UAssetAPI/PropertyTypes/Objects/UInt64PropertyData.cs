using System;
using System.IO;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes a 64-bit unsigned integer variable (<see cref="ulong"/>).
    /// </summary>
    public class UInt64PropertyData : PropertyData<ulong>
    {
        public UInt64PropertyData(FName name) : base(name)
        {

        }

        public UInt64PropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("UInt64Property");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = reader.ReadUInt64();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value);
            return sizeof(ulong);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = 0;
            if (ulong.TryParse(d[0], out ulong res)) Value = res;
        }
    }
}