using Newtonsoft.Json;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A struct that contains a string reference to an object, either a top level asset or a subobject.
/// This can be used to make soft references to assets that are loaded on demand.
/// This is stored internally as an FName pointing to the top level asset (/package/path.assetname) and an option a string subobject path.
/// </summary>
public class SoftObjectPathPropertyData : PropertyData<FSoftObjectPath>
{
    /// <summary>Used in older versions of the Unreal Engine.</summary>
    [JsonProperty]
    public FString Path;

    public SoftObjectPathPropertyData(FName name) : base(name) { }

    public SoftObjectPathPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("SoftObjectPath");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        if (reader.Asset.ObjectVersion < ObjectVersion.VER_UE4_ADDED_SOFT_OBJECT_PATH)
        {
            Path = reader.ReadFString();
        }
        else
        {
            Value = new FSoftObjectPath(reader);
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        int here = (int)writer.BaseStream.Position;

        if (writer.Asset.ObjectVersion < ObjectVersion.VER_UE4_ADDED_SOFT_OBJECT_PATH)
        {
            writer.Write(Path);
        }
        else
        {
            Value.Write(writer);
        }

        return (int)writer.BaseStream.Position - here;
    }

    public override string ToString()
    {
        return "(" + Value.AssetPath.PackageName.ToString() + ", " + Value.AssetPath.AssetName.ToString() + ", " + Value.SubPathString.ToString() + ")";
    }

    public override void FromString(string[] d, UAsset asset)
    {
        if (asset.ObjectVersion < ObjectVersion.VER_UE4_ADDED_SOFT_OBJECT_PATH)
        {
            Path = FString.FromString(d[0]);
        }
        else
        {
            FName one = FName.FromString(asset, d[0]);
            FName two = FName.FromString(asset, d[1]);
            FString three = string.IsNullOrEmpty(d[2]) ? null : FString.FromString(d[2]);

            Value = new FSoftObjectPath(one, two, three);
        }
    }
}


/// <summary>
/// A struct that contains a string reference to a class. Can be used to make soft references to classes.
/// </summary>
public class SoftClassPathPropertyData : SoftObjectPathPropertyData
{
    public SoftClassPathPropertyData(FName name) : base(name) { }

    public SoftClassPathPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("SoftClassPath");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}


public class SoftAssetPathPropertyData : SoftObjectPathPropertyData
{
    public SoftAssetPathPropertyData(FName name) : base(name) { }

    public SoftAssetPathPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("SoftAssetPath");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}

/// <summary>
/// A struct that contains a string reference to a class. Can be used to make soft references to classes.
/// </summary>
public class StringAssetReferencePropertyData : SoftObjectPathPropertyData
{
    public StringAssetReferencePropertyData(FName name) : base(name) { }

    public StringAssetReferencePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("StringAssetReference");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}

public class StringClassReferencePropertyData : SoftObjectPathPropertyData
{
    public StringClassReferencePropertyData(FName name) : base(name) { }

    public StringClassReferencePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("StringClassReference");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;
}