using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.CustomVersions;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// Special FName struct used within animations.
/// </summary>
public class SmartNamePropertyData : PropertyData
{
    /// <summary>
    /// The display name of this FSmartName.
    /// </summary>
    public FName DisplayName;

    /// <summary>
    /// SmartName::UID_Type - for faster access
    /// </summary>
    public ushort SmartNameID;

    /// <summary>
    /// Uncertain
    /// </summary>
    public Guid TempGUID;

    public SmartNamePropertyData(FName name) : base(name) { }

    public SmartNamePropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("SmartName");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.ReadEndPropertyTag(reader);
        }

        DisplayName = reader.ReadFName();
        if (reader.Asset.GetCustomVersion<FAnimPhysObjectVersion>() < FAnimPhysObjectVersion.RemoveUIDFromSmartNameSerialize)
        {
            SmartNameID = reader.ReadUInt16();
        }
        if (reader.Asset.GetCustomVersion<FAnimPhysObjectVersion>() < FAnimPhysObjectVersion.SmartNameRefactorForDeterministicCooking && !reader.Asset.IsFilterEditorOnly)
        {
            TempGUID = new Guid(reader.ReadBytes(16));
        }
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            this.WriteEndPropertyTag(writer);
        }

        long here = writer.BaseStream.Position;

        writer.Write(DisplayName);
        if (writer.Asset.GetCustomVersion<FAnimPhysObjectVersion>() < FAnimPhysObjectVersion.RemoveUIDFromSmartNameSerialize)
        {
            writer.Write(SmartNameID);
        }
        if (writer.Asset.GetCustomVersion<FAnimPhysObjectVersion>() < FAnimPhysObjectVersion.SmartNameRefactorForDeterministicCooking && !writer.Asset.IsFilterEditorOnly)
        {
            writer.Write(TempGUID.ToByteArray());
        }

        return (int)(writer.BaseStream.Position - here);
    }

    public override void FromString(string[] d, UAsset asset)
    {
        DisplayName = FName.FromString(asset, d[0]);
        if (ushort.TryParse(d[1], out ushort rawSmartNameID)) SmartNameID = rawSmartNameID;
        TempGUID = d[2].ConvertToGUID();
    }

    public override string ToString()
    {
        return "(" + ")";
    }
}
