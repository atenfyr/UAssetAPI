using System;
using System.Collections.Generic;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI;

/// <summary>
/// A custom version. Controls more specific serialization than the main engine object version does.
/// </summary>
public class CustomVersion : ICloneable
{
    /// <summary>
    /// Static map of custom version GUIDs to the object or enum that they represent in the Unreal Engine. This list is not necessarily exhaustive, so feel free to add to it if need be.
    /// </summary>
    public static readonly Dictionary<Guid, string> GuidToCustomVersionStringMap = new Dictionary<Guid, string>()
    {
        { UnusedCustomVersionKey, "UnusedCustomVersionKey" },
        { UAPUtils.GUID(0xB0D832E4, 0x1F894F0D, 0xACCF7EB7, 0x36FD4AA2), "FBlueprintsObjectVersion" },
        { UAPUtils.GUID(0xE1C64328, 0xA22C4D53, 0xA36C8E86, 0x6417BD8C), "FBuildObjectVersion" },
        { UAPUtils.GUID(0x375EC13C, 0x06E448FB, 0xB50084F0, 0x262A717E), "FCoreObjectVersion" },
        { UAPUtils.GUID(0xE4B068ED, 0xF49442E9, 0xA231DA0B, 0x2E46BB41), "FEditorObjectVersion" },
        { UAPUtils.GUID(0xCFFC743F, 0x43B04480, 0x939114DF, 0x171D2073), "FFrameworkObjectVersion" },
        { UAPUtils.GUID(0xB02B49B5, 0xBB2044E9, 0xA30432B7, 0x52E40360), "FMobileObjectVersion" },
        { UAPUtils.GUID(0xA4E4105C, 0x59A149B5, 0xA7C540C4, 0x547EDFEE), "FNetworkingObjectVersion" },
        { UAPUtils.GUID(0x39C831C9, 0x5AE647DC, 0x9A449C17, 0x3E1C8E7C), "FOnlineObjectVersion" },
        { UAPUtils.GUID(0x78F01B33, 0xEBEA4F98, 0xB9B484EA, 0xCCB95AA2), "FPhysicsObjectVersion" },
        { UAPUtils.GUID(0x6631380F, 0x2D4D43E0, 0x8009CF27, 0x6956A95A), "FPlatformObjectVersion" },
        { UAPUtils.GUID(0x12F88B9F, 0x88754AFC, 0xA67CD90C, 0x383ABD29), "FRenderingObjectVersion" },
        { UAPUtils.GUID(0x7B5AE74C, 0xD2704C10, 0xA9585798, 0x0B212A5A), "FSequencerObjectVersion" },
        { UAPUtils.GUID(0xD7296918, 0x1DD64BDD, 0x9DE264A8, 0x3CC13884), "FVRObjectVersion" },
        { UAPUtils.GUID(0xC2A15278, 0xBFE74AFE, 0x6C1790FF, 0x531DF755), "FLoadTimesObjectVersion" },
        { UAPUtils.GUID(0x6EACA3D4, 0x40EC4CC1, 0xb7868BED, 0x9428FC5), "FGeometryObjectVersion" },
        { UAPUtils.GUID(0x29E575DD, 0xE0A34627, 0x9D10D276, 0x232CDCEA), "FAnimPhysObjectVersion" },
        { UAPUtils.GUID(0xAF43A65D, 0x7FD34947, 0x98733E8E, 0xD9C1BB05), "FAnimObjectVersion" },
        { UAPUtils.GUID(0x6B266CEC, 0x1EC74B8F, 0xA30BE4D9, 0x0942FC07), "FReflectionCaptureObjectVersion" },
        { UAPUtils.GUID(0x0DF73D61, 0xA23F47EA, 0xB72789E9, 0x0C41499A), "FAutomationObjectVersion" },
        { UAPUtils.GUID(0x601D1886, 0xAC644F84, 0xAA16D3DE, 0x0DEAC7D6), "FFortniteMainBranchObjectVersion" },
        { UAPUtils.GUID(0x9DFFBCD6, 0x494F0158, 0xE2211282, 0x3C92A888), "FEnterpriseObjectVersion" },
        { UAPUtils.GUID(0xF2AED0AC, 0x9AFE416F, 0x8664AA7F, 0xFA26D6FC), "FNiagaraObjectVersion" },
        { UAPUtils.GUID(0x174F1F0B, 0xB4C645A5, 0xB13F2EE8, 0xD0FB917D), "FDestructionObjectVersion" },
        { UAPUtils.GUID(0x35F94A83, 0xE258406C, 0xA31809F5, 0x9610247C), "FExternalPhysicsCustomObjectVersion" },
        { UAPUtils.GUID(0xB68FC16E, 0x8B1B42E2, 0xB453215C, 0x058844FE), "FExternalPhysicsMaterialCustomObjectVersion" },
        { UAPUtils.GUID(0xB2E18506, 0x4273CFC2, 0xA54EF4BB, 0x758BBA07), "FCineCameraObjectVersion" },
        { UAPUtils.GUID(0x64F58936, 0xFD1B42BA, 0xBA967289, 0xD5D0FA4E), "FVirtualProductionObjectVersion" },
        { UAPUtils.GUID(0x6f0ed827, 0xa6094895, 0x9c91998d, 0x90180ea4), "FMediaFrameworkObjectVersion" },
        { UAPUtils.GUID(0xAFE08691, 0x3A0D4952, 0xB673673B, 0x7CF22D1E), "FPoseDriverCustomVersion" },
        { UAPUtils.GUID(0xCB8AB0CD, 0xE78C4BDE, 0xA8621393, 0x14E9EF62), "FTempCustomVersion" },
        { UAPUtils.GUID(0x2EB5FDBD, 0x01AC4D10, 0x8136F38F, 0x3393A5DA), "FAnimationCustomVersion" },
        { UAPUtils.GUID(0x717F9EE7, 0xE9B0493A, 0x88B39132, 0x1B388107), "FAssetRegistryVersion" },
        { UAPUtils.GUID(0xFB680AF2, 0x59EF4BA3, 0xBAA819B5, 0x73C8443D), "FClothingAssetCustomVersion" },
        { UAPUtils.GUID(0x9C54D522, 0xA8264FBE, 0x94210746, 0x61B482D0), "FReleaseObjectVersion" },
        { UAPUtils.GUID(0x4A56EB40, 0x10F511DC, 0x92D3347E, 0xB2C96AE7), "FParticleSystemCustomVersion" },
        { UAPUtils.GUID(0xD78A4A00, 0xE8584697, 0xBAA819B5, 0x487D46B4), "FSkeletalMeshCustomVersion" },
        { UAPUtils.GUID(0x5579F886, 0x933A4C1F, 0x83BA087B, 0x6361B92F), "FRecomputeTangentCustomVersion" },
        { UAPUtils.GUID(0x612FBE52, 0xDA53400B, 0x910D4F91, 0x9FB1857C), "FOverlappingVerticesCustomVersion" },
        { UAPUtils.GUID(0x430C4D19, 0x71544970, 0x87699B69, 0xDF90B0E5), "FFoliageCustomVersion" },
        { UAPUtils.GUID(0xaafe32bd, 0x53954c14, 0xb66a5e25, 0x1032d1dd), "FProceduralFoliageCustomVersion" },
        { UAPUtils.GUID(0xab965196, 0x45d808fc, 0xb7d7228d, 0x78ad569e), "FLiveLinkCustomVersion" },
        { UAPUtils.GUID(0xE7086368, 0x6B234C58, 0x84391B70, 0x16265E91), "FFortniteReleaseBranchCustomObjectVersion" },
        { UAPUtils.GUID(0xD89B5E42, 0x24BD4D46, 0x8412ACA8, 0xDF641779), "FUE5ReleaseStreamObjectVersion" },
        { UAPUtils.GUID(0xFCF57AFA, 0x50764283, 0xB9A9E658, 0xFFA02D32), "FNiagaraCustomVersion" },
        { UAPUtils.GUID(0x697DD581, 0xE64f41AB, 0xAA4A51EC, 0xBEB7B628), "FUE5MainStreamObjectVersion" }

        // etc.
    };

