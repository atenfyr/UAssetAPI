using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Diagnostics;

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

        public virtual void FromString(string d)
        {

        }

        public virtual void FromString(string d, string d2)
        {
            FromString(d);
        }

        public virtual void FromString(string d, string d2, string d3)
        {
            FromString(d, d2);
        }

        public virtual void FromString(string d, string d2, string d3, string d4)
        {
            FromString(d, d2, d3);
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

        public override void FromString(string d)
        {
            Value = d.Equals("1") || d.ToLower().Equals("true");
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

        public override void FromString(string d)
        {
            Value = 0;
            if (float.TryParse(d, out float res)) Value = res;
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
            HistoryType = (TextHistoryType)reader.ReadSByte();

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
            if (Value == null) return "null";

            string oup = "";
            for (int i = 0; i < Value.Length; i++)
            {
                oup += Value[i] + " ";
            }
            return oup.TrimEnd(' ');
        }

        public override void FromString(string d)
        {
            if (d.Equals("null"))
            {
                HistoryType = TextHistoryType.None;
                Value = null;
                return;
            }

            HistoryType = TextHistoryType.StringTableEntry;
            Value = new string[] { d };
        }

        public override void FromString(string d, string d2)
        {
            HistoryType = TextHistoryType.Base;
            Value = new string[] { d, d2 };
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

        public override void FromString(string d)
        {
            Value = d;
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

        public override void FromString(string d)
        {
            if (int.TryParse(d, out int res))
            {
                LinkValue = res;
                return;
            }

            for (int i = 0; i < Asset.links.Count; i++)
            {
                if (Asset.GetHeaderReference((int)Asset.links[i].Property).Equals(d))
                {
                    Value = Asset.links[i];
                    return;
                }
            }
        }
    }

    public class EnumPropertyData : PropertyData<string>
    {
        public string FullEnum = string.Empty;

        public EnumPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "EnumProperty";
        }

        public EnumPropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            Value = Asset.GetHeaderReference((int)reader.ReadInt64());
            if (Value.Contains("::") || Value.Equals("None")) return;
            if (ForceReadNull) reader.ReadByte(); // null byte
            FullEnum = Asset.GetHeaderReference((int)reader.ReadInt64());
        }

        public override int Write(BinaryWriter writer)
        {
            writer.Write((long)Asset.SearchHeaderReference(Value));
            if (Value.Contains("::") || Value.Equals("None")) return sizeof(long);
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write((long)Asset.SearchHeaderReference(FullEnum));
            return sizeof(long);
        }

        public string GetEnumBase()
        {
            return Value;
        }

        public string GetEnumFull()
        {
            return FullEnum;
        }

        public override string ToString()
        {
            if (Value.Contains("::") || Value.Equals("None")) return Value;
            return FullEnum;
        }

        public override void FromString(string d, string d2)
        {
            Asset.AddHeaderReference(d);
            Asset.AddHeaderReference(d2);
            Value = d;
            FullEnum = d2;
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

        public override void FromString(string d, string d2)
        {
            Asset.AddHeaderReference(d);
            Asset.AddHeaderReference(d2);
            Value = Asset.SearchHeaderReference(d);
            FullEnum = Asset.SearchHeaderReference(d2);
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

        public override void FromString(string d)
        {
            if (Guid.TryParse(d, out Guid res)) Value = res;
        }
    }

    public class LinearColorPropertyData : PropertyData<Color> // R, G, B, A
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
            var data = new float[4];
            for (int i = 0; i < 4; i++)
            {
                data[i] = reader.ReadSingle();
            }
            Value = Color.FromArgb((int)(data[3] * 255), (int)(data[0] * 255), (int)(data[1] * 255), (int)(data[2] * 255));
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write((float)Value.R / 255);
            writer.Write((float)Value.G / 255);
            writer.Write((float)Value.B / 255);
            writer.Write((float)Value.A / 255);
            return 16;
        }

        public override string ToString()
        {
            return Value.ToString();
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

        public override void FromString(string d, string d2, string d3)
        {
            Value = new float[3];
            if (float.TryParse(d, out float res1)) Value[0] = res1;
            if (float.TryParse(d2, out float res2)) Value[1] = res2;
            if (float.TryParse(d2, out float res3)) Value[2] = res3;
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

        public override void FromString(string d, string d2, string d3)
        {
            Value = new float[3];
            if (float.TryParse(d, out float res1)) Value[0] = res1;
            if (float.TryParse(d2, out float res2)) Value[1] = res2;
            if (float.TryParse(d2, out float res3)) Value[2] = res3;
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

    public class QuatPropertyData : PropertyData<float[]>
    {
        public QuatPropertyData(string name, AssetReader asset, bool forceReadNull = false) : base(name, asset, forceReadNull)
        {
            Type = "Quat";
        }

        public QuatPropertyData()
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
            return 0;
        }

        public override void FromString(string d, string d2, string d3, string d4)
        {
            Value = new float[4];
            if (float.TryParse(d, out float res1)) Value[0] = res1;
            if (float.TryParse(d2, out float res2)) Value[1] = res2;
            if (float.TryParse(d2, out float res3)) Value[2] = res3;
            if (float.TryParse(d3, out float res4)) Value[3] = res4;
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

    public class SoftObjectPropertyData : PropertyData<string>
    {
        public long Value2 = 0;

        public SoftObjectPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "SoftObjectProperty";
        }

        public SoftObjectPropertyData()
        {

        }

        public override void Read(BinaryReader reader, long leng)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = Asset.GetHeaderReference(reader.ReadInt32()); // a header reference that isn't a long!? wow!
            Value2 = reader.ReadInt64();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write(Asset.SearchHeaderReference(Value));
            writer.Write(Value2);
            return sizeof(int) + sizeof(long);
        }

        public override string ToString()
        {
            return "(" + Value + ", " + Value2 + ")";
        }

        public override void FromString(string d)
        {
            Asset.AddHeaderReference(d);
            Value = d;
            Value2 = 0;
        }
    }

    public class MulticastDelegatePropertyData : PropertyData<int[]>
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
            return (sizeof(int) * 2) + sizeof(long);
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

        public override void FromString(string d, string d2, string d3)
        {
            Value = new int[] { 0, 0 };
            if (int.TryParse(d, out int res)) Value[0] = res;
            if (int.TryParse(d2, out int res2)) Value[1] = res2;

            Asset.AddHeaderReference(d3);
            Value2 = d3;
        }
    }

    public class NamePropertyData : PropertyData<string>
    {
        public int Value2 = 0;

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
            Value = Asset.GetHeaderReference(reader.ReadInt32());
            Value2 = reader.ReadInt32();
        }

        public override int Write(BinaryWriter writer)
        {
            if (ForceReadNull) writer.Write((byte)0);
            writer.Write((int)Asset.SearchHeaderReference(Value));
            writer.Write(Value2);
            return sizeof(int) * 2;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public override void FromString(string d)
        {
            Value = d;
            Value2 = 0;
        }

        public override void FromString(string d, string d2)
        {
            Value = d;
            Value2 = 0;
            if (int.TryParse(d2, out int res)) Value2 = res;
        }
    }

    public class ArrayPropertyData : PropertyData<PropertyData[]> // Array
    {
        public string ArrayType;
        public StructPropertyData DummyStruct;

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
                var results = new PropertyData[numEntries];
                string name = Asset.GetHeaderReference((int)reader.ReadInt64());
                if (name.Equals("None"))
                {
                    Value = results;
                    return;
                }

                int typeNum = (int)reader.ReadInt64();
                string thisIsStructProperty = name;
                if (typeNum > 0) thisIsStructProperty = Asset.GetHeaderReference(typeNum);
                Debug.Assert(thisIsStructProperty == ArrayType);

                reader.ReadInt64();

                string fullType = Asset.GetHeaderReference((int)reader.ReadInt64());
                Guid structGUID = new Guid(reader.ReadBytes(16));
                reader.ReadByte();

                if (numEntries == 0)
                {
                    DummyStruct = new StructPropertyData(name, Asset, true, fullType)
                    {
                        StructGUID = structGUID
                    };
                }
                else
                {
                    for (int i = 0; i < numEntries; i++)
                    {
                        var data = new StructPropertyData(name, Asset, true, fullType);
                        data.Read(reader, 0);
                        data.StructGUID = structGUID;
                        results[i] = data;
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
            if (Value.Length > 0) ArrayType = Value[0].Type;

            writer.Write((long)Asset.SearchHeaderReference(ArrayType));
            if (ForceReadNull) writer.Write((byte)0);

            int here = (int)writer.BaseStream.Position;
            writer.Write(Value.Length);
            if (ArrayType == "StructProperty")
            {
                StructPropertyData firstElem = Value.Length == 0 ? DummyStruct : (StructPropertyData)Value[0];
                string fullType = firstElem.GetStructType();

                writer.Write((long)Asset.SearchHeaderReference(firstElem.Name));
                writer.Write((long)Asset.SearchHeaderReference("StructProperty"));
                int lengthLoc = (int)writer.BaseStream.Position;
                writer.Write((long)0);
                writer.Write((long)Asset.SearchHeaderReference(fullType));
                writer.Write(firstElem.StructGUID.ToByteArray());
                writer.Write((byte)0);

                for (int i = 0; i < Value.Length; i++)
                {
                    Value[i].ForceReadNull = true;
                    ((StructPropertyData)Value[i]).SetForced(fullType);
                    Value[i].Write(writer);
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

    public class MapPropertyData : PropertyData<OrderedDictionary> // Map
    {
        OrderedDictionary KeysToRemove = null;

        public MapPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "MapProperty";
        }

        public MapPropertyData()
        {
            Type = "MapProperty";
        }

        private PropertyData MapTypeToClass(string type, string name, AssetReader asset, BinaryReader reader, long leng = 0, bool forceReadNull = true)
        {
            switch (type)
            {
                case "StructProperty":
                    StructPropertyData data = new StructPropertyData(name, asset, forceReadNull);
                    data.SetForced("Generic");
                    data.Read(reader, leng);
                    return data;
                default:
                    return MainSerializer.TypeToClass(type, name, asset, reader, leng, forceReadNull);
            }
        }

        private OrderedDictionary ReadRawMap(BinaryReader reader, string type1, string type2, int numEntries)
        {
            var resultingDict = new OrderedDictionary();

            PropertyData data1 = null;
            PropertyData data2 = null;
            for (int i = 0; i < numEntries; i++)
            {
                data1 = MapTypeToClass(type1, Name, Asset, reader, 0, false);
                data2 = MapTypeToClass(type2, Name, Asset, reader, 0, false);

                resultingDict.Add(data1, data2);
            }

            return resultingDict;
        }

        public override void Read(BinaryReader reader, long leng)
        {
            string type1 = Asset.GetHeaderReference((int)reader.ReadInt64());
            string type2 = Asset.GetHeaderReference((int)reader.ReadInt64());

            int numKeysToRemove = reader.ReadInt32();
            if (numKeysToRemove > 0) // i haven't ever actually seen this case but the engine has it so here's an untested implementation of it for now
            {
                KeysToRemove = ReadRawMap(reader, type1, type2, numKeysToRemove);
            }
            if (ForceReadNull) reader.ReadByte();

            int numEntries = reader.ReadInt32();
            Value = ReadRawMap(reader, type1, type2, numEntries);
        }

        private int WriteRawMap(BinaryWriter writer, OrderedDictionary map)
        {
            int here = (int)writer.BaseStream.Position;
            foreach (DictionaryEntry entry in map)
            {
                ((PropertyData)entry.Key).Write(writer);
                ((PropertyData)entry.Value).Write(writer);
            }
            return (int)writer.BaseStream.Position - here;
        }

        public override int Write(BinaryWriter writer)
        {
            var firstEntry = Value.Cast<DictionaryEntry>().ElementAt(0);
            writer.Write((long)Asset.SearchHeaderReference(((PropertyData)firstEntry.Key).Type));
            writer.Write((long)Asset.SearchHeaderReference(((PropertyData)firstEntry.Value).Type));
            writer.Write(KeysToRemove != null ? KeysToRemove.Count : 0);
            if (KeysToRemove != null && KeysToRemove.Count > 0)
            {
                WriteRawMap(writer, KeysToRemove);
            }
            if (ForceReadNull) writer.Write((byte)0);

            writer.Write(Value.Count);
            return WriteRawMap(writer, Value) + 8;
        }
    }

    public class StructPropertyData : PropertyData<IList<PropertyData>> // List
    {
        public bool IsForced = false;
        public string StructType = null;
        public Guid StructGUID = Guid.Empty; // usually set to 0

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
                StructGUID = new Guid(reader.ReadBytes(16));
                reader.ReadByte();
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
                case "Quat": // 4 floats
                    ReadOnce<QuatPropertyData>(reader);
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
                writer.Write(StructGUID.ToByteArray());
                if (ForceReadNull) writer.Write((byte)0);
            }
            switch(StructType)
            {
                case "Guid":
                case "LinearColor":
                case "Quat":
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
