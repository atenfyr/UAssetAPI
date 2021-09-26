using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UAssetAPI
{
    /// <summary>
    /// Base class for UObject resource types. FObjectResources are used to store UObjects on disk
    /// via FLinker's ImportMap (for resources contained in other packages) and ExportMap (for resources
    /// contained within the same package).
    /// </summary>
    public class FObjectResource
    {
        ///<summary>The name of the UObject represented by this resource.</summary>\
        [FObjectExportField(0)]
        public FName ObjectName;

        ///<summary>Location of the resource for this resource's Outer (import/other export). 0 = this resource is a top-level UPackage</summary>
        [FObjectExportField(1)]
        public int OuterIndex;

        public FObjectResource(FName objectName, int outerIndex)
        {
            ObjectName = objectName;
            OuterIndex = outerIndex;
        }

        public FObjectResource()
        {

        }
    }
}
