using System;
using System.Reflection;
using System.Text;

namespace UAssetAPI
{
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
        public EPackageFlags PackageFlags; // If this export is a top-level package, this is the flags for the original package
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

        public ExportDetails(int classIndex, int superIndex, int templateIndex, int outerIndex, FName objectName, EObjectFlags objectFlags, int serialSize, int serialOffset, bool bForcedExport, bool bNotForClient, bool bNotForServer, Guid packageGuid, EPackageFlags packageFlags, bool bNotAlwaysLoadedForEditorGame, bool bIsAsset, int firstExportDependency, int serializationBeforeSerializationDependencies, int createBeforeSerializationDependencies, int serializationBeforeCreateDependencies, int createBeforeCreateDependencies)
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
