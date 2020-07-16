using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UAssetAPI.StructureSerializers
{
    /*
        ForceReadNull should pretty much always be set to true for API usage
    */

    public abstract class PropertyData
    {
        public string Name;
        public string Type;
        public AssetReader Asset;
        public object RawValue;
        public bool ForceReadNull = true;

        public void SetObject(object value)
        {
            RawValue = value;
        }

        public T GetObject<T>()
        {
            return (T)RawValue;
        }

        public PropertyData(string name, AssetReader asset, bool forceReadNull)
        {
            Name = name;
            Asset = asset;
            ForceReadNull = forceReadNull;
        }

        public PropertyData()
        {

        }

        public virtual void Read(BinaryReader reader, long leng)
        {

        }

        public virtual int Write(BinaryWriter writer)
        {
            return 0;
        }
    }

    public abstract class PropertyData<T> : PropertyData
    {
        public T Value
        {
            get => GetObject<T>();
            set => SetObject(value);
        }

        public PropertyData(string name, AssetReader asset, bool forceReadNull) : base(name, asset, forceReadNull)
        {

        }

        public PropertyData()
        {

        }
    }

    public class BoolPropertyData : PropertyData<bool>
    {
        public BoolPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "BoolProperty";
        }

        public BoolPropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            Value = reader.ReadInt16() > 0;
        }

        public override int Write(BinaryWriter writer)
        {
            writer.Write((short)(Value ? 1 : 0));
            return 0;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }
    }

    public class FloatPropertyData : PropertyData<float>
    {
        public FloatPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "FloatProperty";
        }

        public FloatPropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadSingle();
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

    public enum TextHistoryType
    {
        None = -1,
        Base = 0,
        NamedFormat,
        OrderedFormat,
        ArgumentFormat,
        AsNumber,
        AsPercent,
        AsCurrency,
        AsDate,
        AsTime,
        AsDateTime,
        Transform,
        StringTableEntry,
        TextGenerator
    }

    public class TextPropertyData : PropertyData<string[]>
    {
        public int Flag;
        public TextHistoryType HistoryType = TextHistoryType.Base;
        public byte[] Extras;

        public TextPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "TextProperty";
        }

        public TextPropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Flag = reader.ReadInt32();
            HistoryType = (TextHistoryType)reader.ReadByte();

            switch(HistoryType)
            {
                case TextHistoryType.None:
                    Extras = new byte[0];
                    Value = null;
                    break;
                case TextHistoryType.Base:
                    Extras = reader.ReadBytes(4);
                    Value = new string[] { reader.ReadUString(), reader.ReadUString() };
                    break;
                case TextHistoryType.StringTableEntry:
                    Extras = reader.ReadBytes(8);
                    Value = new string[] { reader.ReadUString() };
                    break;
                default:
                    throw new FormatException("Unimplemented reader for " + HistoryType.ToString());
            }
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            int here = (int)writer.BaseStream.Position;
            writer.Write(Flag);
            writer.Write((byte)HistoryType);
            writer.Write(Extras);

            switch(HistoryType)
            {
                case TextHistoryType.None:
                    Value = null;
                    break;
                case TextHistoryType.Base:
                    for (int i = 0; i < 2; i++)
                    {
                        writer.WriteUString(Value[i]);
                    }
                    break;
                case TextHistoryType.StringTableEntry:
                    writer.WriteUString(Value[0]);
                    break;
                default:
                    throw new FormatException("Unimplemented writer for " + HistoryType.ToString());
            }

            return (int)writer.BaseStream.Position - here;
        }

        public override string ToString()
        {
            string oup = "";
            for (int i = 0; i < Value.Length; i++)
            {
                oup += Value[i] + " ";
            }
            return oup.TrimEnd(' ');
        }
    }

    public class StrPropertyData : PropertyData<string>
    {
        public StrPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "StrProperty";
        }

        public StrPropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadUString();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            int here = (int)writer.BaseStream.Position;
            writer.WriteUString(Value);
            return (int)writer.BaseStream.Position - here;
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public class ObjectPropertyData : PropertyData<Link>
    {
        public int LinkValue = 0;

        public ObjectPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "ObjectProperty";
        }

        public ObjectPropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            LinkValue = reader.ReadInt32();
            if (LinkValue < 0 && Utils.GetNormalIndex(LinkValue) >= 0)
            {
                Value = Asset.links[Utils.GetNormalIndex(LinkValue)]; // link reference
            }
            else
            {
                Value = null;
            }
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            if (Value != null) LinkValue = Value.Index;
            writer.Write(LinkValue);
            return 4;
        }

        public override string ToString()
        {
            if (LinkValue > 0) return Convert.ToString(LinkValue);
            if (Value == null) return "null";
            return Asset.GetHeaderReference((int)Value.Property);
        }
    }

    public class EnumPropertyData : PropertyData<int>
    {
        public int FullEnum;

        public EnumPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "EnumProperty";
        }

        public EnumPropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            Value = (int)reader.ReadInt64();
            if (ForceReadNull) reader.ReadByte(); // null byte
            FullEnum = (int)reader.ReadInt64();
        }

        public override int Write(BinaryWriter writer)
        {
            writer.Write((long)Value);
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write((long)FullEnum);
            return 8;
        }

        public string DecodeEnumBase()
        {
            return Asset.GetHeaderReference(Value);
        }

        public string DecodeEnum()
        {
            return Asset.GetHeaderReference(FullEnum);
        }

        public override string ToString()
        {
            return DecodeEnum();
        }
    }

    public class BytePropertyData : PropertyData<int>
    {
        public int FullEnum;

        public BytePropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "ByteProperty";
        }

        public BytePropertyData()
        {

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

        public string DecodeEnumBase()
        {
            return Asset.GetHeaderReference(Value);
        }

        public string DecodeEnum()
        {
            return Asset.GetHeaderReference(FullEnum);
        }

        public override string ToString()
        {
            if (DecodeEnumBase() == "None") return Convert.ToString(FullEnum);
            return DecodeEnum();
        }
    }

    public class GuidPropertyData : PropertyData<Guid>
    {
        public GuidPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "Guid";
        }

        public GuidPropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = new Guid(reader.ReadBytes(16));
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write(Value.ToByteArray());
            return 16;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }
    }

    public class LinearColorPropertyData : PropertyData<float[]>
    {
        public LinearColorPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "LinearColor";
        }

        public LinearColorPropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = new float[4];
            for (int i = 0; i < 4; i++)
            {
                Value[i] = reader.ReadSingle();
            }
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            for (int i = 0; i < 4; i++)
            {
                writer.Write(Value[i]);
            }
            return 16;
        }

        public override string ToString()
        {
            string oup = "";
            for (int i = 0; i < Value.Length; i++)
            {
                oup += Convert.ToString(Value[i]) + " ";
            }
            return oup.TrimEnd(' ');
        }
    }

    public class VectorPropertyData : PropertyData<float[]> // X, Y, Z
    {
        public VectorPropertyData(string name, AssetReader asset, bool forceReadNull = false) : base(name, asset, forceReadNull)
        {
            Type = "Vector";
        }

        public VectorPropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = new float[3];
            for (int i = 0; i < 3; i++)
            {
                Value[i] = reader.ReadSingle();
            }
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            for (int i = 0; i < 3; i++)
            {
                writer.Write(Value[i]);
            }
            return 0;
        }

        public override string ToString()
        {
            string oup = "(";
            for (int i = 0; i < Value.Length; i++)
            {
                oup += Convert.ToString(Value[i]) + ", ";
            }
            return oup.Remove(oup.Length - 2) + ")";
        }
    }

    public class RotatorPropertyData : PropertyData<float[]> // Pitch, Yaw, Roll
    {
        public RotatorPropertyData(string name, AssetReader asset, bool forceReadNull = false) : base(name, asset, forceReadNull)
        {
            Type = "Rotator";
        }

        public RotatorPropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = new float[3];
            for (int i = 0; i < 3; i++)
            {
                Value[i] = reader.ReadSingle();
            }
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            for (int i = 0; i < 3; i++)
            {
                writer.Write(Value[i]);
            }
            return 0;
        }

        public override string ToString()
        {
            string oup = "(";
            for (int i = 0; i < Value.Length; i++)
            {
                oup += Convert.ToString(Value[i]) + ", ";
            }
            return oup.Remove(oup.Length - 2) + ")";
        }
    }

    public class MulticastDelegatePropertyData : PropertyData<int[]> // Pitch, Yaw, Roll
    {
        public string Value2;

        public MulticastDelegatePropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "MulticastDelegateProperty";
        }

        public MulticastDelegatePropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = new int[2];
            for (int i = 0; i < 2; i++)
            {
                Value[i] = reader.ReadInt32();
            }
            Value2 = Asset.GetHeaderReference((int)reader.ReadUInt64());
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            for (int i = 0; i < 2; i++)
            {
                writer.Write(Value[i]);
            }
            writer.Write((long)Asset.SearchHeaderReference(Value2));
            return 16;
        }

        public override string ToString()
        {
            string oup = "(";
            for (int i = 0; i < Value.Length; i++)
            {
                oup += Convert.ToString(Value[i]) + ", ";
            }
            oup += Value2;
            return oup + ")";
        }
    }

    public class NamePropertyData : PropertyData<string>
    {
        public NamePropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "NameProperty";
        }

        public NamePropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = Asset.GetHeaderReference((int)reader.ReadInt64());
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write((long)Asset.SearchHeaderReference(Value));
            return 8;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }
    }

    public class ArrayPropertyData : PropertyData<PropertyData[]> // Array
    {
        public string ArrayType;

        public ArrayPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "ArrayProperty";
        }

        public ArrayPropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            ArrayType = Asset.GetHeaderReference((int)reader.ReadInt64());
            if (ForceReadNull) reader.ReadByte(); // null byte
            int numEntries = reader.ReadInt32();
            if (ArrayType == "StructProperty")
            {
                string fullType = "";
                var results = new PropertyData[numEntries];
                for (int i = 0; i < numEntries; i++)
                {
                    if (i > 0) // without name etc.
                    {
                        var data = new StructPropertyData(Name, Asset, true, fullType);
                        data.Read(reader, 0);
                        results[i] = data;
                    }
                    else // with name etc.
                    {
                        results[i] = MainSerializer.Read(Asset, reader, true);
                        fullType = ((StructPropertyData)results[i]).GetStructType();
                    }
                }
                Value = results;
            }
            else
            {
                var results = new PropertyData[numEntries];
                for (int i = 0; i < numEntries; i++)
                {
                    results[i] = MainSerializer.TypeToClass(ArrayType, Name, Asset, reader, 0, false);
                }
                Value = results;
            }
        }

        public override int Write(BinaryWriter writer)
        {
            ArrayType = Value[0].Type;

            writer.Write((long)Asset.SearchHeaderReference(ArrayType));
            if (ForceReadNull) writer.Write((byte)0);

            int here = (int)writer.BaseStream.Position;
            writer.Write(Value.Length);
            if (ArrayType == "StructProperty")
            {
                int lengthLoc = 0;
                for (int i = 0; i < Value.Length; i++)
                {
                    Value[i].ForceReadNull = true;
                    if (i > 0)
                    {
                        ((StructPropertyData)Value[i]).SetForced(((StructPropertyData)Value[0]).GetStructType());
                        Value[i].Write(writer);
                    }
                    else
                    {
                        lengthLoc = MainSerializer.Write(Value[i], Asset, writer);
                    }
                }

                int fullLen = (int)writer.BaseStream.Position - lengthLoc;
                int newLoc = (int)writer.BaseStream.Position;
                writer.Seek(lengthLoc, SeekOrigin.Begin);
                writer.Write(fullLen - 32 - (ForceReadNull ? 1 : 0));
                writer.Seek(newLoc, SeekOrigin.Begin);
            }
            else
            {
                for (int i = 0; i < Value.Length; i++)
                {
                    Value[i].ForceReadNull = false;
                    Value[i].Write(writer);
                }
            }

            return (int)writer.BaseStream.Position - here;
        }
    }

    public class StructPropertyData : PropertyData<IList<PropertyData>> // List
    {
#pragma warning disable IDE0044 // Add readonly modifier
        public bool IsForced = false;
#pragma warning restore IDE0044 // Add readonly modifier
        public string StructType = null;

        public StructPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            IsForced = false;
            Type = "StructProperty";
        }

        public StructPropertyData(string name, AssetReader asset, bool forceReadNull, string forcedType) : base(name, asset, forceReadNull)
        {
            IsForced = true;
            StructType = forcedType;
            Type = "StructProperty";
        }

        public StructPropertyData()
        {

        }

        public string GetStructType()
        {
            return StructType;
        }

        public void SetStructType(string type)
        {
            StructType = type;
        }

        public void SetForced(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                IsForced = false;
                StructType = null;
            }
            else
            {
                IsForced = true;
                StructType = type;
            }
        }

        private void ReadOnce<T>(BinaryReader reader) where T: PropertyData, new()
        {
            T data = (T)Activator.CreateInstance(typeof(T), Name, Asset, false);
            data.Read(reader, 0);
            Value = new List<PropertyData> { data };
        }

        private void ReadNormal(BinaryReader reader)
        {
            IList<PropertyData> resultingList = new List<PropertyData>();
            PropertyData data = null;
            while ((data = MainSerializer.Read(Asset, reader)) != null)
            {
                resultingList.Add(data);
            }

            Value = resultingList;
        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (!IsForced)
            {
                StructType = Asset.GetHeaderReference((int)reader.ReadInt64());
                reader.ReadBytes(17);
            }
            switch (StructType)
            {
                case "Guid": // 16 byte GUID
                    ReadOnce<GuidPropertyData>(reader);
                    break;
                case "LinearColor": // 4 floats
                    ReadOnce<LinearColorPropertyData>(reader);
                    break;
                case "Vector": // 3 floats
                    ReadOnce<VectorPropertyData>(reader);
                    break;
                case "Rotator": // 3 floats
                    ReadOnce<RotatorPropertyData>(reader);
                    break;
                default:
                    ReadNormal(reader);
                    break;
            }
        }

        private void WriteOnce(BinaryWriter writer)
        {
            Value[0].Write(writer);
        }

        private int WriteNormal(BinaryWriter writer)
        {
            int here = (int)writer.BaseStream.Position;
            if (Value != null)
            {
                for (int i = 0; i < Value.Count; i++)
                {
                    MainSerializer.Write(Value[i], Asset, writer);
                }
            }
            writer.Write((long)Asset.SearchHeaderReference("None"));
            return (int)writer.BaseStream.Position - here;
        }

        public override int Write(BinaryWriter writer)
        {
            if (!IsForced)
            {
                writer.Write((long)Asset.SearchHeaderReference(StructType));
                writer.Write(Enumerable.Repeat((byte)0, 17).ToArray());
            }
            switch(StructType)
            {
                case "Guid":
                case "LinearColor":
                    WriteOnce(writer);
                    return 16;
                case "Vector":
                case "Rotator":
                    WriteOnce(writer);
                    return 12;
                default:
                    return WriteNormal(writer);
            }
        }
    }
}
