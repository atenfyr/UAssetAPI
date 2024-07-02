using Newtonsoft.Json;
using System.ComponentModel;
using UAssetAPI.UnrealTypes;
using UAssetAPI.CustomVersions;

namespace UAssetAPI.Kismet.Bytecode;

/// <summary>
/// Represents a Kismet bytecode pointer to an FProperty or FField.
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public class KismetPropertyPointer
{
    /// <summary>
    /// The pointer serialized as an FPackageIndex. Used in versions older than <see cref="FReleaseObjectVersion.FFieldPathOwnerSerialization"/>.
    /// </summary>
    [JsonProperty]
    [DefaultValue(null)]
    public FPackageIndex Old;

    /// <summary>
    /// The pointer serialized as an FFieldPath. Used in versions newer than <see cref="FReleaseObjectVersion.FFieldPathOwnerSerialization"/>.
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
