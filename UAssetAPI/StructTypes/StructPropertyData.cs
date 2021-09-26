using System;
using System.Collections.Generic;
using System.IO;
using UAssetAPI.PropertyTypes;

namespace UAssetAPI.StructTypes
{
    public class StructPropertyData : PropertyData<IList<PropertyData>> // List
    {
        public FName StructType = null;
        public bool SerializeNone = true;
        public Guid StructGUID = Guid.Empty; // usually set to 0

        public StructPropertyData(FName name, UAsset asset) : base(name, asset)
        {
            Value = new List<PropertyData>();
        }

        public StructPropertyData(FName name, UAsset asset, FName forcedType) : base(name, asset)
        {
            StructType = forcedType;
            Value = new List<PropertyData>();
        }

        public StructPropertyData()
        {

        }

        private static readonly FName CurrentPropertyType = new FName("StructProperty");
        public override FName PropertyType { get { return CurrentPropertyType; } }

        private void ReadOnce(BinaryReader reader, Type T, long offset)
        {
            var data = Activator.CreateInstance(T, Name, Asset) as PropertyData;
            if (data == null) return;
            data.Offset = offset;
            data.Read(reader, false, 0);
            Value = new List<PropertyData> { data };
        }

        private void ReadNTPL(BinaryReader reader)
        {
            IList<PropertyData> resultingList = new List<PropertyData>();
            PropertyData data = null;
            while ((data = MainSerializer.Read(Asset, reader, true)) != null)
            {
                resultingList.Add(data);
            }

            Value = resultingList;
        }

        public override void Read(BinaryReader reader, bool includeHeader, long leng1, long leng2 = 0)
        {
            if (includeHeader) // originally !isForced
            {
                StructType = reader.ReadFName(Asset);
                StructGUID = new Guid(reader.ReadBytes(16));
                reader.ReadByte();
            }

            MainSerializer.PropertyTypeRegistry.TryGetValue(StructType.Value.Value, out RegistryEntry targetEntry);
            bool hasCustomStructSerialization = targetEntry != null && targetEntry.HasCustomStructSerialization;

            if (StructType.Value.Value == "RichCurveKey" && Asset.EngineVersion < UE4Version.VER_UE4_SERIALIZE_RICH_CURVE_KEY) hasCustomStructSerialization = false;

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

        private int WriteOnce(BinaryWriter writer)
        {
            if (Value.Count != 1) throw new InvalidOperationException("Structs with type " + StructType.Value.Value + " must have exactly one entry");
            return Value[0].Write(writer, false);
        }

        private int WriteNTPL(BinaryWriter writer)
        {
            int here = (int)writer.BaseStream.Position;
            if (Value != null)
            {
                foreach (var t in Value)
                {
                    MainSerializer.Write(t, Asset, writer, true);
                }
            }
            writer.WriteFName(new FName("None"), Asset);
            return (int)writer.BaseStream.Position - here;
        }

        public override int Write(BinaryWriter writer, bool includeHeader)
        {
            if (includeHeader)
            {
                writer.WriteFName(StructType, Asset);
                writer.Write(StructGUID.ToByteArray());
                writer.Write((byte)0);
            }

            MainSerializer.PropertyTypeRegistry.TryGetValue(StructType.Value.Value, out RegistryEntry targetEntry);
            bool hasCustomStructSerialization = targetEntry != null && targetEntry.HasCustomStructSerialization;

            if (StructType.Value.Value == "RichCurveKey" && Asset.EngineVersion < UE4Version.VER_UE4_SERIALIZE_RICH_CURVE_KEY) hasCustomStructSerialization = false;

            if (targetEntry != null && hasCustomStructSerialization) return WriteOnce(writer);
            if (Value.Count == 0 && !SerializeNone) return 0;
            return WriteNTPL(writer);
        }

        public override void FromString(string[] d)
        {
            if (d[4] != null) StructType = FName.FromString(d[4]);
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