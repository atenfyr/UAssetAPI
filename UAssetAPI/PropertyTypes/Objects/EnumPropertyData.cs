using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public EnumPropertyData(FName name) : base(name)
        {
        }

        public EnumPropertyData()
        {

        }

        private static readonly List<string> ValidEnumInnerTypeList = ["ByteProperty", "UInt16Property", "UInt32Property", "Int8Property", "Int16Property", "IntProperty", "Int64Property"];

        private static readonly FString CurrentPropertyType = new FString("EnumProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        public static readonly string InvalidEnumIndexFallbackPrefix = "UASSETAPI_INVALID_ENUM_IDX_";

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (reader.Asset.Mappings != null && reader.Asset.Mappings.TryGetPropertyData(Name, Ancestry, reader.Asset, out UsmapEnumData enumDat1))
            {
                EnumType = reader.Asset.HasUnversionedProperties ? FName.DefineDummy(reader.Asset, enumDat1.Name) : new FName(reader.Asset, enumDat1.Name);
                InnerType = reader.Asset.HasUnversionedProperties ? FName.DefineDummy(reader.Asset, enumDat1.InnerType.Type.ToString()) : new FName(reader.Asset, enumDat1.Name);
            }

            if (reader.Asset.HasUnversionedProperties && serializationContext == PropertySerializationContext.Normal && InnerType != null)
            {
                Value = null;
                if (InnerType.Value.Value.Equals("ByteProperty", StringComparison.OrdinalIgnoreCase) ||
                    InnerType.Value.Value.Equals("UInt16Property", StringComparison.OrdinalIgnoreCase) ||
                    InnerType.Value.Value.Equals("UInt32Property", StringComparison.OrdinalIgnoreCase))
                {
                    long enumIndice = 0;

                    switch (InnerType?.Value.Value)
                    {
                        case string s when s.Equals("ByteProperty", StringComparison.OrdinalIgnoreCase):
                            enumIndice = reader.ReadByte();
                            if (enumIndice == byte.MaxValue) return;
                            break;
                        case string s when s.Equals("UInt16Property", StringComparison.OrdinalIgnoreCase):
                            enumIndice = reader.ReadUInt16();
                            if (enumIndice == ushort.MaxValue) return;
                            break;
                        case string s when s.Equals("UInt32Property", StringComparison.OrdinalIgnoreCase):
                            enumIndice = reader.ReadUInt32();
                            if (enumIndice == uint.MaxValue) return;
                            break;
                    }

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
                    return;
                }

                if (InnerType.Value.Value.Equals("Int8Property", StringComparison.OrdinalIgnoreCase) ||
                    InnerType.Value.Value.Equals("Int16Property", StringComparison.OrdinalIgnoreCase) ||
                    InnerType.Value.Value.Equals("IntProperty", StringComparison.OrdinalIgnoreCase) ||
                    InnerType.Value.Value.Equals("Int64Property", StringComparison.OrdinalIgnoreCase))
                {
                    long enumIndice = 0;

                    switch (InnerType?.Value.Value)
                    {
                        case string s when s.Equals("Int8Property", StringComparison.OrdinalIgnoreCase):
                            enumIndice = reader.ReadSByte();
                            break;
                        case string s when s.Equals("Int16Property", StringComparison.OrdinalIgnoreCase):
                            enumIndice = reader.ReadInt16();
                            break;
                        case string s when s.Equals("IntProperty", StringComparison.OrdinalIgnoreCase):
                            enumIndice = reader.ReadInt32();
                            break;
                        case string s when s.Equals("Int64Property", StringComparison.OrdinalIgnoreCase):
                            enumIndice = reader.ReadInt64();
                            break;
                    }

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
                    return;
                }
            }

            if (includeHeader && !reader.Asset.HasUnversionedProperties)
            {
                EnumType = reader.ReadFName();
                this.ReadEndPropertyTag(reader);
            }

            Value = reader.ReadFName();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (writer.Asset.Mappings != null && writer.Asset.Mappings.TryGetPropertyData(Name, Ancestry, writer.Asset, out UsmapEnumData enumDat1))
            {
                EnumType = writer.Asset.HasUnversionedProperties ? FName.DefineDummy(writer.Asset, enumDat1.Name) : new FName(writer.Asset, enumDat1.Name);
                InnerType = writer.Asset.HasUnversionedProperties ? FName.DefineDummy(writer.Asset, enumDat1.InnerType.Type.ToString()) : new FName(writer.Asset, enumDat1.Name);
            }

            if (writer.Asset.HasUnversionedProperties && serializationContext == PropertySerializationContext.Normal)
            {
                if (ValidEnumInnerTypeList.Contains(InnerType?.Value?.Value, StringComparer.OrdinalIgnoreCase))
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

                    switch (InnerType?.Value?.Value)
                    {
                        case "ByteProperty":
                            writer.Write((byte)enumIndice);
                            return sizeof(byte);
                        case "UInt16Property":
                            writer.Write((ushort)enumIndice);
                            return sizeof(ushort);
                        case "UInt32Property":
                            writer.Write((uint)enumIndice);
                            return sizeof(uint);
                        case "Int8Property":
                            writer.Write((sbyte)enumIndice);
                            return sizeof(sbyte);
                        case "Int16Property":
                            writer.Write((short)enumIndice);
                            return sizeof(short);
                        case "IntProperty":
                            writer.Write((int)enumIndice);
                            return sizeof(int);
                        case "Int64Property":
                            writer.Write((long)enumIndice);
                            return sizeof(long);
                    }
                }
            }

            if (includeHeader && !writer.Asset.HasUnversionedProperties)
            {
                writer.Write(EnumType);
                this.WriteEndPropertyTag(writer);
            }
            writer.Write(Value);
            return sizeof(int) * 2;
        }

        internal override void InitializeZero(AssetBinaryReader reader)
        {
            if (reader.Asset.Mappings.TryGetPropertyData(Name, Ancestry, reader.Asset, out UsmapEnumData enumDat1))
            {
                EnumType = FName.DefineDummy(reader.Asset, enumDat1.Name);
                InnerType = FName.DefineDummy(reader.Asset, enumDat1.InnerType.Type.ToString());
            }

            // fill in data for enumIndice = 0 to provide clarity for end-user
            if (ValidEnumInnerTypeList.Contains(InnerType?.Value?.Value))
            {
                long enumIndice = 0;
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
            }
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        // note: Value must be overridden manually after this is called in cases where serializationContext != PropertySerializationContext.Normal, to ensure not dummy
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
                Value = (asset.HasUnversionedProperties && (ValidEnumInnerTypeList.Contains(InnerType?.Value?.Value))) ? FName.DefineDummy(asset, d[1]) : FName.FromString(asset, d[1]);
            }
            else
            {
                Value = null;
            }
        }

        protected override void HandleCloned(PropertyData res)
        {
            EnumPropertyData cloningProperty = (EnumPropertyData)res;
            cloningProperty.EnumType = (FName)this.EnumType?.Clone();
            cloningProperty.InnerType = (FName)this.InnerType?.Clone();
        }
    }
}