using Newtonsoft.Json;
using System.Linq;
using UAssetAPI.JSON;
using UAssetAPI.PropertyTypes.Structs;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;

namespace UAssetAPI.PropertyTypes.Objects
{
    /// <summary>
    /// Describes a map.
    /// </summary>
    public class MapPropertyData : PropertyData
    {
        /// <summary>
        /// The map that this property represents.
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(TMapJsonConverter<PropertyData, PropertyData>))]
        public TMap<PropertyData, PropertyData> Value;

        /// <summary>
        /// Used when the length of the map is zero.
        /// </summary>]
        [JsonProperty]
        public FName KeyType;

        /// <summary>
        /// Used when the length of the map is zero.
        /// </summary>
        [JsonProperty]
        public FName ValueType;

        public bool ShouldSerializeKeyType()
        {
            return Value.Count == 0;
        }

        public bool ShouldSerializeValueType()
        {
            return Value.Count == 0;
        }

        [JsonProperty]
        public PropertyData[] KeysToRemove = null;

        public MapPropertyData(FName name) : base(name)
        {
            Value = new TMap<PropertyData, PropertyData>();
        }

        public MapPropertyData()
        {
            Value = new TMap<PropertyData, PropertyData>();
        }

        private static readonly FString CurrentPropertyType = new FString("MapProperty");
        public override FString PropertyType { get { return CurrentPropertyType; } }

        private PropertyData MapTypeToClass(FName type, FName name, AssetBinaryReader reader, int leng, bool includeHeader, bool isKey)
        {
            switch (type.Value.Value)
            {
                case "StructProperty":
                    FName strucType = null;

                    if (reader.Asset.Mappings != null && reader.Asset.Mappings.TryGetPropertyData(Name, Ancestry, reader.Asset, out UsmapMapData mapDat))
                    {
                        if (isKey && mapDat.InnerType is UsmapStructData strucDat1)
                        {
                            strucType = FName.DefineDummy(reader.Asset, strucDat1.StructType);
                        }
                        else if (mapDat.ValueType is UsmapStructData strucDat2)
                        {
                            strucType = FName.DefineDummy(reader.Asset, strucDat2.StructType);
                        }
                    }
                    else if (reader.Asset.MapStructTypeOverride.ContainsKey(name.Value.Value))
                    {
                        if (isKey)
                        {
                            strucType = FName.DefineDummy(reader.Asset, reader.Asset.MapStructTypeOverride[name.Value.Value].Item1);
                        }
                        else
                        {
                            strucType = FName.DefineDummy(reader.Asset, reader.Asset.MapStructTypeOverride[name.Value.Value].Item2);
                            if (name.Value.Value == "TrackSignatureToTrackIdentifier" && reader.Asset.GetEngineVersion() <= EngineVersion.VER_UE4_18)
                                strucType = FName.DefineDummy(reader.Asset, "Generic");
                        }
                    }

                    if (strucType?.Value == null) strucType = FName.DefineDummy(reader.Asset, "Generic");

                    StructPropertyData data = new StructPropertyData(name, strucType);
                    data.Ancestry.Initialize(Ancestry, Name);
                    data.Offset = reader.BaseStream.Position;
                    data.Read(reader, false, 1, 0, PropertySerializationContext.Map);
                    return data;
                default:
                    var res = MainSerializer.TypeToClass(type, name, Ancestry, Name, null, reader.Asset, null, leng);
                    res.Ancestry.Initialize(Ancestry, Name);
                    res.Read(reader, includeHeader, leng, 0, PropertySerializationContext.Map);
                    return res;
            }
        }

        private TMap<PropertyData, PropertyData> ReadRawMap(AssetBinaryReader reader, FName type1, FName type2, int numEntries)
        {
            var resultingDict = new TMap<PropertyData, PropertyData>();

            PropertyData data1 = null;
            PropertyData data2 = null;
            for (int i = 0; i < numEntries; i++)
            {
                data1 = MapTypeToClass(type1, Name, reader, 0, false, true);
                data2 = MapTypeToClass(type2, Name, reader, 0, false, false);

                resultingDict.Add(data1, data2);
            }

            return resultingDict;
        }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            FName type1 = null, type2 = null;
            if (includeHeader && !reader.Asset.HasUnversionedProperties)
            {
                type1 = reader.ReadFName();
                type2 = reader.ReadFName();
                this.ReadEndPropertyTag(reader);
            }

            if (reader.Asset.Mappings != null && type1 == null && type2 == null && reader.Asset.Mappings.TryGetPropertyData(Name, Ancestry, reader.Asset, out UsmapMapData strucDat1))
            {
                type1 = FName.DefineDummy(reader.Asset, strucDat1.InnerType.Type.ToString());
                type2 = FName.DefineDummy(reader.Asset, strucDat1.ValueType.Type.ToString());
            }

            int numKeysToRemove = reader.ReadInt32();
            KeysToRemove = new PropertyData[numKeysToRemove];
            for (int i = 0; i < numKeysToRemove; i++)
            {
                KeysToRemove[i] = MapTypeToClass(type1, Name, reader, 0, false, true);
            }

            int numEntries = reader.ReadInt32();
            if (numEntries == 0)
            {
                KeyType = type1;
                ValueType = type2;
            }

            Value = ReadRawMap(reader, type1, type2, numEntries);
        }

        public override void ResolveAncestries(UAsset asset, AncestryInfo ancestrySoFar)
        {
            var ancestryNew = (AncestryInfo)ancestrySoFar.Clone();
            ancestryNew.SetAsParent(Name);

            if (Value != null)
            {
                foreach (var entry in Value)
                {
                    entry.Key.ResolveAncestries(asset, ancestryNew);
                    entry.Value.ResolveAncestries(asset, ancestryNew);
                }
            }
            base.ResolveAncestries(asset, ancestrySoFar);
        }

        private void WriteRawMap(AssetBinaryWriter writer, TMap<PropertyData, PropertyData> map)
        {
            if (map == null) return;
            foreach (var entry in map)
            {
                entry.Key.Offset = writer.BaseStream.Position;
                entry.Key.Write(writer, false, PropertySerializationContext.Map);
                entry.Value.Offset = writer.BaseStream.Position;
                entry.Value.Write(writer, false, PropertySerializationContext.Map);
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
        {
            if (includeHeader && !writer.Asset.HasUnversionedProperties)
            {
                if (Value.Count > 0)
                {
                    writer.Write(new FName(writer.Asset, Value.Keys.ElementAt(0).PropertyType));
                    writer.Write(new FName(writer.Asset, Value[0].PropertyType));
                }
                else
                {
                    writer.Write(KeyType);
                    writer.Write(ValueType);
                }
                this.WriteEndPropertyTag(writer);
            }

            int here = (int)writer.BaseStream.Position;
            writer.Write(KeysToRemove?.Length ?? 0);
            if (KeysToRemove != null)
            {
                for (int i = 0; i < KeysToRemove.Length; i++)
                {
                    var entry = KeysToRemove[i];
                    entry.Offset = writer.BaseStream.Position;
                    entry.Write(writer, false, PropertySerializationContext.Array);
                }
            }

            writer.Write(Value?.Count ?? 0);
            WriteRawMap(writer, Value);
            return (int)writer.BaseStream.Position - here;
        }

        protected override void HandleCloned(PropertyData res)
        {
            MapPropertyData cloningProperty = (MapPropertyData)res;

            if (this.Value != null)
            {
                var newDict = new TMap<PropertyData, PropertyData>();
                foreach (var entry in this.Value)
                {
                    newDict[(PropertyData)entry.Key.Clone()] = (PropertyData)entry.Value.Clone();
                }
                cloningProperty.Value = newDict;
            }
            else
            {
                cloningProperty.Value = null;
            }

            cloningProperty.KeysToRemove = (PropertyData[])this.KeysToRemove?.Clone();

            cloningProperty.KeyType = (FName)this.KeyType?.Clone();
            cloningProperty.ValueType = (FName)this.ValueType?.Clone();
        }
    }
}