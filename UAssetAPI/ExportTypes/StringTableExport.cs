using System.Collections.Generic;
using System.IO;

namespace UAssetAPI
{
    public class StringTable : List<FString>
    {
        public FString Name;

        public StringTable(FString name) : base()
        {
            Name = name;
        }
    }

    /// <summary>
    /// A string table. Holds Key->SourceString pairs of text.
    /// </summary>
    public class StringTableExport : NormalExport
    {
        public StringTable Data2;

        public StringTableExport(Export super) : base(super)
        {

        }

        public StringTableExport(StringTable data, UAsset asset, byte[] extras) : base(asset, extras)
        {
            Data2 = data;
        }

        public StringTableExport()
        {

        }

        public override void Read(AssetBinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);

            reader.ReadInt32();

            Data2 = new StringTable(reader.ReadFString());

            int numEntries = reader.ReadInt32() * 2;
            for (int i = 0; i < numEntries; i++)
            {
                FString x = reader.ReadFString();
                Data2.Add(x);
            }
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);

            writer.Write((int)0);

            writer.Write(Data2.Name);

            writer.Write(Data2.Count / 2);
            int lenData = (Data2.Count / 2) * 2;
            for (int i = 0; i < lenData; i++)
            {
                writer.Write(Data2[i]);
            }
        }
    }
}
