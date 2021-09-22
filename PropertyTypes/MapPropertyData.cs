using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using UAssetAPI.StructTypes;

namespace UAssetAPI.PropertyTypes
{
    public class MapPropertyData : PropertyData<OrderedDictionary> // Map
    {
        public FName[] dummyEntry = new FName[] { new FName(string.Empty), new FName(string.Empty) };
        public OrderedDictionary KeysToRemove = null;

        public MapPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Type = new FName("MapProperty");
            Value = new OrderedDictionary();
        }

        public MapPropertyData()
        {
            Type = new FName("MapProperty");
            Value = new OrderedDictionary();
        }

        private PropertyData MapTypeToClass(FName type, FName name, UAsset asset, BinaryReader reader, int leng, bool includeHeader, bool isKey)
        {
            switch (type.Value.Value)
            {
                case "StructProperty":
                    FName strucType = null;

                    if (asset.MapStructTypeOverride.ContainsKey(name.Value.Value))
                    {
                        if (isKey)
                        {
                            strucType = asset.MapStructTypeOverride[name.Value.Value].Item1;
                        }
                        else
                        {
                            strucType = asset.MapStructTypeOverride[name.Value.Value].Item2;
                        }
                    }

                    if (strucType == null) strucType = new FName("Generic");

                    StructPropertyData data = new StructPropertyData(name, asset, strucType);
                    data.Read(reader, false, leng);
                    return data;
                default:
                    var res = MainSerializer.TypeToClass(type, name, asset, null, leng);
                    res.Read(reader, includeHeader, leng);
                    return res;
            }
        }

        private OrderedDictionary ReadRawMap(BinaryReader reader, FName type1, FName type2, int numEntries)
        {
            var resultingDict = new OrderedDictionary();

            PropertyData data1 = null;
            PropertyData data2 = null;
            for (int i = 0; i < numEntries; i++)
            {
                data1 = MapTypeToClass(type1, Name, Asset, reader, 0, false, true);
                data2 = MapTypeToClass(type2, Name, Asset, reader, 0, false, false);

                resultingDict.Add(data1, data2);
            }

            return resultingDict;
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            FName type1 = null, type2 = null;
            if (includeHeader)
            {
                type1 = reader.ReadFName(Asset);
                type2 = reader.ReadFName(Asset);
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
                dummyEntry = new FName[] { type1, type2 };
            }
            Value = ReadRawMap(reader, type1, type2, numEntries);
        }

        private int WriteRawMap(BinaryWriter writer, OrderedDictionary map)
        {
            int here = (int)writer.BaseStream.Position;
            foreach (DictionaryEntry entry in map)
            {
                ((PropertyData)entry.Key).Write(writer, false);
                ((PropertyData)entry.Value).Write(writer, false);
            }
            return (int)writer.BaseStream.Position - here;
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                if (Value.Count > 0)
                {
                    DictionaryEntry firstEntry = Value.Cast<DictionaryEntry>().ElementAt(0);
                    writer.WriteFName(((PropertyData)firstEntry.Key).Type, Asset);
                    writer.WriteFName(((PropertyData)firstEntry.Value).Type, Asset);
                }
                else
                {
                    writer.WriteFName(dummyEntry[0], Asset);
                    writer.WriteFName(dummyEntry[1], Asset);
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
    }
}