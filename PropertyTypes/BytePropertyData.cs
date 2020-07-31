using System;
using System.Diagnostics;
using System.IO;

namespace UAssetAPI.PropertyTypes
{
    public class BytePropertyData : PropertyData<int>
    {
        public int FullEnum;

        public BytePropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "ByteProperty";
        }

        public BytePropertyData()
        {
            Type = "ByteProperty";
        }

        public override void Read(BinaryReader reader, long leng)
        {
            Value = (int)reader.ReadInt64();
            if (ForceReadNull) reader.ReadByte(); // null byte
            if (Asset.GetHeaderReference(Value) == "None")
            {
                FullEnum = (int)reader.ReadByte();
                return;
            }
            FullEnum = (int)reader.ReadInt64();
        }

        public override void ReadInArray(BinaryReader reader, long leng)
        {
            ReadInMap(reader, leng);
        }

        public override void ReadInMap(BinaryReader reader, long leng)
        {
            Value = (int)reader.ReadInt64();
            if (ForceReadNull) reader.ReadByte();
            FullEnum = 0;
        }

        public override int Write(BinaryWriter writer)
        {
            writer.Write((long)Value);
            if (ForceReadNull) writer.Write((byte)0);
            if (Asset.GetHeaderReference(Value) == "None")
            {
                writer.Write((byte)FullEnum);
                return 1;
            }
            writer.Write((long)FullEnum);
            return 8;
        }

        public override int WriteInArray(BinaryWriter writer)
        {
            return WriteInMap(writer);
        }

        public override int WriteInMap(BinaryWriter writer)
        {
            writer.Write((long)Value);
            if (ForceReadNull) writer.Write((byte)0);
            return 0;
        }

        public string GetEnumBase()
        {
            return Asset.GetHeaderReference(Value);
        }

        public string GetEnumFull()
        {
            return Asset.GetHeaderReference(FullEnum);
        }

        public override string ToString()
        {
            if (GetEnumBase() == "None") return Convert.ToString(FullEnum);
            return GetEnumFull();
        }

        public override void FromString(string[] d)
        {
            Value = Asset.AddHeaderReference(d[0]);
            if (d[0].Equals("None"))
            {
                if (byte.TryParse(d[1], out byte res)) FullEnum = res;
            }
            else
            {
                FullEnum = Asset.AddHeaderReference(d[1]);
            }
        }
    }
}