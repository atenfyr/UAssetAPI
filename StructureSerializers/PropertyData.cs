using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace UAssetAPI.StructureSerializers
{
    public abstract class PropertyData
    {
        public string Name;
        public AssetReader Asset;
        public object RawValue;
        internal bool ForceReadNull = true;

        public void SetObject(object value)
        {
            RawValue = value;
        }

        public T GetObject<T>()
        {
            return (T)RawValue;
        }

        public PropertyData(string name, AssetReader asset, bool forceReadNull = true)
        {
            Name = name;
            Asset = asset;
            ForceReadNull = forceReadNull;
        }

        public virtual void Read(BinaryReader reader)
        {

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
    }

    public class BoolPropertyData : PropertyData<bool>
    {
        public BoolPropertyData(string name, AssetReader asset, bool forceReadNull) : base(name, asset, forceReadNull)
        {
            
        }

        public override void Read(BinaryReader reader)
        {
            Value = reader.ReadInt16() > 0;
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }
    }

    public class IntPropertyData : PropertyData<int>
    {
        public IntPropertyData(string name, AssetReader asset, bool forceReadNull) : base(name, asset, forceReadNull)
        {

        }

        public override void Read(BinaryReader reader)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadInt32();
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }
    }

    public class FloatPropertyData : PropertyData<float>
    {
        public FloatPropertyData(string name, AssetReader asset, bool forceReadNull) : base(name, asset, forceReadNull)
        {

        }

        public override void Read(BinaryReader reader)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadSingle();
        }

        public override string ToString()
        {
            return Convert.ToString(Value);
        }
    }

    public class TextPropertyData : PropertyData<string>
    {
        public byte[] Garbage1;
        public byte[] Garbage2;

        public TextPropertyData(string name, AssetReader asset, bool forceReadNull) : base(name, asset, forceReadNull)
        {

        }

        public override void Read(BinaryReader reader)
        {
            Garbage1 = reader.ReadBytes(4);
            if (ForceReadNull) reader.ReadByte(); // null byte
            Garbage2 = reader.ReadBytes(9);
            Value = reader.ReadUString();
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public class ObjectPropertyData : PropertyData<int>
    {
        public ObjectPropertyData(string name, AssetReader asset, bool forceReadNull) : base(name, asset, forceReadNull)
        {

        }

        public override void Read(BinaryReader reader)
        {
            if (ForceReadNull) reader.ReadByte(); // null byte
            Value = reader.ReadInt32(); // link reference
        }

        public override string ToString()
        {
            return Asset.GetHeaderReference(Asset.GetLinkReference(Value));
        }
    }

    public class EnumPropertyData : PropertyData<int>
    {
        public int FullEnum;

        public EnumPropertyData(string name, AssetReader asset, bool forceReadNull) : base(name, asset, forceReadNull)
        {

        }

        public override void Read(BinaryReader reader)
        {
            Value = (int)reader.ReadInt64();
            if (ForceReadNull) reader.ReadByte(); // null byte
            FullEnum = (int)reader.ReadInt64();
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

    public class ArrayPropertyData : PropertyData<PropertyData[]> // Array
    {
        public ArrayPropertyData(string name, AssetReader asset, bool forceReadNull) : base(name, asset, forceReadNull)
        {

        }

        public override void Read(BinaryReader reader)
        {
            string arrayType = Asset.GetHeaderReference((int)reader.ReadInt64());
            if (ForceReadNull) reader.ReadByte(); // null byte
            int numEntries = reader.ReadInt32();
            if (arrayType == "StructProperty")
            {
                var results = new PropertyData[numEntries];
                for (int i = 0; i < numEntries; i++)
                {
                    if (i > 0)
                    {
                        var data = new StructPropertyData(Name, Asset, true, arrayType);
                        data.Read(reader);
                        results[i] = data;
                    }
                    else
                    {
                        results[i] = MainSerializer.Read(Asset, reader, true);
                    }
                }
                Value = results;
            }
            else
            {
                var results = new PropertyData[numEntries];
                for (int i = 0; i < numEntries; i++)
                {
                    results[i] = MainSerializer.Read(Asset, reader, false);
                }
                Value = results;
            }
        }
    }

    public class StructPropertyData : PropertyData<IList<PropertyData>> // List
    {
        private string ForcedType = null;

        public StructPropertyData(string name, AssetReader asset, bool forceReadNull, string forcedType = null) : base(name, asset, forceReadNull)
        {
            ForcedType = forcedType;
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

        public override void Read(BinaryReader reader)
        {
            string structType = ForcedType != null ? (string)ForcedType.Clone() : null;
            if (structType == null)
            {
                structType = Asset.GetHeaderReference((int)reader.ReadInt64());
                reader.ReadBytes(17);
            }
            switch (structType)
            {
                default:
                    ReadNormal(reader);
                    break;
            }
        }
    }
}
