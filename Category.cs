using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UAssetAPI.PropertyTypes;
using UAssetAPI.StructTypes;

namespace UAssetAPI
{
    public enum ZeroPaddingMode
    {
        Unknown,
        Yes,
        No
    }

    public class Category
    {
        public CategoryReference ReferenceData;
        public byte[] Extras;
        public AssetReader Asset;

        public Category(CategoryReference reference, AssetReader asset, byte[] extras)
        {
            ReferenceData = reference;
            Asset = asset;
            Extras = extras;
        }

        public Category()
        {
            ReferenceData = new CategoryReference();
        }

        public virtual void Read(BinaryReader reader, int nextStarting = 0)
        {

        }

        public virtual void Write(BinaryWriter writer)
        {

        }
    }

    public class NormalCategory : Category
    {
        public IList<PropertyData> Data;

        public NormalCategory(Category super)
        {
            ReferenceData = super.ReferenceData;
            Asset = super.Asset;
            Extras = super.Extras;
        }

        public NormalCategory(CategoryReference reference, AssetReader asset, byte[] extras) : base(reference, asset, extras)
        {

        }

        public NormalCategory(IList<PropertyData> data, CategoryReference reference, AssetReader asset, byte[] extras) : base(reference, asset, extras)
        {
            Data = data;
        }

        public override void Read(BinaryReader reader, int nextStarting = 0)
        {
            Data = new List<PropertyData>();
            PropertyData bit;
            while ((bit = MainSerializer.Read(Asset, reader, true)) != null)
            {
                Data.Add(bit);
            }

            Read2(reader, nextStarting);
        }

        public virtual ZeroPaddingMode Read2(BinaryReader reader, int nextStarting)
        {
            return ZeroPaddingMode.Unknown;
        }

        public override void Write(BinaryWriter writer)
        {
            for (int j = 0; j < Data.Count; j++)
            {
                PropertyData current = Data[j];
                MainSerializer.Write(current, Asset, writer, true);
            }
            writer.Write((long)Asset.SearchHeaderReference("None"));
            Write2(writer);
        }

        public virtual void Write2(BinaryWriter writer)
        {

        }
    }

    public class RawCategory : Category
    {
        public byte[] Data;

        public RawCategory(Category super)
        {
            ReferenceData = super.ReferenceData;
            Asset = super.Asset;
            Extras = super.Extras;
        }

