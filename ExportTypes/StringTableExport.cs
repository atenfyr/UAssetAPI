using System.Collections.Generic;
using System.IO;

namespace UAssetAPI
{
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

        public override void Read(BinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);

            reader.ReadInt32();

            Data2 = new StringTable(reader.ReadFString());

            int numEntries = reader.ReadInt32() * 2;
            for (int i = 0; i < numEntries; i++)
            {
                FString x = reader.ReadFStringWithEncoding();
                Data2.Add(x);
            }
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);

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
}
