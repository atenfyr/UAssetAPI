using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAssetAPI
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
        /// Returns true if this is an index into the import map.
        /// </summary>
        /// <returns>Is this an index into the import map?</returns>
        public bool IsImport()
        {
            return Index < 0;
        }

        /// <summary>
        /// Returns true if this is an index into the export map.
        /// </summary>
        /// <returns>Is this an index into the export map?</returns>
        public bool IsExport()
        {
            return Index > 0;
        }

        /// <summary>
        /// Return true if this is null (i.e. neither an import nor an export)
        /// </summary>
        /// <returns>Does this index represent null?</returns>
        public bool IsNull()
        {
            return Index == 0;
        }

        /// <summary>
        /// Creates a FPackageIndex from an import index.
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
        /// Creates a FPackageIndex from an export index.
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
            return asset.Imports[-Index - 1];
        }

        /// <summary>
        /// Check that this is an export index and return the corresponding export.
        /// </summary>
        /// <param name="asset">The asset that this index is used in.</param>
        /// <returns>The export that this index represents in the the export map.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when this is not an index into the export map.</exception>
        public Export ToExport(UAsset asset)
        {
            if (!IsExport()) throw new InvalidOperationException("Index = " + Index + "; cannot call ToExport()");
            return asset.Exports[Index - 1];
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
    }
}