        public RawCategory(byte[] data, CategoryReference reference, AssetReader asset, byte[] extras) : base(reference, asset, extras)
        {
            Data = data;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Data);
        }
    }

    public class UString
    {
        public string Value;
        public Encoding Encoding;

        public UString(string value, Encoding encoding)
        {
            Value = value;
            Encoding = encoding;
        }

        public UString()
        {

        }
    }

    public class StringTable : List<UString>
    {
        public string Name;

        public StringTable(string name) : base()
        {
            Name = name;
        }
    }

    public class StringTableCategory : NormalCategory
    {
        public StringTable Data2;

        public StringTableCategory(Category super) : base(super)
        {

        }

        public StringTableCategory(StringTable data, CategoryReference reference, AssetReader asset, byte[] extras) : base(reference, asset, extras)
        {
            Data2 = data;
        }

        public override ZeroPaddingMode Read2(BinaryReader reader, int nextStarting)
        {
            reader.ReadInt32();

            Data2 = new StringTable(reader.ReadUString());

            int numEntries = reader.ReadInt32() * 2;
            for (int i = 0; i < numEntries; i++)
            {
                UString x = reader.ReadUStringWithEncoding();
                Data2.Add(x);
            }
            return ZeroPaddingMode.Unknown;
        }

        public override void Write2(BinaryWriter writer)
        {
            writer.Write((int)0);

            writer.WriteUString(Data2.Name);

            writer.Write(Data2.Count / 2);
            int lenData = (Data2.Count / 2) * 2;
            for (int i = 0; i < lenData; i++)
            {
                writer.WriteUString(Data2[i]);
            }
        }
    }

    public class FunctionData
    {

    }


    public class FunctionCategory : Category
    {

    }


    // Used in BlueprintGeneratedClassCategory
    public class FunctionDataEntry
    {
        public string Name;
        public int Flags;
        public int Category;

        public FunctionDataEntry(string name, int flags, int category)
        {
            Name = name;
            Flags = flags;
            Category = category;
        }

        public override string ToString()
        {
            return "(" + Name + ", " + Category + ")";
        }
    }

    public class BlueprintGeneratedClassCategory : NormalCategory
    {
        public int BaseClass;
        public List<int> IndexData;
        public List<FunctionDataEntry> FunctionData;
        public int FooterSeparator;
        public int FooterObject;
        public string FooterEngine;

        public BlueprintGeneratedClassCategory(Category super) : base(super)
        {

        }

        public BlueprintGeneratedClassCategory(CategoryReference reference, AssetReader asset, byte[] extras) : base(reference, asset, extras)
        {

        }

        public override ZeroPaddingMode Read2(BinaryReader reader, int nextStarting)
        {
            reader.ReadInt32();
            BaseClass = reader.ReadInt32();

            int numIndexEntries = reader.ReadInt32();
            IndexData = new List<int>();
            for (int i = 0; i < numIndexEntries; i++)
            {
                IndexData.Add(reader.ReadInt32());
            }

            reader.ReadBytes(8); // 8 zeros

            FunctionData = new List<FunctionDataEntry>();
            int numFuncIndexEntries = reader.ReadInt32();
            for (int i = 0; i < numFuncIndexEntries; i++)
            {
                int functionName = reader.ReadInt32();
                int flags = reader.ReadInt32();
                int functionCategory = reader.ReadInt32();

                FunctionData.Add(new FunctionDataEntry(Asset.GetHeaderReference(functionName), flags, functionCategory));
            }

            FooterSeparator = reader.ReadInt32(); // usually 10 00 84 00
            FooterObject = reader.ReadInt32();
            FooterEngine = Asset.GetHeaderReference((int)reader.ReadInt32());
            reader.ReadBytes(16); // zeros
            reader.ReadInt64(); // None

            // Here are a couple weird ints we're ignoring for now

            return ZeroPaddingMode.No;
        }

        public override void Write2(BinaryWriter writer)
        {
            writer.Write((int)0);
            writer.Write(BaseClass);

            writer.Write(IndexData.Count);
            for (int i = 0; i < IndexData.Count; i++)
            {
                writer.Write(IndexData[i]);
            }

            writer.Write((long)0); // 8 zeros

            writer.Write(FunctionData.Count);
            for (int i = 0; i < FunctionData.Count; i++)
            {
                writer.Write((int)Asset.SearchHeaderReference(FunctionData[i].Name));
                writer.Write((int)FunctionData[i].Flags);
                writer.Write((int)FunctionData[i].Category);
            }

            writer.Write(FooterSeparator);
            writer.Write(FooterObject);
            writer.Write((int)Asset.SearchHeaderReference(FooterEngine));
            writer.Write((long)0); writer.Write((long)0); // 16 zeros
            writer.Write((long)Asset.SearchHeaderReference("None"));

            // Here are a couple weird ints we're ignoring for now
        }
    }

    public struct DataTableEntry
    {
        public StructPropertyData Data;
        public int DuplicateIndex;

        public DataTableEntry(StructPropertyData data, int duplicateIndex)
        {
            Data = data;
            DuplicateIndex = duplicateIndex;
        }
    }


    public class DataTable
    {
        public List<DataTableEntry> Table;

        public DataTable()
        {
            Table = new List<DataTableEntry>();
        }

        public DataTable(List<DataTableEntry> data)
        {
            Table = data;
        }
    }

    public class DataTableCategory : NormalCategory
    {
        public DataTable Data2;

        public DataTableCategory(Category super) : base(super)
        {

        }

        public DataTableCategory(DataTable data, CategoryReference reference, AssetReader asset, byte[] extras) : base(reference, asset, extras)
        {
            Data2 = data;
        }

        public override ZeroPaddingMode Read2(BinaryReader reader, int nextStarting)
        {
            // Find an ObjectProperty named RowStruct
            string decidedStructType = "Generic";
            foreach (PropertyData thisData in Data)
            {
                if (thisData.Name == "RowStruct" && thisData is ObjectPropertyData thisObjData)
                {
                    decidedStructType = Asset.GetHeaderReference((int)thisObjData.Value.Property);
                    break;
                }
            }
            Debug.WriteLine(decidedStructType);

            reader.ReadInt32();

            Data2 = new DataTable();

            int numEntries = reader.ReadInt32();
            for (int i = 0; i < numEntries; i++)
            {
                string rowName = Asset.GetHeaderReference(reader.ReadInt32());
                int duplicateIndex = reader.ReadInt32();
                var nextStruct = new StructPropertyData(rowName, Asset)
                {
                    StructType = decidedStructType
                };
                nextStruct.Read(reader, false, 0);
                Data2.Table.Add(new DataTableEntry(nextStruct, duplicateIndex));
            }
            return ZeroPaddingMode.Unknown;
        }

        public override void Write2(BinaryWriter writer)
        {
            // Find an ObjectProperty named RowStruct
            string decidedStructType = "Generic";
            foreach (PropertyData thisData in Data)
            {
                if (thisData.Name == "RowStruct" && thisData is ObjectPropertyData thisObjData)
                {
                    decidedStructType = Asset.GetHeaderReference((int)thisObjData.Value.Property);
                    break;
                }
            }

            writer.Write((int)0);

            writer.Write(Data2.Table.Count);
            for (int i = 0; i < Data2.Table.Count; i++)
            {
                var thisDataTableEntry = Data2.Table[i];
                thisDataTableEntry.Data.StructType = decidedStructType;
                writer.Write((int)Asset.SearchHeaderReference(thisDataTableEntry.Data.Name));
                writer.Write(thisDataTableEntry.DuplicateIndex);
                thisDataTableEntry.Data.Write(writer, false);
            }
        }
    }

    public class NamespacedString
    {
        public string Namespace;
        public string Value;

        public NamespacedString(string Namespace, string Value)
        {
            this.Namespace = Namespace;
            this.Value = Value;
        }

        public NamespacedString()
        {

        }
    }

    public class LevelCategory : NormalCategory
    {
        public List<int> IndexData;
        public NamespacedString LevelType;
        public ulong FlagsProbably;
        public List<int> MiscCategoryData;

        public LevelCategory(Category super) : base(super)
        {

        }

        public LevelCategory(CategoryReference reference, AssetReader asset, byte[] extras) : base(reference, asset, extras)
        {

        }

        public override ZeroPaddingMode Read2(BinaryReader reader, int nextStarting)
        {
            reader.ReadInt32();
            int numIndexEntries = reader.ReadInt32();

            IndexData = new List<int>();
            for (int i = 0; i < numIndexEntries; i++)
            {
                IndexData.Add(reader.ReadInt32());
            }

            var nms = reader.ReadUString();
            reader.ReadInt32(); // null
            var val = reader.ReadUString();
            LevelType = new NamespacedString(nms, val);

            reader.ReadInt64(); // null
            FlagsProbably = reader.ReadUInt64();

            MiscCategoryData = new List<int>();
            while (reader.BaseStream.Position < nextStarting - 1)
            {
                MiscCategoryData.Add(reader.ReadInt32());
            }

            reader.ReadByte();
            return ZeroPaddingMode.No;
        }

        public override void Write2(BinaryWriter writer)
        {
            writer.Write((int)0);
            writer.Write(IndexData.Count);
            for (int i = 0; i < IndexData.Count; i++)
            {
                writer.Write(IndexData[i]);
            }

            writer.WriteUString(LevelType.Namespace);
            writer.Write((int)0);
            writer.WriteUString(LevelType.Value);

            writer.Write((long)0);
            writer.Write(FlagsProbably);

            for (int i = 0; i < MiscCategoryData.Count; i++)
            {
                writer.Write(MiscCategoryData[i]);
            }

            writer.Write((byte)0);
        }
    }
}
