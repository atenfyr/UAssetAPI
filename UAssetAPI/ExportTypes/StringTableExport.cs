using System.Linq;

namespace UAssetAPI
{
    /// <summary>
    /// A string table. Holds Key->SourceString pairs of text.
    /// </summary>
    public class FStringTable : TMap<FString, FString>
    {
        public FString TableNamespace;

        public FStringTable(FString tableNamespace) : base()
        {
            TableNamespace = tableNamespace;
        }
    }

    /// <summary>
    /// An export that stores a string table. Holds Key->SourceString pairs of text.
    /// </summary>
    public class StringTableExport : NormalExport
    {
        public FStringTable Data2;

        public StringTableExport(Export super) : base(super)
        {

        }

        public StringTableExport(FStringTable data, UAsset asset, byte[] extras) : base(asset, extras)
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

            Data2 = new FStringTable(reader.ReadFString());

            int numEntries = reader.ReadInt32();
            for (int i = 0; i < numEntries; i++)
            {
                Data2.Add(reader.ReadFString(), reader.ReadFString());
            }
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);
            writer.Write((int)0);

            writer.Write(Data2.TableNamespace);
            writer.Write(Data2.Count);
            for (int i = 0; i < Data2.Count; i++)
            {
                writer.Write(Data2.Keys.ElementAt(i));
                writer.Write(Data2[i]);
            }
        }
    }
}
