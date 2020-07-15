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
            return 1;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
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
            return 2;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
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
            return 4;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
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
            return 8;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
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
            return 2;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
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
            return 4;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
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
            return 8;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }
    }
}
