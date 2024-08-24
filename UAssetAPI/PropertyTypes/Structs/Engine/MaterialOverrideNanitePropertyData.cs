using UAssetAPI.CustomVersions;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

public class MaterialOverrideNanitePropertyData : StructPropertyData
{
    public FSoftObjectPath OverrideMaterialRef;
    public bool bEnableOverride;
    public FPackageIndex OverrideMaterial;
    public bool bSerializeAsCookedData;

    public MaterialOverrideNanitePropertyData(FName name, FName forcedType) : base(name, forcedType) { }
    public MaterialOverrideNanitePropertyData(FName name) : base(name) { }
    public MaterialOverrideNanitePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("MaterialOverrideNanite");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        if (reader.Asset.GetCustomVersion<FFortniteReleaseBranchCustomObjectVersion>() < FFortniteReleaseBranchCustomObjectVersion.NaniteMaterialOverrideUsesEditorOnly)
        {
            OverrideMaterialRef = new FSoftObjectPath(reader);
            bEnableOverride = reader.ReadBooleanInt();
            OverrideMaterial = FPackageIndex.FromRawIndex(reader.ReadInt32());
            return;
        }

        bSerializeAsCookedData = reader.ReadBooleanInt();
        if (bSerializeAsCookedData) OverrideMaterial = FPackageIndex.FromRawIndex(reader.ReadInt32());

        StructType = FName.DefineDummy(reader.Asset, CurrentPropertyType);
        base.Read(reader, includeHeader, 1, 0, PropertySerializationContext.StructFallback);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader && !writer.Asset.HasUnversionedProperties)
        {
            this.WriteEndPropertyTag(writer);
        }

        int here = (int)writer.BaseStream.Position;

        if (writer.Asset.GetCustomVersion<FFortniteReleaseBranchCustomObjectVersion>() < FFortniteReleaseBranchCustomObjectVersion.NaniteMaterialOverrideUsesEditorOnly)
        {
            OverrideMaterialRef.Write(writer);
            writer.Write(bEnableOverride ? 1 : 0);
            writer.Write(OverrideMaterial?.Index ?? 0);
        }

        writer.Write(bSerializeAsCookedData ? 1 : 0);
        if (bSerializeAsCookedData) writer.Write(OverrideMaterial?.Index ?? 0);

        StructType = FName.DefineDummy(writer.Asset, CurrentPropertyType);
        base.Write(writer, includeHeader, PropertySerializationContext.StructFallback);

        return (int)writer.BaseStream.Position - here;
    }

    public override void FromString(string[] d, UAsset asset)
    {

    }
}