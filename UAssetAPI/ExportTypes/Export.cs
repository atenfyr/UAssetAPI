using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.ExportTypes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    internal class DisplayIndexOrderAttribute : Attribute
    {
        internal int DisplayingIndex = 0;
        internal bool IsIoStore = false;
        internal bool IsTraditional = true;
        internal DisplayIndexOrderAttribute(int displayingIndex, bool isIoStore = false, bool isTraditional = true)
        {
            DisplayingIndex = displayingIndex;
            IsIoStore = isIoStore;
            IsTraditional = isTraditional;
        }
    }

    /// <summary>
    /// Enum used to determine whether an export should be loaded or not on the client/server. Not actually a bitflag.
    /// </summary>
    public enum EExportFilterFlags : byte
    {
        None,
	    NotForClient,
	    NotForServer
    }

    /// <summary>
    /// UObject resource type for objects that are contained within this package and can be referenced by other packages.
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class Export : ICloneable
    {
        ///<summary>The name of the UObject represented by this resource.</summary>
        [DisplayIndexOrder(0, true)]
        public FName ObjectName;
        ///<summary>Location of the resource for this resource's Outer (import/other export). 0 = this resource is a top-level UPackage</summary>
        [DisplayIndexOrder(1)]
        public FPackageIndex OuterIndex;
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
        [DisplayIndexOrder(5, true)]
        public EObjectFlags ObjectFlags;
        ///<summary>The number of bytes to serialize when saving/loading this export's UObject.</summary>
        [DisplayIndexOrder(6, true)]
        public long SerialSize;
        ///<summary>The location (into the FLinker's underlying file reader archive) of the beginning of the data for this export's UObject. Used for verification only.</summary>
        [DisplayIndexOrder(7, true)]
        public long SerialOffset;

        /// <summary>
        /// The location (relative to SerialOffset) of the beginning of the portion of this export's data that is serialized using tagged property serialization.
        /// Serialized into packages using tagged property serialization as of <see cref="ObjectVersionUE5.SCRIPT_SERIALIZATION_OFFSET"/> (5.4).
        /// </summary>
        [DisplayIndexOrder(8)]
        public long ScriptSerializationStartOffset;
        /// <summary>
        /// The location (relative to SerialOffset) of the end of the portion of this export's data that is serialized using tagged property serialization.
        /// Serialized into packages using tagged property serialization as of <see cref="ObjectVersionUE5.SCRIPT_SERIALIZATION_OFFSET"/> (5.4)
        /// </summary>
        [DisplayIndexOrder(9)]
        public long ScriptSerializationEndOffset;

        ///<summary>Was this export forced into the export table via OBJECTMARK_ForceTagExp?</summary>
        [DisplayIndexOrder(10)]
        public bool bForcedExport;
        ///<summary>Should this export not be loaded on clients?</summary>
        [DisplayIndexOrder(11, true)]
        public bool bNotForClient;
        ///<summary>Should this export not be loaded on servers?</summary>
        [DisplayIndexOrder(12, true)]
        public bool bNotForServer;
        ///<summary>If this object is a top level package (which must have been forced into the export table via OBJECTMARK_ForceTagExp), this is the GUID for the original package file. Deprecated</summary>
        [DisplayIndexOrder(13)]
        public Guid PackageGuid;
        ///<summary></summary>
        [DisplayIndexOrder(14)]
        public bool IsInheritedInstance;
        ///<summary>If this export is a top-level package, this is the flags for the original package</summary>
        [DisplayIndexOrder(15)]
        public EPackageFlags PackageFlags;
        ///<summary>Should this export be always loaded in editor game?</summary>
        [DisplayIndexOrder(16)]
        public bool bNotAlwaysLoadedForEditorGame;
        ///<summary>Is this export an asset?</summary>
        [DisplayIndexOrder(17)]
        public bool bIsAsset;
        ///<summary></summary>
        [DisplayIndexOrder(18)]
        public bool GeneratePublicHash;

        /// <summary>
        /// The export table must serialize as a fixed size, this is used to index into a long list, which is later loaded into the array. -1 means dependencies are not present. These are contiguous blocks, so CreateBeforeSerializationDependencies starts at FirstExportDependency + SerializationBeforeSerializationDependencies.
        /// </summary>
        internal int FirstExportDependencyOffset;
        internal int SerializationBeforeSerializationDependenciesSize;
        internal int CreateBeforeSerializationDependenciesSize;
        internal int SerializationBeforeCreateDependenciesSize;
        internal int CreateBeforeCreateDependenciesSize;

        [DisplayIndexOrder(19)]
        public List<FPackageIndex> SerializationBeforeSerializationDependencies = new List<FPackageIndex>();
        [DisplayIndexOrder(20)]
        public List<FPackageIndex> CreateBeforeSerializationDependencies = new List<FPackageIndex>();
        [DisplayIndexOrder(21)]
        public List<FPackageIndex> SerializationBeforeCreateDependencies = new List<FPackageIndex>();
        [DisplayIndexOrder(22)]
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

        [JsonIgnore]
        internal bool alreadySerialized = false;

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

        /// <summary>
        /// Resolves the ancestry of all child properties of this export.
        /// </summary>
        public virtual void ResolveAncestries(UAsset asset, AncestryInfo ancestrySoFar)
        {

        }

        public virtual void Write(AssetBinaryWriter writer)
        {

        }

        // https://github.com/EpicGames/UnrealEngine/commit/1952a8b65290bc5b492f87d57fce87c809e231a0
        // this commit doesn't seem to actually make any changes to how bools are serialized
        private bool ReadBit(AssetBinaryReader reader)
        {
            return reader.ReadInt32() == 1;
        }

        private void WriteBit(AssetBinaryWriter writer, bool b)
        {
            writer.Write(b ? 1 : 0);
        }

        public void ReadExportMapEntry(AssetBinaryReader reader)
        {
            Asset = reader.Asset;

            this.ClassIndex = new FPackageIndex(reader.ReadInt32());
            this.SuperIndex = new FPackageIndex(reader.ReadInt32());
            if (Asset.ObjectVersion >= ObjectVersion.VER_UE4_TemplateIndex_IN_COOKED_EXPORTS)
            {
                this.TemplateIndex = new FPackageIndex(reader.ReadInt32());
            }
            this.OuterIndex = new FPackageIndex(reader.ReadInt32());
            this.ObjectName = reader.ReadFName();
            this.ObjectFlags = (EObjectFlags)reader.ReadUInt32();
            if (Asset.ObjectVersion < ObjectVersion.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)
            {
                this.SerialSize = reader.ReadInt32();
                this.SerialOffset = reader.ReadInt32();
            }
            else
            {
                this.SerialSize = reader.ReadInt64();
                this.SerialOffset = reader.ReadInt64();
            }
            this.bForcedExport = ReadBit(reader);
            this.bNotForClient = ReadBit(reader);
            this.bNotForServer = ReadBit(reader);
            if (Asset.ObjectVersionUE5 < ObjectVersionUE5.REMOVE_OBJECT_EXPORT_PACKAGE_GUID) this.PackageGuid = new Guid(reader.ReadBytes(16));
            if (Asset.ObjectVersionUE5 >= ObjectVersionUE5.TRACK_OBJECT_EXPORT_IS_INHERITED) this.IsInheritedInstance = ReadBit(reader);
            this.PackageFlags = (EPackageFlags)reader.ReadUInt32();
            if (Asset.ObjectVersion >= ObjectVersion.VER_UE4_LOAD_FOR_EDITOR_GAME)
            {
                this.bNotAlwaysLoadedForEditorGame = ReadBit(reader);
            }
            if (Asset.ObjectVersion >= ObjectVersion.VER_UE4_COOKED_ASSETS_IN_EDITOR_SUPPORT)
            {
                this.bIsAsset = ReadBit(reader);
            }
            if (Asset.ObjectVersionUE5 >= ObjectVersionUE5.OPTIONAL_RESOURCES)
            {
                this.GeneratePublicHash = ReadBit(reader);
            }
            if (Asset.ObjectVersion >= ObjectVersion.VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS)
            {
                this.FirstExportDependencyOffset = reader.ReadInt32();
                this.SerializationBeforeSerializationDependenciesSize = reader.ReadInt32();
                this.CreateBeforeSerializationDependenciesSize = reader.ReadInt32();
                this.SerializationBeforeCreateDependenciesSize = reader.ReadInt32();
                this.CreateBeforeCreateDependenciesSize = reader.ReadInt32();
            }
            if (!Asset.HasUnversionedProperties && Asset.ObjectVersionUE5 >= ObjectVersionUE5.SCRIPT_SERIALIZATION_OFFSET)
            {
                this.ScriptSerializationStartOffset = reader.ReadInt64();
                this.ScriptSerializationEndOffset = reader.ReadInt64();
            }
        }

        public static long GetExportMapEntrySize(UAsset asset)
        {
            AssetBinaryWriter testWriter = new AssetBinaryWriter(new MemoryStream(127), asset);
            new Export().WriteExportMapEntry(testWriter);
            
            long res = testWriter.BaseStream.Position;
            testWriter.Dispose();
            return res;
        }

        public void WriteExportMapEntry(AssetBinaryWriter writer)
        {
            Asset = writer.Asset;

            writer.Write(ClassIndex?.Index ?? 0);
            writer.Write(SuperIndex?.Index ?? 0);
            if (Asset.ObjectVersion >= ObjectVersion.VER_UE4_TemplateIndex_IN_COOKED_EXPORTS)
            {
                writer.Write(TemplateIndex?.Index ?? 0);
            }
            writer.Write(OuterIndex?.Index ?? 0);
            writer.Write(ObjectName);
            writer.Write((uint)ObjectFlags);
            if (Asset.ObjectVersion < ObjectVersion.VER_UE4_64BIT_EXPORTMAP_SERIALSIZES)
            {
                writer.Write((int)SerialSize);
                writer.Write((int)SerialOffset);
            }
            else
            {
                writer.Write(SerialSize);
                writer.Write(SerialOffset);
            }
            WriteBit(writer, bForcedExport);
            WriteBit(writer, bNotForClient);
            WriteBit(writer, bNotForServer);
            if (Asset.ObjectVersionUE5 < ObjectVersionUE5.REMOVE_OBJECT_EXPORT_PACKAGE_GUID) writer.Write(PackageGuid.ToByteArray());
            if (Asset.ObjectVersionUE5 >= ObjectVersionUE5.TRACK_OBJECT_EXPORT_IS_INHERITED) WriteBit(writer, IsInheritedInstance);
            writer.Write((uint)PackageFlags);
            if (Asset.ObjectVersion >= ObjectVersion.VER_UE4_LOAD_FOR_EDITOR_GAME)
            {
                WriteBit(writer, bNotAlwaysLoadedForEditorGame);
            }
            if (Asset.ObjectVersion >= ObjectVersion.VER_UE4_COOKED_ASSETS_IN_EDITOR_SUPPORT)
            {
                WriteBit(writer, bIsAsset);
            }
            if (Asset.ObjectVersionUE5 >= ObjectVersionUE5.OPTIONAL_RESOURCES)
            {
                WriteBit(writer, GeneratePublicHash);
            }
            if (Asset.ObjectVersion >= ObjectVersion.VER_UE4_PRELOAD_DEPENDENCIES_IN_COOKED_EXPORTS)
            {
                writer.Write(FirstExportDependencyOffset);
                writer.Write(SerializationBeforeSerializationDependenciesSize);
                writer.Write(CreateBeforeSerializationDependenciesSize);
                writer.Write(SerializationBeforeCreateDependenciesSize);
                writer.Write(CreateBeforeCreateDependenciesSize);
            }
            if (!Asset.HasUnversionedProperties && Asset.ObjectVersionUE5 >= ObjectVersionUE5.SCRIPT_SERIALIZATION_OFFSET)
            {
                writer.Write(ScriptSerializationStartOffset);
                writer.Write(ScriptSerializationEndOffset);
            }
        }

        private static MemberInfo[] _allFields = null;
        private static void InitAllFields()
        {
            if (_allFields != null) return;
            _allFields = UAPUtils.GetOrderedMembers<Export>();
        }

        public static MemberInfo[] GetAllObjectExportFields(UAsset asset)
        {
            InitAllFields();

            var finalFields = new List<MemberInfo>();
            for (int i = 0; i < _allFields.Length; i++)
            {
                if (_allFields[i] == null) continue;
                DisplayIndexOrderAttribute attr = ((DisplayIndexOrderAttribute[])_allFields[i]?.GetCustomAttributes(typeof(DisplayIndexOrderAttribute), true))?[0];
                if (attr == null) continue;
                if (asset is UAsset && !attr.IsTraditional) continue;
                finalFields.Add(_allFields[i]);
            }
            return finalFields.ToArray();
        }

        public static string[] GetAllFieldNames(UAsset asset)
        {
            InitAllFields();

            MemberInfo[] relevantFields = GetAllObjectExportFields(asset);
            string[] allFieldNames = new string[relevantFields.Length];
            for (int i = 0; i < relevantFields.Length; i++)
            {
                allFieldNames[i] = relevantFields[i].Name;
            }
            return allFieldNames;
        }

        public FName GetExportClassType()
        {
            return this.ClassIndex.IsImport() ? this.ClassIndex.ToImport(Asset).ObjectName : FName.DefineDummy(Asset, this.ClassIndex.Index.ToString());
        }

        public FName GetClassTypeForAncestry(UAsset asset, out FName modulePath)
        {
            if (asset == null) asset = Asset;
            return GetClassTypeForAncestry(this.ClassIndex, asset, out modulePath);
        }

        public static FName GetClassTypeForAncestry(FPackageIndex classIndex, UAsset asset, out FName modulePath)
        {
            modulePath = null;
            if (classIndex.IsNull()) return null;
            if (classIndex.IsExport()) return classIndex.ToExport(asset).ObjectName;

            var imp = classIndex.ToImport(asset);
            if (imp.OuterIndex.IsImport()) modulePath = imp.OuterIndex.ToImport(asset).ObjectName;
            return imp.ObjectName;
        }

        public override string ToString()
        {
            InitAllFields();

            var sb = new StringBuilder();
            foreach (var info in _allFields)
            {
                DisplayIndexOrderAttribute attr = ((DisplayIndexOrderAttribute[])info?.GetCustomAttributes(typeof(DisplayIndexOrderAttribute), true))?[0];
                if (attr == null) continue;
                if (Asset is UAsset && !attr.IsTraditional) continue;

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
            foreach (var info in _allFields)
            {
                info.SetValue(res, info.GetValue(this));
            }
            return (T)res;
        }
    }
}
