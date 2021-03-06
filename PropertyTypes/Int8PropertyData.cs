﻿using System;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class Int8PropertyData : PropertyData<sbyte>
    {
        public Int8PropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "Int8Property";
        }

        public Int8PropertyData()
        {
            Type = "Int8Property";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = reader.ReadSByte();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Value);
            return sizeof(sbyte);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = 0;
            if (sbyte.TryParse(d[0], out sbyte res)) Value = res;
        }
    }
}