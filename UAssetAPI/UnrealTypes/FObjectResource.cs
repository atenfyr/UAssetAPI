using UAssetAPI.ExportTypes;

namespace UAssetAPI.UnrealTypes
{
    /// <summary>
    /// Base class for UObject resource types. FObjectResources are used to store UObjects on disk
    /// via the import map (for resources contained in other packages) and export map (for resources
    /// contained within the same package).
    /// </summary>
    public class FObjectResource
    {
        ///<summary>The name of the UObject represented by this resource.</summary>
        [DisplayIndexOrder(0)]
        public FName ObjectName;

        ///<summary>Location of the resource for this resource's Outer (import/other export). 0 = this resource is a top-level UPackage</summary>
        [DisplayIndexOrder(1)]
        public FPackageIndex OuterIndex;

        public FObjectResource(FName objectName, FPackageIndex outerIndex)
        {
            ObjectName = objectName;
            OuterIndex = outerIndex;
        }

        public FObjectResource()
        {

        }
    }
}
