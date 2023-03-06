using UAssetAPI.UnrealTypes;

namespace UAssetAPI
{
    /// <summary>
    /// UObject resource type for objects that are referenced by this package, but contained within another package.
    /// In IO store assets, this is serialized as an FPackageObjectIndex.
    /// </summary>
    public class Import
    {
        ///<summary>The name of the UObject represented by this resource.</summary>
        public FName ObjectName;
        ///<summary>Location of the resource for this resource's Outer (import/other export). 0 = this resource is a top-level UPackage</summary>
        public FPackageIndex OuterIndex;
        public FName ClassPackage;
        public FName ClassName;

        public Import(string classPackage, string className, FPackageIndex outerIndex, string objectName, UAsset asset)
        {
            ObjectName = new FName(asset, objectName);
            OuterIndex = outerIndex;
            ClassPackage = new FName(asset, classPackage);
            ClassName = new FName(asset, className);
        }

        public Import(FName classPackage, FName className, FPackageIndex outerIndex, FName objectName)
        {
            ObjectName = objectName;
            OuterIndex = outerIndex;
            ClassPackage = classPackage;
            ClassName = className;
        }

        public Import(AssetBinaryReader reader)
        {
            if (reader?.Asset is UAsset)
            {
                ClassPackage = reader.ReadFName();
                ClassName = reader.ReadFName();
                OuterIndex = new FPackageIndex(reader.ReadInt32());
                ObjectName = reader.ReadFName();
            }
        }

        public Import()
        {

        }
    }
}
