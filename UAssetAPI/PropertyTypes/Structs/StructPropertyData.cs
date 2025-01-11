using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UAssetAPI.CustomVersions;
using UAssetAPI.ExportTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.PropertyTypes.Structs;

public class StructPropertyData : PropertyData<List<PropertyData>>
{
    [JsonProperty]
    public FName StructType = null;
    [JsonProperty]
    public bool SerializeNone = true;
    [JsonProperty]
    public Guid StructGUID = Guid.Empty; // usually set to 0
    [JsonProperty]
    public EClassSerializationControlExtension SerializationControl;
    [JsonProperty]
    public EOverriddenPropertyOperation Operation;

    /// <summary>
    /// Gets or sets the value associated with the specified key. This operation loops linearly, so it may not be suitable for high-performance environments.
    /// </summary>
    /// <param name="key">The key associated with the value to get or set.</param>
    public virtual PropertyData this[FName key]
    {
        get
        {
            if (Value == null) return null;

            for (int i = 0; i < Value.Count; i++)
            {
                if (Value[i].Name == key) return Value[i];
            }
            return null;
        }
        set
        {
            if (Value == null) Value = [];
            value.Name = key;

            for (int i = 0; i < Value.Count; i++)
            {
                if (Value[i].Name == key)
                {
                    Value[i] = value;
                    return;
                }
            }

            Value.Add(value);
        }
    }

    /// <summary>
    /// Gets or sets the value associated with the specified key. This operation loops linearly, so it may not be suitable for high-performance environments.
    /// </summary>
    /// <param name="key">The key associated with the value to get or set.</param>
    public virtual PropertyData this[string key]
    {
        get
        {
            return this[FName.FromString(Name?.Asset, key)];
        }
        set
        {
            this[FName.FromString(Name?.Asset, key)] = value;
        }
    }

    public StructPropertyData(FName name) : base(name)
    {
        Value = [];
    }

    public StructPropertyData(FName name, FName forcedType) : base(name)
    {
        StructType = forcedType;
        Value = [];
    }

    public StructPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("StructProperty");
    public override FString PropertyType => CurrentPropertyType;

    private void ReadOnce(AssetBinaryReader reader, Type T, long offset, long leng1)
    {
        if (Activator.CreateInstance(T, Name) is not PropertyData data) return;
        data.Offset = offset;
        data.Ancestry.Initialize(Ancestry, Name);
        data.Read(reader, false, leng1);
        Value = new List<PropertyData> { data };
    }

