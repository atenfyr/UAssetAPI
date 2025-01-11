using Newtonsoft.Json;
using System;
using System.IO;
using UAssetAPI.PropertyTypes.Structs;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.PropertyTypes.Objects;

/// <summary>
/// Describes an array.
/// </summary>
public class ArrayPropertyData : PropertyData<PropertyData[]>
{
    [JsonProperty]
    public FName ArrayType;
    [JsonProperty]
    public StructPropertyData DummyStruct;

    internal bool ShouldSerializeStructsDifferently = true;

    public bool ShouldSerializeDummyStruct()
    {
        return Value.Length == 0;
    }

    public ArrayPropertyData(FName name) : base(name)
    {
        Value = [];
    }

    public ArrayPropertyData()
    {
        Value = [];
    }

    private static readonly FString CurrentPropertyType = new FString("ArrayProperty");
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader && !reader.Asset.HasUnversionedProperties)
        {
            ArrayType = reader.ReadFName();
            this.ReadEndPropertyTag(reader);
        }

        FName arrayStructType = null;
        if (reader.Asset.Mappings != null && ArrayType == null && reader.Asset.Mappings.TryGetPropertyData(Name, Ancestry, reader.Asset, out UsmapArrayData strucDat1))
        {
            ArrayType = FName.DefineDummy(reader.Asset, strucDat1.InnerType.Type.ToString());
            if (strucDat1.InnerType is UsmapStructData strucDat2) arrayStructType = FName.DefineDummy(reader.Asset, strucDat2.StructType);
        }

        if (reader.Asset.HasUnversionedProperties && ArrayType == null)
        {
            throw new InvalidOperationException("Unable to determine array type for array " + Name.Value.Value + " in class " + Ancestry.Parent.Value.Value);
        }

        int numEntries = reader.ReadInt32();
        if (ArrayType.Value.Value == "StructProperty" && ShouldSerializeStructsDifferently && !reader.Asset.HasUnversionedProperties)
        {
            var results = new PropertyData[numEntries];

            FName name = this.Name;
            long structLength = 1;
            FName fullType = FName.DefineDummy(reader.Asset, "Generic");
            Guid structGUID = new Guid();

            bool isSpecialCase = false;
            if (reader.Asset is UAsset asset)
            {
                isSpecialCase = reader.Asset.ObjectVersion == ObjectVersion.VER_UE4_INNER_ARRAY_TAG_INFO && asset.WillSerializeNameHashes == true;
            }

            if (reader.Asset.ObjectVersion >= ObjectVersion.VER_UE4_INNER_ARRAY_TAG_INFO && !isSpecialCase)
            {
                name = reader.ReadFName();
                if (name.Value.Value.Equals("None"))
                {
                    Value = results;
                    return;
                }

                FName thisArrayType = reader.ReadFName();
                if (thisArrayType.Value.Value.Equals("None"))
                {
                    Value = results;
                    return;
                }

                if (thisArrayType.Value.Value != ArrayType.Value.Value) throw new FormatException("Invalid array type: " + thisArrayType.ToString() + " vs " + ArrayType.ToString());

                structLength = reader.ReadInt64(); // length value
                fullType = reader.ReadFName();
                structGUID = new Guid(reader.ReadBytes(16));
                reader.ReadPropertyGuid();
            }
            else
            {
                // override using mappings or ArrayStructTypeOverride if applicable
                if (arrayStructType != null)
                {
                    fullType = arrayStructType;
                }
                else if (reader.Asset.ArrayStructTypeOverride.ContainsKey(this.Name.Value.Value))
                {
                    fullType = FName.DefineDummy(reader.Asset, reader.Asset.ArrayStructTypeOverride[this.Name.Value.Value]);
                }
            }

            if (numEntries == 0)
            {
                DummyStruct = new StructPropertyData(name, fullType)
                {
                    StructGUID = structGUID
                };
            }
            else
            {
                for (int i = 0; i < numEntries; i++)
                {
                    var data = new StructPropertyData(name, fullType);
                    data.Offset = reader.BaseStream.Position;
                    data.Ancestry.Initialize(Ancestry, Name);
                    data.Read(reader, false, structLength, 0, PropertySerializationContext.Array);
                    data.StructGUID = structGUID;
                    results[i] = data;
                }
                DummyStruct = (StructPropertyData)results[0];
            }
            Value = results;
        }
        else
        {
            if (numEntries == 0)
            {
                Value = [];
                return;
            }

            int averageSizeEstimate = (int)((leng1 - 4) / numEntries);
            if (averageSizeEstimate <= 0) averageSizeEstimate = 1; // missing possible edge case where leng1 = 4 and numEntries = 2, may result is wrong size

            var results = new PropertyData[numEntries];
            for (int i = 0; i < numEntries; i++)
            {
                results[i] = MainSerializer.TypeToClass(ArrayType, FName.DefineDummy(reader.Asset, i.ToString(), int.MinValue), Ancestry, Name, null, reader.Asset);
                results[i].Offset = reader.BaseStream.Position;
                if (results[i] is StructPropertyData data) data.StructType = arrayStructType == null ? FName.DefineDummy(reader.Asset, "Generic") : arrayStructType;
                results[i].Read(reader, false, averageSizeEstimate, 0, PropertySerializationContext.Array);
            }

            Value = results;
        }
    }

    public override void ResolveAncestries(UAsset asset, AncestryInfo ancestrySoFar)
    {
        var ancestryNew = (AncestryInfo)ancestrySoFar.Clone();
        ancestryNew.SetAsParent(Name);

        if (Value != null)
        {
            for (int i = 0; i < Value.Length; i++) Value[i].ResolveAncestries(asset, ancestryNew);
        }
        base.ResolveAncestries(asset, ancestrySoFar);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (Value == null) Value = [];

        // let's get our ArrayType if we don't already know it
        // if Value has entries, take it from there
        if (Value.Length > 0)
        {
            ArrayType = writer.Asset.HasUnversionedProperties ? FName.DefineDummy(writer.Asset, Value[0].PropertyType) : new FName(writer.Asset, Value[0].PropertyType);
        }

        // otherwise, let's check mappings
        FName arrayStructType = null;
        if (writer.Asset.Mappings != null && ArrayType == null && writer.Asset.Mappings.TryGetPropertyData(Name, Ancestry, writer.Asset, out UsmapArrayData strucDat1))
        {
            ArrayType = FName.DefineDummy(writer.Asset, strucDat1.InnerType.Type.ToString());
            if (strucDat1.InnerType is UsmapStructData strucDat2) arrayStructType = FName.DefineDummy(writer.Asset, strucDat2.StructType);
        }

        // at this point, if we couldn't get our ArrayType and we're using unversioned properties, we can't continue; otherwise, not needed
        if (writer.Asset.HasUnversionedProperties && ArrayType == null)
        {
            throw new InvalidOperationException("Unable to determine array type for array " + Name.Value.Value + " in class " + Ancestry.Parent.Value.Value);
        }

        // begin actual serialization
        if (includeHeader && !writer.Asset.HasUnversionedProperties)
        {
            writer.Write(ArrayType);
            this.WriteEndPropertyTag(writer);
        }

        int here = (int)writer.BaseStream.Position;
        writer.Write(Value.Length);
        if (ArrayType?.Value?.Value == "StructProperty" && ShouldSerializeStructsDifferently && !writer.Asset.HasUnversionedProperties)
        {
            if (Value.Length == 0 && DummyStruct == null)
            {
                // let's try and reconstruct DummyStruct, if we can
                if (arrayStructType == null && writer.Asset.ArrayStructTypeOverride.ContainsKey(this.Name.Value.Value))
                {
                    arrayStructType = FName.DefineDummy(writer.Asset, writer.Asset.ArrayStructTypeOverride[this.Name.Value.Value]);
                }

                if (arrayStructType == null)
                {
                    throw new InvalidOperationException("Unable to reconstruct DummyStruct within empty StructProperty array " + Name.Value.Value + " in class " + Ancestry.Parent.Value.Value);
                }

                DummyStruct = new StructPropertyData(this.Name, arrayStructType)
                {
                    StructGUID = Guid.Empty // not sure what else to use here...
                };
            }
            if (Value.Length > 0) DummyStruct = (StructPropertyData)Value[0];

            FName fullType = DummyStruct.StructType;

            int lengthLoc = -1;

            bool isSpecialCase = false;
            if (writer.Asset is UAsset)
            {
                isSpecialCase = writer.Asset.ObjectVersion == ObjectVersion.VER_UE4_INNER_ARRAY_TAG_INFO && ((UAsset)writer.Asset).WillSerializeNameHashes == true;
            }

            if (writer.Asset.ObjectVersion >= ObjectVersion.VER_UE4_INNER_ARRAY_TAG_INFO && !isSpecialCase)
            {
                writer.Write(DummyStruct.Name);
                writer.Write(new FName(writer.Asset, "StructProperty"));
                lengthLoc = (int)writer.BaseStream.Position;
                writer.Write((long)0);
                writer.Write(fullType);
                if (writer.Asset.ObjectVersion >= ObjectVersion.VER_UE4_STRUCT_GUID_IN_PROPERTY_TAG) writer.Write(DummyStruct.StructGUID.ToByteArray());
                if (writer.Asset.ObjectVersion >= ObjectVersion.VER_UE4_PROPERTY_GUID_IN_PROPERTY_TAG) writer.Write((byte)0);
            }

            for (int i = 0; i < Value.Length; i++)
            {
                ((StructPropertyData)Value[i]).StructType = fullType;
                Value[i].Offset = writer.BaseStream.Position;
                Value[i].Write(writer, false);
            }

            if (writer.Asset.ObjectVersion >= ObjectVersion.VER_UE4_INNER_ARRAY_TAG_INFO && !isSpecialCase)
            {
                int fullLen = (int)writer.BaseStream.Position - lengthLoc;
                int newLoc = (int)writer.BaseStream.Position;
                writer.Seek(lengthLoc, SeekOrigin.Begin);
                writer.Write(fullLen - 32 - (includeHeader ? 1 : 0));
                writer.Seek(newLoc, SeekOrigin.Begin);
            }
        }
        else
        {
            for (int i = 0; i < Value.Length; i++)
            {
                Value[i].Offset = writer.BaseStream.Position;
                Value[i].Write(writer, false, PropertySerializationContext.Array);
            }
        }

        return (int)writer.BaseStream.Position - here;
    }

    public override void FromString(string[] d, UAsset asset)
    {
        if (d[4] != null) ArrayType = FName.FromString(asset, d[4]);
    }

    protected override void HandleCloned(PropertyData res)
    {
        ArrayPropertyData cloningProperty = (ArrayPropertyData)res;
        cloningProperty.ArrayType = (FName)this.ArrayType?.Clone();
        cloningProperty.DummyStruct = (StructPropertyData)this.DummyStruct?.Clone();
    }
}