using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class StructPropertyData : PropertyData<List<PropertyData>> // List
    {
        [JsonProperty]
        public FName StructType = null;
        [JsonProperty]
        public bool SerializeNone = true;
        [JsonProperty]
        public Guid StructGUID = Guid.Empty; // usually set to 0

        public StructPropertyData(FName name) : base(name)
        {
            Value = new List<PropertyData>();
        }

        public StructPropertyData(FName name, FName forcedType) : base(name)
        {
            StructType = forcedType;
            Value = new List<PropertyData>();
        }

        public StructPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("StructProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        private void ReadOnce(AssetBinaryReader reader, Type T, long offset)
        {
            var data = Activator.CreateInstance(T, Name) as PropertyData;
            if (data == null) return;
            data.Offset = offset;
            data.Read(reader, false, 0);
            Value = new List<PropertyData> { data };
        }

        private void ReadNTPL(AssetBinaryReader reader)
        {
            List<PropertyData> resultingList = new List<PropertyData>();
            PropertyData data = null;
            while ((data = MainSerializer.Read(reader, true)) != null)
            {
                resultingList.Add(data);
            }

            Value = resultingList;
        }

        public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader) // originally !isForced
            {
                StructType = reader.ReadFName();
                StructGUID = new Guid(reader.ReadBytes(16));
                reader.ReadByte();
            }

            MainSerializer.PropertyTypeRegistry.TryGetValue(StructType.Value.Value, out RegistryEntry targetEntry);
            bool hasCustomStructSerialization = targetEntry != null && targetEntry.HasCustomStructSerialization;

            if (StructType.Value.Value == "RichCurveKey" && reader.Asset.EngineVersion < UE4Version.VER_UE4_SERIALIZE_RICH_CURVE_KEY) hasCustomStructSerialization = false;

            if (leng1 == 0)
            {
                SerializeNone = false;
                Value = new List<PropertyData>();
                return;
            }

            if (targetEntry != null && hasCustomStructSerialization)
            {
                ReadOnce(reader, targetEntry.PropertyType, reader.BaseStream.Position);
            }
            else
            {
                ReadNTPL(reader);
            }
        }

        private int WriteOnce(AssetBinaryWriter writer)
        {
            if (Value.Count != 1) throw new InvalidOperationException("Structs with type " + StructType.Value.Value + " must have exactly one entry");
            Value[0].Offset = writer.BaseStream.Position;
            return Value[0].Write(writer, false);
        }

        private int WriteNTPL(AssetBinaryWriter writer)
        {
            int here = (int)writer.BaseStream.Position;
            if (Value != null)
            {
                foreach (var t in Value)
                {
                    MainSerializer.Write(t, writer, true);
                }
            }
            writer.Write(new FName("None"));
            return (int)writer.BaseStream.Position - here;
        }

        public override int Write(AssetBinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.Write(StructType);
                writer.Write(StructGUID.ToByteArray());
                writer.Write((byte)0);
            }

            MainSerializer.PropertyTypeRegistry.TryGetValue(StructType.Value.Value, out RegistryEntry targetEntry);
            bool hasCustomStructSerialization = targetEntry != null && targetEntry.HasCustomStructSerialization;

            if (StructType.Value.Value == "RichCurveKey" && writer.Asset.EngineVersion < UE4Version.VER_UE4_SERIALIZE_RICH_CURVE_KEY) hasCustomStructSerialization = false;

            if (targetEntry != null && hasCustomStructSerialization) return WriteOnce(writer);
            if (Value.Count == 0 && !SerializeNone) return 0;
            return WriteNTPL(writer);
        }

        public override void FromString(string[] d, UAsset asset)
        {
            if (d[4] != null) StructType = FName.FromString(d[4]);
            if (StructType == null) StructType = new FName("Generic");
        }

        protected override void HandleCloned(PropertyData res)
        {
            StructPropertyData cloningProperty = (StructPropertyData)res;
            cloningProperty.StructType = (FName)this.StructType.Clone();
            cloningProperty.StructGUID = new Guid(this.StructGUID.ToByteArray());

            List<PropertyData> newData = new List<PropertyData>(this.Value.Count);
            for (int i = 0; i < this.Value.Count; i++)
            {
                newData.Add((PropertyData)this.Value[i].Clone());
            }
            cloningProperty.Value = newData;
        }
    }
}