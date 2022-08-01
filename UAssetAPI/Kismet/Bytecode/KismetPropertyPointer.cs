using Newtonsoft.Json;
using System.ComponentModel;
using UAssetAPI.UnrealTypes;
using UAssetAPI.ExportTypes;

namespace UAssetAPI.Kismet.Bytecode
{
    /// <summary>
    /// Represents a Kismet bytecode pointer to an FProperty or FField.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class KismetPropertyPointer
    {
        public static readonly UE4Version XFER_PROP_POINTER_SWITCH_TO_SERIALIZING_AS_FIELD_PATH_VERSION = UE4Version.VER_UE4_ADDED_PACKAGE_OWNER;

        /// <summary>
        /// The pointer serialized as an FPackageIndex. Used in versions older than <see cref="XFER_PROP_POINTER_SWITCH_TO_SERIALIZING_AS_FIELD_PATH_VERSION"/>.
        /// </summary>
        [JsonProperty]
        [DefaultValue(null)]
        public FPackageIndex Old;

        /// <summary>
        /// The pointer serialized as an FFieldPath. Used in versions newer than <see cref="XFER_PROP_POINTER_SWITCH_TO_SERIALIZING_AS_FIELD_PATH_VERSION"/>.
        /// </summary>
        [JsonProperty]
        [DefaultValue(null)]
        public FFieldPath New;

        public bool ShouldSerializeOld()
        {
            return Old != null;
        }

        public bool ShouldSerializeNew()
        {
            return New != null;
        }

        public KismetPropertyPointer(FPackageIndex older)
        {
            Old = older;
        }

        public KismetPropertyPointer(FFieldPath newer)
        {
            New = newer;
        }

        public KismetPropertyPointer()
        {

        }
    }
}
