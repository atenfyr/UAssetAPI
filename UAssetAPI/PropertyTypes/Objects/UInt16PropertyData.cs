using System;
using System.IO;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes a 16-bit unsigned integer variable (<see cref="ushort"/>).
    /// </summary>
    public class UInt16PropertyData : PropertyData<ushort>
    {
        public UInt16PropertyData(FName name) : base(name)
        {

        }

        public UInt16PropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("UInt16Property");
        public override FString PropertyType { get { return CurrentPropertyType; } }
        public override object DefaultValue { get { return (ushort)0; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                this.ReadEndPropertyTag(reader);
            }

            Value = (reader.Asset.HasUnversionedProperties && serializationContext != PropertySerializationContext.Normal) ? (ushort)reader.ReadInt64() : reader.ReadUInt16();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                this.WriteEndPropertyTag(writer);
            }

            if (writer.Asset.HasUnversionedProperties && serializationContext != PropertySerializationContext.Normal)
            {
                writer.Write((long)Value);
                return sizeof(long);
            }
            writer.Write(Value);
            return sizeof(ushort);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = 0;
            if (ushort.TryParse(d[0], out ushort res)) Value = res;
        }
    }
}