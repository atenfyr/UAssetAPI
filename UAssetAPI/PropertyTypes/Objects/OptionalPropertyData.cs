using Newtonsoft.Json;
using System;
using UAssetAPI.PropertyTypes.Structs;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.PropertyTypes.Objects;

/// <summary>
/// Describes an optional property.
/// </summary>
public class OptionalPropertyData : PropertyData<PropertyData>
{
    [JsonProperty]
    public FName InnerType;
    [JsonProperty]
    public FName InnerStructType;

    public OptionalPropertyData(FName name) : base(name) { }

    public OptionalPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("OptionalProperty");
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader && !reader.Asset.HasUnversionedProperties)
        {
            if (reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.PROPERTY_TAG_COMPLETE_TYPE_NAME)
            {
                if (PropertyTypeName is null) throw new FormatException("PropertyTypeName is required to read OptionalProperty with complete type names.");
                InnerType = PropertyTypeName.GetParameter(0).GetName();
                if (InnerType.Value.Value == "StructProperty") InnerStructType = PropertyTypeName.GetParameter(0).GetParameter(0).GetName();
            }
            else
            {
                InnerType = reader.ReadFName();
            }

            this.ReadEndPropertyTag(reader);
        }

        if (reader.Asset.Mappings != null && InnerType == null && reader.Asset.Mappings.TryGetPropertyData(Name, Ancestry, reader.Asset, out UsmapArrayData optDat))
        {
            InnerType = FName.DefineDummy(reader.Asset, optDat.InnerType.Type.ToString());
            if (optDat.InnerType is UsmapStructData strucDat) InnerStructType = FName.DefineDummy(reader.Asset, strucDat.StructType);
        }

        if (InnerType == null)
        {
            throw new InvalidOperationException("Unable to determine inner type for optional " + Name.Value.Value + " in class " + Ancestry.Parent.Value.Value);
        }

        var bIsSet = reader.ReadBooleanInt();
        if (!bIsSet)
        {
            Value = null;
            return;
        }

        var sizeEstimate = leng1 - 4;
        if (sizeEstimate <= 0) sizeEstimate = 1;

        var propTypeName = PropertyTypeName?.GetParameter(0);
        Value = MainSerializer.TypeToClass(InnerType, FName.DefineDummy(reader.Asset, "0", int.MinValue), Ancestry, Name, null, reader.Asset, propertyTypeName: propTypeName);
        Value.Offset = reader.BaseStream.Position;
        if (Value is StructPropertyData data) data.StructType = InnerStructType ?? FName.DefineDummy(reader.Asset, "Generic");
        Value.Read(reader, false, sizeEstimate, 0, PropertySerializationContext.Array);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (Value != null)
        {
            InnerType = writer.Asset.HasUnversionedProperties ? FName.DefineDummy(writer.Asset, Value.PropertyType) : new FName(writer.Asset, Value.PropertyType);
            if (Value is StructPropertyData s) InnerStructType = s.StructType;
        }

        if (includeHeader && !writer.Asset.HasUnversionedProperties)
        {
            if (writer.Asset.ObjectVersionUE5 < ObjectVersionUE5.PROPERTY_TAG_COMPLETE_TYPE_NAME)
            {
                writer.Write(InnerType);
            }
            this.WriteEndPropertyTag(writer);
        }

        int here = (int)writer.BaseStream.Position;
        writer.WriteBooleanInt(Value != null);
        if (Value != null)
        {
            if (serializationContext == PropertySerializationContext.CanBeZero && ((CanBeZeroStream)writer.BaseStream).HasWrittenNonZero) return -1;
            Value.Offset = writer.BaseStream.Position;
            Value.Write(writer, false, PropertySerializationContext.Array);
        }

        return (int)writer.BaseStream.Position - here;
    }

    public override void ResolveAncestries(UAsset asset, AncestryInfo ancestrySoFar)
    {
        var ancestryNew = (AncestryInfo)ancestrySoFar.Clone();
        ancestryNew.SetAsParent(Name);
        Value?.ResolveAncestries(asset, ancestryNew);
        base.ResolveAncestries(asset, ancestrySoFar);
    }

    public override void FromString(string[] d, UAsset asset)
    {
        if (d[4] != null) InnerType = FName.FromString(asset, d[4]);
        if (!string.IsNullOrWhiteSpace(d[0])) InnerStructType = FName.FromString(asset, d[0]);
    }

    protected override void HandleCloned(PropertyData res)
    {
        var cloning = (OptionalPropertyData)res;
        cloning.Value = (PropertyData)Value?.Clone();
        cloning.InnerType = (FName)InnerType?.Clone();
        cloning.InnerStructType = (FName)InnerStructType?.Clone();
    }
}
