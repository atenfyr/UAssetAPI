using System;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.PropertyTypes.Objects;

/// <summary>
/// Describes a set.
/// </summary>
public class SetPropertyData : ArrayPropertyData
{
    public PropertyData[] ElementsToRemove = null;

    public SetPropertyData(FName name) : base(name)
    {
        Value = [];
        ElementsToRemove = [];
    }

    public SetPropertyData()
    {
        Value = [];
        ElementsToRemove = [];
    }

    private static readonly FString CurrentPropertyType = new FString("SetProperty");
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        ShouldSerializeStructsDifferently = false;

        if (includeHeader && !reader.Asset.HasUnversionedProperties)
        {
            ArrayType = reader.ReadFName();
            this.ReadEndPropertyTag(reader);
        }

        if (reader.Asset.Mappings != null && ArrayType == null && reader.Asset.Mappings.TryGetPropertyData(Name, Ancestry, reader.Asset, out UsmapArrayData strucDat1))
        {
            ArrayType = FName.DefineDummy(reader.Asset, strucDat1.InnerType.Type.ToString());
        }

        if (reader.Asset.HasUnversionedProperties && ArrayType == null)
        {
            throw new InvalidOperationException("Unable to determine array type for array " + Name.Value.Value + " in class " + Ancestry.Parent.Value.Value);
        }

        var removedItemsDummy = new ArrayPropertyData(FName.DefineDummy(reader.Asset, "ElementsToRemove"));
        removedItemsDummy.Ancestry.Initialize(Ancestry, Name);
        removedItemsDummy.ShouldSerializeStructsDifferently = false;
        removedItemsDummy.ArrayType = ArrayType;
        removedItemsDummy.Read(reader, false, leng1, leng2);
        ElementsToRemove = removedItemsDummy.Value;

        base.Read(reader, false, leng1-4, leng2);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        ShouldSerializeStructsDifferently = false;

        if (Value.Length > 0) ArrayType = writer.Asset.HasUnversionedProperties ? FName.DefineDummy(writer.Asset, Value[0].PropertyType) : new FName(writer.Asset, Value[0].PropertyType);

        if (includeHeader && !writer.Asset.HasUnversionedProperties)
        {
            writer.Write(ArrayType);
            this.WriteEndPropertyTag(writer);
        }

        var removedItemsDummy = new ArrayPropertyData(FName.DefineDummy(writer.Asset, "ElementsToRemove"));
        removedItemsDummy.ShouldSerializeStructsDifferently = false;
        removedItemsDummy.ArrayType = ArrayType;
        removedItemsDummy.Value = ElementsToRemove;

        int leng1 = removedItemsDummy.Write(writer, false);
        return leng1 + base.Write(writer, false);
    }

    protected override void HandleCloned(PropertyData res)
    {
        base.HandleCloned(res);
        SetPropertyData cloningProperty = (SetPropertyData)res;

        if (this.ElementsToRemove != null)
        {
            PropertyData[] newData = new PropertyData[this.ElementsToRemove.Length];
            for (int i = 0; i < this.Value.Length; i++)
            {
                newData[i] = (PropertyData)this.Value[i].Clone();
            }
            cloningProperty.ElementsToRemove = newData;
        }
        else
        {
            cloningProperty.ElementsToRemove = null;
        }
    }
}
