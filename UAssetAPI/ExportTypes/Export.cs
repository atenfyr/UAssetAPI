using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.ExportTypes
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class DisplayIndexOrderAttribute : Attribute
    {
        internal int DisplayingIndex = 0;
        internal DisplayIndexOrderAttribute(int displayingIndex)
        {
            DisplayingIndex = displayingIndex;
        }
    }

    /// <summary>
    /// UObject resource type for objects that are contained within this package and can be referenced by other packages.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class Export : FObjectResource, ICloneable
    {
        ///<summary>Location of this export's class (import/other export). 0 = this export is a UClass</summary>
        [DisplayIndexOrder(2)]
        public FPackageIndex ClassIndex;
        ///<summary>Location of this export's parent class (import/other export). 0 = this export is not derived from UStruct</summary>
        [DisplayIndexOrder(3)]
        public FPackageIndex SuperIndex;
        ///<summary>Location of this export's template (import/other export). 0 = there is some problem</summary>
        [DisplayIndexOrder(4)]
        public FPackageIndex TemplateIndex;
        ///<summary>The object flags for the UObject represented by this resource. Only flags that match the RF_Load combination mask will be loaded from disk and applied to the UObject.</summary>
        [DisplayIndexOrder(5)]
        public EObjectFlags ObjectFlags;
        ///<summary>The number of bytes to serialize when saving/loading this export's UObject.</summary>
        [DisplayIndexOrder(6)]
        public long SerialSize;
        ///<summary>The location (into the FLinker's underlying file reader archive) of the beginning of the data for this export's UObject. Used for verification only.</summary>
        [DisplayIndexOrder(7)]
        public long SerialOffset;
        ///<summary>Was this export forced into the export table via OBJECTMARK_ForceTagExp?</summary>
        [DisplayIndexOrder(8)]
        public bool bForcedExport;
        ///<summary>Should this export not be loaded on clients?</summary>
        [DisplayIndexOrder(9)]
        public bool bNotForClient;
        ///<summary>Should this export not be loaded on servers?</summary>
        [DisplayIndexOrder(10)]
        public bool bNotForServer;
        ///<summary>If this object is a top level package (which must have been forced into the export table via OBJECTMARK_ForceTagExp), this is the GUID for the original package file. Deprecated</summary>
        [DisplayIndexOrder(11)]
        public Guid PackageGuid;
        ///<summary>If this export is a top-level package, this is the flags for the original package</summary>
        [DisplayIndexOrder(12)]
        public EPackageFlags PackageFlags;
        ///<summary>Should this export be always loaded in editor game?</summary>
        [DisplayIndexOrder(13)]
        public bool bNotAlwaysLoadedForEditorGame;
        ///<summary>Is this export an asset?</summary>
        [DisplayIndexOrder(14)]
        public bool bIsAsset;

        /// <summary>
        /// The export table must serialize as a fixed size, this is used to index into a long list, which is later loaded into the array. -1 means dependencies are not present. These are contiguous blocks, so CreateBeforeSerializationDependencies starts at FirstExportDependency + SerializationBeforeSerializationDependencies.
        /// </summary>
        internal int FirstExportDependencyOffset;
        internal int SerializationBeforeSerializationDependenciesSize;
        internal int CreateBeforeSerializationDependenciesSize;
        internal int SerializationBeforeCreateDependenciesSize;
        internal int CreateBeforeCreateDependenciesSize;

        [DisplayIndexOrder(15)]
        public List<FPackageIndex> SerializationBeforeSerializationDependencies = new List<FPackageIndex>();
        [DisplayIndexOrder(16)]
        public List<FPackageIndex> CreateBeforeSerializationDependencies = new List<FPackageIndex>();
        [DisplayIndexOrder(17)]
        public List<FPackageIndex> SerializationBeforeCreateDependencies = new List<FPackageIndex>();
        [DisplayIndexOrder(18)]
        public List<FPackageIndex> CreateBeforeCreateDependencies = new List<FPackageIndex>();

        /// <summary>
        /// Miscellaneous, unparsed export data, stored as a byte array.
        /// </summary>
        public byte[] Extras;

        /// <summary>
        /// The asset that this export is parsed with.
        /// </summary>
        [JsonIgnore]
        public UAsset Asset;

        public Export(UAsset asset, byte[] extras)
        {
            Asset = asset;
            Extras = extras;
        }

        public Export()
        {

        }

        public virtual void Read(AssetBinaryReader reader, int nextStarting = 0)
        {

        }

        public virtual void Write(AssetBinaryWriter writer)
        {

        }

        private static FieldInfo[] _allFields = null;
        private static void InitAllFields()
        {
            if (_allFields != null) return;
            _allFields = UAPUtils.GetOrderedFields<Export>();
        }

        public static FieldInfo[] GetAllObjectExportFields()
        {
            InitAllFields();

            return _allFields;
        }

        public static string[] GetAllFieldNames()
        {
            InitAllFields();

            string[] allFieldNames = new string[_allFields.Length];
            for (int i = 0; i < _allFields.Length; i++)
            {
                allFieldNames[i] = _allFields[i].Name;
            }
            return allFieldNames;
        }

        public FName GetExportClassType()
        {
            return this.ClassIndex.IsImport() ? this.ClassIndex.ToImport(Asset).ObjectName : FName.DefineDummy(Asset, this.ClassIndex.Index.ToString());
        }

        public override string ToString()
        {
            InitAllFields();

            var sb = new StringBuilder();
            foreach (var info in _allFields)
            {
                var value = info.GetValue(this) ?? "(null)";
                sb.AppendLine(info.Name + ": " + value.ToString());
            }
            return sb.ToString();
        }

        public object Clone()
        {
            var res = (Export)MemberwiseClone();
            res.SerializationBeforeSerializationDependencies = this.SerializationBeforeSerializationDependencies.ToList();
            res.CreateBeforeSerializationDependencies = this.CreateBeforeSerializationDependencies.ToList();
            res.SerializationBeforeCreateDependencies = this.SerializationBeforeCreateDependencies.ToList();
            res.CreateBeforeCreateDependencies = this.CreateBeforeCreateDependencies.ToList();
            res.Extras = (byte[])this.Extras.Clone();
            res.PackageGuid = new Guid(this.PackageGuid.ToByteArray());
            return res;
        }

        /// <summary>
        /// Creates a child export instance with the same export details as the current export.
        /// </summary>
        /// <typeparam name="T">The type of child export to create.</typeparam>
        /// <returns>An instance of the child export type provided with the export details copied over.</returns>
        public T ConvertToChildExport<T>() where T : Export, new()
        {
            InitAllFields();

            Export res = new T();
            res.SerializationBeforeSerializationDependencies = this.SerializationBeforeSerializationDependencies.ToList();
            res.CreateBeforeSerializationDependencies = this.CreateBeforeSerializationDependencies.ToList();
            res.SerializationBeforeCreateDependencies = this.SerializationBeforeCreateDependencies.ToList();
            res.CreateBeforeCreateDependencies = this.CreateBeforeCreateDependencies.ToList();
            res.Asset = this.Asset;
            res.Extras = this.Extras;
            res.ObjectName = this.ObjectName;
            res.OuterIndex = this.OuterIndex;
            foreach (var info in _allFields)
            {
                info.SetValue(res, info.GetValue(this));
            }
            return (T)res;
        }
    }
}
