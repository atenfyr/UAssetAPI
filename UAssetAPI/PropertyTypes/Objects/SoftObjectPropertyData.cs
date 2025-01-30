using System;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Objects;

/// <summary>
/// Describes a reference variable to another object which may be null, and may become valid or invalid at any point. Near synonym for <see cref="SoftObjectPropertyData"/>.
/// </summary>
public class AssetObjectPropertyData : PropertyData<FString>
{
    public uint ID = 0;

    public AssetObjectPropertyData(FName name) : base(name) { }

    public AssetObjectPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("AssetObjectProperty");
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = reader.ReadFString();
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        return writer.Write(Value);
    }

    public override string ToString()
    {
        return "(" + Value + ", " + ID + ")";
    }

    public override void FromString(string[] d, UAsset asset)
    {
        //asset.AddNameReference(FString.FromString(d[0]));
        Value = FString.FromString(d[0]);
    }
}

public struct FTopLevelAssetPath
{
    /// <summary>
    /// Name of the package containing the asset e.g. /Path/To/Package
    /// If less than 5.1, this is null
    /// </summary>
    public FName PackageName;
    /// <summary>
    /// Name of the asset within the package e.g. 'AssetName'.
    /// If less than 5.1, this contains the full path instead
    /// </summary>
    public FName AssetName;

    public FTopLevelAssetPath(FName packageName, FName assetName)
    {
        PackageName = packageName;
        AssetName = assetName;
    }
}

/// <summary>
/// A reference variable to another object which may be null, and may become valid or invalid at any point.
/// </summary>
public struct FSoftObjectPath
{
    /// <summary>
    /// Asset path, patch to a top level object in a package. This is /package/path.assetname/
    /// </summary>
    public FTopLevelAssetPath AssetPath;
    /// <summary>
    /// Optional FString for subobject within an asset. This is the sub path after the :
    /// </summary>
    public FString SubPathString;

    public FSoftObjectPath(FName packageName, FName assetName, FString subPathString)
    {
        AssetPath = new FTopLevelAssetPath(packageName, assetName);
        SubPathString = subPathString;
    }

    public FSoftObjectPath(FTopLevelAssetPath assetPath, FString subPathString)
    {
        AssetPath = assetPath;
        SubPathString = subPathString;
    }

    public FSoftObjectPath(AssetBinaryReader reader)
    {
        // ObjectVersionUE5.DATA_RESOURCES empirical
        if (reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.DATA_RESOURCES && reader.Asset.SoftPackageReferenceList != null && reader.Asset.SoftPackageReferenceList.Count > 0)
        {
            // serialize as idx
            int idx = reader.ReadInt32();
            AssetPath = new FTopLevelAssetPath(FName.DefineDummy(reader.Asset, reader.Asset.SoftPackageReferenceList[idx]), null);
        }
        else
        {
            if (reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.FSOFTOBJECTPATH_REMOVE_ASSET_PATH_FNAMES)
            {
                AssetPath = new FTopLevelAssetPath(reader.ReadFName(), reader.ReadFName());
            }
            else if (reader.Asset.ObjectVersion >= ObjectVersion.VER_UE4_ADDED_SOFT_OBJECT_PATH)
            {
                AssetPath = new FTopLevelAssetPath(null, reader.ReadFName());
            }
            else
            {
                AssetPath = new FTopLevelAssetPath(null, null);
            }

            SubPathString = reader.ReadFString();
        }
    }

    public int Write(AssetBinaryWriter writer)
    {
        // ObjectVersionUE5.DATA_RESOURCES empirical
        if (writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.DATA_RESOURCES && writer.Asset.SoftPackageReferenceList != null && writer.Asset.SoftPackageReferenceList.Count > 0)
        {
            // serialize as idx
            // don't automatically add to soft package reference list
            FString softName = AssetPath.PackageName?.Value;
            if (softName == null) throw new FormatException("Attempt to serialize invalid AssetPath as index");

            int idx = -1;
            for (int i = 0; i < writer.Asset.SoftPackageReferenceList.Count; i++)
            {
                FString testingEntry = writer.Asset.SoftPackageReferenceList[i];
                if (testingEntry == softName)
                {
                    idx = i;
                    break;
                }
            }
            if (idx < 0) throw new FormatException("Failed to find AssetPath in soft package references list");

            writer.Write(idx);
            return sizeof(int);
        }

        if (writer.Asset.ObjectVersion < ObjectVersion.VER_UE4_ADDED_SOFT_OBJECT_PATH)
            return writer.Write(SubPathString);

        var offset = writer.BaseStream.Position;
        if (writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.FSOFTOBJECTPATH_REMOVE_ASSET_PATH_FNAMES) writer.Write(AssetPath.PackageName);
        writer.Write(AssetPath.AssetName);
        writer.Write(SubPathString);
        return (int)(writer.BaseStream.Position - offset);
    }
}

/// <summary>
/// Describes a reference variable to another object which may be null, and may become valid or invalid at any point. Near synonym for <see cref="AssetObjectPropertyData"/>.
/// </summary>
public class SoftObjectPropertyData : PropertyData<FSoftObjectPath>
{
    public SoftObjectPropertyData(FName name) : base(name) { }

    public SoftObjectPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("SoftObjectProperty");
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        Value = new FSoftObjectPath(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        return Value.Write(writer);
    }

    public override string ToString()
    {   
        return "(" + Value.AssetPath.PackageName.ToString() + ", " + Value.AssetPath.AssetName.ToString() + ", " + Value.SubPathString.ToString() + ")";
    }

    public override void FromString(string[] d, UAsset asset)
    {
        FName one = FName.FromString(asset, d[0]);
        FName two = FName.FromString(asset, d[1]);
        FString three = string.IsNullOrEmpty(d[2]) ? null : FString.FromString(d[2]);

        Value = new FSoftObjectPath(one, two, three);
    }
}