    private void ReadNTPL(AssetBinaryReader reader, bool resetValue = true)
    {
        List<PropertyData> resultingList = resetValue ? new List<PropertyData>() : Value;
        PropertyData data = null;

        var unversionedHeader = new FUnversionedHeader(reader);
        if (!reader.Asset.HasUnversionedProperties && reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.PROPERTY_TAG_EXTENSION_AND_OVERRIDABLE_SERIALIZATION)
        {
            SerializationControl = (EClassSerializationControlExtension)reader.ReadByte();

            if (SerializationControl.HasFlag(EClassSerializationControlExtension.OverridableSerializationInformation))
            {
                Operation = (EOverriddenPropertyOperation)reader.ReadByte();
            }
        }
        while ((data = MainSerializer.Read(reader, Ancestry, StructType, FName.DefineDummy(reader.Asset, reader.Asset.InternalAssetPath + ((Ancestry?.Ancestors?.Count ?? 0) == 0 ? string.Empty : ("." + Ancestry.Parent))), unversionedHeader, true)) != null)
        {
            resultingList.Add(data);
        }

        Value = resultingList;
    }

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader && !reader.Asset.HasUnversionedProperties) // originally !isForced
        {
            StructType = reader.ReadFName();
            if (reader.Asset.ObjectVersion >= ObjectVersion.VER_UE4_STRUCT_GUID_IN_PROPERTY_TAG) StructGUID = new Guid(reader.ReadBytes(16));
            this.ReadEndPropertyTag(reader);
        }

        if (reader.Asset.Mappings != null && (StructType == null || StructType.Value.Value == "Generic") && reader.Asset.Mappings.TryGetPropertyData(Name, Ancestry, reader.Asset, out UsmapStructData strucDat1))
        {
            StructType = FName.DefineDummy(reader.Asset, strucDat1.StructType);
        }

        if (reader.Asset.HasUnversionedProperties && StructType?.Value?.Value == null)
        {
            throw new InvalidOperationException("Unable to determine struct type for struct " + Name.Value.Value + " in class " + Ancestry.Parent.Value.Value);
        }

        RegistryEntry targetEntry = null;
        string structTypeVal = StructType?.Value?.Value;
        if (structTypeVal != null) MainSerializer.PropertyTypeRegistry.TryGetValue(structTypeVal, out targetEntry);
        bool hasCustomStructSerialization = targetEntry != null && targetEntry.HasCustomStructSerialization && serializationContext != PropertySerializationContext.StructFallback;

        if (structTypeVal == "FloatRange")
        {
            // FloatRange is a special case; it can either be manually serialized as two floats (TRange<float>) or as a regular struct (FFloatRange), but the first is overridden to use the same name as the second
            // The best solution is to just check and see if the next bit is an FName or not

            int nextFourBytes = reader.ReadInt32();
            reader.BaseStream.Position -= sizeof(int);
            hasCustomStructSerialization = !(reader.Asset.HasUnversionedProperties || (nextFourBytes >= 0 && nextFourBytes < reader.Asset.GetNameMapIndexList().Count && reader.Asset.GetNameReference(nextFourBytes).Value.EndsWith("Bound")));
        }
        if (structTypeVal == "RichCurveKey" && reader.Asset.ObjectVersion < ObjectVersion.VER_UE4_SERIALIZE_RICH_CURVE_KEY) hasCustomStructSerialization = false;
        if (structTypeVal == "MovieSceneTrackIdentifier" && reader.Asset.GetCustomVersion<FEditorObjectVersion>() < FEditorObjectVersion.MovieSceneMetaDataSerialization) hasCustomStructSerialization = false;
        if (structTypeVal == "MovieSceneFloatChannel" && reader.Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.SerializeFloatChannelCompletely) hasCustomStructSerialization = false;
        if (structTypeVal == "MovieSceneFloatValue" && reader.Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.SerializeFloatChannel) hasCustomStructSerialization = false;
        if (structTypeVal == "MovieSceneTangentData" && reader.Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.SerializeFloatChannel) hasCustomStructSerialization = false;
        if (structTypeVal == "FontData" && reader.Asset.GetCustomVersion<FEditorObjectVersion>() < FEditorObjectVersion.AddedFontFaceAssets) hasCustomStructSerialization = false;

        if (leng1 == 0)
        {
            SerializeNone = false;
            Value = [];
            return;
        }

        if (targetEntry != null && hasCustomStructSerialization)
        {
            ReadOnce(reader, targetEntry.PropertyType, reader.BaseStream.Position, leng1);
        }
        else
        {
            ReadNTPL(reader);
        }
    }

    public override void ResolveAncestries(UAsset asset, AncestryInfo ancestrySoFar)
    {
        var ancestryNew = (AncestryInfo)ancestrySoFar.Clone();
        ancestryNew.SetAsParent(StructType, FName.DefineDummy(asset, asset.InternalAssetPath + (ancestrySoFar.Ancestors.Count == 0 ? string.Empty : ("." + ancestrySoFar.Parent))));

        if (Value != null)
        {
            foreach (var entry in Value) entry?.ResolveAncestries(asset, ancestryNew);
        }
        base.ResolveAncestries(asset, ancestrySoFar);
    }

    private int WriteOnce(AssetBinaryWriter writer)
    {
        if (Value.Count > 1) throw new InvalidOperationException("Structs with type " + StructType.Value.Value + " cannot have more than one entry");

        if (Value.Count == 0)
        {
            // populate fallback zero entry 
            if (Value == null) Value = new List<PropertyData>();
            Value.Clear();
            Value.Add(MainSerializer.TypeToClass(StructType, Name, Ancestry, Name, null, writer.Asset, null, 0, EPropertyTagFlags.None, 0, false));
        }
        Value[0].Offset = writer.BaseStream.Position;
        return Value[0].Write(writer, false);
    }

    private int WriteNTPL(AssetBinaryWriter writer)
    {
        int here = (int)writer.BaseStream.Position;

        List<PropertyData> allDat = Value;
        MainSerializer.GenerateUnversionedHeader(ref allDat, StructType, FName.DefineDummy(writer.Asset, writer.Asset.InternalAssetPath + ((Ancestry?.Ancestors?.Count ?? 0) == 0 ? string.Empty : ("." + Ancestry.Parent))), writer.Asset)?.Write(writer);
        foreach (var t in allDat)
        {
            MainSerializer.Write(t, writer, true);
        }
        if (!writer.Asset.HasUnversionedProperties) writer.Write(new FName(writer.Asset, "None"));
        return (int)writer.BaseStream.Position - here;
    }

    internal bool DetermineIfSerializeWithCustomStructSerialization(UAsset Asset, PropertySerializationContext serializationContext, out RegistryEntry targetEntry)
    {
        targetEntry = null;
        string structTypeVal = StructType?.Value?.Value;
        if (structTypeVal != null) MainSerializer.PropertyTypeRegistry.TryGetValue(structTypeVal, out targetEntry);
        bool hasCustomStructSerialization = targetEntry != null && targetEntry.HasCustomStructSerialization && serializationContext != PropertySerializationContext.StructFallback;

        if (structTypeVal == "FloatRange") hasCustomStructSerialization = Value.Count == 1 && Value[0] is FloatRangePropertyData;
        if (structTypeVal == "RichCurveKey" && Asset.ObjectVersion < ObjectVersion.VER_UE4_SERIALIZE_RICH_CURVE_KEY) hasCustomStructSerialization = false;
        if (structTypeVal == "MovieSceneTrackIdentifier" && Asset.GetCustomVersion<FEditorObjectVersion>() < FEditorObjectVersion.MovieSceneMetaDataSerialization) hasCustomStructSerialization = false;
        if (structTypeVal == "MovieSceneFloatChannel" && Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.SerializeFloatChannelCompletely) hasCustomStructSerialization = false;
        if (structTypeVal == "MovieSceneFloatValue" && Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.SerializeFloatChannel) hasCustomStructSerialization = false;
        if (structTypeVal == "MovieSceneTangentData" && Asset.GetCustomVersion<FSequencerObjectVersion>() < FSequencerObjectVersion.SerializeFloatChannel) hasCustomStructSerialization = false;
        if (structTypeVal == "FontData" && Asset.GetCustomVersion<FEditorObjectVersion>() < FEditorObjectVersion.AddedFontFaceAssets) hasCustomStructSerialization = false;
        return hasCustomStructSerialization;
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        // if available and needed, fetch StructType from mappings
        if (writer.Asset.Mappings != null && (StructType == null || StructType.Value.Value == "Generic") && writer.Asset.Mappings.TryGetPropertyData(Name, Ancestry, writer.Asset, out UsmapStructData strucDat1))
        {
            StructType = FName.DefineDummy(writer.Asset, strucDat1.StructType);
        }

        if (includeHeader && !writer.Asset.HasUnversionedProperties)
        {
            writer.Write(StructType);
            if (writer.Asset.ObjectVersion >= ObjectVersion.VER_UE4_STRUCT_GUID_IN_PROPERTY_TAG) writer.Write(StructGUID.ToByteArray());
            this.WriteEndPropertyTag(writer);
        }

        if (Value == null) Value = [];

        bool hasCustomStructSerialization = DetermineIfSerializeWithCustomStructSerialization(writer.Asset, serializationContext, out RegistryEntry targetEntry);
        if (targetEntry != null && hasCustomStructSerialization) return WriteOnce(writer);
        if (Value.Count == 0 && !SerializeNone) return 0;
        return WriteNTPL(writer);
    }

    // removed; we'll actually just use default PropertyData alg

    /*public override bool CanBeZero(UAsset asset)
    {
        if (StructType?.Value?.Value == "Guid")
        {
            return base.CanBeZero(asset);
        }
        return !DetermineIfSerializeWithCustomStructSerialization(asset, out _, out __) && base.CanBeZero(asset);
    }*/

    public override void FromString(string[] d, UAsset asset)
    {
        if (d[4] != null && d[4] != "Generic") StructType = asset.HasUnversionedProperties ? FName.DefineDummy(asset, d[4]) : FName.FromString(asset, d[4]);
        if (StructType == null) StructType = FName.DefineDummy(asset, "Generic");
    }

    protected override void HandleCloned(PropertyData res)
    {
        StructPropertyData cloningProperty = (StructPropertyData)res;
        cloningProperty.StructType = (FName)this.StructType?.Clone();
        cloningProperty.StructGUID = new Guid(this.StructGUID.ToByteArray());

        if (this.Value != null)
        {
            List<PropertyData> newData = new List<PropertyData>(this.Value.Count);
            for (int i = 0; i < this.Value.Count; i++)
            {
                newData.Add((PropertyData)this.Value[i].Clone());
            }
            cloningProperty.Value = newData;
        }
        else
        {
            cloningProperty.Value = null;
        }
    }
}