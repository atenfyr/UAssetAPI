using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UAssetAPI.StructureSerializers;

namespace UAssetAPI
{
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

        public virtual void Read(BinaryReader reader)
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

        public NormalCategory(IList<PropertyData> data, CategoryReference reference, AssetReader asset, byte[] extras) : base(reference, asset, extras)
        {
            Data = data;
        }

        public override void Read(BinaryReader reader)
        {
            Data = new List<PropertyData>();
            PropertyData bit;
            while ((bit = MainSerializer.Read(Asset, reader)) != null)
            {
                Data.Add(bit);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            for (int j = 0; j < Data.Count; j++)
            {
                PropertyData current = Data[j];
                MainSerializer.Write(current, Asset, writer);
            }
            writer.Write((long)Asset.SearchHeaderReference("None"));
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

    public class StringTable : List<string>
    {
        public string Name;

        public StringTable(string name) : base()
        {
            Name = name;
        }
    }

    public class StringTableCategory : Category
    {
        public StringTable Data;

        public StringTableCategory(Category super)
        {
            ReferenceData = super.ReferenceData;
            Asset = super.Asset;
            Extras = super.Extras;
        }

        public StringTableCategory(StringTable data, CategoryReference reference, AssetReader asset, byte[] extras) : base(reference, asset, extras)
        {
            Data = data;
        }

        public override void Read(BinaryReader reader)
        {
            Debug.Assert(Asset.GetHeaderReference((int)reader.ReadInt64()).Equals("None"));
            reader.ReadInt32();

            Data = new StringTable(reader.ReadUString());

            int numEntries = reader.ReadInt32() * 2;
            for (int i = 0; i < numEntries; i++)
            {
                Data.Add(reader.ReadUString());
            }
        }

        public override void Write(BinaryWriter writer)
        {
            writer.Write((long)Asset.SearchHeaderReference("None"));
            writer.Write((int)0);

            writer.WriteUString(Data.Name);

            writer.Write(Data.Count / 2);
            for (int i = 0; i < Data.Count; i++)
            {
                writer.WriteUString(Data[i]);
            }
        }
    }
}
