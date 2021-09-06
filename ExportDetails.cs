using System;
using System.Reflection;
using System.Text;

namespace UAssetAPI
{
    [Flags]
    public enum EObjectFlags
    {
        RF_NoFlags = 0x00000000,
        RF_Public = 0x00000001,
        RF_Standalone = 0x00000002,
        RF_MarkAsNative = 0x00000004,
        RF_Transactional = 0x00000008,
        RF_ClassDefaultObject = 0x00000010,
        RF_ArchetypeObject = 0x00000020,
        RF_Transient = 0x00000040,
        RF_MarkAsRootSet = 0x00000080,
        RF_TagGarbageTemp = 0x00000100,
        RF_NeedInitialization = 0x00000200,
        RF_NeedLoad = 0x00000400,
        RF_KeepForCooker = 0x00000800,
        RF_NeedPostLoad = 0x00001000,
        RF_NeedPostLoadSubobjects = 0x00002000,
        RF_NewerVersionExists = 0x00004000,
        RF_BeginDestroyed = 0x00008000,
        RF_FinishDestroyed = 0x00010000,
        RF_BeingRegenerated = 0x00020000,
        RF_DefaultSubObject = 0x00040000,
        RF_WasLoaded = 0x00080000,
        RF_TextExportTransient = 0x00100000,
        RF_LoadCompleted = 0x00200000,
        RF_InheritableComponentTemplate = 0x00400000,
        RF_DuplicateTransient = 0x00800000,
        RF_StrongRefOnFrame = 0x01000000,
        RF_NonPIEDuplicateTransient = 0x02000000,
        RF_Dynamic = 0x04000000,
        RF_WillBeLoaded = 0x08000000,
        RF_HasExternalPackage = 0x10000000,
    }

    public class ExportDetails
    {
        // Public Fields //
        public int ClassIndex; // Location of this export's class (import/other export). 0 = this export is a UClass
        public int SuperIndex; // Location of this export's parent class (import/other export). 0 = this export is not derived from UStruct
        public int TemplateIndex; // Location of this export's template (import/other export). 0 = there is some problem
        public int OuterIndex; // Location of this export's Outer (import/other export). 0 = this resource is a top-level UPackage
        public FName ObjectName; // Name of the UObject represented by this export
        public EObjectFlags ObjectFlags; 
        public long SerialSize;
        public long SerialOffset;
        public bool bForcedExport; // Was this export forced into the export table via OBJECTMARK_ForceTagExp?
        public bool bNotForClient; // Should this export not be loaded on clients?
        public bool bNotForServer; // Should this export not be loaded on servers?
        public Guid PackageGuid; // Deprecated
        public uint PackageFlags; // If this export is a top-level package, this is the flags for the original package
        public bool bNotAlwaysLoadedForEditorGame; // Should this export be always loaded in editor game?
        public bool bIsAsset; // Is this an asset?

        public int FirstExportDependency;
        public int SerializationBeforeSerializationDependencies;
        public int CreateBeforeSerializationDependencies;
        public int SerializationBeforeCreateDependencies;
        public int CreateBeforeCreateDependencies;
        // End Public Fields //

        private static FieldInfo[] _allFields = null;

        public static string[] GetAllFieldNames()
        {
            if (_allFields == null) _allFields = typeof(ExportDetails).GetFields();

            string[] allFieldNames = new string[_allFields.Length];
            for (int i = 0; i < _allFields.Length; i++)
            {
                allFieldNames[i] = _allFields[i].Name;
            }
            return allFieldNames;
        }

        public override string ToString()
        {
            if (_allFields == null) _allFields = typeof(ExportDetails).GetFields();

            var sb = new StringBuilder();
            foreach (var info in _allFields)
            {
                var value = info.GetValue(this) ?? "(null)";
                sb.AppendLine(info.Name + ": " + value.ToString());
            }
            return sb.ToString();
        }

        public ExportDetails(int classIndex, int superIndex, int templateIndex, int outerIndex, FName objectName, EObjectFlags objectFlags, int serialSize, int serialOffset, bool bForcedExport, bool bNotForClient, bool bNotForServer, Guid packageGuid, uint packageFlags, bool bNotAlwaysLoadedForEditorGame, bool bIsAsset, int firstExportDependency, int serializationBeforeSerializationDependencies, int createBeforeSerializationDependencies, int serializationBeforeCreateDependencies, int createBeforeCreateDependencies)
        {
            ClassIndex = classIndex;
            SuperIndex = superIndex;
            TemplateIndex = templateIndex;
            OuterIndex = outerIndex;
            ObjectName = objectName;
            ObjectFlags = objectFlags;
            SerialSize = serialSize;
            SerialOffset = serialOffset;
            this.bForcedExport = bForcedExport;
            this.bNotForClient = bNotForClient;
            this.bNotForServer = bNotForServer;
            PackageGuid = packageGuid;
            PackageFlags = packageFlags;
            this.bNotAlwaysLoadedForEditorGame = bNotAlwaysLoadedForEditorGame;
            this.bIsAsset = bIsAsset;
            FirstExportDependency = firstExportDependency;
            SerializationBeforeSerializationDependencies = serializationBeforeSerializationDependencies;
            CreateBeforeSerializationDependencies = createBeforeSerializationDependencies;
            SerializationBeforeCreateDependencies = serializationBeforeCreateDependencies;
            CreateBeforeCreateDependencies = createBeforeCreateDependencies;
        }

        public ExportDetails(ExportDetails refer)
        {
            ClassIndex = refer.ClassIndex;
            SuperIndex = refer.SuperIndex;
            TemplateIndex = refer.TemplateIndex;
            OuterIndex = refer.OuterIndex;
            ObjectName = refer.ObjectName;
            ObjectFlags = refer.ObjectFlags;
            SerialSize = refer.SerialSize;
            SerialOffset = refer.SerialOffset;
            this.bForcedExport = refer.bForcedExport;
            this.bNotForClient = refer.bNotForClient;
            this.bNotForServer = refer.bNotForServer;
            PackageGuid = refer.PackageGuid;
            PackageFlags = refer.PackageFlags;
            this.bNotAlwaysLoadedForEditorGame = refer.bNotAlwaysLoadedForEditorGame;
            this.bIsAsset = refer.bIsAsset;
            FirstExportDependency = refer.FirstExportDependency;
            SerializationBeforeSerializationDependencies = refer.SerializationBeforeSerializationDependencies;
            CreateBeforeSerializationDependencies = refer.CreateBeforeSerializationDependencies;
            SerializationBeforeCreateDependencies = refer.SerializationBeforeCreateDependencies;
            CreateBeforeCreateDependencies = refer.CreateBeforeCreateDependencies;
        }

        public ExportDetails()
        {

        }
    }
}
