using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using UAssetAPI.StructTypes;

namespace UAssetAPI.PropertyTypes
{
    public class MapPropertyData : PropertyData<OrderedDictionary> // Map
    {
        public string[] dummyEntry = new string[] { string.Empty, string.Empty };
        public OrderedDictionary KeysToRemove = null;

        public MapPropertyData(string name, AssetReader asset) : base(name, asset)
        {
            Type = "MapProperty";
            Value = new OrderedDictionary();
        }

        public MapPropertyData()
        {
            Type = "MapProperty";
            Value = new OrderedDictionary();
        }

        private PropertyData MapTypeToClass(string type, string name, AssetReader asset, BinaryReader reader, int leng, bool includeHeader)
        {
            switch (type)
            {
                case "StructProperty":
                    string strucType = "Generic";
                    switch(name)
                    {
                        case "ColorDatabase":
                            strucType = "LinearColor";
                            break;
                    }

                    StructPropertyData data = new StructPropertyData(name, asset, strucType);
                    data.Read(reader, false, leng);
                    return data;
                default:
                    var res = MainSerializer.TypeToClass(type, name, asset, null, leng);
                    res.Read(reader, includeHeader, leng);
                    return res;
            }
        }

        private OrderedDictionary ReadRawMap(BinaryReader reader, string type1, string type2, int numEntries)
        {
            var resultingDict = new OrderedDictionary();

            PropertyData data1 = null;
            PropertyData data2 = null;
            for (int i = 0; i < numEntries; i++)
            {
                data1 = MapTypeToClass(type1, Name, Asset, reader, 0, false);
                data2 = MapTypeToClass(type2, Name, Asset, reader, 0, false);

                resultingDict.Add(data1, data2);
            }

            return resultingDict;
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng)
        {
            string type1 = null, type2 = null;
            if (includeHeader)
            {
                type1 = Asset.GetHeaderReference((int)reader.ReadInt64());
                type2 = Asset.GetHeaderReference((int)reader.ReadInt64());
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
                dummyEntry = new string[] { type1, type2 };
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
                    writer.Write((long)Asset.SearchHeaderReference(((PropertyData)firstEntry.Key).Type));
                    writer.Write((long)Asset.SearchHeaderReference(((PropertyData)firstEntry.Value).Type));
                }
                else
                {
                    writer.Write((long)Asset.SearchHeaderReference(dummyEntry[0]));
                    writer.Write((long)Asset.SearchHeaderReference(dummyEntry[1]));
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