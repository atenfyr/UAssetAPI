using Newtonsoft.Json;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using UAssetAPI.StructTypes;

namespace UAssetAPI.PropertyTypes
{
    /// <summary>
    /// Describes a map (<see cref="OrderedDictionary"/>).
    /// </summary>
    public class MapPropertyData : PropertyData<TMap<PropertyData, PropertyData>> // Map
    {
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
        public TMap<PropertyData, PropertyData> KeysToRemove = null;

        public MapPropertyData(FName name) : base(name)
        {
            Value = new TMap<PropertyData, PropertyData>();
        }

        public MapPropertyData()
        {
            Value = new TMap<PropertyData, PropertyData>();
        }

        private static readonly FName CurrentPropertyType = new FName("MapProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

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
                            strucType = reader.Asset.MapStructTypeOverride[name.Value.Value].Item1;
                        }
                        else
                        {
                            strucType = reader.Asset.MapStructTypeOverride[name.Value.Value].Item2;
                        }
                    }

                    if (strucType == null) strucType = new FName("Generic");

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
                reader.ReadByte();
            }

            int numKeysToRemove = reader.ReadInt32();
            if (numKeysToRemove > 0) // i haven't ever actually seen this case but the engine has it so here's an untested implementation of it for now
            {
                KeysToRemove = ReadRawMap(reader, type1, type2, numKeysToRemove);
            }

            int numEntries = reader.ReadInt32();
            if (numEntries == 0)
            {
                KeyType = type1;
                ValueType = type2;
            }
            Value = ReadRawMap(reader, type1, type2, numEntries);
        }

        private int WriteRawMap(AssetBinaryWriter writer, TMap<PropertyData, PropertyData> map)
        {
            int here = (int)writer.BaseStream.Position;
            foreach (var entry in map)
            {
                entry.Key.Offset = writer.BaseStream.Position;
                entry.Key.Write(writer, false);
                entry.Value.Offset = writer.BaseStream.Position;
                entry.Value.Write(writer, false);
            }
            return (int)writer.BaseStream.Position - here;
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                if (Value.Count > 0)
                {
                    writer.Write(Value.Keys.ElementAt(0).PropertyType);
                    writer.Write(Value[0].PropertyType);
                }
                else
                {
                    writer.Write(KeyType);
                    writer.Write(ValueType);
                }
                writer.Write((byte)0);
            }

            writer.Write(KeysToRemove?.Count ?? 0);
            if (KeysToRemove != null && KeysToRemove.Count > 0)
            {
                WriteRawMap(writer, KeysToRemove);
            }

            writer.Write(Value.Count);
            return WriteRawMap(writer, Value) + 8;
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

            newDict = new TMap<PropertyData, PropertyData>();
            foreach (var entry in this.KeysToRemove)
            {
                newDict[(PropertyData)entry.Key.Clone()] = (PropertyData)entry.Value.Clone();
            }
            cloningProperty.KeysToRemove = newDict;

            cloningProperty.KeyType = (FName)this.KeyType.Clone();
            cloningProperty.ValueType = (FName)this.ValueType.Clone();
        }
    }
}