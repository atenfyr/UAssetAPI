using System.Collections.Generic;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.PropertyTypes.Structs;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;
using System.Reflection.PortableExecutable;

namespace UAssetAPI.ExportTypes
{
    /// <summary>
    /// Imported spreadsheet table.
    /// </summary>
    public class UDataTable
    {
        public List<StructPropertyData> Data;

        public UDataTable()
        {
            Data = new List<StructPropertyData>();
        }

        public UDataTable(List<StructPropertyData> data)
        {
            Data = data;
        }
    }

    /// <summary>
    /// Export for an imported spreadsheet table. See <see cref="UDataTable"/>.
    /// </summary>
    public class DataTableExport : NormalExport
    {
        /// <summary>
        /// Gets or sets the value associated with the specified key. This operation loops linearly, so it may not be suitable for high-performance environments.
        /// </summary>
        /// <param name="key">The key associated with the value to get or set.</param>
        public override PropertyData this[FName key]
        {
            get
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    if (Data[i].Name == key) return Data[i];
                }
                for (int i = 0; i < Table.Data.Count; i++)
                {
                    if (Table.Data[i].Name == key) return Table.Data[i];
                }
                return null;
            }
            set
            {
                value.Name = key;

                for (int i = 0; i < Data.Count; i++)
                {
                    if (Data[i].Name == key)
                    {
                        Data[i] = value;
                        return;
                    }
                }

                if (value is StructPropertyData)
                {
                    for (int i = 0; i < Table.Data.Count; i++)
                    {
                        if (Table.Data[i].Name == key)
                        {
                            Table.Data[i] = (StructPropertyData)value;
                            return;
                        }
                    }

                    Table.Data.Add((StructPropertyData)value);
                }
                else
                {
                    Data.Add(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key. This operation loops linearly, so it may not be suitable for high-performance environments.
        /// </summary>
        /// <param name="key">The key associated with the value to get or set.</param>
        public override PropertyData this[string key]
        {
            get
            {
                return this[FName.FromString(Asset, key)];
            }
            set
            {
                this[FName.FromString(Asset, key)] = value;
            }
        }

        public UDataTable Table;

        public DataTableExport(Export super) : base(super)
        {

        }

        public DataTableExport(UDataTable data, UAsset asset, byte[] extras) : base(asset, extras)
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
            FName decidedStructType = FName.DefineDummy(reader.Asset, "Generic");
            foreach (PropertyData thisData in Data)
            {
                if (thisData.Name.Value.Value == "RowStruct" && thisData is ObjectPropertyData thisObjData && thisObjData.Value.IsImport())
                {
                    decidedStructType = thisObjData.ToImport(reader.Asset).ObjectName;
                    break;
                }
            }

            if (decidedStructType.ToString() == "Generic")
            {
                // overrides here...
                FName exportClassTypeName = this.GetExportClassType();
                string exportClassType = exportClassTypeName.Value.Value;
                switch(exportClassType)
                {
                    case "CommonGenericInputActionDataTable":
                        decidedStructType = FName.DefineDummy(reader.Asset, "CommonInputActionDataBase");
                        break;
                }
            }

            Table = new UDataTable();

            int numEntries = reader.ReadInt32();
            FName pcen = reader.Asset.GetParentClassExportName(out FName pcen2);
            for (int i = 0; i < numEntries; i++)
            {
                FName rowName = reader.ReadFName();
                var nextStruct = new StructPropertyData(rowName)
                {
                    StructType = decidedStructType
                };
                nextStruct.Ancestry.Initialize(null, pcen, pcen2);
                nextStruct.Read(reader, false, 1);
                Table.Data.Add(nextStruct);
            }
        }

        public override void ResolveAncestries(UAsset asset, AncestryInfo ancestrySoFar)
        {
            var ancestryNew = (AncestryInfo)ancestrySoFar.Clone();
            FName pcen = asset.GetParentClassExportName(out FName pcen2);
            ancestryNew.SetAsParent(pcen, pcen2);

            if (Data != null)
            {
                for (int i = 0; i < Data.Count; i++) Data[i]?.ResolveAncestries(asset, ancestryNew);
            }
            if (Table?.Data != null)
            {
                for (int i = 0; i < Table.Data.Count; i++) Table.Data[i]?.ResolveAncestries(asset, ancestryNew);
            }
            base.ResolveAncestries(asset, ancestrySoFar);
        }

        public override void Write(AssetBinaryWriter writer)
        {
            base.Write(writer);

            // Find an ObjectProperty named RowStruct
            FName decidedStructType = FName.DefineDummy(writer.Asset, "Generic");
            foreach (PropertyData thisData in Data)
            {
                if (thisData.Name.Value.Value == "RowStruct" && thisData is ObjectPropertyData thisObjData)
                {
                    decidedStructType = thisObjData.ToImport(writer.Asset).ObjectName;
                    break;
                }
            }

            if (decidedStructType.ToString() == "Generic")
            {
                // overrides here...
                FName exportClassTypeName = this.GetExportClassType();
                string exportClassType = exportClassTypeName.Value.Value;
                switch (exportClassType)
                {
                    case "CommonGenericInputActionDataTable":
                        decidedStructType = FName.DefineDummy(writer.Asset, "CommonInputActionDataBase");
                        break;
                }
            }

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
