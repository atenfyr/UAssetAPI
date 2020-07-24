using System;
using System.IO;

namespace UAssetAPI.StructureSerializers
{
    public class Int8PropertyData : PropertyData<sbyte>
    {
        public Int8PropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "Int8Property";
        }

        public Int8PropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadSByte();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
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

    public class Int16PropertyData : PropertyData<short>
    {
        public Int16PropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "Int16Property";
        }

        public Int16PropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadInt16();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write(Value);
            return sizeof(short);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = 0;
            if (short.TryParse(d[0], out short res)) Value = res;
        }
    }

    public class IntPropertyData : PropertyData<int>
    {
        public IntPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "IntProperty";
        }

        public IntPropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadInt32();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write(Value);
            return sizeof(int);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = 0;
            if (int.TryParse(d[0], out int res)) Value = res;
        }
    }

    public class Int64PropertyData : PropertyData<long>
    {
        public Int64PropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "Int64Property";
        }

        public Int64PropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadInt64();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write(Value);
            return sizeof(long);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = 0;
            if (long.TryParse(d[0], out long res)) Value = res;
        }
    }

    public class UInt16PropertyData : PropertyData<ushort>
    {
        public UInt16PropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "UInt16Property";
        }

        public UInt16PropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadUInt16();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write(Value);
            return sizeof(ushort);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = 0;
            if (ushort.TryParse(d[0], out ushort res)) Value = res;
        }
    }

    public class UInt32PropertyData : PropertyData<uint>
    {
        public UInt32PropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "UInt32Property";
        }

        public UInt32PropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadUInt32();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write(Value);
            return sizeof(uint);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = 0;
            if (uint.TryParse(d[0], out uint res)) Value = res;
        }
    }

    public class UInt64PropertyData : PropertyData<ulong>
    {
        public UInt64PropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "UInt64Property";
        }

        public UInt64PropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadUInt64();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write(Value);
            return sizeof(ulong);
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string[] d)
        {
            Value = 0;
            if (ulong.TryParse(d[0], out ulong res)) Value = res;
        }
    }
}
