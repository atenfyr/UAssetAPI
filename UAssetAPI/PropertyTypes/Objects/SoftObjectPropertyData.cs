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

    public FSoftObjectPath(AssetBinaryReader reader, bool allowIndex = true)
    {
        if (allowIndex && reader.Asset.SoftObjectPathList != null && reader.Asset.SoftObjectPathList.Count > 0)
        {
            // serialize as idx
            int idx = reader.ReadInt32();

            FSoftObjectPath target = reader.Asset.SoftObjectPathList[idx];
            this.AssetPath = target.AssetPath;
            this.SubPathString = target.SubPathString;
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

    public int Write(AssetBinaryWriter writer, bool allowIndex = true)
    {
        if (allowIndex && writer.Asset.SoftObjectPathList != null && writer.Asset.SoftObjectPathList.Count > 0)
        {
            // serialize as idx
            int idx = -1;
            for (int i = 0; i < writer.Asset.SoftObjectPathList.Count; i++)
            {
                FSoftObjectPath testingEntry = writer.Asset.SoftObjectPathList[i];
                if (testingEntry == this)
                {
                    idx = i;
                    break;
                }
            }
            if (idx < 0) throw new FormatException("Failed to find AssetPath in SoftObjectPathList");

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

    public static bool operator ==(FSoftObjectPath lhs, FSoftObjectPath rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(FSoftObjectPath lhs, FSoftObjectPath rhs)
    {
        return !lhs.Equals(rhs);
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