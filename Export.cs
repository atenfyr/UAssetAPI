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

    public class Export
    {
        public ExportDetails ReferenceData;
        public byte[] Extras;
        public UAsset Asset;

        public Export(ExportDetails reference, UAsset asset, byte[] extras)
        {
            ReferenceData = reference;
            Asset = asset;
            Extras = extras;
        }

        public Export()
        {
            ReferenceData = new ExportDetails();
        }

        public virtual void Read(BinaryReader reader, int nextStarting = 0)
        {

        }

        public virtual void Write(BinaryWriter writer)
        {

        }
    }

    public class NormalExport : Export
    {
        public IList<PropertyData> Data;

        public NormalExport(Export super)
        {
            ReferenceData = super.ReferenceData;
            Asset = super.Asset;
            Extras = super.Extras;
        }

        public NormalExport(ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
        {

        }

        public NormalExport(IList<PropertyData> data, ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
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
            writer.WriteFName(new FName("None"), Asset);
            Write2(writer);
        }

        public virtual void Write2(BinaryWriter writer)
        {

        }
    }

    public class RawExport : Export
    {
        public byte[] Data;

        public RawExport(Export super)
        {
            ReferenceData = super.ReferenceData;
            Asset = super.Asset;
            Extras = super.Extras;
        }

        public RawExport(byte[] data, ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
        {
            Data = data;
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write(Data);
        }
    }

    public class StringTable : List<FString>
    {
        public string Name;

        public StringTable(string name) : base()
        {
            Name = name;
        }
    }

    public class StringTableExport : NormalExport
    {
        public StringTable Data2;

        public StringTableExport(Export super) : base(super)
        {

        }

        public StringTableExport(StringTable data, ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
        {
            Data2 = data;
        }

        public override ZeroPaddingMode Read2(BinaryReader reader, int nextStarting)
        {
            reader.ReadInt32();

            Data2 = new StringTable(reader.ReadFString());

            int numEntries = reader.ReadInt32() * 2;
            for (int i = 0; i < numEntries; i++)
            {
                FString x = reader.ReadFStringWithEncoding();
                Data2.Add(x);
            }
            return ZeroPaddingMode.Unknown;
        }

        public override void Write2(BinaryWriter writer)
        {
            writer.Write((int)0);

            writer.WriteFString(Data2.Name);

            writer.Write(Data2.Count / 2);
            int lenData = (Data2.Count / 2) * 2;
            for (int i = 0; i < lenData; i++)
            {
                writer.WriteFString(Data2[i]);
            }
        }
    }

    public class FunctionData
    {

    }


    public class FunctionCategory : Export
    {

    }


    // Used in BlueprintGeneratedClassExport
    public class FunctionDataEntry
    {
        public FString Name;
        public int Flags;
        public int Category;

        public FunctionDataEntry(FString name, int flags, int category)
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

    public class BlueprintGeneratedClassExport : NormalExport
    {
        public int BaseClass;
        public List<int> IndexData;
        public byte[] DummyInbetweenData; // Usually zeros, sometimes not
        public List<FunctionDataEntry> FunctionData;
        public int FooterSeparator;
        public int FooterObject;
        public FString FooterEngine;
        public byte[] DummyInbetweenData2; // Usually zeros, sometimes not

        public BlueprintGeneratedClassExport(Export super) : base(super)
        {

        }

        public BlueprintGeneratedClassExport(ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
        {

        }

        public override ZeroPaddingMode Read2(BinaryReader reader, int nextStarting)
        {
            // This serialization is not yet fully complete, so if this asset is too old just forget about it
            if (Asset.EngineVersion < UE4Version.VER_UE4_22)
            {
                BaseClass = 0;
                IndexData = new List<int>();
                DummyInbetweenData = new byte[8];
                FunctionData = new List<FunctionDataEntry>();
                FooterSeparator = 0;
                FooterObject = 0;
                DummyInbetweenData2 = new byte[16];
                return ZeroPaddingMode.No;
            }

            reader.ReadInt32();
            BaseClass = reader.ReadInt32();

            int numIndexEntries = reader.ReadInt32();
            IndexData = new List<int>();
            for (int i = 0; i < numIndexEntries; i++)
            {
                IndexData.Add(reader.ReadInt32());
            }

            DummyInbetweenData = reader.ReadBytes(8);

            FunctionData = new List<FunctionDataEntry>();
            int numFuncIndexEntries = reader.ReadInt32();
            for (int i = 0; i < numFuncIndexEntries; i++)
            {
                int functionName = reader.ReadInt32();
                int flags = reader.ReadInt32();
                int functionCategory = reader.ReadInt32();

                FunctionData.Add(new FunctionDataEntry(Asset.GetNameReference(functionName), flags, functionCategory));
            }

            FooterSeparator = reader.ReadInt32(); // usually 10 00 84 00
            FooterObject = reader.ReadInt32();
            int footerEngineRaw = (int)reader.ReadInt32();
            if (footerEngineRaw < 0 || footerEngineRaw > Asset.GetNameMapIndexList().Count)
            {
                FooterEngine = new FString(footerEngineRaw.ToString());
            }
            else
            {
                FooterEngine = Asset.GetNameReference(footerEngineRaw);
            }
            DummyInbetweenData2 = reader.ReadBytes(16); // zeros

            //reader.ReadInt64(); // None

            // Here are a couple weird ints we're ignoring for now

            return ZeroPaddingMode.No;
        }

        public override void Write2(BinaryWriter writer)
        {
            // This serialization is not yet fully complete, so if this asset is too old just forget about it
            if (Asset.EngineVersion < UE4Version.VER_UE4_22)
            {
                return;
            }

            writer.Write((int)0);
            writer.Write(BaseClass);

            writer.Write(IndexData.Count);
            for (int i = 0; i < IndexData.Count; i++)
            {
                writer.Write(IndexData[i]);
            }

            writer.Write(DummyInbetweenData);

            writer.Write(FunctionData.Count);
            for (int i = 0; i < FunctionData.Count; i++)
            {
                writer.Write((int)Asset.SearchNameReference(FunctionData[i].Name));
                writer.Write((int)FunctionData[i].Flags);
                writer.Write((int)FunctionData[i].Category);
            }

            writer.Write(FooterSeparator);
            writer.Write(FooterObject);
            if (Asset.NameReferenceContains(FooterEngine))
            {
                writer.Write((int)Asset.SearchNameReference(FooterEngine));
            }
            else
            {
                writer.Write(int.Parse(FooterEngine.Value));
            }
            writer.Write(DummyInbetweenData2);

            // There is a "None" here, but to allow for writing for different engine versions we leave it as part of the extra data section
            //writer.Write((long)Asset.SearchNameReference("None"));

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

    public class DataTableExport : NormalExport
    {
        public DataTable Data2;

        public DataTableExport(Export super) : base(super)
        {

        }

        public DataTableExport(DataTable data, ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
        {
            Data2 = data;
        }

        public override ZeroPaddingMode Read2(BinaryReader reader, int nextStarting)
        {
            // Find an ObjectProperty named RowStruct
            FName decidedStructType = new FName("Generic");
            foreach (PropertyData thisData in Data)
            {
                if (thisData.Name.Value.Value == "RowStruct" && thisData is ObjectPropertyData thisObjData)
                {
                    decidedStructType = thisObjData.Value.ObjectName;
                    break;
                }
            }

            reader.ReadInt32();

            Data2 = new DataTable();

            int numEntries = reader.ReadInt32();
            for (int i = 0; i < numEntries; i++)
            {
                FString rowName = Asset.GetNameReference(reader.ReadInt32());
                int duplicateIndex = reader.ReadInt32();
                var nextStruct = new StructPropertyData(new FName(rowName), Asset)
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
            FName decidedStructType = new FName("Generic");
            foreach (PropertyData thisData in Data)
            {
                if (thisData.Name.Value.Value == "RowStruct" && thisData is ObjectPropertyData thisObjData)
                {
                    decidedStructType = thisObjData.Value.ObjectName;
                    break;
                }
            }

            writer.Write((int)0);

            writer.Write(Data2.Table.Count);
            for (int i = 0; i < Data2.Table.Count; i++)
            {
                var thisDataTableEntry = Data2.Table[i];
                thisDataTableEntry.Data.StructType = decidedStructType;
                writer.Write((int)Asset.SearchNameReference(thisDataTableEntry.Data.Name.Value));
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

    public class LevelExport : NormalExport
    {
        public List<int> IndexData;
        public NamespacedString LevelType;
        public ulong FlagsProbably;
        public List<int> MiscCategoryData;

        public LevelExport(Export super) : base(super)
        {

        }

        public LevelExport(ExportDetails reference, UAsset asset, byte[] extras) : base(reference, asset, extras)
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

            var nms = reader.ReadFString();
            reader.ReadInt32(); // null
            var val = reader.ReadFString();
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

            writer.WriteFString(LevelType.Namespace);
            writer.Write((int)0);
            writer.WriteFString(LevelType.Value);

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
