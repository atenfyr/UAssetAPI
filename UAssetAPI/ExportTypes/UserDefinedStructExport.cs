using System.Collections.Generic;
using System.Linq;
using UAssetAPI.CustomVersions;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.ExportTypes;

public class UserDefinedStructExport : StructExport
{
    public uint StructFlags;
    public List<PropertyData> StructData = [];
    public EClassSerializationControlExtension SerializationControl2;
    public EOverriddenPropertyOperation Operation2;

    public UserDefinedStructExport(Export super) : base(super) { }

    public UserDefinedStructExport(UAsset asset, byte[] extras) : base(asset, extras) { }

    public UserDefinedStructExport() { }

    public override void Read(AssetBinaryReader reader, int nextStarting)
    {
        base.Read(reader, nextStarting);

        if (reader.Asset.Mappings?.Schemas != null && this.ObjectName?.Value?.Value != null)
        {
            UsmapSchema newSchema = Usmap.GetSchemaFromStructExport(this, reader.Asset.Mappings?.AreFNamesCaseInsensitive ?? true);
            reader.Asset.Mappings.Schemas[this.ObjectName.Value.Value] = newSchema;
        }

        if (reader.Asset.GetCustomVersion<FFrameworkObjectVersion>() < FFrameworkObjectVersion.UserDefinedStructsStoreDefaultInstance
            || (Data.FirstOrDefault(x => x.Name.Value.Value == "Status") is BytePropertyData byteprop && byteprop.EnumValue?.Value.Value != "UDSS_UpToDate"))
            return;

        if (this.ObjectFlags.HasFlag(EObjectFlags.RF_ClassDefaultObject)) return;

        StructFlags = reader.ReadUInt32();

        PropertyData bit;
        var unversionedHeader = new FUnversionedHeader(reader);
        if (!reader.Asset.HasUnversionedProperties && reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.PROPERTY_TAG_EXTENSION_AND_OVERRIDABLE_SERIALIZATION)
        {
            SerializationControl2 = (EClassSerializationControlExtension)reader.ReadByte();

            if (SerializationControl2.HasFlag(EClassSerializationControlExtension.OverridableSerializationInformation))
            {
                Operation2 = (EOverriddenPropertyOperation)reader.ReadByte();
            }
        }
        while ((bit = MainSerializer.Read(reader, null, this.ObjectName, FName.DefineDummy(reader.Asset, reader.Asset.InternalAssetPath), unversionedHeader, true)) != null)
        {
            StructData.Add(bit);
        }
    }

    public override void ResolveAncestries(UAsset asset, AncestryInfo ancestrySoFar)
    {
        var ancestryNew = (AncestryInfo)ancestrySoFar.Clone();
        ancestryNew.SetAsParent(this.ObjectName, null);

        if (StructData != null)
        {
            for (int i = 0; i < StructData.Count; i++) StructData[i]?.ResolveAncestries(asset, ancestryNew);
        }
        base.ResolveAncestries(asset, ancestrySoFar);
    }

    public override void Write(AssetBinaryWriter writer)
    {
        base.Write(writer);

        if (writer.Asset.GetCustomVersion<FFrameworkObjectVersion>() < FFrameworkObjectVersion.UserDefinedStructsStoreDefaultInstance
            || (Data.FirstOrDefault(x => x.Name.Value.Value == "Status") is BytePropertyData byteprop && byteprop.EnumValue?.Value.Value != "UDSS_UpToDate"))
            return;

        if (this.ObjectFlags.HasFlag(EObjectFlags.RF_ClassDefaultObject)) return;

        writer.Write(StructFlags);

        MainSerializer.GenerateUnversionedHeader(ref StructData, this.ObjectName, FName.DefineDummy(writer.Asset, writer.Asset.InternalAssetPath), writer.Asset)?.Write(writer);
        for (int j = 0; j < StructData.Count; j++)
        {
            PropertyData current = StructData[j];
            MainSerializer.Write(current, writer, true);
        }
        if (!writer.Asset.HasUnversionedProperties) writer.Write(new FName(writer.Asset, "None"));
    }
}