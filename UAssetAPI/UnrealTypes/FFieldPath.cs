using Newtonsoft.Json;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.UnrealTypes
{
    [JsonObject(MemberSerialization.OptIn)]
    public class FFieldPath
    {
        /// <summary>
        /// Path to the FField object from the innermost FField to the outermost UObject (UPackage)
        /// </summary>
        [JsonProperty]
        public FName[] Path;

        /// <summary>
        /// The cached owner of this field.
        /// </summary>
        [JsonProperty]
        public FPackageIndex ResolvedOwner;

        public FFieldPath(FName[] path, FPackageIndex resolvedOwner)
        {
            Path = path;
            ResolvedOwner = resolvedOwner;
        }

        public FFieldPath()
        {

        }
    }
}
