using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.PropertyTypes.Objects
{
    public enum BytePropertyType
    {
        Byte,
        FName,
    }

    /// <summary>
    /// Describes a byte or an enumeration value.
    /// </summary>
    public class BytePropertyData : PropertyData
    {
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public BytePropertyType ByteType;

        [JsonProperty]
        public FName EnumType;
        [JsonProperty]
        public byte Value;
        [JsonProperty]
        public FName EnumValue;

        public bool ShouldSerializeValue()
        {
            return ByteType == BytePropertyType.Byte;
        }

        public bool ShouldSerializeEnumValue()
        {
            return ByteType == BytePropertyType.FName;
        }

        public BytePropertyData(FName name) : base(name)
        {

        }

        public BytePropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("ByteProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            ReadCustom(reader, includeHeader, leng1, leng2, true);
        }

        private void ReadCustom(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2, bool canRepeat)
        {
            if (includeHeader && !reader.Asset.HasUnversionedProperties)
            {
                EnumType = reader.ReadFName();
                this.ReadEndPropertyTag(reader);
            }

            bool useFailsafe = true;
            if (reader.Asset.Mappings != null && reader.Asset.Mappings.TryGetPropertyData(Name, Ancestry, reader.Asset, out UsmapPropertyData propDat))
            {
                useFailsafe = false;
                ByteType = propDat is UsmapEnumData ? BytePropertyType.FName : BytePropertyType.Byte; // TODO: not always accurate?
            }

            // override with length if it makes sense to do so
            if (!reader.Asset.HasUnversionedProperties)
            {
                switch (leng1)
                {
                    case 1:
                        ByteType = BytePropertyType.Byte;
                        useFailsafe = false;
                        break;
                    case 8:
                        ByteType = BytePropertyType.FName;
                        useFailsafe = false;
                        break;
                }
            }
            
            if (useFailsafe)
            {
                switch (leng1)
                {
                    case 0: // Should be only seen in maps; fallback "make our best guess and pray we're right" behavior
                        int nameMapPointer = reader.ReadInt32();
                        int nameMapIndex = reader.ReadInt32();
                        reader.BaseStream.Position -= sizeof(int) * 2;

                        // In the case of it being serialized as just a byte, it will probably try to parse part of the next property and produce something too big for the name map
                        if (nameMapPointer >= 0 && nameMapPointer < reader.Asset.GetNameMapIndexList().Count && nameMapIndex == 0 && !reader.Asset.GetNameReference(nameMapPointer).ToString().Contains("/"))
                        {
                            ByteType = BytePropertyType.FName;
                            break;
                        }
                        else
                        {
                            ByteType = BytePropertyType.Byte;
                            break;
                        }
                    default:
                        if (canRepeat)
                        {
                            ReadCustom(reader, false, leng2, 0, false);
                            return;
                        }
                        throw new FormatException("Invalid length " + leng1 + " for ByteProperty");
                }
            }

            if (ByteType == BytePropertyType.Byte)
            {
                Value = reader.ReadByte();
            }
            else if (ByteType == BytePropertyType.FName)
            {
                EnumValue = reader.ReadFName();
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader && !writer.Asset.HasUnversionedProperties)
            {
                writer.Write(EnumType);
                this.WriteEndPropertyTag(writer);
            }

            switch (ByteType)
            {
                case BytePropertyType.Byte:
                    writer.Write((byte)Value);
                    return 1;
                case BytePropertyType.FName:
                    writer.Write(EnumValue);
                    return 8;
                default:
                    throw new FormatException("Invalid BytePropertyType " + ByteType);
            }
        }

        public FName GetEnumBase()
        {
            return EnumType;
        }

        public FName GetEnumFull()
        {
            return EnumValue;
        }

        public override string ToString()
        {
            if (ByteType == BytePropertyType.Byte) return Convert.ToString(Value);
            //return GetEnumFull().Value;
            return Value.ToString();
        }

        public override void FromString(string[] d, UAsset asset)
        {
            EnumType = FName.FromString(asset, d[0]);
            if (byte.TryParse(d[1], out byte res))
            {
                ByteType = BytePropertyType.Byte;
                Value = res;
            }
            else
            {
                ByteType = BytePropertyType.FName;
                EnumValue = FName.FromString(asset, d[1]);
            }
        }
    }
}