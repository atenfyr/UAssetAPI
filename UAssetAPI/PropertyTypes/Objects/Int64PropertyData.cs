﻿using System;
using System.IO;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes a 64-bit signed integer variable (<see cref="long"/>).
    /// </summary>
    public class Int64PropertyData : PropertyData<long>
    {
        public Int64PropertyData(FName name) : base(name)
        {

        }

        public Int64PropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("Int64Property");
        public override FString PropertyType { get { return CurrentPropertyType; } }
        public override object DefaultValue { get { return (long)0; } }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                this.ReadEndPropertyTag(reader);
            }

            Value = reader.ReadInt64();
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader)
            {
                this.WriteEndPropertyTag(writer);
            }

            writer.Write(Value);
            return sizeof(long);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            Value = 0;
            if (long.TryParse(d[0], out long res)) Value = res;
        }
    }
}