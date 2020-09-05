using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UAssetAPI.PropertyTypes;

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

    public class BPPortion
    {

    }

    public class BPSeparator : BPPortion
    {
        public BPSeparator()
        {

        }

        public override string ToString()
        {
            return "()";
        }
    }

    public class BPPair : BPPortion
    {
        public string Key;
        public string Value;

        public BPPair(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return "(" + Key + ", " + Value + ")";
        }
    }

    public class BlueprintGeneratedClassCategory : NormalCategory
    {
        public int BaseClass;
        public List<int> IndexData;
        public List<BPPortion> Data2;

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

            Data2 = new List<BPPortion>();
            int noneRef = Asset.SearchHeaderReference("None");
            while (true)
            {
                int firstBit = reader.ReadInt32();
                if (firstBit == 0)
                {
                    Data2.Add(new BPSeparator());
                    continue;
                }
                if (firstBit == noneRef) break;

                int secondBit = reader.ReadInt32();

                string k = Convert.ToString(firstBit);
                if (firstBit >= 0)
                {
                    k = Asset.GetHeaderReference(firstBit);
                }

                string v = Convert.ToString(secondBit);
                if (secondBit >= 0)
                {
                    v = Asset.GetHeaderReference(secondBit);
                }

                Data2.Add(new BPPair(k, v));
            }

            reader.ReadBytes(4);
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

            for (int i = 0; i < Data2.Count; i++)
            {
                if (Data2[i] is BPSeparator)
                {
                    writer.Write((int)0);
                }
                else if (Data2[i] is BPPair us)
                {
                    int.TryParse(us.Key, out int part1);
                    if (part1 == 0 && Asset.HeaderReferenceContains(us.Key)) part1 = Asset.SearchHeaderReference(us.Key);
                    int.TryParse(us.Value, out int part2);
                    if (part2 == 0 && Asset.HeaderReferenceContains(us.Value)) part2 = Asset.SearchHeaderReference(us.Value);

                    writer.Write(part1);
                    writer.Write(part2);
                }
            }

            writer.Write((long)Asset.SearchHeaderReference("None"));
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
