using System.Collections.Generic;
using UAssetAPI.PropertyTypes;
using UAssetAPI.StructTypes;

namespace UAssetAPI
{
    /// <summary>
    /// Imported spreadsheet table.
    /// </summary>
    public class UImalVersionizedDataTable
    {
        public List<StructPropertyData> Data;

        public UImalVersionizedDataTable()
        {
            Data = new List<StructPropertyData>();
        }

        public UImalVersionizedDataTable(List<StructPropertyData> data)
        {
            Data = data;
        }
    }

    /// <summary>
    /// Export for an imported spreadsheet table. See <see cref="UImalVersionizedDataTable"/>.
    /// </summary>
    public class ImalVersionizedDataTableExport : NormalExport
    {
        public UImalVersionizedDataTable Table;

        public ImalVersionizedDataTableExport(Export super) : base(super)
        {

        }

        public ImalVersionizedDataTableExport(UImalVersionizedDataTable data, UAsset asset, byte[] extras) : base(asset, extras)
        {
            Table = data;
        }

        public ImalVersionizedDataTableExport()
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

            Table = new UImalVersionizedDataTable();

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
