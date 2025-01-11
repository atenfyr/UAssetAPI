using Newtonsoft.Json;
using System;
using UAssetAPI.ExportTypes;
using UAssetAPI.JSON;

namespace UAssetAPI.UnrealTypes
{
    /// <summary>
    /// Wrapper for index into an ImportMap or ExportMap.
    /// 
    /// Values greater than zero indicate that this is an index into the ExportMap.
    /// The actual array index will be (FPackageIndex - 1).
    /// 
    /// Values less than zero indicate that this is an index into the ImportMap.
    /// The actual array index will be (-FPackageIndex - 1)
    /// </summary>
    [JsonConverter(typeof(FPackageIndexJsonConverter))]
    public class FPackageIndex
    {
        /// <summary>
        /// Values greater than zero indicate that this is an index into the ExportMap.
        /// The actual array index will be (FPackageIndex - 1).
        /// 
        /// Values less than zero indicate that this is an index into the ImportMap.
        /// The actual array index will be (-FPackageIndex - 1)
        /// </summary>
        public int Index;

        /// <summary>
        /// Returns an FPackageIndex based off of the index provided. Equivalent to <see cref="FPackageIndex(int)"/>.
        /// </summary>
        /// <param name="index">The index to create a new FPackageIndex with.</param>
        /// <returns>A new FPackageIndex with the index provided.</returns>
        public static FPackageIndex FromRawIndex(int index)
        {
            return new FPackageIndex(index);
        }

        /// <summary>
        /// Returns true if this is an index into the import map.
        /// </summary>
        /// <returns>true if this is an index into the import map, false otherwise</returns>
        public bool IsImport()
        {
            return Index < 0;
        }

        /// <summary>
        /// Returns true if this is an index into the export map.
        /// </summary>
        /// <returns>true if this is an index into the export map, false otherwise</returns>
        public bool IsExport()
        {
            return Index > 0;
        }

        /// <summary>
        /// Return true if this represents null (i.e. neither an import nor an export)
        /// </summary>
        /// <returns>true if this index represents null, false otherwise</returns>
        public bool IsNull()
        {
            return this == null || Index == 0;
        }

        /// <summary>
        /// Creates a FPackageIndex from an index in the import map.
        /// </summary>
        /// <param name="importIndex">An import index to create an FPackageIndex from.</param>
        /// <returns>An FPackageIndex created from the import index.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when the provided import index is less than zero.</exception>
        public static FPackageIndex FromImport(int importIndex)
        {
            if (importIndex < 0) throw new InvalidOperationException("importIndex must be greater than or equal to zero");
            return new FPackageIndex(-importIndex - 1);
        }

        /// <summary>
        /// Creates a FPackageIndex from an index in the export map.
        /// </summary>
        /// <param name="exportIndex">An export index to create an FPackageIndex from.</param>
        /// <returns>An FPackageIndex created from the export index.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when the provided export index is less than zero.</exception>
        public static FPackageIndex FromExport(int exportIndex)
        {
            if (exportIndex < 0) throw new InvalidOperationException("exportIndex must be greater than or equal to zero");
            return new FPackageIndex(exportIndex + 1);
        }

        /// <summary>
        /// Check that this is an import index and return the corresponding import.
        /// </summary>
        /// <param name="asset">The asset that this index is used in.</param>
        /// <returns>The import that this index represents in the import map.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when this is not an index into the import map.</exception>
        public Import ToImport(UAsset asset)
        {
            if (!IsImport()) throw new InvalidOperationException("Index = " + Index + "; cannot call ToImport()");

            int newIndex = -Index - 1;

            if (asset is UAsset uas)
            {
                if (newIndex < 0 || newIndex >= uas.Imports.Count) return null;
                return uas.Imports[newIndex];
            }
            return null;
        }

        /// <summary>
        /// Check that this is an export index and return the corresponding export.
        /// </summary>
        /// <param name="asset">The asset that this index is used in.</param>
        /// <returns>The export that this index represents in the the export map.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when this is not an index into the export map.</exception>
        public Export ToExport(UAsset asset)
        {
            if (!IsExport() || Index > asset.Exports.Count) throw new InvalidOperationException("Index = " + Index + "; cannot call ToExport()");
            return asset.Exports[Index - 1];
        }

        public T ToExport<T>(UAsset asset) where T : Export
        {
            if (!IsExport() || Index > asset.Exports.Count) throw new InvalidOperationException("Index = " + Index + "; cannot call ToExport()");
            return (T)asset.Exports[Index-1];
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FPackageIndex comparingPackageIndex)) return false;
            return comparingPackageIndex.Index == this.Index;
        }

        public static bool operator <(FPackageIndex first, FPackageIndex second)
        {
            return first.Index < second.Index;
        }

        public static bool operator >(FPackageIndex first, FPackageIndex second)
        {
            return first.Index > second.Index;
        }

        public static bool operator <=(FPackageIndex first, FPackageIndex second)
        {
            return first.Index <= second.Index;
        }

        public static bool operator >=(FPackageIndex first, FPackageIndex second)
        {
            return first.Index >= second.Index;
        }

        public override int GetHashCode()
        {
            return Index.GetHashCode();
        }

        public override string ToString()
        {
            return Index.ToString();
        }

        public FPackageIndex(int index = 0)
        {
            Index = index;
        }

        public FPackageIndex(AssetBinaryReader reader)
        {
            Index = reader.ReadInt32();
        }

        public int Write(AssetBinaryWriter writer)
        {
            writer.Write(Index);
            return sizeof(int);
        }
    }
}
