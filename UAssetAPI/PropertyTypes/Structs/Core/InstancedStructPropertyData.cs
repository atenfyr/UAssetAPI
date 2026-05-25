using Newtonsoft.Json;
using System;
using UAssetAPI.CustomVersions;
using UAssetAPI.ExportTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.PropertyTypes.Structs;

public class InstancedStructPropertyData : PropertyData<StructPropertyData>
{
    const uint LegacyEditorHeader = 0xABABABAB;
    [JsonProperty] public FPackageIndex Struct;
    [JsonProperty] public byte Version;
    public int SerialSize;

    public InstancedStructPropertyData(FName name) : base(name) { }
    public InstancedStructPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("InstancedStruct");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        if (reader.Asset.GetCustomVersion<FInstancedStructCustomVersion>() < FInstancedStructCustomVersion.CustomVersionAdded)
        {
            var header = reader.ReadUInt32();
            if (header != LegacyEditorHeader)
                throw new FormatException("Incorrect InstancedStruct header value");
            Version = reader.ReadByte();
        }

        Struct = new FPackageIndex(reader);
        SerialSize = reader.ReadInt32();
        Value = new StructPropertyData(FName.DefineDummy(reader.Asset, "InstancedStruct"));
        if (Struct.IsNull()) return;

        PropertyData bit;
        var unversionedHeader = new FUnversionedHeader(reader);
        FName parentName = Export.GetClassTypeForAncestry(Struct, reader.Asset, out FName parentModulePath);
        while ((bit = MainSerializer.Read(reader, null, parentName, parentModulePath, unversionedHeader, true)) != null)
        {
            Value.Value.Add(bit);
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        var start = writer.BaseStream.Position;
        if (writer.Asset.GetCustomVersion<FInstancedStructCustomVersion>() < FInstancedStructCustomVersion.CustomVersionAdded)
        {
            writer.Write(LegacyEditorHeader);
            writer.Write(Version);
        }

        writer.Write(Struct.Index);
        var saved = writer.BaseStream.Position;
        writer.Write(0);
        if (Struct.IsNull()) return (int)(writer.BaseStream.Position - start);

        FName parentName = Export.GetClassTypeForAncestry(Struct, writer.Asset, out FName parentModulePath);
        Value.Ancestry.Initialize(null, parentName, parentModulePath);
        var data = Value.Value;
        foreach (PropertyData current in data)
        {
            current.Ancestry.Initialize(null, parentName, parentModulePath);
        }

        MainSerializer.GenerateUnversionedHeader(ref data, parentName, parentModulePath, writer.Asset)?.Write(writer);

        foreach (var current in data)
        {
            MainSerializer.Write(current, writer, true);
        }

        var end = writer.BaseStream.Position;
        SerialSize = (int)(end - saved - sizeof(int));
        writer.BaseStream.Position = saved;
        writer.Write(SerialSize);
        writer.BaseStream.Position = end;

        return (int)(end - start);
    }
}
