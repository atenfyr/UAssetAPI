using System;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;
using System.Reflection.PortableExecutable;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes a boolean (<see cref="bool"/>).
    /// </summary>
    public class BoolPropertyData : PropertyData<bool>
    {
        public BoolPropertyData(FName name) : base(name)
        {

        }

        public BoolPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("BoolProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }
        public override object DefaultValue { get { return false; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (reader.Asset.HasUnversionedProperties || reader.Asset.ObjectVersionUE5 < ObjectVersionUE5.PROPERTY_TAG_COMPLETE_TYPE_NAME)
            {
                Value = reader.ReadBoolean();
            }
            else
            {
                Value = this.PropertyTagFlags.HasFlag(EPropertyTagFlags.BoolTrue);
            }
            if (includeHeader)
            {
                this.ReadEndPropertyTag(reader);
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (writer.Asset.HasUnversionedProperties || writer.Asset.ObjectVersionUE5 < ObjectVersionUE5.PROPERTY_TAG_COMPLETE_TYPE_NAME)
            {
                writer.Write(Value);
            }
            // else, special case in MainSerializer

            if (includeHeader)
            {
                this.WriteEndPropertyTag(writer);
            }
            return 0;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = d[0].Equals("1") || d[0].ToLowerInvariant().Equals("true");
        }
    }
}