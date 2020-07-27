using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using UAssetAPI.StructTypes;

namespace UAssetAPI.PropertyTypes
{
    public class MapPropertyData : PropertyData<OrderedDictionary> // Map
    {
        OrderedDictionary KeysToRemove = null;

        public MapPropertyData(string name, AssetReader asset, bool forceReadNull = true) : base(name, asset, forceReadNull)
        {
            Type = "MapProperty";
            Value = new OrderedDictionary();
        }

        public MapPropertyData()
        {
            Type = "MapProperty";
            Value = new OrderedDictionary();
        }

        private PropertyData MapTypeToClass(string type, string name, AssetReader asset, BinaryReader reader, long leng = 0, bool forceReadNull = true)
        {
            switch (type)
            {
                case "StructProperty":
                    StructPropertyData data = new StructPropertyData(name, asset, forceReadNull);
                    data.SetForced("Generic");
                    data.Read(reader, leng);
                    return data;
                default:
                    var res = MainSerializer.TypeToClass(type, name, asset, null, leng);
                    res.ForceReadNull = forceReadNull;
                    res.ReadInMap(reader, leng);
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

        public override void Read(BinaryReader reader, long leng)
        {
            string type1 = Asset.GetHeaderReference((int)reader.ReadInt64());
            string type2 = Asset.GetHeaderReference((int)reader.ReadInt64());

            int numKeysToRemove = reader.ReadInt32();
            if (numKeysToRemove > 0) // i haven't ever actually seen this case but the engine has it so here's an untested implementation of it for now
            {
                KeysToRemove = ReadRawMap(reader, type1, type2, numKeysToRemove);
            }
            if (ForceReadNull) reader.ReadByte();

            int numEntries = reader.ReadInt32();
            Value = ReadRawMap(reader, type1, type2, numEntries);
        }

        private int WriteRawMap(BinaryWriter writer, OrderedDictionary map)
        {
            int here = (int)writer.BaseStream.Position;
            foreach (DictionaryEntry entry in map)
            {
                ((PropertyData)entry.Key).WriteInMap(writer);
                ((PropertyData)entry.Value).WriteInMap(writer);
            }
            return (int)writer.BaseStream.Position - here;
        }

        public override int Write(BinaryWriter writer)
        {
            var firstEntry = Value.Cast<DictionaryEntry>().ElementAt(0);
            writer.Write((long)Asset.SearchHeaderReference(((PropertyData)firstEntry.Key).Type));
            writer.Write((long)Asset.SearchHeaderReference(((PropertyData)firstEntry.Value).Type));
            writer.Write(KeysToRemove != null ? KeysToRemove.Count : 0);
            if (KeysToRemove != null && KeysToRemove.Count > 0)
            {
                WriteRawMap(writer, KeysToRemove);
            }
            if (ForceReadNull) writer.Write((byte)0);

            writer.Write(Value.Count);
            return WriteRawMap(writer, Value) + 8;
        }
    }
}