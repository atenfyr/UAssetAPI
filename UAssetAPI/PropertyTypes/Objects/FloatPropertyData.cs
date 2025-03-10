﻿using Newtonsoft.Json;
using System;
using UAssetAPI.JSON;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes an IEEE 32-bit floating point variable (<see cref="float"/>).
    /// </summary>
    public class FloatPropertyData : PropertyData
    {
        /// <summary>
        /// The float that this property represents.
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(FSignedZeroJsonConverter))]
        public float Value;

        public FloatPropertyData(FName name) : base(name)
        {

        }

        public FloatPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("FloatProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }
        public override object DefaultValue { get { return (float)0; } }


        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                this.ReadEndPropertyTag(reader);
            }

            Value = reader.ReadSingle();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                this.WriteEndPropertyTag(writer);
            }

            writer.Write(Value);
            return sizeof(float);
        }

        public override string ToString()
        {
            return Convert.ToString(Value); // maybe: , System.Globalization.CultureInfo.InvariantCulture
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = 0;
            if (float.TryParse(d[0], out float res)) Value = res;
        }
    }
}