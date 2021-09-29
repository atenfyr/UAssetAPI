using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UAssetAPI.PropertyTypes;
using UAssetAPI.StructTypes;

namespace UAssetAPI
{
    /// <summary>
    /// Imported spreadsheet table.
    /// </summary>
    public class DataTable
    {
        public List<StructPropertyData> Data;

        public DataTable()
        {
            Data = new List<StructPropertyData>();
        }

        public DataTable(List<StructPropertyData> data)
        {
            Data = data;
        }
    }

    /// <summary>
    /// Imported spreadsheet table.
    /// </summary>
    public class DataTableExport : NormalExport
    {
        public DataTable Table;

        public DataTableExport(Export super) : base(super)
        {

        }

        public DataTableExport(DataTable data, UAsset asset, byte[] extras) : base(asset, extras)
        {
            Table = data;
        }

        public DataTableExport()
        {

        }

        public override void Read(AssetBinaryReader reader, int nextStarting)
        {
            base.Read(reader, nextStarting);

            // Find an ObjectProperty named RowStruct
            FName decidedStructType = new FName("Generic");
            foreach (PropertyData thisData in Data)
            {
                if (thisData.Name.Value.Value == "RowStruct" && thisData is ObjectPropertyData thisObjData && thisObjData.Value.IsImport())
                {
                    decidedStructType = thisObjData.ToImport(reader.Asset).ObjectName;
                    break;
                }
            }

            reader.ReadInt32();

            Table = new DataTable();

            int numEntries = reader.ReadInt32();
            for (int i = 0; i < numEntries; i++)
            {
                FName rowName = reader.ReadFName();
                var nextStruct = new StructPropertyData(rowName)
                {
                    StructType = decidedStructType
                };
                nextStruct.Read(reader, false, 1);
                Table.Data.Add(nextStruct);
            }
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);

            // Find an ObjectProperty named RowStruct
            FName decidedStructType = new FName("Generic");
            foreach (PropertyData thisData in Data)
            {
                if (thisData.Name.Value.Value == "RowStruct" && thisData is ObjectPropertyData thisObjData)
                {
                    decidedStructType = thisObjData.ToImport(writer.Asset).ObjectName;
                    break;
                }
            }

            writer.Write((int)0);

            writer.Write(Table.Data.Count);
            for (int i = 0; i < Table.Data.Count; i++)
            {
                var thisDataTableEntry = Table.Data[i];
                thisDataTableEntry.StructType = decidedStructType;
                writer.Write(thisDataTableEntry.Name);
                thisDataTableEntry.Write(writer, false);
            }
        }
    }
}
