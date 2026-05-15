using Newtonsoft.Json;
using System;
using UAssetAPI.PropertyTypes.Objects;

namespace UAssetAPI.UnrealTypes;

[JsonObject(MemberSerialization.OptIn)]
public class FFieldPath : IStruct<FFieldPath>
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

    public FFieldPath(FName[] path, FPackageIndex resolvedOwner, int numExports = -1)
    {
        Path = path;
        ResolvedOwner = resolvedOwner;

        // sanity check: throw if makes no sense
        if (numExports > 0 && ResolvedOwner.Index > numExports) throw new FormatException("Received nonsensical FFieldPath ResolvedOwner: " + ResolvedOwner.Index);
    }

    public FFieldPath()
    {
        Path = [];
        ResolvedOwner = FPackageIndex.FromRawIndex(0);
    }

    public FFieldPath(AssetBinaryReader reader)
    {
        Path = reader.ReadArray(reader.ReadFName);
        ResolvedOwner = new FPackageIndex(reader.ReadInt32());
    }

    public static FFieldPath Read(AssetBinaryReader reader) => new FFieldPath(reader);

    public int Write(AssetBinaryWriter writer)
    {
        if (Path == null && ResolvedOwner == null)
        {
            return 0;
        }
        writer.Write(Path.Length);
        foreach (FName name in Path)
        {
            writer.Write(name);
        }

        writer.Write(ResolvedOwner.Index);
        return sizeof(int) * (2 + Path.Length * 2);
    }

    public static FFieldPath FromString(string[] d, UAsset asset)
    {
        throw new NotImplementedException();
    }
}
