using Newtonsoft.Json;
using System;
using System.Linq;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes an enumeration value.
    /// </summary>
    public class EnumPropertyData : PropertyData<FName>
    {
        [JsonProperty]
        public FName EnumType;
        /// <summary>
        /// Only used with unversioned properties.
        /// </summary>
        [JsonProperty]
        public FName InnerType;

        public byte[] ExtraData;

        public EnumPropertyData(FName name) : base(name)
        {

        }

        public EnumPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("EnumProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public static readonly string InvalidEnumIndexFallbackPrefix = "UASSETAPI_INVALID_ENUM_IDX_";

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            bool skipEndingFName = false;
            if (reader.Asset.HasUnversionedProperties)
            {
                if (reader.Asset.Mappings.TryGetPropertyData(Name, Ancestry, reader.Asset, out UsmapEnumData enumDat1))
                {
                    EnumType = FName.DefineDummy(reader.Asset, enumDat1.Name);
                    InnerType = FName.DefineDummy(reader.Asset, enumDat1.InnerType.Type.ToString());
                }

                if (InnerType?.Value.Value == "ByteProperty")
                {
                    long enumIndice = reader.ReadByte();
                    var listOfValues = reader.Asset.Mappings.EnumMap[EnumType.Value.Value].Values;
                    if (enumIndice == byte.MaxValue)
                    {
                        Value = null;
                    }
                    else if (enumIndice < listOfValues.Count)
                    {
                        Value = FName.DefineDummy(reader.Asset, listOfValues[enumIndice]);
                    }
                    else
                    {
                        // fallback
                        Value = FName.DefineDummy(reader.Asset, InvalidEnumIndexFallbackPrefix + enumIndice.ToString());
                    }
                    skipEndingFName = true;
                }

                if (InnerType?.Value.Value == "IntProperty")
                {
                    long enumIndice = reader.ReadInt32();
                    var listOfValues = reader.Asset.Mappings.EnumMap[EnumType.Value.Value].Values;
                    if (enumIndice < listOfValues.Count)
                    {
                        Value = FName.DefineDummy(reader.Asset, listOfValues[enumIndice]);
                    }
                    else
                    {
                        // fallback
                        Value = FName.DefineDummy(reader.Asset, InvalidEnumIndexFallbackPrefix + enumIndice.ToString());
                    }
                    skipEndingFName = true;
                }
            }

            if (includeHeader && !reader.Asset.HasUnversionedProperties)
            {
                EnumType = reader.ReadFName();
                PropertyGuid = reader.ReadPropertyGuid();
            }

            if (skipEndingFName)
            {
                // TODO: figure out exactly where this weird extra data is coming from...
                if (serializationContext == PropertySerializationContext.Array)
                {
                    ExtraData = reader.ReadBytes(7);
                }
            }
            else
            {
                Value = reader.ReadFName();
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (writer.Asset.HasUnversionedProperties)
            {
                if (InnerType?.Value?.Value == "ByteProperty" || InnerType?.Value?.Value == "IntProperty")
                {
                    long enumIndice = 0;
                    var listOfEnums = writer.Asset.Mappings.EnumMap[EnumType.Value.Value].Values;
                    var validIndices = listOfEnums.Where(pair => pair.Value == Value.Value.Value).Select(pair => pair.Key);
                    if (Value == null)
                    {
                        enumIndice = -1;
                    }
                    else if (validIndices.Count() == 0)
                    {
                        bool success = false;
                        if (Value.Value.Value.StartsWith(InvalidEnumIndexFallbackPrefix))
                        {
                            success = long.TryParse(Value.Value.Value.Substring(InvalidEnumIndexFallbackPrefix.Length), out enumIndice);
                        }

                        if (!success)
                        {
                            throw new FormatException("Could not serialize EnumProperty value " + Value.Value.Value + " as " + InnerType?.Value?.Value);
                        }
                    }
                    else
                    {
                        enumIndice = validIndices.FirstOrDefault();
                    }

                    int sz = 0;
                    switch (InnerType?.Value?.Value)
                    {
                        case "ByteProperty":
                            writer.Write((byte)enumIndice);
                            sz += sizeof(byte);

                            if (serializationContext == PropertySerializationContext.Array)
                            {
                                writer.Write(ExtraData);
                                sz += ExtraData.Length;
                            }
                            break;
                        case "IntProperty":
                            writer.Write((int)enumIndice);
                            sz += sizeof(int);

                            if (serializationContext == PropertySerializationContext.Array)
                            {
                                writer.Write(ExtraData);
                                sz += ExtraData.Length;
                            }
                            break;
                    }
                    return sz;
                }
            }

            if (includeHeader && !writer.Asset.HasUnversionedProperties)
            {
                writer.Write(EnumType);
                writer.WritePropertyGuid(PropertyGuid);
            }
            writer.Write(Value);
            return sizeof(int) * 2;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override void FromString(string[] d, UAsset asset)
        {
            if (d[0] != "null" && d[0] != null)
            {
                EnumType = asset.HasUnversionedProperties ? FName.DefineDummy(asset, d[0]) : FName.FromString(asset, d[0]);
            }
            else
            {
                EnumType = null;
            }

            if (d[1] != "null" && d[1] != null)
            {
                Value = asset.HasUnversionedProperties ? FName.DefineDummy(asset, d[1]) : FName.FromString(asset, d[1]);
            }
            else
            {
                Value = null;
            }

            if (!string.IsNullOrEmpty(d[2]))
            {
                ExtraData = UAPUtils.ConvertHexStringToByteArray(d[2]);
            }
            else
            {
                ExtraData = Array.Empty<byte>();
            }
        }

        protected override void HandleCloned(PropertyData res)
        {
            EnumPropertyData cloningProperty = (EnumPropertyData)res;
            cloningProperty.EnumType = (FName)this.EnumType?.Clone();
        }
    }
}