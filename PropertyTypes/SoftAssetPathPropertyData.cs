﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAssetAPI.PropertyTypes
{
    public class SoftAssetPathPropertyData : PropertyData<string>
    {
        public long Value2 = 0;

        public SoftAssetPathPropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "SoftAssetPath";
        }

        public SoftAssetPathPropertyData()
        {
            Type = "SoftAssetPath";
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng)
        {
            if (includeHeader)
            {
                reader.ReadByte();
            }

            Value = Asset.GetHeaderReference(reader.ReadInt32()); // a header reference that isn't a long!? wow!
            Value2 = reader.ReadInt64();
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write((byte)0);
            }

            writer.Write(Asset.SearchHeaderReference(Value));
            writer.Write(Value2);
            return sizeof(int) + sizeof(long);
        }

        public override string ToString()
        {
            return "(" + Value + ", " + Value2 + ")";
        }

        public override void FromString(string[] d)
        {
            Asset.AddHeaderReference(d[0]);
            Value = d[0];

            if (int.TryParse(d[1], out int res2))
            {
                Value2 = res2;
            }
            else
            {
                Value2 = 0;
            }
        }
    }
}
