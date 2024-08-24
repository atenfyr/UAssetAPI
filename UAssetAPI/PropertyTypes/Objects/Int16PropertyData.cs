using System;
using System.IO;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes a 16-bit signed integer variable (<see cref="short"/>).
    /// </summary>
    public class Int16PropertyData : PropertyData<short>
    {
        public Int16PropertyData(FName name) : base(name)
        {

        }

        public Int16PropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("Int16Property");
        public override FString PropertyType { get { return CurrentPropertyType; } }
        public override object DefaultValue { get { return (short)0; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                this.ReadEndPropertyTag(reader);
            }

            Value = (reader.Asset.HasUnversionedProperties && serializationContext != PropertySerializationContext.Normal) ? (short)reader.ReadInt64() : reader.ReadInt16();
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
            return sizeof(short);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = 0;
            if (short.TryParse(d[0], out short res)) Value = res;
        }
    }
}