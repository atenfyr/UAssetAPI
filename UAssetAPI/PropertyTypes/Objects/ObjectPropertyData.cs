using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes a reference variable to another object (import/export) which may be null (<see cref="FPackageIndex"/>).
    /// </summary>
    public class ObjectPropertyData : PropertyData<FPackageIndex>
    {
        public ObjectPropertyData(FName name) : base(name)
        {

        }

        public ObjectPropertyData()
        {

        }

        private static readonly FString CurrentPropertyType = new FString("ObjectProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        /// <summary>
        /// Returns true if this ObjectProperty represents an import.
        /// </summary>
        /// <returns>Is this ObjectProperty an import?</returns>
        public bool IsImport()
        {
            return Value.IsImport();
        }

        /// <summary>
        /// Returns true if this ObjectProperty represents an export.
        /// </summary>
        /// <returns>Is this ObjectProperty an export?</returns>
        public bool IsExport()
        {
            return Value.IsExport();
        }

        /// <summary>
        /// Return true if this ObjectProperty represents null (i.e. neither an import nor an export)
        /// </summary>
        /// <returns>Does this ObjectProperty represent null?</returns>
        public bool IsNull()
        {
            return Value.IsNull();
        }

        /// <summary>
        /// Check that this ObjectProperty is an import index and return the corresponding import.
        /// </summary>
        /// <returns>The import that this ObjectProperty represents in the import map.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when this is not an index into the import map.</exception>
        public Import ToImport(UAsset asset)
        {
            return Value.ToImport(asset);
        }

        /// <summary>
        /// Check that this ObjectProperty is an export index and return the corresponding export.
        /// </summary>
        /// <returns>The export that this ObjectProperty represents in the the export map.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when this is not an index into the export map.</exception>
        public Export ToExport(UAsset asset)
        {
            return Value.ToExport(asset);
        }

        public override void Read(AssetBinaryReader reader, FName parentName, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader)
            {
                PropertyGuid = reader.ReadPropertyGuid();
            }

            Value = new FPackageIndex(reader.ReadInt32());
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WritePropertyGuid(PropertyGuid);
            }

            writer.Write(Value.Index);
            return sizeof(int);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override void FromString(string[] d, UAsset asset)
        {
            if (int.TryParse(d[0], out int res))
            {
                Value = new FPackageIndex(res);
                return;
            }
        }
    }
}