    /// <summary>
    /// A GUID that represents an unused custom version.
    /// </summary>
    public static readonly Guid UnusedCustomVersionKey = UAPUtils.GUID(0, 0, 0, 0xF99D40C1);

    /// <summary>
    /// Returns the name of the object or enum that a custom version GUID represents, as specified in <see cref="GuidToCustomVersionStringMap"/>.
    /// </summary>
    /// <param name="guid">A GUID that represents a custom version.</param>
    /// <returns>A string that represents the friendly name of the corresponding custom version.</returns>
    public static string GetCustomVersionFriendlyNameFromGuid(Guid guid)
    {
        return GuidToCustomVersionStringMap.ContainsKey(guid) ? GuidToCustomVersionStringMap[guid] : null;
    }

    /// <summary>
    /// Returns the GUID of the custom version that the object or enum name provided represents.
    /// </summary>
    /// <param name="friendlyName">The name of a custom version object or enum.</param>
    /// <returns>A GUID that represents the custom version</returns>
    public static Guid GetCustomVersionGuidFromFriendlyName(string friendlyName)
    {
        foreach (KeyValuePair<Guid, string> entry in GuidToCustomVersionStringMap)
        {
            if (entry.Value == friendlyName) return entry.Key;
        }
        return UnusedCustomVersionKey;
    }

    public FString Name = null;
    public Guid Key;
    public string FriendlyName = null;
    public int Version;
    public bool IsSerialized = true;

    public CustomVersion SetIsSerialized(bool val)
    {
        this.IsSerialized = val;
        return this;
    }

    public object Clone()
    {
        return new CustomVersion(Key, Version)
        {
            Name = Name,
            FriendlyName = FriendlyName,
            IsSerialized = IsSerialized
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomVersion"/> class given an object or enum name and a version number.
    /// </summary>
    /// <param name="friendlyName">The friendly name to use when initializing this custom version.</param>
    /// <param name="version">The version number to use when initializing this custom version.</param>
    public CustomVersion(string friendlyName, int version)
    {
        Key = GetCustomVersionGuidFromFriendlyName(friendlyName);
        FriendlyName = friendlyName;
        Version = version;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomVersion"/> class given a custom version GUID and a version number.
    /// </summary>
    /// <param name="key">The GUID to use when initializing this custom version.</param>
    /// <param name="version">The version number to use when initializing this custom version.</param>
    public CustomVersion(Guid key, int version)
    {
        Key = key;
        if (GuidToCustomVersionStringMap.ContainsKey(key)) FriendlyName = GuidToCustomVersionStringMap[key];
        Version = version;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomVersion"/> class.
    /// </summary>
    public CustomVersion()
    {
        Key = UnusedCustomVersionKey;
        Version = 0;
    }
}