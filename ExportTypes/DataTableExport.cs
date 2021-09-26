using System.Collections.Generic;
using System.IO;
using UAssetAPI.PropertyTypes;
using UAssetAPI.StructTypes;

namespace UAssetAPI
{
    /// <summary>
    /// An entry in a <see cref="DataTable"/>.
    /// </summary>
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

    /// <summary>
    /// Imported spreadsheet table.
    /// </summary>
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

    /// <summary>
    /// Imported spreadsheet table.
    /// </summary>
    public class DataTableExport : NormalExport
    {
        public DataTable Data2;

        public DataTableExport(Export super) : base(super)
        {

        }

        public DataTableExport(DataTable data, UAsset asset, byte[] extras) : base(asset, extras)
        {
            Data2 = data;
        }

        public DataTableExport()
        {

        }

        public override void Read(BinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);

            // Find an ObjectProperty named RowStruct
            FName decidedStructType = new FName("Generic");
            foreach (PropertyData thisData in Data)
            {
                if (thisData.Name.Value.Value == "RowStruct" && thisData is ObjectPropertyData thisObjData && thisObjData.Value.IsImport())
                {
                    decidedStructType = thisObjData.ToImport().ObjectName;
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
        }

        public override void Write(BinaryWriter writer)
        {
            base.Write(writer);

            // Find an ObjectProperty named RowStruct
            FName decidedStructType = new FName("Generic");
            foreach (PropertyData thisData in Data)
            {
                if (thisData.Name.Value.Value == "RowStruct" && thisData is ObjectPropertyData thisObjData)
                {
                    decidedStructType = thisObjData.ToImport().ObjectName;
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
}
