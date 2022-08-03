using Newtonsoft.Json;
using System.Linq;
using UAssetAPI.JSON;
using UAssetAPI.PropertyTypes.Structs;
using UAssetAPI.UnrealTypes;

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

                    if (reader.Asset.MapStructTypeOverride.ContainsKey(name.Value.Value))
                    {
                        if (isKey)
                        {
                            strucType = new FName(reader.Asset, reader.Asset.MapStructTypeOverride[name.Value.Value].Item1);
                        }
                        else
                        {
                            strucType = new FName(reader.Asset, reader.Asset.MapStructTypeOverride[name.Value.Value].Item2);
                        }
                    }

                    if (strucType?.Value == null) strucType = FName.DefineDummy(reader.Asset, "Generic");

                    StructPropertyData data = new StructPropertyData(name, strucType);
                    data.Offset = reader.BaseStream.Position;
                    data.Read(reader, false, 1);
                    return data;
                default:
                    var res = MainSerializer.TypeToClass(type, name, reader.Asset, null, leng);
                    res.Offset = reader.BaseStream.Position;
                    res.Read(reader, includeHeader, leng);
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

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            FName type1 = null, type2 = null;
            if (includeHeader)
            {
                type1 = reader.ReadFName();
                type2 = reader.ReadFName();
                PropertyGuid = reader.ReadPropertyGuid();
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

        private void WriteRawMap(AssetBinaryWriter writer, TMap<PropertyData, PropertyData> map)
        {
            foreach (var entry in map)
            {
                entry.Key.Offset = writer.BaseStream.Position;
                entry.Key.Write(writer, false);
                entry.Value.Offset = writer.BaseStream.Position;
                entry.Value.Write(writer, false);
            }
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
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
                writer.WritePropertyGuid(PropertyGuid);
            }

            int here = (int)writer.BaseStream.Position;
            writer.Write(KeysToRemove?.Length ?? 0);
            if (KeysToRemove != null)
            {
                for (int i = 0; i < KeysToRemove.Length; i++)
                {
                    var entry = KeysToRemove[i];
                    entry.Offset = writer.BaseStream.Position;
                    entry.Write(writer, false);
                }
            }

            writer.Write(Value.Count);
            WriteRawMap(writer, Value);
            return (int)writer.BaseStream.Position - here;
        }

        protected override void HandleCloned(PropertyData res)
        {
            MapPropertyData cloningProperty = (MapPropertyData)res;

            var newDict = new TMap<PropertyData, PropertyData>();
            foreach (var entry in this.Value)
            {
                newDict[(PropertyData)entry.Key.Clone()] = (PropertyData)entry.Value.Clone();
            }
            cloningProperty.Value = newDict;

            cloningProperty.KeysToRemove = (PropertyData[])this.KeysToRemove.Clone();

            cloningProperty.KeyType = (FName)this.KeyType.Clone();
            cloningProperty.ValueType = (FName)this.ValueType.Clone();
        }
    }
}